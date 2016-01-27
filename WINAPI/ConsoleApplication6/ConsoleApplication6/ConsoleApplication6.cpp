// ConsoleApplication6.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <stdlib.h>
#include <time.h>

int _tmain(int argc, _TCHAR* argv[])
{
	const int size1 = 2, size2 = 3, size3 = 4, size4 = 5;
	int str=0, i,j,max, mas1[size1][size2], mas2[size3][size4];
	srand(time(NULL));
	max = mas1[0][0];
	for (i = 0; i < size1; ++i)
	{
		for (j = 0; j < size2; ++j)
		{
			printf("%d ", mas1[i][j] = rand()%9);
			if (max < mas1[i][j])
			{
				max = mas1[i][j];
				str = i;
			}
				
		}
			printf("\n");
	}
	printf("\n");
	printf("%d ", str);
	printf("\n");
	printf("\n");
	for (i = 0; i < size3; ++i)
	{
		for (j = 0; j < size4; ++j)
		{
			printf("%d ", mas2[i][j] = rand()%10);
			if (max < mas2[i][j])
				max = mas2[i][j];
		}
			printf("\n");
	}
	printf("\n");
	printf("%d", max);
	printf("\n");
	return 0;
}

