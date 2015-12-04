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
	// --> Perlin Algorithm <--
	Map m = Map(size_x, size_y, map);
	Generator g = Generator();

	//Generate some random noise
	int i = 0;
	for (; i < m.getTilesNumber(); i++) {
		m.setRawPoint(i,(TileType) 0);
	}

	//We choose an interpolation between linear, cos and cub

	//We choose a step and interpolate

	//We're summing this noises to have a coherent noise by choosing persistancy and octaves
}