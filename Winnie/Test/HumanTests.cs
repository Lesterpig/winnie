using NUnit.Framework;
using System;
using Core;

namespace Test
{
	[TestFixture ()]
	public class HumanTests
	{
		[Test()]
		public void CanMoveTest()
		{
			Assert.IsTrue(Human.Instance.CanMove(new WaterTileType()));
			Assert.IsTrue(Human.Instance.CanMove(new PlainTileType()));
			Assert.IsTrue(Human.Instance.CanMove(new ForestTileType()));
			Assert.IsTrue(Human.Instance.CanMove(new MountainTileType()));
		}

		[Test()]
		public void GetRequiredMovePointsTest()
		{
			Assert.AreEqual(1, Human.Instance.GetRequiredMovePoints(new WaterTileType()));
            Assert.AreEqual(1, Human.Instance.GetRequiredMovePoints(new PlainTileType()));
			Assert.AreEqual(1, Human.Instance.GetRequiredMovePoints(new ForestTileType()));
			Assert.AreEqual(1, Human.Instance.GetRequiredMovePoints(new MountainTileType()));
		}

		[Test()]
		public void GetVictoryPointsTest()
		{
			Assert.AreEqual(0, Human.Instance.GetVictoryPoints(new WaterTileType()));
			Assert.AreEqual(2, Human.Instance.GetVictoryPoints(new PlainTileType()));
			Assert.AreEqual(1, Human.Instance.GetVictoryPoints(new ForestTileType()));
			Assert.AreEqual(1, Human.Instance.GetVictoryPoints(new MountainTileType()));
		}

        [Test()]
        public void CanDoRangedAttackTest()
        {
            Assert.IsFalse(Human.Instance.CanDoRangedAttack(new WaterTileType()));
            Assert.IsFalse(Human.Instance.CanDoRangedAttack(new PlainTileType()));
            Assert.IsFalse(Human.Instance.CanDoRangedAttack(new ForestTileType()));
            Assert.IsFalse(Human.Instance.CanDoRangedAttack(new MountainTileType()));
        }
	}
}

