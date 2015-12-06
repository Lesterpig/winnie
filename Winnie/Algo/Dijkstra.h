#pragma once
#include <iostream>
#include <math.h>
#include <queue>
#include "Point.h"
#include "Node.h"
#include "Graph.h"

class Dijkstra {
public:
	Dijkstra(std::vector<double> data, int sx, int sy, Point *start, Point *goal);
	~Dijkstra();
	double* getDistance(Point *p);
	Point* getPath(Point *p);
private:
	bool found;
};