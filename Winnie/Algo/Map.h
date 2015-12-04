#pragma once
#include "Map.h"

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
	TileType getPoint(int x, int y);

private:
	int map_x, map_y;
	TileType *map;
};
