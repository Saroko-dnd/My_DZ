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
#include "init_questions_answers.h"
#include <cstdio>


using namespace std;

int main()
{
    ifstream file("my_data.csv");
    string f_1,f_2;
    int num;
    getline(file,f_1);
    cout<<f_1<<endl;
    getline(file,f_1,';');
    cout<<f_1<<endl;
    num=f_1.find('i');
    cout<<num<<endl;
    f_2=f_1.substr(0,1);
    cout<<f_2<<endl;
    num=stoi(f_2);
    file.close();
	system("pause");
    return 0;
}
