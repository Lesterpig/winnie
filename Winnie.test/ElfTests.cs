using Microsoft.VisualStudio.TestTools.UnitTesting;
using winnie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winnie.Tests
{
    [TestClass()]
    public class ElfTests
    {
        [TestMethod()]
        public void CanMoveTest()
        {
            Assert.IsFalse(Elf.Instance.CanMove(new WaterTileType()));
            Assert.IsTrue(Elf.Instance.CanMove(new PlainTileType()));
            Assert.IsTrue(Elf.Instance.CanMove(new ForestTileType()));
            Assert.IsTrue(Elf.Instance.CanMove(new MountainTileType()));
        }

        [TestMethod()]
        public void GetRequiredMovePointsTest()
        {
            Assert.AreEqual(Elf.Instance.GetRequiredMovePoints(new WaterTileType()), 1);
            Assert.AreEqual(Elf.Instance.GetRequiredMovePoints(new PlainTileType()), 1);
            Assert.AreEqual(Elf.Instance.GetRequiredMovePoints(new ForestTileType()), 1);
            Assert.AreEqual(Elf.Instance.GetRequiredMovePoints(new MountainTileType()), 2);
        }

        [TestMethod()]
        public void GetVictoryPointsTest()
        {
            Assert.AreEqual(Elf.Instance.GetVictoryPoints(new WaterTileType()), 0);
            Assert.AreEqual(Elf.Instance.GetVictoryPoints(new PlainTileType()), 1);
            Assert.AreEqual(Elf.Instance.GetVictoryPoints(new ForestTileType()), 3);
            Assert.AreEqual(Elf.Instance.GetVictoryPoints(new MountainTileType()), 0);
        }
    }
}