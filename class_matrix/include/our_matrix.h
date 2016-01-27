#ifndef OUR_MATRIX_H
#define OUR_MATRIX_H

#include <vector>
#include <iostream>
#include <cmath>
#include <initializer_list>

template <typename Type>
class our_matrix
{
    private:
        std::vector<Type> matrix;
        unsigned int size_;
        unsigned int str_;
        unsigned int col_;
    public:
        void print();
        void rotate(double);
        void operator =(our_matrix);
        bool operator ==(our_matrix);
        bool operator !=(our_matrix);
        our_matrix operator +(our_matrix);
        void operator+=(our_matrix);
        our_matrix operator -(our_matrix);
        void operator-=(our_matrix);
        our_matrix operator *(our_matrix);
        void operator*=(our_matrix);
        class iterator
        {
        private:
            std::vector<Type> line;
        public:
            Type operator[](unsigned int);
            iterator (std::vector<Type> to_line)
            {
                line=to_line;
            }
        };
        iterator operator [](unsigned int);
        our_matrix (std::vector<Type> our_vector,unsigned int str, unsigned int col)
        {
            if ((str*col)!=our_vector.size())
                std::cout<<"Error! Matrix created incorrect! We recommend you restart the program"<<std::endl;
            size_=our_vector.size();
            str_=str;
            col_=col;
            matrix=our_vector;
        };
        our_matrix(int size,int str,int col)
        {
            if ((str*col)!=size)
                std::cout<<"Error! Matrix created incorrect! We recommend you restart the program"<<std::endl;
            size_=size;
            str_=str;
            col_=col;
            matrix(size,0);
        };
        our_matrix(double rad_angle)
        {
            size_=9;
            str_=3;
            col_=3;
            matrix(9);
            matrix[0]=cos(rad_angle);
            matrix[1]=0.0;
            matrix[2]=-sin(rad_angle);
            matrix[3]=0.0;
            matrix[4]=1.0;
            matrix[5]=0.0;
            matrix[6]=sin(rad_angle);
            matrix[7]=0.0;
            matrix[8]=cos(rad_angle);
        };
        our_matrix(size_t str, size_t col, std::initializer_list<Type> list)
        {
            if (str*col!=list.size())
                 std::cout<<"Error! Matrix created incorrect! We recommend you restart the program"<<std::endl;
            matrix;
            str_=str;
            col_=col;
            size_=str*col;
            unsigned int i=0;
            for (auto it = list.begin(); it != list.end(); ++it)
            {
                matrix.push_back(*it);
            }
        };
        our_matrix(size_t str, size_t col, std::vector<Type> our_vector)
        {
            if (str*col!=our_vector.size())
                 std::cout<<"Error! Matrix created incorrect! We recommend you restart the program"<<std::endl;
            matrix=our_vector;
            str_=str;
            col_=col;
            size_=str*col;
        };
        ~our_matrix()
        {
        };
};



template <typename Type>
void our_matrix<Type>::operator=(our_matrix another_matrix)
{
    matrix=another_matrix.matrix;
    str_=another_matrix.str_;
    col_=another_matrix.col_;
    size_=another_matrix.size_;
}

template <typename Type>
bool our_matrix<Type>::operator==(our_matrix another_matrix)
{
    if (matrix==another_matrix.matrix)
        return true;
    else
        return false;
}

template <typename Type>
bool our_matrix<Type>::operator!=(our_matrix another_matrix)
{
    if (matrix!=another_matrix.matrix)
        return true;
    else
        return false;
}

template <typename Type>
our_matrix<Type> our_matrix<Type>::operator+(our_matrix another_matrix)
{
    try
    {
        if (str_!=another_matrix.str_ || col_!=another_matrix.col_)
            throw 1;

        else
        {
            for (int counter=0;counter<size_;++counter)
            {
                matrix[counter]+=another_matrix.matrix[counter];
            }
            return this;
        }
    }
    catch(int res)
    {
        std::cout<<"Error in (operator +) - cant do this with different matrix"<<std::endl;
    }
}

template <typename Type>
void our_matrix<Type>::operator+=(our_matrix another_matrix)
{
    if (str_!=another_matrix.str_ || col_!=another_matrix.col_)
        std::cout<<"Error in (operator +=) - cant do this with different matrix"<<std::endl;
    else
    {
        for (int counter=0;counter<size_;++counter)
        {
            matrix[counter]+=another_matrix.matrix[counter];
        }
    }
}

