#include <iostream>
#include <string>
#include <vector>
#include <fstream>
#include "answer.h"
#include "addiction.h"
#include "profession.h"
#include "question.h"
#include "subject.h"
#include "test.h"
#include "init_addictions.h"
#include "init_profession.h"
#include "init_questions_answers.h"
#include <cstdio>


using namespace std;

int main()
{
	cout << "***WELCOME TO PROFESSION TEST***" << endl;
	cout << "NOTE: to choose your answer just enter his number from keyboard.\n(to start test just press 'enter')" << endl;
	getchar();
	if (!init_addictions())
	{
		getchar();
		return 0;
	}
	if (!init_profession())
	{
		getchar();
		return 0;
	}
	if (!init_questions_answers())
	{
		getchar();
		return 0;
	}
	system("cls");
	cout << "Please enter yor name:" << endl;
	string current_name;
	cin >> current_name;
	subject current_subject(current_name);
	test current_test(current_subject);
	bool test_switch=true;
	system("cls");
	while (test_switch)
	{
		test_switch = current_test.ask();
		system("cls");
	}
	current_test.show_results();
	getchar();
	return 0;
}