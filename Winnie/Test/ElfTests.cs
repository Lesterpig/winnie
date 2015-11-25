using NUnit.Framework;
using System;
using Core;

namespace Test
{
	[TestFixture ()]
	public class ElfTests
	{
		[Test()]
		public void CanMoveTest()
		{
			Assert.IsFalse(Elf.Instance.CanMove(new WaterTileType()));
			Assert.IsTrue(Elf.Instance.CanMove(new PlainTileType()));
			Assert.IsTrue(Elf.Instance.CanMove(new ForestTileType()));
			Assert.IsTrue(Elf.Instance.CanMove(new MountainTileType()));
		}

		[Test()]
		public void GetRequiredMovePointsTest()
		{
			Assert.AreEqual(1, Elf.Instance.GetRequiredMovePoints(new WaterTileType()));
			Assert.AreEqual(1, Elf.Instance.GetRequiredMovePoints(new PlainTileType()));
			Assert.AreEqual(1, Elf.Instance.GetRequiredMovePoints(new ForestTileType()));
			Assert.AreEqual(2, Elf.Instance.GetRequiredMovePoints(new MountainTileType()));
		}

		[Test()]
		public void GetVictoryPointsTest()
		{
			Assert.AreEqual(0, Elf.Instance.GetVictoryPoints(new WaterTileType()));
            Assert.AreEqual(1, Elf.Instance.GetVictoryPoints(new PlainTileType()));
			Assert.AreEqual(3, Elf.Instance.GetVictoryPoints(new ForestTileType()));
			Assert.AreEqual(0, Elf.Instance.GetVictoryPoints(new MountainTileType()));
		}
	}
}

