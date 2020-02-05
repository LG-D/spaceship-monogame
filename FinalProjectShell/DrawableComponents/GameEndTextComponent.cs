using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    class GameEndTextComponent : DrawableGameComponent
    {
        Texture2D gameEndImage;
        int score;
        SpriteFont font;
        SpriteFont text;

        public GameEndTextComponent(Game game) : base(game)
        {
        }

        /// <summary>
        /// This will be called when the game draws itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();
            score = Game.Services.GetService<Score>().GetScore();

            spriteBatch.Begin();

            spriteBatch.Draw(gameEndImage, new Vector2(-250, 0), Color.White);
            spriteBatch.DrawString(font,"Score: " + score.ToString(), new Vector2(400, 500), Color.White);
            spriteBatch.DrawString(text, "Press Esc key to go back to the menu", new Vector2(220, 800), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// This will update the frame each gametime
        /// </summary>
        /// <param name="gameTime">A snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            gameEndImage = Game.Content.Load<Texture2D>("Images/gameEnd");
            font = Game.Content.Load<SpriteFont>("Fonts/regularFont");
            text = Game.Content.Load<SpriteFont>("Fonts/text");

            base.LoadContent();
        }
    }
}
