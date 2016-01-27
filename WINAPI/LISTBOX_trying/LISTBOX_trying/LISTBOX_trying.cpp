// LISTBOX_trying.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "LISTBOX_trying.h"
#include <vector>
#include <string>
#include <sstream>

using namespace std;
bool swi = true;
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

int counter = 0,counter_copy_1,counter_copy_2;
int number_list_1, number_list_2;
int number_of_celected_item = -1;
int number_of_celected_item_2 = -1;
int money = 0;
vector <wstring> our_list;
vector <int> int_list;
wstring our_buf, our_buf_2, our_buf_3;
wstringstream string_numbers;

HWND hWnd, first_list, second_list, add_but, remove_but, text_1, text_2, text_money, text_sum;

int APIENTRY _tWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPTSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);
	our_list.push_back(L"грабли");
	our_list.push_back(L"очки");
	our_list.push_back(L"катана");
	our_list.push_back(L"зуб белки");
	our_list.push_back(L"глаз жабы");
	int_list.push_back(100);
	int_list.push_back(300);
	int_list.push_back(2000);
	int_list.push_back(10);
	int_list.push_back(10);

 	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_LISTBOX_TRYING, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_LISTBOX_TRYING));

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
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LISTBOX_TRYING));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_LISTBOX_TRYING);
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

   hInst = hInstance; // Store instance handle in our global variable

   hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);
   first_list = CreateWindow(L"LISTBOX", szTitle, WS_CHILD | WS_VISIBLE | WS_BORDER,
	   50, 50, 300, 300, hWnd, (HMENU)3000, hInstance, NULL);
   second_list = CreateWindow(L"LISTBOX", szTitle, WS_CHILD | WS_VISIBLE | WS_BORDER,
	   500, 50, 300, 300, hWnd, (HMENU)3000, hInstance, NULL);
   text_1 = CreateWindow(TEXT("STATIC"), TEXT("МАГАЗИН"), WS_BORDER | WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   50, 0, 300, 40, hWnd, NULL, hInstance, NULL);
   text_2 = CreateWindow(TEXT("STATIC"), TEXT("КОРЗИНА"), WS_BORDER | WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   500, 0, 300, 40, hWnd, NULL, hInstance, NULL);
   add_but = CreateWindow(TEXT("BUTTON"), TEXT("положить в корзину"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   350, 200, 150, 50, hWnd, NULL, hInstance, NULL);
   remove_but = CreateWindow(TEXT("BUTTON"), TEXT("убрать из корзины"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   350, 260, 150, 50, hWnd, NULL, hInstance, NULL);
   text_money = CreateWindow(TEXT("STATIC"), TEXT("Общая стоимость"), WS_BORDER | WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   500, 350, 200, 40, hWnd, NULL, hInstance, NULL);
   text_sum = CreateWindow(TEXT("STATIC"), TEXT(" "), WS_BORDER | WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   700, 350, 100, 40, hWnd, NULL, hInstance, NULL);

   if (!first_list)
   {
	   exit(0);
   }

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
	PAINTSTRUCT ps;
	HDC hdc;

	switch (message)
	{
	case WM_SHOWWINDOW:
		SendMessage(first_list, LB_ADDSTRING, 0,
			(LPARAM)our_list[0].c_str());
		SendMessage(first_list, LB_ADDSTRING, 0,
			(LPARAM)our_list[1].c_str());
		SendMessage(first_list, LB_ADDSTRING, 0,
			(LPARAM)our_list[2].c_str());
		SendMessage(first_list, LB_ADDSTRING, 0,
			(LPARAM)our_list[3].c_str());
		SendMessage(first_list, LB_ADDSTRING, 0,
			(LPARAM)our_list[4].c_str());
		break;
	case WM_COMMAND:
		if (swi)
		{
			number_of_celected_item = SendMessage(first_list, LB_GETCURSEL, 0, 0);
			SendMessage(first_list, LB_GETTEXT, number_of_celected_item,(LPARAM) our_buf.c_str());
			if (number_of_celected_item >= 0)
			{
				while (counter < our_list.size() && our_buf[0] != our_list[counter][0])
				{
					++counter;
				}
			}
			counter_copy_1 = counter;
			counter = 0;
			number_of_celected_item_2 = SendMessage(second_list, LB_GETCURSEL, 0, 0);
			SendMessage(second_list, LB_GETTEXT, number_of_celected_item_2, (LPARAM)our_buf_2.c_str());
			if (number_of_celected_item_2 >= 0)
			{
				while (counter < our_list.size() && our_buf_2[0] != our_list[counter][0])
				{
					++counter;
				}
			}
			counter_copy_2 = counter;
			counter = 0;
		}
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Parse the menu selections:
		if (HIWORD(wParam) == BN_CLICKED)
		{
			if (add_but == (HWND)lParam)
			{
				if (number_of_celected_item >= 0)
				{
					SendMessage(first_list, LB_DELETESTRING, number_of_celected_item, 0);
					SendMessage(second_list, LB_ADDSTRING, 0, (LPARAM)our_buf.c_str());
					if (counter_copy_1 < int_list.size())
					{
						money += int_list[counter_copy_1];
						our_buf_3 = to_wstring(money);
						//string_numbers << money;
						//our_buf_3.resize(0);
						//string_numbers >> our_buf_3;
						SetWindowText(text_sum, our_buf_3.c_str());
					}
				}
				number_of_celected_item = -1;
			}

			if (remove_but == (HWND)lParam)
			{
				if (number_of_celected_item_2 >= 0)
				{
					SendMessage(second_list, LB_DELETESTRING, number_of_celected_item_2, 0);
					SendMessage(first_list, LB_ADDSTRING, 0, (LPARAM)our_buf_2.c_str());
					if (counter_copy_2 < int_list.size())
					{
						money -= int_list[counter_copy_2];
						our_buf_3 = to_wstring(money);
						//string_numbers << money;
						//our_buf_3.resize(0);
						//string_numbers >> our_buf_3;
						SetWindowText(text_sum, our_buf_3.c_str());
					}
				}
				number_of_celected_item_2 = -1;
			}
		}	
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
		hdc = BeginPaint(hWnd, &ps);
		// TODO: Add any drawing code here...
		EndPaint(hWnd, &ps);
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
