#ifndef TREE_H
#define TREE_H

#include <iostream>
//не хватает исключений!
template <typename Tvalue,typename Tkey>
class tree
{
private:
    typedef struct element
    {
        Tvalue value;
        Tkey key;
        element* right;
        element* left;
        element* next;
        element* prev;
    } element;
    element*first;
    element*first_list;
    element*last_list;
    element*current;
    int swi;
    element*pointer;
    int size;
public:
    Tvalue operator [](Tkey);
    void add_element (Tkey,Tvalue);
    void show_elements()
    {
        pointer=&first;
        show_elements_real(pointer);
    }
    void show_elements_real(element*);
    void delete_elements(element*);
    void erase_element(Tkey);
    void cur_to_first();
    void cur();
    tree()
    {
        first=nullptr;
        pointer=nullptr;
        first_list=nullptr;
        last_list=nullptr;
        current=nullptr;
        swi=0;
        size=0;
    }
    ~tree()
    {
        if (pointer!=nullptr)
            delete_elements(pointer);
    }
};

template <typename Tvalue,typename Tkey>
void tree<Tvalue,Tkey>::cur()
{
    while (current!=nullptr)
    {
        std::cout<<current->value<<std::endl;
        current=current->next;
    }
}

template <typename Tvalue,typename Tkey>
void tree<Tvalue,Tkey>::cur_to_first()
{
    current=first_list;
}

template <typename Tvalue,typename Tkey>
Tvalue tree<Tvalue,Tkey>::operator[](Tkey key_search)
{
    int swi=1;
    pointer=&first;
    while (swi==1)
    {
        if (key_search<pointer->key)
        {
            if (pointer->left==nullptr)
                swi=0;
            else
                pointer=pointer->left;
        }
        else if (key_search>pointer->key)
        {
            if (pointer->right==nullptr)
                swi=0;
            else
                pointer=pointer->right;
        }
        else
            return pointer->value;
    }
    std::cout<<"element does not exist"<<std::endl;
    return pointer->value;

}

template <typename Tvalue,typename Tkey>
void tree<Tvalue,Tkey>::add_element(Tkey key_new,Tvalue value_new)
{
    element*pointer_copy=pointer;
    element*for_prev=nullptr;
    element*for_next=nullptr;
    int left=0,right=0;
    int swi=1,swi_2=0;
    if (size==0)
    {
        first=new element;
        first->key=key_new;
        first->value=value_new;
        first->left=nullptr;
        first->right=nullptr;
        first->next=nullptr;
        first->prev=nullptr;

        pointer=first;
        first_list=first;
        last_list=first;
        ++size;
    }
    else
    {
        while (swi==1 && size>0)
        {
            if (key_new<pointer->key)
            {
                ++left;
                if (swi_2==0)
                    swi_2=1;
                if (pointer->left==nullptr)
                {
                    pointer->left=new element;
                    if (pointer==pointer_copy)
                    {
                        ++size;
                        pointer=pointer->left;
                        pointer->key=key_new;
                        pointer->value=value_new;
                        pointer->left=nullptr;
                        pointer->right=nullptr;
                        pointer->prev=nullptr;
                        pointer->next=pointer_copy;
                        pointer_copy->prev=pointer;
                        first_list=pointer;
                    }
                    else
                    {
                        ++size;
                        for_next=pointer;
                        pointer=pointer->left;
                        pointer->key=key_new;
                        pointer->value=value_new;
                        pointer->left=nullptr;
                        pointer->right=nullptr;
                        pointer->next=for_next;
                        for_next->prev=pointer;
                        if (right==0)
                        {
                            pointer->prev=nullptr;
                            first_list=pointer;
                        }
                        else
                        {
                            pointer->prev=for_prev;
                            for_prev->next=pointer;
                        }
                    }
                        swi=0;
                }
                else
                {
                    if (swi_2==1)
                        for_prev=pointer;
                    if (swi_2==2 && pointer->left->key<key_new)
                    {
                        for_prev=pointer;
                    }
                    pointer=pointer->left;
                }
            }
            else if (key_new>pointer->key)
            {
                ++right;
                if (swi_2==0)
                    swi_2=2;
                if (pointer->right==nullptr)
                {
                    pointer->right=new element;
                    if (pointer==pointer_copy)
                    {
                        ++size;
                        for_prev=pointer;
                        pointer=pointer->right;
                        pointer->key=key_new;
                        pointer->value=value_new;
                        pointer->left=nullptr;
                        pointer->right=nullptr;
                        pointer->prev=for_prev;
                        for_prev->next=pointer;
                        pointer->next=nullptr;
                        last_list=pointer;
                    }
                    else
                    {
                        ++size;
                        for_next=pointer;
                        pointer=pointer->right;
                        pointer->key=key_new;
                        pointer->value=value_new;
                        pointer->left=nullptr;
                        pointer->right=nullptr;
                        pointer->prev=for_next;
                        for_next->next=pointer;
                        if (left==0)
                        {
                            pointer->next=nullptr;
                            last_list=pointer;
                        }
                        else
                        {
                            pointer->next=for_prev;
                            for_prev->prev=pointer;
                        }
                    }
                        swi=0;
                }
                else
                {
                    if (swi_2==2)
                        for_prev=pointer;
                    if (swi_2==1 && pointer->right->key>key_new)
                    {
                        for_prev=pointer;
                    }
                    pointer=pointer->right;
                }
            }
            else if (key_new==pointer->key)
            {
                std::cout<<"elements already exist"<<std::endl;
                swi=0;
            }
        }
        pointer=pointer_copy;
    }
}

