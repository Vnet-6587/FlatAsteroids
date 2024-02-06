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
    public class MainShip : Entity
    {
        private bool isRocketForce;
        private Vector2[] rocketVertices1;
        private Vector2[] rocketVertices2;
        private Vector2[] rocketVertices3;
        private double randomRocketTime;
        private double randomRocketStartTime;

        public MainShip(Vector2[] vertices, Vector2 position, Color color)
            : base(vertices, position, color)
        {
            isRocketForce = false;

            rocketVertices1 = new Vector2[3];
            rocketVertices1[0] = this.vertices[3];
            rocketVertices1[1] = this.vertices[2];
            rocketVertices1[2] = new Vector2(-24, 0);

            rocketVertices2 = new Vector2[3];
            rocketVertices2[0] = this.vertices[3];
            rocketVertices2[1] = this.vertices[2];
            rocketVertices2[2] = new Vector2(-16, -8);

            rocketVertices3 = new Vector2[3];
            rocketVertices3[0] = this.vertices[3];
            rocketVertices3[1] = this.vertices[2];
            rocketVertices3[2] = new Vector2(-16, 8);

            randomRocketTime = 60d;
            randomRocketStartTime = 0d;
        }

        public void Rotate(float amount)
        {
            angle += amount;
            if (angle < 0f) 
            {
                angle += MathHelper.TwoPi;
            }
            if (angle >= MathHelper.TwoPi)
            {
                angle -= MathHelper.TwoPi;
            }
        }

        public override void Update(GameTime gameTime, Camera camera)
        {
            double now = gameTime.TotalGameTime.TotalMilliseconds;

            if(now - this.randomRocketStartTime >= randomRocketTime)
            {
                randomRocketStartTime = now;

                float rocketMinX = -26f;
                float rocketMaxX = -24f;
                float rocketMinY = -2f;
                float rocketMaxY = 2f;
                float shift = 6;

                rocketVertices1[2] = new Vector2(RandomHelper.RandomSingle(rocketMinX, rocketMaxX), 
                    RandomHelper.RandomSingle(rocketMinY, rocketMaxY));
                rocketVertices2[2] = new Vector2(RandomHelper.RandomSingle(rocketMinX + shift, rocketMaxX + shift),
                    RandomHelper.RandomSingle(rocketMinY - shift, rocketMaxY - shift));
                rocketVertices3[2] = new Vector2(RandomHelper.RandomSingle(rocketMinX + shift, rocketMaxX + shift),
                    RandomHelper.RandomSingle(rocketMinY + shift, rocketMaxY + shift));
            }

            base.Update(gameTime, camera);
        }

        public override void Draw(Shapes shapes)
        {
            if(isRocketForce)
            {
                FlatTransform transform = new FlatTransform(position, angle, 1f);
                shapes.DrawPolygon(rocketVertices1, transform, 1f, Color.Orange);
                shapes.DrawPolygon(rocketVertices2, transform, 1f, Color.Orange);
                shapes.DrawPolygon(rocketVertices3, transform, 1f, Color.Orange);
            }

            base.Draw(shapes);
        }

        public void ApplyRocketForce(float amount)
        {
            Vector2 forceDir = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
            velocity += forceDir * amount;
            isRocketForce = true;
        }

        public void DisableRocketForce()
        {
            isRocketForce = false;
        }
    }
}
