#include "SuslikiRobot.h"
#include "Definitions.h"
#include <cmath>
#include "stdafx.h"
#include "Bolt.h"
#include <string>
//#include "Enemy.h"




double safe_distance = 2 * robot_speed * (5 / FPS) + robot_radius * 4;



using namespace std;
void SuslikiRobot::makeDecision()
{
	// check at the end of the card
	if (x + safe_distance >= arenaXSize || y + safe_distance >= arenaYSize || x - safe_distance <= 0 || y - safe_distance <= 0)
	{
		VX_VY temp(0, 0);
		double new_x, new_y;
		double alpha = (rand() % 359 + 1);// * 3.14 / 180;
		double start_alpha = alpha;
		bool res;
		do
		{ 
			temp = Turn(alpha);
			new_x = x + temp.vx / (5.0 / FPS);
			new_y = y + temp.vy / (5.0 / FPS);
			res = CheckSafe(new_x, new_y);
			if (!res)
				alpha += 1 * 3.14 / 180; // +1*
			if(alpha > (start_alpha +6.28) )
				break;
		} while (!res);

		// if safe distance is find
		
		vx = temp.vx;
		vy = temp.vy;

		return;
	}




	// find enemy in a safe radius with min distance to tank
	Circle *min_enemy = nullptr;
	double min_distance;
	for (auto it = objectStorage.begin(); it != objectStorage.end(); it++)
	{
		min_distance = pow(((*it)->x - x), 2) + pow(((*it)->y - y), 2);
		// not me
		if (min_distance == 0)
			continue;
		if (min_distance <= pow(safe_distance, 2))
		{
			if (min_enemy == nullptr || (pow((min_enemy->x - x), 2) + pow((min_enemy->y - y), 2)) > min_distance)
				min_enemy = *it;
				
			auto temp = it;
			temp++;

			if (temp != objectStorage.end())
			{
				double temp_dist = pow(((*temp)->x - x), 2) + pow(((*temp)->y - y), 2);
				if (min_distance > temp_dist)
				{
					min_distance = temp_dist;
					min_enemy = *temp;
				}
			}
		}
	}

	// if enemy not found
	if (min_enemy == nullptr)
	{
		// find robot in a safe radius + robot radius with min distance to tank
		Circle *min_enemy = nullptr;
		double min_distance;
		for (auto it = objectStorage.begin(); it != objectStorage.end(); it++)
		{
			min_distance = pow(((*it)->x - x), 2) + pow(((*it)->y - y), 2);
			// not me
			if (min_distance == 0)
				continue;
			if (min_distance <= pow(safe_distance * robot_radius * 5, 2))
			{
				if ((min_enemy == nullptr || (pow((min_enemy->x - x), 2) + pow((min_enemy->y - y), 2)) > min_distance) && ((*it)->v == robot_speed))
					min_enemy = *it;

				auto temp = it;
				temp++;

				if (temp != objectStorage.end())
				{
					double temp_dist = pow(((*temp)->x - x), 2) + pow(((*temp)->y - y), 2);
					if (min_distance > temp_dist && (*temp)->v == robot_speed)
					{
						min_distance = temp_dist;
						min_enemy = *temp;
					}
				}
			}
		}
		// if enemy is found
		if (min_enemy != nullptr && dynamic_cast<InterfaceRobort*>(min_enemy) && !dynamic_cast<SuslikiRobot*>(min_enemy) && energy > 50)
		{
			double t, a, b, c, D;
			a = pow(min_enemy->v, 2) - pow(bolt_speed, 2);
			b = 2 * ((min_enemy->x - x) * min_enemy->vx + (min_enemy->y - y) * min_enemy->vy);
			c = pow((min_enemy->x - x), 2) + pow((min_enemy->y - y), 2);
			D = pow(b, 2) - 4 * a * c;
			if (D >= 0 /*&& energy > 100*/)
			{
				t = (-b + sqrt(D)) / (2 * a);
				if (t < 0)
					t = (-b - sqrt(D)) / (2 * a);
				double x_bolt, y_bolt;
				x_bolt = min_enemy->x + min_enemy->vx * t;
				y_bolt = min_enemy->y + min_enemy->vy * t;
				Circle::objectStorage.push_back(new Bolt(x, y, x_bolt, y_bolt, 10, this));
				energy -= 10; // -energy = 10
			}
		}


		VX_VY temp(0, 0);
		double new_x, new_y;
		double alpha;
		double start_alpha;
		bool res;
		new_x = x + vx / (5.0 / FPS);
		new_y = y + vy / (5.0 / FPS);
		res = CheckSafe(new_x, new_y);
		if (!res)
		{
			alpha = (rand() % 359 + 1) * 3.14 / 180; 
			start_alpha = alpha;
		}
		else
			return;

		while(res == false)
		{
			temp = Turn(alpha);
			new_x = x + temp.vx / (5.0 / FPS);
			new_y = y + temp.vy / (5.0 / FPS);
			res = CheckSafe(new_x, new_y);
			if (res == false)
				alpha += 1 * 3.14 / 180; // +1*
			if(alpha > start_alpha + (2*3.14))
				break;
		} 

		// main - найти место с наименьшей опасностью!!!

		//temp
		vx = temp.vx;
		vy = temp.vy;

		return;
	}


	
	// if min enemy is found
	// if robot - make the shot
	if (dynamic_cast<InterfaceRobort*>(min_enemy) && !dynamic_cast<SuslikiRobot*>(min_enemy) && energy > 50)
	{
		double t, a, b, c, D;
		a = pow(min_enemy->v, 2) - pow(bolt_speed, 2);
		b = 2 * ((min_enemy->x - x) * min_enemy->vx + (min_enemy->y - y) * min_enemy->vy); 
		c = pow((min_enemy->x - x), 2) + pow((min_enemy->y - y), 2);
		D = pow(b, 2) - 4 * a * c;
		if(D >= 0 /*&& energy > 100*/)
		{
			t = (-b - sqrt(D)) / (2 * a);
			double x_bolt, y_bolt;
			x_bolt = min_enemy->x + min_enemy->vx * t;
			y_bolt = min_enemy->y + min_enemy->vy * t;
			Circle::objectStorage.push_back(new Bolt(x, y, x_bolt, y_bolt, 10, this));
			energy -= 10; // -energy = 10
		}
	}
	// retreat
	double a = pow(vx, 2) - 2 * vx * min_enemy->vx + pow(min_enemy->vx, 2) + pow(vy, 2) -
		2 * vy * min_enemy->vy + pow(min_enemy->vy, 2);
	double b = 2 * x * vx - 2 * x * min_enemy->vx - 2 * min_enemy->x * vx + 2 * min_enemy->x * min_enemy->vx +
		2 * y * vy - 2 * y * min_enemy->vy - 2 * min_enemy->y * vy + 2 * min_enemy->y * min_enemy->vy;
	double c = pow(x, 2) - 2 * x * min_enemy->x + pow(min_enemy->y, 2) - 2 * y * min_enemy->y +
		pow(min_enemy->y, 2) - pow(robot_radius, 2);

	// if the motions path are intersects
	if (pow(b, 2) >= 4 * a * c)
	{
		VX_VY temp(0, 0);
		double new_x, new_y;
		double alpha = (rand() % 360 + 1) * 3.14 / 180;
		double start_alpha = alpha;
		bool res;
		do
		{
			temp = Turn(alpha);
			new_x = x + temp.vx / (5.0 / FPS);
			new_y = y + temp.vy / (5.0 / FPS);
			res = CheckSafe(new_x, new_y);
			if (!res)
				alpha += 1 * 3.14 / 180; // +1*
			if(alpha > (start_alpha+6.28))
				break;
		} while(!res);

		if (res)
		{
			vx = temp.vx;
			vy = temp.vy;
		}
		else
		{
			temp = Turn((rand() % 360 + 1) * 3.14 / 180);
			vx = temp.vx;
			vy = temp.vy;
		}
		return;
	}
	
	
	if(min_enemy->v == robot_speed)
	{
		VX_VY temp(0, 0);
		double new_x, new_y;
		double alpha = (rand() % 360 + 1) * 3.14 / 180;
		double start_alpha = alpha;
		bool res;
		do
		{
			temp = Turn(alpha);
			new_x = x + temp.vx / (5.0 / FPS);
			new_y = y + temp.vy / (5.0 / FPS);
			res = CheckSafe(new_x, new_y);
			if (!res)
				alpha += 1 * 3.14 / 180; // +1*
			if(alpha == start_alpha)
				break;
		} while(!res);
	}
	
}



