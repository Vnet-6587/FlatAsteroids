using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flat.Graphics;

namespace Flat.Input
{
    public sealed class FlatMouse
    {
        private static readonly Lazy<FlatMouse> Lazy = new Lazy<FlatMouse>(() => new FlatMouse());

        public static FlatMouse Instance
        {
            get { return Lazy.Value; }
        }

        private MouseState prevMouseState;
        private MouseState currMouseState;

        public Point WindowPosition
        {
            get { return currMouseState.Position; }
        }

        public FlatMouse()
        {
            prevMouseState = Mouse.GetState();
            currMouseState = prevMouseState;
        }

        public void Update()
        {
            prevMouseState = currMouseState;
            currMouseState = Mouse.GetState();
        }

        public bool IsLeftButtonDown()
        {
            return currMouseState.LeftButton == ButtonState.Pressed;
        }
        public bool IsRightButtonDown()
        {
            return currMouseState.RightButton == ButtonState.Pressed;
        }
        public bool IsMiddleButtonDown()
        {
            return currMouseState.MiddleButton == ButtonState.Pressed;
        }

        public bool IsLeftButtonClicked()
        {
            return currMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released;
        }
        public bool IsRightButtonClicked()
        {
            return currMouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released;
        }
        public bool IsMiddleButtonClicked()
        {
            return currMouseState.MiddleButton == ButtonState.Pressed && prevMouseState.MiddleButton == ButtonState.Released;
        }

        public Vector2 GetScreenPosition(Screen screen)
        {
            Rectangle screenDestinationRect = screen.CalculateDestinationRect();

            Point windowPosition = WindowPosition;

            float sx = windowPosition.X - screenDestinationRect.X;
            float sy = windowPosition.Y - screenDestinationRect.Y;

            sx /= screenDestinationRect.Width;
            sy /= screenDestinationRect.Height;

            sx *= screen.Width;
            sy *= screen.Height;

            sy = screen.Height - sy;

            return new Vector2(sx, sy);
        }
    }
}
