#include "Algo.h"
#include <iostream>
#include <algorithm>
#include <time.h>
#include <math.h> 

using namespace std;

void Algo::fillMap(TileType map[], int size)
{
	//TODO : init map tiles with a better algorithm
	for (int i = 0; i < size; i++)
		map[i] = (TileType)(i % 4);
}