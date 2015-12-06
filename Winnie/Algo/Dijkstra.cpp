#include "Dijkstra.h"

Dijkstra::Dijkstra(std::vector<double> data, int sx, int sy, Point *start, Point *goal) : found(false)
{
	Graph g = Graph(data,sx,sy);
	std::priority_queue<Node*, std::vector<Node*>, NodeCompare> frontier;
	Node* startNode = g.getNode(start);
	Node* currentNode = nullptr;
	Node* neighbourgNode = nullptr;
	frontier.push(startNode);

	while (!frontier.empty()) {
		currentNode = frontier.top();
		frontier.pop();

		if (startNode->equals(currentNode)) {
			found = true;
			break;
		}
			
		while(g.unknownNeighbourg(currentNode,neighbourgNode)) {
			if (neighbourgNode->getCost() < 0) break;

			double newCost = currentNode->getCostSoFar() + neighbourgNode->getCost();

			if (neighbourgNode->getCostSoFar() == -1 || newCost < neighbourgNode->getCostSoFar()) {
				neighbourgNode->setCostSoFar(newCost);
				neighbourgNode->setCameFrom(currentNode);

				frontier.push(neighbourgNode);
			}
		}
	}
}

Dijkstra::~Dijkstra()
{
}

double* Dijkstra::getDistance(Point *p)
{

}

Point* Dijkstra::getPath(Point* p)
{

}