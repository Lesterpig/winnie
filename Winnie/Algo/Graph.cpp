#include "Graph.h"
#include <iostream>

Graph::Graph(double data[], int sx, int sy, Point* startPoint) : sizeX(sx), sizeY(sy)
{
	allNodes.assign(sx*sy, nullptr);
	for (int i = 0; i < sx; i++) {
		for (int j = 0; j < sy; j++) {
			allNodes[i + sx * j] = new Node(i, j, data[i + sx * j]);
		}
	}
	dirtyNodes.push_back(getNode(startPoint));
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
	return allNodes.at(x + sizeX * y);
}

bool Graph::pristine(Node* n)
{
	bool found = std::find (dirtyNodes.begin(), dirtyNodes.end(), n) != dirtyNodes.end();
	if (!found) dirtyNodes.push_back(n);
	return !found;
}

Node* Graph::unknownNeighbourg(const Node* c)
{
	if (c->getY() > 0 && pristine(getNode(c->getX(), c->getY() - 1))) {
		return getNode(c->getX(), c->getY() - 1);
	} else if (c->getX() > 0 && pristine(getNode(c->getX() - 1, c->getY()))) {
		return getNode(c->getX() - 1, c->getY());
	} else if (c->getY() < sizeY - 1 && pristine(getNode(c->getX(), c->getY() + 1))) {
		return getNode(c->getX(), c->getY() + 1);
	} else if (c->getX() < sizeX - 1 && pristine(getNode(c->getX() + 1, c->getY()))) {
		return getNode(c->getX() + 1, c->getY());
	}
	return nullptr;
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