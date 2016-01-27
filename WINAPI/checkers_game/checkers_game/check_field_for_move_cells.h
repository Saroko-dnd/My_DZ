#pragma once

#include "field_load.h"
#include <vector>

bool check_king_attacks(std::vector<std::vector<cell>>, int, int,int);
std::vector<std::vector<cell>> check_near_cells(std::vector<std::vector<cell>>,int, int,int,bool,bool*);
std::vector<std::vector<cell>> create_king_arrows(std::vector<std::vector<cell>>, int, int, int, bool*);
std::vector<std::vector<cell>> check_all_roads(std::vector<std::vector<cell>>, int, int, int,bool*);

std::vector<std::vector<cell>> right_top(std::vector<std::vector<cell>>, int, int, int,bool*);
std::vector<std::vector<cell>> left_top(std::vector<std::vector<cell>>, int, int, int,bool*);
std::vector<std::vector<cell>> right_down(std::vector<std::vector<cell>>, int, int, int,bool*);
std::vector<std::vector<cell>> left_down(std::vector<std::vector<cell>>, int, int, int,bool*);