using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFlash.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Empty.GameObjects
{
    public static class CameraMover
    {
        public static int prevMouseX;
        public static int prevMouseY;

        public static int previousMouseWheelValue;
        public static int currentMouseWheelValue;

        public static void UpdateCamera(this Camera camera)
        {

            Vector2 cameraMovement = Vector2.Zero;
            float moveSpeed = (3) / camera.Zoom;




            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {

                cameraMovement.X = moveSpeed * (prevMouseX - Mouse.GetState().X);
            }

            prevMouseX = Mouse.GetState().X;
            prevMouseY = Mouse.GetState().Y;




            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

            if (currentMouseWheelValue > previousMouseWheelValue)
            {
                camera.Zoom += .05f;
            }

            if (currentMouseWheelValue < previousMouseWheelValue)
            {
                camera.Zoom -= .05f;
            }

            camera.Pos+=cameraMovement/5f;
        }
    }
}
