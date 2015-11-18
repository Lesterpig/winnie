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
    public class GameTests
    {
        [TestMethod()]
        public void NextTurnTest()
        {
            Game g = new Game();
            g.NextTurn();
        }

        [TestMethod()]
        public void NewActionTest()
        {
            Game g = new Game();
        }
    }
}