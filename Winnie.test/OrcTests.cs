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
    public class OrcTests
    {
        [TestMethod()]
        public void CanMoveTest()
        {
            Assert.IsFalse(Orc.Instance.CanMove(new WaterTileType()));
            Assert.IsTrue(Orc.Instance.CanMove(new PlainTileType()));
            Assert.IsTrue(Orc.Instance.CanMove(new ForestTileType()));
            Assert.IsTrue(Orc.Instance.CanMove(new MountainTileType()));
        }

        [TestMethod()]
        public void GetRequiredMovePointsTest()
        {
            Assert.AreEqual(Orc.Instance.GetRequiredMovePoints(new WaterTileType()), 1);
            Assert.AreEqual(Orc.Instance.GetRequiredMovePoints(new PlainTileType()), 0.5);
            Assert.AreEqual(Orc.Instance.GetRequiredMovePoints(new ForestTileType()), 1);
            Assert.AreEqual(Orc.Instance.GetRequiredMovePoints(new MountainTileType()), 1);
        }

        [TestMethod()]
        public void GetVictoryPointsTest()
        {
            Assert.AreEqual(Orc.Instance.GetVictoryPoints(new WaterTileType()), 0);
            Assert.AreEqual(Orc.Instance.GetVictoryPoints(new PlainTileType()), 1);
            Assert.AreEqual(Orc.Instance.GetVictoryPoints(new ForestTileType()), 1);
            Assert.AreEqual(Orc.Instance.GetVictoryPoints(new MountainTileType()), 2);
        }
    }
}