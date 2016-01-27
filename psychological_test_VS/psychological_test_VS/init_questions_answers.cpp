#include <fstream>
#include <string>
#include "answer.h"
#include "question.h"
#include "test.h"
#include <fstream>
#include <iostream>


bool init_questions_answers()
{
    std::string current_text,buf_main;
	int useful_number,buf_int,buf_int_2,current_value=0;
    std::ifstream answers_in("our_answers.csv");
    std::ifstream questions_in("our_question.csv");
	if (!answers_in.is_open() || !questions_in.is_open())
        return false;
	while (!questions_in.eof())
    {
        std::getline(questions_in,current_text);
		useful_number = current_text.find(';');
		buf_int_2 =stoi(current_text.substr(0, useful_number - 1));
		if (buf_int_2-current_value!=1)
		{
			std::cout<<"Error - file 'our_question.csv' was corrupted"<<std::endl;
			return false;
		}
		question buf(current_text.substr(useful_number+1,current_text.size()-(useful_number+1)));
		test::add_question(buf);
		++current_value;
    }
	questions_in.close();
	while (!answers_in.eof())
    {
		std::getline(answers_in,current_text);
		useful_number = current_text.find(';');
		buf_main=current_text.substr(0,useful_number);
		buf_int=stoi(buf_main);
		buf_main=current_text.substr(useful_number+1,current_text.size()-useful_number-1);
		answer new_answer(buf_main);
		test::add_answer_for_question(buf_int,new_answer);
    }
    return true;
}

