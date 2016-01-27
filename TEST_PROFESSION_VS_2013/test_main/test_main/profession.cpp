#include "profession.h"
#include <vector>

bool profession::check_addiction(int index_for_search)
{
	std::vector<int>::iterator current_position = addictions.begin();
	while (current_position != addictions.end())
	{
		if (*current_position == index_for_search)
			return true;
		++current_position;
	}
	return false;
}