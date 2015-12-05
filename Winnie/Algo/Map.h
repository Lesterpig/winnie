#pragma once
#include <cmath>
#include "Map.h"

typedef struct {
	int x;
	int y;
} Point;

enum TileType {
	WATER = 0,
	PLAIN = 1,
	FOREST = 2,
	MOUNTAIN = 3
};

class Map {
public:
	Map(int x, int y, TileType *m);
	~Map();
	void setPoint(int x, int y, TileType p);
	void setRawPoint(int i, TileType p);
	TileType getPoint(int x, int y);
	int getTilesNumber();
	int distance(Point p1, Point p2);
	int getSizeX();
	int getSizeY();

private:
	int map_x, map_y;
	TileType *map;
};
