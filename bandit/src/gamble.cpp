#include "gamble.h"


int gamble::player_make_bet(unsigned int rate)
{
    if (rate<=current_player.show_money())
    {
        current_player.make_bet(rate);
        current_automat.get_bet(rate);
        return 1;
    }
    else
    {
        std::cout<<"You do not have enough money to bet!"<<std::endl;
        return 0;
    }
}

void gamble::start_game()
{
    current_automat.print_cur_position();
    std::cout<<"To start - press spacebar..."<<std::endl;
    while(!GetAsyncKeyState(0x20))
    {
    }
    current_automat.rotate();
}

void gamble::get_prize()
{
    unsigned int prize_sum=current_automat.result();
    if (prize_sum==0)
        std::cout<<"You have not won anything"<<std::endl;
    else
        std::cout<<"Congratulations! You win "<<prize_sum<<std::endl;
    current_player.get_win(prize_sum);
}
