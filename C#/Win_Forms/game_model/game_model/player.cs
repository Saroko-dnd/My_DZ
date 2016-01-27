using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_model
{
    class player
    {
        public List<item> inventory = new List<item>();
        public list_of_characteristics characteristics = new list_of_characteristics();
        public list_of_skills skills = new list_of_skills();

        public int max_hit_pints = 6;
        public int current_hit_points = 6;
        public int max_damage = 1;
        public int current_damage = 1;
        public int max_weight = 50;
        public int current_weight = 0;
        public int max_fatigue = 50;
        public int current_fatigue = 0;
        public int action_points = 4;
        public int resistance_to_poisons = 0;
        public int current_chance_to_hit = 5;
        public int current_chance_to_dodge = 0;
        public int current_chance_to_block = 0;
        public int current_chance_of_critical_hit = 0;
        public int bad_food_poisoning_chance = 100;
    }
}
