// Win32Lesson1.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "Win32Lesson1.h"
#include <ctime>
#include "my_robot.h"

#define MAX_LOADSTRING 100


// Global Variables:
	
my_robot* new_robot;
int robots_count = 0;

HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name

// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);

void initRobots() {
	srand(time(NULL));

	//InterfaceRobort* temp_robot = ;
	Circle::objectStorage.push_back(new Robot(50, 450, "q"));
	robots_count++;
	//temp_robot = ;
	Circle::objectStorage.push_back(new Robot(250, 100, "r"));
	robots_count++;
	//temp_robot = new Robot(250, 400, "w");
	Circle::objectStorage.push_back(new Robot(250, 400, "w"));
	robots_count++;



	/*temp_robot = new Robot(0, 250, "e");
	Circle::objectStorage.push_back(temp_robot);
	robots_count++;*/


	
	Circle::objectStorage.push_back(new SuslikiRobot(200, 280));
	robots_count++;

	Circle::objectStorage.push_back(new SuslikiRobot(30, 45));
	robots_count++;

	Circle::objectStorage.push_back(new SuslikiRobot(250, 150));
	robots_count++;


	new_robot = new my_robot(100, 60, "V");
	Circle::objectStorage.push_back(new_robot);
	robots_count++;
	

	//Circle::objectStorage.push_back(new Bolt(100, 100, 400, 500, 100, nullptr));
	

	

	
	for (auto obj : Circle::objectStorage)
	{
		if (dynamic_cast<SuslikiRobot*>(obj) != nullptr)
			obj->moveToPoint(rand() % 450 + 20, rand() % 450 + 20);
	}
	


	//int fff = 8;

}



int APIENTRY _tWinMain(HINSTANCE hInstance,
	HINSTANCE hPrevInstance,
	LPTSTR    lpCmdLine,
	int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_WIN32LESSON1, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance(hInstance, nCmdShow))
	{
		return FALSE;
	}

	initRobots();


	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_WIN32LESSON1));

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
//  COMMENTS:
//
//    This function and its usage are only necessary if you want this code
//    to be compatible with Win32 systems prior to the 'RegisterClassEx'
//    function that was added to Windows 95. It is important to call this function
//    so that the application will get 'well formed' small icons associated
//    with it.
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
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_WIN32LESSON1));
	wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wcex.lpszMenuName = MAKEINTRESOURCE(IDC_WIN32LESSON1);
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

	hInst = hInstance; // Store instance handle in our global variable

	hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);

	if (!hWnd)
	{
		return FALSE;
	}






	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);


	COLORREF qRobotColor = RGB(0, 200, 200);
	line::pen1 = CreatePen(PS_SOLID, 5, qRobotColor);

	COLORREF qDethColor = RGB(255, 140, 0);
	line::pen2 = CreatePen(PS_SOLID, 3, qDethColor);

	COLORREF qLifeColor = RGB(50, 205, 50);
	line::pen3 = CreatePen(PS_SOLID, 3, qLifeColor);

	COLORREF qEnemyColor = RGB(200, 20, 133);
	line::pen4 = CreatePen(PS_SOLID, 5, qEnemyColor);

	COLORREF my_color = RGB(255, 0, 0);
	line::pen5 = CreatePen(PS_SOLID, 5, my_color);

	// ��������� ��� � 50 ���������� 
	SetTimer(hWnd, 0, 1000/FPS, 0); // period of refresh screen



	return TRUE;
}


