using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGUI
{
    class HudShow : Blittable
    {

        Game1 game;

        public HudShow(Game1 g)
        {
            game = g;
        }

        public void Blit()
        {
            drawScores();
        }

        private void drawScores()
        {
            drawString(game.GameModel.Players[0].Name, 10, 10, 1, 0, Color.White);
            drawString(game.GameModel.Players[1].Name, 10, 50, 1, 0, Color.Gray);
        }

        private void drawString(string text, float x, float y, float scale, float rotation, Color color)
        {
            Vector2 position = new Vector2(x, y);
            Vector2 origin = new Vector2(0, 0);
            game.HUDBatch.DrawString(game.MainFont, text, position, color, rotation, origin, scale, SpriteEffects.None, 1);
        }
    }
}
