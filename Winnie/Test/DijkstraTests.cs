using NUnit.Framework;
using System;
using System.Collections.Generic;
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

		[Test()]
		public void GetDistanceWallTest()
		{
			double[] data = {
				0.5, 0.5, 0.5,
				9.0, 9.0, -1,
				0.5, 0.5, 0.5,
			};
			Dijkstra d = new Dijkstra (data, 3, 3, new Point (2, 0));
			Assert.AreEqual (10.5, d.getDistance (new Point (2, 2)));
		}

		[Test()]
		public void GetPathTest()
		{
			double[] data = {
				0.5, 0.5, 0.5,
				9.0, 9.0, 0.5,
				0.5, 0.5, 0.5,
			};
			Dijkstra d = new Dijkstra (data, 3, 3, new Point (0, 0));
			List<Point> path = d.getPath (new Point (0, 2));
			Assert.AreEqual (7, path.Count);

			//pt1 = 1,2
			Assert.AreEqual (1, path[1].x);
			Assert.AreEqual (2, path[1].y);

			//pt2 = 2,2
			Assert.AreEqual (2, path[2].x);
			Assert.AreEqual (2, path[2].y);

			//pt3 = 2,1
			Assert.AreEqual (2, path[3].x);
			Assert.AreEqual (1, path[3].y);

			//pt4 = 2,0
			Assert.AreEqual (2, path[4].x);
			Assert.AreEqual (0, path[4].y);

			//pt5 = 1,0
			Assert.AreEqual (1, path[5].x);
			Assert.AreEqual (0, path[5].y);

			//pt6 = 0,0
			Assert.AreEqual (0, path[6].x);
			Assert.AreEqual (0, path[6].y);
		}
	}
}

