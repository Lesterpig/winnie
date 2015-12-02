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
            TileType plainA = TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN);
            TileType plainB = TileTypeFactory.Get(TileTypeFactory.Identifier.PLAIN);

			Assert.IsNotNull(plainA);
			Assert.IsNotNull(plainB);
			Assert.AreEqual(1, plainA.Height);
			Assert.AreEqual("Plain", plainA.Type);
			Assert.AreSame(plainA, plainB); // Flyweight test here
		}

		[Test()]
		public void GetWrongTest()
		{
            TileType wrong = TileTypeFactory.Get((TileTypeFactory.Identifier) 5);
			Assert.IsNull(wrong);
		}

		[Test()]
		public void GetFullTest()
		{
            TileType water = TileTypeFactory.Get(TileTypeFactory.Identifier.WATER);
            TileType forest = TileTypeFactory.Get(TileTypeFactory.Identifier.FOREST);
            TileType mountain = TileTypeFactory.Get(TileTypeFactory.Identifier.MOUNTAIN);

            Assert.AreEqual(0, water.Height);
			Assert.AreEqual(2, forest.Height);
			Assert.AreEqual(3, mountain.Height);

			Assert.AreEqual("Water", water.Type);
			Assert.AreEqual("Forest", forest.Type);
            Assert.AreEqual("Mountain", mountain.Type);
		}
	}
}

