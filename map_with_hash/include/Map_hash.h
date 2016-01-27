#ifndef MAP_H
#define MAP_H

#include "../src/Map_hash.cpp"
#include "md5.h.h"
#include <windows.h>
#include <wincrypt.h>
#include <iostream>
#include <list>
#include <stdexcept>
#include <exception>
#include <cstring>
#include <cstdio>
#include <time.h>

int hash_sum(char key,int size)
{
    int result=key;
    return result%size;
}

int hash_sum(int new_key,int size)
{
    char buf [10];
    sprintf(buf,"%d",new_key);

    //std::cout<<"3"<<std::endl;
    std::string new_string=buf;
    // std::cout<<"4"<<std::endl;
    std::string result_string=md5(new_string);
    //std::cout<<"5"<<std::endl;
    const char* res=result_string.c_str();
    // std::cout<<"6"<<std::endl;
    int result_int=0;
    int length=strlen(res);
    int counter=0;
    while (counter<length)
    {
        result_int+=res[counter];
        ++counter;
    }
    // std::cout<<"7"<<std::endl;
    std::cout<<new_string<<" -> "<<result_string<<" -> "<<result_int<<std::endl;

    result_int%=size;
    // std::cout<<"8"<<std::endl;
    return result_int;
}

typedef struct not_found : std::exception
{
    const char* what() const noexcept
    {
        return "ERROR:element does not exist!.\n";
    }
} not_found;

template <typename Tkey,typename Tvalue>
class Map_with_hash
{
private:
    typedef struct pair
    {
        Tkey key;
        Tvalue value;
    } pair;
    pair element;
    std::list<pair> all_pairs;
    std::list<pair>* array_of_lists;
    int size;
    size_t index_first;
    typename std::list<pair>::iterator first;
    size_t index_last;
    typename std::list<pair>::iterator last;
    float check;
    float number;
    int swi;
public:
    class iterator
    {
    private:
        typename std::list<pair>::iterator current;
        size_t index_iter;
    public:
        void to_first();
        void to_last();
        void operator++();
        void operator--();
        Tvalue operator[](int);
        Tvalue operator*(iterator&);
        iterator ()
        {
            index_iter=0;
        }
    };
    void add_element(Tkey,Tvalue);
    Tvalue operator[](Tkey);
    void erase_element(Tkey);
    void erase_element(iterator&);
    Tkey operator[](Tvalue);
    void show_elements();
    float percent_of_coincidence();
    Map_with_hash(int max_size)
    {
        check=0.0;
        number=0.0;
        size=max_size*3;
        array_of_lists=new std::list<pair>[size];
        swi=0;
    }
    ~Map_with_hash()
    {
        delete array_of_lists;
    }
};

template <typename Tkey,typename Tvalue>
void Map_with_hash<Tkey,Tvalue>::add_element(Tkey key_new,Tvalue value_new)
{
    // std::cout<<"ooo"<<std::endl;
    element.key=key_new;
    // std::cout<<"1"<<std::endl;
    element.value=value_new;
    // std::cout<<"2"<<std::endl;
    int index=hash_sum(key_new,size);
    //std::cout<<"9"<<std::endl;
    if (array_of_lists[index].size()>0)
    {
        check+=1.0;
    }
    // std::cout<<"3"<<std::endl;
    number+=1.0;
    //std::cout<<"4"<<std::endl;
    array_of_lists[index].push_back(element);
    if (swi==0)
    {
        index_first=index;
        index_last=index;
        first=array_of_lists[index].begin();
        last=array_of_lists[index].begin();
        swi=1;
    }
    else
    if (index<index_first)
    {
        first=array_of_lists[index].begin();
        index_first=index;
    }
    else
    if (index>index_last)
    {
        index_last=index;
        last=array_of_lists[index].begin();
        // std::cout<<"5"<<std::endl;
    }
    else
    if(index==index_last)
        ++last;
}

template <typename Tkey,typename Tvalue>
float Map_with_hash<Tkey,Tvalue>::percent_of_coincidence()
{
    float result=check/number;
    return result;
}

template <typename Tkey,typename Tvalue>
Tvalue Map_with_hash<Tkey,Tvalue>::operator[](Tkey key_word)
{
    try
    {
        Tvalue result;
        int index=hash_sum(key_word,size);
        auto pointer=array_of_lists[index].begin();
        if (array_of_lists[index].size()==1)
        {
            return pointer->value;
        }
        else
        {
            while (key_word!=pointer->key && pointer!=array_of_lists[index].end())
            {
                ++pointer;
            }
            if (key_word==pointer->key)
            {
                return pointer->value;
            }
            else
                throw not_found();
        }
    }
    catch (std::exception& res)
    {
        std::cerr<<"(in operator[]) exception caught: "<<res.what();
    }
}

template <typename Tkey,typename Tvalue>
Tkey Map_with_hash<Tkey,Tvalue>::operator[](Tvalue value_word)
{
    try
    {
        Tkey result;
        int index=0;
        while (index<size)
        {
            auto pointer=array_of_lists[index].begin();
            if (array_of_lists[index].size()==1)
            {
                return pointer->key;
            }
            if (array_of_lists[index].size()>1)
            {
                while(pointer!=array_of_lists[index].end() && pointer->value!=value_word)
                {
                    ++pointer;
                }
                if (pointer->value==value_word)
                {
                    return pointer->key;
                }
            }
            ++index;
        }
        throw not_found();
    }
    catch (std::exception& res)
    {
        std::cerr<<"(in operator[]) exception caught: "<<res.what();
    }
}

