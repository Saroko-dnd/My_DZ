#pragma once

#include <vector>

typedef struct cell
{
	int x;
	int y;
	bool arrow;
	bool white;
	bool state;
	bool color_check;
	bool simple_check;
	bool alloc;
	bool marked_for_death;
	bool dead_lock;
	bool check_point_here;
}cell;

typedef struct pos_x_y
{
	int x_pos;
	int y_pos;
}pos_x_y;

typedef struct coordinates_and_field
{
	std::vector<pos_x_y> coordinates;
	std::vector<cell> our_field;
}coordinates_and_field;

#define BLUE_PLAYER 1
#define GREEN_PLAYER 2

std::vector<std::vector<cell>> load_field_function();
