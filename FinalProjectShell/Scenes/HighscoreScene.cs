using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    class HighscoreScene : GameScene
    {
        HighscoreTextComponent highScoreRef;

        public HighscoreScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            highScoreRef = new HighscoreTextComponent(Game);
            AddComponent(highScoreRef);
            this.Hide();
            base.Initialize();
        }

        /// <summary>
        /// This will be called when the game updates itself
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

        /// <summary>
        /// This will add a new score to the highscore file
        /// </summary>
        /// <param name="score">Score that is current</param>
        public void AddNewHighScore(int score)
        {
            highScoreRef.AddNewScore(score);
        }

        /// <summary>
        /// This will just give the highest score
        /// </summary>
        /// <returns></returns>
        public int GetHighestScore()
        {
           return highScoreRef.GetHighestScore();
        }
    }
}
