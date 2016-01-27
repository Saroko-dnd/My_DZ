#pragma once

#include "Circle.h"

class Bolt : public Circle {
public:
	Bolt(double init_x, double init_y, double target_x, double target_y, int energy) : Circle(init_x, init_y, bolt_speed, energy) { moveToPoint(target_x, target_y); }

	static const double bolt_speed;
};