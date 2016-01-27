#ifndef QUESTION_H
#define QUESTION_H

#include <string>
#include "answer.h"
#include <vector>
#include <map>

class question
{
    private:
        std::string question_itself;//предположительно текст вопроса
		std::vector<answer> list_answers;
    public:
		std::map<int, int> get_results_list(int);
        void print_question();
		std::string get_question();
		void print_current_answer(int);
		int get_number_of_answers();
		void add_answer(answer);
		question::question(std::string new_string) :question_itself(new_string)
		{
		};
};

#endif // QUESTION_H

