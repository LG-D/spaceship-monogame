using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalProjectShell
{
    public class ActionScene : GameScene
    {
        public ActionScene (Game game): base(game)
        {
        }

        public override void Initialize()
        {
            this.AddComponent(new Score(Game, "Fonts/scoreFont", HudLocation.TopRight));
            this.AddComponent(new Background(Game, Game.Content.Load<Texture2D>("Images/galaxy"), new Vector2(3, 0), BackgroundAnchor.Top));
            this.AddComponent(new Rocket(Game));
            this.AddComponent(new AsteroidManager(Game, this));
            this.AddComponent(new StarManager(Game, this));
            //this.AddComponent(new DebugInfo(Game, "Fonts/scoreFont", HudLocation.BottomLeft));
            base.Initialize();
        }

        /// <summary>
        /// Overriding the method that is called before starting the scene
        /// </summary>
        public override void Show()
        {
            Game.Services.GetService<Score>().ResetScore();
            this.RemoveComponent(new Rocket(Game));
            this.RemoveComponent(new AsteroidManager(Game, this));
            this.RemoveComponent(new StarManager(Game, this));
            base.Show();
        }

        /// <summary>
        /// Will be called when the game updates itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled )
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
