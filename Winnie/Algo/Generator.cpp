#include <iostream>
#include <random>
#include "Generator.h"

Generator::Generator() 
{
    //Could be replaced here by a int, which could be our seed
    rng.seed(std::random_device()());
}

Generator::~Generator()
{

}

TileType Generator::randomTile() 
{
    return (TileType) std::uniform_int_distribution<MyRNG::result_type>{0, 3}(rng);
}