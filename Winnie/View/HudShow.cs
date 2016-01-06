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
            //game.HUDBatch.DrawString();
        }
    }
}
