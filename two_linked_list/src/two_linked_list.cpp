#include "two_linked_list.h"

template <typename T>
list_two_link<T>& list_two_link<T>::push_front(T data)
{
    try
    {
        unit* new_element=new unit;
        new_element->data=data;
        new_element->next=first;
        new_element->prev=nullptr;
        if (first==nullptr)
            first=new_element;
        else
        {
            first->prev=new_element;
            first=new_element;
        }
        if (size==0)
        {
            last=new_element;
        }
        ++size;
        return *this;
    }
    catch (std::exception &res)
    {
        std::cerr<<"(in push_front()) exception caught: "<<res.what()<<std::endl;
        return *this;
    }

}

template <typename T>
list_two_link<T>& list_two_link<T>::push_back(T data)
{
    try
    {
        if (last==nullptr)
        {
            push_front(data);
            return*this;
        }
        else
        {
            unit*temp=last;
            unit* new_element=new unit;
            new_element->data=data;
            new_element->next=nullptr;;
            new_element->prev=temp;
            temp->next=new_element;
            ++size;
            return *this;
        }
    }
    catch (std::exception &res)
    {
        std::cerr<<"(in push_back()) exception caught: "<<res.what()<<std::endl;
        return *this;
    }
}

template <typename T>
void list_two_link<T>::pop_front()
{
    try
    {
        if (first!=nullptr)
        {
            unit *temp=first;
            first=first->next;
            delete temp;
            --size;
            if (size==0)
            {
                last=nullptr;
                first=nullptr;
            }
            else
                first->prev=nullptr;
        }
        else
            throw list_empty();
    }
    catch (std::exception& res)
    {
        std::cerr<<"(in pop_front()) exception caught: "<<res.what();
    }

}

template <typename T>
void list_two_link<T>::pop_back()
{
    try
    {
        if (first!=nullptr)
        {
            unit *temp=last;
            last=last->prev;
            delete temp;
            --size;
            if (size==0)
            {
                last=nullptr;
                first=nullptr;
            }
            else
            {
                last->next=nullptr;
            }
        }
        else
            throw list_empty();
    }
    catch (std::exception& res)
    {
        std::cerr<<"(in pop_back()) exception caught: "<<res.what();
    }
}

template <typename T>
void list_two_link<T>::reverse()
{
    try
    {
        if (first!=nullptr && size>1)
        {
            unit*temp_1=first;
            unit*temp_2=last;
            unit*buf_1=first;
            while (last!=buf_1)
            {
                first=temp_1;
                last=temp_2;
                temp_1=temp_1->next;
                temp_2=temp_2->prev;
            }
            temp_1=last;
            while (temp_1!=nullptr)
            {
                buf_1=temp_1->next;
                temp_1->next=temp_1->prev;
                temp_1->prev=buf_1;
                temp_1=temp_1->prev;
            }
        }
        else
            throw list_empty();
    }
    catch (std::exception& res)
    {
        std::cerr<<"(in reverse()) exception caught: "<<res.what();
    }
}

template <typename T>
T list_two_link<T>::operator[](size_t number)
{
    iterator new_=end();
    return new_.operator[](number);
}

template <typename T>
void list_two_link<T>::operator=(list_two_link& another)
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

template <typename T>
void list_two_link<T>::swap(size_t first_,size_t second_)
{
    try
    {
        if (first_!=second_)
        {
            if (first_<size && first_>=0 && second_<size && second_>=00)
            {
                unit*element_1=first;
                unit*element_2=first;
                size_t counter=0;
                int swi_1=0,swi_2=0;
                while (swi_1!=1 || swi_2!=1)
                {
                    if (swi_1!=1)
                    {
                        if (counter==first_)
                            swi_1=1;
                        else
                            element_1=element_1->next;
                    }
                    if (swi_2!=1)
                    {
                        if (counter==second_)
                            swi_2=1;
                        else
                            element_2=element_2->next;
                    }
                    ++counter;
                }
                int buf=element_1->data;
                element_1->data=element_2->data;
                element_2->data=buf;
            }
            else
                throw unit_number_exception();
        }
        else
            throw swap_same_exception();
    }
    catch (std::exception& res)
    {
        std::cout<<"(in operation - swap) exception caught:"<<res.what();
    }

}

template <typename T>
void list_two_link<T>::insert_element(size_t object,size_t number)
{
    try
    {
        if (number<0 || number>=size)
            throw unit_number_exception();
        else
        {
            if (number==0)
            {
                push_front(object);
            }
            else
            {
                if (number<((size-1)/2))
                {
                    unit*temp_1=first;
                    unit*temp_2=first;
                    size_t counter=0;
                    while (counter<number)
                    {
                        temp_1=temp_1->next;
                        temp_2=temp_2->next;
                        ++counter;
                    }
                    temp_2=temp_2->next;
                    unit*new_element=new unit;
                    new_element->data=object;
                    new_element->next=temp_2;
                    new_element->prev=temp_1;
                    temp_1->next=new_element;
                    if (temp_2!=nullptr)
                        temp_2->prev=new_element;
                }
                else
                {
                    unit*temp_1=last;
                    unit*temp_2=last;
                    size_t counter=0;
                    while (counter<number)
                    {
                        temp_1=temp_1->prev;
                        temp_2=temp_2->prev;
                        ++counter;
                    }
                    temp_2=temp_2->prev;
                    unit*new_element=new unit;
                    new_element->data=object;
                    new_element->prev=temp_2;
                    new_element->next=temp_1;
                    temp_1->prev=new_element;
                    if (temp_2!=nullptr)
                        temp_2->next=new_element;
                }
            }
        }
    }
    catch (std::exception& res)
    {
        std::cout<<"(in operation - insert_element) exception caught:"<<res.what();
    }
}

