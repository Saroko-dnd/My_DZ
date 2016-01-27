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
	std::string get_profession()
	{
		return name;
	};
	bool check_addiction(int);
	void add_addiction(int new_addiction)
	{
		addictions.push_back(new_addiction);
	};
	profession(std::string new_string) :name(new_string)
	{
	}; 
};

#endif // PROFESSION_H

