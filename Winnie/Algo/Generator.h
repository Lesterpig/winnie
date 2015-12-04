#pragma once
#include <iostream>
#include <random>
#include "Map.h"
#include "Generator.h"

//We'll use the Mersenne Twister which is a popular random number generator
typedef std::mt19937 MyRNG;

class Generator {
public:
	Generator(); 
	~Generator();
	TileType randomTile();

private:
	MyRNG rng;

};
