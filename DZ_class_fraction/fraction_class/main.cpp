#include <iostream>
#include "class_header.h"
#include <string>
#include <sstream>
#include <stdio.h>
#include <cmath>

using namespace std;

int main()
{
    float d=0.5;
    int a_=10,b_=0,c_;
    c_=pow(a_,b_);
    fraction a(1,3),b(1,3),c(1,2);
    c=c/d;
    cout<<"result:"<<endl;
    c.PrintFraction();
    c=-a;
    c.PrintFraction();
    c=b+a;
    c.PrintFraction();
    c=a+b;
    c.PrintFraction();
    if (a==b)
    cout<<"Hey!!!"<<endl;
    a/=b;
    a.PrintFraction();
    return 0;
}



