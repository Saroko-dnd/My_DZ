// Файл WINDOWS.H содержит определения, макросы, и структуры 
// которые используются при написании приложений под Windows. 
#include <windows.h>
#include <tchar.h>
#include <map>
using namespace std;

//прототип оконной процедуры
LRESULT CALLBACK WindowProc(HWND, UINT, WPARAM, LPARAM);

TCHAR szClassWindow[] = TEXT("Каркасное приложение"); /* Имя класса окна */
TCHAR mainMessage[] = TEXT("Демонстрация отрисовки цветного текста!");//for paint
int RGB_color_1 = 0, RGB_color_2 = 0, RGB_color_3=0;

typedef LRESULT CALLBACK EventProc(HWND hWnd, UINT uMessage, WPARAM wParam, LPARAM lParam);



map<UINT, EventProc*> eventMap;

LRESULT CALLBACK KeyboardHanldler(HWND hWnd, UINT uMessage, WPARAM wParam, LPARAM lParam) {
	TCHAR str[50];
	BOOL bRepaint = true;
	RECT our_rect;
	GetWindowRect(hWnd, &our_rect);
	if (wParam == VK_UP)
	{
		MoveWindow(
				hWnd,
				our_rect.left,
				our_rect.top - 1,
				our_rect.right - our_rect.left,
				our_rect.bottom - our_rect.top,
				bRepaint
				);
		return 0;
	}
	if (wParam == VK_DOWN)
	{
		MoveWindow(
			hWnd,
			our_rect.left,
			our_rect.top + 1,
			our_rect.right - our_rect.left,
			our_rect.bottom - our_rect.top,
			bRepaint
			);
		return 0;
	}
	if (wParam == VK_LEFT)
	{
		MoveWindow(
			hWnd,
			our_rect.left - 1,
			our_rect.top,
			our_rect.right - our_rect.left,
			our_rect.bottom - our_rect.top,
			bRepaint
			);
		return 0;
	}
	if (wParam == VK_RIGHT)
	{
		MoveWindow(
			hWnd,
			our_rect.left + 1,
			our_rect.top,
			our_rect.right - our_rect.left,
			our_rect.bottom - our_rect.top,
			bRepaint
			);
		return 0;
	}
	else
	{
		wsprintf(str, TEXT("code=%d"), wParam); // текущие координаты курсора мыши
		SetWindowText(hWnd, str);
		return 0;
	}
}


LRESULT CALLBACK OnDoubleClick(HWND hWnd, UINT uMessage, WPARAM wParam, LPARAM lParam){
	MessageBox(0,
		TEXT("Двойной щелчок левой кнопкой мыши"),
		TEXT("WM_LBUTTONDBLCLK"),
		MB_OK | MB_ICONINFORMATION);
	return 0;

}

LRESULT CALLBACK OnExit(HWND hWnd, UINT uMessage, WPARAM wParam, LPARAM lParam){
	PostQuitMessage(0);	// посылка сообщения WM_QUIT
	return 0;
}

LRESULT CALLBACK run_cursor(HWND hWnd, UINT uMessage, WPARAM wParam, LPARAM lParam){
	BOOL bRepaint = true;
	RECT our_rect;
	GetWindowRect(hWnd, &our_rect);
		int left_mod, top_mod;
		int x = LOWORD(lParam);
		if ((our_rect.right - our_rect.left) / 2 > LOWORD(lParam))
			left_mod = 1;
		else
			left_mod = -1;
		if ((our_rect.bottom - our_rect.top) / 2 > HIWORD(lParam))
			top_mod = 1;
		else
			top_mod = -1;
		MoveWindow(
			hWnd,
			our_rect.left + left_mod,
			our_rect.top + top_mod,
			our_rect.right - our_rect.left,
			our_rect.bottom - our_rect.top,
			bRepaint
			);
	return 0;
}

