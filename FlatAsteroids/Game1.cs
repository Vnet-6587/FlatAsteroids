using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Flat;
using Flat.Graphics;
using Flat.Input;

namespace FlatAsteroids
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private Screen screen;
        private Sprites sprites;
        private Shapes shapes;
        private Camera camera;

        private List<Entity> entities;

        private SoundEffect rocketSound;
        private SoundEffectInstance rocketSoundInstance;
        private bool hitboxDraw;
        private float playerSpeed = 50f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
        }

        protected override void Initialize()
        {
            DisplayMode dm = GraphicsDevice.DisplayMode;
            graphics.PreferredBackBufferWidth = (int)(dm.Width * 0.8f);
            graphics.PreferredBackBufferHeight = (int)(dm.Height * 0.8f);
            graphics.ApplyChanges();

            screen = new Screen(this, 1280, 720);
            sprites = new Sprites(this);
            shapes = new Shapes(this);
            camera = new Camera(screen);

            Random rand = new Random(0);

            entities = new List<Entity>();

            Vector2[] vertices = new Vector2[5];
            vertices[0] = new Vector2(10, 0);
            vertices[1] = new Vector2(-10, -10);
            vertices[2] = new Vector2(-6, -3);
            vertices[3] = new Vector2(-6, 3);
            vertices[4] = new Vector2(-10, 10);

            MainShip player1 = new MainShip(vertices, new Vector2(0, 0), Color.LightGreen);
            entities.Add(player1);

            MainShip player2 = new MainShip(vertices, new Vector2(0, 0), Color.LightBlue);
            entities.Add(player2);

            int asteroidCount = 10;

            for(int i = 0; i < asteroidCount; i++)
            {
                Asteroid asteroid = new Asteroid(rand, camera);
                entities.Add(asteroid);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            rocketSound = Content.Load<SoundEffect>("expl05");
            rocketSoundInstance = rocketSound.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            FlatKeyboard keyboard = FlatKeyboard.Instance;
            keyboard.Update();

            FlatMouse mouse = FlatMouse.Instance;
            mouse.Update();

            if (keyboard.IsKeyClicked(Keys.OemPlus)) 
            {
                camera.IncZoom();
            }
            if (keyboard.IsKeyClicked(Keys.OemMinus))
            {
                camera.DecZoom();
            }

            MainShip player1 = (MainShip)entities[0];
            MainShip player2 = (MainShip)entities[1];

            float playerRotationAmount = MathHelper.TwoPi * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboard.IsKeyDown(Keys.Left))
            {
                player1.Rotate(playerRotationAmount);
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                player1.Rotate(-playerRotationAmount);
            }
            if (keyboard.IsKeyDown(Keys.Up)) 
            {
                player1.ApplyRocketForce(playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                if (rocketSoundInstance.State != SoundState.Playing)
                {
                    rocketSoundInstance.Volume = 0.2f;
                    rocketSoundInstance.Play();
                }
            }
            else if (!keyboard.IsKeyDown(Keys.W))
            {
                player1.DisableRocketForce();
                if (rocketSoundInstance.State == SoundState.Playing)
                {
                    rocketSoundInstance.Stop();
                }
            }
            else
            {
                player1.DisableRocketForce();
            }

            if (keyboard.IsKeyDown(Keys.A))
            {
                player2.Rotate(playerRotationAmount);
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                player2.Rotate(-playerRotationAmount);
            }
            if (keyboard.IsKeyDown(Keys.W))
            {
                player2.ApplyRocketForce(playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                if (rocketSoundInstance.State != SoundState.Playing)
                {
                    rocketSoundInstance.Volume = 0.2f;
                    rocketSoundInstance.Play();
                }
            }
            else if (!keyboard.IsKeyDown(Keys.Up))
            {
                player2.DisableRocketForce();
                if (rocketSoundInstance.State == SoundState.Playing)
                {
                    rocketSoundInstance.Stop();
                }
            }
            else
            {
                player2.DisableRocketForce();
            }

            if (keyboard.IsKeyDown(Keys.OemTilde))
            {
                hitboxDraw = true;
            }
            else
            {
                hitboxDraw = false;
            }

            foreach (var entity in entities)
            {
                entity.Update(gameTime, camera);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            screen.Set();
            GraphicsDevice.Clear(Color.Black);

            shapes.Begin(camera);

            foreach (var entity in entities)
            {
                entity.Draw(shapes);
                if (hitboxDraw)
                {
                    entity.DrawHitboxes(shapes);
                }
            }

            shapes.End();

            screen.UnSet();
            screen.Present(sprites);



            base.Draw(gameTime);
        }
    }
}