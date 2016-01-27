#ifndef ANSWER_H
#define ANSWER_H

#include "addiction.h"
#include <string>
#include <vector>
#include <map>

class answer
{
private:
	std::string answer_itself;
	std::map<int, int> score_information;
public:
	std::map<int, int> get_score();
	void print_answer();
	answer(std::string new_string);
};

#endif // ANSWER_H