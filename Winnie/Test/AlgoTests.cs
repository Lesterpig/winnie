using NUnit.Framework;
using System;
using Core;

namespace Test
{
	[TestFixture ()]
	public class AlgoTests
	{

		[Test()]
		public void CreateMapTest()
		{
            Algo a = new Algo();
            Map m = a.CreateMap(5);
            Assert.AreEqual(5, m.Tiles.Length);
            foreach (TileTypeFactory.Identifier ti in m.Tiles)
            {
                Assert.IsInstanceOf<TileTypeFactory.Identifier>(ti);
            }
		}
	}
}

