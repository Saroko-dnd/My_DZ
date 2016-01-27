#include <iostream>

using namespace std;

class first
{
    public:
    first()
    {
        cout<<"first_created"<<endl;
    }
};

class A1:virtual public first
{
    public:
    void speak()
    {
        cout<<"A1_speaking"<<endl;
    }
    A1()
    {
        cout<<"A1_created"<<endl;
    }
};

class A2:virtual public first
{
    public:
    void speak()
    {
        cout<<"A2_speaking"<<endl;
    }
    A2()
    {
        cout<<"A2_created"<<endl;
    }
};

class result: public A1, public A2
{
public:
    result()
    {
        cout<<"result_created"<<endl;
    }
};

int main()
{
    result checking;
    checking.A1::speak();
    checking.A2::speak();
    return 0;
}
