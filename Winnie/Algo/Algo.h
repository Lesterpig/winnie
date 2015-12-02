#pragma once
#include <iostream>
#include <random>

enum TileType {
	WATER = 0,
	PLAIN = 1,
	FOREST = 2,
	MOUNTAIN = 3
};

//We'll use the Mersenne Twister which is a popular random number generator
typedef std::mt19937 MyRNG;

class Algo {

public:
	Algo(); 
	~Algo() {}

	// You can change the return type and the parameters according to your needs.
	void fillMap(TileType map[], int size);

private:
	MyRNG rng;
	std::vector<TileType> map;
	TileType RandomTile();
};


//#define EXPORTCDECL extern "C" __declspec(dllexport)
#define EXPORTCDECL extern "C"
//
// export all C++ class/methods as friendly C functions to be consumed by external component in a portable way
///

EXPORTCDECL void Algo_fillMap(Algo* algo, TileType map[], int size) {
	return algo->fillMap(map, size);
}

EXPORTCDECL Algo* Algo_new() {
	return new Algo();
}

EXPORTCDECL void Algo_delete(Algo* algo) {
	delete algo;
}