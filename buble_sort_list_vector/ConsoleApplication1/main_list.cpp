#include "main_list.h"
#include <vector>
#include <iostream>
#include <initializer_list>

void main_list::print_this()
{
	int current_position=0;
	std::cout << '[';
	while (current_position < current_json.size())
	{
		current_json[current_position].print_this();
		std::cout << ',';
		++current_position;
	}
	std::cout << ']';
}
