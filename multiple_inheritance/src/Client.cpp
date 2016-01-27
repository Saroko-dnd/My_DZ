#include "Client.h"

#include <ctime>
#include <stdlib.h>
#include <cmath>

void Client::act()
{
    int res=rand();
    double mod;
    if (stock.getSellRate()/stock.getBuyRate()<1.0)
        mod=6.0;
    else
        mod=5.0;
    if (res%2==0)
    {
        stock.buy(pow(10, rand() * mod/ RAND_MAX));
    }
    else
    {
        stock.sell(pow(10, rand() * mod/ RAND_MAX));
    }
}
