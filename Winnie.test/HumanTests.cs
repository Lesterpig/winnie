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
    public class HumanTests
    {
        [TestMethod()]
        public void CanMoveTest()
        {
            Assert.IsTrue(Human.Instance.CanMove(new WaterTileType()));
            Assert.IsTrue(Human.Instance.CanMove(new PlainTileType()));
            Assert.IsTrue(Human.Instance.CanMove(new ForestTileType()));
            Assert.IsTrue(Human.Instance.CanMove(new MountainTileType()));
        }

        [TestMethod()]
        public void GetRequiredMovePointsTest()
        {
            Assert.AreEqual(Human.Instance.GetRequiredMovePoints(new WaterTileType()), 1);
            Assert.AreEqual(Human.Instance.GetRequiredMovePoints(new PlainTileType()), 1);
            Assert.AreEqual(Human.Instance.GetRequiredMovePoints(new ForestTileType()), 1);
            Assert.AreEqual(Human.Instance.GetRequiredMovePoints(new MountainTileType()), 1);
        }

        [TestMethod()]
        public void GetVictoryPointsTest()
        {
            Assert.AreEqual(Human.Instance.GetVictoryPoints(new WaterTileType()), 0);
            Assert.AreEqual(Human.Instance.GetVictoryPoints(new PlainTileType()), 2);
            Assert.AreEqual(Human.Instance.GetVictoryPoints(new ForestTileType()), 1);
            Assert.AreEqual(Human.Instance.GetVictoryPoints(new MountainTileType()), 1);
        }
    }
}