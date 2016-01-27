// Who_want_money.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "Who_want_money.h"
#include <iostream>
#include <string>
#include <vector>
#include <fstream>
#include <locale>
#include <locale.h>
#include <assert.h>
#include <codecvt>
#include <sstream>

using namespace std;

HWND hWnd, question_button, our_button, our_button_2, our_button_3, our_button_4, money,money_amount;

typedef struct one_block
{
	wstring question;
	vector <wstring> answers;
	int correct_answer;
}one_block;

vector <one_block> my_vector;
int number_of_question = 0;
std::wstringstream ws;
int num = 12;

ws << num << L" hello";

vector <one_block> load_text_function();

vector <one_block> load_text_function()
{
	int type_of_document = 1;
	wstring buf;
	one_block new_block;
	vector <one_block> new_vector;
	wifstream current_file("questions.txt", std::ios::binary);
	current_file.imbue(std::locale(current_file.getloc(), new std::codecvt_utf16<wchar_t, 0x10ffff, std::consume_header>));
	int number_of_answer;
	for (int counter = 0; counter < 3; ++counter)
	{
		number_of_answer = 0;
		while (type_of_document == 1 && getline<wchar_t>(current_file, buf))
		{
			if (buf[0] == L'*')
				type_of_document = 0;
			else
				new_block.question = buf;
		}
		while (type_of_document == 0 && getline<wchar_t>(current_file, buf))
		{
			++number_of_answer;
			if (buf[0] == L'@')
			{
				type_of_document = 1;
				new_vector.push_back(new_block);
				new_block.answers.resize(0);
			}
			else
			{
				if (buf[0] == L'+')
				{
					buf[0] = ' ';
					new_block.correct_answer = number_of_answer;
				}
				new_block.answers.push_back(buf);
			}
		}
	}

	return new_vector;

}

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
	LoadString(hInstance, IDC_WHO_WANT_MONEY, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_WHO_WANT_MONEY));

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
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_WHO_WANT_MONEY));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_WHO_WANT_MONEY);
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

   my_vector = load_text_function();
   hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, 800, 800, NULL, NULL, hInstance, NULL);
   question_button = CreateWindow(TEXT("BUTTON"), TEXT("2"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   40, 100, 200, 50, hWnd, NULL, hInstance, NULL);
   our_button = CreateWindow(TEXT("BUTTON"), TEXT("2"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   40, 200, 200, 50, hWnd, NULL, hInstance, NULL);
   our_button_2 = CreateWindow(TEXT("BUTTON"), TEXT("3"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   40, 260, 200, 50, hWnd, NULL, hInstance, NULL);
   our_button_3 = CreateWindow(TEXT("BUTTON"), TEXT("нет верного ответа"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   40, 320, 200, 50, hWnd, NULL, hInstance, NULL);
   our_button_4 = CreateWindow(TEXT("BUTTON"), TEXT("нет верного ответа"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   40, 380, 200, 50, hWnd, NULL, hInstance, NULL);
   money = CreateWindow(TEXT("STATIC"), TEXT("Деньги:"), WS_BORDER | WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   40, 500, 200, 50, hWnd, NULL, hInstance, NULL);
   money_amount = CreateWindow(TEXT("STATIC"), TEXT("0"), WS_BORDER | WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   300, 500, 200, 50, hWnd, NULL, hInstance, NULL);

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
		SetWindowText(question_button, my_vector[0].question.c_str());
		SetWindowText(our_button, my_vector[0].answers[0].c_str());
		SetWindowText(our_button_2, my_vector[0].answers[1].c_str());
		SetWindowText(our_button_3, my_vector[0].answers[2].c_str());
		SetWindowText(our_button_4, my_vector[0].answers[3].c_str());
		break;
	case WM_COMMAND:
		if (HIWORD(wParam) == BN_CLICKED)
		{
			if (our_button == (HWND)lParam && my_vector[number_of_question].correct_answer == 1)
			{
				MessageBox(hWnd, TEXT("Правильный ответ!"), TEXT("Правильный ответ!"), MB_OK);
				++number_of_question;
				if (number_of_question < 3)
				{
					SetWindowText(question_button, my_vector[number_of_question].question.c_str());
					SetWindowText(our_button, my_vector[number_of_question].answers[0].c_str());
					SetWindowText(our_button_2, my_vector[number_of_question].answers[1].c_str());
					SetWindowText(our_button_3, my_vector[number_of_question].answers[2].c_str());
					SetWindowText(our_button_4, my_vector[number_of_question].answers[3].c_str());
				}
			}
			if (our_button_2 == (HWND)lParam && my_vector[number_of_question].correct_answer == 2)
			{
				MessageBox(hWnd, TEXT("Правильный ответ!"), TEXT("Правильный ответ!"), MB_OK);
				++number_of_question;
				if (number_of_question < 3)
				{
					SetWindowText(question_button, my_vector[number_of_question].question.c_str());
					SetWindowText(our_button, my_vector[number_of_question].answers[0].c_str());
					SetWindowText(our_button_2, my_vector[number_of_question].answers[1].c_str());
					SetWindowText(our_button_3, my_vector[number_of_question].answers[2].c_str());
					SetWindowText(our_button_4, my_vector[number_of_question].answers[3].c_str());
				}
			}
			if (our_button_3 == (HWND)lParam && my_vector[number_of_question].correct_answer == 3)
			{
				MessageBox(hWnd, TEXT("Правильный ответ!"), TEXT("Правильный ответ!"), MB_OK);
				++number_of_question;
				if (number_of_question < 3)
				{
					SetWindowText(question_button, my_vector[number_of_question].question.c_str());
					SetWindowText(our_button, my_vector[number_of_question].answers[0].c_str());
					SetWindowText(our_button_2, my_vector[number_of_question].answers[1].c_str());
					SetWindowText(our_button_3, my_vector[number_of_question].answers[2].c_str());
					SetWindowText(our_button_4, my_vector[number_of_question].answers[3].c_str());
				}
			}
			if (our_button_4 == (HWND)lParam && my_vector[number_of_question].correct_answer == 4)
			{
				MessageBox(hWnd, TEXT("Правильный ответ!"), TEXT("Правильный ответ!"), MB_OK);
				++number_of_question;
				if (number_of_question < 3)
				{
					SetWindowText(question_button, my_vector[number_of_question].question.c_str());
					SetWindowText(our_button, my_vector[number_of_question].answers[0].c_str());
					SetWindowText(our_button_2, my_vector[number_of_question].answers[1].c_str());
					SetWindowText(our_button_3, my_vector[number_of_question].answers[2].c_str());
					SetWindowText(our_button_4, my_vector[number_of_question].answers[3].c_str());
				}
				if (number_of_question == 1)
				{
					SetWindowText(our_button_4, my_vector[0].answers[3].c_str());
				}
			}
		}
		wmId    = LOWORD(wParam);
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
