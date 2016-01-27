#ifndef ANSWER_H
#define ANSWER_H

#include "addiction_itself.h"
#include <string>
#include <vector>
#include <map>

class answer
{
    private:
        static std::vector<answer> all_answers;
        static std::vector<answer>::iterator current_position;
        int number;//номер ответа
        std::string answer_itself;
        std::map<addiction_itself,int> score_information;
    public:
        std::map<addiction_itself,int> get_score ();
        void print_answer();
        answer(std::vector<int>,std::vector<int>);
        virtual ~answer();

};

#endif // ANSWER_H
