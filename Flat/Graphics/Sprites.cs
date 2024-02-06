using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flat.Graphics
{
    public sealed class Sprites : IDisposable
    {
        private bool isDisposed;
        private Game game;
        private SpriteBatch sprites;
        private BasicEffect effect;

        public Sprites(Game game)
        {
            if (game == null) 
            {
                throw new ArgumentNullException("game");
            }
            this.game = game;

            isDisposed = false;

            sprites = new SpriteBatch(this.game.GraphicsDevice);

            effect = new BasicEffect(this.game.GraphicsDevice);

            effect.FogEnabled = false;
            effect.TextureEnabled = true;
            effect.LightingEnabled = false;
            effect.VertexColorEnabled = false;

            effect.World = Matrix.Identity;

            effect.Projection = Matrix.Identity;
            effect.View = Matrix.Identity;
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                sprites?.Dispose();
                effect?.Dispose();
                isDisposed = true;
            }
        }

        public void Begin(Camera camera, bool isTextureFilteringEnabled)
        {
            SamplerState sampler = SamplerState.PointClamp;
            if (isTextureFilteringEnabled)
            {
                sampler = SamplerState.LinearClamp;
            }

            if (camera == null)
            {
                Viewport vp = game.GraphicsDevice.Viewport;
                effect.Projection = Matrix.CreateOrthographicOffCenter(0, vp.Width, 0, vp.Height, 0f, 1f);
                effect.View = Matrix.Identity;
            }
            else
            {
                camera.UpdateMatrices();

                effect.View = camera.View;
                effect.Projection = camera.Projection;
            }
            
            sprites.Begin(blendState: BlendState.AlphaBlend, samplerState: sampler, rasterizerState: RasterizerState.CullNone, effect: effect);
        }

        public void End()
        {
            sprites.End();
        }

        public void Draw(Texture2D texture, Vector2 origin, Vector2 position, Color color)
        {
            sprites.Draw(texture, position, null, color, 0f, origin, 1f, SpriteEffects.FlipVertically, 0f);
        }

        public void Draw(Texture2D texture, Rectangle? srcRect, Vector2 origin, Vector2 position, float rotation, Vector2 scale, Color color)
        {
            sprites.Draw(texture, position, srcRect, color, rotation, origin, scale, SpriteEffects.FlipVertically, 0f);
        }

        public void Draw(Texture2D texture, Rectangle? srcRect, Rectangle destinationRect, Color color)
        {
            sprites.Draw(texture, destinationRect, srcRect, color, 0f, Vector2.Zero, SpriteEffects.FlipVertically, 0f);
        }
    }
}
