#include "../includes/Pascal's_triangle.h"

    int* Pascal_triangle::operator[](int str)
    {
        count_=str;
        int counter=0;
        ++str;
        triangle=(int**)malloc(str*sizeof(int));
        while (counter<str)
        {
            triangle[counter]=(int*)malloc(str*sizeof(int));
            ++counter;
        }
        --str;
        int str_copy=str,counter_2=str_copy;
        counter=str_copy;
        while (str_copy>=0)
        {
            triangle[str_copy][counter]=1;
            triangle[str_copy][0]=1;
            while(counter<str)
            {
                ++counter;
                triangle[str_copy][counter]=0;
            }
            --counter_2;
            counter=counter_2;
            --str_copy;
        }
        if (str>1)
        {
            str_copy=2;
            counter=1;
            while (str_copy<=str)
            {
                while(counter<str_copy)
                {
                    triangle[str_copy][counter]=triangle[str_copy-1][counter-1]+triangle[str_copy-1][counter];
                    ++counter;
                }
                counter=1;
                ++str_copy;
            }
        }
        return  triangle [str];
    }
