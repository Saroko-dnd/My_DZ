// ConsoleApplication1.cpp : Defines the entry point for the console application.
//

#include <iterator>
#include <string>
#include <list>
#include <vector>
#include <sstream>
#include <iostream>

using namespace std;

int main()
{
	vector<int> our_vector;
	list<int> our_list;

	//сортировка выборкой
	auto first = our_vector.begin();
	auto buf = *first;
	auto current = first;
	auto second = our_vector.end();

	while (first != second)
	{
		++current;
		while (current != second)
		{
			if (*first > *current)
			{
				buf = *first;
				*first = *current;
				*current = buf;
			}
			++current;
		}
		++first;
		current = first;
	}

	//вывод содежимого контейнера на экран в формате [1,2,3]
	stringstream our_ssteaam;
	auto our_begin = our_vector.begin();

	our_ssteaam << '[';
	while (our_begin != our_vector.end())
	{
		our_ssteaam << *our_begin;
		our_ssteaam << ',';
		++our_begin;
	}
	our_ssteaam << ']';
	string result = our_ssteaam.str();
	cout << result << endl;
	return 0;
}

