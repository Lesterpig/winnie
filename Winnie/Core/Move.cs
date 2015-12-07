using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Move : Action
    {   

        public Move(Unit unit, Tile to)
        {
            this._unit = unit;
            this._tileFrom = unit.Tile;
            this._tileTo = to;
        }

        public Move(Unit unit, Tile fr, Tile to)
        {
            this._unit = unit;
            this._tileFrom = fr;
            this._tileTo = to;
        }

        public override void Execute()
        {
            this._unit.Move(this._tileTo);
        }

        public override void ReverseExecute()
        {
            this._unit.Move(this._tileFrom, true);
            this.reversePoints();
        }
    }
}