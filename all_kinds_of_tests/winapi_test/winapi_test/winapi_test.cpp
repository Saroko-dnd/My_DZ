// winapi_test.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "winapi_test.h"
#include <mmsystem.h>
#include <process.h>
#include <fstream>
#include <string>
#include <vector>
#include <windowsx.h>
#include <locale>
#include <locale.h>
#include <assert.h>
#include <codecvt>
#include <Commdlg.h>
#include <tchar.h>

using namespace std;

#define MAX_LOADSTRING 100

class OpenFileDialog
{
public:
	OpenFileDialog(void);

	TCHAR*DefaultExtension;
	TCHAR*FileName;
	TCHAR*Filter;
	int FilterIndex;
	int Flags;
	TCHAR*InitialDir;
	HWND Owner;
	TCHAR*Title;

	bool ShowDialog();
};

OpenFileDialog::OpenFileDialog(void)
{
	this->DefaultExtension = 0;
	this->FileName = new TCHAR[MAX_PATH];
	this->Filter = 0;
	this->FilterIndex = 0;
	this->Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;
	this->InitialDir = 0;
	this->Owner = 0;
	this->Title = 0;
}

bool OpenFileDialog::ShowDialog()
{
	OPENFILENAME ofn;

	ZeroMemory(&ofn, sizeof(ofn));

	ofn.lStructSize = sizeof(ofn);
	ofn.hwndOwner = this->Owner;
	ofn.lpstrDefExt = this->DefaultExtension;
	ofn.lpstrFile = this->FileName;
	ofn.lpstrFile[0] = '\0';
	ofn.nMaxFile = MAX_PATH;
	ofn.lpstrFilter = this->Filter;
	ofn.nFilterIndex = this->FilterIndex;
	ofn.lpstrInitialDir = this->InitialDir;
	ofn.lpstrTitle = this->Title;
	ofn.Flags = this->Flags;

	GetOpenFileName(&ofn);

	if (_tcslen(this->FileName) == 0) return false;

	return true;
}

// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING]=TEXT("llllll");					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name
bool create_win = true;
BOOL checked, checked_2;
int x, y;
// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);
bool play_music=false;
bool end_thread=false;
int counter_global = 0;
wstring buf_main, buf_main_2;
int first_time = 0, sec_time = 0;
vector <int> main_buf_for_file, main_buf_for_file_copy;
vector <int> main_buf_for_file_2, main_buf_for_file_2_copy;
vector <int> single_thread_buf;
bool thread_1_swi = true;
bool thread_2_swi = true;
bool swi_counter_2 = false;
bool swi_counter_1 = false;
HWND window_for_progress_bar, progress_bar, erase_button;
bool progress_test = true,swi_copy_file = false;
COLORREF NewColor = RGB(255, 125, 255);
OpenFileDialog openFileDialog1;
OPENFILENAME ofn,ofn_2;       // структура станд. блока диалога
TCHAR szFile[260],file_name_itself[260];       // буфер для имени файла
HANDLE hf;              // дескриптор файла
ifstream fin;
ofstream fout;
bool files_exists = false,file_exist_2 = false;

DWORD WINAPI Thread_for_file_copy(LPVOID lp)
{
	int number_of_copied_byte = 0;
	//fin.imbue(std::locale(fin.getloc(), new std::codecvt_utf16<wchar_t, 0x10ffff, std::consume_header>));
	int size = 0;
	float buf_float;
	fin.seekg(0, std::ios::end);
	size = fin.tellg();//размер в 2 байтах (для удобства)
	fin.seekg(0, std::ios::beg);
	swi_copy_file = true;
	char buf_char[64];
	while (fin.read(buf_char, 64) && progress_test)
	{
		number_of_copied_byte += 64;
		buf_float = ((float)number_of_copied_byte / (float)size)*100.0;
		SendMessage(progress_bar, PBM_SETPOS, (int)buf_float, 0);
		fout.write(buf_char,64);
		Sleep(100);
	}
	/*while (getline(fin, buf_main) && progress_test)
	{
		number_of_copied_byte += buf_main.size();
		buf_float = ((float)number_of_copied_byte / (float)size)*100.0;
		SendMessage(progress_bar, PBM_SETPOS, (int)buf_float, 0);
		fout << buf_main;
		Sleep(100);
	}*/
	if (!progress_test)
	{
		fin.close();
		fout.close();
		unlink("second.txt");
		//remove("second.txt");
		SendMessage(progress_bar, PBM_SETPOS, 0, 0);
		progress_test = true;
		swi_copy_file = false;
		return 0;
	}
	fin.close();
	fout.close();
	progress_test = true;
	swi_copy_file = false;
	return 0;
}

