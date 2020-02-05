using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    class AboutTextComponent : DrawableGameComponent
    {
        Texture2D texture;
        Texture2D aboutPage;
        
        public AboutTextComponent(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.Draw(aboutPage, new Vector2(-220,0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Images/space");
            aboutPage = Game.Content.Load<Texture2D>("Images/AboutScene/aboutPage1");
            base.LoadContent();
        }

    }
}
