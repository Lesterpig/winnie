using NUnit.Framework;
using System;
using System.Collections.Generic;
using Core;

namespace Test
{
    [TestFixture()]
    public class PlayerTests
    {
        [Test()]
        public void CreateTest()
        {
            Player p = new Player("Tester", Human.Instance);

            Assert.AreEqual("Tester", p.Name);
            Assert.AreSame(Human.Instance, p.Race);
        }

        [Test()]
        public void AddUnitTest()
        {
            Player p = new Player("Tester", Human.Instance);
            addUnitsToPlayer(p);

            ISet<Unit> units = p.Units;
            Assert.AreEqual(3, units.Count);
        }

        [Test()]
        public void ScoreTest()
        {
            Player p = new Player("Tester", Human.Instance);
            Assert.AreEqual(0, p.Score);

            addUnitsToPlayer(p);
            Assert.AreEqual(3, p.Score);
        }

        [Test()]
        public void StartTurnTest()
        {   
            Player p = new Player("Tester", Human.Instance);
            Tile t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.WATER));
            Unit a = UnitFactory.Build(p, t);
            Unit b = UnitFactory.Build(p, t);

            Assert.AreEqual(0, a.MovePoints);
            Assert.AreEqual(0, b.MovePoints);

            p.StartTurn();

            Assert.AreEqual(2, a.MovePoints);
            Assert.AreEqual(2, b.MovePoints);
        }

        private void addUnitsToPlayer(Player p) {
            Tile water = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.WATER));
            Tile plain = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN));
            Tile mountain = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.MOUNTAIN));

            UnitFactory.Build(p, water);
            UnitFactory.Build(p, mountain);
            Unit a = UnitFactory.Build(p, plain);
            p.AddUnit(a); // should not be added twice
        }
    }
}

