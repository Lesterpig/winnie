using System;
using System.Collections.Generic;

namespace Core
{
    public class GameBuilder
    {
        public static Game New<T>(Player p1, Player p2, bool cheatMode = false, int seed = 0) where T : GameType, new()
        {   

            if (p1.Race.Identifier == p2.Race.Identifier)
            {
                throw new SameRaceException();
            }

            var t = new T();

            Algo a = new Algo();
            var s = t.Size;
            var map = new Map(a.CreateMap(seed, s, s), s, s);
            a.FindBestStartPosition(p1, p2, map);

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
        int Size { get; } // Not static because would not be understood by c# generics
        int UnitQty { get ; }
        int Turns { get ; }
    }
        
    public class DemoGameType : GameType
    {   
        public int Size { get { return 6; } }
        public int UnitQty { get { return 4; } }
        public int Turns { get { return 5; } }
    }

    public class SmallGameType : GameType
    {   
        public int Size { get { return 10; } }
        public int UnitQty { get { return 6; } }
        public int Turns { get { return 20; } }
    }

    public class StandardGameType : GameType
    {   
        public int Size { get { return 14; } }
        public int UnitQty { get { return 8; } }
        public int Turns { get { return 30; } }
    }
        
}