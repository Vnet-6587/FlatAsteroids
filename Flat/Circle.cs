using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Flat
{
    public readonly struct Circle
    {
        public readonly Vector2 Center;
        public readonly float Radius;

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }
        public Circle(float x, float y, float radius)
        {
            Center = new Vector2(x, y);
            Radius = radius;
        }
    }
}
