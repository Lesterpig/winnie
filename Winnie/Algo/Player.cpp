#include "Player.h"

Player::Player(RaceType _r, Point *p) : r(_r), position(p)
{
}

Player::~Player()
{
}

void Player::setPosition(Point *p) 
{
	position = p;
}

Point* Player::getPosition() 
{
	return position;
}

bool Player::authorized(TileType t) 
{
	bool forbidden = false;
	forbidden = forbidden || t == WATER && r == ELF; //Elf can't be on water
	forbidden = forbidden || t == WATER && r == ORC; //Orc can't be on water

	return !forbidden;
}

int Player::farthestPointFrom(Point x, Player p, Map m, Point &pt)
{
	int maxDistance = 0;
	int caseDistance = 0;

	for (int i = 0; i < m.getSizeX(); i++) {
		for (int j = 0; j < m.getSizeY(); j++) {
			if (p.authorized(m.getPoint(i,j))) {
				caseDistance = m.distance(x,Point{i,j});

				if (caseDistance > maxDistance) {
					maxDistance = caseDistance;
					pt.x = i;
					pt.y = j;
				}
			}
		}
	}
	return maxDistance;
}

void Player::setAsFarAsPossible(Player &p1, Player &p2, Map &m)
{
	int maxDistance = 0;
	int caseDistance = 0;
	Point pt;

	for (int i = 0; i < m.getSizeX(); i++) {
		for (int j = 0; j < m.getSizeY(); j++) {
			if (p1.authorized(m.getPoint(i,j))) {
				caseDistance = Player::farthestPointFrom(Point{i,j}, p2, m, pt);

				if (caseDistance > maxDistance) {
					maxDistance = caseDistance;

					p1.getPosition()->x = i;
					p1.getPosition()->y = j; 

					p2.getPosition()->x = pt.x;
					p2.getPosition()->y = pt.y;
				}
			}
		}
	}

	/*
	for (int i = 0; i < size_x; i++) {
		for (int j = 0; j < size_y; j++) {
			if (player1.authorized(m.getPoint(i,j))) {

				for (int k = 0; i < size_x; k++) {
					for (int l = 0; l < size_y; l++) {
						if (player2.authorized(m.getPoint(k,l))) {
							int d = m.distance(Point{i,j}, Point{k,l});
							if (d > bestDistance) {
								bestDistance = d;
								p1->x = i; p1->y = j;
								p2->x = k; p2->y = l;
							}
						}
					}
				}

			}
		}
	}
	*/
}