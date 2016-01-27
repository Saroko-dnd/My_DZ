#include "Player.h"

#include <iostream>

void Player::act()
{
   // std::cout<<"check"<<std::endl;
    //std::cout<<"check"<<std::endl;
    int res=rand();
   // std::cout<<"check"<<std::endl;
    if (res%2==0)
    {
        if (stock.getSellRate()-stock.getBuyRate()>1.0)
        {
            if (rand()%2==0)
                stock.makeBuyBet( stock.getBuyRate()+((double)(rand()%5)/100), pow(10, rand() * 5.0 / RAND_MAX));
            else
                stock.makeSellBet( stock.getSellRate()-((double)(rand()%5)/100), pow(10, rand() * 5.0 / RAND_MAX));
        }
        else
        if (stock.getSellRate()-stock.getBuyRate()<(-1.0))
        {
            if (rand()%2==0)
                stock.makeBuyBet( stock.getBuyRate()-((double)(rand()%5)/100), pow(10, rand() * 5.0 / RAND_MAX));
            else
                stock.makeSellBet( stock.getSellRate()+((double)(rand()%5)/100), pow(10, rand() * 5.0 / RAND_MAX));
        }
        else
        {
        if (rand()%2==0)
            stock.makeBuyBet( stock.getSellRate()+((double)(rand()%5)/100), pow(10, rand() * 5.0 / RAND_MAX));
        else
            stock.makeBuyBet( stock.getSellRate()-((double)(rand()%5)/100), pow(10, rand() * 5.0 / RAND_MAX));
        }
    }
    else
    {
        if (rand()%2==0)
            stock.makeSellBet(stock.getBuyRate()+((double)(rand()%5)/100), pow(10, rand() * 5.0 / RAND_MAX));
        else
            stock.makeSellBet(stock.getBuyRate()-((double)(rand()%5)/100), pow(10, rand() * 5.0 / RAND_MAX));
    }
    //std::cout<<"check"<<std::endl;
}
