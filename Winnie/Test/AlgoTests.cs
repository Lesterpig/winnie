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
            TileTypeFactory.Identifier[] identifiers = a.CreateMap(5);
            Assert.AreEqual(5, identifiers.Length);
            foreach (TileTypeFactory.Identifier id in identifiers)
            {
                Assert.IsInstanceOf<TileTypeFactory.Identifier>(id);
            }
		}
	}
}

