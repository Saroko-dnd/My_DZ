#ifndef TEST_H
#define TEST_H

#include "question.h"
#include "subject.h"
#include "answer.h"
#include <vector>
#include <string>

class test
{
    private:
        static std::vector<question> question_list;
        static std::vector<answer> answer_list;
        std::vector<question>::iterator current_position;//номер текущего вопроса
    public:
        static void add_question(question);
		static void add_answer(answer);
        bool start_test();
        test(subject,std::vector<question>,std::vector<answer>);
        virtual ~test();
};

#endif // TEST_H

