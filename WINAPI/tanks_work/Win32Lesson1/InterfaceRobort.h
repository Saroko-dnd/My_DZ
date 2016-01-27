#pragma once
#include "Circle.h"



class InterfaceRobort :
	public Circle
{
public:

	virtual void makeDecision()=0;

	InterfaceRobort(double init_x, double init_y) : Circle(init_x, init_y, robot_speed, robot_energy, robot_radius){};
	virtual ~InterfaceRobort(){}
	//virtual void Draw() {}
};

