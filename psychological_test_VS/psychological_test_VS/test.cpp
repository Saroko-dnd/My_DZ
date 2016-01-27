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

bool test::ask()//не окончен
{
	std::cout << current_position->get_question()<< std::endl;
	int number_of_answers = current_position->get_number_of_answers();
	int counter =0;
	int answer_number=-1;
	std::cout << "\n";
	while (counter < number_of_answers)
	{
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
	for (int index_2 = 0; index_2 < buf_bonus_list.size(); ++index_2)
		current_subject.change_addiction_value(current_bonus->first, current_bonus->second);
	++current_position;
	if (current_position == question_list.end())
		return false;
	return true;
}

void test::add_adc_for_prof(int index_of_profession, int index_of_addiction)
{
	auto current_position_2 = list_profession.begin();
	for (int counter = 0; counter < index_of_profession - 1; ++counter)
		++current_position_2;
	current_position_2->add_addiction(index_of_addiction);
}

void test::get_results()
{

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