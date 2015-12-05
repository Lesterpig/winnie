#pragma once
#include "Map.h"

enum RaceType {
	HUMAN = 0,
	ELF = 1,
	ORC = 2
};

class Player {
public:
	Player(RaceType _r, Point *p);
	~Player();
	void setPosition(Point *p);
	Point* getPosition();
	bool authorized(TileType t);
	static void setAsFarAsPossible(Player &p1, Player &p2, Map &m);
private:
	RaceType r;
	Point *position;
	static int farthestPointFrom(Point x, Player p, Map m, Point &pt);
};