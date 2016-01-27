#include "automat.h"
#include <windows.h>
#include <unistd.h>

unsigned int automat::result ()
{
    if (state==res_ready)
    {
        bool win=false;
        unsigned int win_money=current_bet;
        for (int counter=1; counter<3; ++counter)
        {
            if (f_[counter]==s_[counter] && f_[counter]==t_[counter])
            {
                win=true;
                if (f_[counter]-48==0)
                    win*=10;
                else
                {
                    win_money+=win_money/(10-(f_[counter]-48));
                }
            }
        }
        if (!win)
            win_money=0;
        current_bet=0;
        state=wait;
        if (repository<win_money)
        {
            std::cout<<"Not enough money - ask your administrator to pick up the win!"<<std::endl;
            return 0;
        }
        repository-=win_money;
        return win_money;
    }
    else
    {
        std::cout<<"You already take a prize!"<<std::endl;
        return 0;
    }
}


void automat::rotate ()
{
    std::cout<<state<<std::endl;
    if (state==ready)
    {
        bool pushed_button=false;
        unsigned int time_f_=rand()%100;
        unsigned int time_s_=rand()%100;
        unsigned int time_t_=rand()%100;
        unsigned int time;//время для цикла
        if (time_f_>time_s_)
            time=time_f_;
        else
            time=time_s_;
        if (time<time_t_)
            time=time_t_;
        unsigned int counter=0;
        while (counter<time)
        {
            if (GetAsyncKeyState(0x20)==1 && !pushed_button)
            {
                pushed_button=true;
                if (time_f_>5)
                    time_f_=rand()%5;
                if (time_s_>5)
                    time_s_=rand()%5;
                if (time_t_>5)
                    time_t_=rand()%5;
                if (time_f_>time_s_)
                    time=time_f_;
                else
                    time=time_s_;
                if (time<time_t_)
                    time=time_t_;
                counter=0;
            }
            if (time_f_>0)
            {
                --time_f_;
                f_=first.res();
            }
            if (time_s_>0)
            {
                --time_s_;
                s_=second.res();
            }
            if (time_t_>0)
            {
                --time_t_;
                t_=third.res();
            }
            system("cls");
            for (int counter_2=0; counter_2<3; ++counter_2)
            {
                std::cout<<f_[counter_2]<<" "<<s_[counter_2]<<" "<<t_[counter_2]<<std::endl;
            }
            if (pushed_button)
                std::cout<<"Wait a moment... "<<std::endl;
            else
                std::cout<<"Press spacebar to stop... "<<std::endl;
            ++counter;
            Sleep(1000);
        }
        state=res_ready;
    }
    else
    {
        std::cout<<"You must first make a bet!"<<std::endl;
    }
}


void automat::get_bet(unsigned int bet)
{
    if (state==wait)
    {
        current_bet=bet;
        repository+=bet;
        state=ready;
        std::cout<<"You have a bet! "<<state<<std::endl;
    }
    else
        std::cout<<"You cant bet at this moment!"<<std::endl;
}
