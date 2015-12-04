#pragma once
#include <iostream>
#include "Generator.h"

class Algo {

public:
	Algo(); 
	~Algo();

	void fillMap(TileType map[], int sizeX, int sizeY);

private:
};

#if defined(_MSC_VER)
	#define EXPORTCDECL extern "C" __declspec(dllexport)
#else
	#define EXPORTCDECL extern "C"
#endif
//
// export all C++ class/methods as friendly C functions to be consumed by external component in a portable way
///

EXPORTCDECL void Algo_fillMap(Algo* algo, TileType map[], int sizeX, int sizeY) {
	return algo->fillMap(map, sizeX, sizeY);
}

EXPORTCDECL Algo* Algo_new() {
	return new Algo();
}

EXPORTCDECL void Algo_delete(Algo* algo) {
	delete algo;
}