#include "field_load.h"
#include <Windows.h>
#include <string>

std::vector<std::vector<cell>> load_field_function()
{
	std::wstring check;
	cell buf_cell;
	std::vector<cell> cell_vector_buf;
	std::vector<std::vector<cell>> buf_field;
	buf_cell.arrow = false;
	buf_cell.alloc = false;
	buf_cell.simple_check = true;
	buf_cell.marked_for_death = false;
	buf_cell.check_point_here = false;
	buf_cell.dead_lock = false;
	int x_start = 100, y_start = 750;

		for (int counter = 0; counter < 8; ++counter)
		{
			for (int counter_2 = 0; counter_2 < 8; ++counter_2)
			{
				if ((counter + counter_2) % 2 == 0)
				{
					buf_cell.white = false;
					if (counter <= 2)
					{
						buf_cell.state = true;
						buf_cell.color_check = true;
					}
					else
					{
						if (counter >= 5)
						{
							buf_cell.state = true;
							buf_cell.color_check = false;
						}
						else
							buf_cell.state = false;
					}

				}
				else
				{
					buf_cell.white = true;
					buf_cell.state = false;
				}
				buf_cell.x = x_start;
				buf_cell.y = y_start;
				cell_vector_buf.push_back(buf_cell);
				x_start += 50;
			}
			y_start -= 50;
			x_start = 100;
			buf_field.push_back(cell_vector_buf);
			cell_vector_buf.resize(0);
		}
		//check = std::to_wstring(y_start);
		//MessageBox(NULL, check.c_str(), TEXT("ОТВЕТ"),
		//MB_OK | MB_ICONWARNING);
	return buf_field;
}