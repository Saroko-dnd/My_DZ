#ifndef CLIENT_H
#define CLIENT_H

#include "Buddy.h"
#include <ctime>
#include <stdlib.h>
#include <cmath>

class Client:public Buddy
{
    public:
        void act();
        Client(Stock& stock) : Buddy(stock) {};
};

#endif // CLIENT_H
