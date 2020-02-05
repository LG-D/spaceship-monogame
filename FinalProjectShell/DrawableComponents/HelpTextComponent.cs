using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    class HelpTextComponent : DrawableGameComponent
    {
        Texture2D texture;
        Texture2D helpImage;
        

        public HelpTextComponent(Game game) : base(game)
        { }
        
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// This will be called when the game draws itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.Draw(helpImage, new Vector2(-90,0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Load will be called once per frame and its called
        /// to load the content of game
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Images/space");
            helpImage = Game.Content.Load<Texture2D>("Images/HelpScene/helpPage");

            base.LoadContent();
        }
    }
}
