using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProjectShell
{
    class HighscoreTextComponent : DrawableGameComponent
    {
        string path = @"highscores.txt";

        string[] highscores;
        List<int> highscoreList;
        int highestScore;

        SpriteFont highscoreFont;
        Vector2 position;
        SpriteFont title;

        public HighscoreTextComponent(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            position = new Vector2(520, 200);

            highscoreList = new List<int>();

            if (File.Exists(path))
            {
                highscores = File.ReadAllLines(path);
                
                for (int i = 0; i < highscores.Length; i++)
                {
                    highscoreList.Add(int.Parse(highscores[i]));
                    highscoreList.Sort();
                    highscoreList.Reverse();
                    highscoreList = highscoreList.Take(10).ToList();
                }
            }

            base.Initialize();
        }

        /// <summary>
        /// This will be called when the game draws itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();
            Vector2 fontPosition = position;
            
            spriteBatch.Begin();

            spriteBatch.DrawString(title, "Top 10 High Scores", new Vector2(250, 100), Color.AntiqueWhite);

            for (int i = 0; i < highscoreList.Count; i++)
            {
                spriteBatch.DrawString(highscoreFont, highscoreList[i].ToString(), fontPosition, Color.LightGoldenrodYellow);
                fontPosition.Y += highscoreFont.LineSpacing;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// This method will add the score to the list
        /// sort it by descending and then just take the first
        /// 10 and write it to file
        /// </summary>
        /// <param name="score"></param>
        internal void AddNewScore(int score)
        {
            highscoreList.Add(score);
            highscoreList.Sort();
            highscoreList.Reverse();
            highscoreList = highscoreList.Take(10).ToList();

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(score);
            }
        }

        /// <summary>
        /// This will be called to load the content for each frame
        /// </summary>
        protected override void LoadContent()
        {
            highscoreFont = Game.Content.Load<SpriteFont>("Fonts/scoreFont");
            title = Game.Content.Load<SpriteFont>("Fonts/regularFont");
            base.LoadContent();
        }

        /// <summary>
        /// This method will get the first element in the list
        /// which is the highest score
        /// </summary>
        /// <returns></returns>
        internal int GetHighestScore()
        {
            if (highscoreList.Count == 0)
            {
                return 0;
            }
            else
            {
                highestScore = highscoreList.ElementAt(0);
                return highestScore;
            }
        }
    }
}
