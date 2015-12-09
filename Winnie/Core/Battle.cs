using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// Battle encapsulates attacks between units.
    /// </summary>
    /// <seealso cref="Action"/>
    public class Battle : Action
    {   
        /// <summary>
        /// Gets or sets the target unit.
        /// </summary>
        /// <value>The target unit.</value>
        public Unit Target { get; protected set; }

        /// <summary>
        /// Gets or sets the battle result.
        /// </summary>
        /// <value>The battle result.</value>
        public Unit.AttackResult Result { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Core.Battle"/> is ranged.
        /// </summary>
        /// <value><c>true</c> if ranged; otherwise, <c>false</c>.</value>
        public bool Ranged { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <c>this.Unit</c> has been moved after the battle (in case of victory).
        /// </summary>
        /// <value><c>true</c> if moved; otherwise, <c>false</c>.</value>
        public bool Moved { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Core.Battle"/> class.
        /// </summary>
        /// <param name="attacker">Attacker.</param>
        /// <param name="target">Target.</param>
        /// <param name="ranged">If set to <c>true</c> ranged attack.</param>
        public Battle(Unit attacker, Unit target, bool ranged)
        {
            this.TileFrom = attacker.Tile;
            this.TileTo = target.Tile;
            this.Unit = attacker;
            this.Target = target;
            this.Ranged = ranged;
        }

        /// <summary>
        /// Overrides the default function to reverse move points.
        /// </summary>
        protected override void reversePoints() {
            this.Unit.MovePoints += this.Ranged ? 1 : this.Unit.Race.GetRequiredMovePoints(this.TileTo.TileType);
        }

        /// <summary>
        /// Execute the action.
        /// Should be called only one time.
        /// </summary>
        public override void Execute()
        {
            this.Result = this.Unit.Attack(this.Target, this.Ranged);
            if (!this.Ranged && this.Result.Winner == this.Unit && this.TileTo.MasterRace == null)
            {
                this.Unit.Move(this.TileTo, true);
                this.Moved = true;
            }
        }

        /// <summary>
        /// Reverses the execution of the action.
        /// Should be called after the execution.
        /// </summary>
        public override void ReverseExecute()
        {   
            if (this.Result == null)
            {
                throw new NotReadyException();
            }

            if (this.Moved)
            {
                this.Unit.Move(this.TileFrom, true);
            }

            this.Result.Loser.Life += this.Result.Dmg;
            this.reversePoints();
        }

        /// <summary>
        /// Means that the Command is not ready to be executed / reversed.
        /// </summary>
        public class NotReadyException : Exception {}
    }
}