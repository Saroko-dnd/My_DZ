#include "money_bot.h"

#include <stdio.h>

void money_bot::act()
{
    if ((main_counter%11!=0 || main_counter==0) && swi==2)
    {
        rate_check=main_counter==0 ? 5.0 : rate_check;
        rate_check=rate_check>stock.getSellRate() ? stock.getSellRate() : rate_check;
        ++main_counter;
    }
    else
    {
        if (swi==2)
            swi=0;
        main_counter=0;
        double buf=dollars,buf_2=euros;
        if (swi==0 && dollars>0.0)
        {
            std::cout<<"search for sell_bet..."<<std::endl;
            dollars=stock.makeBuyBet_bot(rate_check,dollars);
            if ((buf-dollars)!=0)
            {
                std::cout<<"sell_bet found!"<<std::endl;
                swi=1;
            }
            euros += (buf-dollars)/rate_check;
        }
        else
        {
            rate_check_2=stock.getBuyRate();
            std::cout<<"search for customer (current rate): "<<rate_check_2<<std::endl;
            if (euros>0.0 && rate_check<rate_check_2)
            {
                euros=stock.makeSellBet_bot(stock.getBuyRate(),euros);
                dollars += (buf_2-euros)*rate_check_2;
                if (euros==0.0)
                    swi=2;
                std::cout<<"customer was found!"<<std::endl;
            }
        }
        std::cout<<"money_bot buy rate = "<<rate_check<<std::endl;
        std::cout<<"dollars "<<dollars<<"euros "<<euros<<"swi "<<swi<<std::endl;
    getchar();
    }
}
