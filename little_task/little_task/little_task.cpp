// little_task.cpp : Defines the entry point for the console application.
//
#include <iostream>
#include <vector>
#include <numeric>
#include <algorithm>
#include <sstream>
#include <string>
#include <cmath>
#include <regex>

using namespace std;

int main()
{
	string our_string;
	//stringstream our_stringstream;
	vector<int> our_vector{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 15, 16,17 };
	//количество чисел делящихся на 3
	int mycount = count_if(our_vector.begin(), our_vector.end(), [](int a){if (a % 3==0)return true; else return false; });
	cout << "(int%3) result:" << mycount << endl;
	mycount = 0;
	mycount = accumulate(our_vector.begin(), our_vector.end(), mycount, [](int all, int current)
		{int a = 2;
	while (current%a != 0 && a <= current / 2){ ++a; }if ((current%a != 0 && current!=1) || current==2){ cout << current << endl; return all + current; }
	else return all; }
		);
	cout << "(simple) result:" << mycount << endl;
	our_string = accumulate(our_vector.begin(), our_vector.end(), our_string, [](string all, int current)
		{
			stringstream our_stringstream;
			our_stringstream << current << ';';
			return all + our_stringstream.str();
		}
		);
	cout << "(list) result:" << our_string << endl;
	our_string.resize(0);
	our_string/*или вектор из интов*/ = accumulate(our_vector.begin(), our_vector.end(), our_string/*или вектор из интов*/, [](string all/*или vector<int>*/, int current)
	{
		static int prev=0;
		static bool first = true;
		stringstream our_stringstream;
		if (first)
		{
			our_stringstream << current << ':';
			first = false;
			prev = current;
			return all + our_stringstream.str();
		}
		else
		{
			if (abs(prev - current) > 1)
			{
				our_stringstream << current << ':';
				prev = current;
				return all + our_stringstream.str();
			}
			else
			{
				prev = current;
				return all;
			}
		}
		//или для вектора делаем в соответсвующих местах push back
	}
	);
	cout << "(list of numbers differents) result:" << our_string << endl;

	//тестирование работы лямбда функции с явным указанием возращаемого значения (сейчас это double)
	double my_double=5.0;
	my_double = [](double a)->double{return a + 10.5; }(my_double);
	cout << my_double << endl;
	cout << [](double a)->double{return a + 10.5; }(my_double);
	cout << "\nsearch for d\\d\n" << endl;
	string string_numbers("subject12.3sub4.5ject10089.00.rrr1111.2345.3789.1234ttt");
	regex search_numbers("\\d+.\\d+");
	smatch our_smath;
	regex_search(string_numbers, our_smath, search_numbers);


	while (std::regex_search(string_numbers, our_smath, search_numbers)) 
	{
		for (auto x : our_smath) std::cout << x << " ";
		std::cout << std::endl;
		string_numbers = our_smath.suffix().str();
	}

	cout << "search for d.d.d.d\n" << endl;
	string_numbers="subject12.3sub4.5ject100.89.00rrr111.234.378.123ttt";
	search_numbers="\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}";

	while (std::regex_search(string_numbers, our_smath, search_numbers))
	{
		for (auto x : our_smath) std::cout << x << " ";
		std::cout << std::endl;
		string_numbers = our_smath.suffix().str();
	}

	cout << "search for mail\n" << endl;
	string_numbers = "subject12.3sub4.5je23tttdraft@mail.com hct100.89.00rrr111.234.378.123tttdraft@mail.ruhfg";
	search_numbers = "(?:\\w+\\.)+\\w+@(?:\\w+\\.)+\\w+\\b";

	while (std::regex_search(string_numbers, our_smath, search_numbers))
	{
		for (auto x : our_smath) std::cout << x << " ";
		std::cout << std::endl;
		string_numbers = our_smath.suffix().str();
	}

	cout << "search for question\n" << endl;
	string_numbers = "1,fgh;4,4;6,3\n";
	search_numbers = "\\d+,\\w+(?:;\\d+,\\d+)+\\n";//?: - это символы, означающие, что содержимое скобки не должно искаться отдельно от остального
	//выражения
	while (std::regex_search(string_numbers, our_smath, search_numbers))
	{
		for (auto x : our_smath) 
		std::cout << x << " ";
		std::cout << std::endl;
		string_numbers = our_smath.suffix().str();
	}

	//небольшой тест по полоучению строки из smatch
	cout << "search for question\n" << endl;
	string_numbers = "1,fgh;4,4;6,3\n";
	search_numbers = "\\d+";//?: - это символы, означающие, что содержимое скобки не должно искаться отдельно от остального
	//выражения
	string copy;
	regex_search(string_numbers, our_smath, search_numbers);
	copy = our_smath[0];

	cout << "our string" << copy << endl;

	cin >> mycount;

	return 0;
}

