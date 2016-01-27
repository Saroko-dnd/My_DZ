#include "Player.h"
#include "Client.h"
#include "central_bank.h"
#include "money_bot.h"

#include <iostream>
#include <vector>
#include <fstream>
#include <locale>

using namespace std;
int main()
{
    srand(time(NULL));
    double rrr=(double)(rand()%5)/100;
    cout<<"double rand ="<<rrr<<endl;
    ofstream fout("money_rate.csv");
    fout.imbue(std::locale(""));
    int size_days=1200,size_crowd=24;
    Stock our_stock;
    our_stock.makeBuyBet(0.5,1000000000.0);
    our_stock.makeSellBet(5.0,1000000000.0);
    our_stock.makeBuyBet(1.5,500000.0);
    our_stock.makeSellBet(1.6,500000.0);
    our_stock.makeBuyBet(1.4,500000.0);
    our_stock.makeSellBet(1.7,500000.0);
    our_stock.makeBuyBet(1.3,500000.0);
    our_stock.makeSellBet(1.8,500000.0);
    vector<Buddy*> crowd(size_crowd);
    for (int counter=0;counter<size_crowd;++counter)
    {
        if (counter%2==0)
            crowd[counter]=new Player(our_stock);
        else
            crowd[counter]=new Client(our_stock);
    }
    crowd.push_back(new central_bank(our_stock));
    crowd.push_back(new money_bot(our_stock));
    size_crowd+=2;
    system("pause");
    for (int counter=0;counter<size_days;++counter)
    {
        for (int counter_2=0;counter_2<size_crowd;++counter_2)
        {
            crowd[counter_2]->act();
        }
        fout<<our_stock.getBuyRate()<<";"<<our_stock.getSellRate()<<endl;
    }
    fout.close();
    return 0;
}
