using System;
using Core;

namespace Saver
{
    public class PlayerData
    {
        public string Name;
        public int Race;

        public PlayerData(Player p)
        {
            this.Name = p.Name;
            this.Race = 0;
            if (p.Race is Elf)
            {
                this.Race = 1;
            }
            else if (p.Race is Orc)
            {
                this.Race = 2;
            }
        }
    }
}

