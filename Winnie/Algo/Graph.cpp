#include "Graph.h"
#include <iostream>

Graph::Graph(double data[], int sx, int sy, Point* startPoint) : sizeX(sx), sizeY(sy)
{
	for (int i = 0; i < sx; i++) {
		for (int j = 0; j < sy; j++) {
			allNodes.push_back(new Node(i, j, data[i + sx * j]));
		}
	}
}

Graph::~Graph()
{
	//Delete nodes
	
	while (!allNodes.empty()) {
		delete allNodes.back();
		allNodes.pop_back();
	}
}

Node* Graph::getNode(const Point* p)
{
	return getNode(p->x, p->y);
}

Node* Graph::getNode(int x, int y)
{
	if (x + sizeX * y >= allNodes.size()) return nullptr;
	return allNodes.at(x + sizeX * y);
}

Node* Graph::getNeighbourg(const Node*c, int i)
{
	int deltax[] = {0, 1, 0, -1};
	int deltay[] = {1, 0, -1, 0};
	//std::cout << "DELTA " << deltax[i] << "," << deltay[i] << std::endl;

	int newX = c->getX() + deltax[i];
	int newY = c->getY() + deltay[i];

	if (newX < 0 || newX >= sizeX || newY < 0 || newY >= sizeY) return nullptr;
	return getNode(newX, newY);
}

int Graph::getSizeX() const {return sizeX; }
int Graph::getSizeY() const {return sizeY; }