using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace winnie
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

        float GetRequiredMovePoints(TileType tileType);

        float GetVictoryPoints(TileType tileType);

        bool CanMove(TileType tileType);
    }
}