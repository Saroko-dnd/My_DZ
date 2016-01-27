#include "my_structures.h"
#include "stdafx.h"
#include "puzzle_game.h"
#include "check_result_function.h"
#include <windowsx.h>
#include <vector>
#include <algorithm>

using namespace std;

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name

// объявление окон
HWND our_button, our_button_2, our_button_3, our_button_4, our_button_5, our_button_6, our_button_7,
our_question,our_button_8, our_button_9;
HWND rect_1, rect_2, rect_3, rect_4, rect_5, rect_6, rect_7, rect_8, rect_9;
HWND check_button,current_obj_text;

current_hwnd buffer_state, move_buffer;

vector<current_hwnd>  list_of_rect;
vector<current_hwnd>  list_of_rect_pictures;
vector<coordinates>  list_of_coordinates_rect;
vector<coordinates>  list_of_coordinates_pictures;
vector<current_hwnd>::iterator our_iterator;

int x, y,i=0,cmd_show_copy;
bool buffer_is_empty = true,check_ready=true;
BOOL Repeat_p = true;

HBITMAP hBitmap = NULL, hBitmap_2 = NULL, hBitmap_3 = NULL, hBitmap_4 = NULL, hBitmap_5 = NULL, 
hBitmap_6 = NULL, hBitmap_7 = NULL, hBitmap_8 = NULL, hBitmap_9 = NULL, hBitmap_10 = NULL;

// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY _tWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPTSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	buffer_state.check_pos = -1;

	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;
	cmd_show_copy = nCmdShow;
	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_PUZZLE_GAME, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance(hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_PUZZLE_GAME));

	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int)msg.wParam;
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

	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDC_PUZZLE_GAME));
	wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wcex.lpszMenuName = MAKEINTRESOURCE(IDC_PUZZLE_GAME);
	wcex.lpszClassName = szWindowClass;
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

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
	coordinates our_coordinates;

	hInst = hInstance; // Store instance handle in our global variable

	hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
		300, 0, 380, 700, NULL, NULL, hInstance, NULL);

	hBitmap = (HBITMAP)LoadImage(hInst, TEXT("1_rect.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
	hBitmap_2 = (HBITMAP)LoadImage(hInst, TEXT("2_rect.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
	hBitmap_3 = (HBITMAP)LoadImage(hInst, TEXT("3_rect.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
	hBitmap_4 = (HBITMAP)LoadImage(hInst, TEXT("4_rect.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
	hBitmap_5 = (HBITMAP)LoadImage(hInst, TEXT("5_rect.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
	hBitmap_6 = (HBITMAP)LoadImage(hInst, TEXT("6_rect.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
	hBitmap_7 = (HBITMAP)LoadImage(hInst, TEXT("7_rect.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
	hBitmap_8 = (HBITMAP)LoadImage(hInst, TEXT("8_rect.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
	hBitmap_9 = (HBITMAP)LoadImage(hInst, TEXT("9_rect.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
	hBitmap_10 = (HBITMAP)LoadImage(hInst, TEXT("arrow_up.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);

	int new_x = 100, new_y = 100;
	while (new_y <= 200)
	{
		our_coordinates.x = new_x;
		our_coordinates.y = new_y;
		list_of_coordinates_rect.push_back(our_coordinates);
		if (new_x != 200)
			new_x += 50;
		else
		{
			new_x = 100;
			new_y += 50;
		}
	}

	new_x = 100;
	new_y = 300;
	while (new_y <= 400)
	{
		our_coordinates.x = new_x;
		our_coordinates.y = new_y;
		list_of_coordinates_pictures.push_back(our_coordinates);
		if (new_x != 200)
			new_x += 50;
		else
		{
			new_x = 100;
			new_y += 50;
		}
	}

	check_button = our_question = CreateWindow(TEXT("BUTTON"), TEXT("Проверить."), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		125, 60, 100, 35, hWnd, NULL, hInstance, NULL);
	our_question = CreateWindow(TEXT("STATIC"), TEXT(""), WS_CHILD | WS_VISIBLE,
		75, 0, 250, 50, hWnd, NULL, hInstance, NULL);
	current_obj_text = CreateWindow(TEXT("STATIC"), TEXT(""), WS_CHILD | WS_VISIBLE,
		80, 551, 203, 30, hWnd, NULL, hInstance, NULL);


	our_button = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		100, 300, 50, 50, hWnd, NULL, hInstance, NULL);
	our_button_2 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		150, 300, 50, 50, hWnd, NULL, hInstance, NULL);
	our_button_3 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		200, 300, 50, 50, hWnd, NULL, hInstance, NULL);
	our_button_4 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		100, 350, 50, 50, hWnd, NULL, hInstance, NULL);
	our_button_5 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		150, 350, 50, 50, hWnd, NULL, hInstance, NULL);
	our_button_6 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		200, 350, 50, 50, hWnd, NULL, hInstance, NULL);
	our_button_7 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		100, 400, 50, 50, hWnd, NULL, hInstance, NULL);
	our_button_8 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		150, 400, 50, 50, hWnd, NULL, hInstance, NULL);
	our_button_9 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		200, 400, 50, 50, hWnd, NULL, hInstance, NULL);
	current_hwnd cur_handle_1, cur_handle_2, cur_handle_3, cur_handle_4, cur_handle_5, cur_handle_6, cur_handle_7,
		cur_handle_8,cur_handle_9;
	rect_1 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		100, 100, 50, 50, hWnd, NULL, hInstance, NULL);
	rect_2 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		150, 100, 50, 50, hWnd, NULL, hInstance, NULL);
	rect_3 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		200, 100, 50, 50, hWnd, NULL, hInstance, NULL);
	rect_4 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		100, 150, 50, 50, hWnd, NULL, hInstance, NULL);
	rect_5 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		150, 150, 50, 50, hWnd, NULL, hInstance, NULL);
	rect_6 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		200, 150, 50, 50, hWnd, NULL, hInstance, NULL);
	rect_7 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		100, 200, 50, 50, hWnd, NULL, hInstance, NULL);
	rect_8 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		150, 200, 50, 50, hWnd, NULL, hInstance, NULL);
	rect_9 = CreateWindow(TEXT("BUTTON"), TEXT(""), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		200, 200, 50, 50, hWnd, NULL, hInstance, NULL);

	cur_handle_1.check_pos = 2;
	cur_handle_2.check_pos = 2;
	cur_handle_3.check_pos = 2;
	cur_handle_4.check_pos = 2;
	cur_handle_5.check_pos = 2;
	cur_handle_6.check_pos = 2;
	cur_handle_7.check_pos = 2;
	cur_handle_8.check_pos = 2;
	cur_handle_9.check_pos = 2;
	cur_handle_1.cur_hwnd = rect_1;
	cur_handle_2.cur_hwnd = rect_2;
	cur_handle_3.cur_hwnd = rect_3;
	cur_handle_4.cur_hwnd = rect_4;
	cur_handle_5.cur_hwnd = rect_5;
	cur_handle_6.cur_hwnd = rect_6;
	cur_handle_7.cur_hwnd = rect_7;
	cur_handle_8.cur_hwnd = rect_8;
	cur_handle_9.cur_hwnd = rect_9;
	list_of_rect.push_back(cur_handle_1);
	list_of_rect.push_back(cur_handle_2);
	list_of_rect.push_back(cur_handle_3);
	list_of_rect.push_back(cur_handle_4);
	list_of_rect.push_back(cur_handle_5);
	list_of_rect.push_back(cur_handle_6);
	list_of_rect.push_back(cur_handle_7);
	list_of_rect.push_back(cur_handle_8);
	list_of_rect.push_back(cur_handle_9);
	// присвоение значений для проверки результата
	cur_handle_1.check_pos = 1;
	cur_handle_1.bottom_number=6;
	cur_handle_1.left_number=6;
	cur_handle_1.right_number=6;
	cur_handle_1.top_number = 9;

	cur_handle_2.check_pos = 1;
	cur_handle_2.bottom_number = 8;
	cur_handle_2.left_number = 5;
	cur_handle_2.right_number = 1;
	cur_handle_2.top_number = 9;

	cur_handle_3.check_pos = 1;
	cur_handle_3.bottom_number = 4;
	cur_handle_3.left_number = 8;
	cur_handle_3.right_number = 9;
	cur_handle_3.top_number = 5;


	cur_handle_4.check_pos = 1;
	cur_handle_4.bottom_number = 9;
	cur_handle_4.left_number = 9;
	cur_handle_4.right_number = 5;
	cur_handle_4.top_number = 1;


	cur_handle_5.check_pos = 1;
	cur_handle_5.bottom_number = 1;
	cur_handle_5.left_number = 7;
	cur_handle_5.right_number = 6;
	cur_handle_5.top_number = 4;


	cur_handle_6.check_pos = 1;
	cur_handle_6.bottom_number = 4;
	cur_handle_6.left_number = 6;
	cur_handle_6.right_number = 3;
	cur_handle_6.top_number = 8;



	cur_handle_7.check_pos = 1;
	cur_handle_7.bottom_number = 0;
	cur_handle_7.left_number = 1;
	cur_handle_7.right_number = 9;
	cur_handle_7.top_number = 1;


	cur_handle_8.check_pos = 1;
	cur_handle_8.bottom_number = 4;
	cur_handle_8.left_number = 9;
	cur_handle_8.right_number = 0;
	cur_handle_8.top_number = 6;


	cur_handle_9.check_pos = 1;
	cur_handle_9.bottom_number = 2;
	cur_handle_9.left_number = 0;
	cur_handle_9.right_number = 8;
	cur_handle_9.top_number = 4;


	cur_handle_1.cur_hwnd = our_button;
	cur_handle_2.cur_hwnd = our_button_2;
	cur_handle_3.cur_hwnd = our_button_3;
	cur_handle_4.cur_hwnd = our_button_4;
	cur_handle_5.cur_hwnd = our_button_5;
	cur_handle_6.cur_hwnd = our_button_6;
	cur_handle_7.cur_hwnd = our_button_7;
	cur_handle_8.cur_hwnd = our_button_8;
	cur_handle_9.cur_hwnd = our_button_9;
	list_of_rect_pictures.push_back(cur_handle_1);
	list_of_rect_pictures.push_back(cur_handle_2);
	list_of_rect_pictures.push_back(cur_handle_3);
	list_of_rect_pictures.push_back(cur_handle_4);
	list_of_rect_pictures.push_back(cur_handle_5);
	list_of_rect_pictures.push_back(cur_handle_6);
	list_of_rect_pictures.push_back(cur_handle_7);
	list_of_rect_pictures.push_back(cur_handle_8);
	list_of_rect_pictures.push_back(cur_handle_9);


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
	int wmId, wmEvent;
	HDC hDC; // создаём дескриптор ориентации текста на экране
	PAINTSTRUCT ps; // структура, сод-щая информацию о клиентской области (размеры, цвет и тп)
	RECT rect; // стр-ра, определяющая размер клиентской области
	COLORREF colorText = RGB(255, 0, 0);
	LPRECT lp_rect=NULL;

	switch (message)
	{
	case WM_COMMAND:
		if (HIWORD(wParam) == BN_CLICKED)
		{
			int counter = 0;
			while (counter != 9 && (list_of_rect_pictures[counter].cur_hwnd != (HWND)lParam))
				++counter;
			if (counter<9 && list_of_rect_pictures[counter].cur_hwnd == (HWND)lParam)
			{
				if (list_of_rect_pictures[counter].check_pos == 1)
				{
					if (buffer_state.check_pos == -1)
					{
						buffer_state = list_of_rect_pictures[counter];
						list_of_rect_pictures[counter].check_pos = -1;
						MoveWindow(list_of_rect_pictures[counter].cur_hwnd, 150, 500, 50, 50, FALSE);
						InvalidateRect(hWnd, NULL, TRUE);
						UpdateWindow(hWnd);
					}
					else
					{
						int count_number = 0;
						while (list_of_rect_pictures[count_number].check_pos != -1)
							++count_number;
						list_of_rect_pictures[count_number] = buffer_state;
						buffer_state.check_pos = -1;
						MoveWindow(buffer_state.cur_hwnd, list_of_coordinates_pictures[count_number].x, list_of_coordinates_pictures[count_number].y, 50, 50, FALSE);
						InvalidateRect(hWnd, NULL, TRUE);
						UpdateWindow(hWnd);
					}
				}
				else if (list_of_rect_pictures[counter].check_pos == -1)
				{
					list_of_rect_pictures[counter] = buffer_state;
					buffer_state.check_pos = -1;
					MoveWindow(buffer_state.cur_hwnd, list_of_coordinates_pictures[counter].x, list_of_coordinates_pictures[counter].y, 50, 50, FALSE);
					InvalidateRect(hWnd, NULL, TRUE);
					UpdateWindow(hWnd);
				}
				else 
				{
					InvalidateRect(hWnd, NULL, TRUE);
					UpdateWindow(hWnd);
				}
			}
			else
			{
				counter = 0;
				while (counter != 9 && (list_of_rect[counter].cur_hwnd != (HWND)lParam))
					++counter;
				if (counter<9 && list_of_rect[counter].cur_hwnd == (HWND)lParam)
				{
					if (list_of_rect[counter].check_pos == 2)
					{
						if (buffer_state.check_pos == 1)
						{
							int count_number = 0;
							while (list_of_rect_pictures[count_number].check_pos != -1)
								++count_number;
							list_of_rect_pictures[count_number] = list_of_rect[counter];
							MoveWindow(list_of_rect[counter].cur_hwnd, list_of_coordinates_pictures[count_number].x, list_of_coordinates_pictures[count_number].y, 50, 50, FALSE);
							list_of_rect[counter] = buffer_state;
							buffer_state.check_pos = -1;
							MoveWindow(buffer_state.cur_hwnd, list_of_coordinates_rect[counter].x, list_of_coordinates_rect[counter].y, 50, 50, FALSE);
							InvalidateRect(hWnd, NULL, TRUE);
							UpdateWindow(hWnd);
						}
						else
						{
							InvalidateRect(hWnd, NULL, TRUE);
							UpdateWindow(hWnd);
						}
					}
					else
					{
						int count_number = 0;
						while (list_of_rect_pictures[count_number].check_pos != 2)
							++count_number;
						MoveWindow(list_of_rect_pictures[count_number].cur_hwnd, list_of_coordinates_rect[counter].x, list_of_coordinates_rect[counter].y, 50, 50, FALSE);
						MoveWindow(list_of_rect[counter].cur_hwnd, list_of_coordinates_pictures[count_number].x, list_of_coordinates_pictures[count_number].y, 50, 50, FALSE);
						move_buffer = list_of_rect_pictures[count_number];
						list_of_rect_pictures[count_number] = list_of_rect[counter];
						list_of_rect[counter] = move_buffer;
						InvalidateRect(hWnd, NULL, TRUE);
						UpdateWindow(hWnd);
					}
				}
			}
			if (check_button == (HWND)lParam)
			{
				if (check_result(list_of_rect) == 0)
					MessageBox(hWnd,TEXT("Вы еще не заполнили все пустые квадраты!"),TEXT("ОТВЕТ"),
					MB_OK | MB_ICONWARNING);
				else if (check_result(list_of_rect) == 1)
					MessageBox(hWnd, TEXT("Вы неверно заполнили поле квадратами!"), TEXT("ОТВЕТ"),
					MB_OK | MB_ICONWARNING);
				else
				{
					MessageBox(hWnd, TEXT("Поздравляем с верным решением головоломки!"), TEXT("ОТВЕТ"),
						MB_OK);
				}
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
		PAINTSTRUCT 	ps;
		HDC 			hdc;
		BITMAP 			bitmap;
		HDC 			hdcMem;
		HGDIOBJ 		oldBitmap;
		HFONT hFont;
		RECT rectPlace;

		{
			hdc = BeginPaint(hWnd, &ps);
			Rectangle(hdc, 100, 100, 150, 150);
			Rectangle(hdc, 150, 100, 200, 150);
			Rectangle(hdc, 200, 100, 250, 150);
			Rectangle(hdc, 100, 150, 150, 200);
			Rectangle(hdc, 150, 150, 200, 200);
			Rectangle(hdc, 200, 150, 250, 200);
			Rectangle(hdc, 100, 200, 150, 250);
			Rectangle(hdc, 150, 200, 200, 250);
			Rectangle(hdc, 200, 200, 250, 250);

			Rectangle(hdc, 100, 300, 150, 350);
			Rectangle(hdc, 150, 300, 200, 350);
			Rectangle(hdc, 200, 300, 250, 350);
			Rectangle(hdc, 100, 350, 150, 400);
			Rectangle(hdc, 150, 350, 200, 400);
			Rectangle(hdc, 200, 350, 250, 400);
			Rectangle(hdc, 100, 400, 150, 450);
			Rectangle(hdc, 150, 400, 200, 450);
			Rectangle(hdc, 200, 400, 250, 450);

			Rectangle(hdc, 150, 500, 200, 550);

			hdcMem = CreateCompatibleDC(hdc);
			oldBitmap = SelectObject(hdcMem, hBitmap_10);
			GetObject(hBitmap_10, sizeof(bitmap), &bitmap);
			BitBlt(hdc, 150, 250, 50, 50, hdcMem, 0, 0, SRCCOPY);
			SelectObject(hdcMem, oldBitmap);
			DeleteDC(hdcMem);
			EndPaint(hWnd, &ps);
		}
		{
			hdc = BeginPaint(current_obj_text, &ps);
			COLORREF colorText_2 = RGB(0, 0, 255);
			GetClientRect(current_obj_text, &rectPlace);
			SetTextColor(hdc, colorText_2);
			hFont = CreateFont(25, 0, 0, 0, 0, 0, 0, 0,
				DEFAULT_CHARSET,
				0, 0, 0, 0,
				L"Arial Bold"
				);
			SelectObject(hdc, hFont);
			DrawText(hdc, TEXT("выбранный квадрат"), 17, &rectPlace, DT_SINGLELINE | DT_CENTER | DT_VCENTER);
			EndPaint(current_obj_text, &ps);
		}
		{
			hdc = BeginPaint(our_question, &ps);
			GetClientRect(our_question, &rectPlace);
			SetTextColor(hdc, colorText);
			hFont = CreateFont(35, 0, 0, 0, 0, 0, 0, 0,
				DEFAULT_CHARSET,
				0, 0, 0, 0,
				L"Arial Bold"
				);
			SelectObject(hdc, hFont);
			DrawText(hdc, TEXT("игра Тетравекс"), 17, &rectPlace, DT_SINGLELINE | DT_CENTER | DT_VCENTER);

			EndPaint(our_question, &ps);
		}
		{
			hdc = BeginPaint(our_button, &ps);

			hdcMem = CreateCompatibleDC(hdc);
			oldBitmap = SelectObject(hdcMem, hBitmap);

			GetObject(hBitmap, sizeof(bitmap), &bitmap);
			BitBlt(hdc, 0, 0, 50, 50, hdcMem, 0, 0, SRCCOPY);

			SelectObject(hdcMem, oldBitmap);
			DeleteDC(hdcMem);

			EndPaint(our_button, &ps);
		}
		{
			hdc = BeginPaint(our_button_2, &ps);

			hdcMem = CreateCompatibleDC(hdc);
			oldBitmap = SelectObject(hdcMem, hBitmap_2);

			GetObject(hBitmap_2, sizeof(bitmap), &bitmap);
			BitBlt(hdc, 0, 0, 50, 50, hdcMem, 0, 0, SRCCOPY);

			SelectObject(hdcMem, oldBitmap);
			DeleteDC(hdcMem);

			EndPaint(our_button_2, &ps);
		}
		{
			hdc = BeginPaint(our_button_3, &ps);

			hdcMem = CreateCompatibleDC(hdc);
			oldBitmap = SelectObject(hdcMem, hBitmap_3);

			GetObject(hBitmap_3, sizeof(bitmap), &bitmap);
			BitBlt(hdc, 0, 0, 50, 50, hdcMem, 0, 0, SRCCOPY);

			SelectObject(hdcMem, oldBitmap);
			DeleteDC(hdcMem);

			EndPaint(our_button_3, &ps);
		}
		{
			hdc = BeginPaint(our_button_4, &ps);

			hdcMem = CreateCompatibleDC(hdc);
			oldBitmap = SelectObject(hdcMem, hBitmap_4);

			GetObject(hBitmap_4, sizeof(bitmap), &bitmap);
			BitBlt(hdc, 0, 0, 50, 50, hdcMem, 0, 0, SRCCOPY);

			SelectObject(hdcMem, oldBitmap);
			DeleteDC(hdcMem);

			EndPaint(our_button_4, &ps);
		}
		{
			hdc = BeginPaint(our_button_5, &ps);

			hdcMem = CreateCompatibleDC(hdc);
			oldBitmap = SelectObject(hdcMem, hBitmap_5);

			GetObject(hBitmap_5, sizeof(bitmap), &bitmap);
			BitBlt(hdc, 0, 0, 50, 50, hdcMem, 0, 0, SRCCOPY);

			SelectObject(hdcMem, oldBitmap);
			DeleteDC(hdcMem);

			EndPaint(our_button_5, &ps);
		}
		{
			hdc = BeginPaint(our_button_6, &ps);

			hdcMem = CreateCompatibleDC(hdc);
			oldBitmap = SelectObject(hdcMem, hBitmap_6);

			GetObject(hBitmap_6, sizeof(bitmap), &bitmap);
			BitBlt(hdc, 0, 0, 50, 50, hdcMem, 0, 0, SRCCOPY);

			SelectObject(hdcMem, oldBitmap);
			DeleteDC(hdcMem);

			EndPaint(our_button_6, &ps);
		}
		{
			hdc = BeginPaint(our_button_7, &ps);

			hdcMem = CreateCompatibleDC(hdc);
			oldBitmap = SelectObject(hdcMem, hBitmap_7);

			GetObject(hBitmap_7, sizeof(bitmap), &bitmap);
			BitBlt(hdc, 0, 0, 50, 50, hdcMem, 0, 0, SRCCOPY);

			SelectObject(hdcMem, oldBitmap);
			DeleteDC(hdcMem);

			EndPaint(our_button_7, &ps);
		}
		{
			hdc = BeginPaint(our_button_8, &ps);

			hdcMem = CreateCompatibleDC(hdc);
			oldBitmap = SelectObject(hdcMem, hBitmap_8);

			GetObject(hBitmap_8, sizeof(bitmap), &bitmap);
			BitBlt(hdc, 0, 0, 50, 50, hdcMem, 0, 0, SRCCOPY);

			SelectObject(hdcMem, oldBitmap);
			DeleteDC(hdcMem);

			EndPaint(our_button_8, &ps);
		}
		{
			hdc = BeginPaint(our_button_9, &ps);

			hdcMem = CreateCompatibleDC(hdc);
			oldBitmap = SelectObject(hdcMem, hBitmap_9);

			GetObject(hBitmap_9, sizeof(bitmap), &bitmap);
			BitBlt(hdc, 0, 0, 50, 50, hdcMem, 0, 0, SRCCOPY);

			SelectObject(hdcMem, oldBitmap);
			DeleteDC(hdcMem);

			EndPaint(our_button_9, &ps);
		}
		break;
	case WM_LBUTTONDOWN:
		break;
	case WM_MOUSEMOVE:
		x = LOWORD(lParam);
		y = HIWORD(lParam);
		break;
	case WM_KEYDOWN:
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
