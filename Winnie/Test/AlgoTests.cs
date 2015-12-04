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
            TileTypeFactory.Identifier[] identifiers = a.CreateMap(5,6);

            Assert.AreEqual(30, identifiers.Length);

            string GeneratedMap = "map: ";
            foreach (TileTypeFactory.Identifier id in identifiers)
            {
                GeneratedMap += id.ToString() + ", ";
                Assert.IsTrue(
                    id == TileTypeFactory.Identifier.WATER ||
                    id == TileTypeFactory.Identifier.PLAIN ||
                    id == TileTypeFactory.Identifier.FOREST ||
                    id == TileTypeFactory.Identifier.MOUNTAIN
                );
            }

            Console.WriteLine(GeneratedMap);
		}
	}
}

