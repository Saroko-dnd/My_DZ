#ifndef STOCK_H
#define STOCK_H

#include <iostream>
#include <map>

class Stock
{
private:
	std::map<double, double> sellBets, buyBets;

public:
	double getSellRate() const {
        std::cout<<"1_"<<std::endl;
		return sellBets.begin()->first;
	};
	double getBuyRate() const {
        std::cout<<"2_"<<std::endl;
		return buyBets.rbegin()->first;
	};


	void makeSellBet(const double rate,  double amount);
	void makeBuyBet(const double rate,  double amount);
	void buy( double amount);
	void sell( double amount);
	Stock();
	~Stock();
};

#endif // STOCK_H
