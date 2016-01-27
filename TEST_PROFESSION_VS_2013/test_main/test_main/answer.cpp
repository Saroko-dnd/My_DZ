#include "answer.h"
#include <vector>
#include <iostream>

void answer::print_answer()
{
	std::cout << answer_itself << std::endl;
}

std::map<int, int> answer::get_score()
{
	return score_information;
}

answer::answer(std::string new_string)
{
	std::string string_buf_1, string_buf_2;
	int useful_number, buf_int_1, buf_int_2, current_position, buf_int;
	bool swi = true;
	useful_number = new_string.find(';');
	answer_itself = new_string.substr(0, useful_number);
	while (swi)
	{
		buf_int = new_string.find(';', useful_number + 1);
		current_position = new_string.find(',', useful_number + 1);
		buf_int_1 = stoi(new_string.substr(useful_number + 1, current_position - (useful_number + 1)));
		if (buf_int == -1)
		{
			swi = false;
			buf_int_2 = stoi(new_string.substr(current_position + 1, new_string.size() - (current_position + 1)));
		}
		else
		{
			buf_int_2 = stoi(new_string.substr(current_position + 1, new_string.find(';', current_position + 1) - (current_position + 1)));
			useful_number = new_string.find(';', useful_number + 1);
		}
		score_information[buf_int_1] = buf_int_2;
	}
}