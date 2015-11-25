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
            this._tile = t;
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
            get { return _tile; }
        }

        private int _life;
        public int Life
        {
            get { return _life; }
        }

        public float MovePoints
        {
            get;
            set;
        }

        public int VictoryPoints
        {
            get
            {
                return this.Life > 0 ? this.Race.GetVictoryPoints(this.Tile.TileType) : 0;
            }
        }

        public List<Action> Possibilities
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}