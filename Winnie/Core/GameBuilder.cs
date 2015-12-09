using System;
using System.Collections.Generic;

namespace Core
{   
    /// <summary>
    /// A builder to simplify game creation.
    /// It is the starting point of the library.
    /// </summary>
    public class GameBuilder
    {   
        /// <summary>
        /// Build a new Game
        /// </summary>
        /// <exception cref="SameRaceException">When <c>p1</c> has the same race as <c>p2</c>.</exception>
        /// <param name="p1">First player.</param>
        /// <param name="p2">Second player.</param>
        /// <param name="cheatMode">If set to <c>true</c>, enable cheat mode.</param>
        /// <param name="seed">Random seed configuration. Used for tests.</param>
        /// <typeparam name="Type">Kind of game to generate (DemoGameType, SmallGameType or StandardGameType).</typeparam>
        /// <typeparam name="Strategy">The procedural generation algorithm to use for map creation (NaiveMap or PerlinMap).</typeparam>
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

            // Ignition!

            var g = new Game(players, map, t.Turns, cheatMode);
            g.CurrentPlayerIndex = 0;
            g.CurrentPlayer.StartTurn();
            return g;
        }

        public class SameRaceException : Exception {}
    }

    /// <summary>
    /// Contains useful information for game generation.
    /// This kind of things because enums are "deprecated".
    /// </summary>
    public interface GameType
    {   
        /// <summary>
        /// Size of the map (the result will be a square map of size * size tiles).
        /// Not static because would not be understood by c# generics.
        /// </summary>
        /// <value>The size.</value>
        uint Size { get; }

        /// <summary>
        /// Number of units by player.
        /// </summary>
        /// <value>The unit qty.</value>
        uint UnitQty { get ; }

        /// <summary>
        /// Number of turns before automatic game end.
        /// </summary>
        /// <value>The number of turns.</value>
        uint Turns { get ; }
    }

    /// <summary>
    /// Demo game type.
    /// </summary>
    public class DemoGameType : GameType
    {   
        public uint Size { get { return 6; } }
        public uint UnitQty { get { return 4; } }
        public uint Turns { get { return 5; } }
    }

    /// <summary>
    /// Small game type.
    /// </summary>
    public class SmallGameType : GameType
    {   
        public uint Size { get { return 10; } }
        public uint UnitQty { get { return 6; } }
        public uint Turns { get { return 20; } }
    }

    /// <summary>
    /// Standard game type.
    /// </summary>
    public class StandardGameType : GameType
    {   
        public uint Size { get { return 14; } }
        public uint UnitQty { get { return 8; } }
        public uint Turns { get { return 30; } }
    }
        
}