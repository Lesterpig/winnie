#include "Perlin.h"
#include <math.h>

Perlin::Perlin(int seed, int sx, int sy, int min, int max, int st, int oc, double pers) : sizeX(sx), sizeY(sy), step(st), octaves(oc), persistence(pers) 
{
	auto g = Generator(seed);
	int maxWidth = (int) ceil(sizeX * pow(2, octaves - 1) / step);
	int maxHeight = (int) ceil(sizeY * pow(2, octaves - 1) / step);
	v = g.randomDouble(min, max, maxWidth * maxHeight);
}

Perlin::~Perlin() 
{
}

double Perlin::getPoint(int x, int y) 
{
	return v->at(x + sizeX * y);
}

double Perlin::linearInterpolation(int a, int b, double x) {
	return a * (1 - x) + b * x;
}

double Perlin::cosinusInterpolation(int a, int b, double x) {
	double k = (1 - cos(x * pi)) / 2;
	return linearInterpolation(a, b, k);
}

double Perlin::cosinusInterpolation2D(double a1, double b1, double a2, double b2, double x, double y) {
	double x1 = cosinusInterpolation(a1, b1, x);
	double x2 = cosinusInterpolation(a2, b2, x);
	return cosinusInterpolation(x1, x2, y);
	return 2.2;
}

double Perlin::noise2D(double x, double y) {
	int i = (int) (x / step);
	int j = (int) (y / step);

	return cosinusInterpolation2D(
   		getPoint(i,j), 
   		getPoint(i + 1, j), 
   		getPoint(i, j + 1), 
   		getPoint(i + 1, j + 1), 
   		i, 
   		j
	);
}

double Perlin::coherentNoise2D(double x, double y) {
	double sum = 0;
	double p = 1;
	int f = 1;

	int i = 0;
	for(; i < octaves ; i++) {
		sum += p * noise2D(x * f, y * f);
		p *= persistence;
		f *= 2;
	}
	  
    return sum * (1 - persistence) / (1 - p);
}