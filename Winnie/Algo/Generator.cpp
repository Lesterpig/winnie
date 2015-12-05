#include <iostream>
#include <random>
#include "Generator.h"

Generator::Generator() 
{
    rng.seed(std::random_device()());
}

Generator::Generator(int seed) 
{
    rng.seed(seed);
}

Generator::~Generator()
{

}

std::vector<double>* Generator::randomDouble(double min, double max, int length) 
{
    auto v = new std::vector<double>();
    std::uniform_real_distribution<double> gen(min,max);

    int i = 0;
    for (; i < length; i++) {
    	v->push_back(gen(rng));
    }

    return v;
}