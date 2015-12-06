#pragma once
#include <vector>
#include <algorithm>
#include "Node.h"
#include "Point.h"

class Graph 
{
public:
	Graph(std::vector<double> data, int sx, int sy);
	~Graph();

	Node* getNode(const Point* p);
	Node* getNode(int x, int y);
	bool unknownNeighbourg(const Node* c, Node* n);

private:
	int sizeX;
	int sizeY;
	std::vector<Node*> allNodes;
	std::vector<Node*> dirtyNodes;
	bool pristine(Node* n);
};
