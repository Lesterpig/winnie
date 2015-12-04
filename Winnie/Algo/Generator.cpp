﻿#include <iostream>
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

std::vector<double>* Generator::randomDouble(int min, int max, int length) 
{
    auto v = new std::vector<double>();
    std::uniform_real_distribution<double> gen(min,max);
    v->reserve(length);

    int i = 0;
    for (; i < length; i++) {
    	double j = gen(rng);
    }

    return v;
}