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

	for (int i; i < size_x; i++) {
		for (int j; j < size_y; j++) {
			m.setPoint(i,j,(TileType)0);
		}
	}
}