template <typename T>
void list_two_link<T>::erase_element(size_t number)
{
    try
    {
        if (first!=nullptr)
        {
            if (number>=0 && number<size)
            {
                unit*search_=nullptr;
                if (number>(size-1)/2)
                {
                    number=size-(number+1);
                    search_=last;
                    while (number>0)
                    {
                        search_=search_->prev;
                        --number;
                    }
                    if (search_->next!=nullptr)
                        search_->next->prev=search_->prev;
                    if (search_->prev!=nullptr)
                        search_->prev->next=search_->next;
                }
                else
                {
                    search_=first;
                    while (number>0)
                    {
                        search_=search_->next;
                        --number;
                    }
                    if (search_->next!=nullptr)
                        search_->next->prev=search_->prev;
                    if (search_->prev!=nullptr)
                        search_->prev->next=search_->next;
                }
                if (size==1)
                {
                    first=nullptr;
                    last=nullptr;
                }
                else if (search_==first)
                    first=first->next;
                else if (search_==last)
                    last=last->prev;
                delete search_;
            }
            else
                throw unit_number_exception();
        }
        else
            throw list_empty();
    }
    catch (std::exception& res)
    {
        std::cout<<"(in operation - erase_element) exception caught:"<<res.what();
    }
}

template <typename T>
void list_two_link<T>::unique()
{
    try
    {
        if (size>1)
        {
            unit*temp_1=first;
            unit*temp_2=first->next;
            unit*destroy_it=nullptr;
            while (temp_1!=last)
            {
                while (temp_2!=nullptr)
                {
                    if (temp_2->data==temp_1->data)
                    {
                        if (temp_2->next!=nullptr)
                            temp_2->next->prev=temp_2->prev;
                        temp_2->prev->next=temp_2->next;
                        destroy_it=temp_2;
                        if (destroy_it==last)
                            last=last->prev;
                        temp_2=temp_2->next;
                        delete destroy_it;
                        destroy_it=nullptr;
                    }
                    else
                        temp_2=temp_2->next;
                }
                if (temp_1!=last)
                {
                    temp_1=temp_1->next;
                    temp_2=temp_1->next;
                }
            }
        }
        else
            throw list_empty();
    }
    catch (std::exception& res)
    {
        std::cout<<"(in operation - unique) exception caught:"<<res.what();
    }
}

template <typename T>
void list_two_link<T>::merge(list_two_link& object)
{
    size_t counter=object.size;
    unit*temp=object.first;
    while (counter>0)
    {
        push_back(temp->data);
        temp=temp->next;
        --counter;
    }
}

template <typename T>
void list_two_link<T>::sort_(unit* first,unit* last,size_t size,bool (*algoritm)(int ,int ))
{
    unit*i=first;
    unit*j=last;
    unit* p=last;
    size_t half=size;
    T temp;

    while (half>size/2)
    {
        p=p->prev;
        --half;
    }
    std::cout<<p->data<<std::endl;

    half=1;
    size_t size_copy=size;

    do
    {
        while (algoritm(i->data,p->data))
        {i=i->next;
        ++half;}
        while (algoritm(p->data,j->data))
        {j=j->prev;
        --size_copy;}
        std::cout<<half<<" "<<size_copy<<std::endl;
        if (half<=size_copy)
        {
            temp = i->data;
            i->data = j->data;
            j->data = temp;
            i=i->next;
            ++half;
            j=j->prev;
            --size_copy;
        }
    }
    while ( half<=size_copy);
    if ( size_copy >1) sort_(first, j,size_copy,algoritm);
    if ( size > half) sort_(i, last,size-half,algoritm);
}

template <typename T>
void list_two_link<T>::sort(bool (*algoritm)(int,int))
{
    sort_(first,last,size,algoritm);
}


template <typename T>
void list_two_link<T>::iterator::operator--()
{
    if (current!=nullptr)
        current=current->prev;
}

template <typename T>
void list_two_link<T>::iterator::operator++()
{
    if (current!=nullptr)
        current=current->next;
}

template <typename T>
bool list_two_link<T>::iterator::operator==(iterator& another_iterator)
{
    return current->data==another_iterator.current->data ? true : false;
}

template <typename T>
bool list_two_link<T>::iterator::operator!=(iterator& another_iterator)
{
    return current->data!=another_iterator.current->data ? true : false;
}

template <typename T>
T list_two_link<T>::iterator::operator[](size_t number)
{
    try
    {
        if (first!=nullptr && number>0 && number<size)
        {
            size_t counter;
            if (number<size/2)
            {
                counter=0;
                current=first;
                while (counter!=number)
                {
                    current=current->next;
                    ++counter;
                }
                return current->data;
            }
            else
            {
                counter=size-1;
                current=last;
                while (counter!=number)
                {
                    current=current->prev;
                    --counter;
                }
                return current->data;
            }
        }
        else
            throw unit_number_exception();
    }
    catch (std::exception& res)
    {
        std::cout<<"(in operator[]) exception caught:"<<res.what();
    }
}

template <typename T>
T list_two_link<T>::iterator::operator*()
{
    try
    {
        if (current!=nullptr)
        {
            return current->data;
        }
        else
            throw list_empty();
    }
    catch (std::exception& res)
    {
        std::cout<<"(in operator*) exception caught:"<<res.what();
        return 0;
    }
}
