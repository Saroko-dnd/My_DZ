// encoding.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <locale>
#include <iostream>
#include <fstream>
#include <string>
#include <string.h>
#include <vector>
#include <algorithm>
#include <locale>
#include <codecvt>

using namespace std;

bool compare_names(wstring, wstring);

bool compare_names(wstring first, wstring second)
{
	int counter = 0,counter_2=0;
	counter = first.find(L" ")+1;
	counter_2=second.find(L" ")+1;
	if (counter < 0)
		counter = 0;
	if (counter_2 < 0)
		counter_2 = 0;
	cout << counter<<"   "<<counter_2<< " positions_ready\n";
	return first[counter] < second[counter_2];
}

int _tmain(int argc, _TCHAR* argv[])
{
	vector<wstring> our_vector;
	wstring our_string(L"");
	wifstream our_file_in("names.txt");
	wofstream our_file_out("names_sort.txt");
	our_file_in.imbue(locale(our_file_in.getloc(), new codecvt_utf16<wchar_t, 0x10ffff, consume_header>));
	our_file_out.imbue(locale(our_file_out.getloc(), new codecvt_utf16<wchar_t, 0x10ffff, consume_header>));
	cout << "file_ready\n";

	int counter = 0;

	while (getline(our_file_in, our_string))//не берет 7-ю строку ни проверкой на 'eof' ни так
	{
		our_vector.push_back(our_string);
		cout <<"vector_\n";
	}
	cout << our_vector[7].size() << "vector_ready\n";
	our_file_in.close();

	sort(our_vector.begin(), our_vector.end(), compare_names);
	cout << our_vector[1].size()<< "sort_ready\n";
	
	wchar_t eln = 13,elr = 10;

	for (auto current = our_vector.begin(); current != our_vector.end(); ++current)
	{
		our_file_out << *current<<eln<<elr;//<<L"\n";//не работает корректно \n
		cout << "vector_\n";
	}

	our_file_out.close();
	cin >> counter;
	return 0;
}
