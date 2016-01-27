#pragma once
#include "Circle.h"
#include "Bolt.h"
#include "Definitions.h"
#include "InterfaceRobort.h"



class SuslikiRobot : public InterfaceRobort
{
public:
	SuslikiRobot(double init_x, double init_y)
		: InterfaceRobort(init_x, init_y) {};

	virtual void makeDecision();
	/*void shoot(double target_x, double target_y, int energy)
	{
		new Bolt(x, y, target_x, target_y, energy, );
		// add it to array
	}*/
	bool CheckSafe(double new_x, double new_y);

	struct VX_VY
	{
		double vx, vy;
		VX_VY(double vx_, double vy_) : vx(vx_), vy(vy_) {}
	};

	VX_VY Turn(double alpha);
	//static int direction = 1;

	virtual void Draw(HDC hdc);
};