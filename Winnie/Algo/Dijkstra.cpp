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

		for (int i = 0; i < 4; i++) {	
		//while((neighbourgNode = graph.unknownNeighbourg(currentNode)) != nullptr) {
			neighbourgNode = graph.getNeighbourg(currentNode,i);
			if (neighbourgNode == nullptr) continue;
			if (neighbourgNode->getCost() < 0) continue;
			//std::cout << "neighbourg node: " << neighbourgNode->getX() << "," << neighbourgNode->getY() << std::endl;

			double newCost = currentNode->getCostSoFar() + neighbourgNode->getCost();

			if (neighbourgNode != startNode && (neighbourgNode->getCostSoFar() == 0 || newCost < neighbourgNode->getCostSoFar())) {
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
	//std::cout << "getDistance " << dest->x << "," << dest->y << "," << graph.getNode(dest)->getCostSoFar() << std::endl;
	return graph.getNode(dest)->getCostSoFar();
}

int Dijkstra::getPath(Point* dest, int* path)
{
	//std::cout << "getPath" << std::endl;
	auto currentNode = graph.getNode(dest);
	int i = 0;
	//std::cout << "beforeLoop" << std::endl;
	while (currentNode != nullptr) {
		//std::cout << "looping" << currentNode->getPoint().x << "," << currentNode->getPoint().y << std::endl;
		path[i++] = currentNode->getPoint().x + graph.getSizeX() * currentNode->getPoint().y;
		currentNode = currentNode->getCameFrom();
	}

	return i;
}