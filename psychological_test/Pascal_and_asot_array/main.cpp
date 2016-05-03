
#include "../includes/Pascal's_triangle.h"
#include "../includes/associative_array.h"
using namespace std;

int main()
{
    Pascal_triangle a;
    int s=a[4][1];
    cout<<s<<endl;
    assot_array q(10);
    string first="Hello",second="Walk";
    double f_1=0.5,f_2=4.9;
    q.add_element(first,f_1);
    q.add_element(second,f_2);
    double result=0.0;
    first=q[4.9];
    cout<<first<<endl;
    return 0;
}
