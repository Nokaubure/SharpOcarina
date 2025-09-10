#region --- About ---
/*
 * Project SayakaGL
 */
#endregion

#region --- Using Directives ---
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
#endregion

namespace SharpOcarina
{
    public static class Camera
    {
        #region Variables

        public static float Speed = 0.01f;
        public static float CameraCoeff = 0.05f;
        public static Vector3d Pos, Rot;
        public static Vector2d MouseCoord;
        

        public static Keys[] CameraControlKeys = new Keys[0];

        #endregion

        #region Camera Functions

        public static void Initialize()
        {
            Pos = new Vector3d(0.0f, 0.0f, -5.0f);
            Rot = new Vector3d(0.0f, 0.0f, 0.0f);

            switch (Program.KeyboardLayout)
            {
                case "AZERTY":
                    CameraControlKeys = new Keys[] { Keys.Z, Keys.Q, Keys.S, Keys.D, Keys.A, Keys.E }; break;
                case "DVORAK":
                    CameraControlKeys = new Keys[] { Keys.Oemcomma, Keys.A, Keys.O, Keys.E, Keys.OemQuotes, Keys.OemPeriod }; break;
                default:
                    CameraControlKeys = new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D, Keys.Q, Keys.E }; break;
            }
        }

        public static int GetMaxCameraXRotation()
        {
            if (MainForm.settings.FullCameraRotation) return 180;
            else return 90;
        }

        public static void MouseCenter(Vector2d NewMouseCoord)
        {
            MouseCoord = NewMouseCoord;
        }

        public static void MouseMove(Vector2d NewMouseCoord)
        {
            bool Changed = false;
            double Dx = 0.0f, Dy = 0.0f;

            if (NewMouseCoord.X != MouseCoord.X)
            {
                Dx = (NewMouseCoord.X - MouseCoord.X) * Speed;
                Changed = true;
            }
            if (NewMouseCoord.Y != MouseCoord.Y)
            {
                Dy = (NewMouseCoord.Y - MouseCoord.Y) * Speed;
                Changed = true;
            }

            if (Changed)
            {
                if ((MouseCoord.X < NewMouseCoord.X))
                {
                    Rot.Y += ((NewMouseCoord.X - MouseCoord.X) * (CameraCoeff * 5.0f)) * (Math.Abs(Rot.X) > 90 ? -1.0f : 1.0f);
                    if (Rot.Y > 360) Rot.Y = 0;
                }
                else
                {
                    Rot.Y -= ((MouseCoord.X - NewMouseCoord.X) * (CameraCoeff * 5.0f)) * (Math.Abs(Rot.X) > 90 ? -1.0f : 1.0f);
                    if (Rot.Y < -360) Rot.Y = 0;
                }
                
                if (MouseCoord.Y < NewMouseCoord.Y)
                {
                    if(Rot.X >= GetMaxCameraXRotation())
                        Rot.X = MainForm.settings.FullCameraRotation ? -Rot.X : GetMaxCameraXRotation();
                    else
                        Rot.X += (Dy / Speed) * (CameraCoeff * 5.0f);
                }
                else
                {
                    if (Rot.X <= -GetMaxCameraXRotation())
                        Rot.X = MainForm.settings.FullCameraRotation ? -Rot.X : -GetMaxCameraXRotation();
                    else
                        Rot.X += (Dy / Speed) * (CameraCoeff * 5.0f);
                }
            }

            MouseCoord = NewMouseCoord;
        }

        public static void KeyUpdate(bool[] KeysDown)
        {
            const int yawSign = -1;
            const int zForwardSign = +1;

            float speed = CameraCoeff * 2.0f;
            if (KeysDown[(char)Keys.Space]) speed *= 10.0f;
            else if (KeysDown[(char)Keys.ShiftKey]) speed *= 0.25f;

            double yaw = yawSign * Rot.Y * Math.PI / 180.0;
            double pitch = Rot.X * Math.PI / 180.0;

            // Camera forward (yaw around Y, pitch around X)
            Vector3d forward = new Vector3d(
                (float)(Math.Cos(pitch) * Math.Sin(yaw)),
                (float)(Math.Sin(pitch)),
                (float)(zForwardSign * Math.Cos(pitch) * Math.Cos(yaw))
            );

            // Build a stable, screen-true basis
            Vector3d worldUp = Vector3d.UnitY;

            // Right = normalize( worldUp x forward )  -> gives +X when facing +Z
            Vector3d right = Vector3d.Cross(worldUp, forward);
            double rLen = right.Length;
            if (rLen < 1e-6f)
            {
                // Looking nearly straight up/down: derive a horizontal right from yaw
                right = new Vector3d(
                    (float)Math.Sin(yaw - Math.PI / 2.0),
                    0f,
                    (float)(zForwardSign * Math.Cos(yaw - Math.PI / 2.0))
                );
            }
            else
            {
                right /= rLen;
            }
            if (Math.Abs(Rot.X) < 90f)
            {
                //forward = -forward;
                right = -right;
            }
            // Make the basis orthonormal (helps numerical stability)
            Vector3d upCam = Vector3d.Normalize(Vector3d.Cross(forward, right));

            // Input -> camera-relative movement
            Vector3d move = Vector3d.Zero;
            if (KeysDown[(char)CameraControlKeys[0]]) move += forward; // W
            if (KeysDown[(char)CameraControlKeys[2]]) move -= forward; // S
            if (KeysDown[(char)CameraControlKeys[1]]) move -= right;   // A
            if (KeysDown[(char)CameraControlKeys[3]]) move += right;   // D

            // Vertical in world space (keep as you had it)
            if (KeysDown[(char)CameraControlKeys[5]]) move += worldUp; // Up
            if (KeysDown[(char)CameraControlKeys[4]]) move -= worldUp; // Down

            if (move.LengthSquared > 0f)
            {
                move = Vector3d.Normalize(move) * speed;
                Pos += move;
            }
        }



        public static void Position()
        {
            GL.Rotate(Rot.Z, 0.0f, 0.0f, 1.0f);
            GL.Rotate(Rot.X, 1.0f, 0.0f, 0.0f);
            GL.Rotate(Rot.Y, 0.0f, 1.0f, 0.0f);
            GL.Translate(Pos);
        }

        #endregion
    }
}
