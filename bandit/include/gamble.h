#ifndef GAMBLE_H
#define GAMBLE_H

#include "automat.h"
#include "player.h"
#include "cylinder.h"
#include <iostream>
#include <windows.h>
#include <conio.h>

class gamble
{
    private:
        player current_player;
        automat current_automat;
    public:
        gamble(player cur_player,automat cur_automat):current_player(cur_player),current_automat(cur_automat)
        {
        };
        int player_make_bet(unsigned int rate);
        void start_game();
        void get_prize();
        void show_results()
        {
            std::cout<<"player "<<current_player.show_name()<<"\n"<<"money: "<<current_player.show_money()<<std::endl;
        }
};

#endif // GAMBLE_H
