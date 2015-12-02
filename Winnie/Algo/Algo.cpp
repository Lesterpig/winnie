#include "Algo.h"
#include <iostream>
#include <algorithm>
#include <time.h>
#include <math.h> 

using namespace std;

Algo::Algo() {
    //Could be replaced here by a int, which could be our seed
    rng.seed(std::random_device()());
}

TileType Algo::RandomTile() {
    return (TileType) std::uniform_int_distribution<MyRNG::result_type>{0, 3}(rng);
}

void Algo::fillMap(TileType map[], int size)
{
	for (int i = 0; i < size; i++) {
		map[i] = Algo::RandomTile();
    }
}