INT WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrevInst, LPTSTR lpszCmdLine, int nCmdShow)
{
	HWND hWnd;
	MSG Msg;
	WNDCLASSEX wcl;

	/* 1. Определение класса окна  */

	wcl.cbSize = sizeof (wcl);	// размер структуры WNDCLASSEX 
	wcl.style = CS_HREDRAW | CS_VREDRAW | CS_DBLCLKS;	// окно сможет получать сообщения о двойном щелчке (DBLCLK)
	wcl.lpfnWndProc = WindowProc;	// адрес оконной процедуры
	wcl.cbClsExtra = 0;		// используется Windows 
	wcl.cbWndExtra = 0; 	// используется Windows 
	wcl.hInstance = hInst;	// дескриптор данного приложения
	wcl.hIcon = LoadIcon(NULL, IDI_APPLICATION);	// загрузка стандартной иконки
	wcl.hCursor = LoadCursor(NULL, IDC_ARROW);		// загрузка стандартного курсора
	wcl.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);	//заполнение окна белым цветом			
	wcl.lpszMenuName = NULL;	// приложение не содержит меню
	wcl.lpszClassName = szClassWindow;	// имя класса окна
	wcl.hIconSm = NULL;	// отсутствие маленькой иконки для связи с классом окна


	/*  2. Регистрация класса окна  */

	if (!RegisterClassEx(&wcl))
		return 0;	// при неудачной регистрации - выход

	/*  3. Создание окна  */

	// создается окно и  переменной hWnd присваивается дескриптор окна
	hWnd = CreateWindowEx(
		0,		// расширенный стиль окна
		szClassWindow,	// имя класса окна
		TEXT("Каркас  Windows приложения"),	// заголовок окна
		/* Заголовок, рамка, позволяющая менять размеры, системное меню,
		кнопки развёртывания и свёртывания окна  */
		WS_OVERLAPPEDWINDOW,	// стиль окна
		CW_USEDEFAULT,	// х-координата левого верхнего угла окна
		CW_USEDEFAULT,	// y-координата левого верхнего угла окна
		CW_USEDEFAULT,	// ширина окна
		CW_USEDEFAULT,	// высота окна
		NULL,			// дескриптор родительского окна
		NULL,			// дескриптор меню окна
		hInst,		// идентификатор приложения, создавшего окно
		NULL);		// указатель на область данных приложения


	/* 4. Отображение окна */

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);	// перерисовка окна

	/* 4.5 Регистрируем события */

	eventMap[WM_KEYDOWN] = KeyboardHanldler;
	eventMap[WM_LBUTTONDBLCLK] = OnDoubleClick;
	eventMap[WM_DESTROY] = OnExit;
	eventMap[WM_MOUSEMOVE] = run_cursor;

	/* 5. Запуск цикла обработки сообщений  */

	// получение очередного сообщения из очереди сообщений
	while (GetMessage(&Msg, NULL, 0, 0))
	{
		TranslateMessage(&Msg);	// трансляция сообщения
		DispatchMessage(&Msg);	// диспетчеризация сообщений
	}
	return Msg.wParam;
}


// Оконная процедура вызывается операционной системой и получает в качестве 
// параметров сообщения из очереди сообщений данного приложения	




LRESULT CALLBACK WindowProc(HWND hWnd, UINT uMessage, WPARAM wParam, LPARAM lParam)
{
	HDC hDC; // создаём дескриптор ориентации текста на экране
	PAINTSTRUCT ps; // структура, сод-щая информацию о клиентской области (размеры, цвет и тп)
	RECT rect; // стр-ра, определяющая размер клиентской области
	COLORREF colorText = RGB(RGB_color_1, RGB_color_2, RGB_color_3);
	if (RGB_color_1 < 255)
		++RGB_color_1;
	else if (RGB_color_2 < 255) ++RGB_color_2;
	else if (RGB_color_3 < 255) ++RGB_color_3;
	else
	{
		RGB_color_1 = 0;
		RGB_color_2 = 0;
		RGB_color_3 = 0;
	}
		
	if (eventMap.find(uMessage) != eventMap.end())
		return (eventMap[uMessage])  (hWnd, uMessage, wParam, lParam);
	if (uMessage == WM_PAINT)
	{
		hDC = BeginPaint(hWnd, &ps); // инициализируем контекст устройства
		GetClientRect(hWnd, &rect); // получаем ширину и высоту области для рисования
		SetTextColor(hDC, colorText); // устанавливаем цвет контекстного устройства
		DrawText(hDC, mainMessage, -1, &rect, DT_SINGLELINE | DT_CENTER | DT_VCENTER); // рисуем текст
		EndPaint(hWnd, &ps);
	}
	else
		return DefWindowProc(hWnd, uMessage, wParam, lParam);


	/*
	switch (uMessage)
	{


	case WM_LBUTTONDOWN:
	MessageBox(
	0,
	TEXT("Нажата левая кнопка мыши"),
	TEXT("WM_LBUTTONDOWN"),
	MB_OK | MB_ICONINFORMATION);
	break;

	case WM_LBUTTONUP:
	MessageBox(
	0,
	TEXT("Отпущена левая кнопка мыши"),
	TEXT("WM_LBUTTONUP"),
	MB_OK | MB_ICONINFORMATION);
	break;

	case WM_RBUTTONDOWN:
	MessageBox(
	0,
	TEXT("Нажата правая кнопка мыши"),
	TEXT("WM_RBUTTONDOWN"),
	MB_OK | MB_ICONINFORMATION);
	break;

	case WM_MOUSEMOVE:{
	TCHAR str[50];
	wsprintf(str, TEXT("X=%d  Y=%d"), LOWORD(lParam), HIWORD(lParam)); // текущие координаты курсора мыши
	SetWindowText(hWnd, str);	// строка выводится в заголовок окна
	}break;

	case WM_CHAR:{
	TCHAR str[50];

	wsprintf(str, TEXT("Нажата клавиша %c"), (TCHAR)wParam);	// ASCII-код нажатой клавиши
	MessageBox(0, str, TEXT("WM_CHAR"), MB_OK | MB_ICONINFORMATION);
	break;
	}
	default:
	return DefWindowProc(hWnd, uMessage, wParam, lParam);
	}

	*/
	return 0;

}
