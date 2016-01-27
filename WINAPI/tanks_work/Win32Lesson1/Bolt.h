#pragma once
#include "Circle.h"
#include "Definitions.h"
#include "InterfaceRobort.h"

class InterfaceRobort;

class Bolt : public Circle {

public:
	InterfaceRobort* robot;
	Bolt(double init_x, double init_y, double target_x, double target_y, int energy, InterfaceRobort* robot_)
		: Circle(init_x, init_y, bolt_speed, energy, 0), robot(robot_)
	{
		moveToPoint(target_x, target_y);
	}

	virtual void Draw(HDC hdc);
};