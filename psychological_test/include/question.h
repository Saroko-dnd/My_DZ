#ifndef QUESTION_H
#define QUESTION_H

#include <string>
#include "answer.h"
#include <vector>

class question
{
    private:
        std::string question_itself;//предположительно текст вопроса
    public:
        void print_question();
        question(std::string)
        {

        };
};

#endif // QUESTION_H
