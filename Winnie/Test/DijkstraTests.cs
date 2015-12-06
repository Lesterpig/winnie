using NUnit.Framework;
using System;
using Core;

namespace Test
{
	[TestFixture ()]
	public class DijkstraTests
	{

		[Test()]
		public void GetDistanceTest()
		{
			double[] data = {
				0.5, 0.5, 0.5,
				9.0, 9.0, 0.5,
				0.5, 0.5, 0.5,
			};
			Dijkstra d = new Dijkstra (data, 3, 3, new Point (0, 0));
			Assert.AreEqual (3.0, d.getDistance (new Point (0, 2)));
		}
	}
}

