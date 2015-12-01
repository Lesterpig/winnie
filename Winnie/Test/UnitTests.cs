﻿using NUnit.Framework;
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

        [Test()]
        public void AliveTest()
        {
            Unit a = new Unit(_p, _t);
            Assert.AreEqual(15, a.Life);
            Assert.IsTrue(a.Alive);
            a.Life = 0;
            Assert.IsFalse(a.Alive);
            a.Life = -5;
            Assert.IsFalse(a.Alive);
        }

        [Test()]
        public void AttackPointsTest()
        {
            Unit a = new Unit(_p, _t);
            Assert.AreEqual(6, a.AttackPoints);
            a.Life = 7;
            Assert.AreEqual(3, a.AttackPoints);
        }

        [Test()]
        public void DefensePointsTest()
        {
            Unit a = new Unit(_p, _t);
            Assert.AreEqual(3, a.DefensePoints);
            a.Life = 7;
            Assert.AreEqual(2, a.DefensePoints);
            a.Life = 1;
            Assert.AreEqual(1, a.DefensePoints);
        }

        [Test()]
        public void MoveTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Forest"));
            Unit u = new Unit(_p, a);

            Assert.AreEqual(0, u.MovePoints);
            Assert.AreSame(a, u.Tile);
            Assert.IsTrue(a.Units.Contains(u));
            Assert.IsFalse(b.Units.Contains(u));
            Assert.Throws<Unit.NotEnoughMovePointsException>(delegate()
                {
                    u.Move(b);
                });

            // Start turn to init movePoints
            _p.StartTurn();
            Assert.AreEqual(2, u.MovePoints);
            u.Move(b);
            Assert.AreEqual(1, u.MovePoints);
            Assert.AreSame(b, u.Tile);
            Assert.IsFalse(a.Units.Contains(u));
            Assert.IsTrue(b.Units.Contains(u));
        }

        [Test()]
        public void FreeMoveTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Forest"));
            Unit u = new Unit(_p, a);
            u.Move(b, true);
            Assert.AreEqual(0, u.MovePoints);
            Assert.AreSame(b, u.Tile);
            Assert.IsFalse(a.Units.Contains(u));
            Assert.IsTrue(b.Units.Contains(u));
        }

        [Test()]
        public void MovementNotAllowedTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Water"));
            Unit u = new Unit(new Player("Elf", Elf.Instance), a);
            Assert.Throws<Unit.MovementNotAllowedException>(delegate()
                {
                    u.Move(b);
                });
        }

        [Test()]
        public void MoveToAllyTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Plain"));
            Unit me = new Unit(_p, a);
            Unit ally = new Unit(new Player("Ally", Human.Instance), b);

            me.Player.StartTurn();
            me.Move(b);

            Assert.AreEqual(0, a.Units.Count);
            Assert.AreEqual(2, b.Units.Count);
            Assert.IsTrue(b.Units.Contains(me));
            Assert.IsTrue(b.Units.Contains(ally));
        }

        [Test()]
        public void MoveToDeadEnemyTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Plain"));
            Unit me = new Unit(_p, a);
            Unit enemy = new Unit(new Player("Enemy", Orc.Instance), b);
            enemy.Life = 0;

            me.Player.StartTurn();
            me.Move(b);

            Assert.AreEqual(0, a.Units.Count);
            Assert.AreEqual(2, b.Units.Count);
            Assert.IsTrue(b.Units.Contains(me));
            Assert.IsTrue(b.Units.Contains(enemy));
        }

        [Test()]
        public void MoveToAliveEnemyTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Plain"));
            Unit me = new Unit(_p, a);
            Unit enemy = new Unit(new Player("Enemy", Orc.Instance), b);

            me.Player.StartTurn();
            Assert.Throws<Unit.EnnemiesRemainingException>(delegate()
                {
                    me.Move(b);
                });

            Assert.AreEqual(1, a.Units.Count);
            Assert.AreEqual(1, b.Units.Count);
            Assert.IsTrue(a.Units.Contains(me));
            Assert.IsTrue(b.Units.Contains(enemy));
        }

        [Test()]
        public void RandomTest()
        {
            Game.Random = new Random(0);
            Assert.AreEqual(56, Game.Random.Next(100));
            Assert.AreEqual(1, Game.Random.Next(1, 6));

            Game.Random = new Random(1);
            Assert.AreEqual(25, Game.Random.Next(100));
            Assert.AreEqual(3, Game.Random.Next(1, 6));

            Game.Random = new Random(2);
            Assert.AreEqual(94, Game.Random.Next(100));
            Assert.AreEqual(5, Game.Random.Next(1, 6));

            Game.Random = new Random(0);
            Assert.AreEqual(4, Game.Random.Next(0, 6));
        }

        [Test()]
        public void AttackNotAllowedTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Water"));
            Unit me = new Unit(new Player("Me", Orc.Instance), a); // Orcs cannot attack on water
            Unit enemy = new Unit(_p, b);

            me.Player.StartTurn();

            Assert.Throws<Unit.MovementNotAllowedException>(delegate()
                {
                    me.Attack(enemy);
                });
        }

        [Test()]
        public void AttackMovementPointsTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Plain"));
            Unit me = new Unit(_p, a);
            Unit enemy = new Unit(new Player("Enemy", Orc.Instance), b);

            Assert.Throws<Unit.NotEnoughMovePointsException>(delegate()
                {
                    me.Attack(enemy);
                });
        }

        [Test()]
        public void AttackSameRaceTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Plain"));
            Unit me = new Unit(_p, a);
            Unit ally = new Unit(new Player("Ally", Human.Instance), b);

            me.Player.StartTurn();
            Assert.Throws<Unit.SameRaceAttackException>(delegate()
                {
                    me.Attack(ally);
                });
        }

        [Test()]
        public void AttackWinTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Plain"));
            Unit me = new Unit(_p, a);
            Unit enemy = new Unit(new Player("Enemy", Orc.Instance), b);

            me.Player.StartTurn();
            Game.Random = new Random(1);
            Unit.AttackResult result = me.Attack(enemy);

            Assert.AreSame(me, result.Winner);
            Assert.AreSame(enemy, result.Loser);
            Assert.AreEqual(3, result.Dmg);

            Assert.AreEqual(me.Race.Life, me.Life);
            Assert.AreEqual(enemy.Race.Life - 3, enemy.Life);
        }

        [Test()]
        public void AttackLoseTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Plain"));
            Unit me = new Unit(_p, a);
            Unit enemy = new Unit(new Player("Enemy", Orc.Instance), b);

            me.Player.StartTurn();
            Game.Random = new Random(2);
            Unit.AttackResult result = me.Attack(enemy);

            Assert.AreSame(enemy, result.Winner);
            Assert.AreSame(me, result.Loser);
            Assert.AreEqual(5, result.Dmg);

            Assert.AreEqual(me.Race.Life - 5, me.Life);
            Assert.AreEqual(enemy.Race.Life, enemy.Life);
        }

        [Test()]
        public void AttackKillTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Plain"));
            Unit me = new Unit(_p, a);
            Unit enemy = new Unit(new Player("Enemy", Orc.Instance), b);
            enemy.Life = 3;

            me.Player.StartTurn();
            Game.Random = new Random(1);
            Unit.AttackResult result = me.Attack(enemy);

            Assert.AreSame(me, result.Winner);
            Assert.AreSame(enemy, result.Loser);
            Assert.AreEqual(3, result.Dmg);
        
            Assert.AreEqual(me.Race.Life, me.Life);
            Assert.AreEqual(0, enemy.Life);
        }

        [Test()]
        public void RangedAttackTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get("Water"));
            Unit me = new Unit(new Player("Me", Elf.Instance), a);
            Unit enemy = new Unit(_p, b);

            me.Player.StartTurn();
            Game.Random = new Random(0);
            Unit.AttackResult result = me.Attack(enemy, true);

            Assert.AreSame(me, result.Winner);
            Assert.AreSame(enemy, result.Loser);
            Assert.AreEqual(4, result.Dmg);

            Assert.AreEqual(me.Race.Life, me.Life);
            Assert.AreEqual(enemy.Race.Life - 4, enemy.Life);
        }


    }
}

