#include <iostream>
using namespace std;
class A{
	int a;
public:
	A(int f) :a(f){ cout << "A"; }
	int& GetAndSet(){
		return a;
	}
	~A(){ cout << "~A"; }
};
void main()
{
	A obj(10);
	obj = 34;
	cout << obj.GetAndSet();
	int k; cin >> k;
}
//int k; cin >> k;