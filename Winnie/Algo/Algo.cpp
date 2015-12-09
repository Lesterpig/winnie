#include <iostream>
#include <algorithm>
#include <math.h> 
#include "Action.h"
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
	int step = 4, octaves = 5, persistance = 0.1;

	Map m = Map(size_x, size_y, map);
	Perlin p = Perlin(seed, size_x, size_y, min, max, step, octaves, persistance);

	for (int i = 0; i < size_x; i++) {
		for (int j = 0; j < size_y; j++) {
			TileType t; 
			double n = p.coherentNoise2D(i,j);
			if (n < 1.0) t = WATER;
			else if (n < 2.0) t = PLAIN;
			else if (n < 3.0) t = FOREST;
			else t = MOUNTAIN;

			m.setPoint(i, j, t);
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

void Algo::findBestActions(TileType map[], int sx, int sy, int allies[], int nallies, int ennemies[], int nennemies, RaceType pl, Action* a1, Action* a2, Action* a3) 
{
	Map m = Map(sx, sy, map);
	m.addAllies(allies, nallies);
	m.addEnnemies(ennemies, nennemies);

	ActionQueue actions;

	double* dist = new double[sx*sy];
	m.getDistanceMap(dist, pl);
	Action listActions[nallies];

	for (int i = 0; i < nallies; i++) {
		Point selectedAllie = m.getAllie(i);
		//std::cout << "selected allie " << selectedAllie.x << "," << selectedAllie.y << std::endl;
		Dijkstra pf = Dijkstra(dist, sx, sy, &selectedAllie);
		listActions[i] = m.bestPosition(pf,MAX_STEP,pl,i);
		//std::cout << a.start.x << "," <<a.start.y << ";" << a.goal.x << "," << a.goal.y << std::endl;

		actions.push(&(listActions[i]));
	}
	affect(&actions, a1);
	affect(&actions, a2);
	affect(&actions, a3);

}

void Algo::affect(ActionQueue *actions, Action* a) 
{
	Action* tmp = actions->top();
	actions->pop();
	a->start = tmp->start;
	a->goal = tmp->goal;
	a->bonus = tmp->bonus;
}