#include "Map.h"

Map::Map(int x, int y, TileType *m) : map_x(x), map_y(y), map(m)
{
}

Map::~Map()
{
}

TileType Map::getPoint(int x, int y) 
{
	return map[x + map_x * y];
}

void Map::setPoint(int x, int y, TileType p)
{
	map[x + map_x * y] = p;
}

void Map::setRawPoint(int i, TileType p)
{
	map[i] = p;
}

int Map::getTilesNumber()
{
	return map_x * map_y;
}

int Map::distance(Point p1, Point p2)
{
	return std::abs(p1.x - p2.x) + std::abs(p1.y - p2.y);
}

int Map::getSizeX() 
{
	return map_x;
}

int Map::getSizeY()
{
	return map_y;
}

void Map::addAllies(int* al, int nallies)
{
	allies.reserve(nallies);
	for(int i = 0; i<nallies; i++) {
			allies[i] = Point{al[i] % map_x, al[i] / map_x};
	}
}

void Map::addEnnemies(int* en, int nennemies) 
{
	ennemies.reserve(nennemies);
	while (nennemies-- > 0) {
		ennemies[nennemies] = Point{en[nennemies] % map_x, en[nennemies] / map_x};
	}
}

void Map::getDistanceMap(double* arr, RaceType pl) 
{
	//Set cost for each case according to race
	for (int i = 0; i < map_x*map_y; i++) {
		arr[i] = getCost(map[i], pl);
	}

	//Remove ennemy units from accessible cases
	for (int j = 0; j < ennemies.size(); j++) {
		Point e = ennemies[j];
		arr[e.x + map_x * e.y] = -1;
	}
}

Point Map::getAllie(int i) 
{
	return allies[i];
}

Action Map::bestPosition(Dijkstra &pf, double maxStep, RaceType pl, int allieIdentifier)
{
	// @TODO If after me in allie vector, there is my point with the same position as me, 
	// I should set stayingCaseBonus to 0 and not my case value 
	// (because this unit can stay on case and keep the bonus)

	Point startPosition = getAllie(allieIdentifier);
	Action best = Action{startPosition,startPosition,0};
	int stayingCaseBonus = getVictory(getPoint(startPosition.x, startPosition.y), pl);

	for (int i = 0; i < map_x; i++) {
		for (int j = 0; j < map_y; j++) {
			Point selectedPoint = Point{i,j};

			double costSoFar = pf.getDistance(&selectedPoint);
			if(costSoFar <= maxStep && costSoFar > 0) {
				int vict = getVictory(getPoint(i,j), pl) - stayingCaseBonus;
				if (vict > best.bonus) {
					//std::cout << "selectedPoint " << i << "," << j << std::endl;
					best.goal = selectedPoint;
					best.bonus = vict;
				}
			}
		}
	}
	return best;
}

double Map::getCost(TileType t, RaceType r)
{
	if (t == WATER    && (r == ELF || r == ORC)) return -1;
	if (t == PLAIN    && r == ORC) return 0.5;
	if (t == MOUNTAIN && r == ELF) return 2; 
	return 1;
}

int Map::getVictory(TileType t, RaceType r)
{
	if (t == WATER) return 0;
	if (t == PLAIN && r == HUMAN) return 2;
	if (t == FOREST && r == ELF) return 3;
	if (t == MOUNTAIN && r == ELF) return 0;
	if (t == MOUNTAIN && r == ORC) return 2; 
	return 1;
}