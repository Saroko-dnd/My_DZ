
#include "init_profession.h"
#include "profession.h"
#include "test.h"
#include <fstream>
#include <string>
#include <iostream>

bool init_addictions()
{
	std::string current_text,string_buf,str_buf_addiction;
	int useful_number,int_buf=0;
	int position_of_profession = 0,check_1=1,check_2=1,addiction_buf;
	std::ifstream professions_in("our_professions.csv");
	std::ifstream addictions_for_professions_in("adc_for_prof.csv");
	if (!professions_in.is_open())
	{
		std::cout<<"Error - cant open file 'our_addictions.csv'"<<std::endl;
		return false;
	}
	while (!professions_in.eof())
	{
		std::getline(professions_in,current_text);
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
		profession new_profession(string_buf);
		test::add_profession(new_profession);
		++position_of_profession;
		while (check_1-check_2==0)//доделать
		{
			std::getline(addictions_for_professions_in, current_text);
			addiction_buf = stoi(current_text.substr(current_text.find(';') + 1, current_text.size() - (current_text.find(';') + 1)));
			test::add_adc_for_prof(position_of_profession, addiction_buf);
		}
	}
	professions_in.close();
	return true;
}

