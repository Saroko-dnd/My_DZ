#include "../includes/associative_array.h"

double assot_array::operator[](std::string str)
{
    int counter=0;
    while (counter<count_ && str_array[counter]!=str)
    {
        ++counter;
    }
    if (str_array[counter]==str)
    return double_array[counter];
    else
    return 0.0;
}

std::string assot_array::operator[](double number)
{
    int counter=0;
    std::string str_error=" ";
    while (counter<count_ && double_array[counter]!=number)
    {
        ++counter;
    }
    if (double_array[counter]==number)
    return str_array[counter];
    else
    return str_error;
}

void assot_array::add_element(std::string str,double number)
{
    if (counter<count_)
    {
        str_array[counter]=str;
        double_array[counter]=number;
        ++counter;
    }
}
