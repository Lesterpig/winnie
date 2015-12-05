using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Battle : Action
    {   
        private Unit _target;
        private Unit.AttackResult _result;
        private bool _ranged;
        private bool _moved;

        public Battle(Unit attacker, Unit target, bool ranged)
        {
            this._tileFrom = attacker.Tile;
            this._tileTo = target.Tile;
            this._unit = attacker;
            this._target = target;
            this._ranged = ranged;
        }

        public override void Execute()
        {
            this._result = this._unit.Attack(this._target, this._ranged);
            if (!this._ranged && this._result.Winner == this._unit && this._tileTo.MasterRace == null)
            {
                this._unit.Move(this._tileTo, true);
                this._moved = true;
            }
        }

        public override void ReverseExecute()
        {   
            if (this._result == null)
            {
                throw new NotReadyException();
            }

            if (this._moved)
            {
                this._unit.Move(this._tileFrom, true);
            }

            this._result.Loser.Life += this._result.Dmg;
            this.reversePoints();
        }

        public class NotReadyException : Exception {}
    }
}