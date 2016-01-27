#ifndef ONE_LINK_LIST_H_INCLUDED
#define ONE_LINK_LIST_H_INCLUDED

#include <iostream>
#include <stdexcept>
#include <exception>
#include <new>

typedef struct list_empty : std::exception
{
  const char* what() const noexcept {return "ERROR:list is empty.\n";}
}list_empty;

typedef struct unit_number_exception : std::exception
{
  const char* what() const noexcept {return "ERROR: element does not exist!\n";}
}unit_number_exception;

typedef struct swap_same_exception : std::exception
{
  const char* what() const noexcept {return "ERROR: there is no need to change the same elements!\n";}
}swap_same_exception;

template <typename T>
class list_two_link
{
private:
    typedef struct unit
    {
        T data;
        unit* next;
        unit* prev;
    } unit;
    //указатель на первый элемент списка
    unit*first;
    unit*last;
    //размер списка
    size_t size;
public:
    //возвращает ссылку для удобства
    class iterator
    {
        private:
            unit*current;
        public:
            iterator():current(nullptr)
            {
            }
            T operator[](size_t);
            T operator*();
            void operator--();
            void operator++();
            bool operator==(iterator&);
            bool operator!=(iterator&);
            iterator(unit*point_1)
            {
                current=point_1;
            }
    };
    int get_size()
    {
        return size;
    }
    bool check_empty()
    {
        return size>0 ? false:true;
    }
    list_two_link& push_front(T);
    void pop_front();
    list_two_link& push_back(T);
    void pop_back();
    void reverse();
    void swap(size_t,size_t);
    void insert_element(size_t,size_t);
    void erase_element(size_t);
    void operator=(list_two_link&);
    void unique();
    void merge(list_two_link&);
    void sort(bool(*)(int,int));
    void sort_(unit*,unit*,size_t,bool(*)(int,int));
    T operator[](size_t);
    iterator begin()
    {
        iterator obj(first);
        return obj;
    };
	iterator end()
	{
        iterator obj(last);
	    return obj;
    };
    iterator null()
	{
        iterator obj(nullptr);
	    return obj;
    };
    //конструктор со списком инициализации
    list_two_link():first(nullptr),last(nullptr),size(0)
    {
    }
    ~list_two_link()
    {
        while (size>0)
        {
            pop_front();
        }
    }
    list_two_link(list_two_link& another)
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
