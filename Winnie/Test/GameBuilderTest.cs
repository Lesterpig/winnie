using NUnit.Framework;
using System;
using Core;

namespace Test
{
    [TestFixture()]
    public class GameBuilderTest
    {
        [Test()]
        public void DemoBuildTest()
        {
            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2);

            Assert.AreEqual(6*6, g.Map.Tiles.Length);
            Assert.AreEqual(4, g.Players[0].Units.Count);
            Assert.AreEqual(4, g.Players[1].Units.Count);
        }

        [Test()]
        public void SmallBuildTest()
        {
            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<SmallGameType>(p1, p2);

            Assert.AreEqual(10*10, g.Map.Tiles.Length);
            Assert.AreEqual(6, g.Players[0].Units.Count);
            Assert.AreEqual(6, g.Players[1].Units.Count);
        }

        [Test()]
        public void StandardBuildTest()
        {
            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<StandardGameType>(p1, p2);

            Assert.AreEqual(14*14, g.Map.Tiles.Length);
            Assert.AreEqual(8, g.Players[0].Units.Count);
            Assert.AreEqual(8, g.Players[1].Units.Count);
        }

        [Test()]
        public void CheatModeBuildTest()
        {
            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g1 = GameBuilder.New<StandardGameType>(p1, p2, true);
            Game g2 = GameBuilder.New<StandardGameType>(p1, p2, false);

            Assert.IsTrue(g1.CheatMode);
            Assert.IsFalse(g2.CheatMode);
        }

        [Test()]
        public void FixedSeedBuildTest()
        {
            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2, false, 1);

            Assert.AreEqual(TileTypeFactory.Identifier.MOUNTAIN, g.Map.Tiles[0].TileType.Type);
            Assert.AreEqual(TileTypeFactory.Identifier.MOUNTAIN, g.Map.Tiles[10].TileType.Type);
            Assert.AreEqual(TileTypeFactory.Identifier.PLAIN, g.Map.Tiles[20].TileType.Type);
            Assert.AreEqual(TileTypeFactory.Identifier.PLAIN, g.Map.Tiles[30].TileType.Type);
        }

        [Test()]
        public void SameRaceErrorTest()
        {
            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Human.Instance);

            Assert.Throws<GameBuilder.SameRaceException>(delegate()
                {
                    GameBuilder.New<DemoGameType>(p1, p2);
                });
        }
    }
}

