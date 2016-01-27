#pragma once

#include "Value.h"
#include <vector>
#include "scalar.h"
#include <initializer_list>

//основной класс, содержащий все данные json
class main_list :public Value
{
	std::vector<Value> current_json;
public:
	void print_this();
	void add_element(scalar new_scalar)
	{
		current_json.push_back(new_scalar);
	};
	void add_element(main_list new_list)
	{
		current_json.push_back(new_list);
	};
	main_list(std::initializer_list<Value> new_init_list)
	{
		for (std::initializer_list<Value>::iterator current_position = new_init_list.begin(); current_position != new_init_list.end();++current_position)
		{
			current_json.push_back(*current_position);
		}
	};
	~main_list(){};
};

