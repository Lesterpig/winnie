using NUnit.Framework;
using System;
using Core;

namespace Test
{
    [TestFixture()]
    public class UnitTests
    {   
        private Tile _t;
        private Player _p;

        [SetUp]
        public void Init()
        {
            _t = new Tile(TileTypeFactory.Get("Plain"));
            _p = new Player("Tester", Human.Instance);
        }

        [Test()]
        public void CreateTest()
        {
            Unit a = new Unit(_p, _t);
            Assert.AreSame(a.Tile, _t);
            Assert.AreSame(a.Player, _p);
            Assert.AreSame(a.Race, Human.Instance);
            Assert.AreEqual(a.Life, Human.Instance.Life);
        }

        [Test()]
        public void VictoryPointsTest()
        {
            Unit a = new Unit(_p, _t);
            Assert.AreEqual(a.VictoryPoints, 2);
        }
    }
}

