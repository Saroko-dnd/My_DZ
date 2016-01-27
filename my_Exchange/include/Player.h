#ifndef PLAYER_H
#define PLAYER_H

#include "Buddy.h"
#include <ctime>
#include <stdlib.h>
#include <cmath>

class Player:public Buddy
{
    public:
        void act();
        Player(Stock& stock) : Buddy(stock)
        {
        };
};

#endif // PLAYER_H
