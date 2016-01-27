#include "test.h"
#include "answer.h"
#include "question.h"
#include <vector>
#include <string>
#include <iostream>
#include <map>

std::vector<question> test::question_list;
std::vector<addiction_itself> test::list_addiction;
std::vector<profession> test::list_profession;

bool test::ask()
{
	std::cout <<question_number<<"."<< current_position->get_question() << std::endl;
	int number_of_answers = current_position->get_number_of_answers();
	int counter = 0;
	int answer_number = 0;
	std::cout << "\n";
	while (counter < number_of_answers)
	{
		std::cout << counter + 1 << ".";
		current_position->print_current_answer(counter);
		++counter;
	}
	while (answer_number<1 || answer_number>number_of_answers)
	{
		std::cin >> answer_number;
		if (answer_number<1 || answer_number>number_of_answers)
			std::cout << "Please enter correct number of answer.\n";
	}
	std::map<int, int> buf_bonus_list = current_position->get_results_list(answer_number);
	std::map<int, int>::iterator current_bonus = buf_bonus_list.begin();
	while (current_bonus != buf_bonus_list.end())
	{
		current_subject.change_addiction_value(current_bonus->first, current_bonus->second);
		++current_bonus;
	}
	++current_position;
	++question_number;
	if (current_position == question_list.end())
		return false;
	return true;
}

void test::add_adc_for_prof(int index_of_profession, int index_of_addiction)
{
	std::vector<profession>::iterator current_position_2 = list_profession.begin();
	for (int counter = 0; counter < index_of_profession; ++counter)
		++current_position_2;
	current_position_2->add_addiction(index_of_addiction);
}

void test::show_results()
{
	bool search_switch = true;
	bool switch_index = false;
	std::cout << "List of eligible professions for " << current_subject.get_name() << " (based on the test results):\n" << std::endl;
	std::vector<profession>::iterator cur_position = list_profession.begin();
	std::map<int, int> results_of_subject = current_subject.get_addiction_list();
	std::map<int, int>::iterator cur_position_2 = results_of_subject.begin();
	std::vector<std::string> results_for_print;
	std::vector<std::string>::iterator result_position = results_for_print.begin();
	while (search_switch)
	{
		while (cur_position_2 != results_of_subject.end() && cur_position_2->second <=2)
			++cur_position_2;
		if (cur_position_2 == results_of_subject.end())
			search_switch = false;
		else
		{
			while (cur_position != list_profession.end())
			{
				if (cur_position->check_addiction(cur_position_2->first))
				{
					if (result_position == results_for_print.begin())
					{
						results_for_print.push_back(cur_position->get_profession());
					}
					else
					{
						while (result_position != results_for_print.end() && *result_position != cur_position->get_profession())
							++result_position;
						if (result_position == results_for_print.end())
						{
							results_for_print.push_back(cur_position->get_profession());
						}
					}
				}
				result_position = results_for_print.begin();
				++cur_position;
			}
			cur_position = list_profession.begin();
			++cur_position_2;
		}
	}
	if (results_for_print.size() == 0)
	{
		std::cout << "Unfortunately your answers are not allowed to make a list of suitable professions." << std::endl;
	}
	else
	{
		while (result_position != results_for_print.end())
		{
			std::cout << *result_position << std::endl;
			++result_position;
		}
	}
}

void test::add_profession(profession new_profession)
{
	list_profession.push_back(new_profession);
}

void test::add_addiction(addiction_itself new_addiction)
{
	list_addiction.push_back(new_addiction);
}

void test::add_question(question current_question)
{
	question_list.push_back(current_question);
}

void test::add_answer_for_question(int number_of_question, answer new_answer)
{
	question_list[number_of_question - 1].add_answer(new_answer);
}