#include <stdio.h>
#include <stdlib.h>
#include "../../SarokoDZ/modules/funcRand.h"
#include "../../SarokoDZ/modules/Arrayss.h"

#define size_array 20

int array_1[size_array];
int location=0;

int main()
{
    int size=size_array-1;
    int check;
    printf("Хотите ввести массив сами? да-1 нет-2\n");
    scanf("%d",&check);
    switch (check)
    {
    case 1:
    array_input (size);
    break;
    case 2:
    array_input_Random (size);
    }
    if (array_search_last (size)==0)
    {
        printf("No -elements\n");
    }
    else
    {
    printf ("-Element %d has number %d\n",array_search_last (size),location);
    }

    location=0;
    if (array_search_first (size)==0)
    {
        printf("No +elements");
    }
    else
    {
    printf ("+Element %d has number %d\n",array_search_first (size),location);
    }
    return 0;
}

