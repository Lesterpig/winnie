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
            TileTypeFactory.Identifier[] identifiers = a.CreateMap(0, 5, 6);

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

		[Test()]
		public void findBestStartPositionTest() {
			Algo a = new Algo();
			Player p1 = new Player ("Agathe", new Elf());
			Player p2 = new Player ("Bob", new Orc());

			TileTypeFactory.Identifier w = TileTypeFactory.Identifier.WATER;
			TileTypeFactory.Identifier p = TileTypeFactory.Identifier.PLAIN;

			TileTypeFactory.Identifier[] tiles = new TileTypeFactory.Identifier[]
			{ 
				w,w,w,
				p,p,p,
				w,w,p
			};

			Map m = new Map(tiles, 3, 3);

			a.FindBestStartPosition (p1, p2, m);

			// Check if pos1 == (0,1)
			Assert.AreEqual (0, p1.InitialPosition.x);
			Assert.AreEqual (1, p1.InitialPosition.y);

			// Check if pos2 == (2,2)
			Assert.AreEqual (2, p2.InitialPosition.x);
			Assert.AreEqual (2, p2.InitialPosition.y);
		}
	}
}

