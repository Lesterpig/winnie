#pragma once
#include <iostream>
#include "Generator.h"
#include "Export.h"
#include "Perlin.h"
#include "Player.h"
#include "Map.h"

class Algo {

public:
	Algo(); 
	~Algo();

	void fillMap(TileType map[], int seed, int sizeX, int sizeY);
	void findBestStartPosition(TileType map[], int sizeX, int sizeY, RaceType pl1, RaceType pl2, Point *p1, Point *p2);
	void findBestActions(TileType map[], int units[]);
private:
};

//Export Algo
EXPORTCDECL Algo* Algo_new() {
	return new Algo();
}

EXPORTCDECL void Algo_delete(Algo* algo) {
	delete algo;
}

EXPORTCDECL void Algo_fillMap(Algo* algo, TileType map[], int seed, int sizeX, int sizeY) {
	return algo->fillMap(map, seed, sizeX, sizeY);
}

EXPORTCDECL void Algo_findBestStartPosition(Algo* algo, TileType map[], int sizeX, int sizeY, RaceType pl1, RaceType pl2, Point *p1, Point *p2) {
	return algo->findBestStartPosition(map, sizeX, sizeY, pl1, pl2, p1, p2);
}

//Export Perlin
EXPORTCDECL Perlin* Perlin_new(int seed, int sx, int sy, double min, double max, int st, int oc, double pers) {
	return new Perlin(seed, sx, sy, min, max, st, oc, pers);
}

EXPORTCDECL void Perlin_delete(Perlin* perlin) {
	delete perlin;
}

EXPORTCDECL double Perlin_coherentNoise2D(Perlin* perlin, double x, double y) {
	return perlin->coherentNoise2D(x,y);
}