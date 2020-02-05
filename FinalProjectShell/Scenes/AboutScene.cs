using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    class AboutScene : GameScene
    {
        public AboutScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            AddComponent(new AboutTextComponent(Game));
            this.Hide();
            base.Initialize();
        }

        /// <summary>
        /// Will be called when the game updates itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    ((Game1)Game).HideAllScenes();
                    Game.Services.GetService<StartScene>().Show();
                }
            }
            base.Update(gameTime);
        }
    }
}
