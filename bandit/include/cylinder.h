#ifndef CYLINDER_H
#define CYLINDER_H

#include <vector>
#include <string>
#include <cstdlib>

class cylinder
{
    private:
        std::string cylinder_itself;
        int middle;//номер символа, который будет посередине
    public:
        cylinder():cylinder_itself("0123456789123456789123456123456123123"),middle(rand()%cylinder_itself.size()){};
        std::string res();
};

#endif // CYLINDER_H
