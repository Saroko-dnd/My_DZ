#include <iostream>
#include <vector>
#include "our_matrix.h"
#include <SDL2/SDL.h>
#include <SDL2/SDL_image.h>


using namespace std;

int main(int argc,char*argv[])
{
    our_matrix<double> top_1(3,1,{50.0,50.0,50.0}),top_2(3,1,{300.0,50.0,50.0}),top_3(3,1,{100.0,150.0,100.0}),top_4(3,1,{350.0,150.0,100.0});
    our_matrix<double> bottom_1(3,1,{50.0,350.0,50.0}),bottom_2(3,1,{300.0,350.0,50.0}),bottom_3(3,1,{100.0,450.0,100.0}),bottom_4(3,1,{350.0,450.0,100.0});
    our_matrix<double> to_2d(2,3,{1.0,0.0,0.7,0.0,1.0,0.7});
    our_matrix<double> buf(2,1,{0.0,0.0}),buf_2(2,1,{0.0,0.0});
    double angle=0.01;

    if (SDL_Init(SDL_INIT_VIDEO) != 0)
    {
        std::cout << "SDL_Init Error: " << SDL_GetError() << std::endl;
        return 1;
    }

    SDL_Window *win = SDL_CreateWindow("Textures test", 100, 100, 600, 600, SDL_WINDOW_SHOWN);
    if (win == nullptr)
    {
        std::cout << "SDL_CreateWindow Error: " << SDL_GetError() << std::endl;
        SDL_Quit();
        return 1;
    }

    SDL_Renderer *ren = SDL_CreateRenderer(win, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
    if (ren == nullptr)
    {
        SDL_DestroyWindow(win);
        std::cout << "SDL_CreateRenderer Error: " << SDL_GetError() << std::endl;
        SDL_Quit();
        return 1;
    }

    SDL_Surface *bmp = IMG_Load("textures/rabstol_net_square_07.jpg");
    if (bmp == nullptr)
    {
        SDL_DestroyRenderer(ren);
        SDL_DestroyWindow(win);
        std::cout << "SDL_LoadBMP Error: " << SDL_GetError() << std::endl;
        SDL_Quit();
        return 1;
    }

    SDL_Texture *sprite = SDL_CreateTextureFromSurface(ren, bmp);
    if (sprite == nullptr)
    {
        SDL_DestroyRenderer(ren);
        SDL_DestroyWindow(win);
        std::cout << "SDL_CreateTextureFromSurface Error: " << SDL_GetError() << std::endl;
        SDL_Quit();
        return 1;
    }

    SDL_Rect srcrect;
    SDL_Rect dstrect;
    srcrect.x = 0;
    srcrect.y = 0;
    srcrect.w = 3;
    srcrect.h = 3;

    dstrect.x = 0;
    dstrect.y = 0;
    dstrect.w = 600;
    dstrect.h = 600;
    SDL_Event e;
    bool quit = false;
   // SDL_SetRenderDrawColor(ren, 255, 0, 0, 255);
    while (!quit)
    {
        while (SDL_PollEvent(&e))
        {
                if (e.key.keysym.sym == SDLK_SPACE)
                {
                    quit = true;
                }
        }
        SDL_RenderClear(ren);
        SDL_RenderCopy(ren,sprite,&srcrect,&dstrect);
        top_1.rotate(angle);
        top_2.rotate(angle);
        top_3.rotate(angle);
        top_4.rotate(angle);
        bottom_1.rotate(angle);
        bottom_2.rotate(angle);
        bottom_3.rotate(angle);
        bottom_4.rotate(angle);
        buf=to_2d*top_1;
        buf_2=to_2d*top_2;//1
        SDL_RenderDrawLine(ren,buf[0][0],buf[1][0],buf_2[0][0],buf_2[1][0]);
        buf=to_2d*top_3;
        buf_2=to_2d*top_4;//2
        SDL_RenderDrawLine(ren,buf[0][0],buf[1][0],buf_2[0][0],buf_2[1][0]);
        buf=to_2d*top_1;
        buf_2=to_2d*top_3;//3
        SDL_RenderDrawLine(ren,buf[0][0],buf[1][0],buf_2[0][0],buf_2[1][0]);
        buf=to_2d*top_2;
        buf_2=to_2d*top_4;//4
        SDL_RenderDrawLine(ren,buf[0][0],buf[1][0],buf_2[0][0],buf_2[1][0]);
        buf=to_2d*bottom_1;
        buf_2=to_2d*bottom_2;//5
        SDL_RenderDrawLine(ren,buf[0][0],buf[1][0],buf_2[0][0],buf_2[1][0]);
        buf=to_2d*bottom_3;
        buf_2=to_2d*bottom_4;//6
        SDL_RenderDrawLine(ren,buf[0][0],buf[1][0],buf_2[0][0],buf_2[1][0]);
        buf=to_2d*bottom_1;
        buf_2=to_2d*bottom_3;//7
        SDL_RenderDrawLine(ren,buf[0][0],buf[1][0],buf_2[0][0],buf_2[1][0]);
        buf=to_2d*bottom_2;
        buf_2=to_2d*bottom_4;//8
        SDL_RenderDrawLine(ren,buf[0][0],buf[1][0],buf_2[0][0],buf_2[1][0]);
        buf=to_2d*top_1;
        buf_2=to_2d*bottom_1;//9
        SDL_RenderDrawLine(ren,buf[0][0],buf[1][0],buf_2[0][0],buf_2[1][0]);
        buf=to_2d*top_2;
        buf_2=to_2d*bottom_2;//10
        SDL_RenderDrawLine(ren,buf[0][0],buf[1][0],buf_2[0][0],buf_2[1][0]);
        buf=to_2d*top_3;
        buf_2=to_2d*bottom_3;//11
        SDL_RenderDrawLine(ren,buf[0][0],buf[1][0],buf_2[0][0],buf_2[1][0]);
        buf=to_2d*top_4;
        buf_2=to_2d*bottom_4;//12
        SDL_RenderDrawLine(ren,buf[0][0],buf[1][0],buf_2[0][0],buf_2[1][0]);
        SDL_RenderPresent(ren);
        SDL_Delay(100);
    }

    SDL_DestroyTexture(sprite);
    SDL_FreeSurface(bmp);
    SDL_DestroyRenderer(ren);
    SDL_DestroyWindow(win);
    SDL_Quit();
    return 0;
}
