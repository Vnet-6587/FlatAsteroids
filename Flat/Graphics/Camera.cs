using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flat.Graphics
{
    public sealed class Camera
    {
        public readonly static float MinZ = 1f;
        public readonly static float MaxZ = 2048f;

        public readonly static int MinZoom = 1;
        public readonly static int MaxZoom = 20;

        private Vector2 position;
        private float baseZ;
        private float z;

        private float aspectRatio;
        private float fieldOfView;

        private Matrix view;
        private Matrix projection;

        private int zoom;

        public Vector2 Position 
        { 
            get { return position; } 
        }
        public float Z 
        { 
            get { return z; } 
        }
        public float BaseZ
        {
            get { return baseZ; }
        }
        public Matrix View
        {
            get { return view; }
        }
        public Matrix Projection
        {
            get { return projection; }
        }
        public Camera(Screen screen)
        {
            if (screen == null)
            {
                throw new ArgumentNullException("screen");
            }

            aspectRatio = (float)screen.Width / screen.Height;
            fieldOfView = MathHelper.PiOver2;

            position = new Vector2(0, 0);
            baseZ = GetZFromHeight(screen.Height);
            z = baseZ;

            UpdateMatrices();

            zoom = 1;
        }

        public void UpdateMatrices()
        {
            view = Matrix.CreateLookAt(new Vector3(0, 0, z), Vector3.Zero, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, MinZ, MaxZ);
        }

        public float GetZFromHeight(float height) 
        {
            return (0.5f * height) / MathF.Tan(0.5f * fieldOfView);
        }

        public float GetHeightFromZ()
        {
            return z * MathF.Tan(0.5f *fieldOfView) * 2f;
        }

        public void MoveZ(float amount)
        {
            z += amount;
            z = Util.Clamp(z, MinZ, MaxZ);
        }

        public void ResetZ()
        {
            z = baseZ;
        }

        public void Move(Vector2 amount)
        {
            position += amount;
        }

        public void MoveTo(Vector2 destination)
        {
            position = destination;
        }

        public void IncZoom()
        {
            zoom++;
            zoom = Util.Clamp(zoom, MinZoom, MaxZoom);
            z = baseZ / zoom;
        }
        
        public void DecZoom()
        {
            zoom--;
            zoom = Util.Clamp(zoom, MinZoom, MaxZoom);
            z = baseZ / zoom;
        }

        public void SetZoom(int amount)
        {
            zoom = amount;
            zoom = Util.Clamp(zoom, MinZoom, MaxZoom);
            z = baseZ / zoom;
        }

        public void GetExtents(out float width, out float height)
        {
            height = GetHeightFromZ();
            width = height * aspectRatio;
        }

        public void GetExtents(out float left, out float right, out float bottom, out float top)
        {
            GetExtents(out float width, out float height);

            left = position.X - width * 0.5f;
            right = left + width;
            bottom = position.Y - height * 0.5f;
            top = bottom + height;
        }

        public void GetExtents(out Vector2 min, out Vector2 max) 
        {
            GetExtents(out float left, out float right, out float bottom, out float top);

            min = new Vector2(left, bottom);
            max = new Vector2(right, top);
        }
    }
}
