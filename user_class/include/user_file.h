#ifndef USER_FILE_H
#define USER_FILE_H

#include <string.h>
#include <fstream>
#include <iostream>

class OnlyOne
{
    public:
        static int vov ;

        int num;
        std::string name;
private:

        OnlyOne():num(++vov)
        {

            std::ifstream file("settings.txt");
            file>>name;
            std::cout<<name<<std::endl;
            file.close();
        };
        ~OnlyOne()
        {
            std::ofstream file("settings.txt");
            file<<name;
            file.close();
            std::cout<<"its over"<<std::endl;
        };

public:
        static const OnlyOne& Instance()
        {
            static OnlyOne theSingleInstance;
            return theSingleInstance;
        }

};
#endif // USER_FILE_H
