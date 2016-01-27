#include "line.h"
#include "stdafx.h"

HDC line::hdc;
HPEN line::pen1;
HPEN line::pen2;
HPEN line::pen3;
HPEN line::pen4;
HPEN line::pen5;



void line::Draw()
{
	MoveToEx(hdc, x1, y1, nullptr);
	LineTo(hdc, x2, y2);
}
int line::indent = 0;
