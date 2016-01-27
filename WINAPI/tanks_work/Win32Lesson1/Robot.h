#pragma once
#include "Circle.h"
#include "Definitions.h"
#include "Bolt.h"
#include <list>
#include <string>
#include <vector>
#include "InterfaceRobort.h"




class Robot :
	public InterfaceRobort {
private: 

	bool taran_flag = false;
	

public:

	std::string name;

	Robot(double init_x, double init_y, std::string init_n) :name(init_n), InterfaceRobort(init_x, init_y) {};

	virtual void makeDecision();

	
	void shoots(double dis);
	void startStep();
	void pricel		 (Circle* it, double l);
	
	void shoot(Robot* robot_, double target_x, double target_y, int energy)
	{
		Circle::objectStorage.push_back(new Bolt(x, y, target_x, target_y, energy, robot_));
	}

	void taran(Circle* it)
	{
		if (!(dynamic_cast<Robot*>(it)))
			moveToPoint(it->x, it->y);//taran!! not energy 
	}; 
	double Robot::chekL(double objx,double objy){return hypot(objx - x, objy - y);};
	
	virtual ~Robot(){};
	virtual void Draw(HDC hdc);
};
