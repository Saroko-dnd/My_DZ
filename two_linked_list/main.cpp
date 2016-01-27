#include "two_linked_list.h"
#include "src/two_linked_list.cpp"


using namespace std;
bool test(int,int);

int main()
{
    list_two_link<int> f;
    bool (*h)(int,int);
    int res=10;
    h=test;
    f.push_front(5);
    f.push_front(8);
    f.sort(h);
    list_two_link<int>::iterator new_=f.begin();
    res=*new_;
    cout<<res<<endl;
    ++new_;
    res=*new_;
    cout<<res<<endl;

    return 0;
}

bool test (int a,int b)
{
    return a<b ?true:false;
}
