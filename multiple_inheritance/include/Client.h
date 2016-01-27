#ifndef CLIENT_H
#define CLIENT_H

#include "Buddy.h"


class Client:public Buddy
{
    public:
        void act();
        Client(Stock& stock) : Buddy(stock) {};
};

#endif // CLIENT_H
