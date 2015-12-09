using NUnit.Framework;
using System;
using Core;
using System.Collections.Generic;

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

            //Console.WriteLine(GeneratedMap);
		}

		[Test()]
		public void findBestStartPositionTest() 
		{
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

		[Test()]
		public void findBestActionsTest() 
		{
			Algo a = new Algo();
			Player p1 = new Player ("Agathe", new Elf());
			Player p2 = new Player ("Bob", new Orc());

			TileTypeFactory.Identifier w = TileTypeFactory.Identifier.WATER;
			TileTypeFactory.Identifier p = TileTypeFactory.Identifier.PLAIN;
			TileTypeFactory.Identifier f = TileTypeFactory.Identifier.FOREST;


			TileTypeFactory.Identifier[] tiles = new TileTypeFactory.Identifier[]
			{ 
				w,w,w,f,
				f,p,w,p,
				p,p,w,w,
				p,p,p,f
			};

			Map m = new Map(tiles, 4, 4);

			p1.AddUnit (new Unit (p1, m.getTile (3, 1)));
			p1.AddUnit (new Unit (p1, m.getTile (0, 3)));
			p1.AddUnit (new Unit (p1, m.getTile (1, 3)));

			p2.AddUnit (new Unit (p2, m.getTile (1, 1)));

			List<Proposition> props = a.FindBestActions (p1,p2,m);

			// Goal: x=3, y=0
			Assert.AreEqual (2, props [0].bonus);
			Assert.AreEqual (3, props [0].start.x);
			Assert.AreEqual (1, props [0].start.y);
			Assert.AreEqual (3, props[0].goal.x);
			Assert.AreEqual (0, props[0].goal.y);

			// Goal: x=
			Assert.AreEqual (2, props[2].bonus);
			Assert.AreEqual (1, props [2].start.x);
			Assert.AreEqual (3, props [2].start.y);
			Assert.AreEqual (3, props[2].goal.x);
			Assert.AreEqual (3, props[2].goal.y);

			Assert.AreEqual (2, props[1].bonus);
			Assert.AreEqual (0, props [1].start.x);
			Assert.AreEqual (3, props [1].start.y);
			Assert.AreEqual (0, props[1].goal.x);
			Assert.AreEqual (1, props[1].goal.y);



		}

		// @TODO Test when there are two units on the same case
		// @TODO Test that goal is not already proposed

	}
}

