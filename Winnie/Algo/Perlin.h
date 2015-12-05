#pragma once
#include <iostream>
#include "Export.h"
#include "Generator.h"

const double pi = 3.14159265;

class Perlin {
public:
	Perlin(int seed, int sx, int sy, double min, double max, int st, int oc, double pers);
	~Perlin();
	double coherentNoise2D(double x, double y);

private:
	std::vector<double> *v;
	int sizeX;
	int sizeY;
	int step;
	int octaves;
	double persistence;

	double getPoint(int x, int y);
	double linearInterpolation(int a, int b, double x);
	double cosinusInterpolation(int a, int b, double x);
	double cosinusInterpolation2D(double a1, double b1, double a2, double b2, double x, double y);
	double noise2D(double x, double y);
};

