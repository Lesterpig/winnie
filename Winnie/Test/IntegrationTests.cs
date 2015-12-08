using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Linq;
using Core;

namespace Test
{   
    /// <summary>
    /// Integration tests for the project.
    /// This simulates a real game from scratch: the GUI have to follow this behavior.
    /// </summary>
    [TestFixture()]
    public class IntegrationTests
    {   
        private Game game;

        [Test()]
        public void DemoGameIntegrationTest()
        {
            // Create players
            var p1 = new Player("Player A", Human.Instance);
            var p2 = new Player("Player B", Elf.Instance);

            // Create the demo game with a naive map; illustrated as follow:
            //
            //   0 1 2 3 4 5 x
            // 0 W P F M W P
            // 1 F M W P F M
            // 2 W P F M W P
            // 3 F M W P F M
            // 4 W P F M W P
            // 5 F M W P F M
            // y
            //
            // Cheatmode is enabled.
            // P1 units are on (1,0) and P2 are on (3,0)
            game = GameBuilder.New<DemoGameType, NaiveMap>(p1, p2, true);

            ///////////////////// TURN 1
            ////////////// PLAYER 1
            //   0 1 2 3 4 5 x
            // 0 - A - B - -
            // 1 - - - - - -
            // 2 - - - - - -
            // 3 - - - - - -
            // 4 - - - - - -
            // 5 - - - - - -
            // y
            Assert.AreEqual(0, game.CurrentPlayerIndex);
            Assert.AreEqual(0, game.CurrentTurn);
            Assert.AreEqual(2 * 4, p1.Score);
            Assert.AreEqual(0 * 4, p2.Score);
            Assert.IsNull(game.Winner);

            // P1 will move an unit to (0,0)
            Unit a1 = game.Map.getTile(1, 0).Units.First();
            executeMovement(a1.MovePossibilites[game.Map.getTile(0, 0)]);
            Assert.AreEqual(1, a1.MovePoints);
            Assert.AreSame(game.Map.getTile(0, 0), a1.Tile);

            // P1 will move an unit to (2,0)
            Unit a2 = game.Map.getTile(1, 0).Units.First();
            executeMovement(a2.MovePossibilites[game.Map.getTile(2, 0)]);
            Assert.AreEqual(1, a2.MovePoints);
            Assert.AreSame(game.Map.getTile(2, 0), a2.Tile);

            // P1 will move an unit to (1,2)
            Unit a3 = game.Map.getTile(1, 0).Units.First();
            executeMovement(a3.MovePossibilites[game.Map.getTile(1, 2)]);
            Assert.AreEqual(0, a3.MovePoints);
            Assert.AreSame(game.Map.getTile(1, 2), a3.Tile);

            Unit a4 = game.Map.getTile(1, 0).Units.First();

            game.NextTurn();

            ////////////// PLAYER 2
            //   0  1  2  3  4  5
            // 0 A1 A4 A2 B  -  -
            // 1 -  -  -  -  -  -
            // 2 -  A3 -  -  -  -
            // 
            Assert.AreEqual(1, game.CurrentPlayerIndex);
            Assert.AreEqual(0, game.CurrentTurn);
            Assert.AreEqual(0 + 1 + 2 + 2, p1.Score);
            Assert.AreEqual(0 + 0 + 0 + 0, p2.Score);
            Assert.IsNull(game.Winner);

            // P2 will move two units to (4,1)
            Unit b1 = game.Map.getTile(3, 0).Units.First();
            executeMovement(b1.MovePossibilites[game.Map.getTile(4, 1)]);
            Assert.AreEqual(0, b1.MovePoints);
            Assert.AreSame(game.Map.getTile(4, 1), b1.Tile);

            Unit b2 = game.Map.getTile(3, 0).Units.First();
            executeMovement(b2.MovePossibilites[game.Map.getTile(4, 1)]);
            Assert.AreEqual(0, b2.MovePoints);
            Assert.AreSame(game.Map.getTile(4, 1), b2.Tile);

            // P2 will move an unit to (3,1)
            Unit b3 = game.Map.getTile(3, 0).Units.First();
            executeMovement(b3.MovePossibilites[game.Map.getTile(3, 1)]);
            Assert.AreEqual(1, b3.MovePoints);
            Assert.AreSame(game.Map.getTile(3, 1), b3.Tile);

            // P2 will undo this operation
            game.Actions.Pop().ReverseExecute();
            Assert.AreEqual(2, b3.MovePoints);
            Assert.AreSame(game.Map.getTile(3, 0), b3.Tile);

            // P2 will use b3 to attack on (2,0) two times (defeat)
            configureRandomHigh();
            b3.BattlePossibilities[game.Map.getTile(2, 0)].Execute();
            b3.BattlePossibilities[game.Map.getTile(2, 0)].Execute();
            Assert.AreEqual(0, b3.MovePoints);
            Assert.AreEqual(2, b3.Life);
            Assert.AreEqual(15, a2.Life);

            // P2 will use b4 to attack on (1, 0) two times
            configureRandomHigh();
            Unit b4 = game.Map.getTile(3, 0).Units.First(u => u.Life > 5);
            b4.BattlePossibilities[game.Map.getTile(1, 0)].Execute();
            b4.BattlePossibilities[game.Map.getTile(1, 0)].Execute();
            Assert.AreEqual(0, b3.MovePoints);
            Assert.AreEqual(12, b4.Life);
            Assert.AreEqual(5, a4.Life);

            game.NextTurn();

            ///////////////////// TURN 2
            ////////////// PLAYER 1
            //   0  1  2  3   4   5
            // 0 A1 A4 A2 B34 -   -
            // 1 -  -  -  -   B12 -
            // 2 -  A3 -  -   -   -
            // 
            Assert.AreEqual(0, game.CurrentPlayerIndex);
            Assert.AreEqual(1, game.CurrentTurn);
            Assert.AreEqual(0 + 1 + 2 + 2, p1.Score);
            Assert.AreEqual(3 + 3 + 0 + 0, p2.Score);
            Assert.IsNull(game.Winner);

            // P1 will use a2 to attack on (3,0) two times (victory)
            configureRandomLow();
            a2.BattlePossibilities[game.Map.getTile(3, 0)].Execute();
            a2.BattlePossibilities[game.Map.getTile(3, 0)].Execute();
            Assert.AreEqual(0, a2.MovePoints);
            Assert.AreEqual(Human.Instance.Life, a2.Life);
            Assert.AreEqual(2, b3.Life);
            Assert.AreEqual(8, b4.Life);

            // P1 will move a1 to help a4 in (1,0)
            executeMovement(a1.MovePossibilites[game.Map.getTile(1, 0)]);
            Assert.AreEqual(a1, game.Map.getTile(1, 0).StrongestUnit);

            game.NextTurn();

            ////////////// PLAYER 2
            //   0 1   2  3   4   5
            // 0 - A14 A2 B34 -   -
            // 1 - -   -  -   B12 -
            // 2 - A3  -  -   -   -
            //
            Assert.AreEqual(1, game.CurrentPlayerIndex);
            Assert.AreEqual(1, game.CurrentTurn);
            Assert.AreEqual(2 + 1 + 2 + 2, p1.Score);
            Assert.AreEqual(3 + 3 + 0 + 0, p2.Score);
            Assert.IsNull(game.Winner);

            // P2 will use b3 to attack on (1,0) two times
            configureRandomHigh();
            b3.BattlePossibilities[game.Map.getTile(1, 0)].Execute();
            b3.BattlePossibilities[game.Map.getTile(1, 0)].Execute();
            Assert.AreEqual(5, a1.Life);
            Assert.AreEqual(5, a4.Life);

            // P2 will use b4 to kill a1 with a ranged attack
            configureRandomHigh();
            a4.Life--; // just to have a lower life than a1, so a1 will be the target
            b4.BattlePossibilities[game.Map.getTile(1, 0)].Execute();
            Assert.IsFalse(a1.Alive);

            game.NextTurn();

            ///////////////////// TURN 3
            ////////////// PLAYER 1
            //   0 1  2  3   4   5
            // 0 - A4 A2 B34 -   -
            // 1 - -  -  -   B12 -
            // 2 - A3 -  -   -   -
            //
            Assert.AreEqual(0, game.CurrentPlayerIndex);
            Assert.AreEqual(2, game.CurrentTurn);
            Assert.AreEqual(0 + 1 + 2 + 2, p1.Score);
            Assert.AreEqual(3 + 3 + 0 + 0, p2.Score);
            Assert.IsNull(game.Winner);

            // P1 will move a2 to (2,1)
            executeMovement(a2.MovePossibilites[game.Map.getTile(2, 1)]);

            game.NextTurn();

            ////////////// PLAYER 2
            //   0 1  2  3   4   5
            // 0 - A4 -  B34 -   -
            // 1 - -  A2 -   B12 -
            // 2 - A3 -  -   -   -
            //
            Assert.AreEqual(1, game.CurrentPlayerIndex);
            Assert.AreEqual(2, game.CurrentTurn);
            Assert.AreEqual(0 + 0 + 2 + 2, p1.Score);
            Assert.AreEqual(3 + 3 + 0 + 0, p2.Score);
            Assert.IsNull(game.Winner);

            // P2 will move b3 and b4 to (2,0)
            executeMovement(b3.MovePossibilites[game.Map.getTile(2, 0)]);
            executeMovement(b4.MovePossibilites[game.Map.getTile(2, 0)]);

            // P2 will use b3 to harass a4
            configureRandomLow();
            b3.BattlePossibilities[game.Map.getTile(1, 0)].Execute();
            Assert.AreEqual(2, a4.Life);

            // P2 will use b3 to kill a4 and move freely to its tile
            b4.BattlePossibilities[game.Map.getTile(1, 0)].Execute();
            Assert.IsFalse(a4.Alive);
            Assert.AreSame(game.Map.getTile(1, 0), b4.Tile);
            Assert.AreEqual(0, b4.MovePoints);

            game.NextTurn();

            ///////////////////// TURN 4
            ////////////// PLAYER 1
            //   0 1  2  3 4   5
            // 0 - B4 B3 - -   -
            // 1 - -  A2 - B12 -
            // 2 - A3 -  - -   -
            //
            Assert.AreEqual(0, game.CurrentPlayerIndex);
            Assert.AreEqual(3, game.CurrentTurn);
            Assert.AreEqual(0 + 1 + 1 + 0, p1.Score);
            Assert.AreEqual(3 + 3 + 3 + 1, p2.Score);
            Assert.IsNull(game.Winner);

            // End of game simulation

            try
            {
                game.NextTurn();
            }
            catch (Game.EndOfGameException)
            {
                Assert.AreSame(p2, game.Winner);
            }
        }

        /// <summary>
        /// Simple way to execute a list of movements.
        /// Should be implemented in the GUI side.
        /// </summary>
        /// <param name="l">The list of movements</param>
        private void executeMovement(List<Move> l)
        {
            foreach (Move m in l)
            {
                m.Execute();
                game.Actions.Push(m); // The stack will have to be managed by the GUI
            }
        }

        private void configureRandomLow() // victory
        {
            Game.Random = new CustomRandom(CustomRandom.Mode.LOW);
        }

        private void configureRandomHigh() // defeat
        {
            Game.Random = new CustomRandom(CustomRandom.Mode.HIGH);
        }
    }
}

