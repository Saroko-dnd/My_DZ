#include "Robot.h"
#include "Bolt.h"
#include "GlobalVariables.h"
#include <cmath>
#include <cstdlib>
#include <typeinfo>
#include <iostream>

const double Robot::robot_speed = 10;
const double Robot::robot_radius = 20;
const int Robot::robot_energy = 1000;

const double Robot::safeDestForBolts = Robot::robot_radius*3;
const double Robot::safeDestForRobots = Robot::robot_radius*3;

//const double Robot::safeDestForBolts = ;
//const double Robot::safeDestForRobots = 400;

void Robot::makeDecision() {
	bool move_random = true;
	std::cout << objectStorage.size();
	for (auto it = objectStorage.begin(); it != objectStorage.end(); ++it) {
		std::cout << "cycle\n";
		//make sure that an object isn't our robot
		if (*it != this) {
			//if an object is another robot
			Robot* is_robot = dynamic_cast<Robot*>(*it);
			if (is_robot) {
				double destination = hypot((*it)->x - x,(*it)->y - y);
				std::cout << "destination:" << destination << std::endl;

				if (destination < Robot::safeDestForRobots) {
					//we're going to shoot

					//evaluating energy to shoot
					int energy_to_shoot=0;
					if (destination <= safeDestForRobots * 3 / 10)
						energy_to_shoot = energy * 15 / 100;
					else if (destination > safeDestForRobots * 3 / 10 && destination <= safeDestForRobots * 7 / 10)
						energy_to_shoot = energy * 10 / 100;
					else
						energy_to_shoot = energy * 5 / 100;

					// if energy_to_shoot is 0, we won't shoot
					if (!energy_to_shoot)
						continue;
					std::cout << "shoot" << energy_to_shoot << std::endl;
					//evaluate future position of enemy 
					double a = pow(Robot::robot_speed, 2) - pow(Bolt::bolt_speed, 2);
					double b = 2 * (((*it)->x - x) * (*it)->vx + ((*it)->y - y) * (*it)->vy);
					double c = pow(((*it)->x - x), 2) + pow(((*it)->y - y), 2);
					double D = b * b - 4 * a * c;
					double ans = (-b - sqrt(D)) / (2 * a);

					//descending robot's energy
					energy -= energy_to_shoot;
					//shooting
					shoot((*it)->x + (*it)->vx * ans, (*it)->y + (*it)->vy * ans, energy_to_shoot * 2);

					vx *= -1;
					vy *= -1;

					if ((x + vx * 1.0 / FPS) <= 0 || (x + vx * 1.0 / FPS) >= arenaXSize) {
						vx *= -1;
					}
					if ((y + vy * 1.0 / FPS) <= 0 || (y + vy * 1.0 / FPS) >= arenaYSize) {
						vy *= -1;
					}
					move(1.0 / FPS);

					move_random = false;
				}

			}
			//if an object is a bolt
			else {
				double destination = hypot(x - (*it)->x, y - (*it)->y);

				if (destination < Robot::safeDestForBolts) {
					vx *= -1;
					vy *= -1;

					if ((x + vx * 1.0 /  FPS) <= 0 || (x + vx * 1.0 /  FPS) >=  arenaXSize) {
						vx *= -1;
					}
					if ((y + vy * 1.0 /  FPS) <= 0 || (y + vy * 1.0 /  FPS) >=  arenaYSize) {
						vy *= -1;
					}
					move(1.0 /  FPS);

					move_random = false;
				}

			}
		}
	}

	if (move_random) {
		int random_x = (rand() % (int)(arenaXSize - robot_radius)) + robot_radius;
		int random_y = (rand() % (int)(arenaYSize - robot_radius)) + robot_radius;
		moveToPoint(random_x, random_y);
		move(1.0 /  FPS);
	}
}
