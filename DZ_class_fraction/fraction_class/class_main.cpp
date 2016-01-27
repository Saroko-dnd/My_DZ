#include "class_header.h"
#include <iostream>
#include <sstream>
#include <stdio.h>
#include <cmath>

using namespace std;

fraction::fraction(int up,int down)
{
    numerator=up;
    denominator=down;
}

fraction::fraction()
{
    numerator=1;
    denominator=2;
}

void fraction::operator+=(fraction& add)
{
    int b_copy=denominator;
    if (denominator!=add.denominator)
    {
        numerator*=add.denominator;
        denominator*=add.denominator;
        add.numerator*=b_copy;
        add.denominator*=b_copy;
    }
    numerator+=add.numerator;
}

void fraction::operator-=(fraction& subtract)
{
    int b_copy=denominator;
    if (denominator!=subtract.denominator)
    {
        numerator*=subtract.denominator;
        denominator*=subtract.denominator;
        subtract.numerator*=b_copy;
        subtract.denominator*=b_copy;
    }
    numerator-=subtract.numerator;
}

void fraction::operator*=(fraction& mpl)
{
    numerator*=mpl.numerator;
    denominator*=mpl.denominator;
}

void fraction::operator/=(fraction& div)
{
    numerator*=div.denominator;
    denominator*=div.numerator;
}

bool fraction::operator>(fraction& comp)
{
    int b_copy=denominator;
    if (denominator!=comp.denominator)
    {
        numerator*=comp.denominator;
        denominator*=comp.denominator;
        comp.numerator*=b_copy;
        comp.denominator*=b_copy;
    }
    if (numerator>comp.numerator)
        return true;
    else
        return false;
}

bool fraction::operator==(fraction& comp)
{
    int b_copy=denominator;
    if (denominator!=comp.denominator)
    {
        numerator*=comp.denominator;
        denominator*=comp.denominator;
        comp.numerator*=b_copy;
        comp.denominator*=b_copy;
    }
    if (numerator==comp.numerator)
        return true;
    else
        return false;
}

fraction fraction::operator+(fraction&first)
{
    fraction res(first.numerator,first.denominator);
    res+=first;
    return res;
}

fraction fraction::operator+(int number)
{
    fraction res(numerator,denominator);
    number*=denominator;
    res.numerator+=number;
    return res;
}

fraction fraction::operator+(float number)
{
    fraction res(numerator,denominator);
    char buf[18];
    float our = static_cast<float>(numerator)/denominator;
    our+=number;
    int int_part=static_cast<int>(our);
    int counter=0,fract_part=0,counter_main=0;
    sprintf(buf,"%f",our);
    while (buf[counter]!='\0')
    {
        ++counter;
    }
    --counter;
    while (buf[counter]=='0')
    {
        --counter;
    }
    while (buf[counter]!='.')
    {
        if (buf[counter]!='0' && buf[counter]!='.')
        fract_part+=(buf[counter]-48)*pow(10,counter_main);
        --counter;
        counter_main+=1;
    }
    res.numerator=int_part*pow(10,counter_main)+fract_part;
    res.denominator=pow(10,counter_main);
    if (res.denominator%10!=0 && res.denominator!=1)
    ++res.denominator;
    return res;
}

fraction fraction::operator-(float number)
{
    fraction res(numerator,denominator);
    char buf[18];
    float our = static_cast<float>(numerator)/denominator;
    our-=number;
    int int_part=static_cast<int>(our);
    int counter=0,fract_part=0,counter_main=0;
    sprintf(buf,"%f",our);
    while (buf[counter]!='\0')
    {
        ++counter;
    }
    --counter;
    while (buf[counter]=='0')
    {
        --counter;
    }
    while (buf[counter]!='.')
    {
        if (buf[counter]!='0' && buf[counter]!='.')
        fract_part+=(buf[counter]-48)*pow(10,counter_main);
        --counter;
        counter_main+=1;
    }
    res.numerator=int_part*pow(10,counter_main)+fract_part;
    res.denominator=pow(10,counter_main);
    if (res.denominator%10!=0 && res.denominator!=1)
    ++res.denominator;
    return res;
}

fraction fraction::operator*(float number)
{
    fraction res(numerator,denominator);
    char buf[18];
    float our = static_cast<float>(numerator)/denominator;
    our*=number;
    int int_part=static_cast<int>(our);
    int counter=0,fract_part=0,counter_main=0;
    sprintf(buf,"%f",our);
    while (buf[counter]!='\0')
    {
        ++counter;
    }
    --counter;
    while (buf[counter]=='0')
    {
        --counter;
    }
    while (buf[counter]!='.')
    {
        if (buf[counter]!='0' && buf[counter]!='.')
        fract_part+=(buf[counter]-48)*pow(10,counter_main);
        --counter;
        counter_main+=1;
    }
    res.numerator=int_part*pow(10,counter_main)+fract_part;
    res.denominator=pow(10,counter_main);
    if (res.denominator%10!=0 && res.denominator!=1)
    ++res.denominator;
    return res;
}

fraction fraction::operator/(float number)
{
    fraction res(numerator,denominator);
    char buf[18];
    float our = static_cast<float>(numerator)/denominator;
    our/=number;
    int int_part=static_cast<int>(our);
    int counter=0,fract_part=0,counter_main=0;
    sprintf(buf,"%f",our);
    while (buf[counter]!='\0')
    {
        ++counter;
    }
    --counter;
    while (buf[counter]=='0')
    {
        --counter;
    }
    while (buf[counter]!='.')
    {
        if (buf[counter]!='0' && buf[counter]!='.')
        fract_part+=(buf[counter]-48)*pow(10,counter_main);
        --counter;
        counter_main+=1;
    }
    res.numerator=int_part*pow(10,counter_main)+fract_part;
    res.denominator=pow(10,counter_main);
    if (res.denominator%10!=0 && res.denominator!=1)
    ++res.denominator;
    return res;
}

fraction fraction::operator-(int number)
{
    fraction res(numerator,denominator);
    number*=denominator;
    res.numerator-=number;
    return res;
}

fraction fraction::operator*(int number)
{
    fraction res(numerator,denominator);
    res.numerator*=number;
    return res;
}

fraction fraction::operator/(int number)
{
    fraction res(numerator,denominator);
    res.denominator*=number;
    return res;
}

fraction fraction::operator-(fraction&first)
{
    fraction res(first.numerator,first.denominator);
    res-=first;
    return res;
}

fraction fraction::operator*(fraction&first)
{
    fraction res(first.numerator,first.denominator);
    res*=first;
    return res;
}

fraction fraction::operator/(fraction&first)
{
    fraction res(first.numerator,first.denominator);
    res/=first;
    return res;
}

fraction fraction::operator-()
{
    fraction res(-numerator,denominator);
    return res;
}

bool fraction::operator!=(fraction& comp)
{
    fraction res(numerator,denominator);
    return res==comp ? false : true;
}

bool fraction::operator<=(fraction& comp)
{
    fraction res(numerator,denominator);
    return res>comp ? false:true;
}

bool fraction::operator>=(fraction& comp)
{
    fraction res(numerator,denominator);
    return res>comp || res==comp ? true:false;
}

bool fraction::operator<(fraction& comp)
{
    fraction res(numerator,denominator);
    return res>comp || res==comp ? false:true;
}

void fraction::PrintFraction()
{
    if (numerator==0)
    cout<<numerator<<endl;
    else if (numerator==denominator)
    cout<<"1"<<endl;
    else
    cout<<numerator<<"/"<<denominator<<endl;
}
