#ifndef EXCHANGE__H
#define EXCHANGE__H

#include <iostream>
#include <map>

class exchange_
{
private:
    std::map<double,double> buying;
    std::map<double,double> selling;
    std::map<double,double>::iterator it;
public:
    void create_buy_obj(double rate,double amount);//сделать случайным
    void create_sell_obj(double rate,double amount);//сделать случайным
    void buy_something (double amount);
    void sell_something (double amount);
    void sell_buy_check();
    void get_rate();
    void get_amount();
    exchange_() {}
    ~exchange_() {}
};

void exchange_::create_sell_obj(double rate,double amount)
{
    it=selling.find(rate);
    if (it!=selling.end())
        selling.at(rate)+=amount;
    else
    {
        selling[rate]=amount;
    }
}

void exchange_::create_buy_obj(double rate,double amount)
{
    it=buying.find(rate);
    if (it!=buying.end())
        buying.at(rate)+=amount;
    else
    {
        buying[rate]=amount;
    }
}

void exchange_::buy_something(double amount)
{
    while (amount>0.0)
    {
        it=selling.begin();
        if (it!=selling.end() && amount<it->second)
        {
            it->second-=amount;
        }
        else if (it!=selling.end() && amount>it->second)
        {
            amount-=it->second;
            selling.erase(it);
        }
        else
        {
            amount=0.0;
            selling.erase(it);
        }
        if (amount>0.0)
        {
            ++it;
        }
    }
}

void exchange_::sell_something(double amount)
{
    while (amount>0.0)
    {
        it=buying.begin();
        if (it!=buying.end() && amount<it->second)
        {
            it->second-=amount;
        }
        else if (it!=buying.end() && amount>it->second)
        {
            amount-=it->second;
            buying.erase(it);
        }
        else
        {
            amount=0.0;
            buying.erase(it);
        }
        if (amount>0.0)
        {
            ++it;
        }
    }
}


#endif // EXCHANGE__H
