
#include "Player.h"
#include "Client.h"


#include <iostream>
#include <vector>

using namespace std;



using namespace std;
int main()
{
    Stock our_stock;
    our_stock.makeBuyBet(1.7,800.0);
    our_stock.makeSellBet(2.0,800.0);
    vector<Buddy*> crowd(4);
    Player p_1(our_stock),p_2(our_stock),p_3(our_stock),p_4(our_stock);
    crowd[0]=&p_1;
    crowd[1]=&p_2;
    crowd[2]=&p_3;
    crowd[3]=&p_4;
    Buddy* stf=crowd[0];
    stf->act();
    return 0;
}
