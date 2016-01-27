#include "Bolt.h"
#include "Circle.h"
#include "Robot.h"
#include "GlobalVariables.h"
#include <SDL.h>
#include <stdio.h>
#include <iostream>
#include <SDL_image.h>
#include <SDL_ttf.h>
#include <sstream>
#include <list>
#include <typeinfo>
#include <cmath>
#include <string>
#include <ctime>

using namespace std;

//static std::list<Circle*> objectStorage;

const int SCREEN_WIDTH = 500;
const int SCREEN_HEIGHT = 500;

int main(int argc, char* args[])
{
	stringstream energy_bufer;
	Robot* test_var;
	srand(time(0));
	bool to_shoot = true;

	Circle::objectStorage.push_back(new Robot(30, 30));
	Circle::objectStorage.push_back(new Robot(200, 100));
	int nothing;
	int counter = 0;
	Robot* bbn = dynamic_cast <Robot*> (*Circle::objectStorage.begin());
	//bbn->makeDecision();
	cout << Circle::objectStorage.size();
	cin >> nothing;

	if (SDL_Init(SDL_INIT_VIDEO) != 0)
	{
		cout << "SDL_Init Error: " << SDL_GetError() << endl;
		return 1;
	}

	if (TTF_Init() != 0)
	{
		cout << "TTF_Init Error: " << SDL_GetError() << endl;
		return 1;
	}

	SDL_Window *win = SDL_CreateWindow("Textures test", 100, 100, SCREEN_WIDTH, SCREEN_HEIGHT, SDL_WINDOW_SHOWN);
	if (win == nullptr)
	{
		cout << "SDL_CreateWindow Error: " << SDL_GetError() << endl;
		SDL_Quit();
		return 1;
	}

	SDL_Renderer *ren = SDL_CreateRenderer(win, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
	if (ren == nullptr)
	{
		SDL_DestroyWindow(win);
		cout << "SDL_CreateRenderer Error: " << SDL_GetError() << endl;
		SDL_Quit();
		return 1;
	}

	// все связанное с текстом регулируется здесь
	SDL_Color White = { 255, 255, 255 }; 	// цвет шрифта (сейчас белый)
	TTF_Font *font = TTF_OpenFont("C:/windows/fonts/cour.ttf", 10);	// выбор шрифта и его размера
	SDL_Surface* energy_surface = TTF_RenderText_Blended(font, "1000", White);// создание поверхности с текстом

	SDL_Texture *energy_texture = SDL_CreateTextureFromSurface(ren, energy_surface);
	if (energy_texture == nullptr)
	{
		SDL_FreeSurface(energy_surface);
		SDL_DestroyRenderer(ren);
		SDL_DestroyWindow(win);
		cout << "SDL_CreateTextureFromSurface Error: " << SDL_GetError() << endl;
		SDL_Quit();
		return 1;
	}

	SDL_Surface *robotS_bullets = IMG_Load("robot_3.png");
	if (robotS_bullets == nullptr)
	{
		SDL_DestroyRenderer(ren);
		SDL_DestroyWindow(win);
		cout << "SDL_LoadPNG Error: " << SDL_GetError() << endl;
		SDL_Quit();
		return 1;
	}

	SDL_Texture *our_texture = SDL_CreateTextureFromSurface(ren, robotS_bullets);
	if (our_texture == nullptr)
	{
		SDL_FreeSurface(robotS_bullets);
		SDL_DestroyRenderer(ren);
		SDL_DestroyWindow(win);
		cout << "SDL_CreateTextureFromSurface Error: " << SDL_GetError() << endl;
		SDL_Quit();
		return 1;
	}

	robotS_bullets = IMG_Load("bullet.png");
	if (robotS_bullets == nullptr)
	{
		SDL_DestroyTexture(our_texture);
		SDL_DestroyRenderer(ren);
		SDL_DestroyWindow(win);
		cout << "SDL_LoadPNG Error: " << SDL_GetError() << endl;
		SDL_Quit();
		return 1;
	}

	SDL_Texture *our_texture_2 = SDL_CreateTextureFromSurface(ren, robotS_bullets);
	if (our_texture == nullptr)
	{
		SDL_FreeSurface(robotS_bullets);
		SDL_DestroyTexture(our_texture);
		SDL_DestroyRenderer(ren);
		SDL_DestroyWindow(win);
		cout << "SDL_CreateTextureFromSurface Error: " << SDL_GetError() << endl;
		SDL_Quit();
		return 1;
	}

	SDL_Rect srcrect_robot;
	SDL_Rect dstrect_robot;

	srcrect_robot.x = 0;
	srcrect_robot.y = 0;
	srcrect_robot.w = 40;
	srcrect_robot.h = 40;

	dstrect_robot.x = 0;
	dstrect_robot.y = 0;
	dstrect_robot.w = 40;
	dstrect_robot.h = 40;

	SDL_Rect srcrect_bullet;
	SDL_Rect dstrect_bullet;

	srcrect_bullet.x = 0;
	srcrect_bullet.y = 0;
	srcrect_bullet.w = 40;
	srcrect_bullet.h = 40;

	dstrect_bullet.x = 100;
	dstrect_bullet.y = 0;
	dstrect_bullet.w = 5;
	dstrect_bullet.h = 5;

	SDL_Rect dstrect_energy;

	dstrect_energy.x = 200;
	dstrect_energy.y = 0;
	dstrect_energy.w = 40;
	dstrect_energy.h = 25;

	SDL_Event our_event;
	bool quit = false;

	list<Circle*>::iterator current_position;

	while (!quit && Circle::objectStorage.size()>1)
	{

		//****************************************************************

			for (auto it = Circle::objectStorage.begin(); it != Circle::objectStorage.end();) {
				//check boarders
				Robot* it_conv = dynamic_cast<Robot*>(*it);
				if (it_conv && ((*it)->x - it_conv->robot_radius < 0 || (*it)->x + it_conv->robot_radius > arenaXSize || (*it)->y - it_conv->robot_radius < 0 || (*it)->y + it_conv->robot_radius > arenaYSize)) {
					it = Circle::objectStorage.erase(it);
					continue;
				}
				else
				{
					if ((*it)->x < 0 || (*it)->x > arenaXSize || (*it)->y < 0 || (*it)->y > arenaYSize)
					{
						it = Circle::objectStorage.erase(it);
						continue;
					}
				}
				//if object's in correct boarders, act
				//if object is robot
				if (it_conv) {
					it_conv->makeDecision();
				}
				//if object is bolt
				else {
					(*it)->move(2.0 / FPS);
				}
				++it;
			}

			//check bolts and collisions
			for (auto it = Circle::objectStorage.begin(); it != Circle::objectStorage.end(); ++it) {
				Robot* is_robot = dynamic_cast<Robot*>(*it);
				//if it's robot
				if (is_robot) {
					//check other objects
					for (auto enemy = Circle::objectStorage.begin(); enemy != Circle::objectStorage.end(); ++enemy) {
						//if that object is our robot
						if (*it == *enemy)
							continue;

						Robot* enemy_is_robot = dynamic_cast<Robot*>(*enemy);
						//if that object is other robot
						if (enemy_is_robot) {
							//double distance = hypot((*it)->x - (*enemy)->x, (*it)->y - (*enemy)->y);
							cout << "it->x" << (*it)->x << "enemy->x" << (*enemy)->x << endl;
							cout << "it->y" << (*it)->y << "enemy->y" << (*enemy)->y << endl;
							double distance = hypot((*enemy)->x - (*it)->x, (*enemy)->y - (*it)->y);
							cout << "distanse beetwin robots" << distance<< endl;
							if (distance < Robot::robot_radius*2) {
								//double deltaEnergy = abs((*it)->energy - (*enemy)->energy);
								//(*it)->energy -= deltaEnergy;
								//(*enemy)->energy -= deltaEnergy;
								cout << "crash with another robot" << endl;
								int min_energy = (*it)->energy < (*enemy)->energy ? (*it)->energy : (*enemy)->energy;
								(*it)->energy -= min_energy;
								(*enemy)->energy -= min_energy;
							}
						}
						//if that object is bolt
						else {
							double distance = hypot((*it)->x - (*enemy)->x, (*it)->y - (*enemy)->y);
							if (distance < Robot::robot_radius) {
								(*it)->energy -= (*enemy)->energy;
								(*enemy)->energy = 0;
							}
						}

					}
				}
			}

			//if object is dead
			for (auto it = Circle::objectStorage.begin(); it != Circle::objectStorage.end();) {
				cout << "energy:" << (*it)->energy<<endl;
				if ((*it)->energy <= 0) {
					it = Circle::objectStorage.erase(it);
				}
				else {
					++it;
				}
			}

			//***************************************************************************************
			while (SDL_PollEvent(&our_event))
			{
				if (our_event.key.keysym.sym == SDLK_SPACE)
				{
					quit = true;
				}
				if (our_event.type == SDL_QUIT){
					quit = true;
				}
			}
			SDL_RenderClear(ren);
			current_position = Circle::objectStorage.begin();
			while (current_position != Circle::objectStorage.end())//здесь будет цикл до конца вектора, содержащего список обьектов
			{
				test_var = dynamic_cast<Robot*>(*current_position);
				if (test_var)//здесь будет проверка - является ли обьект роботом
				{
					/*if (!to_shoot)
					{
						//to_shoot = false;
						test_var->shoot(300, 30, 1);
					}*/
					++counter;
					cout << "number of robot" << counter << endl;
					//здесь будет присвоение координат в dstrect_bullet и dstrect_energy, а также создаваться новая строка для 'energy_bufer'
					energy_bufer.str(std::string());
					energy_bufer << (*current_position)->energy;
					SDL_Surface* energy_surface = TTF_RenderText_Blended(font, energy_bufer.str().c_str(), White);
					SDL_Texture *energy_texture = SDL_CreateTextureFromSurface(ren, energy_surface);
					dstrect_energy.x = (int)((*current_position)->x-20);
					dstrect_energy.y = (int)((*current_position)->y-40);
					SDL_RenderCopy(ren, energy_texture, NULL, &dstrect_energy);
					dstrect_robot.x = (int)((*current_position)->x-20);
					dstrect_robot.y = (int)((*current_position)->y-20);
					SDL_RenderCopy(ren, our_texture, &srcrect_robot, &dstrect_robot);
				}
				else //рисование пули
				{
					//здесь будет присвоение координат в dstrect_bullet 
					dstrect_bullet.x = (int)((*current_position)->x);
					dstrect_bullet.y = (int)((*current_position)->y);
					SDL_RenderCopy(ren, our_texture_2, &srcrect_bullet, &dstrect_bullet);
				}
				++current_position;
			}
			to_shoot = false;
			counter = 0;
			/*SDL_RenderCopy(ren, our_texture, &srcrect_robot, &dstrect_robot);
			SDL_RenderCopy(ren, our_texture_2, &srcrect_bullet, &dstrect_bullet);
			SDL_RenderCopy(ren, energy_texture, NULL, &dstrect_energy);*/
			SDL_RenderPresent(ren);

			SDL_Delay(100);

	}
	cin >> nothing;
	cin >> nothing;
	SDL_FreeSurface(robotS_bullets);
	SDL_FreeSurface(energy_surface);
	SDL_DestroyTexture(energy_texture);
	SDL_DestroyTexture(our_texture);
	SDL_DestroyTexture(our_texture_2);
	SDL_DestroyRenderer(ren);
	SDL_DestroyWindow(win);
	SDL_Quit();

	return 0;
}

