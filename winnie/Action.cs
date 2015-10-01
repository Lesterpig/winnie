using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace winnie
{
    public abstract class Action
    {
        public Tile TileFrom
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Unit Unit
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Tile TileTo
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void ReverseExecute()
        {
            throw new System.NotImplementedException();
        }
    }

    public enum ActionType
    {
        Battle,
        Move
    }
}