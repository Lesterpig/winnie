using NUnit.Framework;
using System;
using Core;

namespace Test
{
    [TestFixture()]
    public class ActionTests
    {

        protected Player _a;
        protected Player _b;

        protected Tile _from;
        protected Tile _toEnemy;
        protected Tile _toFree;

        protected Unit _attacker;
        protected Unit _defender;


        [SetUp()]
        public void Init()
        {
            _a = new Player("PlayerA", Elf.Instance);
            _b = new Player("PlayerB", Human.Instance);

            _from = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN));
            _toEnemy = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.MOUNTAIN));
            _toFree = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.MOUNTAIN));

            _attacker = UnitFactory.Build(_a, _from);
            _defender = UnitFactory.Build(_b, _toEnemy);

            _a.StartTurn();
        }

        [Test()]
        public void MoveTest()
        {
            Move action = new Move(_attacker, _toFree);

            action.Execute();
            Assert.AreSame(_toFree, _attacker.Tile);
            Assert.AreEqual(0, _attacker.MovePoints);
            Assert.IsFalse(_from.Units.Contains(_attacker));
            Assert.IsTrue(_toFree.Units.Contains(_attacker));

            action.ReverseExecute();
            Assert.AreSame(_from, _attacker.Tile);
            Assert.AreEqual(2, _attacker.MovePoints);
            Assert.IsTrue(_from.Units.Contains(_attacker));
            Assert.IsFalse(_toFree.Units.Contains(_attacker));
        }

        [Test()]
        public void NearBattleWinTest()
        {
            Battle action = new Battle(_attacker, _defender, false);

            Game.Random = new CustomRandom(CustomRandom.Mode.LOW);

            action.Execute();
            Assert.AreSame(_from, _attacker.Tile);
            Assert.AreEqual(0, _attacker.MovePoints);
            Assert.AreEqual(_defender.Race.Life - 2, _defender.Life);
            Assert.AreEqual(_attacker.Race.Life, _attacker.Life);

            action.ReverseExecute();
            Assert.AreSame(_from, _attacker.Tile);
            Assert.AreEqual(2, _attacker.MovePoints);
            Assert.AreEqual(_defender.Race.Life, _defender.Life);
            Assert.AreEqual(_attacker.Race.Life, _attacker.Life);
        }

        [Test()]
        public void NearBattleKillTest()
        {
            Battle action = new Battle(_attacker, _defender, false);
            _defender.Life = 1;

            Game.Random = new CustomRandom(CustomRandom.Mode.LOW);

            action.Execute();
            Assert.AreSame(_toEnemy, _attacker.Tile);
            Assert.AreEqual(0, _attacker.MovePoints);
            Assert.AreEqual(1 - 2, _defender.Life);
            Assert.AreEqual(_attacker.Race.Life, _attacker.Life);

            action.ReverseExecute();
            Assert.AreSame(_from, _attacker.Tile);
            Assert.AreEqual(2, _attacker.MovePoints);
            Assert.AreEqual(1, _defender.Life);
            Assert.AreEqual(_attacker.Race.Life, _attacker.Life);
        }

        [Test()]
        public void NearBattleLoseTest()
        {
            Battle action = new Battle(_attacker, _defender, false);

            Game.Random = new CustomRandom(CustomRandom.Mode.HIGH);

            action.Execute();
            Assert.AreSame(_from, _attacker.Tile);
            Assert.AreEqual(0, _attacker.MovePoints);
            Assert.AreEqual(_defender.Race.Life, _defender.Life);
            Assert.AreEqual(_attacker.Race.Life - 5, _attacker.Life);

            action.ReverseExecute();
            Assert.AreSame(_from, _attacker.Tile);
            Assert.AreEqual(2, _attacker.MovePoints);
            Assert.AreEqual(_defender.Race.Life, _defender.Life);
            Assert.AreEqual(_attacker.Race.Life, _attacker.Life);
        }

        [Test()]
        public void RangedBattleTest()
        {
            Battle action = new Battle(_attacker, _defender, true);

            Game.Random = new CustomRandom(CustomRandom.Mode.HIGH);

            action.Execute();
            Assert.AreSame(_from, _attacker.Tile);
            Assert.AreEqual(1, _attacker.MovePoints);
            Assert.AreEqual(_defender.Race.Life - 5, _defender.Life);
            Assert.AreEqual(_attacker.Race.Life, _attacker.Life);

            action.ReverseExecute();
            Assert.AreSame(_from, _attacker.Tile);
            Assert.AreEqual(2, _attacker.MovePoints);
            Assert.AreEqual(_defender.Race.Life, _defender.Life);
            Assert.AreEqual(_attacker.Race.Life, _attacker.Life);
        }

        [Test()]
        public void RangedBattleKillTest()
        {
            Battle action = new Battle(_attacker, _defender, true);
            _defender.Life = 2;

            Game.Random = new CustomRandom(CustomRandom.Mode.HIGH);

            action.Execute();
            Assert.AreSame(_from, _attacker.Tile);
            Assert.AreEqual(1, _attacker.MovePoints);
            Assert.AreEqual(2 - 5, _defender.Life);
            Assert.AreEqual(_attacker.Race.Life, _attacker.Life);

            action.ReverseExecute();
            Assert.AreSame(_from, _attacker.Tile);
            Assert.AreEqual(2, _attacker.MovePoints);
            Assert.AreEqual(2, _defender.Life);
            Assert.AreEqual(_attacker.Race.Life, _attacker.Life);
        }
    }
}

