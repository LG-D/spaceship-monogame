using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    public class MenuComponent : DrawableGameComponent
    {
        
        SpriteFont regularFont;
        SpriteFont highlightFont;

        private List<string> menuItems;
        private int SelectedIndex { get; set; }
        private Vector2 position;

        private Color regularColor = Color.AliceBlue;
        private Color hilightColor = Color.Salmon;

        private KeyboardState oldState; 

        public MenuComponent(Game game, List<string>menuNames) : base(game)
        {
            menuItems = menuNames; 
        }

        /// <summary>
        /// This will call the update for frame 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {               
                KeyboardState ks = Keyboard.GetState();
                if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                {
                    SelectedIndex++;
                    if (SelectedIndex == menuItems.Count)
                    {
                        SelectedIndex = 0;
                    }
                }
                if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = menuItems.Count - 1;
                    }

                }
                oldState = ks;

                if (ks.IsKeyDown(Keys.Enter))
                {
                    SwitchScenesBasedOnSelection();
                }

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Will switch the scenes based on the selection they make
        /// </summary>
        private void SwitchScenesBasedOnSelection()
        {
            ((Game1)Game).HideAllScenes();

            switch ((MenuSelection)SelectedIndex)
            {
                case MenuSelection.StartGame:
                    Game.Services.GetService<ActionScene>().Show();
                    break;
                case MenuSelection.Help:
                    Game.Services.GetService<HelpScene>().Show();
                    break;
                case MenuSelection.HighScore:
                    Game.Services.GetService<HighscoreScene>().Show();
                    break;
                case MenuSelection.Credit:
                    Game.Services.GetService<AboutScene>().Show();
                    break;
                case MenuSelection.Quit:
                    Game.Exit();
                    break;
                default:
                    Game.Services.GetService<StartScene>().Show();
                    break;
            }
        }

        /// <summary>
        /// This will be called when the game draws itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            Vector2 tempPos = position;

            sb.Begin();

            for (int i = 0; i < menuItems.Count; i++)
            {
                SpriteFont activeFont = regularFont;
                Color activeColor = regularColor;
                
                if (SelectedIndex == i)
                {
                    activeFont = highlightFont;
                    activeColor = hilightColor;                    
                }
                
                sb.DrawString(activeFont, menuItems[i], tempPos, activeColor);
                
                tempPos.Y += regularFont.LineSpacing;                
            }

            sb.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            // starting position of the menu items
            position = new Vector2(150,150);
               
            base.Initialize();
        }

        /// <summary>
        /// This will be called when we load the content
        /// </summary>
        protected override void LoadContent()
        {
            // load the fonts we will be using for this menu
            regularFont = Game.Content.Load<SpriteFont>("Fonts/regularFont");
            highlightFont = Game.Content.Load<SpriteFont>("Fonts/hilightFont");
            base.LoadContent();
        }
    }
}
