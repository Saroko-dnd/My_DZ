#include "Client.h"

void Client::act()
{
    srand(time(NULL));
    int res=rand();
    if (res%2==0)
    {
        stock.buy(pow(10, rand() * 0.9 / RAND_MAX));
    }
    else
    {
        stock.sell(pow(10, rand() * 0.9 / RAND_MAX));
    }
}
