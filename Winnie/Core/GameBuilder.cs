using System;
using System.Collections.Generic;

namespace Core
{
    public class GameBuilder
    {   
        public static Game New<Type, Strategy>(Player p1, Player p2, bool cheatMode = false, int seed = 0) where Type : GameType, new() where Strategy : MapGeneration, new()
        {   

            if (p1.Race.Identifier == p2.Race.Identifier)
            {
                throw new SameRaceException();
            }

            var t = new Type();
            var s = new Strategy();

            var map = s.Generate(t.Size, seed);
            s.PlacePlayers(p1, p2);

            // Build units

            for (int i = 0; i < t.UnitQty; i++)
            {
                UnitFactory.Build(p1, map.getTile(p1.InitialPosition.x, p1.InitialPosition.y));
                UnitFactory.Build(p2, map.getTile(p2.InitialPosition.x, p2.InitialPosition.y));
            }

            Player[] players = { p1, p2 };

            var g = new Game(players, map, t.Turns, cheatMode);
            g.CurrentPlayerIndex = 0;
            g.CurrentPlayer.StartTurn();
            return g;
        }

        public class SameRaceException : Exception {}
    }

    // This kind of things because enums are "deprecated"
    // TODO upgrade this

    public interface GameType
    {   
        uint Size { get; } // Not static because would not be understood by c# generics
        uint UnitQty { get ; }
        uint Turns { get ; }
    }
        
    public class DemoGameType : GameType
    {   
        public uint Size { get { return 6; } }
        public uint UnitQty { get { return 4; } }
        public uint Turns { get { return 5; } }
    }

    public class SmallGameType : GameType
    {   
        public uint Size { get { return 10; } }
        public uint UnitQty { get { return 6; } }
        public uint Turns { get { return 20; } }
    }

    public class StandardGameType : GameType
    {   
        public uint Size { get { return 14; } }
        public uint UnitQty { get { return 8; } }
        public uint Turns { get { return 30; } }
    }
        
}