using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    class Rocket : DrawableGameComponent
    {
        Texture2D explodeTexture;
        public Texture2D Texture { get; private set; }

        SoundEffect soundFx;
        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        int speed = 12;
        bool isDead = false;
        public bool GameIsOver = false;

        const int DEATH_DURATION = 1;
        double deathTimer = 0.0;

        int currentScore;
        int rocketWidth;
        int rocketHeight;

        public Rocket(Game game) : base(game)
        {
            if (Game.Services.GetService<Rocket>() == null)
            {
                Game.Services.AddService<Rocket>(this);
            }
        }

        /// <summary>
        /// This will be called to update the frame 
        /// Here will be checking for collisions between rocket and asteroid
        /// if collision happened then will have to handle by ending the game
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            rocketWidth = Texture.Width;
            rocketHeight = Texture.Height;

            for (int i = 0; i < Game.Components.Count; i++)
            {
                GameComponent component = (GameComponent)Game.Components[i];

                if (component is Asteroid)
                {
                    Asteroid asteroid = (Asteroid)component;
                    Vector2 posAsteroid = asteroid.Position;
                    int asteroidWidth = asteroid.Texture.Width;
                    int asteroidHeight = asteroid.Texture.Height;

                    if (Math.Abs(Position.X - posAsteroid.X) <= (asteroidWidth + rocketWidth) / 2 && Math.Abs(Position.Y - posAsteroid.Y) <= (asteroidHeight + rocketHeight) / 2)
                    {
                        Game.Components.RemoveAt(i);
                        HandleCollision();
                        break;
                    }
                }
            }
          
            if (isDead)
            {
                deathTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (deathTimer >= DEATH_DURATION)
                {
                    ((Game1)Game).HideAllScenes();
                    Game.Services.GetService<GameEndScene>().Show();
                    currentScore = Game.Services.GetService<Score>().GetScore();

                    if (currentScore > 0)
                    {
                        Game.Services.GetService<HighscoreScene>().AddNewHighScore(currentScore);
                    }
                    
                    isDead = false;
                }
            }
            else
            {
                KeyboardState ks = Keyboard.GetState();
                if (ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W))
                {
                    position.Y -= speed;
                }
                else if (ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S))
                {
                    position.Y += speed;
                }
                position.Y = MathHelper.Clamp(position.Y, 0, Game.GraphicsDevice.Viewport.Height - Texture.Height);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This will be called when the game draws itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            if (isDead)
            {
                sb.Draw(explodeTexture, position, Color.White);
            }
            else
            {
                sb.Draw(Texture, position, null, Color.White, 0f, new Vector2(20, 0), 1f, SpriteEffects.None, 0f);
            }

            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// This will be called when the game loads the content
        /// </summary>
        protected override void LoadContent()
        {
            Texture = Game.Content.Load<Texture2D>("Images/rocketship");
            explodeTexture = Game.Content.Load<Texture2D>("Images/explosion");

            soundFx = Game.Content.Load<SoundEffect>("MusicAndSounds/Explosion");

            position = new Vector2(0, (GraphicsDevice.Viewport.Height - Texture.Height) / 2 + 100);

            base.LoadContent();
        }

        internal void HandleCollision()
        {
            isDead = true;
            soundFx.Play();
            GameIsOver = true;
        }
    }
}
