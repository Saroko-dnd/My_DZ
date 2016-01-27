#ifndef ADDICTION_ITSELF_H
#define ADDICTION_ITSELF_H

#include <string>
#include <vector>

class addiction_itself
{
    private:
        std::string name;
        static std::vector<addiction_itself> full_list_addiction;
    public:
        addiction_itself();
        virtual ~addiction_itself();
};

#endif // ADDICTION_ITSELF_H
