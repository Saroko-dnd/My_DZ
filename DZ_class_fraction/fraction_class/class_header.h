#ifndef CLASS_HEADER_H_INCLUDED
#define CLASS_HEADER_H_INCLUDED

#include <iostream>

class fraction
{
private:
    int numerator;
    int denominator;
public:
    fraction();
    fraction(int,int);
    void PrintFraction();
    void operator+=(fraction&);
    void operator-=(fraction&);
    void operator*=(fraction&);
    void operator/=(fraction&);
    fraction operator+(fraction&);
    fraction operator+(int);
    fraction operator+(float);
    fraction operator-(fraction&);
    fraction operator-(int);
    fraction operator-(float);
    fraction operator*(fraction&);
    fraction operator*(int);
    fraction operator*(float);
    fraction operator/(fraction&);
    fraction operator/(int);
    fraction operator/(float);
    fraction operator-();
    bool operator!=(fraction&);
    bool operator==(fraction&);
    bool operator<=(fraction&);
    bool operator>=(fraction&);
    bool operator>(fraction&);
    bool operator<(fraction&);
};

#endif // CLASS_HEADER_H_INCLUDED
