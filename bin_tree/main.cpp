#include "tree.h"

using namespace std;

int main()
{
    tree<int,int> test;
    test.add_element(10,10);
    test.add_element(15,15);
    test.add_element(12,12);
    test.add_element(11,11);
    test.add_element(13,13);
    test.add_element(8,8);
    test.add_element(20,20);
    test.add_element(5,5);
    test.add_element(3,3);
    test.erase_element(11);
    test.cur_to_first();
    test.cur();
    return 0;
}
