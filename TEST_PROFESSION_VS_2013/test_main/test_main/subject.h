#ifndef SUBJECT_H
#define SUBJECT_H

#include "addiction.h"
#include "answer.h"
#include <map>
#include <string>

class subject
{
private:
	std::map<int, int> list_aptitudes;
	std::string name;
public:
	std::map<int, int> get_addiction_list()
	{
		return  list_aptitudes;
	};
	void add_addiction(int index_addiction, int value)
	{
		list_aptitudes[index_addiction] = value;
	};
	std::string get_name()
	{
		return name;
	};
	void change_addiction_value(int , int );
	subject(std::string new_name) :name(new_name)
	{
	};

};

#endif // SUBJECT_H
