#include "central_bank.h"

void central_bank::act()
{
    if (((rate_past_buy+rate_past_sell)/2-(stock.getBuyRate()+stock.getSellRate())/2)>0.05)
    {
        stock.makeBuyBet(stock.getBuyRate(), 500000.0);
    }
    else
    if (((rate_past_buy+rate_past_sell)/2-(stock.getBuyRate()+stock.getSellRate())/2)<(-0.05))
    {
        stock.makeSellBet(stock.getSellRate(), 500000.0);
    }
    rate_past_buy=stock.getBuyRate();
    rate_past_sell=stock.getSellRate();
}
