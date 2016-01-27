#ifndef ADDICTION_H
#define ADDICTION_H

#include <string>

class addiction
{
    private:
        std::string current_addiction;
        int status;//количество баллов для данной склонности
    public:
        void change_status(int);
        addiction(std::string);
        virtual ~addiction();
};

#endif // ADDICTION_H