void mainloop(HDC hdc){
	line::SetHdc(hdc);

	// temp for prev value
	/*	struct Value
		{
		int id;
		double prev_x, prev_y, prev_vx, prev_vy;
		Value(double x, double y, double vx, double vy, int id) {}
		};
		std::vector<Value*> prev;
		*/
	for (auto c : Circle::objectStorage)
	{
		// prev step robot or bolt
		//prev.push_back(new Value(c->x, c->y, c->vx, c->vy, c->id));

		// if robot
		InterfaceRobort* r = dynamic_cast<InterfaceRobort*>(c);
		if (r != nullptr) {
			r->makeDecision();
		}
		c->move(5.0 / FPS);
		if (c->x > arenaXSize || c->y > arenaYSize || c->x < 0 || c->y < 0)
			c->energy = 0;
	}




	// checks cycles 
	/*
	// 1. exit from arena size - delete from objectStorage
	std::vector<std::list<Circle*>::iterator> temp;
	for (auto os = Circle::objectStorage.begin(); os != Circle::objectStorage.end(); os++)
	{
	if ((*os)->v == robot_speed)
	{
	if ((*os)->x + robot_radius >= arenaXSize || (*os)->y + robot_radius >= arenaYSize || (*os)->x - robot_radius <= 0 || (*os)->y - robot_radius <= 0)
	{
	temp.push_back(os);
	//Circle::objectStorage.erase(os);
	robots_count--;
	}
	}
	else
	{
	if ((*os)->x >= arenaXSize || (*os)->y >= arenaYSize || (*os)->x <= 0 || (*os)->y <= 0)
	temp.push_back(os);
	//Circle::objectStorage.erase(os);
	}
	}

	for(auto t: temp)
	Circle::objectStorage.erase(t);


	// 2, 3. bolt hit in robot (vector), taran
	std::vector<std::list<Circle*>::iterator> dead;
	for (auto explosion = Circle::objectStorage.begin(); explosion != Circle::objectStorage.end(); explosion++)// Берем текущий
	{
	for (auto crexplosion = Circle::objectStorage.crbegin(); crexplosion != Circle::objectStorage.crend(); crexplosion++) // Берем с конца и на начало
	{
	if ((*explosion)->x + robot_radius >= (*crexplosion)->x || (*explosion)->y + robot_radius >= (*crexplosion)->y || (*explosion)->x <= (*crexplosion)->x + robot_radius || (*explosion)->y <= (*crexplosion)->y + robot_radius) // Не могу понять проверки
	{
	temp.push_back(explosion);
	robots_count--;
	}
	}
	}

	for (auto t : dead)
	Circle::objectStorage.erase(t);

	// end

	*/

	for (auto r1_ : Circle::objectStorage)
	{
		InterfaceRobort* r1 = dynamic_cast<InterfaceRobort*> (r1_);
		if (!r1) 	continue;

		for (auto r2 : Circle::objectStorage)
		{
			if ((r1_ != r2) && (hypot(r1->x - r2->x, r1->y - r2->y) <= (r1->radius + r2->radius)))
			{
				InterfaceRobort* rob2 = dynamic_cast<InterfaceRobort*> (r2);
				if (rob2)
				{
					// robot
					//pust dynamic _cust to  other type robots 
					//connect to other Robots (crash robot)
					int denergy = min(r1->energy, r2->energy);
					r1->energy -= denergy;
					r2->energy -= denergy;

				}

				Bolt* bolt = dynamic_cast<Bolt*> (r2);
				if (bolt != nullptr)
				{
					if (bolt->robot != r1)// need all bolt adition link to host Robot
					{
						//TODO: debug for add new class AngelRobot
						r1->energy -= bolt->energy * 3;
						bolt->energy = 0;
					}
				}

			}
		}
	}

	//remove_if(objectStorage.begin(), objectStorage.end(), p); crash
	Circle::objectStorage.erase(remove_if(Circle::objectStorage.begin(), Circle::objectStorage.end(), [](Circle* val)->bool{return val->energy <= 0; }), Circle::objectStorage.end());








	// function() - processing arena after update
	// if robot is dies - robots_count--
	// if something change : output to screen!!!!!

	line::indent = 0;

	Rectangle(hdc,0, 0, arenaXSize, arenaYSize);

	for (auto obj : Circle::objectStorage) {
		obj->Draw(hdc);
	}


	//>>>>>>>>>>>>>>>>>//


}

	
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;

	switch (message)
	{
	case WM_TIMER:
		InvalidateRgn(hWnd, 0, 1);
		RedrawWindow(hWnd, 0, 0, 0);// RDW_ERASE);
		break;
	case WM_LBUTTONDOWN:
		new_robot->moveToPoint(LOWORD(lParam), HIWORD(lParam));
		break;
	case WM_RBUTTONDOWN:
		new_robot->shoot(new_robot, LOWORD(lParam), HIWORD(lParam), 10);
		break;
	case WM_PAINT:{
					  HDC hdc;
					  hdc = BeginPaint(hWnd, &ps);
					  mainloop(hdc);
					  EndPaint(hWnd, &ps);

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
