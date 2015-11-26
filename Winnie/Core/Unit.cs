using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Unit
    {   

        public Unit(Player player, Tile t)
        {
            this._player = player;
            this._player.AddUnit(this);
            this._tile = t;
            this._tile.AddUnit(this);
            this._life = this.Race.Life;
        }

        private Player _player;
        public Player Player
        {
            get { return this._player; }
        }

        public Race Race
        {
            get { return this._player.Race; }
        }

        private Tile _tile;
        public Tile Tile
        {
            get { return this._tile; }
        }

        private int _life;
        public int Life
        {
            get { return this._life; }
            set { this._life = value; }
        }

        public bool Alive
        {
            get { return this._life > 0; }
        }

        public double LifeRatio
        {
            get { return (double)this._life / this.Race.Life; }
        }

        public int AttackPoints
        {
            get { return this.Alive ? (int)Math.Ceiling(this.Race.Attack * LifeRatio) : 0; }
        }

        public int DefensePoints
        {
            get { return this.Alive ? (int)Math.Ceiling(this.Race.Armor * LifeRatio) : 0; }
        }

        public double MovePoints
        {
            get;
            set;
        }

        public int VictoryPoints
        {
            get
            {
                return this.Alive ? this.Race.GetVictoryPoints(this.Tile.TileType) : 0;
            }
        }

        public List<Action> Possibilities
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public void Move(Tile to, bool reverse = false)
        {   

            if (to == this.Tile)
            {
                return;
            }

            if (!this.Race.CanMove(to.TileType))
            {
                throw new Unit.MovementNotAllowedException();
            }
                
            if (to.MasterRace != this.Race && to.MasterRace != null)
            {
                throw new Unit.EnnemiesRemainingException();
            }

            double requiredPoints = this.Race.GetRequiredMovePoints(to.TileType);

            if (!reverse)
            {
                if (requiredPoints > this.MovePoints)
                {
                    throw new Unit.NotEnoughMovePointsException();
                }
                this.MovePoints -= requiredPoints;
            }
            else
            {
                this.MovePoints += requiredPoints; // TODO check what to do during nextTurns actions for reverse operation
            }

            this.Tile.RemoveUnit(this);
            this._tile = to;
            this.Tile.AddUnit(this);
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        // Exceptions

        public class MovementNotAllowedException : Exception {}
        public class NotEnoughMovePointsException : Unit.MovementNotAllowedException {}
        public class EnnemiesRemainingException : Unit.MovementNotAllowedException {}
    }
}