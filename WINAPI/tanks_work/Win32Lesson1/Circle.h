#pragma once
#include <cmath>
#include <list>
#include "Definitions.h"
#include <Windows.h>



static int id_generate = 1;

class Circle {
protected:
	Circle(double init_x, double init_y, double init_v, int init_e, double r)
		: x(init_x), y(init_y), v(init_v), energy(init_e), vx(0), vy(0), radius(r) {
		id = id_generate++;
	}
public:
	double radius;
	int id;
	// êîîðäèíàòû
	double x, y,
		// âåêòîð òåêóùåé ñêîðîñòè
		vx, vy;
	// ðàçðåø¸ííàÿ ñêîðîñòü
	const double v;
	int energy;
	// ïåðåìåñòèòü îáúåêò â íîâóþ ïîçèöèþ 
	// â ñîîòâåòñòâèè ñ åãî âåêòîðîì ñêîðîñòè
	void move(double deltaT) {

		x += vx*deltaT;
		y += vy*deltaT;

	}
	// çàäàòü ñêîðîñòü â çàäàííîì íàïðàâëåíèè
	void moveToPoint(double init_x, double init_y) {
		double xx = (init_x - x);
		double yy = (init_y - y);
		double l = hypot(xx, yy);
		if (l == 0)
			return;
		vx = (xx / l*v);
		vy = (yy / l*v);
	}

	virtual void Draw(HDC hdc) = 0;



	static std::list<Circle*> objectStorage;
};

