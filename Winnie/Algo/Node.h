#pragma once

class Node
{
public:
	Node(int _x, int _y, int _cost);
	~Node();

	int getPriority() const;

	int getX() const;
	int getY() const;
	int getCost() const;

	Node* getCameFrom();
	void setCameFrom(Node *c);

	int getCostSoFar();
	void setCostSoFar(int csf);

	int getHeuristic();
	void setHeuristic(int h);

	bool equals(Node* n);

private:
	int x;
	int y;
	int cost;
	int costSoFar;
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
