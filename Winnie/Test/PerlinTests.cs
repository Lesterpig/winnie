using NUnit.Framework;
using System;
using Core;

namespace Test
{
	[TestFixture ()]
	public class PerlinTests
	{
		const int seed = 1337;
		const int sizeX = 6;
		const int sizeY = 5;
		const int min = 0;
		const int max = 100;
		const int step = 1;
		const int octaves = 3;
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

