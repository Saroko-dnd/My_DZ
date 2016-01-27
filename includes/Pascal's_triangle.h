#ifndef PASCALS_TRIANGLE_H_INCLUDED
#define PASCALS_TRIANGLE_H_INCLUDED

#include <iostream>
#include <cstdlib>

class Pascal_triangle
{
    private:
    int**triangle;
    int count_;
    public:
    Pascal_triangle()
    {
        triangle=NULL;
        count_=0;
    }
    ~Pascal_triangle()
    {
        if (triangle!=NULL)
        {
            while (count_>=0)
            {
                free(triangle[count_]);
                --count_;
            }
            free(triangle);
        }
    }
    int* operator[](int);
};

#endif // PASCAL'S_TRIANGLE_H_INCLUDED
