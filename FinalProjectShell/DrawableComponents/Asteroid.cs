using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    class Asteroid : DrawableGameComponent
    {
        public Texture2D Texture { get; private set; }
        Vector2 velocity = new Vector2(20, 0);
        
        private Vector2 position = Vector2.Zero;
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        /// <summary>
        /// Constructor that takes the parameters:
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="texture">The texture that will load</param>
        /// <param name="position">Vector2 position of the texture</param>
        /// <param name="velocity">And the speed</param>
        public Asteroid(Game game, Texture2D texture, Vector2 position, Vector2 velocity) : base(game)
        {
            Texture = texture;
            this.velocity = velocity;
            this.position = position;
            if (Game.Services.GetService<Asteroid>() == null)
            {
                Game.Services.AddService<Asteroid>(this);
            }
        }
        

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// This will update the frame each gametime
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            position -= velocity;
            if (position.X + Texture.Width < 0)
            {
                Game.Components.Remove(this);
            }

            base.Update(gameTime);
        }

        /// <summary>
        ///  This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            sb.Draw(Texture, position, Color.Yellow);
            sb.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            if (Texture == null)
            {
                Texture = Game.Content.Load<Texture2D>("Images/asteroid");
            }

            base.LoadContent();
        }
        
    }
}
