#include "check_attacks.h"
#include <vector>

std::vector<pos_x_y> check_attack_cells(std::vector<cell> our_field, int cur_player_id)
{
	std::vector<pos_x_y> list_of_locked_chekers;
	pos_x_y buf;
	if (cur_player_id == BLUE_PLAYER)
	{
		for (int number_of_cell = 0; number_of_cell < 64; ++number_of_cell)
		{
			if (number_of_cell + 7 < 64 && our_field[number_of_cell + 7].state && !our_field[number_of_cell + 7].color_check)
			{
				if (number_of_cell + 14 < 64 && !our_field[number_of_cell + 14].state)
				{
					buf.x_pos = our_field[number_of_cell].x;
					buf.y_pos = our_field[number_of_cell].y;
					list_of_locked_chekers.push_back(buf);
				}
			}
			if (number_of_cell + 9 < 64 && our_field[number_of_cell + 9].state && !our_field[number_of_cell + 9].color_check)
			{
				if (number_of_cell + 18 < 64 && !our_field[number_of_cell + 18].state)
				{
					buf.x_pos = our_field[number_of_cell].x;
					buf.y_pos = our_field[number_of_cell].y;
					list_of_locked_chekers.push_back(buf);
				}
			}
		}
	}
	else
	{
		for (int number_of_cell = 0; number_of_cell < 64; ++number_of_cell)
		{
			if (number_of_cell - 7 >= 0 && our_field[number_of_cell - 7].state && our_field[number_of_cell - 7].color_check)
			{
				if (number_of_cell - 14 >= 0 && !our_field[number_of_cell - 14].state)
				{
					buf.x_pos = our_field[number_of_cell].x;
					buf.y_pos = our_field[number_of_cell].y;
					list_of_locked_chekers.push_back(buf);
				}
			}
			if (number_of_cell - 9 >= 0 && our_field[number_of_cell - 9].state && our_field[number_of_cell - 9].color_check)
			{
				if (number_of_cell - 18 >= 0 && !our_field[number_of_cell - 18].state)
				{
					buf.x_pos = our_field[number_of_cell].x;
					buf.y_pos = our_field[number_of_cell].y;
					list_of_locked_chekers.push_back(buf);
				}
			}
		}
	}
	return list_of_locked_chekers;
}