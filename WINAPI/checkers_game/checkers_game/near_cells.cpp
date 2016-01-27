
#include "check_field_for_move_cells.h"
#include "field_load.h"
#include <vector>
#include <Windows.h>

std::vector<std::vector<cell>> right_top(std::vector<std::vector<cell>> our_field, int y_position, 
	int x_position, int current_player, bool* check_point)
{
	bool marked = false;
	int y_buf = y_position;
	int x_buf = x_position;
	if (current_player == BLUE_PLAYER)
	{
		while (y_buf < 8 && x_buf > -1)
		{
			++y_buf;
			--x_buf;
			if (y_buf == 8 || x_buf == -1)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf + 1 < 8 && x_buf - 1 > -1 &&
				our_field[y_buf + 1][x_buf - 1].state)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
			{
				if (y_buf + 1 < 8 && x_buf - 1 > -1 && !our_field[y_buf + 1][x_buf - 1].state)
				{
					marked = true;
					break;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		while (y_buf > -1 && x_buf < 8)
		{
			--y_buf;
			++x_buf;
			if (y_buf == -1 || x_buf == 8)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf - 1 > -1 && x_buf +1 < 8 &&
				our_field[y_buf - 1][x_buf + 1].state)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
			{
				if (y_buf - 1 > -1 && x_buf + 1 < 8 && !our_field[y_buf - 1][x_buf + 1].state)
				{
					marked = true;
					break;
				}
			}
		}
	}
	else
	{
		while (y_buf < 8 && x_buf > -1)
		{
			++y_buf;
			--x_buf;
			if (y_buf == 8 || x_buf == -1)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf + 1 < 8 && x_buf - 1 > -1 &&
				our_field[y_buf + 1][x_buf - 1].state)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
			{
				if (y_buf + 1 < 8 && x_buf - 1 > -1 && !our_field[y_buf + 1][x_buf - 1].state)
				{
					marked = true;
					break;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		while (y_buf > -1 && x_buf < 8)
		{
			--y_buf;
			++x_buf;
			if (y_buf == -1 || x_buf == 8)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf - 1 > -1 && x_buf + 1 < 8 &&
				our_field[y_buf - 1][x_buf + 1].state)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
			{
				if (y_buf - 1 > -1 && x_buf + 1 < 8 && !our_field[y_buf - 1][x_buf + 1].state)
				{
					marked = true;
					break;
				}
			}
		}
	}
	if (marked)
	{
		*check_point = true;
		our_field[y_position][x_position].check_point_here = true;
	}
	if (y_position + 1 < 8 && x_position + 1 < 8 && !our_field[y_position + 1][x_position + 1].state)
		our_field = right_top(our_field, y_position + 1, x_position + 1, current_player, check_point);
	return our_field;
}

std::vector<std::vector<cell>> left_top(std::vector<std::vector<cell>> our_field, int y_position,
	int x_position, int current_player, bool* check_point)
{
	bool marked = false;
	int y_buf = y_position;
	int x_buf = x_position;
	if (current_player == BLUE_PLAYER)
	{
		while (y_buf < 8 && x_buf < 8)
		{
			++y_buf;
			++x_buf;
			if (y_buf == 8 || x_buf == 8)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf + 1 < 8 && x_buf + 1 < 8 &&
				our_field[y_buf + 1][x_buf + 1].state)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
			{
				if (y_buf + 1 < 8 && x_buf + 1 < 8 && !our_field[y_buf + 1][x_buf + 1].state)
				{
					marked = true;
					break;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		while (y_buf > -1 && x_buf > -1)
		{
			--y_buf;
			--x_buf;
			if (y_buf == -1 || x_buf == -1)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf - 1 > -1 && x_buf - 1 > -1 &&
				our_field[y_buf - 1][x_buf - 1].state)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
			{
				if (y_buf - 1 > -1 && x_buf - 1 > -1 && !our_field[y_buf - 1][x_buf - 1].state)
				{
					marked = true;
					break;
				}
			}
		}
	}
	else
	{
		while (y_buf < 8 && x_buf < 8)
		{
			++y_buf;
			++x_buf;
			if (y_buf == 8 || x_buf == 8)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf + 1 < 8 && x_buf + 1 < 8 &&
				our_field[y_buf + 1][x_buf + 1].state)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
			{
				if (y_buf + 1 < 8 && x_buf + 1 < 8 && !our_field[y_buf + 1][x_buf + 1].state)
				{
					marked = true;
					break;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		while (y_buf > -1 && x_buf > -1)
		{
			--y_buf;
			--x_buf;
			if (y_buf == -1 || x_buf == -1)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf - 1 > -1 && x_buf - 1 > -1 &&
				our_field[y_buf - 1][x_buf - 1].state)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
			{
				if (y_buf - 1 > -1 && x_buf - 1 > -1 && !our_field[y_buf - 1][x_buf - 1].state)
				{
					marked = true;
					break;
				}
			}
		}
	}
	if (marked)
	{
		*check_point = true;
		our_field[y_position][x_position].check_point_here = true;
	}
	if (y_position + 1 < 8 && x_position - 1 > -1 && !our_field[y_position + 1][x_position - 1].state)
		our_field = left_top(our_field, y_position + 1, x_position - 1, current_player, check_point);
	return our_field;
}

std::vector<std::vector<cell>> right_down(std::vector<std::vector<cell>> our_field, int y_position,
	int x_position, int current_player, bool* check_point)
{
	bool marked = false;
	int y_buf = y_position;
	int x_buf = x_position;
	if (current_player == BLUE_PLAYER)
	{
		while (y_buf < 8 && x_buf < 8)
		{
			++y_buf;
			++x_buf;
			if (y_buf == 8 || x_buf == 8)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf + 1 < 8 && x_buf + 1 < 8 &&
				our_field[y_buf + 1][x_buf + 1].state)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
			{
				if (y_buf + 1 < 8 && x_buf + 1 < 8 && !our_field[y_buf + 1][x_buf + 1].state)
				{
					marked = true;
					break;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		while (y_buf > -1 && x_buf > -1)
		{
			--y_buf;
			--x_buf;
			if (y_buf == -1 || x_buf == -1)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf - 1 > -1 && x_buf - 1 > -1 &&
				our_field[y_buf - 1][x_buf - 1].state)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
			{
				if (y_buf - 1 > -1 && x_buf - 1 > -1 && !our_field[y_buf - 1][x_buf - 1].state)
				{
					marked = true;
					break;
				}
			}
		}
	}
	else
	{
		while (y_buf < 8 && x_buf < 8)
		{
			++y_buf;
			++x_buf;
			if (y_buf == 8 || x_buf == 8)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf + 1 < 8 && x_buf + 1 < 8 &&
				our_field[y_buf + 1][x_buf + 1].state)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
			{
				if (y_buf + 1 < 8 && x_buf + 1 < 8 && !our_field[y_buf + 1][x_buf + 1].state)
				{
					marked = true;
					break;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		while (y_buf > -1 && x_buf > -1)
		{
			--y_buf;
			--x_buf;
			if (y_buf == -1 || x_buf == -1)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf - 1 > -1 && x_buf - 1 > -1 &&
				our_field[y_buf - 1][x_buf - 1].state)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
			{
				if (y_buf - 1 > -1 && x_buf - 1 > -1 && !our_field[y_buf - 1][x_buf - 1].state)
				{
					marked = true;
					break;
				}
			}
		}
	}
	if (marked)
	{
		*check_point = true;
		our_field[y_position][x_position].check_point_here = true;
	}
	if (y_position - 1 > -1 && x_position + 1 < 8 && !our_field[y_position - 1][x_position + 1].state)
		our_field = right_down(our_field, y_position - 1, x_position + 1, current_player, check_point);
	return our_field;
}

std::vector<std::vector<cell>> left_down(std::vector<std::vector<cell>> our_field, int y_position,
	int x_position, int current_player,bool* check_point)
{
	bool marked = false;
	int y_buf = y_position;
	int x_buf = x_position;
	if (current_player == BLUE_PLAYER)
	{
		while (y_buf < 8 && x_buf > -1)
		{
			++y_buf;
			--x_buf;
			if (y_buf == 8 || x_buf == -1)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf + 1 < 8 && x_buf - 1 > -1 &&
				our_field[y_buf + 1][x_buf - 1].state)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
			{
				if (y_buf + 1 < 8 && x_buf - 1 > -1 && !our_field[y_buf + 1][x_buf - 1].state)
				{
					marked = true;
					break;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		while (y_buf > -1 && x_buf < 8)
		{
			--y_buf;
			++x_buf;
			if (y_buf == -1 || x_buf == 8)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf - 1 > -1 && x_buf + 1 < 8 &&
				our_field[y_buf - 1][x_buf + 1].state)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
			{
				if (y_buf - 1 > -1 && x_buf + 1 < 8 && !our_field[y_buf - 1][x_buf + 1].state)
				{
					marked = true;
					break;
				}
			}
		}
	}
	else
	{
		while (y_buf < 8 && x_buf > -1)
		{
			++y_buf;
			--x_buf;
			if (y_buf == 8 || x_buf == -1)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf + 1 < 8 && x_buf - 1 > -1 &&
				our_field[y_buf + 1][x_buf - 1].state)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
			{
				if (y_buf + 1 < 8 && x_buf - 1 > -1 && !our_field[y_buf + 1][x_buf - 1].state)
				{
					marked = true;
					break;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		while (y_buf > -1 && x_buf < 8)
		{
			--y_buf;
			++x_buf;
			if (y_buf == -1 || x_buf == 8)
				break;
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				break;
			if (our_field[y_buf][x_buf].state && y_buf - 1 > -1 && x_buf + 1 < 8 &&
				our_field[y_buf - 1][x_buf + 1].state)
				break;
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
			{
				if (y_buf - 1 > -1 && x_buf + 1 < 8 && !our_field[y_buf - 1][x_buf + 1].state)
				{
					marked = true;
					break;
				}
			}
		}
	}

	if (marked)
	{
		*check_point = true;
		our_field[y_position][x_position].check_point_here = true;
	}
	if (y_position - 1 > -1 && x_position - 1 > -1 && !our_field[y_position - 1][x_position - 1].state)
		our_field = left_down(our_field, y_position - 1, x_position - 1, current_player, check_point);

	return our_field;
}

std::vector<std::vector<cell>> check_all_roads(std::vector<std::vector<cell>> our_field, int y_position,
	int x_position, int current_player, bool* check_points)
{
		for (int counter = 0; counter < 8; ++counter)
		{
			for (int counter_2 = 0; counter_2 < 8; ++counter_2)
			{
				if (our_field[counter][counter_2].marked_for_death)
				{
					int x_counter = counter_2, y_counter = counter;
					if (counter > y_position)
					{
						if (counter_2 > x_position)
						{
							our_field = right_top(our_field, counter + 1, counter_2 + 1, current_player, check_points);
						}
						else
						{
							our_field = left_top(our_field, counter + 1, counter_2 - 1, current_player, check_points);
						}
					}
					else
					{
						if (counter_2 > x_position)
						{
							our_field = right_down(our_field, counter - 1, counter_2 + 1, current_player, check_points);
						}
						else
						{
							our_field = left_down(our_field, counter - 1, counter_2 - 1, current_player, check_points);
						}

					}
				}
			}
		}

	return our_field;
}

std::vector<std::vector<cell>> create_king_arrows(std::vector<std::vector<cell>> our_field, int y_position,
	int x_position, int current_player, bool* marked)
{
	bool attack_line = false;
	int y_buf = y_position, x_buf = x_position;
	if (current_player == BLUE_PLAYER)
	{
		while (y_buf <= 7 && x_buf <= 7)
		{
			++y_buf;
			++x_buf;
			if (y_buf > 7 || x_buf > 7)
				break;
			if (our_field[y_buf][x_buf].state && attack_line)
			{
				break;
			}
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
			{
				if (y_buf < 7 && x_buf < 7 && !our_field[y_buf + 1][x_buf + 1].state)
				{
					our_field[y_buf + 1][x_buf + 1].dead_lock = true;
					our_field[y_buf + 1][x_buf + 1].arrow = true;
					our_field[y_buf][x_buf].marked_for_death = true;
					attack_line = true;
					*marked = true;
				}
				else
					break;
			}
			else if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				break;
			else if (!attack_line && !our_field[y_buf][x_buf].state)
				our_field[y_buf][x_buf].arrow = true;
			else
			{
				if (attack_line && !our_field[y_buf][x_buf].state)
				{
					our_field[y_buf][x_buf].dead_lock = true;
					our_field[y_buf][x_buf].arrow = true;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		attack_line = false;
		while (y_buf <= 7 && x_buf >= 0)
		{
			++y_buf;
			--x_buf;
			if (y_buf > 7 || x_buf < 0)
				break;
			if (our_field[y_buf][x_buf].state && attack_line)
			{
				break;
			}
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
			{
				if (y_buf < 7 && x_buf > 0 && !our_field[y_buf + 1][x_buf - 1].state)
				{
					our_field[y_buf + 1][x_buf - 1].dead_lock = true;
					our_field[y_buf + 1][x_buf - 1].arrow = true;
					our_field[y_buf][x_buf].marked_for_death = true;
					attack_line = true;
					*marked = true;
				}
				else
					break;
			}
			else if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				break;
			else if (!attack_line && !our_field[y_buf][x_buf].state)
				our_field[y_buf][x_buf].arrow = true;
			else
			{
				if (attack_line && !our_field[y_buf][x_buf].state)
				{
					our_field[y_buf][x_buf].dead_lock = true;
					our_field[y_buf][x_buf].arrow = true;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		attack_line = false;
		while (y_buf >= 0 && x_buf <= 7)
		{
			--y_buf;
			++x_buf;
			if (y_buf < 0 || x_buf > 7)
				break;
			if (our_field[y_buf][x_buf].state && attack_line)
			{
				break;
			}
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
			{
				if (y_buf > 0 && x_buf < 7 && !our_field[y_buf - 1][x_buf + 1].state)
				{
					our_field[y_buf - 1][x_buf + 1].dead_lock = true;
					our_field[y_buf - 1][x_buf + 1].arrow = true;
					our_field[y_buf][x_buf].marked_for_death = true;
					attack_line = true;
					*marked = true;
				}
				else
					break;
			}
			else if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				break;
			else if (!attack_line && !our_field[y_buf][x_buf].state)
				our_field[y_buf][x_buf].arrow = true;
			else
			{
				if (attack_line && !our_field[y_buf][x_buf].state)
				{
					our_field[y_buf][x_buf].dead_lock = true;
					our_field[y_buf][x_buf].arrow = true;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		attack_line = false;
		while (y_buf >= 0 && x_buf >= 0)
		{
			--y_buf;
			--x_buf;
			if (y_buf < 0 || x_buf < 0)
				break;
			if (our_field[y_buf][x_buf].state && attack_line)
			{
				break;
			}
			if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
			{
				if (y_buf > 0 && x_buf > 0 && !our_field[y_buf - 1][x_buf - 1].state)
				{
					our_field[y_buf - 1][x_buf - 1].dead_lock = true;
					our_field[y_buf - 1][x_buf - 1].arrow = true;
					our_field[y_buf][x_buf].marked_for_death = true;
					attack_line = true;
					*marked = true;
				}
				else
					break;
			}
			else if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				break;
			else if (!attack_line && !our_field[y_buf][x_buf].state)
				our_field[y_buf][x_buf].arrow = true;
			else
			{
				if (attack_line && !our_field[y_buf][x_buf].state)
				{
					our_field[y_buf][x_buf].dead_lock = true;
					our_field[y_buf][x_buf].arrow = true;
				}
			}
		}
	}
	else//GREEN PLAYER
	{
		while (y_buf <= 7 && x_buf <= 7)
		{
			++y_buf;
			++x_buf;
			if (y_buf > 7 || x_buf > 7)
				break;
			if (our_field[y_buf][x_buf].state && attack_line)
			{
				break;
			}
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
			{
				if (y_buf < 7 && x_buf < 7 && !our_field[y_buf + 1][x_buf + 1].state)
				{
					our_field[y_buf + 1][x_buf + 1].dead_lock = true;
					our_field[y_buf + 1][x_buf + 1].arrow = true;
					our_field[y_buf][x_buf].marked_for_death = true;
					attack_line = true;
					*marked = true;
				}
				else
					break;
			}
			else if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				break;
			else if (!attack_line && !our_field[y_buf][x_buf].state)
				our_field[y_buf][x_buf].arrow = true;
			else
			{
				if (attack_line && !our_field[y_buf][x_buf].state)
				{
					our_field[y_buf][x_buf].dead_lock = true;
					our_field[y_buf][x_buf].arrow = true;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		attack_line = false;
		while (y_buf <= 7 && x_buf >= 0)
		{
			++y_buf;
			--x_buf;
			if (y_buf > 7 || x_buf < 0)
				break;
			if (our_field[y_buf][x_buf].state && attack_line)
			{
				break;
			}
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
			{
				if (y_buf < 7 && x_buf > 0 && !our_field[y_buf + 1][x_buf - 1].state)
				{
					our_field[y_buf + 1][x_buf - 1].dead_lock = true;
					our_field[y_buf + 1][x_buf - 1].arrow = true;
					our_field[y_buf][x_buf].marked_for_death = true;
					attack_line = true;
					*marked = true;
				}
				else
					break;
			}
			else if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				break;
			else if (!attack_line && !our_field[y_buf][x_buf].state)
				our_field[y_buf][x_buf].arrow = true;
			else
			{
				if (attack_line && !our_field[y_buf][x_buf].state)
				{
					our_field[y_buf][x_buf].dead_lock = true;
					our_field[y_buf][x_buf].arrow = true;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		attack_line = false;
		while (y_buf >= 0 && x_buf <= 7)
		{
			--y_buf;
			++x_buf;
			if (y_buf < 0 || x_buf > 7)
				break;
			if (our_field[y_buf][x_buf].state && attack_line)
			{
				break;
			}
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
			{
				if (y_buf > 0 && x_buf < 7 && !our_field[y_buf - 1][x_buf + 1].state)
				{
					our_field[y_buf - 1][x_buf + 1].dead_lock = true;
					our_field[y_buf - 1][x_buf + 1].arrow = true;
					our_field[y_buf][x_buf].marked_for_death = true;
					attack_line = true;
					*marked = true;
				}
				else
					break;
			}
			else if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				break;
			else if (!attack_line && !our_field[y_buf][x_buf].state)
				our_field[y_buf][x_buf].arrow = true;
			else
			{
				if (attack_line && !our_field[y_buf][x_buf].state)
				{
					our_field[y_buf][x_buf].dead_lock = true;
					our_field[y_buf][x_buf].arrow = true;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		attack_line = false;
		while (y_buf >= 0 && x_buf >= 0)
		{
			--y_buf;
			--x_buf;
			if (y_buf < 0 || x_buf < 0)
				break;
			if (our_field[y_buf][x_buf].state && attack_line)
			{
				break;
			}
			if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
			{
				if (y_buf > 0 && x_buf > 0 && !our_field[y_buf - 1][x_buf - 1].state)
				{
					our_field[y_buf - 1][x_buf - 1].dead_lock = true;
					our_field[y_buf - 1][x_buf - 1].arrow = true;
					our_field[y_buf][x_buf].marked_for_death = true;
					attack_line = true;
					*marked = true;
				}
				else
					break;
			}
			else if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				break;
			else if (!attack_line && !our_field[y_buf][x_buf].state)
				our_field[y_buf][x_buf].arrow = true;
			else
			{
				if (attack_line && !our_field[y_buf][x_buf].state)
				{
					our_field[y_buf][x_buf].dead_lock = true;
					our_field[y_buf][x_buf].arrow = true;
				}
			}
		}
	}

	return our_field;
}

bool check_king_attacks(std::vector<std::vector<cell>> our_field, int y_position, int x_position , 
	int current_player)
{
	int y_buf = y_position, x_buf = x_position;
	if (current_player == BLUE_PLAYER)
	{
		if (y_position < 6 && x_position < 6)
		{
			while (y_buf < 6 && x_buf < 6)
			{
				++y_buf;
				++x_buf;
				if (our_field[y_buf][x_buf].state && our_field[y_buf + 1][x_buf + 1].state)
					break;
				if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
					break;
				if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				{
					if (!our_field[y_buf + 1][x_buf + 1].state)
						return true;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		if (y_position < 6 && x_position > 1)
		{
			while (y_buf < 6 && x_buf > 1)
			{
				++y_buf;
				--x_buf;
				if (our_field[y_buf][x_buf].state && our_field[y_buf + 1][x_buf - 1].state)
					break;
				if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
					break;
				if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				{
					if (!our_field[y_buf + 1][x_buf - 1].state)
						return true;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		if (y_position > 1 && x_position < 6)
		{
			while (y_buf > 1 && x_buf < 6)
			{
				--y_buf;
				++x_buf;
				if (our_field[y_buf][x_buf].state && our_field[y_buf - 1][x_buf + 1].state)
					break;
				if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
					break;
				if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				{
					if (!our_field[y_buf - 1][x_buf + 1].state)
						return true;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		if (y_position > 1 && x_position > 1)
		{
			while (y_buf > 1 && x_buf > 1)
			{
				--y_buf;
				--x_buf;
				if (our_field[y_buf][x_buf].state && our_field[y_buf - 1][x_buf - 1].state)
					break;
				if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
					break;
				if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
				{
					if (!our_field[y_buf - 1][x_buf - 1].state)
						return true;
				}
			}
		}
	}
	else
	{
		if (y_position < 6 && x_position < 6)
		{
			while (y_buf < 6 && x_buf < 6)
			{
				++y_buf;
				++x_buf;
				if (our_field[y_buf][x_buf].state && our_field[y_buf + 1][x_buf + 1].state)
					break;
				if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
					break;
				if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				{
					if (!our_field[y_buf + 1][x_buf + 1].state)
						return true;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		if (y_position < 6 && x_position > 1)
		{
			while (y_buf < 6 && x_buf > 1)
			{
				++y_buf;
				--x_buf;
				if (our_field[y_buf][x_buf].state && our_field[y_buf + 1][x_buf - 1].state)
					break;
				if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
					break;
				if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				{
					if (!our_field[y_buf + 1][x_buf - 1].state)
						return true;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		if (y_position > 1 && x_position < 6)
		{
			while (y_buf > 1 && x_buf < 6)
			{
				--y_buf;
				++x_buf;
				if (our_field[y_buf][x_buf].state && our_field[y_buf - 1][x_buf + 1].state)
					break;
				if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
					break;
				if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				{
					if (!our_field[y_buf - 1][x_buf + 1].state)
						return true;
				}
			}
		}
		y_buf = y_position;
		x_buf = x_position;
		if (y_position > 1 && x_position > 1)
		{
			while (y_buf > 1 && x_buf > 1)
			{
				--y_buf;
				--x_buf;
				if (our_field[y_buf][x_buf].state && our_field[y_buf - 1][x_buf - 1].state)
					break;
				if (our_field[y_buf][x_buf].state && !our_field[y_buf][x_buf].color_check)
					break;
				if (our_field[y_buf][x_buf].state && our_field[y_buf][x_buf].color_check)
				{
					if (!our_field[y_buf - 1][x_buf - 1].state)
						return true;
				}
			}
		}
	}
	return false;
}

std::vector<std::vector<cell>> check_near_cells(std::vector<std::vector<cell>> our_field,int y_position, 
	int x_position, int current_player, bool all_stuff,bool* attacks_here)
{
	std::vector<pos_x_y> buf;
	bool marked = false,check_points = false;
	bool attacks_check = false,checker_alloc_attacks = false;
	for (int counter = 0; counter < 8; ++counter)
	{
		for (int counter_2 = 0; counter_2 < 8; ++counter_2)
		{
			our_field[counter][counter_2].dead_lock = false;
			our_field[counter][counter_2].arrow = false;
			our_field[counter][counter_2].marked_for_death = false;
			our_field[counter][counter_2].check_point_here = false;
		}
	}
	for (int counter = 0; counter < 8; ++counter)
	{
		if (current_player == BLUE_PLAYER)
		for (int counter_2 = 0; counter_2 < 8; ++counter_2)
		{
			if (our_field[counter][counter_2].simple_check && 
				our_field[counter][counter_2].state && our_field[counter][counter_2].color_check)//SIMPLE BLUE
			{
				if (counter < 7 && counter_2 > 0)
				{
					if (our_field[counter + 1][counter_2 - 1].state &&
						!our_field[counter + 1][counter_2 - 1].color_check)
					{
						if (counter + 2 < 8 && counter_2 - 2 > -1 &&
							!our_field[counter + 2][counter_2 - 2].state)
						{
							attacks_check = true;
							if (counter == y_position && counter_2 == x_position)
							{
								*attacks_here = true;
								checker_alloc_attacks = true;
							}
							else if (!checker_alloc_attacks)
								*attacks_here = false;
						}
					}
				}
				if (counter < 7 && counter_2 < 7)
				{
					if (our_field[counter + 1][counter_2 + 1].state &&
						!our_field[counter + 1][counter_2 + 1].color_check)
					{
						if (counter + 2 < 8 && counter_2 + 2 < 8 &&
							!our_field[counter + 2][counter_2 + 2].state)
						{
							attacks_check = true;
							if (counter == y_position && counter_2 == x_position)
							{
								*attacks_here = true;
								checker_alloc_attacks = true;
							}
							else if (!checker_alloc_attacks)
								*attacks_here = false;
						}
					}
				}
				if (counter > 1 && counter_2 > 1)
				{
					if (our_field[counter - 1][counter_2 - 1].state &&
						!our_field[counter - 1][counter_2 - 1].color_check)
					{
						if (!our_field[counter - 2][counter_2 - 2].state)
						{
							attacks_check = true;
							if (counter == y_position && counter_2 == x_position)
							{
								*attacks_here = true;
								checker_alloc_attacks = true;
							}
							else if (!checker_alloc_attacks)
								*attacks_here = false;
						}
					}
				}
				if (counter > 1 && counter_2 < 6)
				{
					if (our_field[counter - 1][counter_2 + 1].state &&
						!our_field[counter - 1][counter_2 + 1].color_check)
					{
						if (!our_field[counter - 2][counter_2 + 2].state)
						{
							attacks_check = true;
							if (counter == y_position && counter_2 == x_position)
							{
								*attacks_here = true;
								checker_alloc_attacks = true;
							}
							else if (!checker_alloc_attacks)
								*attacks_here = false;
						}
					}
				}
			}
			else//KING BLUE
			{
				if (!our_field[counter][counter_2].simple_check &&
					our_field[counter][counter_2].state && our_field[counter][counter_2].color_check)
				{
					if (check_king_attacks(our_field, counter, counter_2, current_player))
					{
						attacks_check = true;
						if (counter == y_position && counter_2 == x_position)
						{
							*attacks_here = true;
							checker_alloc_attacks = true;
						}
						else if (!checker_alloc_attacks)
							*attacks_here = false;
					}
				}
			}
		}
		else
		for (int counter_2 = 0; counter_2 < 8; ++counter_2)
		{
			if (our_field[counter][counter_2].simple_check && our_field[counter][counter_2].state
				&& !our_field[counter][counter_2].color_check)//SIMPLE GREEN
			{
				if (counter > 0 && counter_2 > 0)
				{
					if (our_field[counter - 1][counter_2 - 1].state &&
						our_field[counter - 1][counter_2 - 1].color_check)
					{
						if (counter - 2 > -1 && counter_2 - 2 > -1 &&
							!our_field[counter - 2][counter_2 - 2].state)
						{
							attacks_check = true;
							if (counter == y_position && counter_2 == x_position)
							{
								*attacks_here = true;
								checker_alloc_attacks = true;
							}
							else if (!checker_alloc_attacks)
								*attacks_here = false;
						}
					}
				}
				if (counter > 0 && counter_2 < 7)
				{
					if (our_field[counter - 1][counter_2 + 1].state &&
						our_field[counter - 1][counter_2 + 1].color_check)
					{
						if (counter - 2 > -1 && counter_2 + 2 < 8 &&
							!our_field[counter - 2][counter_2 + 2].state)
						{
							attacks_check = true;
							if (counter == y_position && counter_2 == x_position)
							{
								*attacks_here = true;
								checker_alloc_attacks = true;
							}
							else if (!checker_alloc_attacks)
								*attacks_here = false;
						}
					}
				}
				if (counter < 6 && counter_2 > 1)
				{
					if (our_field[counter + 1][counter_2 - 1].state &&
						our_field[counter + 1][counter_2 - 1].color_check)
					{
						if (!our_field[counter + 2][counter_2 - 2].state)
						{
							attacks_check = true;
							if (counter == y_position && counter_2 == x_position)
							{
								*attacks_here = true;
								checker_alloc_attacks = true;
							}
							else if (!checker_alloc_attacks)
								*attacks_here = false;
						}
					}
				}
				if (counter < 6 && counter_2 < 6)
				{
					if (our_field[counter + 1][counter_2 + 1].state &&
						our_field[counter + 1][counter_2 + 1].color_check)
					{
						if (!our_field[counter + 2][counter_2 + 2].state)
						{
							attacks_check = true;
							if (counter == y_position && counter_2 == x_position)
							{
								*attacks_here = true;
								checker_alloc_attacks = true;
							}
							else if (!checker_alloc_attacks)
								*attacks_here = false;
						}
					}
				}
			}
			else//KING GREEN
			{
				if (!our_field[counter][counter_2].simple_check &&
					our_field[counter][counter_2].state && !our_field[counter][counter_2].color_check)
				{
					if (check_king_attacks(our_field, counter, counter_2, current_player))
					{
						attacks_check = true;
						if (counter == y_position && counter_2 == x_position)
						{
							*attacks_here = true;
							checker_alloc_attacks = true;
						}
						else if (!checker_alloc_attacks)
							*attacks_here = false;
					}
				}
			}
		}
	}
	if (!attacks_check || (attacks_check && checker_alloc_attacks))
	{
		if (current_player == BLUE_PLAYER)
		{
			if (our_field[y_position][x_position].simple_check)//SIMPLE BLUE
			{
				if (y_position < 7 && x_position > 0)
				{
					if (!our_field[y_position + 1][x_position - 1].state)
						our_field[y_position + 1][x_position - 1].arrow = true;
					else if (our_field[y_position + 1][x_position - 1].state &&
						!our_field[y_position + 1][x_position - 1].color_check)
					{
						if (y_position + 2 < 8 && x_position - 2 > -1 &&
							!our_field[y_position + 2][x_position - 2].state)
						{
							marked = true;
							our_field[y_position + 2][x_position - 2].dead_lock = true;
							our_field[y_position + 2][x_position - 2].arrow = true;
							our_field[y_position + 1][x_position - 1].marked_for_death = true;
						}
					}
				}
				if (y_position < 7 && x_position < 7)
				{
					if (!our_field[y_position + 1][x_position + 1].state)
						our_field[y_position + 1][x_position + 1].arrow = true;
					else if (our_field[y_position + 1][x_position + 1].state &&
						!our_field[y_position + 1][x_position + 1].color_check)
					{
						if (y_position + 2 < 8 && x_position + 2 < 8 &&
							!our_field[y_position + 2][x_position + 2].state)
						{
							marked = true;
							our_field[y_position + 2][x_position + 2].dead_lock = true;
							our_field[y_position + 2][x_position + 2].arrow = true;
							our_field[y_position + 1][x_position + 1].marked_for_death = true;
						}
					}
				}
				if (y_position > 1 && x_position > 1)
				{
					if (our_field[y_position - 1][x_position - 1].state &&
						!our_field[y_position - 1][x_position - 1].color_check)
					{
						if (!our_field[y_position - 2][x_position - 2].state)
						{
							marked = true;
							our_field[y_position - 2][x_position - 2].dead_lock = true;
							our_field[y_position - 2][x_position - 2].arrow = true;
							our_field[y_position - 1][x_position - 1].marked_for_death = true;
						}
					}
				}
				if (y_position > 1 && x_position < 6)
				{
					if (our_field[y_position - 1][x_position + 1].state &&
						!our_field[y_position - 1][x_position + 1].color_check)
					{
						if (!our_field[y_position - 2][x_position + 2].state)
						{
							marked = true;
							our_field[y_position - 2][x_position + 2].dead_lock = true;
							our_field[y_position - 2][x_position + 2].arrow = true;
							our_field[y_position - 1][x_position + 1].marked_for_death = true;
						}
					}
				}
			}
			else//KING BLUE
			{
				our_field = create_king_arrows(our_field, y_position, x_position, current_player,&marked);
				if (marked)
					our_field = check_all_roads(our_field, y_position, x_position, current_player, &check_points);
			}
		}
		else//GREEN PLAYER
		{
			if (our_field[y_position][x_position].simple_check)//SIMPLE GREEN
			{
				if (y_position > 0 && x_position > 0)
				{
					if (!our_field[y_position - 1][x_position - 1].state)
						our_field[y_position - 1][x_position - 1].arrow = true;
					else if (our_field[y_position - 1][x_position - 1].state &&
						our_field[y_position - 1][x_position - 1].color_check)
					{
						if (y_position - 2 > -1 && x_position - 2 > -1 &&
							!our_field[y_position - 2][x_position - 2].state)
						{
							marked = true;
							our_field[y_position - 2][x_position - 2].dead_lock = true;
							our_field[y_position - 2][x_position - 2].arrow = true;
							our_field[y_position - 1][x_position - 1].marked_for_death = true;
						}
					}
				}
				if (y_position > 0 && x_position < 7)
				{
					if (!our_field[y_position - 1][x_position + 1].state)
						our_field[y_position - 1][x_position + 1].arrow = true;
					else if (our_field[y_position - 1][x_position + 1].state &&
						our_field[y_position - 1][x_position + 1].color_check)
					{
						if (y_position - 2 > -1 && x_position + 2 < 8 &&
							!our_field[y_position - 2][x_position + 2].state)
						{
							marked = true;
							our_field[y_position - 2][x_position + 2].dead_lock = true;
							our_field[y_position - 2][x_position + 2].arrow = true;
							our_field[y_position - 1][x_position + 1].marked_for_death = true;
						}
					}
				}
				if (y_position < 6 && x_position > 1)
				{
					if (our_field[y_position + 1][x_position - 1].state &&
						our_field[y_position + 1][x_position - 1].color_check)
					{
						if (!our_field[y_position + 2][x_position - 2].state)
						{
							marked = true;
							our_field[y_position + 2][x_position - 2].dead_lock = true;
							our_field[y_position + 2][x_position - 2].arrow = true;
							our_field[y_position + 1][x_position - 1].marked_for_death = true;
						}
					}
				}
				if (y_position < 6 && x_position < 6)
				{
					if (our_field[y_position + 1][x_position + 1].state &&
						our_field[y_position + 1][x_position + 1].color_check)
					{
						if (!our_field[y_position + 2][x_position + 2].state)
						{
							marked = true;
							our_field[y_position + 2][x_position + 2].dead_lock = true;
							our_field[y_position + 2][x_position + 2].arrow = true;
							our_field[y_position + 1][x_position + 1].marked_for_death = true;
						}
					}
				}
			}
			else//KING GREEN
			{
				our_field = create_king_arrows(our_field, y_position, x_position, current_player, &marked);
				if (marked)
					our_field = check_all_roads(our_field, y_position, x_position, current_player, &check_points);
			}
		}
	}
	if (marked && check_points)
	{
		for (int counter = 0; counter < 8; ++counter)
		{
			for (int counter_2 = 0; counter_2 < 8; ++counter_2)
			{
				if (our_field[counter][counter_2].check_point_here == false)
					our_field[counter][counter_2].arrow = false;
			}
		}
	}
	else if (marked)
	{
		for (int counter = 0; counter < 8; ++counter)
		{
			for (int counter_2 = 0; counter_2 < 8; ++counter_2)
			{
				if (our_field[counter][counter_2].dead_lock == false)
					our_field[counter][counter_2].arrow = false;
			}
		}
	}
	if (!all_stuff && !checker_alloc_attacks)
	{
		for (int counter = 0; counter < 8; ++counter)
		{
			for (int counter_2 = 0; counter_2 < 8; ++counter_2)
			{
				our_field[counter][counter_2].dead_lock = false;
				our_field[counter][counter_2].arrow = false;
				our_field[counter][counter_2].marked_for_death = false;
			}
		}
	}
	if (!checker_alloc_attacks)
		*attacks_here = false;
	return our_field;
}