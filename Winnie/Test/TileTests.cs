using NUnit.Framework;
using System;
using Core;

namespace Test
{
    [TestFixture()]
    public class TileTests
    {   

        private Game g;

        [SetUp()]
        public void Init()
        {
            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            g = GameBuilder.New<DemoGameType>(p1, p2);
        }

        [Test()]
        public void NeighborTest()
        {
            Assert.AreSame(g.Map.Tiles[1], g.Map.Tiles[0].GetNeighbor(new Tile.Diff(1, 0)));
            Assert.AreSame(g.Map.Tiles[1], g.Map.Tiles[0].GetNeighbor(new Tile.Diff(1, 0))); // twice to test cache
            Assert.AreSame(g.Map.Tiles[g.Map.SizeX], g.Map.Tiles[0].GetNeighbor(new Tile.Diff(0, 1)));
            Assert.AreSame(g.Map.Tiles[g.Map.SizeX + 1], g.Map.Tiles[0].GetNeighbor(new Tile.Diff(1, 1)));
            Assert.AreSame(g.Map.Tiles[0], g.Map.Tiles[g.Map.SizeX + 1].GetNeighbor(new Tile.Diff(-1, -1)));
        }

        [Test()]
        public void NeighborBoundsTest()
        {
            Assert.IsNull(g.Map.Tiles[0].GetNeighbor(new Tile.Diff(-1, 0)));
            Assert.IsNull(g.Map.Tiles[0].GetNeighbor(new Tile.Diff(0, -1)));
            Assert.IsNull(g.Map.Tiles[0].GetNeighbor(new Tile.Diff(g.Map.SizeX, 0)));
            Assert.IsNull(g.Map.Tiles[g.Map.SizeX-1].GetNeighbor(new Tile.Diff(1, 0)));
            Assert.IsNull(g.Map.Tiles[0].GetNeighbor(new Tile.Diff(0, g.Map.SizeY)));
            Assert.IsNull(g.Map.Tiles[g.Map.SizeX * (g.Map.SizeY-1)].GetNeighbor(new Tile.Diff(0, 1)));
        }
    }
}

