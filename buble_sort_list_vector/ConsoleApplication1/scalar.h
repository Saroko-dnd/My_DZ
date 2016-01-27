#pragma once

#include "Value.h"
#include <vector>
#include <iostream>

class scalar :public Value
{
	unsigned int current_number;
public:
	void print_this()
	{
		std::cout << current_number;
	};
	void operator=(int new_integer)
	{
		current_number = new_integer;
	};
	scalar(int new_number) :current_number(new_number)
	{};
	scalar() :current_number(0)
	{};
	~scalar(){};
};

