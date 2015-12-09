using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// Action is a Command design pattern.
    /// It encapsulates Unit actions and provide a way to undo (reverse) these actions.
    /// 
    /// Create an action prepare its execution, it's just a possible action.
    /// The action is really executed when <c>Execute</c> is called.
    /// </summary>
    /// <seealso cref="Battle"/>
    /// <seealso cref="Move"/>
    public abstract class Action
    {   
        /// <summary>
        /// Gets or sets the tile from.
        /// </summary>
        /// <value>Where the unit started the action.</value>
        public Tile TileFrom { get; protected set; }

        /// <summary>
        /// Gets or sets the tile to.
        /// </summary>
        /// <value>Where the unit ended the action.</value>
        public Tile TileTo { get; protected set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>The unit that performed / is going to perform the action.</value>
        public Unit Unit { get; protected set; }

        /// <summary>
        /// Execute the action.
        /// Should be called only one time.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Reverses the execution of the action.
        /// Should be called after the execution.
        /// </summary>
        public abstract void ReverseExecute();

        /// <summary>
        /// Default function to reverse used move points.
        /// </summary>
        protected virtual void reversePoints() {
            this.Unit.MovePoints += this.Unit.Race.GetRequiredMovePoints(this.TileTo.TileType);
        }
    }
}