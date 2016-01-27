#include "Player.h"

#include <iostream>

void Player::act()
{
    std::cout<<"check"<<std::endl;
    srand(time(NULL));
    std::cout<<"check"<<std::endl;
    int res=rand();
    std::cout<<"check"<<std::endl;
    if (res%2==0)
    {
        stock.makeBuyBet(stock.getBuyRate() + ((double)rand() / RAND_MAX - 0.5) * (stock.getSellRate() - stock.getBuyRate()), pow(10, rand() * 9.0 / RAND_MAX));
    }
    else
    {
        stock.makeSellBet(stock.getSellRate() + ((double)rand() / RAND_MAX - 0.5) * (stock.getSellRate() - stock.getBuyRate()), pow(10, rand() * 9.0 / RAND_MAX));
    }
    std::cout<<"check"<<std::endl;
}
