#include <fstream>
#include <string>
#include "answer.h"
#include "question.h"
#include "test.h"


bool init_questions_answers()
{
    std::string current_text;
    question buf(current_text);
    std::ifstream answers_in("our_answers.csv");
    std::ifstream questions_in("our_question.csv");
    if (answers_in==nullptr || questions_in==nullptr)
        return false;
    while (!questions_in.eof())
    {
        std::getline(questions_in,current_text);
		//question buf(current_text);
        test::add_question(buf);
    }
    while (!answers_in.eof())
    {
        std::getline(answers_in,current_text);

    }
    return true;
}
