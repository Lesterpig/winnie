#pragma once
#include <iostream>
#include <math.h>
#include <queue>
#include "Point.h"
#include "Node.h"
#include "Graph.h"

class Dijkstra {
public:
	Dijkstra(double data[], int sx, int sy, Point *start);
	~Dijkstra();
	double getDistance(Point *dest);
	int getPath(Point* dest, int* path);
private:
	Graph graph;

};