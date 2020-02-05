using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProjectShell
{
    class AsteroidManager : GameComponent
    {
        Texture2D texture;
        Random random = new Random();

        int timer = 0;
        int asteroidCreationTime = 110;
        GameScene parent;

        public AsteroidManager(Game game, GameScene parent) : base(game)
        {
            this.parent = parent;
            if (Game.Services.GetService<AsteroidManager>() == null)
            {
                Game.Services.AddService<AsteroidManager>(this);
            }
        }

        public override void Initialize()
        {
            texture = Game.Content.Load<Texture2D>("Images/asteroid");
            base.Initialize();
        }

        /// <summary>
        /// Will update the game 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            timer++;
            if (timer >= asteroidCreationTime)
            {
                timer = 0;
                asteroidCreationTime--;
                if (asteroidCreationTime < 40)
                {
                    asteroidCreationTime = 40;
                }
                CreateNewAsteroid();
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// Will create a new Asteroid based on the position values and speed we gave
        /// </summary>
        private void CreateNewAsteroid()
        {
            int height = Game.GraphicsDevice.Viewport.Height;

            Vector2 position = new Vector2(Game.GraphicsDevice.Viewport.Width, height / 2 + random.Next(-height / 2, height / 2));
            parent.AddComponent(new Asteroid(Game, texture, position, new Vector2(random.Next(4, 16), random.Next(-2, 2))));
        }
        
        internal void ResetAsteroid()
        {
            timer = 0;
            asteroidCreationTime = 110;
        }

    }
}
