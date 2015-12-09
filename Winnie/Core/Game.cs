using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// Game represents a whole Winnie Game, containing players, map, actions...
    /// </summary>
    public class Game
    {   
        /// <summary>
        /// Initializes a new instance of the <see cref="Core.Game"/> class.
        /// </summary>
        /// <remarks>
        /// Currently, only 2 players are allowed.
        /// </remarks>
        /// <exception cref="System.NotSupportedException">When the length of <c>players</c> is not 2.</exception>
        /// <param name="players">Players.</param>
        /// <param name="map">Map.</param>
        /// <param name="turns">Number of turns.</param>
        /// <param name="cheatMode">If set to <c>true</c>, enable cheat mode.</param>
        public Game(Player[] players, Map map, uint turns, bool cheatMode)
        {
            if (players.Length != 2)
            {
                throw new NotSupportedException("The game class only supports two players");
            }
            this.Players = players;
            this.Map = map;
            this.Turns = turns;
            this.CheatMode = cheatMode;
            this.Actions = new Stack<Action>();
        }

        /// <summary>
        /// The random generator.
        /// </summary>
        /// <remarks>
        /// It is the same for all games loaded.
        /// Modifiable for tests only.
        /// </remarks>
        public static CustomRandom Random = new CustomRandom(CustomRandom.Mode.NORMAL);

        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        /// <value>The map.</value>
        public Map Map { get; set; }

        /// <summary>
        /// Gets the players.
        /// </summary>
        /// <value>The players.</value>
        public Player[] Players { get; private set; }

        /// <summary>
        /// Gets the actions stack.
        /// </summary>
        /// <value>The actions.</value>
        public Stack<Action> Actions { get; private set; }

        /// <summary>
        /// Gets the total number of turns allowed.
        /// </summary>
        /// <value>The total number of turns.</value>
        public uint Turns { get; private set; }

        /// <summary>
        /// Gets the index of the current turn (from 0)
        /// </summary>
        /// <value>The index of the current turn.</value>
        public uint CurrentTurn { get; set; }

        /// <summary>
        /// Gets a value indicating whether the cheat mode is enabled.
        /// </summary>
        /// <value><c>true</c> if cheat mode enabled; otherwise, <c>false</c>.</value>
        public bool CheatMode { get; private set; }

        /// <summary>
        /// Gets or sets the index of the current player.
        /// </summary>
        /// <value>The index of the current player.</value>
        public uint CurrentPlayerIndex { get; set; }

        /// <summary>
        /// Gets the current player.
        /// </summary>
        /// <value>The current player.</value>
        public Player CurrentPlayer { get { return this.Players[this.CurrentPlayerIndex]; } }

        /// <summary>
        /// Triggers the end of the current turn, and starts the next turn.
        /// </summary>
        /// <exception cref="Game.EndOfGameException"></exception>
        public void NextTurn()
        {
        
            if (this.CurrentPlayerIndex == this.Players.Length - 1)
            {
                if (++this.CurrentTurn >= this.Turns)
                {
                    throw new EndOfGameException();
                }
            }

            this.CurrentPlayerIndex = (uint) ((this.CurrentPlayerIndex + 1) % this.Players.Length);
            this.CurrentPlayer.StartTurn();
        }

        /// <summary>
        /// Gets the winner.
        /// </summary>
        /// <remarks>
        /// This value is <c>null</c> if there is no winner yet (game not finished or draw).
        /// </remarks>
        /// <value>The winner, or <c>null</c>.</value>
        public Player Winner
        {
            get
            {
                if (this.CurrentTurn >= this.Turns)
                {
                    var players = this.Players.OrderByDescending(p => p.Score);
                    if (players.First().Score == players.Skip(1).First().Score)
                    {
                        return null;
                    }
                    else
                    {
                        return players.First();
                    }
                }

                var alivePlayers = this.Players.Where(p => p.Units.Where(u => u.Alive).Count() > 0);

                if (alivePlayers.Count() == 1)
                {
                    return alivePlayers.First();
                }
                return null;
            }
        }

        /// <summary>
        /// End of game exception.
        /// </summary>
        public class EndOfGameException : Exception {}
    }
}