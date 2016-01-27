#ifndef HERO_H
#define HERO_H

typedef struct saves
{
    int will;
    int durability;
    int reaction;
}saves;

typedef struct abilities
{
    //пока пусто
}abilities;

typedef struct characteristics
{
    abilities hero_abilities;
    saves saving_throws;
    int strength;
    int dexterity;
    int constitution;
    int intellect;
    int charisma;
    int wisdom;
    int hit_points;
    int defense;
    int crit_chance;
    int attack;
    int damage;
    float speed;
    int dark_vision;
    char*name[20];
}characteristics;

class hero
{
    private:


    public:
        hero();
        virtual ~hero();
};

#endif // HERO_H
