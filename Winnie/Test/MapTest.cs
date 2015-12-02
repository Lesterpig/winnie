using NUnit.Framework;
using System;
using Core;

namespace Test
{
    [TestFixture()]
    public class MapTest
    {
        [Test()]
        public void ConstructorTest()
        {
            TileTypeFactory.Identifier[] tiles = new TileTypeFactory.Identifier[]
                { TileTypeFactory.Identifier.WATER,
                  TileTypeFactory.Identifier.PLAIN,
                  TileTypeFactory.Identifier.FOREST,
                  TileTypeFactory.Identifier.MOUNTAIN };

            Map m = new Map(tiles);

            Assert.AreEqual(4, m.Tiles.Length);
            Assert.AreEqual("Water", m.Tiles[0, 0].TileType.Type);
            Assert.AreEqual("Plain", m.Tiles[0, 1].TileType.Type);
            Assert.AreEqual("Forest", m.Tiles[1, 0].TileType.Type);
            Assert.AreEqual("Mountain", m.Tiles[1, 1].TileType.Type);
        }
    }
}

