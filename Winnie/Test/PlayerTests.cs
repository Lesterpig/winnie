﻿using NUnit.Framework;
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

        private void addUnitsToPlayer(Player p) {
            Tile water = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.WATER));
            Tile plain = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN));
            Tile mountain = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.MOUNTAIN));

            new Unit(p, water);
            new Unit(p, mountain);
            Unit a = new Unit(p, plain);
            p.AddUnit(a); // should not be added twice
        }
    }
}

