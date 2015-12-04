#include <iostream>
#include <algorithm>
#include <time.h>
#include <math.h> 
#include "Algo.h"

using namespace std;

Algo::Algo() 
{
}

Algo::~Algo()
{
}

void Algo::fillMap(TileType map[], int size_x, int size_y)
{
	Map m = Map(size_x, size_y, map);
	Perlin p = Perlin(1, size_x, size_y, 0, 4, 1, 3, 0.25);

	//Generate some random noise
	for (int i = 0; i < size_x; i++) {
		for (int j = 0; j < size_y; j++) {
			m.setPoint(i, j, (TileType) p.coherentNoise2D(i,j));
		}
	}

}