template <typename Tvalue,typename Tkey>
void tree<Tvalue,Tkey>::show_elements_real(element* pointer_main)
{
    int swi=0,swi_2=0;
    std::cout<<pointer_main->value<<std::endl;
    if (pointer_main->left!=nullptr && swi==0)
    {
        swi=1;
        show_elements(pointer_main->left);
    }
    if (pointer_main->right!=nullptr && swi_2==0)
    {
        swi_2=1;
        show_elements(pointer_main->right);
    }
}

template <typename Tvalue,typename Tkey>
void tree<Tvalue,Tkey>::erase_element(Tkey key_word)
{
    element*pointer_copy=pointer;
    element*main_prev=nullptr;
    element*main_next=nullptr;
    int swi=1;
    while (swi==1 && size>0)
    {
        if (key_word<pointer->key)
        {
            if (pointer->left==nullptr)
                swi=0;
            if (pointer->left->key==key_word)
            {
                element*copy_p=pointer->left->left;
                element*copy_main=pointer->left;
                pointer->left=pointer->left->right;
                if (copy_main==first_list)
                    first_list=first_list->next;
                if (copy_main==last_list)
                    last_list=last_list->prev;
                main_prev=copy_main->prev;
                main_next=copy_main->next;
                if (main_prev!=nullptr)
                    main_prev->next=main_next;
                if (main_next!=nullptr)
                    main_next->prev=main_prev;
                delete copy_main;
                pointer=pointer->left;
                if (copy_p==nullptr)
                    swi=2;
                else
                {
                    if (pointer==nullptr)
                    {
                        swi=2;
                        pointer=copy_p;
                    }
                    else
                    {
                        while (pointer->left!=nullptr)
                        {
                            pointer=pointer->left;
                        }
                        pointer->left=copy_p;
                        swi=2;
                    }
                }
            }
            else
            {
                pointer=pointer->left;
            }
        }
        else if (key_word>pointer->key)
        {
            if (pointer->right==nullptr)
                swi=0;
            if (pointer->right->key==key_word)
            {
                element*copy_p=pointer->right->left;
                element*copy_main=pointer->right;
                pointer->right=pointer->right->right;
                if (copy_main==first_list)
                    first_list=first_list->next;
                if (copy_main==last_list)
                    last_list=last_list->prev;
                main_prev=copy_main->prev;
                main_next=copy_main->next;
                if (main_prev!=nullptr)
                    main_prev->next=main_next;
                if (main_next!=nullptr)
                    main_next->prev=main_prev;
                delete copy_main;
                pointer=pointer->right;
                if (copy_p==nullptr)
                    swi=2;
                else
                {
                    if (pointer==nullptr)
                    {
                        swi=2;
                        pointer=copy_p;
                    }
                    else
                    {
                    while (pointer->left!=nullptr)
                    {
                        pointer=pointer->left;
                    }
                    pointer->left=copy_p;
                    swi=2;
                    }
                }
            }
            else
            {
                pointer=pointer->right;
            }
        }
        else
        {
            if (pointer_copy==pointer)
            {
                pointer_copy=pointer->right;
            }
            element*copy_main=pointer;
            element*copy_p=pointer->left;
            pointer=pointer->right;
            if (copy_main==first_list)
                first_list=first_list->next;
            if (copy_main==last_list)
                last_list=last_list->prev;
            main_prev=copy_main->prev;
            main_next=copy_main->next;
            if (main_prev!=nullptr)
                main_prev->next=main_next;
            if (main_next!=nullptr)
                main_next->prev=main_prev;
            delete copy_main;
            if (copy_p==nullptr)
                swi=2;
            else
            {
                if (pointer==nullptr)
                {
                    swi=2;
                    pointer=copy_p;
                }
                else
                {
                    while (pointer->left!=nullptr)
                    {
                        pointer=pointer->left;
                    }
                    pointer->left=copy_p;
                    swi=2;
                }
            }
        }
    }
    pointer=pointer_copy;
    if (swi==0 || size==0)
        std::cout<<"cant find element"<<std::endl;
    else
        --size;
}

template <typename Tvalue,typename Tkey>
void tree<Tvalue,Tkey>::delete_elements(element*pointer_main)
{
    element*pointer_left=pointer_main->right;
    element*pointer_right=pointer_main->left;
    delete pointer_main;
    if (pointer_left!=nullptr)
    {
        delete_elements(pointer_left);
    }
    if (pointer_right!=nullptr)
    {
        delete_elements(pointer_right);
    }
}

#endif // TREE_H
