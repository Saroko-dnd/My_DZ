#ifndef PLAYER_H
#define PLAYER_H

#include <iostream>
#include "automat.h"
#include <string>

class player
{
private:
    std::string name;
    unsigned int money;
public:
    unsigned int make_bet(unsigned int amount);
    unsigned int show_money()
    {
        return money;
    };
    std::string show_name()
    {
        return name;
    };
    void get_win (unsigned int result)
    {
        money+=result;
    };
    player(unsigned int amount_of_money,std::string current_name):money(amount_of_money),name(current_name)
    {
    };
};

#endif // PLAYER_H
