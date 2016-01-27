#pragma once
#include <Windows.h>


class line
{

private:
	static HDC hdc;
public:
	static HPEN pen1;
	static HPEN pen2;
	static HPEN pen3;
	static HPEN pen4;
	static HPEN pen5;


	double x1, y1, x2, y2;
public:
	line(double x1, double y1, double x2, double y2):
		x1(x1), x2(x2), y1(y1), y2(y2) {}

	void Draw();

	static void SetHdc(HDC hdc_) 
	{
		hdc = hdc_;
	}
	static int indent;
};
