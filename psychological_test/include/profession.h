#ifndef PROFESSION_H
#define PROFESSION_H

#include "subject.h"
#include <vector>
#include <string>

class profession
{
    private:
        std::vector<std::string> profession_list;
    public:
        std::string get_profession (subject);
        profession(std::string); //конструктор будет содержать путь к файлу в качестве параметра
        virtual ~profession();
};

#endif // PROFESSION_H
