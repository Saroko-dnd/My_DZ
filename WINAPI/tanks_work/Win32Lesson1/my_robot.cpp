#include "my_robot.h"
#include "Bolt.h"
#include <string.h>
#include "stdafx.h"

const double save_range = 2 * robot_radius;

static int count = 0;

void my_robot::makeDecision()
{

};

void my_robot::startStep()
{
	int rand_numb = rand() % 5;
	if (rand_numb == 1)
	{
		moveToPoint(arenaXSize / 2, arenaYSize / 2);
	}
	else if (rand_numb == 2)
	{
		moveToPoint(arenaXSize, 0);
	}
	else if (rand_numb == 3)
	{
		moveToPoint(0, arenaYSize);
	}
	else
	{
		moveToPoint(0, 0);
	}
};


void my_robot::Draw(HDC hdc) {

	//COLORREF qRobotColor = RGB(200, 0, 200);
	//HPEN hLinePen = CreatePen(PS_SOLID, 5, qRobotColor);
	HPEN hPenOld = (HPEN)SelectObject(hdc, line::pen5);
	MoveToEx(hdc, x, y, nullptr);

	// drow robot
	// circle
	Ellipse(hdc, x - robot_radius, y - robot_radius, x + robot_radius, y + robot_radius);

	//COLORREF qDethColor = RGB(255, 140, 0);
	//hLinePen = CreatePen(PS_SOLID, 3, qDethColor);
	hPenOld = (HPEN)SelectObject(hdc, line::pen2);
	MoveToEx(hdc, x - 10, y - 15, nullptr);
	LineTo(hdc, x + 10, y - 15);

	//COLORREF qLifeColor = RGB(50, 205, 50);
	//hLinePen = CreatePen(PS_SOLID, 3, qLifeColor);
	hPenOld = (HPEN)SelectObject(hdc, line::pen3);
	MoveToEx(hdc, x - 10, y - 15, nullptr);
	int xxx = (robot_energy - energy) / 100 * 2;
	LineTo(hdc, (x + 10) - xxx, y - 15);


	//LineTo(hdc, x2, y2);
	// text
	TCHAR str[1000];
	wsprintf(str, TEXT("Robot id %d : energy - %d."), id, energy);
	TextOut(hdc, 10, arenaXSize + (line::indent += 20), str, wcslen(str));
}


void my_robot::pricel(Circle* it, double l)
{
	double t = 0;
	double a = robot_speed*robot_speed - bolt_speed*bolt_speed;
	double b = 2 * ((it->x - x) * it->vx + (it->y - y) * it->vy);
	double c = pow((it->x - x), 2) + pow((it->y - y), 2);
	double D = b * b - 4 * a * c;
	if (D < 0)
	{
		std::cerr << "logic fail";
		exit(-1);
	}
	double tt = (-b - sqrt(D)) / (2 * a);

	// target bolt

	double x_bolt, y_bolt;
	x_bolt = (it->x + it->vx * tt);
	y_bolt = (it->y + it->vy * tt);
	shoot(this, x_bolt, y_bolt, 1600 / l);
};


void my_robot::shoots(double dist)
{
	for (auto it : Circle::objectStorage)
	{
		if (it != this){

			double l = chekL(it->x, it->y);
			if (dynamic_cast<InterfaceRobort*>(it) && !dynamic_cast<Robot*>(it))
			{
				if ((l < (8 * robot_radius)) && !(rand() % ((int)l / 4)) && !(energy < 100)) //dynamic_cast to Robot*  stop attaceked my robot
				{
					pricel(it, l);
					this->energy -= 1000 / l;
				}
				else if (energy < 100) taran_flag = true;



			}
		}
	}
};
