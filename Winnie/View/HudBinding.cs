using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MGUI
{
    class HudBinding
    {

        const int size = 48;

        private static Dictionary<string, Rectangle> binding = new Dictionary<string, Rectangle>()
        {

            {"Heart", new Rectangle(0, 0, size, size)},
            {"Sword", new Rectangle(size, 0, size, size)},
            {"Shield", new Rectangle(size*2, 0, size, size)},
            {"Gold", new Rectangle(size*3, 0, size, size)},
            {"Arrow", new Rectangle(size*4, 0, size, size)},

        };


        public static Rectangle GetTexture(string name)
        {
            Rectangle res;
            if (!binding.TryGetValue(name, out res))
                res = new Rectangle(0, 0, 0, 0);

            return res;
        }
    }
}
