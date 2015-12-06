#include "Node.h"

Node::Node(int _x, int _y, double _cost) : x(_x), y(_y), cost(_cost), costSoFar(0), heuristic(0), cameFrom(nullptr)
{
}

Node::~Node() 
{
}

inline int Node::getX() const { return x; }

inline int Node::getY() const { return y; }

double Node::getCost() const { return cost; }

Node* Node::getCameFrom() { return cameFrom; }

void Node::setCameFrom(Node *c) { cameFrom = c; }

double Node::getCostSoFar() { return costSoFar; }

void Node::setCostSoFar(double csf) { costSoFar = csf; }

int Node::getHeuristic() { return heuristic; }

void Node::setHeuristic(int h) { heuristic = h; }

double Node::getPriority() const { return cost + heuristic; }

bool Node::equals(Node* n) {
	return n->getX() == getX() && n->getY() == getY();
}

Point Node::getPoint() {
	return Point{x,y};
}