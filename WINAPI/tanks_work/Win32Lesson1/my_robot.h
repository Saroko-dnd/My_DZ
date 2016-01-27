#pragma once

class my_robot :
	public InterfaceRobort {
private:

	bool taran_flag = false;


public:

	std::string name;

	my_robot(double init_x, double init_y, std::string init_n) : name(init_n), InterfaceRobort(init_x, init_y) {};

	virtual void makeDecision();


	void shoots(double dis);
	void startStep();
	void pricel(Circle* it, double l);

	void shoot(my_robot* robot_, double target_x, double target_y, int energy)
	{
		Circle::objectStorage.push_back(new Bolt(x, y, target_x, target_y, energy, robot_));
	}

	void taran(Circle* it)
	{
		if (!(dynamic_cast<my_robot*>(it)))
			moveToPoint(it->x, it->y);//taran!! not energy 
	};
	double my_robot::chekL(double objx, double objy){ return hypot(objx - x, objy - y); };

	virtual ~my_robot(){};
	virtual void Draw(HDC hdc);
};

