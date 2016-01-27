
#include "init_addictions.h"
#include "addiction.h"
#include "test.h"
#include <fstream>
#include <string>
#include <iostream>

bool init_addictions()
{
	std::string current_text,string_buf;
	int useful_number,int_buf=0;
	std::ifstream addictions_in("our_addictions.csv");
	if (!addictions_in.is_open())
	{
		std::cout<<"Error - cant open file 'our_addictions.csv'"<<std::endl;
		return false;
	}
	while (!addictions_in.eof())
	{
		std::getline(addictions_in,current_text);
		useful_number = current_text.find(';');
		string_buf=current_text.substr(0,useful_number);
		if (stoi(string_buf)-1==int_buf)
			int_buf=stoi(string_buf);
		else
		{
			std::cout<<"addiction load error - file corrupted"<<std::endl;
			return false;
		}
		string_buf=current_text.substr(useful_number+1,current_text.size()-useful_number-1);
		addiction_itself new_addiction(string_buf);
		test::add_addiction(new_addiction);
	}
	addictions_in.close();
	return true;
}

