using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProjectShell
{
    class StarManager : GameComponent
    {
        Texture2D texture;
        Random random = new Random();

        int timer = 0;
        int starCreationTime = 80;
        GameScene parent;

        public StarManager(Game game, GameScene parent) : base(game)
        {
            this.parent = parent;
            if (Game.Services.GetService<StarManager>() == null)
            {
                Game.Services.AddService<StarManager>(this);
            }
        }

        public override void Initialize()
        {
            texture = Game.Content.Load<Texture2D>("Images/StarAnimation/11");
        }

        public override void Update(GameTime gameTime)
        {
            timer++;
            if (timer >= starCreationTime)
            {
                timer = 0;
                starCreationTime--;
                if (starCreationTime < 40)
                {
                    starCreationTime = 40;
                }
                CreateNewStar();
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// Will create a new star with values of position and speed provided
        /// </summary>
        private void CreateNewStar()
        {
            int height = Game.GraphicsDevice.Viewport.Height;

            Vector2 position = new Vector2(Game.GraphicsDevice.Viewport.Width, height / 2 + random.Next(-height / 2, height / 2));
            parent.AddComponent(new Star(Game, texture, position, new Vector2(random.Next(4, 16), random.Next(-2, 2))));
        }


        internal void ResetStar()
        {
            timer = 0;
            starCreationTime = 80;
        }
    }
}
