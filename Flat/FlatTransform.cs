using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flat
{
    public struct FlatTransform
    {
        public float PosX;
        public float PosY;
        public float CosScaleX;
        public float SinScaleX;
        public float CosScaleY;
        public float SinScaleY;

        public FlatTransform(Vector2 position, float angle, Vector2 scale)
        {
            float sin = MathF.Sin(angle);
            float cos = MathF.Cos(angle);

            PosX = position.X;
            PosY = position.Y;
            CosScaleX = cos * scale.X;
            SinScaleX = sin * scale.X;
            CosScaleY = cos * scale.Y;
            SinScaleY = sin * scale.Y;
        }
        public FlatTransform(Vector2 position, float angle, float scale)
        {
            float sin = MathF.Sin(angle);
            float cos = MathF.Cos(angle);

            PosX = position.X;
            PosY = position.Y;
            CosScaleX = cos * scale;
            SinScaleX = sin * scale;
            CosScaleY = cos * scale;
            SinScaleY = sin * scale;
        }

        public Matrix ToMatrix()
        {
            Matrix result = Matrix.Identity;
            result.M11 = CosScaleX;
            result.M12 = SinScaleY;
            result.M21 = -SinScaleX;
            result.M22 = CosScaleY;
            result.M41 = PosX;
            result.M42 = PosY;

            return result;
        }
    }
}
