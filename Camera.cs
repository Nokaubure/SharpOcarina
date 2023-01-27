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

        #endregion

        #region Camera Functions

        public static void Initialize()
        {
            Pos = new Vector3d(0.0f, 0.0f, -5.0f);
            Rot = new Vector3d(0.0f, 0.0f, 0.0f);
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
                if (MouseCoord.X < NewMouseCoord.X)
                {
                    Rot.Y += (NewMouseCoord.X - MouseCoord.X) * (CameraCoeff * 5.0f);
                    if (Rot.Y > 360) Rot.Y = 0;
                }
                else
                {
                    Rot.Y -= (MouseCoord.X - NewMouseCoord.X) * (CameraCoeff * 5.0f);
                    if (Rot.Y < -360) Rot.Y = 0;
                }
                
                if (MouseCoord.Y < NewMouseCoord.Y)
                {
                    if(Rot.X >= 90)
                        Rot.X = 90;
                    else
                        Rot.X += (Dy / Speed) * (CameraCoeff * 5.0f);
                }
                else
                {
                    if (Rot.X <= -90)
                        Rot.X = -90;
                    else
                        Rot.X += (Dy / Speed) * (CameraCoeff * 5.0f);
                }
            }

            MouseCoord = NewMouseCoord;
        }

        public static void KeyUpdate(bool[] KeysDown)
        {
            double RotYRad = (Rot.Y / 180.0f * Math.PI);
            double RotXRad = (Rot.X / 180.0f * Math.PI);

            double Modifier = 1.0f;
            if (KeysDown[(char)Keys.Space]) Modifier = 10.0f;
            else if (KeysDown[(char)Keys.ShiftKey]) Modifier = 0.25f;

            if (KeysDown[(char)Keys.W])
            {
                if (Rot.X >= 90.0f || Rot.X <= -90.0f)
                {
                    Pos.Y += (float)Math.Sin(RotXRad) * CameraCoeff * 2.0f * Modifier;
                }
                else
                {
                    Pos.X -= (float)Math.Sin(RotYRad) * CameraCoeff * 2.0f * Modifier;
                    Pos.Z += (float)Math.Cos(RotYRad) * CameraCoeff * 2.0f * Modifier;
                    Pos.Y += (float)Math.Sin(RotXRad) * CameraCoeff * 2.0f * Modifier;
                }
            }

            if (KeysDown[(char)Keys.S])
            {
                if (Rot.X >= 90.0f || Rot.X <= -90.0f)
                {
                    Pos.Y -= (float)Math.Sin(RotXRad) * CameraCoeff * 2.0f * Modifier;
                }
                else
                {
                    Pos.X += (float)Math.Sin(RotYRad) * CameraCoeff * 2.0f * Modifier;
                    Pos.Z -= (float)Math.Cos(RotYRad) * CameraCoeff * 2.0f * Modifier;
                    Pos.Y -= (float)Math.Sin(RotXRad) * CameraCoeff * 2.0f * Modifier;
                }
            }

            if (KeysDown[(char)Keys.A])
            {
                Pos.X += (float)Math.Cos(RotYRad) * CameraCoeff * 2.0f * Modifier;
                Pos.Z += (float)Math.Sin(RotYRad) * CameraCoeff * 2.0f * Modifier;
            }

            if (KeysDown[(char)Keys.D])
            {
                Pos.X -= (float)Math.Cos(RotYRad) * CameraCoeff * 2.0f * Modifier;
                Pos.Z -= (float)Math.Sin(RotYRad) * CameraCoeff * 2.0f * Modifier;
            }

            if (KeysDown[(char)Keys.Q])
            {
                Pos.Y -= 1 * CameraCoeff * 2.0f * Modifier;
            }

            if (KeysDown[(char)Keys.E])
            {
                Pos.Y += 1 * CameraCoeff * 2.0f * Modifier;
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
