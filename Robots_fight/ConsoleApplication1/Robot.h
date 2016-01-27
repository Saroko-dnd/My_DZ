
#pragma once

#include "Circle.h"
#include "Bolt.h"
#include "GlobalVariables.h"

class Robot : public Circle {
public:
	Robot(double init_x, double init_y) : Circle(init_x, init_y, robot_speed, robot_energy) {};

	void makeDecision();

	void shoot(double target_x, double target_y, int energy) {
		objectStorage.push_back(new Bolt(x + Robot::robot_radius, y + Robot::robot_radius, target_x, target_y, energy));
	}

	static const double robot_speed;  // pixels / sec
	static const double robot_radius; // pixels
	static const int    robot_energy; // initial energy

	static const double safeDestForBolts;
	static const double safeDestForRobots;
};