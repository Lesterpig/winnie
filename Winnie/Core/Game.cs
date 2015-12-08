using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Game
    {   
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
    
        public static CustomRandom Random = new CustomRandom(CustomRandom.Mode.NORMAL); // TODO customize random per game?
        public Map Map { get; set; }
        public Player[] Players { get; private set; }
        public Stack<Action> Actions { get; private set; }
        public uint Turns { get; private set; }
        public uint CurrentTurn { get; set; }
        public bool CheatMode { get; private set; }
        public uint CurrentPlayerIndex { get; set; }
        public Player CurrentPlayer { get { return this.Players[this.CurrentPlayerIndex]; } }

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

        public class EndOfGameException : Exception {}
    }
}