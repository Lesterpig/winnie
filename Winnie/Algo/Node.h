#pragma once
#include "Point.h"
class Node
{
public:
	Node(int _x, int _y, double _cost);
	~Node();

	double getPriority() const;

	int getX() const;
	int getY() const;
	double getCost() const;

	Node* getCameFrom();
	void setCameFrom(Node *c);

	double getCostSoFar();
	void setCostSoFar(double csf);

	int getHeuristic();
	void setHeuristic(int h);

	bool equals(Node* n);

	Point getPoint();

private:
	int x;
	int y;
	double cost;
	double costSoFar;
	int heuristic;
	Node* cameFrom;
};

struct NodeCompare
{
	//Should reverse order to pop first smallest element
	bool operator()(const Node* n1, const Node* n2) const
	{
		return n1->getPriority() > n2->getPriority();
	}
};
