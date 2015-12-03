using System;
using System.Collections.Generic;
using Core;

namespace Saver
{
    public class PlayerData
    {   

        public enum RaceEnum { HUMAN, ELF, ORC }

        public HashSet<UnitData> Units;
        public string Name;
        public RaceEnum Race;

        public PlayerData()
        {
        }

        public PlayerData(Player p)
        {
            this.Name = p.Name;
            this.Race = RaceEnum.HUMAN;
            if (p.Race is Elf)
            {
                this.Race = RaceEnum.ELF;
            }
            else if (p.Race is Orc)
            {
                this.Race = RaceEnum.ORC;
            }

            this.Units = new HashSet<UnitData>();

            foreach (Unit u in p.Units)
            {
                this.Units.Add(new UnitData(u));
            }

        }
    }
}

