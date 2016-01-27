#include "cylinder.h"
#include "player.h"
#include "automat.h"
#include "gamble.h"
#include <string>
#include <iostream>
#include <conio.h>
#include <cstdlib>
#include <ctime>
#include <windows.h>
#include "game_itself.h"

using namespace std;

int main()
{
    srand(time(NULL));
    unsigned int amount_of_money;
    string player_name;
    cout<<"Please enter name of player:"<<endl;
    cin>>player_name;
    cout<<"Please enter amount of money for player:"<<endl;
    cin>>amount_of_money;
    player our_player(amount_of_money,player_name);
    automat our_automat;
    gamble our_gamble(our_player,our_automat);
    start_game(our_gamble);
    cout<<"Thanks for playing!"<<endl;
    system("pause");
    return 0;
}
