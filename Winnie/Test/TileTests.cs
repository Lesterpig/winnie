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

            g = GameBuilder.New<DemoGameType,PerlinMap>(p1, p2);
        }

        [Test()]
        public void ConstructorTest()
        {
            Tile a = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));

            Assert.AreSame(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST), a.TileType);
            Assert.AreEqual(-1, a.Position);
            Assert.AreEqual(0, a.Units.Count);

            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST), 1);

            Assert.AreSame(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST), b.TileType);
            Assert.AreEqual(1, b.Position);
            Assert.AreEqual(0, b.Units.Count);
        }

        [Test()]
        public void PointTest()
        {
            Map m = (new PerlinMap()).Generate(2);
            Assert.AreEqual(0, m.getTile(0, 0).Point.x);
            Assert.AreEqual(0, m.getTile(0, 0).Point.y);
            Assert.AreEqual(1, m.getTile(1, 1).Point.x);
            Assert.AreEqual(1, m.getTile(1, 1).Point.y);
        }

        [Test()]
        public void InvalidPointTest()
        {
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Assert.IsNull(t.Point);
        }

        [Test()]
        public void AddUnitTest()
        {
            Player p = new Player("P", Human.Instance);
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Unit u = new Unit(p, t);

            Assert.AreEqual(0, t.Units.Count);
            t.AddUnit(u);
            Assert.AreEqual(1, t.Units.Count);
        }

        [Test()]
        public void AddSameUnitTest()
        {
            Player p = new Player("P", Human.Instance);
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Unit u = new Unit(p, t);

            Assert.AreEqual(0, t.Units.Count);
            t.AddUnit(u);
            t.AddUnit(u);
            t.AddUnit(u);
            Assert.AreEqual(1, t.Units.Count);
        }

        [Test()]
        public void RemoveUnitTest()
        {
            Player p = new Player("P", Human.Instance);
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Unit u = new Unit(p, t);

            Assert.AreEqual(0, t.Units.Count);
            t.AddUnit(u);
            Assert.AreEqual(1, t.Units.Count);
            t.RemoveUnit(u);
            Assert.AreEqual(0, t.Units.Count);
        }

        [Test()]
        public void RemoveMissingUnitTest()
        {
            Player p = new Player("P", Human.Instance);
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Unit u = new Unit(p, t);

            Assert.AreEqual(0, t.Units.Count);
            t.RemoveUnit(u);
            Assert.AreEqual(0, t.Units.Count);
        }

        [Test()]
        public void StrongestUnitNullTest()
        {
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Assert.IsNull(t.StrongestUnit);
        }

        [Test()]
        public void StrongestUnitSoloTest()
        {
            Player p = new Player("P", Human.Instance);
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Unit u = UnitFactory.Build(p, t);

            Assert.AreSame(u, t.StrongestUnit);
        }

        [Test()]
        public void StrongestUnitMultipleTest()
        {
            Player p = new Player("P", Human.Instance);
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Unit a = UnitFactory.Build(p, t);
            Unit b = UnitFactory.Build(p, t);
            Unit c = UnitFactory.Build(p, t);

            a.Life = 2;
            c.Life = 4;

            Assert.AreSame(b, t.StrongestUnit);
        }

        [Test()]
        public void StrongestUnitDeadTest()
        {
            Player p = new Player("P", Human.Instance);
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Unit u = UnitFactory.Build(p, t);

            u.Life = 0;

            Assert.IsNull(t.StrongestUnit);
        }

        [Test()]
        public void StrongestUnitFatTest()
        {
            Player p1 = new Player("A", Human.Instance);
            Player p2 = new Player("B", Orc.Instance);

            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Unit a = UnitFactory.Build(p1, t);
            Unit b = UnitFactory.Build(p1, t);

            a.Life = 0;
            b.Life = -1;

            Unit c = UnitFactory.Build(p2, t);
            Unit d = UnitFactory.Build(p2, t);

            c.Life = -2;

            Assert.AreSame(d, t.StrongestUnit);
        }
            
        [Test()]
        public void MasterRaceNullTest()
        {
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Assert.IsNull(t.MasterRace);
        }

        [Test()]
        public void MasterRaceSoloTest()
        {
            Player p = new Player("P", Human.Instance);
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            UnitFactory.Build(p, t);

            Assert.AreSame(Human.Instance, t.MasterRace);
        }

        [Test()]
        public void MasterRaceDeadTest()
        {
            Player p = new Player("P", Human.Instance);
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Unit u = UnitFactory.Build(p, t);

            u.Life = 0;

            Assert.IsNull(t.MasterRace);
        }

        [Test()]
        public void MasterRaceFatTest()
        {
            Player p1 = new Player("A", Human.Instance);
            Player p2 = new Player("B", Orc.Instance);

            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
            Unit a = UnitFactory.Build(p1, t);
            Unit b = UnitFactory.Build(p1, t);

            a.Life = 0;
            b.Life = -1;

            Unit c = UnitFactory.Build(p2, t);
            UnitFactory.Build(p2, t);

            c.Life = -2;

            Assert.AreSame(Orc.Instance, t.MasterRace);
        }

        [Test()]
        public void DiffTest()
        {
            var a = new Tile.Diff(1, 2);
            var b = new Tile.Diff(1, 2);
            var c = new Tile.Diff(-1, 2);

            Assert.AreEqual(1, a.Dx);
            Assert.AreEqual(2, a.Dy);
            Assert.AreEqual(a, b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a.GetHashCode(), c.GetHashCode());
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
            Assert.IsNull(g.Map.Tiles[0].GetNeighbor(new Tile.Diff((int) g.Map.SizeX, 0)));
            Assert.IsNull(g.Map.Tiles[g.Map.SizeX-1].GetNeighbor(new Tile.Diff(1, 0)));
            Assert.IsNull(g.Map.Tiles[0].GetNeighbor(new Tile.Diff(0, (int) g.Map.SizeY)));
            Assert.IsNull(g.Map.Tiles[g.Map.SizeX * (g.Map.SizeY-1)].GetNeighbor(new Tile.Diff(0, 1)));
        }
    }
}

