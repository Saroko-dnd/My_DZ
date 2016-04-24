// dll_test.cpp : Defines the exported functions for the DLL application.
//

#include "dll_test_call.h"
#include <string>
#include <iostream>

using namespace std;

typedef struct our_struct
{
	int first_int;
	int second_int;
	wstring  current_string;
}our_struct;

int MultiplyByTen(our_struct current_struct)
{
	cout << current_struct.first_int + current_struct.second_int;
	wcout << current_struct.current_string;
}
