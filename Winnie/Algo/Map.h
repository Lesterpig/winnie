﻿#pragma once
#include <cmath>
#include "Action.h"
#include "Dijkstra.h"
#include "Point.h"
#include "Race.h"

enum TileType {
	WATER = 0,
	PLAIN = 1,
	FOREST = 2,
	MOUNTAIN = 3
};

class Map {
public:
	Map(int x, int y, enum TileType *m);
	~Map();
	void setPoint(int x, int y, TileType p);
	void setRawPoint(int i, TileType p);
	void addAllies(int* allies, int nallies);
	void addEnnemies(int* ennemies, int nennemies);
	void getDistanceMap(double* arr, RaceType pl);
	Point getAllie(int i);
	Action bestPosition(Dijkstra &pf, double maxStep, RaceType pl, int allieIdentifier);
	TileType getPoint(int x, int y);
	double getCost(TileType t, RaceType r);
	int getVictory(TileType t, RaceType r);
	int getTilesNumber();
	int distance(Point p1, Point p2);
	int getSizeX();
	int getSizeY();

private:
	int map_x, map_y;
	TileType *map;
	std::vector<Point> allies;
	std::vector<Point> ennemies;
};