bool SuslikiRobot::CheckSafe(double new_x, double new_y)
{
	if (new_x + safe_distance >= arenaXSize || new_y + safe_distance >= arenaYSize || new_x - safe_distance <= 0 || new_y - safe_distance <= 0)
		return false;

	Circle *min_enemy = nullptr;
	// find enemy in a safe radius with min distance to tank
	for (auto it = objectStorage.begin(); it != objectStorage.end(); it++)
	{
		double min_distance = pow(((*it)->x - new_x), 2) + pow(((*it)->y - new_y), 2);
		if (min_distance == 0)
			continue;
		if (min_distance <= pow(safe_distance, 2))
		{
			min_enemy = *it;
			auto temp = it;
			temp++;

			if (temp != objectStorage.end())
			{
				double temp_dist = pow(((*temp)->x - new_x), 2) + pow(((*temp)->y - new_y), 2);
				if (min_distance > temp_dist)
				{
					min_distance = temp_dist;
					min_enemy = *temp;
				}
			}
		}
	}

	if (!min_enemy)
		return true; //safe

	// if bolt
	if (min_enemy->v == bolt_speed)
	{
		double a = pow(vx, 2) - 2 * vx * min_enemy->vx + pow(min_enemy->vx, 2) + pow(vy, 2) -
			2 * vy * min_enemy->vy + pow(min_enemy->vy, 2);
		double b = 2 * x * vx - 2 * x * min_enemy->vx - 2 * min_enemy->x * vx + 2 * min_enemy->x * min_enemy->vx +
			2 * y * vy - 2 * y * min_enemy->vy - 2 * min_enemy->y * vy + 2 * min_enemy->y * min_enemy->vy;
		double c = pow(x, 2) - 2 * x * min_enemy->x + pow(min_enemy->y, 2) - 2 * y * min_enemy->y +
			pow(min_enemy->y, 2) - pow(robot_radius, 2);

		// if the motions path are not intersects
		if (pow(b, 2) < 4 * a * c)
			return true;
	}
	return false;
}


SuslikiRobot::VX_VY SuslikiRobot::Turn(double alpha)
{
	return VX_VY( vx*cos(alpha) - vy*sin(alpha), vx*sin(alpha) + vy*cos(alpha) );
}



void SuslikiRobot::Draw(HDC hdc) {

	//COLORREF qRobotColor = RGB(0, 200, 200);
	//HPEN hLinePen = CreatePen(PS_SOLID, 5, qRobotColor);
	HPEN hPenOld = (HPEN)SelectObject(hdc, line::pen1);
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