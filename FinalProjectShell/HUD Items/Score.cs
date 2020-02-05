using System;
using Microsoft.Xna.Framework;

namespace FinalProjectShell
{
    public class Score : HudString
    {
        int score;

        public int Value;

        public Score(Game game, string fontName, HudLocation screenLocation) : base(game, fontName, screenLocation)
        {
            if (Game.Services.GetService<Score>() == null)
            {
                Game.Services.AddService<Score>(this);
            }
        }

        /// <summary>
        /// This will be called when the game updates itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            displayString = $"Score: {score}";
            base.Update(gameTime);
        }

        /// <summary>
        /// Will add the score by 1
        /// </summary>
        public void AddScore()
        {
            score += 1;
        }
        
        /// <summary>
        /// Will reset the score to 0 (initial value)
        /// </summary>
        internal void ResetScore()
        {
            score = 0;
        }

        /// <summary>
        /// Will just return the score
        /// </summary>
        /// <returns></returns>
        public int GetScore()
        {
            return score;
        }

    }
}
