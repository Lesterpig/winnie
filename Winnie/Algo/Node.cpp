#include "Node.h"

Node::Node(int _x, int _y, int _cost) : x(_x), y(_y), cost(_cost), costSoFar(-1), heuristic(-1), cameFrom(nullptr)
{
}

Node::~Node() 
{
}

inline int Node::getX() const { return x; }

inline int Node::getY() const { return y; }

inline int Node::getCost() const { return cost; }

inline Node* Node::getCameFrom() { return cameFrom; }

void Node::setCameFrom(Node *c) { cameFrom = c; }

inline int Node::getCostSoFar() { return costSoFar; }

void Node::setCostSoFar(int csf) { costSoFar = csf; }

inline int Node::getHeuristic() { return heuristic; }

void Node::setHeuristic(int h) { heuristic = h; }

int Node::getPriority() const { return cost + heuristic; }

bool Node::equals(Node* n) {
	return n->getX() == getX() && n->getY() == getY();
}