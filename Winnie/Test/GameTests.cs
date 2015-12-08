using NUnit.Framework;
using System;
using Core;

namespace Test
{
    [TestFixture()]
    public class GameTests
    {

        Player _p1;
        Player _p2;
        Player _p3;
        Map _m;

        [SetUp()]
        public void Init()
        {
            _p1 = new Player("A", Human.Instance);
            _p2 = new Player("B", Orc.Instance);
            _p3 = new Player("C", Elf.Instance);
            _m = (new PerlinMap()).Generate(2, 1);
        }

        [Test()]
        public void ConstructorTest()
        {
            Player[] p = { _p1, _p2 };
            var g = new Game(p, _m, 2, false);

            Assert.AreEqual(2, g.Turns);
            Assert.IsFalse(g.CheatMode);
            Assert.AreSame(_p1, g.CurrentPlayer);
            Assert.AreEqual(0, g.CurrentTurn);
        }

        [Test()]
        public void NextTurnTest()
        {
            Player[] p = { _p1, _p2 };
            var g = new Game(p, _m, 2, false);

            var u = UnitFactory.Build(_p2, g.Map.getTile(0, 0));

            g.NextTurn();
            Assert.AreSame(_p2, g.CurrentPlayer);
            Assert.AreEqual(0, g.CurrentTurn);
            Assert.AreEqual(2, u.MovePoints);
        }

        [Test()]
        public void NextTurnMultipleTest()
        {
            Player[] p = { _p1, _p2 };
            var g = new Game(p, _m, 2, false);

            g.NextTurn();
            g.NextTurn();
            Assert.AreSame(_p1, g.CurrentPlayer);
            Assert.AreEqual(1, g.CurrentTurn);
        }

        [Test()]
        public void NextTurnEndOfGameTest()
        {
            Player[] p = { _p1, _p2 };
            var g = new Game(p, _m, 2, false);

            g.NextTurn();
            g.NextTurn();
            g.NextTurn();
            Assert.Throws<Game.EndOfGameException>(delegate()
                {
                    g.NextTurn();
                });

            Assert.AreEqual(2, g.CurrentTurn);
            Assert.AreSame(_p2, g.CurrentPlayer);
        }

        [Test()]
        public void WinnerNullTest()
        {
            Player[] p = { _p1, _p2 };
            var g = new Game(p, _m, 2, false);

            UnitFactory.Build(_p1, g.Map.getTile(0, 0));
            UnitFactory.Build(_p2, g.Map.getTile(0, 1));

            Assert.IsNull(g.Winner);
        }

        [Test()]
        public void WinnerEndOfGameTest()
        {
            Player[] p = { _p1, _p2 };
            var g = new Game(p, _m, 2, false);
            g.CurrentTurn = 2;

            UnitFactory.Build(_p1, g.Map.getTile(0, 0));
            UnitFactory.Build(_p2, g.Map.getTile(0, 1));

            Assert.AreEqual(1, _p1.Score);
            Assert.AreEqual(2, _p2.Score);
            Assert.AreSame(_p2, g.Winner);
        }

        [Test()]
        public void WinnerDefeatTest()
        {
            Player[] p = { _p1, _p2 };
            var g = new Game(p, _m, 2, false);

            UnitFactory.Build(_p1, g.Map.getTile(0, 0));
            var u = UnitFactory.Build(_p2, g.Map.getTile(0, 1));

            u.Life = 0;

            Assert.AreEqual(1, _p1.Score);
            Assert.AreEqual(0, _p2.Score);
            Assert.AreSame(_p1, g.Winner);
        }

        [Test()]
        public void WinnerDrawTest()
        {
            Player[] p = { _p1, _p2 };
            var g = new Game(p, _m, 2, false);
            g.CurrentTurn = 2;

            UnitFactory.Build(_p1, g.Map.getTile(0, 0));
            UnitFactory.Build(_p1, g.Map.getTile(0, 0));
            UnitFactory.Build(_p2, g.Map.getTile(0, 1));

            Assert.AreEqual(2, _p1.Score);
            Assert.AreEqual(2, _p2.Score);
            Assert.IsNull(g.Winner);
        }

        [Test()]
        public void NotEnoughPlayersTest()
        {
            Player[] p = { _p1 };

            Assert.Throws<System.NotSupportedException>(delegate()
                {
                    new Game(p, _m, 2, false);
                });
        }

        [Test()]
        public void TooMuchPlayersTest()
        {
            Player[] p = { _p1, _p2, _p3 };

            Assert.Throws<System.NotSupportedException>(delegate()
                {
                    new Game(p, _m, 2, false);
                });
        }

    }
}

