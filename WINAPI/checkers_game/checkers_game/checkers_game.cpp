// checkers_game.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "checkers_game.h"
#include <vector>
#include "field_load.h"
#include "check_field_for_move_cells.h"
#include <iostream>
#include <string>
#include <cmath>

using namespace std;

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name

// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);

//pictures
HBITMAP black_bitmap = NULL, blue_checker_bitmap = NULL, green_checker_bitmap = NULL, alloc_cell_bitmap = NULL,
king_blue_bitmap = NULL, king_green_bitmap = NULL,alloc_blue_bitmap = NULL, alloc_green_bitmap = NULL,
alloc_blue_king_bitmap = NULL, alloc_green_king_bitmap = NULL, marked_blue_bitmap = NULL,
marked_blue_king_bitmap = NULL, marked_green_bitmap = NULL, marked_green_king_bitmap = NULL;

HWND new_game_button;
coordinates_and_field main_buf;
vector<vector<cell>> game_field;
vector<pos_x_y> x_y_positions;
vector<pos_x_y> accesable_positions;//для хранения положения шашек, которые должны бить.
wstring bufer, bufer_2;
int x_mouse, y_mouse, vector_search_main, vector_search_second,vec_1_copy,vec_2_copy;
bool checker_is_allocated = false;
int alloc_position = -1;
int move_position = -1;
bool test = false,forbidden_change_player = false,act = false,attack_check_alloc = false,help_attacks = false;
int current_player = BLUE_PLAYER;
int r_but_y_copy = -1, r_but_x_copy = -1;
bool repaint_swi = false;
int repaint_swi_2 = true;
int move_dif = 0;
bool alloc_is_simple = true;

int APIENTRY _tWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPTSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

 	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_CHECKERS_GAME, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_CHECKERS_GAME));

	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int) msg.wParam;
}



