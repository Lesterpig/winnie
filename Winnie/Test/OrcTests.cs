using NUnit.Framework;
using System;
using Core;

namespace Test
{
	[TestFixture ()]
	public class OrcTests
	{
		[Test()]
		public void CanMoveTest()
		{
			Assert.IsFalse(Orc.Instance.CanMove(new WaterTileType()));
			Assert.IsTrue(Orc.Instance.CanMove(new PlainTileType()));
			Assert.IsTrue(Orc.Instance.CanMove(new ForestTileType()));
			Assert.IsTrue(Orc.Instance.CanMove(new MountainTileType()));
		}

		[Test()]
		public void GetRequiredMovePointsTest()
		{
			Assert.AreEqual(1, Orc.Instance.GetRequiredMovePoints(new WaterTileType()), 1);
			Assert.AreEqual(0.5, Orc.Instance.GetRequiredMovePoints(new PlainTileType()), 0.5);
			Assert.AreEqual(1, Orc.Instance.GetRequiredMovePoints(new ForestTileType()), 1);
			Assert.AreEqual(1, Orc.Instance.GetRequiredMovePoints(new MountainTileType()), 1);
		}

		[Test()]
		public void GetVictoryPointsTest()
		{
			Assert.AreEqual(0, Orc.Instance.GetVictoryPoints(new WaterTileType()));
			Assert.AreEqual(1, Orc.Instance.GetVictoryPoints(new PlainTileType()));
			Assert.AreEqual(1, Orc.Instance.GetVictoryPoints(new ForestTileType()));
			Assert.AreEqual(2, Orc.Instance.GetVictoryPoints(new MountainTileType()));
		}
	}
}

