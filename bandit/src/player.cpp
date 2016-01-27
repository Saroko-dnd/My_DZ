#include "player.h"

unsigned int player::make_bet(unsigned int amount)
{
    try
    {
        if (amount>money)
            throw 0;
        money-=amount;
        return amount;
    }
    catch (int res)
    {
        std::cout<<"you cant make bet - not enough money"<<std::cout;
    }
}
