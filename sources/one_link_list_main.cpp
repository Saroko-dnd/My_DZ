#include "../includes/one_link_list.h"

list_one_link& list_one_link::push_front(int data)
{
    unit* new_element=new unit;
    new_element->data=data;
    //проверка на пустоту списка (в данном случае бесполезно)
    new_element->data=data;
    new_element->next=first;
    first=new_element;
    ++size;
    return *this;
}

list_one_link& list_one_link::push_back(int data)
{
    unit*temp=first;
    size_t counter=0;
    while (counter<size)
    {
        temp=temp->next;
        ++counter;
    }
    unit* new_element=new unit;
    new_element->data=data;
    new_element->next=temp;
    temp=new_element;
    ++size;
    return *this;
}

void list_one_link::pop_front_()
{
    if (first!=nullptr)
    {
        unit *temp=first;
        first=first->next;
        delete temp;
    }
    else
        throw 0;
}

void list_one_link::pop_front()
{
    try
    {
        pop_front_();
    }
    catch (int result)
    {
        std::cout<<"ERROR: list is empty!"<<std::endl;
    }
}

void list_one_link::pop_back_()
{
    if (first!=nullptr)
    {
        unit *temp=first;
        size_t counter=0;
        while (counter<(size-1))
        {
            temp=temp->next;
            ++counter;
        }
        delete temp;
    }
    else
        throw 0;
}

void list_one_link::pop_back()
{
    try
    {
        pop_back_();
    }
    catch (int result)
    {
        std::cout<<"ERROR: list is empty!"<<std::endl;
    }
}

int list_one_link::operator[](int number)
{
    static int junk;
    unit* first_=first;
    if (first_!=nullptr)
    {
        while (number>0)
        {
            first_=first_->next;
            --number;
        }
        return first_->data;
    }
    else
    {
        std::cout<<"ERROR: element does not exist!"<<std::endl;
        return junk;
    }
}

void list_one_link::operator=(list_one_link& another)
{
    while (size>0)
    {
        pop_front();
        --size;
    }
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

