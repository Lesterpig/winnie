using NUnit.Framework;
using System;
using Core;

namespace Test
{
	[TestFixture()]
	public class TileTypeFactoryTests
	{
		[Test()]
		public void GetSimpleTest()
		{
			TileType plainA = TileTypeFactory.Get("Plain");
			TileType plainB = TileTypeFactory.Get("Plain");

			Assert.IsNotNull(plainA);
			Assert.IsNotNull(plainB);
			Assert.AreEqual(plainA.Height, 1);
			Assert.AreEqual(plainA.Type, "Plain");
			Assert.AreSame(plainA, plainB); // Flyweight test here
		}

		[Test()]
		public void GetWrongTest()
		{
			TileType wrong = TileTypeFactory.Get("Wrong");
			Assert.IsNull(wrong);
		}

		[Test()]
		public void GetFullTest()
		{
			TileType water = TileTypeFactory.Get("Water");
			TileType forest = TileTypeFactory.Get("Forest");
			TileType mountain = TileTypeFactory.Get("Mountain");

			Assert.AreEqual(water.Height, 0);
			Assert.AreEqual(forest.Height, 2);
			Assert.AreEqual(mountain.Height, 3);

			Assert.AreEqual(water.Type, "Water");
			Assert.AreEqual(forest.Type, "Forest");
			Assert.AreEqual(mountain.Type, "Mountain");
		}
	}
}

