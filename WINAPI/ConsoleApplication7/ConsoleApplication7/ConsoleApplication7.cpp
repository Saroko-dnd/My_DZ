// ConsoleApplication7.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <stdlib.h>
#include <time.h>


void findmax(int mas1[][3] ,int size1, int size2 )
{
	
	int row = 0, i, j, max;
	srand((unsigned int)time(NULL));
	max = mas1[0][0];
	for (i = 0; i < size1; ++i)
	{
		for (j = 0; j < size2; ++j)
		{
			if (max < mas1[i][j])
			{
				max = mas1[i][j];
				row = i;
			}
		}
	}
	printf("max=%d\n", max);
	printf("line=%d\n", row);
}

void findmax(int mas1[][5], int size1, int size2)
{

	int row = 0, i, j, max;
	srand((unsigned int)time(NULL));
	max = mas1[0][0];
	for (i = 0; i < size1; ++i)
	{
		for (j = 0; j < size2; ++j)
		{
			if (max < mas1[i][j])
			{
				max = mas1[i][j];
				row = i;
			}
		}
	}
	printf("max=%d\n", max);
	printf("line=%d\n", row);
}



int _tmain(int argc, _TCHAR* argv[])
{	
	const int size1 = 2, size2 = 3, size3 = 4, size4 = 5;
	int i, j,  mas1[size1][size2], mas2[size3][size4];
	srand(time(NULL));
	
	for (i = 0; i < size1; ++i)
	{
		for (j = 0; j < size2; ++j)
		{
            mas1[i][j] = rand() % 10;
		}
	}

	findmax(mas1, size1, size2);

	for (i = 0; i < size3; ++i)
	{
		for (j = 0; j < size4; ++j)
		{
			mas2[i][j] = rand() % 10;
		}
	}

	findmax(mas2, size3, size4);

	return 0;
}

