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

int Graph::getSizeX() const {return sizeX; }
int Graph::getSizeY() const {return sizeY; }