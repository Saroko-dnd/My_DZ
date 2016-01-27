#include "game_itself.h"
#include <iostream>
#include <windows.h>
#include <conio.h>
#include "cylinder.h"
#include "player.h"
#include "automat.h"
#include "gamble.h"

void start_game(gamble our_gamble)
{
    HANDLE stdInputHandle;
    stdInputHandle = GetStdHandle(STD_INPUT_HANDLE);
    bool game=true;
    unsigned int amount_of_money;
    while (game)
    {
        std::cout<<"Please enter amount of bet:"<<std::endl;
        std::cin>>amount_of_money;
        if (our_gamble.player_make_bet(amount_of_money))
        {
            our_gamble.start_game();
            our_gamble.get_prize();
        };
        our_gamble.show_results();
        std::cout<<"Do you wish to continue? (Spacebar-yes) (Escape-no)"<<std::endl;
        while(!GetAsyncKeyState(0x20) && !GetAsyncKeyState(0x1B))
        {
        }
        if(GetAsyncKeyState(0x1B))
            game=false;
        system("cls");
        FlushConsoleInputBuffer(stdInputHandle);//Очистка буфера для cin.
    }
}
