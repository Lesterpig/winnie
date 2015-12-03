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
            Assert.AreEqual(TileTypeFactory.Identifier.WATER, m.Tiles[0].TileType.Type);
            Assert.AreEqual(TileTypeFactory.Identifier.PLAIN, m.Tiles[1].TileType.Type);
            Assert.AreEqual(TileTypeFactory.Identifier.FOREST, m.Tiles[2].TileType.Type);
            Assert.AreEqual(TileTypeFactory.Identifier.MOUNTAIN, m.Tiles[3].TileType.Type);

            Assert.AreEqual(0, m.Tiles[0].Position);
            Assert.AreEqual(1, m.Tiles[1].Position);
            Assert.AreEqual(2, m.Tiles[2].Position);
            Assert.AreEqual(3, m.Tiles[3].Position);
        }
    }
}

