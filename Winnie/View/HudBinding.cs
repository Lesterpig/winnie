using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MGUI
{
    class HudBinding
    {

        private static Dictionary<string, Rectangle> binding = new Dictionary<string, Rectangle>()
        {

			{"Heart", new Rectangle(0, 0, 24, 24)},
            {"Sword", new Rectangle(24, 0, 24, 24)},
            {"Shield", new Rectangle(48, 0, 24, 24)},
            {"Gold", new Rectangle(72, 0, 24, 24)}

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
