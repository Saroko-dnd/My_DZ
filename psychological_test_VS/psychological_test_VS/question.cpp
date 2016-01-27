#include "answer.h" 
#include "question.h"

void question::add_answer(answer new_answer)
{
	list_answers.push_back(new_answer);
}


std::string question::get_question()
{
	return question_itself;
}

int question::get_number_of_answers()
{
	return list_answers.size();
}

void question::print_current_answer(int index)
{
	list_answers[index - 1].print_answer();
}

std::map<int, int> question::get_results_list(int index_answer)
{
	return list_answers[index_answer - 1].get_score();
}