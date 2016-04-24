// test_of_speed_for_C++_for_cmp_with_NET.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>;
#include <fstream>;
#include <chrono>;
#include <string>;
#include <list>;

using namespace std;
using namespace std::chrono;

class test_class
{
public: 
	int first_int = 79;
	int second_int = 98;
	string test_string = "this is test";
};

static bool checkForPrimality(int number)
{
	if (number > 0)
	{
		for (int i = 2; i * i <= number; i++)
		{
			if (number % i == 0)
			{
				return false;
			}
		}
		return true;
	}
	return false;
}

int _tmain(int argc, _TCHAR* argv[])
{
	srand(time(NULL));
	ofstream fout("simple_numbers.txt");
	int buf_number = 0;
	system_clock::time_point first_time = system_clock::now();
	for (int counter = 0; counter < 1000000; ++counter)
	{
		buf_number = rand();
		if (checkForPrimality(buf_number))
			fout << buf_number<<"\n";
		if (counter % 100000 == 0)
			cout << counter <<"\n";
	}
	fout.close();
	system_clock::time_point second_time = system_clock::now();
	long result_time = duration_cast<milliseconds>(second_time - first_time).count();
	cout << result_time;


	
	system_clock::time_point one_time = system_clock::now();
	list<test_class> list_of_tests;
	for (int counter = 0; counter < 1000000; ++counter)
	{
		list_of_tests.(new test_class());

		buf_number = rand();
		if (checkForPrimality(buf_number))
			fout << buf_number << "\n";
		if (counter % 100000 == 0)
			cout << counter << "\n";
	}
	fout.close();
	system_clock::time_point two_time = system_clock::now();
	long result_time_2 = duration_cast<milliseconds>(two_time - one_time).count();
	cout << result_time_2;
	cout << result_time_2;
	return 0;
}

