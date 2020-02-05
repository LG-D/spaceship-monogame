using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalProjectShell
{
    enum BackgroundAnchor
    {
        Top,
        Bottom
    }
    
    class Background : DrawableGameComponent
    {
        private static int backgroundCount = 0;

        Texture2D texture;
        SpriteFont text;
        Vector2 velocity;
        BackgroundAnchor anchor;

        bool gameStarted = true;
        Song backgroundMusic;
        List<Rectangle> backgroundRec;

        /// <summary>
        /// The constructor that takes these parameters:
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="texture">Texture to show</param>
        /// <param name="velocity">Speed</param>
        /// <param name="anchor">And background anchor</param>
        public Background(Game game, Texture2D texture, Vector2 velocity, BackgroundAnchor anchor) : base(game)
        {

            DrawOrder = backgroundCount;
            backgroundCount++;

            this.texture = texture;
            this.velocity = velocity;
            this.anchor = anchor;

            backgroundRec = CalculateBackgroundRectangleList();
        }

        /// <summary>
        /// This will calculate the background list of rectangles 
        /// and show them by calculating their position
        /// </summary>
        /// <returns></returns>
        private List<Rectangle> CalculateBackgroundRectangleList()
        {
            List<Rectangle> neededRec = new List<Rectangle>();

            int recCount = Game.GraphicsDevice.Viewport.Width / texture.Width + 2;
            int yPosition = CalculateYPosition();

            for (int i = 0; i < recCount; i++)
            {
                neededRec.Add(new Rectangle(texture.Width * i, yPosition, texture.Width, texture.Height));
            }

            return neededRec;
        }

        private int CalculateYPosition()
        {
            switch (anchor)
            {
                default:
                case BackgroundAnchor.Top:
                    return 0;
                case BackgroundAnchor.Bottom:
                    return Game.GraphicsDevice.Viewport.Height - texture.Height;
            }
        }

        /// <summary>
        ///  This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            int highestScore = Game.Services.GetService<HighscoreScene>().GetHighestScore();

            sb.Begin();
            foreach (Rectangle rect in backgroundRec)
            {
                sb.Draw(texture, rect, Color.Firebrick);
            }
            sb.DrawString(text, "Highest score is: " + highestScore, new Vector2(10,15), Color.LightSalmon);
            sb.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        ///  This will update the frame each gametime
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (gameStarted)
            {
                for (int i = 0; i < backgroundRec.Count; i++)
                {
                    Rectangle rect = backgroundRec[i];
                    rect.Location -= velocity.ToPoint();
                    backgroundRec[i] = rect;
                    
                }

                // make sure that the right edge of the first one
                // is not past the edge of the screen
                Rectangle firstRect = backgroundRec[0];
                if (firstRect.Right < 0)
                {
                    backgroundRec.RemoveAt(0);
                    Rectangle lastRect = backgroundRec[backgroundRec.Count - 1];
                    firstRect.X = lastRect.Right;
                    
                    backgroundRec.Add(firstRect);

                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            backgroundMusic = Game.Content.Load<Song>("MusicAndSounds/BravePilots");
            text = Game.Content.Load<SpriteFont>("Fonts/scoreFont");

            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;

            base.LoadContent();
        }
    }
}
