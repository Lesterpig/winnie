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
			Assert.AreEqual(Human.Instance.GetRequiredMovePoints(new WaterTileType()), 1);
			Assert.AreEqual(Human.Instance.GetRequiredMovePoints(new PlainTileType()), 1);
			Assert.AreEqual(Human.Instance.GetRequiredMovePoints(new ForestTileType()), 1);
			Assert.AreEqual(Human.Instance.GetRequiredMovePoints(new MountainTileType()), 1);
		}

		[Test()]
		public void GetVictoryPointsTest()
		{
			Assert.AreEqual(Human.Instance.GetVictoryPoints(new WaterTileType()), 0);
			Assert.AreEqual(Human.Instance.GetVictoryPoints(new PlainTileType()), 2);
			Assert.AreEqual(Human.Instance.GetVictoryPoints(new ForestTileType()), 1);
			Assert.AreEqual(Human.Instance.GetVictoryPoints(new MountainTileType()), 1);
		}
	}
}

