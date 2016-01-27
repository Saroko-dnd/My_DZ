// winapi_button.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "winapi_button.h"
#include <windowsx.h>

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name
HWND our_button, our_button_2, our_button_3,our_question;
int number_of_question=1,x=300,y=300;
bool swi = true;
HBITMAP hBitmap = NULL;
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
	LoadString(hInstance, IDC_WINAPI_BUTTON, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_WINAPI_BUTTON));

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
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_WINAPI_BUTTON));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_WINAPI_BUTTON);
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

	hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);
	hBitmap = (HBITMAP)LoadImage(hInst, TEXT("9_rect.bmp"), IMAGE_BITMAP, 50, 50, LR_LOADFROMFILE);
	our_question = CreateWindow(TEXT("STATIC"), TEXT("Сколько будет 2+2?"), WS_CHILD | WS_VISIBLE,
		0, 0, 200, 50, hWnd, NULL, hInstance, NULL);
	our_button = CreateWindow(TEXT("BUTTON"), TEXT("2"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
	   40, 40, 200, 50, hWnd, NULL, hInstance, NULL);
	our_button_2 = CreateWindow(TEXT("BUTTON"), TEXT("3"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		40, 100, 200, 50, hWnd, NULL, hInstance, NULL);
	our_button_3 = CreateWindow(TEXT("BUTTON"), TEXT("нет верного ответа"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		40, 160, 200, 50, hWnd, NULL, hInstance, NULL);


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
	if (number_of_question == 2 && swi)
	{
		SetWindowText(our_button, TEXT("3"));
		SetWindowText(our_button_2, TEXT("8"));
		SetWindowText(our_button_3, TEXT("1"));
		swi = false;
	}
	switch (message)
	{
	case WM_COMMAND:
		if (HIWORD(wParam) == BN_CLICKED)
		{
			if (number_of_question == 1)
			{
				if (our_button == (HWND)lParam)
				{
					MessageBox(hWnd, TEXT("Неверно!"), TEXT("Результат"), MB_OK);
				}
				if (our_button_2 == (HWND)lParam)
				{
					MessageBox(hWnd, TEXT("Неверно!"), TEXT("Результат"), MB_OK);
				}
				if (our_button_3 == (HWND)lParam)
				{
					MessageBox(hWnd, TEXT("Правильно."), TEXT("Результат"), MB_OK);
					++number_of_question;
					SetWindowText(our_question, TEXT("Сколько будет 8-5?"));
				}
			}
			else
			{
				if (our_button == (HWND)lParam)
				{
					MessageBox(hWnd, TEXT("Правильно."), TEXT("Результат"), MB_OK);
					++number_of_question;
					SetWindowText(our_question, TEXT("Поздравляем! Вы прошли тест!"));
				}
				if (our_button_2 == (HWND)lParam)
				{
					MessageBox(hWnd, TEXT("Неверно!"), TEXT("Результат"), MB_OK);
				}
				if (our_button_3 == (HWND)lParam)
				{
					MessageBox(hWnd, TEXT("Неверно!"), TEXT("Результат"), MB_OK);
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
		PAINTSTRUCT 	ps;
		HDC 			hdc;
		BITMAP 			bitmap;
		HDC 			hdcMem;
		HGDIOBJ 		oldBitmap;
		{
			hdc = BeginPaint(hWnd, &ps);
			Rectangle(hdc, 100, 100, 300, 300);
			hdcMem = CreateCompatibleDC(hdc);
			oldBitmap = SelectObject(hdcMem, hBitmap);

			GetObject(hBitmap, sizeof(bitmap), &bitmap);
			BitBlt(hdc, x, y, 50, 50, hdcMem, 0, 0, SRCCOPY);

			SelectObject(hdcMem, oldBitmap);
			DeleteDC(hdcMem);
			EndPaint(hWnd, &ps);
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
		break;
	case WM_LBUTTONDOWN:
		if (LOWORD(lParam) > 300 && LOWORD(lParam) < 350)
		{
			if (HIWORD(lParam) > 300 && HIWORD(lParam) < 350)
			{
				y = 0;
				BOOL check = true;
				MoveWindow(our_button, 400, 400, 200, 50, check);
			}
		}
		break;
	case WM_MOUSEMOVE:
		x = LOWORD(lParam);
		y = HIWORD(lParam);
		MoveWindow(our_button, x, y, 200, 50, true);
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
