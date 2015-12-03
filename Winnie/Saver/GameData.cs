using System;
using Core;

namespace Saver
{
    public class GameData
    {
        public TileData[] Tiles;
        public PlayerData[] Players;
        public int CurrentTurn;
        public int Turns;
        public int CurrentPlayerIndex;
        public bool CheatMode;

        public GameData()
        {
        }

        public GameData(Game g)
        {   
            this.Turns = g.Turns;
            this.CurrentTurn = g.CurrentTurn;
            this.CurrentPlayerIndex = g.CurrentPlayerIndex;
            this.CheatMode = g.CheatMode;

            this.Tiles = new TileData[g.Map.Tiles.Length];

            for (int i = 0; i < g.Map.Tiles.Length; i++)
            {
                this.Tiles[i] = new TileData((Tile)g.Map.Tiles[i], i);
            }

            this.Players = new PlayerData[g.Players.Length];
            for (int i = 0; i < g.Players.Length; i++)
            {
                this.Players[i] = new PlayerData((Player) g.Players.GetValue(i));
            }
        }
    }
}

