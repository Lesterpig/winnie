#pragma once
#include "Point.h"

struct Action {
	Point start;
	Point goal;
	int bonus;
};

struct ActionCompare
{
	bool operator()(const Action* a1, const Action* a2) const
	{
		return a1->bonus < a2->bonus;
	}
};
