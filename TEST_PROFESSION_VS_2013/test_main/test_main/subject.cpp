#include "subject.h"
#include <map>

void subject::change_addiction_value(int addiction_index, int bonus)
{
	std::map<int, int>::iterator current_position = list_aptitudes.begin();
	while (current_position->first != addiction_index)
		++current_position;
	current_position->second += bonus;
}