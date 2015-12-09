using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    // TODO documentation
    public class Move : Action
    {   
        /// <summary>
        /// Initializes a new instance of the <see cref="Core.Move"/> class.
        /// </summary>
        /// <param name="unit">Unit.</param>
        /// <param name="to">Destination.</param>
        public Move(Unit unit, Tile to)
        {
            this.Unit = unit;
            this.TileFrom = unit.Tile;
            this.TileTo = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Core.Move"/> class.
        /// </summary>
        /// <param name="unit">Unit.</param>
        /// <param name="fr">Departure (hypothetical).</param>
        /// <param name="to">Destination.</param>
        public Move(Unit unit, Tile fr, Tile to)
        {
            this.Unit = unit;
            this.TileFrom = fr;
            this.TileTo = to;
        }

        /// <summary>
        /// Execute the action.
        /// Should be called only one time.
        /// </summary>
        public override void Execute()
        {
            this.Unit.Move(this.TileTo);
        }

        /// <summary>
        /// Reverses the execution of the action.
        /// Should be called after the execution.
        /// </summary>
        public override void ReverseExecute()
        {
            this.Unit.Move(this.TileFrom, true);
            this.reversePoints();
        }
    }
}