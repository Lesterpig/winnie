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

TileType Generator::randomTile() 
{
    return (TileType) std::uniform_int_distribution<MyRNG::result_type>{0, 3}(rng);
}