#ifndef TEST_H
#define TEST_H

#include "profession.h"
#include "question.h"
#include "subject.h"
#include "answer.h"
#include <vector>
#include <string>
#include <iostream>

class test
{
private:
	static std::vector<question> question_list;
	static std::vector<addiction_itself> list_addiction;
	static std::vector<profession> list_profession;
	std::vector<question>::iterator current_position;
	//std::vector<profession>::iterator current_position_2;
	int question_number;
	subject current_subject;
public:
	bool ask();
	void show_results();
	static void add_adc_for_prof(int, int);
	static void add_profession(profession);
	static void add_addiction(addiction_itself);
	static void add_question(question);
	static void add_answer_for_question(int, answer);
	bool start_test();
	test(subject new_subject) :current_subject(new_subject), current_position(question_list.begin()), question_number(1)
	{
		for (int index_addiction = 1; index_addiction <= list_addiction.size(); ++index_addiction)
			current_subject.add_addiction(index_addiction, 0);
	};
};

#endif // TEST_H
