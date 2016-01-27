#ifndef ASSOCIATIVE_ARRAY_H_INCLUDED
#define ASSOCIATIVE_ARRAY_H_INCLUDED

#include <iostream>
#include <cstdlib>
#include <string>

class assot_array
{
    private:
    std::string* str_array;
    double* double_array;
    int count_;
    int counter;
    public:
    assot_array()
    {
        str_array=NULL;
        double_array=NULL;
        count_=0;
    }
    assot_array(int length)
    {
        str_array=(std::string*)malloc(length*sizeof(std::string));
        double_array=(double*)malloc(length*sizeof(double));
        count_=length;
        counter=0;
    }
    assot_array(assot_array& add)
    {
        str_array=(std::string*)malloc(add.count_*sizeof(std::string));
        double_array=(double*)malloc(add.count_*sizeof(double));
        count_=add.count_;
        counter=add.counter;
        int count_num=0;
        while (count_num<count_)
        {
            str_array[count_num]=add.str_array[count_num];
            double_array[count_num]=add.double_array[count_num];
            ++count_num;
        }
    }
    ~assot_array()
    {
        if (str_array!=NULL && double_array!=NULL)
        {
            free(str_array);
            free(double_array);
        }
    }
    double operator[](std::string);
    std::string operator[](double);
    void add_element(std::string,double);

};

#endif // ASSOCIATIVE_ARRAY_H_INCLUDED
