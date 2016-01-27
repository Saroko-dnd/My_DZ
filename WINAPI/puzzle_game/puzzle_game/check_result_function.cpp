
#include "check_result_function.h"
#include <vector>
#include "my_structures.h"

int check_result(std::vector<current_hwnd> list_of_rect)
{
	for (auto counter = list_of_rect.begin(); counter != list_of_rect.end(); ++counter)
	{
		if (counter->check_pos == 2)
			return 0;
	}
	if (list_of_rect[0].bottom_number != list_of_rect[3].top_number)
		return 1;
	if (list_of_rect[0].right_number != list_of_rect[1].left_number)
		return 1;
	if (list_of_rect[1].bottom_number != list_of_rect[4].top_number)
		return 1;
	if (list_of_rect[1].right_number != list_of_rect[2].left_number)
		return 1;
	if (list_of_rect[2].bottom_number != list_of_rect[5].top_number)
		return 1;
	if (list_of_rect[3].right_number != list_of_rect[4].left_number)
		return 1;
	if (list_of_rect[3].bottom_number != list_of_rect[6].top_number)
		return 1;
	if (list_of_rect[4].bottom_number != list_of_rect[7].top_number)
		return 1;
	if (list_of_rect[4].right_number != list_of_rect[5].left_number)
		return 1;
	if (list_of_rect[5].bottom_number != list_of_rect[8].top_number)
		return 1;
	if (list_of_rect[6].right_number != list_of_rect[7].left_number)
		return 1;
	if (list_of_rect[7].right_number != list_of_rect[8].left_number)
		return 1;
	return 2;
}
