using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Game
    {   
        public Game(Player[] players, Map map, int turns, bool cheatMode)
        {
            this.Players = players;
            this.Map = map;
            this.Turns = turns;
            this.CheatMode = cheatMode;
        }
    
        public static CustomRandom Random = new CustomRandom(CustomRandom.Mode.NORMAL); // TODO customize random per game?
        public Map Map { get; set; }
        public Player[] Players { get; private set; }
        public Stack<Action> Actions { get; private set; }
        public int Turns { get; private set; }
        public int CurrentTurn { get; set; }
        public bool CheatMode { get; private set; }
        public int CurrentPlayerIndex { get; set; }
        public Player CurrentPlayer { get { return this.Players[this.CurrentPlayerIndex]; } }

        public void NextTurn()
        {
            if (this.CurrentTurn >= this.Turns)
            {
                throw new EndOfGameException();
            }

            this.CurrentTurn++;
            this.CurrentPlayerIndex = (this.CurrentPlayerIndex + 1) % this.Players.Length;
        }

        public Player CheckVictory(Tile destination)
        {
            if (this.CurrentTurn >= this.Turns)
            {
                return this.Players.OrderByDescending(p => p.Score).First();
            }

            var alivePlayers = this.Players.Where(p => p.Units.Where(u => u.Alive).Count() > 0);

            if (alivePlayers.Count() == 1)
            {
                return alivePlayers.First();
            }
            return null;
        }

        public class EndOfGameException : Exception {}
    }
}