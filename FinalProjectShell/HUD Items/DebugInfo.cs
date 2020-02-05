using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    class DebugInfo : HudString
    {
        bool processDKey = true;

        public DebugInfo(Game game, string fontName, HudLocation screenLocation)
            : base(game, fontName, screenLocation)
        {
        }

        /// <summary>
        /// This will be called when the game updates itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.D) && processDKey)
            {
                processDKey = false;
                this.Visible = !Visible;
            }

            if (processDKey == false && ks.IsKeyUp(Keys.D))
            {
                processDKey = true;
            }

            if (Visible)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Debug: <Press D to enable/disable>");
                builder.AppendLine($"   Asterid Count in Game.Components: {GetAsteroidCount()}");
                builder.AppendLine($"   Asteroid Count in ActionScene: {Game.Services.GetService<ActionScene>().GetComponentCount(typeof(Asteroid))}");
                builder.AppendLine($"   Star Count in Game.Components: {GetStarCount()}");
                builder.AppendLine($"   Star Count in ActionScene: {Game.Services.GetService<ActionScene>().GetComponentCount(typeof(Star))}");
                displayString = builder.ToString();
            }

            base.Update(gameTime);
        }



        private int GetAsteroidCount()
        {
            int count = 0;
            foreach (GameComponent component in Game.Components)
            {
                if (component is Asteroid)
                {
                    count++;
                }
            }
            return count;
        }

        private int GetStarCount()
        {
            int count = 0;
            foreach (GameComponent component in Game.Components)
            {
                if (component is Star)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
