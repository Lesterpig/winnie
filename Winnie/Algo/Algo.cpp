#include "Algo.h"
#include <iostream>
#include <algorithm>
#include <time.h>
#include <math.h> 

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
	Generator g = Generator();

	//Generate some random noise
	int i = 0;
	for (; i < m.getTilesNumber(); i++) {
		m.setRawPoint(i,g.randomTile());
	}
}