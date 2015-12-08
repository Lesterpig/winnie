#pragma once
#include <iostream>
#include <queue>
#include "Generator.h"
#include "Export.h"
#include "Perlin.h"
#include "Player.h"
#include "Map.h"
#include "Dijkstra.h"

#define MAX_STEP 2

typedef std::priority_queue<Action*, std::vector<Action*>, ActionCompare> ActionQueue;

class Algo {

public:
	Algo(); 
	~Algo();

	void fillMap(TileType map[], int seed, int sizeX, int sizeY);
	void findBestStartPosition(TileType map[], int sizeX, int sizeY, RaceType pl1, RaceType pl2, Point *p1, Point *p2);
	void findBestActions(TileType map[], int sx, int sy, int allies[], int nallies, int ennemies[], int nennemies, RaceType pl, Action* a1, Action* a2, Action* a3);
private:
	void affect(ActionQueue &actions, Action *a);
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

//Export Dijkstra
EXPORTCDECL Dijkstra* Dijkstra_new(double data[], int sx, int sy, Point *start) {
	return new Dijkstra(data,sx,sy, start);
}

EXPORTCDECL void Dijkstra_delete(Dijkstra* dijkstra) {
	delete dijkstra;
}

EXPORTCDECL double Dijkstra_getDistance(Dijkstra* dijkstra, Point *dest) {
	return dijkstra->getDistance(dest);
}

EXPORTCDECL int Dijkstra_getPath(Dijkstra* dijkstra, Point* dest, int* path) {
	return dijkstra->getPath(dest, path);
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