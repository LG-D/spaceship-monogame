using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    class Star : DrawableGameComponent
    {
        public Texture2D Texture { get; private set; }
        Texture2D texture2;

        SoundEffect soundFxStar;
        bool showTexture1;
        double timeSinceShow = 0.0;
        const double SHOW_INTERVAL = 0.1;
        
        Vector2 velocity = new Vector2(20, 0);

        private Vector2 position = Vector2.Zero;
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Star(Game game, Texture2D texture, Vector2 position, Vector2 velocity) : base(game)
        {
            Texture = texture;
            this.velocity = velocity;
            this.position = position;
            if (Game.Services.GetService<Star>() == null)
            {
                Game.Services.AddService<Star>(this);
            }
        }

        /// <summary>
        /// This will be called when the game draws itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(showTexture1 == true ? Texture : texture2, position, Color.White);
            sb.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// This will update the frame within gametime and 
        /// will add score when the rocket intersects with the star
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            position -= velocity;
            if (position.X + Texture.Width < 0)
            {
                Game.Components.Remove(this);
            }

            timeSinceShow += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceShow >= SHOW_INTERVAL)
            {
                showTexture1 = !showTexture1;
                timeSinceShow = 0.0;
            }

            for (int i = 0; i < Game.Components.Count; i++)
            {
                Rocket rocket;
                Rectangle rocketRect = new Rectangle();

                GameComponent component = (GameComponent)Game.Components[i];
                if (component is Rocket)
                {
                    rocket = (Rocket)component;
                    rocketRect = rocket.Texture.Bounds;
                    rocketRect.Location = rocket.Position.ToPoint();
                }

                Rectangle starRect = this.Texture.Bounds;
                starRect.Location = position.ToPoint();

                if (rocketRect.Intersects(starRect))
                {
                    Game.Components.Remove(this);
                    soundFxStar.Play();
                    Game.Services.GetService<Score>().AddScore();
                }
            }

            base.Update(gameTime);
        }
        
        /// <summary>
        /// This will be called when the game loads the content
        /// </summary>
        protected override void LoadContent()
        {
            Texture = Game.Content.Load<Texture2D>("Images/StarAnimation/11");
            texture2 = Game.Content.Load<Texture2D>("Images/StarAnimation/22");
            soundFxStar = Game.Content.Load<SoundEffect>("MusicAndSounds/star");

            base.LoadContent();
        }
    }
}
