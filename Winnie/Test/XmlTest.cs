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
        [Test()]
        public void ExportTest()
        {
            // Setup map and players
            TileTypeFactory.Identifier[] tiles = new TileTypeFactory.Identifier[]
                { TileTypeFactory.Identifier.WATER,
                    TileTypeFactory.Identifier.PLAIN,
                    TileTypeFactory.Identifier.FOREST,
                    TileTypeFactory.Identifier.MOUNTAIN };

            Map map = new Map(tiles);
            Player[] players = { new Player("A", Human.Instance), new Player("B", Orc.Instance) };

            Game game = new Game(players, map, 10, true);

            game.CurrentPlayerIndex = 1;
            game.CurrentTurn = 4;

            // Add some units
            Unit a = new Unit(players[0], map.Tiles[0, 0]);
            Unit b = new Unit(players[0], map.Tiles[0, 1]);
            Unit c = new Unit(players[1], map.Tiles[1, 1]);
            Unit d = new Unit(players[1], map.Tiles[1, 1]);

            a.MovePoints = 1;
            b.MovePoints = 2;
            c.MovePoints = 0.5;
            d.MovePoints = 0;

            b.Life = 1;
            d.Life = -5;

            // Save as XML
            Stream stream = new MemoryStream();
            Xml.GameToXml(game, stream);
            Assert.IsTrue(stream.Length > 1000);

            // TODO find a better solution for XML comparison.

            //string path = Path.Combine("..", Path.Combine("..", Path.Combine("fixtures", Path.Combine("save0.xml"))));
            //System.Xml.XmlTextReader expected = new System.Xml.XmlTextReader(new FileStream(path, FileMode.Open, FileAccess.Read));
        }
    }
}

