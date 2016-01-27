#ifndef CENTRAL_BANK_H
#define CENTRAL_BANK_H

#include "Stock.h"
#include "Buddy.h"

class central_bank:public Buddy
{
private:
double rate_past_buy;
double rate_past_sell;
public:
    void act();
    central_bank(Stock& stock): Buddy(stock)
    {
    };
};

#endif // CENTRAL_BANK_H
