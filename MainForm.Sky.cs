using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SharpOcarina
{
    public partial class MainForm
    {
        private readonly Dictionary<string, int> skyTextures = new Dictionary<string, int>();
        private readonly Stopwatch skyClock = Stopwatch.StartNew();
        private double skyLastSeconds;
        private int celestialTime = -1;
        private int sunTexture, moonTexture;
        private bool cutsceneEnvironmentActive;
        private double cutsceneDayTime;
        private double cutsceneEnvironmentFrame;
        private int cutsceneEnvironmentIndex;
        private bool cutsceneLightingOverride;
        private readonly HashSet<int> cutsceneEnvironmentCommands = new HashSet<int>();

        private void InitializeCutsceneEnvironment()
        {
            cutsceneEnvironmentIndex = 0;
            cutsceneLightingOverride = false;
            cutsceneDayTime = 32768; // Noon unless the room supplies a starting time.
            if (!DisableStartTime.Checked && RoomList.SelectedIndex >= 0 && RoomList.SelectedIndex < CurrentScene.Rooms.Count)
            {
                int time = CurrentScene.Rooms[RoomList.SelectedIndex].StartTime;
                if (time != 0xFFFF)
                    cutsceneDayTime = (((time >> 8) * 60 + (time & 255)) / 1440.0 * 65536) % 65536;
            }
            cutsceneEnvironmentFrame = cutsceneplaycurframe;
            cutsceneEnvironmentCommands.Clear();
            cutsceneEnvironmentActive = true;
            UpdateCutsceneEnvironment(cutsceneplaycurframe);
        }

        private void AdvanceCutsceneTime(double frames)
        {
            int speed = RoomList.SelectedIndex >= 0 && RoomList.SelectedIndex < CurrentScene.Rooms.Count
                ? CurrentScene.Rooms[RoomList.SelectedIndex].TimeSpeed : 0;
            // Scene_CommandTimeSettings treats 0xFF as stopped time as well.
            if (speed == 0 || speed == 255) return;
            while (frames > 0)
            {
                // Environment_Update advances once per game frame, twice as fast at night.
                bool day = cutsceneDayTime >= 16384 && cutsceneDayTime < 49152;
                double rate = speed * (day ? 1 : 2);
                double boundary = cutsceneDayTime < 16384 ? 16384 : (day ? 49152 : 65536);
                double step = Math.Min(frames, (boundary - cutsceneDayTime) / rate);
                cutsceneDayTime = (cutsceneDayTime + step * rate) % 65536;
                frames -= step;
            }
        }

        private void UpdateCutsceneEnvironment(double frame)
        {
            if (!cutsceneEnvironmentActive) return;
            // These commands are independent of textbox/transition dispatch. Process all
            // crossed start frames in order, even when a slow redraw skips their duration.
            if (!cutscenePreviewOneCommand)
            {
                while (true)
                {
                    int next = -1;
                    for (int i = 0; i < CurrentScene.Cutscene.Count; i++)
                    {
                        ZCutscene command = CurrentScene.Cutscene[i];
                        if ((command.Marker != CS_ENV && command.Marker != CS_TIME) ||
                            command.StartFrame > frame || cutsceneEnvironmentCommands.Contains(i)) continue;
                        if (next < 0 || command.StartFrame < CurrentScene.Cutscene[next].StartFrame) next = i;
                    }
                    if (next < 0) break;
                    ZCutscene nextCommand = CurrentScene.Cutscene[next];
                    double commandFrame = Math.Max(cutsceneEnvironmentFrame, nextCommand.StartFrame);
                    AdvanceCutsceneTime(commandFrame - cutsceneEnvironmentFrame);
                    cutsceneEnvironmentFrame = commandFrame;
                    if (nextCommand.Data != null)
                    {
                        if (nextCommand.Marker == CS_ENV && nextCommand.Data.Length >= 1)
                        {
                            // Cutscene_Command_SetLighting stores a one-based setting.
                            cutsceneEnvironmentIndex = Math.Max(0, nextCommand.Data[0] - 1);
                            cutsceneLightingOverride = true;
                        }
                        else if (nextCommand.Marker == CS_TIME && nextCommand.Data.Length >= 2)
                        {
                            cutsceneDayTime = ((nextCommand.Data[0] * 60 + nextCommand.Data[1]) / 1440.0 * 65536) % 65536;
                        }
                    }
                    cutsceneEnvironmentCommands.Add(next);
                }
            }
            AdvanceCutsceneTime(Math.Max(0, frame - cutsceneEnvironmentFrame));
            cutsceneEnvironmentFrame = frame;
        }

        private ZCutscene GetSelectedSetTimeCommand()
        {
            if (CurrentScene == null || MarkerSelect == null || MarkerSelect.SelectedIndex < 0 ||
                MarkerSelect.SelectedIndex >= CurrentScene.Cutscene.Count) return null;
            ZCutscene command = CurrentScene.Cutscene[MarkerSelect.SelectedIndex];
            return command.Marker == CS_TIME && command.Data != null && command.Data.Length >= 2 ? command : null;
        }

        private bool IsCutsceneTimePreview()
        {
            return tabControl1.SelectedTab == tabCutscene && GetSelectedSetTimeCommand() != null;
        }

        private int GetSelectedCutsceneEnvironmentIndex()
        {
            if (tabControl1.SelectedTab != tabCutscene || MarkerSelect == null || CutsceneActorListBox == null ||
                MarkerSelect.SelectedIndex < 0 || MarkerSelect.SelectedIndex >= CurrentScene.Cutscene.Count) return -1;
            ZCutscene command = CurrentScene.Cutscene[MarkerSelect.SelectedIndex];
            int item = CutsceneActorListBox.SelectedIndex;
            if (command.Marker != CS_ENV || command.CutsceneActors == null ||
                item < 0 || item >= command.CutsceneActors.Count) return -1;
            // CS_ENV items use Animation for OoT's one-based lighting setting.
            return Math.Max(0, command.CutsceneActors[item].Animation - 1);
        }

        private static Color BlendEnvironmentColor(Color first, Color second, double blend)
        {
            return Color.FromArgb((int)(first.A + (second.A - first.A) * blend),
                (int)(first.R + (second.R - first.R) * blend),
                (int)(first.G + (second.G - first.G) * blend),
                (int)(first.B + (second.B - first.B) * blend));
        }

        private ZEnvironment GetPreviewEnvironment()
        {
            if (CurrentScene == null || CurrentScene.Environments.Count == 0) return null;
            bool playing = cutscenePreviewEnabled && cutsceneEnvironmentActive;
            if (!playing)
            {
                int cutsceneSelection = GetSelectedCutsceneEnvironmentIndex();
                if (cutsceneSelection >= 0)
                    return CurrentScene.Environments[Math.Min(cutsceneSelection, CurrentScene.Environments.Count - 1)];
            }
            int selected = Math.Max(0, Math.Min(playing ? cutsceneEnvironmentIndex : (int)EnvironmentSelect.Value,
                CurrentScene.Environments.Count - 1));
            if ((playing && cutsceneLightingOverride) || !CurrentScene.OutdoorLight ||
                (!playing && tabControl1.SelectedTab != tabRoomEnv && !IsCutsceneTimePreview()))
                return CurrentScene.Environments[selected];

            // z_kankyo.c sTimeBasedLightConfigs: blend within the selected group
            // of four environments without changing the environment being edited.
            int time = GetSkyTime();
            int[] ends = { SkyClockTime(4) + 1, SkyClockTime(6), SkyClockTime(8) + 1,
                SkyClockTime(16), SkyClockTime(17) + 1, SkyClockTime(19) + 1, 65535 };
            int[] first = { 3, 3, 0, 1, 1, 2, 3 };
            int[] second = { 3, 0, 1, 1, 2, 3, 3 };
            int entry = 0;
            while (entry < 6 && time >= ends[entry]) entry++;
            int start = entry == 0 ? 0 : ends[entry - 1];
            double blend = first[entry] == second[entry] ? 0 : (double)(time - start) / (ends[entry] - start);
            int group = selected / 4 * 4;
            ZEnvironment a = CurrentScene.Environments[Math.Min(group + first[entry], CurrentScene.Environments.Count - 1)];
            ZEnvironment b = CurrentScene.Environments[Math.Min(group + second[entry], CurrentScene.Environments.Count - 1)];
            ZEnvironment result = new ZEnvironment(BlendEnvironmentColor(a.C1C, b.C1C, blend), Color.Black,
                BlendEnvironmentColor(a.C3C, b.C3C, blend), Color.Black, BlendEnvironmentColor(a.C5C, b.C5C, blend),
                BlendEnvironmentColor(a.FogColorC, b.FogColorC, blend),
                (ushort)(a.FogDistance + (b.FogDistance - a.FogDistance) * blend),
                (ushort)(a.DrawDistance + (b.DrawDistance - a.DrawDistance) * blend), a.FogUnknown);
            double angle = (time - 32768) * Math.PI / 32768;
            result.C2X = unchecked((byte)(sbyte)(-Math.Sin(angle) * 120));
            result.C2Y = unchecked((byte)(sbyte)(Math.Cos(angle) * 120));
            result.C2Z = unchecked((byte)(sbyte)(Math.Cos(angle) * 20));
            result.C4X = unchecked((byte)-(sbyte)result.C2X);
            result.C4Y = unchecked((byte)-(sbyte)result.C2Y);
            result.C4Z = unchecked((byte)-(sbyte)result.C2Z);
            return result;
        }

        // Room StartTime is stored as hours/minutes, rather than OoT's binary angle clock.
        private int GetSkyTime()
        {
            if (cutscenePreviewEnabled && cutsceneEnvironmentActive)
                return (int)cutsceneDayTime & 65535;
            if (CurrentScene.OutdoorLight && IsCutsceneTimePreview())
            {
                ZCutscene command = GetSelectedSetTimeCommand();
                return (int)((command.Data[0] * 60 + command.Data[1]) / 1440.0 * 65536) & 65535;
            }
            ZScene.ZRoom room = RoomList.SelectedIndex >= 0 && RoomList.SelectedIndex < CurrentScene.Rooms.Count
                ? CurrentScene.Rooms[RoomList.SelectedIndex] : null;
            if (CurrentScene.OutdoorLight && tabControl1.SelectedTab == tabRoomEnv && DisableStartTime.Checked)
                return (int)(((double)TimeHour.Value * 60 + (double)TimeMinute.Value) / 1440 * 65536) & 65535;
            if (!CurrentScene.OutdoorLight || (tabControl1.SelectedTab == tabRoomEnv && !DisableStartTime.Checked))
            {
                int time = room == null ? 0xFFFF : room.StartTime;
                if (time != 0xFFFF)
                    return (int)((((time >> 8) * 60 + (time & 255)) / 1440.0) * 65536) & 65535;
            }
            // Stable representative times for the four outdoor environment entries.
            int[] times = { 0x4000, 0x8000, 0xC000, 0 };
            return times[Math.Max(0, (int)EnvironmentSelect.Value) % 4];
        }

        private static int SkyClockTime(int hour)
        {
            return hour * 65536 / 24;
        }

        // z_kankyo.c gTimeBasedSkyboxConfigs[0/1/3]. ID 5 always uses holy0/holy1;
        // its time table only changes the blend, even during its non-transition periods.
        private void GetSkyTextures(int time, out string first, out string second, out double blend)
        {
            bool holy = !settings.MajorasMask && CurrentScene.SkyboxType == 5;
            if (settings.MajorasMask)
            {
                first = second = "MM/" + (CurrentScene.Cloudy ? "cloudy" : "clear");
                blend = 0;
                return;
            }
            int[] ends = holy
                ? new[] { SkyClockTime(5) + 1, SkyClockTime(6), SkyClockTime(7), SkyClockTime(8) + 1,
                    SkyClockTime(16), SkyClockTime(17) + 1, SkyClockTime(18) + 1, SkyClockTime(19) + 1, 65535 }
                : new[] { SkyClockTime(4) + 1, SkyClockTime(5) + 1, SkyClockTime(6), SkyClockTime(8) + 1,
                    SkyClockTime(16), SkyClockTime(17) + 1, SkyClockTime(18) + 1, SkyClockTime(19) + 1, 65535 };
            int[] a = { 3, 3, 0, 0, 1, 1, 2, 2, 3 };
            int[] b = { 3, 0, 0, 1, 1, 2, 2, 3, 3 };
            int entry = 0;
            while (entry < 8 && time >= ends[entry]) entry++;
            int start = entry == 0 ? 0 : ends[entry - 1];
            blend = a[entry] == b[entry] ? 0 : (int)(255.0 * (time - start) / (ends[entry] - start)) / 255.0;
            string[] names = { "sunrise", "day", "sunset", "night" };
            string prefix = "OOT/" + (CurrentScene.Cloudy ? "overcast_" : "");
            first = holy ? "OOT/holy0" : prefix + names[a[entry]];
            second = holy ? "OOT/holy1" : prefix + names[b[entry]];
        }

        private static string SkyAsset(string name)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "F3DEX2/Skybox/" + name);
        }

        private static int UploadSkyTexture(Bitmap bitmap, int texture)
        {
            if (texture == 0) texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            try
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height,
                    0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            }
            finally { bitmap.UnlockBits(data); }
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            return texture;
        }

        private int GetSkyTexture(string name)
        {
            int texture;
            if (!skyTextures.TryGetValue(name, out texture))
            {
                using (Bitmap bitmap = new Bitmap(SkyAsset(name))) texture = UploadSkyTexture(bitmap, 0);
                skyTextures.Add(name, texture);
            }
            return texture;
        }

        // z_vr_box.c D_8012AF0C: the side faces map T=0,31,62,31,0 across Y=64..-64.
        private static void SkyVertex(int face, int column, int row)
        {
            double u = (column * 31 + 0.5) / 128.0;
            double v = ((face < 4 ? (row <= 2 ? row : 4 - row) : row) * 31 + 0.5)
                / (face < 4 ? 64.0 : 128.0);
            GL.TexCoord2(u, v);
            double x = -64 + column * 32, y = 64 - row * 32;
            switch (face)
            {
                case 0: GL.Vertex3(x, y, -64); break;
                case 1: GL.Vertex3(-x, y, 64); break;
                case 2: GL.Vertex3(-64, y, -x); break;
                case 3: GL.Vertex3(64, y, x); break;
                case 4: GL.Vertex3(x, 64, y); break;
                case 5: GL.Vertex3(x, -64, -y); break;
            }
        }

        private void DrawSkyFaces(string name, int faces, double alpha)
        {
            GL.Color4(1.0, 1.0, 1.0, alpha);
            for (int face = 0; face < faces; face++)
            {
                GL.BindTexture(TextureTarget.Texture2D, GetSkyTexture(name + "_" + (face + 1) + ".ci8.png"));
                GL.Begin(PrimitiveType.Quads);
                for (int row = 0; row < 4; row++)
                    for (int col = 0; col < 4; col++)
                    {
                        SkyVertex(face, col, row);
                        SkyVertex(face, col + 1, row);
                        SkyVertex(face, col + 1, row + 1);
                        SkyVertex(face, col, row + 1);
                    }
                GL.End();
            }
        }

        private void UpdateCelestialTextures(int time, double color, double moonAlpha)
        {
            if (celestialTime == time) return;
            using (Bitmap sun = new Bitmap(64, 64))
            using (Bitmap moon = new Bitmap(SkyAsset("OOT/moon.ia8.png")))
            {
                // SETUPDL_54: RGB = lerp(env, prim, lerp(day, evening, envAlpha));
                // The alpha combiner uses the same environment alpha and intensity blend.
                int[] offsets = { 0, 31, 47 };
                for (int strip = 0; strip < 3; strip++)
                using (Bitmap day = new Bitmap(SkyAsset("OOT/sun_" + (strip + 1) + ".i4.png")))
                using (Bitmap evening = new Bitmap(SkyAsset("OOT/sun_evening_" + (strip + 1) + ".i4.png")))
                    // The exported strips omit their repeated edge row. Restore it for
                    // the 32/17/17-texel loads used by gSunDL, overlapping at Y=31 and 47.
                    for (int y = 0; y <= day.Height; y++)
                        for (int x = 0; x < 64; x++)
                        {
                            int sourceY = Math.Min(y, day.Height - 1);
                            double d = day.GetPixel(x, sourceY).R / 255.0, e = evening.GetPixel(x, sourceY).R / 255.0;
                            double intensity = d * color + e * (1 - color);
                            double alpha = intensity;
                            int green = (int)(color * 255 + ((int)(color * 75) + 180 - color * 255) * intensity);
                            int blue = (int)(color * 255 + ((int)(color * 155) + 100 - color * 255) * intensity);
                            sun.SetPixel(x, offsets[strip] + y, Color.FromArgb((int)(alpha * 255), 255, green, blue));
                        }
                for (int y = 0; y < moon.Height; y++)
                    for (int x = 0; x < moon.Width; x++)
                    {
                        Color pixel = moon.GetPixel(x, y);
                        double i = pixel.R / 255.0;
                        moon.SetPixel(x, y, Color.FromArgb((int)(pixel.A * moonAlpha),
                            (int)(80 + 160 * i), (int)(70 + 185 * i), (int)(20 + 160 * i)));
                    }
                sunTexture = UploadSkyTexture(sun, sunTexture);
                moonTexture = UploadSkyTexture(moon, moonTexture);
            }
            celestialTime = time;
        }

        private static void DrawCelestialQuad(int texture, Vector3d center, double scale, double low, double high,
            Vector3d right, Vector3d up)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Color4(1f, 1f, 1f, 1f);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.5 / 64, 0.5 / 64); GL.Vertex3(center + (right * low + up * low) * scale);
            GL.TexCoord2(63.5 / 64, 0.5 / 64); GL.Vertex3(center + (right * high + up * low) * scale);
            GL.TexCoord2(63.5 / 64, 63.5 / 64); GL.Vertex3(center + (right * high + up * high) * scale);
            GL.TexCoord2(0.5 / 64, 63.5 / 64); GL.Vertex3(center + (right * low + up * high) * scale);
            GL.End();
        }

        private void DrawSkyBackground()
        {
            double seconds = skyClock.Elapsed.TotalSeconds;
            double elapsed = seconds - skyLastSeconds;
            skyLastSeconds = seconds;
            if (!SimulateN64Gfx || CurrentScene == null) return;
            bool supported = settings.MajorasMask
                ? CurrentScene.SkyboxType == 1 || CurrentScene.SkyboxType == 6 || CurrentScene.SkyboxType == 10
                : CurrentScene.SkyboxType == 1 || CurrentScene.SkyboxType == 5;
            ZScene.ZRoom room = RoomList.SelectedIndex >= 0 && RoomList.SelectedIndex < CurrentScene.Rooms.Count
                ? CurrentScene.Rooms[RoomList.SelectedIndex] : null;
            int time = GetSkyTime();
            skyboxangle = (skyboxangle - elapsed * 20 * (CurrentScene.SkyboxType == 5 && !settings.MajorasMask ? 0.005 : 0.001)
                * 180 / Math.PI) % 360;
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            SayakaGL.UcodeSimulator.ApplyEnvironmentProjection(null, false);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.PushAttrib(AttribMask.AllAttribBits);
            int previousProgram = GL.GetInteger(GetPName.CurrentProgram);
            try
            {
                GL.Disable((EnableCap)All.VertexProgram);
                GL.Disable((EnableCap)All.FragmentProgram);
                GL.UseProgram(0);
                GL.ActiveTexture(TextureUnit.Texture1);
                GL.Disable(EnableCap.Texture2D);
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.MatrixMode(MatrixMode.Texture); GL.PushMatrix(); GL.LoadIdentity();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.Disable(EnableCap.TextureGenS);
                GL.Disable(EnableCap.TextureGenT);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                GL.Disable(EnableCap.Lighting);
                GL.Disable(EnableCap.Fog);
                GL.Disable(EnableCap.CullFace);
                GL.Disable(EnableCap.AlphaTest);
                GL.Disable(EnableCap.DepthTest);
                GL.DepthMask(false);
                GL.Enable(EnableCap.Texture2D);
                GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Modulate);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                Matrix4d view;
                GL.GetDouble(GetPName.ModelviewMatrix, out view);
                Matrix4d inverseView = Matrix4d.Invert(view);
                GL.Translate(inverseView.Row3.X, inverseView.Row3.Y, inverseView.Row3.Z);
                double[] matrix = new double[16];
                GL.GetDouble(GetPName.ModelviewMatrix, matrix);
                Vector3d right = new Vector3d(matrix[0], matrix[4], matrix[8]); right.Normalize();
                Vector3d up = new Vector3d(matrix[1], matrix[5], matrix[9]); up.Normalize();
                if (supported && (room == null || !room.DisableSkybox))
                {
                    GL.PushMatrix();
                    GL.Rotate(skyboxangle, 0, 1, 0);
                    string first, second; double blend;
                    GetSkyTextures(time, out first, out second, out blend);
                    int faces = !settings.MajorasMask && CurrentScene.SkyboxType == 5 ? 6 : 5;
                    GL.Disable(EnableCap.Blend);
                    DrawSkyFaces(first, faces, 1);
                    if (blend > 0)
                    {
                        GL.Enable(EnableCap.Blend);
                        DrawSkyFaces(second, faces, blend);
                    }
                    GL.PopMatrix();
                }
                GL.Enable(EnableCap.Blend);
                if (room == null || !room.DisableSunMoon)
                {
                    double angle = (time - 32768) * Math.PI / 32768;
                    Vector3d sun = new Vector3d(-Math.Sin(angle) * 3000, Math.Cos(angle) * 3000, Math.Cos(angle) * 500);
                    double y = sun.Y / 25;
                    double color = Math.Max(0, Math.Min(1, y / 80));
                    double moonAlpha = Math.Max(0, Math.Min(1, -y / 80));
                    UpdateCelestialTextures(time, color, moonAlpha);
                    DrawCelestialQuad(sunTexture, sun, color * 2 + 10, -31, 32, right, up);
                    if (!settings.MajorasMask && moonAlpha > 0)
                        DrawCelestialQuad(moonTexture, -sun, 25 - 15 * Math.Max(0, -y / 120), -15, 16, right, up);
                }
                if (supported && settings.ApplyEnvLighting && CurrentScene.Environments.Count > 0 && (room == null || !room.DisableSkybox))
                {
                    ZEnvironment env = GetPreviewEnvironment();
                    if (env.FogDistance < 980)
                    {
                        GL.Disable(EnableCap.Texture2D);
                        GL.MatrixMode(MatrixMode.Projection); GL.PushMatrix(); GL.LoadIdentity();
                        GL.MatrixMode(MatrixMode.Modelview); GL.PushMatrix(); GL.LoadIdentity();
                        GL.Color4(env.FogColorC.R / 255.0, env.FogColorC.G / 255.0, env.FogColorC.B / 255.0,
                            (int)(255 * Math.Min(1, (1000 - env.FogDistance) * 0.02)) / 255.0);
                        GL.Begin(PrimitiveType.Quads);
                        GL.Vertex2(-1, -1); GL.Vertex2(1, -1); GL.Vertex2(1, 1); GL.Vertex2(-1, 1);
                        GL.End();
                        GL.PopMatrix(); GL.MatrixMode(MatrixMode.Projection); GL.PopMatrix();
                        GL.MatrixMode(MatrixMode.Modelview);
                    }
                }
            }
            finally
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.MatrixMode(MatrixMode.Texture); GL.PopMatrix();
                GL.UseProgram(previousProgram);
                GL.PopAttrib();
                GL.MatrixMode(MatrixMode.Modelview); GL.PopMatrix();
                GL.MatrixMode(MatrixMode.Projection); GL.PopMatrix();
                GL.MatrixMode(MatrixMode.Modelview);
            }
        }
    }
}
