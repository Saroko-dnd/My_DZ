#include "cylinder.h"

std::string cylinder::res()
{
    std::string result(3,'h');
    result[1]=cylinder_itself[middle];
    if ((middle-1)<0)
        result[0]=cylinder_itself[cylinder_itself.size()-1];
    else
        result[0]=cylinder_itself[middle-1];
    if ((middle+1)>(cylinder_itself.size()-1))
        result[2]=cylinder_itself[0];
    else
        result[2]=cylinder_itself[middle+1];
    ++middle;
    if (middle>cylinder_itself.size()-1)
        middle=0;
    return result;
}
