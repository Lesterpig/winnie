using NUnit.Framework;
using System;
using Core;

namespace Test
{
	[TestFixture ()]
	public class PerlinTests
	{
		const int seed = 12;
		const int sizeX = 6;
		const int sizeY = 5;
		const double min = 0.0;
		const double max = 5.0;
		const int step = 4;
		const int octaves = 5;
		const double persistence = 0.25;

		[Test()]
		public void RandomNoiseTest()
		{
			Perlin p = new Perlin(seed,sizeX,sizeY,min,max,step,octaves,persistence);
			for (int i = 0; i < sizeX; i++) {
				for (int j = 0; j < sizeY; j++) {
					double v = p.coherentNoise2D (i, j);
					Assert.GreaterOrEqual (v, min);
					Assert.Less (v,max);
					//Console.WriteLine (v);
				}
			}
		}
	}
}

