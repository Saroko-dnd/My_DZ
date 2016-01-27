#ifndef SUBJECT_H
#define SUBJECT_H

#include "addiction.h"
#include "answer.h"
#include <vector>
#include <string>

class subject
{
    private:
        std::vector<addiction> list_aptitudes;
        std::string name;
    public:
        addiction get_addiction(int);
        subject(std::string);
        virtual ~subject();
};

#endif // SUBJECT_H
