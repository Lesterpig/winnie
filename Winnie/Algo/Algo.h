#pragma once
#include <iostream>
#include "Generator.h"
#include "Export.h"
#include "Perlin.h"

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

EXPORTCDECL Perlin* Perlin_new(int seed, int sx, int sy, int min, int max, int st, int oc, double pers) {
	return new Perlin(seed, sx, sy, min, max, st, oc, pers);
}

EXPORTCDECL void Perlin_delete(Perlin* perlin) {
	delete perlin;
}

EXPORTCDECL double Perlin_coherentNoise2D(Perlin* perlin, double x, double y) {
	return perlin->coherentNoise2D(x,y);
}