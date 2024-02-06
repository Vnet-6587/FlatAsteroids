using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Flat;
using Flat.Graphics;

namespace FlatAsteroids
{
    public class Asteroid : Entity
    {
        public Asteroid(Random rand, Camera camera)
            : base(null, Vector2.Zero, Color.Gray)
        {
            int minPoints = 6;
            int maxPoints = 10;

            int points = rand.Next(minPoints, maxPoints);

            vertices = new Vector2[points];

            float deltaAngle = MathHelper.TwoPi / (float)points;
            float angle = 0f;

            float minDist = 12f;
            float maxDist = 24f;

            for(int i = 0; i < points; i++)
            {
                float dist = RandomHelper.RandomSingle(rand, minDist, maxDist);

                float x = MathF.Cos(angle) * dist;
                float y = MathF.Sin(angle) * dist;

                vertices[i] = new Vector2(x, y);

                angle += deltaAngle;
            }

            camera.GetExtents(out Vector2 camMin, out Vector2 camMax);

            camMin *= 0.75f;
            camMax *= 0.75f;

            float px = RandomHelper.RandomSingle(rand, camMin.X, camMax.X);
            float py = RandomHelper.RandomSingle(rand, camMin.Y, camMax.Y);

            position = new Vector2(px, py);

            float minSpeed = 20f;
            float maxSpeed = 40f;

            Vector2 velDir = RandomHelper.RandomDirection(rand);
            float speed = RandomHelper.RandomSingle(rand, minSpeed, maxSpeed);

            velocity = velDir * speed;

            radius = FindCollisionCircleRadius(vertices);
        }
    }
}
