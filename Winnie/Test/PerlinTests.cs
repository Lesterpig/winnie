using NUnit.Framework;
using System;
using Core;

namespace Test
{
	[TestFixture ()]
	public class PerlinTests
	{

		[Test()]
		public void RandomNoiseTest()
		{
            Perlin p = new Perlin(0,6,5,1,3,0.25);
			Assert.GreaterOrEqual (p.coherentNoise2D(1,1),0);
		}
	}
}

