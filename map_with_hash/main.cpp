
#include "Map_hash.h"

using namespace std;

int main(int argc, char *argv[])
{
    Map_with_hash<int,char> our_map(300);
    srand(time(NULL));
    int counter=0;
    while (counter<300)
    {
        our_map.add_element(rand(),'v');
        ++counter;
    }
    float res= our_map.percent_of_coincidence();
    cout<<"percent -"<<res<<endl;
    return 0;
}
