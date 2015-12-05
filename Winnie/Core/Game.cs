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

        public bool CheatMode { get; set; }

        public int CurrentPlayerIndex { get; set; }

        public void NextTurn()
        {
            throw new System.NotImplementedException();
        }

        public void NewAction(Tile destination)
        {
            throw new System.NotImplementedException();
        }
    }
}