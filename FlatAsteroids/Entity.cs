using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Flat;
using Flat.Graphics;
using Flat.Physics;

namespace FlatAsteroids
{
    public abstract class Entity
    {
        protected Vector2[] vertices;
        protected Vector2 position;
        protected Vector2 velocity;
        protected float angle;
        protected Color color;
        protected float radius;

        public Entity(Vector2[] vertices, Vector2 position, Color color) 
        {
            this.vertices = vertices;
            this.position = position;
            this.color = color;

            velocity = Vector2.Zero;
            angle = 0f;

            if(vertices != null)
            {
                radius = FindCollisionCircleRadius(vertices);
            }
        }

        protected static float FindCollisionCircleRadius(Vector2[] vertices)
        {
            float polygonArea = PolygonHelper.FindPolygonArea(vertices);
            return MathF.Sqrt(polygonArea / MathHelper.Pi);
        }

        public virtual void Update(GameTime gameTime, Camera camera)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            camera.GetExtents(out Vector2 camMin, out Vector2 camMax);

            float cameraViewWidth = camMax.X - camMin.X;
            float cameraViewHeight = camMax.Y - camMin.Y;

            if (position.X < camMin.X) { position.X += cameraViewWidth; }
            if (position.X > camMax.X) { position.X -= cameraViewWidth; }
            if (position.Y < camMin.Y) { position.Y += cameraViewHeight; }
            if (position.Y > camMax.Y) { position.Y -= cameraViewHeight; }
        }

        public virtual void Draw(Shapes shapes)
        {
            FlatTransform transform = new FlatTransform(position, angle, 1f);
            shapes.DrawPolygon(vertices, transform, 1f, color);

            //shapes.DrawCircle(position.X, position.Y, radius, 32, 1f, Color.Red);
;
        }

        public virtual void DrawHitboxes(Shapes shapes)
        {
            shapes.DrawCircle(position.X, position.Y, radius, 32, 1f, Color.Red);
;       }
    }
}
