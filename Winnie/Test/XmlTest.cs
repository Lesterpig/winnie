using NUnit.Framework;
using System;
using System.IO;
using Core;
using Saver;

namespace Test
{
    [TestFixture()]
    public class XmlTest
    {   

        private Game BuildGame()
        {
            // Setup map and players
            TileTypeFactory.Identifier[] tiles = new TileTypeFactory.Identifier[]
                { TileTypeFactory.Identifier.WATER,
                    TileTypeFactory.Identifier.PLAIN,
                    TileTypeFactory.Identifier.FOREST,
                    TileTypeFactory.Identifier.MOUNTAIN };

            Map map = new Map(tiles, 2, 2);
            Player[] players = { new Player("A", Human.Instance), new Player("B", Orc.Instance) };

            Game game = new Game(players, map, 10, true);

            game.CurrentPlayerIndex = 1;
            game.CurrentTurn = 4;

            // Add some units
            Unit a = UnitFactory.Build(players[0], map.Tiles[0]);
            Unit b = UnitFactory.Build(players[0], map.Tiles[1]);
            Unit c = UnitFactory.Build(players[1], map.Tiles[2]);
            Unit d = UnitFactory.Build(players[1], map.Tiles[3]);

            a.MovePoints = 1;
            b.MovePoints = 2;
            c.MovePoints = 0.5;
            d.MovePoints = 0;

            b.Life = 1;
            d.Life = -5;

            return game;
        }

        [Test()]
        public void ExportTest()
        {
            // Save as XML
            Stream stream = new MemoryStream();
            Xml.GameToXml(this.BuildGame(), stream);
            Assert.IsTrue(stream.Length > 1000);

            // TODO find a better solution for XML comparison.
        }

        [Test()]
        public void ImportTest()
        {
            string path = Path.Combine("..", Path.Combine("..", Path.Combine("fixtures", Path.Combine("save0.xml"))));
            Stream input = new FileStream(path, FileMode.Open, FileAccess.Read);

            Game game = Xml.XmlToGame(input);

            Assert.AreEqual(4, game.CurrentTurn);
            Assert.AreEqual(10, game.Turns);
            Assert.AreEqual(1, game.CurrentPlayerIndex);
            Assert.IsTrue(game.CheatMode);

            Assert.AreEqual(4, game.Map.Tiles.Length);

            Assert.AreSame(TileTypeFactory.Get(TileTypeFactory.Identifier.WATER), game.Map.Tiles[0].TileType);
            Assert.AreSame(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN), game.Map.Tiles[1].TileType);
            Assert.AreSame(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST), game.Map.Tiles[2].TileType);
            Assert.AreSame(TileTypeFactory.Get(TileTypeFactory.Identifier.MOUNTAIN), game.Map.Tiles[3].TileType);

            Assert.AreEqual(1, game.Map.Tiles[0].Units.Count);
            Assert.AreEqual(1, game.Map.Tiles[1].Units.Count);
            Assert.AreEqual(0, game.Map.Tiles[2].Units.Count);
            Assert.AreEqual(2, game.Map.Tiles[3].Units.Count);

            Assert.AreEqual(2, game.Players.Length);

            Assert.AreEqual("A", game.Players[0].Name);
            Assert.AreEqual("B", game.Players[1].Name);

            Assert.AreEqual(2, game.Players[0].Units.Count);
            Assert.AreEqual(2, game.Players[1].Units.Count);

            foreach (Unit u in game.Map.Tiles[1].Units)
            {
                Assert.AreEqual(2, u.MovePoints);
                Assert.AreEqual(1, u.Life);
            }
        }

        [Test()]
        public void MultipleExportImportTest()
        {
            Stream stream = new MemoryStream();
            Xml.GameToXml(this.BuildGame(), stream);

            stream.Position = 0;
            Game rebuilt = Xml.XmlToGame(stream);
            Stream stream2 = new MemoryStream();
            Xml.GameToXml(rebuilt, stream2);

            stream.Position = 0;
            stream2.Position = 0;

            Assert.IsTrue(stream.Length > 1000);
            Assert.AreEqual(stream, stream2);
        }
    }
}

