// ConsoleApplication1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <stdlib.h>
#include <time.h>
void rev(int mas[], int const);
int _tmain(int argc, _TCHAR* argv[])
{
	const int n = 10;
		int mas[n];
		for (int i = 0; i < n; ++i)
		{
			mas[n] = rand()%10;
			printf("%d ", mas[n]);
		}
	printf(\n);
	rev(mas, n);
	for (int i = 0; i < n; ++i)
		printf("%d ", mas[i]);

	return 0;
}
void rev(int mas[], int count)
{ 
	int temp;
	for (int i = 0; i < count / 2; ++i)
	{
		temp = mas[i];
		mas[i] = mas[count - i - 1];
		mas[count - i - 1] = temp;
	}
}

