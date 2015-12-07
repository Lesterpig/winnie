using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public abstract class Action
    {   
        protected Tile _tileFrom;
        protected Tile _tileTo;
        protected Unit _unit;

        public Tile TileTo { get { return this._tileTo; } }

        public abstract void Execute();
        public abstract void ReverseExecute();

        protected virtual void reversePoints() {
            this._unit.MovePoints += this._unit.Race.GetRequiredMovePoints(this._tileTo.TileType);
        }
    }

    public enum ActionType
    {
        Battle,
        Move
    }
}