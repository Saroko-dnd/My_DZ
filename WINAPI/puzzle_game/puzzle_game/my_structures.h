#pragma once

#include <Windows.h>

typedef struct current_hwnd
{
	HWND cur_hwnd;
	int right_number;
	int left_number;
	int top_number;
	int bottom_number;
	int check_pos;
}current_hwnd;

typedef struct coordinates
{
	int x;
	int y;
}coordinates;