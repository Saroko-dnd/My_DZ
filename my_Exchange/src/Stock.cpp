#include "Stock.h"

Stock::Stock()
{
}


Stock::~Stock()
{
}

void Stock::makeSellBet(const double rate, double amount)
{
    std::cout<<"1"<<std::endl;
    while (rate <= getBuyRate())
    {
        auto bet = buyBets.rbegin();
        if (amount < bet->second)
        {
            bet->second -= amount;
            return;
        }
        amount -= bet->second;
        buyBets.erase(bet->first);
        if (amount == 0)
        {
            return;
        }
    }
    std::cout<<"2"<<std::endl;
    if (sellBets.find(rate) == sellBets.end() )
    {
        sellBets[rate] = amount;
    }
    else
    {
        sellBets[rate] += amount;
    }
    std::cout<<"3"<<std::endl;
}

void Stock::makeBuyBet(const double rate, double amount)
{
    std::cout<<"1"<<std::endl;
    int swi=0;
    while (rate >= getSellRate() && swi==0)
    {
        auto bet = sellBets.begin();
        if (bet==sellBets.end())
            swi=1;
        else
        {
            if (amount < bet->second)
            {
                bet->second -= amount;
                return;
            }
            amount -= bet->second;
            sellBets.erase(bet->first);
            if (amount == 0)
            {
                return;
            }
        }
    }
    std::cout<<"2"<<std::endl;
    if (buyBets.find(rate) == buyBets.end() )
    {
        buyBets[rate] = amount;
    }
    else
    {
        buyBets[rate] += amount;
    }
    std::cout<<"3"<<std::endl;
}

void Stock::sell (double amount)
{
    while (amount > 0)
    {
        auto bet = buyBets.rbegin();
        if (amount < bet->second)
        {
            bet->second -= amount;
            return;
        }
        amount -= bet->second;
        buyBets.erase(bet->first);
    }
}

void Stock::buy (double amount)
{
    while (amount > 0)
    {
        auto bet = sellBets.begin();
        if (amount < bet->second)
        {
            bet->second -= amount;
            return;
        }
        amount -= bet->second;
        sellBets.erase(bet->first);
    }
}
