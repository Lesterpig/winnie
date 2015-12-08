using NUnit.Framework;
using System;
using Core;

namespace Test
{
    [TestFixture()]
    public class NaiveMapTests
    {
        [Test()]
        public void BuildTest()
        {
            var map = (new NaiveMap()).Generate(6);

            Assert.AreEqual(TileTypeFactory.Identifier.WATER, map.getTile(0, 0).TileType.Type);
            Assert.AreEqual(TileTypeFactory.Identifier.PLAIN, map.getTile(1, 0).TileType.Type);
            Assert.AreEqual(TileTypeFactory.Identifier.FOREST, map.getTile(2, 0).TileType.Type);
            Assert.AreEqual(TileTypeFactory.Identifier.MOUNTAIN, map.getTile(3, 0).TileType.Type);
            Assert.AreEqual(TileTypeFactory.Identifier.WATER, map.getTile(4, 0).TileType.Type);
        }
    }
}

