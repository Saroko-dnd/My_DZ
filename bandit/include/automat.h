#ifndef AUTOMAT_H
#define AUTOMAT_H

#include "cylinder.h"
#include <string>
#include <cstdlib>
#include <ctime>
#include <iostream>
#include <conio.h>

class automat
{
private:
    std::string f_,s_,t_;
    enum status {wait,ready,res_ready} state;//состояния
    unsigned int current_bet;
    unsigned int repository;
    cylinder first;
    cylinder second;
    cylinder third;
public:
    automat():repository(10000),current_bet(0),state (wait)
    {
        f_=first.res();
        s_=second.res();
        t_=third.res();
    };
    void print_cur_position()
    {
        system("cls");
        for (int counter_2=0; counter_2<3; ++counter_2)
        {
            std::cout<<f_[counter_2]<<" "<<s_[counter_2]<<" "<<t_[counter_2]<<std::endl;
        }
    };
    unsigned int result ();
    void rotate ();
    void get_bet(unsigned int bet);
};

#endif // AUTOMAT_H
