#include "stdafx.h"
#include "Bolt.h"



void Bolt::Draw(HDC hdc)
{
	COLORREF qBoltColor = RGB(255, 0, 0);
	HPEN hLinePen = CreatePen(PS_SOLID, 5, qBoltColor);
	HPEN hPenOld = (HPEN)SelectObject(hdc, hLinePen);
	MoveToEx(hdc, x, y, nullptr);
	LineTo(hdc, x, y);
}