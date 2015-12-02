using NUnit.Framework;
using System;
using Core;

namespace Test
{
    [TestFixture()]
    public class CustomRandomTest
    {
        [Test()]
        public void NormalTest()
        {
            CustomRandom r = new CustomRandom(CustomRandom.Mode.NORMAL);

            // Simple tests to avoid mistakes
            bool[] found = new bool[5];

            for (int i = 0; i < 1000; i++)
            {
                int a = r.Next(0, 5);
                Assert.IsTrue(a >= 0 && a < 5);
                found[a] = true;
            }

            foreach (bool f in found)
            {
                Assert.IsTrue(f);
            }
        }

        [Test()]
        public void HighTest()
        {
            CustomRandom r = new CustomRandom(CustomRandom.Mode.HIGH);

            for (int i = 0; i < 1000; i++)
            {
                Assert.AreEqual(4, r.Next(0, 5));
            }
        }

        [Test()]
        public void LowTest()
        {
            CustomRandom r = new CustomRandom(CustomRandom.Mode.LOW);

            for (int i = 0; i < 1000; i++)
            {
                Assert.AreEqual(1, r.Next(0, 5));
            }
        }
            
    }
}