template <typename Tkey,typename Tvalue>
void Map_with_hash<Tkey,Tvalue>::erase_element(Tkey key_word)
{
    try
    {
        int index=hash_sum(key_word,size);
        auto pointer=array_of_lists[index].begin();
        if (array_of_lists[index].size()==1)
        {
            array_of_lists[index].pop_back();
            if (index==index_first)
            {
                while (array_of_lists[index].size()<1 && index<size)
                {
                    ++index;
                }
                if (index<size)
                {
                    index_first=index;
                    first=array_of_lists[index].begin();
                }
                else
                    swi=0;
            }
            else if (index==index_last)
            {
                while (array_of_lists[index].size()<1 && index>=0)
                {
                    --index;
                }
                if (index>=0)
                {
                    index_last=index;
                    int size_copy=array_of_lists[index].size()-1;
                    last=array_of_lists[index].begin();
                    while (size_copy>0)
                    {
                        ++last;
                        --size_copy;
                    }
                }
                else
                    swi=0;
            }
        }
        else
        {
            if (index==index_last)
                --last;
            while (key_word!=pointer->key && pointer!=array_of_lists[index].end())
            {
                ++pointer;
            }
            if (key_word==pointer->key)
            {
                array_of_lists[index].erase(pointer);
            }
            else
                throw not_found();
        }
    }
    catch (std::exception& res)
    {
        std::cerr<<"(in erase_element) exception caught: "<<res.what();
    }
}

template <typename Tkey,typename Tvalue>
void Map_with_hash<Tkey,Tvalue>::show_elements()
{
    int index=0,count=1;
    while (index<size)
    {
        auto pointer=array_of_lists[index].begin();
        if (array_of_lists[index].size()==1)
        {
            std::cout<<pointer->key<<"-"<<pointer->value<<std::endl;
        }
        if (array_of_lists[index].size()>1)
        {
            while(pointer!=array_of_lists[index].end())
            {
                std::cout<<pointer->key<<"-"<<pointer->value<<std::endl;
                ++pointer;
            }
        }
        ++index;
    }
}

template <typename Tkey,typename Tvalue>
void Map_with_hash<Tkey,Tvalue>::iterator::operator++()
{
    ++current;
    if (current==array_of_lists[index_iter].end())
    {
        while (array_of_lists[index_iter].size()==0 && index_iter<size)
        {
            ++index_iter;
        }
        if (index_iter<size)
            current=array_of_lists[index_iter].begin();
        else
            index_iter=0;
            std::cout<<"error: out of size"<<std::endl;
    }
}

template <typename Tkey,typename Tvalue>
void Map_with_hash<Tkey,Tvalue>::iterator::operator--()
{
    if (current!=array_of_lists[index_iter].begin())
        --current;
    else
        {
            while (array_of_lists[index_iter].size()==0 && index_iter>=0)
            {
                --index_iter;
            }
            if (index_iter>=0)
            {
                int size_copy=array_of_lists[index_iter].size()-1;
                current=array_of_lists[index_iter].begin();
                while (size_copy>0)
                {
                    --size_copy;
                    ++current;
                }
            }
            else
            {
                index_iter=0;
                std::cout<<"error: out of size"<<std::endl;
            }
        }
}

template <typename Tkey,typename Tvalue>
void Map_with_hash<Tkey,Tvalue>::iterator::to_first()
{
        index_iter=index_first;
        current=first;
}

template <typename Tkey,typename Tvalue>
void Map_with_hash<Tkey,Tvalue>::iterator::to_last()
{
        index_iter=index_last;
        current=last;
}

template <typename Tkey,typename Tvalue>
Tvalue Map_with_hash<Tkey,Tvalue>::iterator::operator*(iterator& to_solve)
{
    Tvalue result;
    if (swi==1)
    {
        Tvalue result=to_solve.current->value;
        return result;
    }
    else
    {
        std::cout<<"ERROR: hash table is empty (returned trash)!"<<std::endl;
        return result;
    }
}

template <typename Tkey,typename Tvalue>
Tvalue Map_with_hash<Tkey,Tvalue>::iterator::operator[](int number)
{
    Tvalue result;
    current=first;
    index_iter=index_first;
    int num_copy=number-1;
    while (num_copy>=0)
    {
        ++current;
        if (current==array_of_lists[index_iter].end())
        {
            while (array_of_lists[index_iter].size()==0 && index_iter<size)
            {
                ++index_iter;
            }
            if (index_iter<size)
            {
                current=array_of_lists[index_iter].begin();
            }
            else
            {
                std::cout<<"ERROR: (operator []) cant find element (returned trash)!"<<std::endl;
                return result;
            }
        }
        --num_copy;
    }
    result=current->value;
    return result;
}

template <typename Tkey,typename Tvalue>
void Map_with_hash<Tkey,Tvalue>::erase_element(iterator& to_solve)
{
    try
    {
        if (swi==1)
        {
            typename std::list<pair>::iterator copy=to_solve.current;
            if (to_solve.current==first)
            {
                ++to_solve.current;
                if (to_solve.index_iter<size && array_of_lists[index_first].size()==1)
                {
                    first=to_solve.current;
                    index_first=to_solve.index_iter;
                }
                else
                    swi=0;
            }
            else
            if (to_solve.current==last)
            {
                --to_solve.current;
                if (to_solve.index_iter>=0 && array_of_lists[index_last].size()==1)
                {
                    last=to_solve.current;
                    index_last=to_solve.index_iter;
                }
                else
                    swi=0;
            }
            else
            {
                to_solve.current=first;
                to_solve.index_iter=index_first;
            }
            array_of_lists[to_solve.index_iter].erase(copy);
        }
        else
            throw not_found();
    }
    catch (std::exception& res)
    {
        std::cerr<<"(in erase_element) exception caught: "<<res.what();
    }
}

#endif // MAP_H
