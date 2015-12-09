#pragma once
#include <vector>
#include <algorithm>
#include "Node.h"
#include "Point.h"

class Graph 
{
public:
	Graph(double data[], int sx, int sy, Point *startPoint);
	~Graph();

	Node* getNode(const Point* p);
	Node* getNode(int x, int y);
	int getSizeX() const;
	int getSizeY() const;
	Node* getNeighbourg(const Node* c, int i);

private:
	int sizeX;
	int sizeY;
	std::vector<Node*> allNodes;
};
