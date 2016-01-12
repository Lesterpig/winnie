using Core;
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MGUI
{
    public class UnitShow : Blittable
    {
        Game1 game;
        public List<Unit> ListUnits { get; private set; }

        public UnitShow(Game1 g)
        {
            game = g;
            ListUnits = new List<Unit>();
            Unit.GeneratorSeed = g.Seed;

            foreach (Core.Player p in game.GameModel.Players)
            {
                foreach (Core.Unit u in p.Units)
                {
                    ListUnits.Add(Unit.New(u));
                }
            }
        }

        void BlitQty()
        {
            foreach (Tile t in game.GameModel.Map.Tiles)
            {
                int count = t.Units.Count(unit => unit.Alive);
                if (count > 1)
                {
                    int shift = game.TileSize * 2 / 3 - 20;
                    Vector2 position = new Vector2(game.TileSize * t.Point.x + shift, game.TileSize * t.Point.y + shift);
                    game.CharacterBatch.DrawString(game.MainFont, count.ToString(), position, Color.Black);
                }
            }
        }

        public void Blit()
        {

            Unit selected = null;

            foreach (Unit elem in ListUnits.Where(unit => unit.UnitModel.Alive))
            {
                if (elem.UnitModel != game.SelectedUnit)
                    elem.Blit(game.CharacterBatch, game.Character, game.SquareSize);
                else
                    selected = elem;
            }

            // Draw selected unit over all other units
            if (selected != null)
                selected.Blit(game.CharacterBatch, game.Character, game.SquareSize);

            BlitQty();
        }
    }
}

