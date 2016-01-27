#ifndef BUDDY_H
#define BUDDY_H

#include "Stock.h"

class Buddy
{
protected:
    Stock& stock;
public:
    Buddy(Stock& st) : stock(st) {};
	virtual void act() = 0;
};


#endif // BUDDY_H_H
