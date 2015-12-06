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
	Node* unknownNeighbourg(const Node* c);

private:
	int sizeX;
	int sizeY;
	std::vector<Node*> allNodes;
	std::vector<Node*> dirtyNodes;
	bool pristine(Node* n);
};
