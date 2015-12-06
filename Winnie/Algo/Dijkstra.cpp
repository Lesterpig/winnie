#include "Dijkstra.h"
#include <iostream>

Dijkstra::Dijkstra(double data[], int sx, int sy, Point *start) : graph(data,sx,sy,start)
{
	std::priority_queue<Node*, std::vector<Node*>, NodeCompare> frontier;
	Node* startNode = graph.getNode(start);
	Node* currentNode = nullptr;
	Node* neighbourgNode = nullptr;
	frontier.push(startNode);
	//std::cout << "init" << std::endl;

	while (!frontier.empty()) {
		currentNode = frontier.top();
		frontier.pop();
		//std::cout << "current node: " << currentNode->getX() << "," << currentNode->getY() << "," << currentNode->getCostSoFar() << std::endl;
			
		while((neighbourgNode = graph.unknownNeighbourg(currentNode)) != nullptr) {
			//std::cout << "neighbourg node: " << neighbourgNode->getX() << "," << neighbourgNode->getY() << std::endl;
			if (neighbourgNode->getCost() < 0) break;

			double newCost = currentNode->getCostSoFar() + neighbourgNode->getCost();

			if (neighbourgNode->getCostSoFar() == 0 || newCost < neighbourgNode->getCostSoFar()) {
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

double Dijkstra::getDistance(Point *dest)
{
	return graph.getNode(dest)->getCostSoFar();
}

void Dijkstra::getPath(Point* dest, Point* path)
{
	auto currentNode = graph.getNode(dest);
	int i = 0;
	while (currentNode->getCameFrom() != nullptr) {
		path[i++] = currentNode->getCameFrom()->getPoint();
	}
}