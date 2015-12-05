﻿#include "Map.h"

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

void Map::setRawPoint(int i, TileType p)
{
	map[i] = p;
}

int Map::getTilesNumber()
{
	return map_x * map_y;
}

int Map::distance(Point p1, Point p2)
{
	return std::abs(p1.x - p2.x) + std::abs(p1.y - p2.y);
}

int Map::getSizeX() {
	return map_x;
}

int Map::getSizeY() {
	return map_y;
}