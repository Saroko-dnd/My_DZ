#ifndef MONEY_BOT_H
#define MONEY_BOT_H

#include "Stock.h"
#include "Buddy.h"

class money_bot:public Buddy
{
    private:
        double dollars;
        double euros;
        double rate_check;//Эта переменная хранит значение наименьшего курса продажи за определенное время.
        double rate_check_2;//Эта пременная хранит значение лучшего курса покупки в данный момент.
        int swi;//Отвечает за выбор действий для бота (в зависимости от ее значения act() выполняет разные действияz)
        int main_counter;//Этот счетчик отвечает за время в течение которого бот собирает статистику по SellRate
                         //(выбирает наименьшее значение)
    public:
    void act();
    money_bot(Stock& stock): Buddy(stock),dollars(100000.0),euros(0.0),rate_check(5.0),rate_check_2(0.0),swi(2),main_counter(0)
    {
    };
};

#endif // MONEY_BOT_H
