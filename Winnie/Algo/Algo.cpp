#include <iostream>
#include <algorithm>
#include <time.h>
#include <math.h> 
#include "Algo.h"

using namespace std;

Algo::Algo() 
{
}

Algo::~Algo()
{
}

void Algo::fillMap(TileType map[], int seed, int size_x, int size_y)
{
	double min = 0.0, max = 5;
	int step = 5, octaves = 5, persistance = 0.1;

	Map m = Map(size_x, size_y, map);
	Perlin p = Perlin(seed, size_x, size_y, min, max, step, octaves, persistance);

	for (int i = 0; i < size_x; i++) {
		for (int j = 0; j < size_y; j++) {
			m.setPoint(i, j, (TileType) (p.coherentNoise2D(i,j)));
		}
	}

}
void Algo::findBestStartPosition(TileType map[], int size_x, int size_y, RaceType pl1, RaceType pl2, Point *p1, Point *p2)
{
	//Initialize data
	Map m = Map(size_x, size_y, map);
	p1->x = -1; p1->y = -1;
	p2->x = -1; p2->y = -1;

	Player player1 = Player(pl1, p1);
	Player player2 = Player(pl2, p2);

	Player::setAsFarAsPossible(player1, player2, m);
}

void Algo::findBestActions(TileType map[], int units[]) 
{
}