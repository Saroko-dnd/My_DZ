#pragma once

#include <vector>
#include "scalar.h"

class Value
{
public:
	virtual void print_this()=0;
	Value(){};
	virtual ~Value(){};
};

