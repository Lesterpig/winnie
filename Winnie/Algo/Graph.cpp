#include "Graph.h"

Graph::Graph(std::vector<double> data, int sx, int sy) : sizeX(sx), sizeY(sy)
{
	allNodes.reserve(sx*sy);
	for (int i = 0; i < sx; i++) {
		for (int j = 0; j < sy; j++) {
			allNodes.push_back(new Node(i, j, data[i + sx * j]));
		}
	}
}

Graph::~Graph()
{
	//Delete nodes
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
	return found;
}

bool Graph::unknownNeighbourg(const Node* c, Node* n)
{
	if (c->getY() > 0 && pristine(getNode(c->getX(), c->getY() - 1))) {
		n = getNode(c->getX(), c->getY() - 1);
		return true;
	}
	return false;
}