void test(void *param)
{
	while (1)
	{
		if (play_music)
		{
			play_music = false;
			PlaySound(
				TEXT("voice_set_thaos_0008.WAV"),
				NULL,
				SND_FILENAME
				);
		}
		if (end_thread)
			_endthread();
	}
}

int APIENTRY _tWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPTSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
	InitCommonControls();

	//HANDLE th1 = CreateThread(NULL, 0, Thread3, (LPVOID)0, 0, NULL);
	//HANDLE th2 = CreateThread(NULL, 0, Thread4, (LPVOID)1, 0, NULL);
	//HANDLE th3 = CreateThread(NULL, 0, Thread_file, (LPVOID)1, 0, NULL);

	//WaitForSingleObject(th1, INFINITE);
	//WaitForSingleObject(th2, INFINITE);
	//WaitForSingleObject(th3, INFINITE);

	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);
	_beginthread(test, 0, NULL);
 	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_WINAPI_TEST, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_WINAPI_TEST));

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
	WNDCLASSEX wcex, wcex_2;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= hInstance;
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_WINAPI_TEST));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_WINAPI_TEST);
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

   if (!hWnd)
   {
	   return FALSE;
   }

   HWND new_window_1, new_window_2;
		new_window_1 = CreateWindow(TEXT("BUTTON"), szTitle, WS_CHILD | WS_BORDER | WS_VISIBLE | BS_CHECKBOX,
	   0, 0, 100, 50, NULL, (HMENU) 1, hInst, NULL);
		new_window_2 = CreateWindow(TEXT("BUTTON"), szTitle, WS_CHILD | WS_BORDER | WS_VISIBLE | BS_CHECKBOX,
	   150, 5, 100, 50, hWnd, (HMENU)2, hInst, NULL); 
		erase_button = CreateWindow(TEXT("BUTTON"), szTitle, WS_CHILD | WS_BORDER | WS_VISIBLE,
		300, 270, 100, 50, hWnd, (HMENU)2, hInst, NULL);
		progress_bar = CreateWindowEx(0, PROGRESS_CLASS, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER,
			100, 200, 600, 50, hWnd, (HMENU)1, hInst, NULL);
		SetWindowText(erase_button, TEXT("ОТМЕНА"));
		window_for_progress_bar = CreateWindow(TEXT("BUTTON"), szTitle, WS_CHILD | WS_BORDER | WS_VISIBLE,
			5, 5, 50, 20, new_window_2, (HMENU)2, hInst, NULL);

		openFileDialog1.Owner = hWnd;
		openFileDialog1.Flags = 

   CheckDlgButton(hWnd, 1, BST_UNCHECKED /*BST_CHECKED*/);
   CheckDlgButton(hWnd, 2, /*BST_UNCHECKED*/ BST_CHECKED);

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

	//обработка кнопок управления...............
	if (message == WM_COMMAND && wParam == 1)
	{
			checked = IsDlgButtonChecked(hWnd, 1);
			if (checked) {
				CheckDlgButton(hWnd, 1, BST_UNCHECKED);
				SetWindowText(hWnd, TEXT("NO YOU DONT"));
			}
			else {
				CheckDlgButton(hWnd, 1, BST_CHECKED);
				SetWindowText(hWnd, TEXT("YOU DID THIS"));
			}
	}
	if (message == WM_COMMAND && wParam == 2)
	{
			checked_2 = IsDlgButtonChecked(hWnd, 2);
			if (checked_2) {
				CheckDlgButton(hWnd, 2, BST_UNCHECKED);
				SetWindowText(hWnd, TEXT("NO YOU DONT"));
			}
			else {
				CheckDlgButton(hWnd, 2, BST_CHECKED);
				SetWindowText(hWnd, TEXT("YOU DID THIS"));
			}
	}
	//...........................................

	switch (message)
	{
	case WM_KEYDOWN:
		if (wParam == VK_DOWN)
		{
			//-----------------------------------------------------
			TCHAR lpszBuffer[256];
			// Инициализация структуры OPENFILENAME
			ZeroMemory(&ofn, sizeof(OPENFILENAME));
			ofn.lStructSize = sizeof(OPENFILENAME);
			ofn.hwndOwner = hWnd;
			ofn.lpstrFile = szFile;
			ofn.nMaxFile = sizeof(szFile);
			ofn.lpstrFilter = TEXT("All\0*.*\0Text\0*.TXT\0");
			ofn.nFilterIndex = 1;
			ofn.lpstrFileTitle = file_name_itself;
			ofn.nMaxFileTitle = 0;
			ofn.lpstrInitialDir = NULL;
			ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;

			// Показываем на экране диалоговое окно Открыть (Open).

			if (GetOpenFileName(&ofn) == TRUE)//функция открытия файла
			{
				wstring buf_ = ofn.lpstrFileTitle;
				fin.open(ofn.lpstrFile, std::ios::binary);
				files_exists = true;
			}

			//----------------------------------------------------

				SendMessage(progress_bar, PBM_SETPOS, 50, 0);
				SendMessage(progress_bar, PBM_SETBARCOLOR, 0, NewColor);
			//play_music = true;
		}
		if (wParam == VK_LEFT)
		{
			//-----------------------------------------------------

			// Инициализация структуры OPENFILENAME
			ZeroMemory(&ofn_2, sizeof(OPENFILENAME));
			ofn_2.lStructSize = sizeof(OPENFILENAME);
			ofn_2.hwndOwner = hWnd;
			ofn_2.lpstrFile = szFile;
			ofn_2.nMaxFile = sizeof(szFile);
			ofn_2.lpstrFilter = TEXT("All\0*.*\0Text\0*.TXT\0");
			ofn_2.nFilterIndex = 1;
			ofn_2.lpstrFileTitle = file_name_itself;
			ofn_2.lpstrFileTitle[0] = L'\0';
			ofn_2.nMaxFileTitle = 0;
			ofn_2.lpstrInitialDir = NULL;
			ofn_2.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;

			// Показываем на экране диалоговое окно Открыть (Open).

			if (GetOpenFileName(&ofn) == TRUE)//функция открытия файла
			{
				wstring buf_ = ofn_2.lpstrFile;
				fout.open(ofn_2.lpstrFile, std::ios::binary);
				file_exist_2 = true;
			}

			//----------------------------------------------------

			SendMessage(progress_bar, PBM_SETPOS, 50, 0);
			SendMessage(progress_bar, PBM_SETBARCOLOR, 0, NewColor);
			//play_music = true;
		}
		if (wParam == VK_UP && files_exists && file_exist_2)
		{

			CreateThread(NULL, 0, Thread_for_file_copy, (LPVOID)0, 0, NULL);
			//play_music = true;
		}
		break;
	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Parse the menu selections:
		if (HIWORD(wParam) == BN_CLICKED)
		{
			if (erase_button == (HWND)lParam && swi_copy_file && progress_test)
			{
				progress_test = false;
			}
		}
		switch (wmId)
		{
		case IDM_ABOUT:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
			end_thread = true;
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
			MessageBox(hDlg,
				(LPCWSTR)L"Resource not available\nDo you want to try again?",
				(LPCWSTR)L"Account Details",
				MB_ICONWARNING | MB_OK
				);
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}

