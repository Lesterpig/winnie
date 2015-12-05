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
                this.Tiles[i] = new TileData((Tile)g.Map.Tiles[i]);
            }

            this.Players = new PlayerData[g.Players.Length];
            for (int i = 0; i < g.Players.Length; i++)
            {
                this.Players[i] = new PlayerData((Player) g.Players.GetValue(i));
            }
        }

        public Game Rebuild()
        {
            var map = this.RebuildMap();
            var players = this.RebuildPlayers(map);
            var game = new Game(players, map, this.Turns, this.CheatMode);
            game.CurrentTurn = this.CurrentTurn;
            game.CurrentPlayerIndex = this.CurrentPlayerIndex;
            
            return game;
        }

        private Map RebuildMap()
        {
            var tiles = new TileTypeFactory.Identifier[this.Tiles.Length];

            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = this.Tiles[i].Type;
            }

			return new Map(tiles, (int)Math.Sqrt(tiles.Length), (int)Math.Sqrt(tiles.Length));
        }

        private Player[] RebuildPlayers(Map map)
        {
            var players = new Player[this.Players.Length];

            for (int i = 0; i < players.Length; i++)
            {
                players[i] = this.Players[i].Rebuild(map);
            }

            return players;
        }
    }
}

