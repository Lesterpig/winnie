using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public interface Race
    {
        int Life
        {
            get;
        }

        int Armor
        {
            get;
        }

        int Attack
        {
            get;
        }

		//Used for C++ wrapper
		int Identifier {
			get;
		}

        double GetRequiredMovePoints(TileType tileType);
        int GetVictoryPoints(TileType tileType);
        bool CanMove(TileType tileType);
        bool CanDoRangedAttack(TileType tileType);
    }
}