template <typename Type>
our_matrix<Type> our_matrix<Type>::operator-(our_matrix another_matrix)
{
    try
    {
        if (str_!=another_matrix.str_ || col_!=another_matrix.col_)
            throw 1;

        else
        {
            for (int counter=0;counter<size_;++counter)
            {
                matrix[counter]-=another_matrix.matrix[counter];
            }
            return this;
        }
    }
    catch(int res)
    {
        std::cout<<"Error in (operator -) - cant do this with different matrix"<<std::endl;
    }
}

template <typename Type>
void our_matrix<Type>::operator-=(our_matrix another_matrix)
{
    if (str_!=another_matrix.str_ || col_!=another_matrix.col_)
        std::cout<<"Error in (operator -=) - cant do this with different matrix"<<std::endl;
    else
    {
        for (int counter=0;counter<size_;++counter)
        {
            matrix[counter]-=another_matrix.matrix[counter];
        }
    }
}

template <typename Type>
our_matrix<Type> our_matrix<Type>::operator*(our_matrix another_matrix)
{
    try
    {
        if (col_!=another_matrix.str_)
            throw 1;

        else
        {
            char g;
            int counter_2,counter_3,counter_4=0,counter_5=0;
            unsigned int size_of_buf=str_*another_matrix.col_;
            std::vector<Type> buf(size_of_buf,0);
            for (int counter=0;counter<str_;++counter)
            {
                counter_2=0;
                while (counter_2<another_matrix.col_)
                {
                    counter_3=0;
                    counter_5=0;
                    while (counter_3<col_)
                    {
                        buf[counter_4]+=matrix[((size_/str_)*counter)+counter_3]*another_matrix.matrix[((another_matrix.size_/another_matrix.str_)*counter_5)+counter_2];
                        ++counter_3;
                        ++counter_5;
                    }
                    ++counter_4;
                    ++counter_2;
                }
            }
            our_matrix<Type> buf_matrix(str_,another_matrix.col_,buf);
            return buf_matrix;
        }
    }
    catch(int res)
    {
        std::cout<<"Error in (operator *) - cant do this with such matrix"<<std::endl;
    }
}

template <typename Type>
void our_matrix<Type>::operator*=(our_matrix another_matrix)
{
    if (str_!=another_matrix.str_ || col_!=another_matrix.col_)
        std::cout<<"Error in (operator *=) - cant do this with different matrix"<<std::endl;
    else
    {
        int counter_2,counter_3,counter_4=0,counter_5=0;
        unsigned int size_of_buf=str_*another_matrix.col_;
        std::vector<Type> buf(size_of_buf,0);
        for (int counter=0;counter<str_;++counter)
        {
            counter_2=0;
            while (counter_2<another_matrix.col_)
            {
                counter_3=0;
                counter_5=0;
                while (counter_3<col_)
                {
                    buf[counter_4]+=matrix[((size_/str_)*counter)+counter_3]*another_matrix.matrix[((another_matrix.size_/another_matrix.str_)*counter_5)+counter_2];
                    ++counter_3;
                    ++counter_5;
                }
                ++counter_4;
                ++counter_2;
            }
        }
        matrix=buf;
        col_=another_matrix.col_;
    }
}

template <typename Type>
typename our_matrix<Type>::iterator our_matrix<Type>::operator [](unsigned int str_number)
{
    std::vector<Type> buf(size_/str_);
    for (int counter=0;counter<size_/str_;++counter)
    {
        buf[counter]=matrix[((size_/str_)*str_number)+counter];
    }
    our_matrix<Type>::iterator new_iterator(buf);
    return new_iterator;
}

template <typename Type>
Type our_matrix<Type>::iterator::operator [](unsigned int col_number)
{
    Type result=line[col_number];
    return result;
}

template <typename Type>
void our_matrix<Type>::print()
{
    for (int counter=0;counter<str_;++counter)
    {
        for (int counter_2=0;counter_2<col_;++counter_2)
        {
           std::cout<<matrix[((size_/str_)*counter)+counter_2]<<" ";
        }
        std::cout<<"\n";
    }
    std::cout<<"hhh"<<str_<<"--"<<col_<<std::endl;
}

template <typename Type>
void our_matrix<Type>::rotate(double rad_angle)
{
    our_matrix<double> rotation_matrix(3,3,{cos(rad_angle),0.0,-sin(rad_angle),0.0,1.0,0.0,sin(rad_angle),0.0,cos(rad_angle)});
    *this=rotation_matrix*(*this);
}

#endif // OUR_MATRIX_H