//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= hInstance;
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_CHECKERS_GAME));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_CHECKERS_GAME);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   HWND hWnd;

   hInst = hInstance; // Store instance handle in our global variable

   game_field = load_field_function();

   hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      5, 5, 650, 900, NULL, NULL, hInstance, NULL);
   new_game_button = CreateWindow(TEXT("BUTTON"), TEXT("Начать новую игру"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   150, 200, 300, 50, hWnd, NULL, hInstance, NULL);

   //loading pictures
   black_bitmap = (HBITMAP)LoadImage(hInst, TEXT("black_cell.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   blue_checker_bitmap = (HBITMAP)LoadImage(hInst, TEXT("blue_checker.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   green_checker_bitmap = (HBITMAP)LoadImage(hInst, TEXT("green_checker.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   alloc_cell_bitmap = (HBITMAP)LoadImage(hInst, TEXT("arrow_down_black.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   king_blue_bitmap = (HBITMAP)LoadImage(hInst, TEXT("blue_checker_king.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   king_green_bitmap = (HBITMAP)LoadImage(hInst, TEXT("green_checker_king.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   alloc_blue_bitmap = (HBITMAP)LoadImage(hInst, TEXT("blue_checker_alloc.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   alloc_green_bitmap = (HBITMAP)LoadImage(hInst, TEXT("green_checker_alloc.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   marked_blue_bitmap = (HBITMAP)LoadImage(hInst, TEXT("blue_checker_marked.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   marked_blue_king_bitmap = (HBITMAP)LoadImage(hInst, TEXT("blue_checker_king_marked.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   marked_green_bitmap = (HBITMAP)LoadImage(hInst, TEXT("green_checker_marked.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   marked_green_king_bitmap = (HBITMAP)LoadImage(hInst, TEXT("green_checker_king_marked.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   alloc_blue_king_bitmap = (HBITMAP)LoadImage(hInst, TEXT("blue_checker_king_alloc.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
   alloc_green_king_bitmap = (HBITMAP)LoadImage(hInst, TEXT("green_checker_king_alloc.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_COMMAND	- process the application menu
//  WM_PAINT	- Paint the main window
//  WM_DESTROY	- post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int check_counter = 0;
	int counter_for_mouse_pos_check = 0;
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;
	HGDIOBJ oldBitmap;
	BITMAP 	bitmap;
	HDC hdcMem;
	int high_left_x = 100, high_left_y = 750, low_right_x = 150, low_right_y = 800;//for painting rect
	cell main_cell_buf = game_field[vector_search_main][vector_search_second];

	switch (message)
	{
	case WM_MOUSEMOVE:
		x_mouse = LOWORD(lParam);
		y_mouse = HIWORD(lParam);
		break;
	case WM_LBUTTONDOWN:
		if (y_mouse <= 800 && y_mouse >= 400 && x_mouse >= 100 && x_mouse <= 500 && !forbidden_change_player)
		{
			vector_search_main = 7 - ((y_mouse - 400) / 50);
			vector_search_second = (x_mouse - 100) / 50;
			if (current_player == BLUE_PLAYER)
			{
				if (game_field[vector_search_main][vector_search_second].state &&
					game_field[vector_search_main][vector_search_second].color_check)
				{
					if (game_field[vector_search_main][vector_search_second].simple_check)
						alloc_is_simple = true;
					else
						alloc_is_simple = false;
					game_field[vec_1_copy][vec_2_copy].alloc = false;
					game_field[vector_search_main][vector_search_second].alloc = true;
					vec_1_copy = vector_search_main;
					vec_2_copy = vector_search_second;
					game_field = check_near_cells(game_field, vector_search_main, vector_search_second, 
						current_player, true,&attack_check_alloc);
					help_attacks = attack_check_alloc;
					checker_is_allocated = true;
				}
			}
			else
			{
				if (game_field[vector_search_main][vector_search_second].state &&
					!game_field[vector_search_main][vector_search_second].color_check)
				{
					if (game_field[vector_search_main][vector_search_second].simple_check)
						alloc_is_simple = true;
					else
						alloc_is_simple = false;
					game_field[vec_1_copy][vec_2_copy].alloc = false;
					game_field[vector_search_main][vector_search_second].alloc = true;
					vec_1_copy = vector_search_main;
					vec_2_copy = vector_search_second;
					game_field = check_near_cells(game_field, vector_search_main, vector_search_second,
						current_player, true, &attack_check_alloc);
					help_attacks = attack_check_alloc;
					checker_is_allocated = true;
				}
			}
			InvalidateRect(hWnd, NULL, TRUE);
			UpdateWindow(hWnd);
		}
		break;
	case WM_RBUTTONDOWN:
		if (y_mouse <= 800 && y_mouse >= 400 && x_mouse >= 100 && x_mouse <= 500)
		{
			int buf_y, buf_x;
			bool king_attacks = false;
			vector_search_main = 7 - ((y_mouse - 400) / 50);
			vector_search_second = (x_mouse - 100) / 50;

			if (game_field[vector_search_main][vector_search_second].arrow)
			{
				repaint_swi_2 = true;
				if (checker_is_allocated)
				{
					r_but_y_copy = -1;
					r_but_x_copy = -1;
				}
				if (r_but_y_copy == -1 && r_but_x_copy == -1)
				{
					game_field[vector_search_main][vector_search_second].state = game_field[vec_1_copy][vec_2_copy].state;
					game_field[vector_search_main][vector_search_second].color_check = game_field[vec_1_copy][vec_2_copy].color_check;
					game_field[vector_search_main][vector_search_second].simple_check = game_field[vec_1_copy][vec_2_copy].simple_check;
					if (vector_search_main == 7 && game_field[vector_search_main][vector_search_second].color_check)
					{
						alloc_is_simple = false;
						game_field[vector_search_main][vector_search_second].simple_check = false;
					}
					else if (vector_search_main == 0 && !game_field[vector_search_main][vector_search_second].color_check)
					{
						alloc_is_simple = false;
						game_field[vector_search_main][vector_search_second].simple_check = false;
					}
					game_field[vector_search_main][vector_search_second].alloc = false;
					game_field[vec_1_copy][vec_2_copy].alloc = false;
					game_field[vec_1_copy][vec_2_copy].state = false;
					move_dif = abs(vector_search_main - vec_1_copy);
					buf_y = vector_search_main;
					buf_x = vector_search_second;
					if (move_dif > 1 && alloc_is_simple)
					{
						if (vector_search_main > vec_1_copy)
							buf_y = vector_search_main - 1;
						else
							buf_y = vector_search_main + 1;
						if (vector_search_second > vec_2_copy)
							buf_x = vector_search_second - 1;
						else
							buf_x = vector_search_second + 1;
					}
					else if (move_dif > 1 && !alloc_is_simple)
					{
						if (vec_1_copy > vector_search_main && vec_2_copy > vector_search_second)
						{
							while (buf_y != vec_1_copy && buf_x != vec_2_copy)
							{
								++buf_y;
								++buf_x;
								if (game_field[buf_y][buf_x].state)
								{
									king_attacks = true;
									break;
								}
							}
						}
						else if (vec_1_copy > vector_search_main && vec_2_copy < vector_search_second)
						{
							while (buf_y != vec_1_copy && buf_x != vec_2_copy)
							{
								++buf_y;
								--buf_x;
								if (game_field[buf_y][buf_x].state)
								{
									king_attacks = true;
									break;
								}
							}
						}
						else if (vec_1_copy < vector_search_main && vec_2_copy > vector_search_second)
						{
							while (buf_y != vec_1_copy && buf_x != vec_2_copy)
							{
								--buf_y;
								++buf_x;
								if (game_field[buf_y][buf_x].state)
								{
									king_attacks = true;
									break;
								}
							}
						}
						else if (vec_1_copy < vector_search_main && vec_2_copy < vector_search_second)
						{
							while (buf_y != vec_1_copy && buf_x != vec_2_copy)
							{
								--buf_y;
								--buf_x;
								if (game_field[buf_y][buf_x].state)
								{
									king_attacks = true;
									break;
								}
							}
						}
					}
				}
				else
				{
					game_field[vector_search_main][vector_search_second].state = game_field[r_but_y_copy][r_but_x_copy].state;
					game_field[vector_search_main][vector_search_second].color_check = game_field[r_but_y_copy][r_but_x_copy].color_check;
					game_field[vector_search_main][vector_search_second].simple_check = game_field[r_but_y_copy][r_but_x_copy].simple_check;
					if (vector_search_main == 7 && game_field[vector_search_main][vector_search_second].color_check)
					{
						alloc_is_simple = false;
						game_field[vector_search_main][vector_search_second].simple_check = false;
					}
					else if (vector_search_main == 0 && !game_field[vector_search_main][vector_search_second].color_check)
					{
						alloc_is_simple = false;
						game_field[vector_search_main][vector_search_second].simple_check = false;
					}
					game_field[vector_search_main][vector_search_second].alloc = false;
					game_field[r_but_y_copy][r_but_x_copy].alloc = false;
					game_field[r_but_y_copy][r_but_x_copy].state = false;
					move_dif = abs(vector_search_main - r_but_y_copy);
					buf_y = vector_search_main;
					buf_x = vector_search_second;
					if (move_dif > 1 && alloc_is_simple)
					{
						if (vector_search_main > r_but_y_copy)
							buf_y = vector_search_main - 1;
						else
							buf_y = vector_search_main + 1;
						if (vector_search_second > r_but_x_copy)
							buf_x = vector_search_second - 1;
						else
							buf_x = vector_search_second + 1;
					}
					else if (move_dif > 1 && !alloc_is_simple)
					{
						if (r_but_y_copy > vector_search_main && r_but_x_copy > vector_search_second)
						{
							while (buf_y != r_but_y_copy && buf_x != r_but_x_copy)
							{
								++buf_y;
								++buf_x;
								if (game_field[buf_y][buf_x].state)
								{
									king_attacks = true;
									break;
								}
							}
						}
						else if (r_but_y_copy > vector_search_main && r_but_x_copy < vector_search_second)
						{
							while (buf_y != r_but_y_copy && buf_x != r_but_x_copy)
							{
								++buf_y;
								--buf_x;
								if (game_field[buf_y][buf_x].state)
								{
									king_attacks = true;
									break;
								}
							}
						}
						else if (r_but_y_copy < vector_search_main && r_but_x_copy > vector_search_second)
						{
							while (buf_y != r_but_y_copy && buf_x != r_but_x_copy)
							{
								--buf_y;
								++buf_x;
								if (game_field[buf_y][buf_x].state)
								{
									king_attacks = true;
									break;
								}
							}
						}
						else if (r_but_y_copy < vector_search_main && r_but_x_copy < vector_search_second)
						{
							while (buf_y != r_but_y_copy && buf_x != r_but_x_copy)
							{
								--buf_y;
								--buf_x;
								if (game_field[buf_y][buf_x].state)
								{
									king_attacks = true;
									break;
								}
							}
						}
					}
				}

				r_but_y_copy = vector_search_main;
				r_but_x_copy = vector_search_second;
				//bufer = to_wstring(vec_1_copy);
				//bufer_2 = to_wstring(vec_2_copy);
				//MessageBox(NULL, bufer.c_str(),bufer_2.c_str(),
				//	MB_OK);
				if (move_dif > 1 && alloc_is_simple)
					game_field[buf_y][buf_x].state = false;
				else if (move_dif > 1 && !alloc_is_simple && king_attacks)
					game_field[buf_y][buf_x].state = false;
				game_field = check_near_cells(game_field, vector_search_main, vector_search_second,
					current_player, false, &attack_check_alloc);
				act = true;
				checker_is_allocated = false;
				forbidden_change_player = false;
				move_dif = 0;
			}
			InvalidateRect(hWnd, NULL, TRUE);
			UpdateWindow(hWnd);
		}
		break;
	case WM_KEYDOWN:
		break;
	case WM_COMMAND:
		if (HIWORD(wParam) == BN_CLICKED)
		{
			if (new_game_button == (HWND)lParam)
			{
				game_field.resize(0);
				int x_mouse, y_mouse, vector_search_main, vector_search_second, vec_1_copy, vec_2_copy;
				checker_is_allocated = false;
				alloc_position = -1;
				move_position = -1;
				test = false;
				forbidden_change_player = false; 
				act = false; 
				attack_check_alloc = false;
				help_attacks = false;
				current_player = BLUE_PLAYER;
				r_but_y_copy = -1;
				r_but_x_copy = -1;
				repaint_swi = false;
				repaint_swi_2 = true;
				move_dif = 0;
				alloc_is_simple = true;
				game_field = load_field_function();
				InvalidateRect(hWnd, NULL, TRUE);
				UpdateWindow(hWnd);
			}
		}
		wmId = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Parse the menu selections:
		switch (wmId)
		{
		case IDM_ABOUT:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
			break;
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case WM_PAINT:
		RECT rectPlace;
		hdc = BeginPaint(hWnd, &ps);
		// TODO: Add any drawing code here...
		if (test)
		{
			hdcMem = CreateCompatibleDC(hdc);
			oldBitmap = SelectObject(hdcMem, alloc_cell_bitmap);

			GetObject(alloc_cell_bitmap, sizeof(bitmap), &bitmap);
			BitBlt(hdc, 400, 10, 50, 50, hdcMem, 0, 0, SRCCOPY);

			SelectObject(hdcMem, oldBitmap);
			DeleteDC(hdcMem);
		}

		for (int counter = 0; counter < 8; ++counter)
		{
			for (int counter_2 = 0; counter_2 < 8; ++counter_2)
			{
				Rectangle(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
					game_field[counter][counter_2].x + 50, game_field[counter][counter_2].y + 50);
				hdcMem = CreateCompatibleDC(hdc);
				if (game_field[counter][counter_2].arrow)
				{
					oldBitmap = SelectObject(hdcMem, alloc_cell_bitmap);
					GetObject(alloc_cell_bitmap, sizeof(bitmap), &bitmap);
					BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
						50, 50, hdcMem, 0, 0, SRCCOPY);
					SelectObject(hdcMem, oldBitmap);
					DeleteDC(hdcMem);
				}
				else
				if (game_field[counter][counter_2].white == false)
				{
					if (game_field[counter][counter_2].state == false)
					{
						oldBitmap = SelectObject(hdcMem, black_bitmap);
						GetObject(black_bitmap, sizeof(bitmap), &bitmap);
						BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
							50, 50, hdcMem, 0, 0, SRCCOPY);
						SelectObject(hdcMem, oldBitmap);
						DeleteDC(hdcMem);
					}
					else
					{
						if (game_field[counter][counter_2].color_check == true)
						{
							if (game_field[counter][counter_2].simple_check == true)
							{
								if (game_field[counter][counter_2].marked_for_death == true)
								{
									if (!checker_is_allocated && help_attacks)
									{
										game_field[vector_search_main][vector_search_second].alloc = true;
										forbidden_change_player = true;
										if (repaint_swi_2)
										{
											repaint_swi_2 = false;
											repaint_swi = true;
										}
									}
									oldBitmap = SelectObject(hdcMem, marked_blue_bitmap);
									GetObject(marked_blue_bitmap, sizeof(bitmap), &bitmap);
									BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
										50, 50, hdcMem, 0, 0, SRCCOPY);
									SelectObject(hdcMem, oldBitmap);
									DeleteDC(hdcMem);
								}
								else if (game_field[counter][counter_2].alloc == true)
								{
									oldBitmap = SelectObject(hdcMem, alloc_blue_bitmap);
									GetObject(alloc_blue_bitmap, sizeof(bitmap), &bitmap);
									BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
										50, 50, hdcMem, 0, 0, SRCCOPY);
									SelectObject(hdcMem, oldBitmap);
									DeleteDC(hdcMem);
								}
								else
								{
									++check_counter;
									oldBitmap = SelectObject(hdcMem, blue_checker_bitmap);
									GetObject(blue_checker_bitmap, sizeof(bitmap), &bitmap);
									BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
										50, 50, hdcMem, 0, 0, SRCCOPY);
									SelectObject(hdcMem, oldBitmap);
									DeleteDC(hdcMem);
								}
							}
							else
							{
								if (game_field[counter][counter_2].marked_for_death == true)
								{
									if (!checker_is_allocated && help_attacks)
									{
										game_field[vector_search_main][vector_search_second].alloc = true;
										forbidden_change_player = true;
										if (repaint_swi_2)
										{
											repaint_swi_2 = false;
											repaint_swi = true;
										}
									}
									oldBitmap = SelectObject(hdcMem, marked_blue_king_bitmap);
									GetObject(marked_blue_king_bitmap, sizeof(bitmap), &bitmap);
									BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
										50, 50, hdcMem, 0, 0, SRCCOPY);
									SelectObject(hdcMem, oldBitmap);
									DeleteDC(hdcMem);
								}
								else if (game_field[counter][counter_2].alloc == true)
								{
									oldBitmap = SelectObject(hdcMem, alloc_blue_king_bitmap);
									GetObject(alloc_blue_king_bitmap, sizeof(bitmap), &bitmap);
									BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
										50, 50, hdcMem, 0, 0, SRCCOPY);
									SelectObject(hdcMem, oldBitmap);
									DeleteDC(hdcMem);
								}
								else
								{
									oldBitmap = SelectObject(hdcMem, king_blue_bitmap);
									GetObject(king_blue_bitmap, sizeof(bitmap), &bitmap);
									BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
										50, 50, hdcMem, 0, 0, SRCCOPY);
									SelectObject(hdcMem, oldBitmap);
									DeleteDC(hdcMem);
								}
							}
						}
						else
						{
							if (game_field[counter][counter_2].simple_check == true)
							{
								if (game_field[counter][counter_2].marked_for_death == true)
								{
									if (!checker_is_allocated && help_attacks)
									{
										game_field[vector_search_main][vector_search_second].alloc = true;
										forbidden_change_player = true;
										if (repaint_swi_2)
										{
											repaint_swi_2 = false;
											repaint_swi = true;
										}
									}
									oldBitmap = SelectObject(hdcMem, marked_green_bitmap);
									GetObject(marked_green_bitmap, sizeof(bitmap), &bitmap);
									BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
										50, 50, hdcMem, 0, 0, SRCCOPY);
									SelectObject(hdcMem, oldBitmap);
									DeleteDC(hdcMem);
								}
								else if (game_field[counter][counter_2].alloc == true)
								{
									oldBitmap = SelectObject(hdcMem, alloc_green_bitmap);
									GetObject(alloc_green_bitmap, sizeof(bitmap), &bitmap);
									BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
										50, 50, hdcMem, 0, 0, SRCCOPY);
									SelectObject(hdcMem, oldBitmap);
									DeleteDC(hdcMem);
								}
								else
								{
									oldBitmap = SelectObject(hdcMem, green_checker_bitmap);
									GetObject(green_checker_bitmap, sizeof(bitmap), &bitmap);
									BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
										50, 50, hdcMem, 0, 0, SRCCOPY);
									SelectObject(hdcMem, oldBitmap);
									DeleteDC(hdcMem);
								}
							}
							else
							{
								if (game_field[counter][counter_2].marked_for_death == true)
								{
									if (!checker_is_allocated && help_attacks)
									{
										game_field[vector_search_main][vector_search_second].alloc = true;
										forbidden_change_player = true;
										if (repaint_swi_2)
										{
											repaint_swi_2 = false;
											repaint_swi = true;
										}
									}
									oldBitmap = SelectObject(hdcMem, marked_green_king_bitmap);
									GetObject(marked_green_king_bitmap, sizeof(bitmap), &bitmap);
									BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
										50, 50, hdcMem, 0, 0, SRCCOPY);
									SelectObject(hdcMem, oldBitmap);
									DeleteDC(hdcMem);
								}
								else if (game_field[counter][counter_2].alloc == true)
								{
									oldBitmap = SelectObject(hdcMem, alloc_green_king_bitmap);
									GetObject(alloc_green_king_bitmap, sizeof(bitmap), &bitmap);
									BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
										50, 50, hdcMem, 0, 0, SRCCOPY);
									SelectObject(hdcMem, oldBitmap);
									DeleteDC(hdcMem);
								}
								else
								{
									oldBitmap = SelectObject(hdcMem, king_green_bitmap);
									GetObject(king_green_bitmap, sizeof(bitmap), &bitmap);
									BitBlt(hdc, game_field[counter][counter_2].x, game_field[counter][counter_2].y,
										50, 50, hdcMem, 0, 0, SRCCOPY);
									SelectObject(hdcMem, oldBitmap);
									DeleteDC(hdcMem);
								}
							}
						}
					}
				}
				//bufer_2 = to_wstring(counter);
				//bufer = to_wstring(counter_2);
				//MessageBox(NULL, bufer.c_str(), bufer_2.c_str(),
				//	MB_OK | MB_ICONWARNING);
			}

			}
			//if (check_counter == 12)
			//	MessageBox(NULL, TEXT("UUUUUUUU"), TEXT("UUUUUUUU"),
			//		MB_OK | MB_ICONWARNING);
			if (act && !forbidden_change_player && current_player == BLUE_PLAYER)
			{
				current_player = GREEN_PLAYER;
				act = false;
				for (int counter = 0; counter < 8; ++counter)
				{
					for (int counter_2 = 0; counter_2 < 8; ++counter_2)
					{
						game_field[counter][counter_2].dead_lock = false;
						game_field[counter][counter_2].arrow = false;
						game_field[counter][counter_2].marked_for_death = false;
					}
				}
				InvalidateRect(hWnd, NULL, TRUE);
				UpdateWindow(hWnd);
			}
			else if (act && !forbidden_change_player && current_player == GREEN_PLAYER)
			{
				current_player = BLUE_PLAYER;
				act = false;
				for (int counter = 0; counter < 8; ++counter)
				{
					for (int counter_2 = 0; counter_2 < 8; ++counter_2)
					{
						game_field[counter][counter_2].dead_lock = false;
						game_field[counter][counter_2].arrow = false;
						game_field[counter][counter_2].marked_for_death = false;
					}
				}
				InvalidateRect(hWnd, NULL, TRUE);
				UpdateWindow(hWnd);
			}
			if (help_attacks)
			{
					//MessageBox(NULL, TEXT("help_attacks TRUE"), TEXT("help_attacks TRUE"),
					//	MB_OK | MB_ICONWARNING);
			}
			EndPaint(hWnd, &ps);
			if (repaint_swi)
			{
				repaint_swi = false;
				InvalidateRect(hWnd, NULL, TRUE);
				UpdateWindow(hWnd);
			}
			break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
		}
		return 0;
	}

// Message handler for about box.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}
