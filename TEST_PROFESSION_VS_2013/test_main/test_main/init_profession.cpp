#include "init_profession.h"
#include "profession.h"
#include "test.h"
#include <fstream>
#include <string>
#include <iostream>

bool init_profession()
{
	bool swi_1 = true, swi_2 = true,check_eof=false;
	std::string current_text,current_text_2, string_buf, str_buf_addiction;
	int useful_number, int_buf = 0;
	int position_of_profession = 0, check_1 = 1, check_2, addiction_buf;
	std::ifstream professions_in("our_professions.csv");
	std::ifstream addictions_for_professions_in("adc_for_prof.csv");
	std::cout << "our_professions.csv-loaded" << std::endl;
	if (!professions_in.is_open())
	{
		std::cout << "Error - cant open file 'our_professions.csv'" << std::endl;
		return false;
	}
	if (!addictions_for_professions_in.is_open())
	{
		professions_in.close();
		std::cout << "Error - cant open file 'adc_for_prof.csv'" << std::endl;
		return false;
	}
	while (!professions_in.eof())
	{
		std::getline(professions_in, current_text);
		useful_number = current_text.find(';');
		string_buf = current_text.substr(0, useful_number);
		if (stoi(string_buf) - 1 == int_buf)
			int_buf = stoi(string_buf);
		else
		{
			addictions_for_professions_in.close();
			professions_in.close();
			std::cout << "addiction load error - file corrupted" << std::endl;
			return false;
		}
		string_buf = current_text.substr(useful_number + 1, current_text.size() - useful_number - 1);
		profession new_profession(string_buf);
		test::add_profession(new_profession);
		if (swi_1)
		{
			std::getline(addictions_for_professions_in, current_text_2);
			swi_1 = false;
		}
		while (swi_2 && !check_eof)
		{
			if (addictions_for_professions_in.eof())
				check_eof = true;
			addiction_buf = stoi(current_text_2.substr(current_text_2.find(';') + 1, current_text_2.size() - (current_text_2.find(';') + 1)));
			test::add_adc_for_prof(position_of_profession, addiction_buf);
			if (!addictions_for_professions_in.eof())
				std::getline(addictions_for_professions_in, current_text_2);
			check_2 = stoi(current_text_2.substr(0, current_text_2.find(';')));
			if ((check_2 - check_1) != 0)
				swi_2 = false;
		}
		swi_2 = true;
		++check_1;
		++position_of_profession;
	}
	addictions_for_professions_in.close();
	professions_in.close();
	std::cout << "our_professions.csv-closed" << std::endl;
	return true;
}