#include "Map.h"

Map::Map(int x, int y, TileType *m) : map_x(x), map_y(y), map(m)
{
}

Map::~Map()
{
}

TileType Map::getPoint(int x, int y) 
{
	return map[x + map_x * y];
}

void Map::setPoint(int x, int y, TileType p)
{
	map[x + map_x * y] = p;
}
