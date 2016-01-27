#ifndef MAP_H
#define MAP_H

#include <cstdlib>
#include <iostream>
#include <ctime>
#include <SDL2/SDL.h>
#include <SDL2/SDL_image.h>

class map
{
    private:
        int size_x;
        int size_y;
        int*map_;

    public:
        map():size_x(10),size_y(10)
        {
            srand(time(NULL));
            int counter=0;
            map_=new int(100);
            while (counter<100)
            {
                map_[counter]=rand()%3;
                ++counter;
            }
        }
        ~map()
        {
            delete map_;
        }
};

#endif // MAP_H
