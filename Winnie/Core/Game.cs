﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Game
    {
    
        public static CustomRandom Random = new CustomRandom(CustomRandom.Mode.NORMAL); // TODO customize random per game?

        public Map Map
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public List<Core.Player> Players
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public List<Action> Actions
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int Turns
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int CurrentTurn
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public bool CheatMode
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Unit CurrentUnit
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

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