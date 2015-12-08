using NUnit.Framework;
using System;
using Core;
using System.Linq;

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
            _t = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN));
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
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
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
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST));
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
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.WATER));
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
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN));
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
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN));
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
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN));
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
        public void AttackNotAllowedTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.WATER));
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
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN));
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
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN));
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
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN));
            Unit me = new Unit(_p, a);
            Unit enemy = new Unit(new Player("Enemy", Orc.Instance), b);

            me.Player.StartTurn();
            Game.Random = new CustomRandom(CustomRandom.Mode.LOW);
            Unit.AttackResult result = me.Attack(enemy);

            Assert.AreSame(me, result.Winner);
            Assert.AreSame(enemy, result.Loser);
            Assert.AreEqual(2, result.Dmg);

            Assert.AreEqual(me.Race.Life, me.Life);
            Assert.AreEqual(enemy.Race.Life - 2, enemy.Life);
        }

        [Test()]
        public void AttackLoseTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN));
            Unit me = new Unit(_p, a);
            Unit enemy = new Unit(new Player("Enemy", Orc.Instance), b);

            me.Player.StartTurn();
            Game.Random = new CustomRandom(CustomRandom.Mode.HIGH);
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
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN));
            Unit me = new Unit(_p, a);
            Unit enemy = new Unit(new Player("Enemy", Orc.Instance), b);
            enemy.Life = 1;

            me.Player.StartTurn();
            Game.Random = new CustomRandom(CustomRandom.Mode.LOW);
            Unit.AttackResult result = me.Attack(enemy);

            Assert.AreSame(me, result.Winner);
            Assert.AreSame(enemy, result.Loser);
            Assert.AreEqual(2, result.Dmg);
        
            Assert.AreEqual(me.Race.Life, me.Life);
            Assert.AreEqual(-1, enemy.Life);
        }

        [Test()]
        public void RangedAttackTest()
        {
            Tile a = _t;
            Tile b = new Tile(TileTypeFactory.Get(TileTypeFactory.Identifier.WATER));
            Unit me = new Unit(new Player("Me", Elf.Instance), a);
            Unit enemy = new Unit(_p, b);

            me.Player.StartTurn();
            Game.Random = new CustomRandom(CustomRandom.Mode.HIGH);
            Unit.AttackResult result = me.Attack(enemy, true);

            Assert.AreSame(me, result.Winner);
            Assert.AreSame(enemy, result.Loser);
            Assert.AreEqual(5, result.Dmg);

            Assert.AreEqual(1, me.MovePoints);
            Assert.AreEqual(me.Race.Life, me.Life);
            Assert.AreEqual(enemy.Race.Life - 5, enemy.Life);
        }

        [Test()]
        public void MovePossibilitiesDeadTest()
        {   
            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            GameBuilder.New<DemoGameType>(p1, p2, false, 1);

            Unit u = p1.Units.First();
            u.Life = 0;
            var possibilities = u.MovePossibilites;
            Assert.AreEqual(0, possibilities.Count);
        }

        [Test()]
        public void MovePossibilitiesTest()
        {   
            /*
             * A o o - - -
             * o o - - - -
             * o - - - - -
             * - - - - - -
             * - - - - - -
             * - - - - - B
             */

            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2, false, 1);

            Unit u = p1.Units.First();
            var possibilities = u.MovePossibilites;

            Assert.AreSame(u.Tile, g.Map.getTile(0, 0));
            Assert.IsFalse(possibilities.ContainsKey(g.Map.getTile(0, 0)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(0, 1)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(1, 0)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(1, 1)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(0, 2)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(2, 0)));
            Assert.AreEqual(5, possibilities.Count());
        }

        [Test()]
        public void MovePossibilitiesRaceTest()
        {   

            /* Map configuration (seed 1)
             * 
             * M M M M M M
             * M M M M M M
             * F F F M M M
             * P P P F M M
             * P P P F M M
             * P P P P F F
             * 
             *
             * A - - - - -
             * - - - - - -
             * - - - - - -
             * - - - - - o
             * - - - - o o
             * - - o o o B
             */

            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2, false, 1);
            p2.StartTurn();

            Unit u = p2.Units.First();
            var possibilities = u.MovePossibilites;

            Assert.AreSame(u.Tile, g.Map.getTile(5, 5));
            Assert.IsFalse(possibilities.ContainsKey(g.Map.getTile(5, 5)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(5, 4)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(5, 3)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(4, 4)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(4, 5)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(3, 5)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(2, 5)));
            Assert.AreEqual(6, possibilities.Count());
        }

        [Test()]
        public void MovePossibilitiesEnemyTest()
        {
            /*
             *  A A - - - -
             *  o B A - - -
             *  o o o - - -
             *  o o o - - -
             *  - o - - - -
             *  - - - - - B
             */

            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2, false, 1);
            p2.StartTurn();

            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(1, 0), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 1), true);
            g.Map.getTile(5, 5).Units.First().Move(g.Map.getTile(1, 1), true);

            Unit u = g.Map.getTile(1, 1).Units.First();
            var possibilities = u.MovePossibilites;

            Assert.AreEqual(2, u.MovePoints);
            Assert.AreEqual(Orc.Instance, u.Race);
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(0, 1)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(0, 2)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(1, 2)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(2, 2)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(0, 3)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(1, 3)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(2, 3)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(1, 4)));
            Assert.AreEqual(8, possibilities.Count());
        }

        [Test()]
        public void ExecuteMovePossibilitiesEnemyTest()
        {
            /*
             *  A A - - - -
             *  o B A - - -
             *  o o o - - -
             *  o o o - - -
             *  - o - - - -
             *  - - - - - B
             */

            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2, false, 1);
            p2.StartTurn();

            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(1, 0), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 1), true);
            g.Map.getTile(5, 5).Units.First().Move(g.Map.getTile(1, 1), true);

            Unit u = g.Map.getTile(1, 1).Units.First();
            var possibilities = u.MovePossibilites;

            var destination = g.Map.getTile(1, 4);

            foreach (Move m in possibilities[destination])
            {
                m.Execute();
            }

            Assert.AreEqual(0, u.MovePoints);
            Assert.AreSame(g.Map.getTile(1, 4), u.Tile);

            possibilities[destination].Reverse();

            foreach (Move m in possibilities[destination])
            {
                m.ReverseExecute();
            }

            Assert.AreEqual(2, u.MovePoints);
            Assert.AreSame(g.Map.getTile(1, 1), u.Tile);
        }

        [Test()]
        public void BattlePossibilitiesBoundsTest()
        {   
            /*
             * A - - - - -
             * - - - - - -
             * - - - - - -
             * - - - - - -
             * - - - - - -
             * - - - - - B
             */

            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            GameBuilder.New<DemoGameType>(p1, p2, false, 1);

            Unit u = p1.Units.First();
            var possibilities = u.BattlePossibilities;

            Assert.AreEqual(0, possibilities.Count);
        }

        [Test()]
        public void BattlePossibilitiesNearTest()
        {
            /*
             *  A A - - - -
             *  - B A - - -
             *  - - - - - -
             *  - - - - - -
             *  - - - - - -
             *  - - - - - B
             */

            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2, false, 1);
            p2.StartTurn();

            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(1, 0), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 1), true);
            g.Map.getTile(5, 5).Units.First().Move(g.Map.getTile(1, 1), true);

            Unit u = g.Map.getTile(1, 1).Units.First();
            var possibilities = u.BattlePossibilities;

            Assert.AreEqual(2, possibilities.Count);
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(1, 0)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(2, 1)));
        }

        [Test()]
        public void BattlePossibilitiesRangedTest()
        {
            /*
             *  A - - - - -
             *  - B - A - -
             *  - - - - - -
             *  - A - - - -
             *  - - - - - -
             *  - - - - - B
             */

            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2, false, 1);
            p2.StartTurn();

            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(1, 3), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(3, 1), true);
            g.Map.getTile(5, 5).Units.First().Move(g.Map.getTile(1, 1), true);

            Unit u = g.Map.getTile(1, 1).Units.First();
            var possibilities = u.BattlePossibilities;

            Assert.AreEqual(2, possibilities.Count);
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(3, 1)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(1, 3)));
        }

        [Test()]
        public void BattlePossibilitiesMixedTest()
        {
            /*
             *  - A - - - -
             *  - B A A - -
             *  - - - - - -
             *  - A - - - -
             *  - - - - - -
             *  - - - - - B
             */

            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2, false, 1);
            p2.StartTurn();

            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(1, 0), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 1), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(1, 3), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(3, 1), true);
            g.Map.getTile(5, 5).Units.First().Move(g.Map.getTile(1, 1), true);

            Unit u = g.Map.getTile(1, 1).Units.First();
            var possibilities = u.BattlePossibilities;

            Assert.AreEqual(4, possibilities.Count);
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(1, 0)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(2, 1)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(3, 1)));
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(1, 3)));
        }

        [Test()]
        public void BattlePossibilitiesMovementPointsTest()
        {
            /*
             *  - - - - - -
             *  - - A - - -
             *  - - A - - -
             *  - - B - - -
             *  - - A - - -
             *  - - A - - B
             */

            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2, false, 1);
            p2.StartTurn();

            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 1), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 2), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 4), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 5), true);
            g.Map.getTile(5, 5).Units.First().Move(g.Map.getTile(2, 3), true);

            Unit u = g.Map.getTile(2, 3).Units.First();
            u.MovePoints = 0.5;
            var possibilities = u.BattlePossibilities;

            Assert.AreEqual(1, possibilities.Count);
            Assert.IsTrue(possibilities.ContainsKey(g.Map.getTile(2, 4)));
        }

        [Test()]
        public void ExecuteBattlePossibilitiesTest()
        {
            /*
             *  A - - - - -
             *  - - - - - -
             *  - - A - - -
             *  - - B - - -
             *  - - A - - -
             *  - - - - - B
             */

            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2, false, 1);
            p2.StartTurn();

            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 2), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 4), true);
            g.Map.getTile(5, 5).Units.First().Move(g.Map.getTile(2, 3), true);

            Unit u = g.Map.getTile(2, 3).Units.First();
            u.MovePoints = 1;
            var possibilities = u.BattlePossibilities;

            possibilities[g.Map.getTile(2, 4)].Execute();
            Assert.AreEqual(0.5, u.MovePoints);
            possibilities[g.Map.getTile(2, 4)].ReverseExecute();
            Assert.AreEqual(1, u.MovePoints);
            possibilities[g.Map.getTile(2, 2)].Execute();
            Assert.AreEqual(0, u.MovePoints);
            possibilities[g.Map.getTile(2, 2)].ReverseExecute();
            Assert.AreEqual(1, u.MovePoints);
            Assert.AreEqual(Orc.Instance.Life, u.Life);
        }

        [Test()]
        public void BattlePossibilitiesDeadTest()
        {
            /*
             *  A - - - - -
             *  - - - - - -
             *  - - A - - -
             *  - - B - - -
             *  - - A - - -
             *  - - - - - B
             */

            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2, false, 1);
            p2.StartTurn();

            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 2), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 4), true);
            g.Map.getTile(5, 5).Units.First().Move(g.Map.getTile(2, 3), true);

            Unit u = g.Map.getTile(2, 3).Units.First();
            u.MovePoints = 1;
            u.Life = 0;
            var possibilities = u.BattlePossibilities;

            Assert.AreEqual(0, possibilities.Count);
        }

        [Test()]
        public void BattlePossibilitiesEnemyDeadTest()
        {
            /*
             *  A - - - - -
             *  - - - - - -
             *  - - A - - -
             *  - - B - - -
             *  - - A - - -
             *  - - - - - B
             */

            var p1 = new Player("A", Human.Instance);
            var p2 = new Player("B", Orc.Instance);

            Game g = GameBuilder.New<DemoGameType>(p1, p2, false, 1);
            p2.StartTurn();

            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 2), true);
            g.Map.getTile(0, 0).Units.First().Move(g.Map.getTile(2, 4), true);
            g.Map.getTile(5, 5).Units.First().Move(g.Map.getTile(2, 3), true);

            g.Map.getTile(2, 2).Units.First().Life = 0;
            g.Map.getTile(2, 4).Units.First().Life = 0;

            Unit u = g.Map.getTile(2, 3).Units.First();
            var possibilities = u.BattlePossibilities;

            Assert.AreEqual(0, possibilities.Count);
        }
    }
}

