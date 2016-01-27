#ifndef SUBJECT_H
#define SUBJECT_H

#include "addiction.h"
#include "answer.h"
#include <map>
#include <string>

class subject
{
    private:
        std::map<int,int> list_aptitudes;
        std::string name;
    public:
        addiction_itself get_addiction(int);
		void add_addiction(int,int);
		void change_addiction_value(int, int);
        subject(std::string);

};

#endif // SUBJECT_H
