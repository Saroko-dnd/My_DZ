#ifndef ONE_LINK_LIST_H_INCLUDED
#define ONE_LINK_LIST_H_INCLUDED

#include <iostream>
#include <stdexcept>

class list_one_link
{
private:
    typedef struct unit
    {
        int data;
        unit* next;
    }unit;
    //указатель на первый элемент списка
    unit*first;
    //размер списка
    size_t size;
public:
    //возвращает ссылку для удобства
    int get_size()
    {
        return size;
    }
    list_one_link& push_front(int);
    void pop_front();
    void pop_front_();
    list_one_link& push_back(int);
    void pop_back();
    void pop_back_();
    void operator=( list_one_link&);
    int operator[](int);
    //конструктор со списком инициализации
    list_one_link():first(nullptr),size(0)
    {
    }
    ~list_one_link()
    {
        while (size>0)
        {
            pop_front();
            --size;
        }
    }
    list_one_link(list_one_link& another)
    {
        size=another.size;
        size_t counter=0;
        unit*temp=another.first;
        while (counter<another.size)
        {
            push_front(temp->data);
            temp=temp->next;
            ++counter;
        }
    }
};

#endif // ONE_LINK_LIST_H_INCLUDED
