#pragma once
#include <iostream>
#include "Generator.h"
#include "Export.h"

class Algo {

public:
	Algo(); 
	~Algo();

	void fillMap(TileType map[], int sizeX, int sizeY);

private:
};

EXPORTCDECL void Algo_fillMap(Algo* algo, TileType map[], int sizeX, int sizeY) {
	return algo->fillMap(map, sizeX, sizeY);
}

EXPORTCDECL Algo* Algo_new() {
	return new Algo();
}

EXPORTCDECL void Algo_delete(Algo* algo) {
	delete algo;
}