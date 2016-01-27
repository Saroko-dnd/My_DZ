#pragma once

#include <cmath>
#include <list>

class Circle {

protected:
	Circle(double init_x, double init_y, double init_v, int init_e) : x(init_x), y(init_y), v(init_v), energy(init_e) {};
public:
	double x, y, vx, vy;
	const double v;
	int energy;


	static std::list<Circle*> objectStorage;


	void move(double deltaT) {
		x += vx * deltaT;
		y += vy * deltaT;
	}

	void moveToPoint(double init_x, double init_y) {
		double xx = init_x - x, yy = init_y - y;
		double l = hypot(xx, yy);
		if (!l)
			return;
		vx = init_x / l * v;
		vy = init_y / l * v;
	}

	virtual ~Circle() {};
};
