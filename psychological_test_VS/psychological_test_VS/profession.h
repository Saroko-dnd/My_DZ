#ifndef PROFESSION_H
#define PROFESSION_H

#include "subject.h"
#include <vector>
#include <string>

class profession
{
    private:
		std::string name;
		std::vector<int> addictions;
    public:
        std::string get_profession ();
		void add_addiction(int);
		profession(std::string new_string) :name(new_string)
		{
		}; //конструктор будет содержать путь к файлу в качестве параметра
};

#endif // PROFESSION_H

