#include "include/map.h"
#include "hero.h"

using namespace std;

int main(int argc,char*argv[])
{
    srand(time(NULL));
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
    SDL_Rect srcrect;
    SDL_Rect dstrect;

    srcrect.x = 0;
    srcrect.y = 0;
    srcrect.w = 60;
    srcrect.h = 60;
    dstrect.x = 0;
    dstrect.y = 0;
    dstrect.w = 60;
    dstrect.h = 60;
    SDL_Rect srcrect_2;
    SDL_Rect dstrect_2;

    srcrect_2.x = 0;
    srcrect_2.y = 0;
    srcrect_2.w = 60;
    srcrect_2.h = 60;
    dstrect_2.x = 60;
    dstrect_2.y = 0;
    dstrect_2.w = 60;
    dstrect_2.h = 60;
    SDL_Rect srcrect_3;
    SDL_Rect dstrect_3;

    srcrect_3.x = 0;
    srcrect_3.y = 0;
    srcrect_3.w = 60;
    srcrect_3.h = 60;
    dstrect_3.x = 120;
    dstrect_3.y = 0;
    dstrect_3.w = 60;
    dstrect_3.h = 60;

    SDL_Rect srcrect_4;
    SDL_Rect dstrect_4;

    srcrect_4.x = 0;
    srcrect_4.y = 0;
    srcrect_4.w = 160;
    srcrect_4.h = 160;
    dstrect_4.x = 0;
    dstrect_4.y = 0;
    dstrect_4.w = 160;
    dstrect_4.h = 160;

    SDL_Rect srcrect_5;
    SDL_Rect dstrect_5;

    srcrect_5.x = 0;
    srcrect_5.y = 0;
    srcrect_5.w = 60;
    srcrect_5.h = 60;
    dstrect_5.x = 180;
    dstrect_5.y = 0;
    dstrect_5.w = 60;
    dstrect_5.h = 60;
//TEXTURES*********************************************************************************
//1
    SDL_Surface *bmp = SDL_LoadBMP("textures/grass.bmp");
    if (bmp == nullptr)
    {
        SDL_DestroyRenderer(ren);
        SDL_DestroyWindow(win);
        std::cout << "SDL_LoadBMP Error: " << SDL_GetError() << std::endl;
        SDL_Quit();
        return 1;
    }

    SDL_Texture *grass = SDL_CreateTextureFromSurface(ren, bmp);
    if (grass == nullptr)
    {
        SDL_DestroyRenderer(ren);
        SDL_DestroyWindow(win);
        std::cout << "SDL_CreateTextureFromSurface Error: " << SDL_GetError() << std::endl;
        SDL_Quit();
        return 1;
    }
//2
    bmp = SDL_LoadBMP("textures/water.bmp");
    if (bmp == nullptr)
    {
        SDL_DestroyRenderer(ren);
        SDL_DestroyWindow(win);
        std::cout << "SDL_LoadBMP Error: " << SDL_GetError() << std::endl;
        SDL_Quit();
        return 1;
    }

    SDL_Texture *water = SDL_CreateTextureFromSurface(ren, bmp);
    if (water == nullptr)
    {
        SDL_DestroyRenderer(ren);
        SDL_DestroyWindow(win);
        std::cout << "SDL_CreateTextureFromSurface Error: " << SDL_GetError() << std::endl;
        SDL_Quit();
        return 1;
    }
//3
    bmp = SDL_LoadBMP("textures/rock.bmp");
    if (bmp == nullptr)
    {
        SDL_DestroyRenderer(ren);
        SDL_DestroyWindow(win);
        std::cout << "SDL_LoadBMP Error: " << SDL_GetError() << std::endl;
        SDL_Quit();
        return 1;
    }

    SDL_Texture *rock = SDL_CreateTextureFromSurface(ren, bmp);
    if (rock == nullptr)
    {
        SDL_DestroyRenderer(ren);
        SDL_DestroyWindow(win);
        std::cout << "SDL_CreateTextureFromSurface Error: " << SDL_GetError() << std::endl;
        SDL_Quit();
        return 1;
    }
//4
    bmp = IMG_Load("textures/rabstol_net_square_07.jpg");
    if (bmp == nullptr)
    {
        SDL_DestroyRenderer(ren);
        SDL_DestroyWindow(win);
        std::cout << "SDL_LoadBMP Error: " << SDL_GetError() << std::endl;
        SDL_Quit();
        return 1;
    }
    //прозрачность белого фона (для спрайта бабочки)
    //SDL_SetColorKey(bmp,SDL_TRUE, SDL_MapRGB(bmp->format,255,255,255));

    SDL_Texture *sprite = SDL_CreateTextureFromSurface(ren, bmp);
    if (sprite == nullptr)
    {
        SDL_DestroyRenderer(ren);
        SDL_DestroyWindow(win);
        std::cout << "SDL_CreateTextureFromSurface Error: " << SDL_GetError() << std::endl;
        SDL_Quit();
        return 1;
    }
    int counter_3=0;
    SDL_Texture*arr[3];//array of textures
    int*arr_2[100];
    while (counter_3<100)
    {
        arr_2[counter_3]=0;
        ++counter_3;
    }
    arr [0]=grass;
    arr [1]=water;
    arr [2]=rock;
//END OF TEXTURES***************************************************************************
    SDL_Event e;
    SDL_KeyboardEvent m;
    bool quit = false;
    int mx,my;
    int swi=0,swi_2=0,swi_3=0,swi_4=0,counter=0,counter_2=0;
    while (!quit)
    {
        while (SDL_PollEvent(&e))
        {
            if (mx<160 && my<160)
            {
                srcrect_4.x = 2400;
            }
            else
            {
                srcrect_4.x = 0;
            }
            if( e.type == SDL_MOUSEBUTTONDOWN )//проверка - нажата ли левая кнопка мыши
            {
                if( e.button.button == SDL_BUTTON_LEFT )
                {
                    if (mx<60 && my<60)//проверка местонахождения курсора
                        quit = true;
                }
            }
            if (e.type == SDL_KEYDOWN && e.key.repeat == 0)//защита от повторного срабатывания
            {
                if (e.key.keysym.sym==SDLK_LEFT && dstrect_4.x>0)
                {
                    dstrect_4.x-=60;
                }
                if (e.key.keysym.sym==SDLK_DOWN && dstrect_4.y<540)
                {
                    dstrect_4.y+=60;
                }
                if (e.key.keysym.sym==SDLK_UP && dstrect_4.y>0)
                {
                    dstrect_4.y-=60;
                }
                if (e.key.keysym.sym == SDLK_RIGHT && dstrect_4.x<540)
                {
                    dstrect_4.x+=60;
                }
                if (e.key.keysym.sym == SDLK_SPACE)
                {
                    quit = true;
                }
            }
        }
        SDL_GetMouseState(&mx, &my);//В эти переменные будут записаны координаты курсора.
        //First clear the renderer
        SDL_RenderClear(ren);
        /*if (counter==80)
        {
            counter=0;
            swi=0;
            srcrect_4.x=0;
            srcrect_4.y=0;
        }
        if (swi==0)
            ++swi;
        if (swi==13)
        {
            srcrect_4.x=0;
            srcrect_4.y+=65;
            swi=1;
        }
        else
        {
            srcrect_4.x+=70;
            ++swi;
        }
        ++counter;*/
        while (counter_2<100)
        {
            if (dstrect.x==600)
            {
                dstrect.x=0;
                dstrect.y+=60;
            }
            ++counter_2;
            SDL_RenderCopy(ren, grass/*arr[rand()%3]*/, &srcrect, &dstrect);
            dstrect.x+=60;
        }
        SDL_RenderCopy(ren,sprite,&srcrect_4,&dstrect_4);
        dstrect.x=0;
        dstrect.y=0;
        counter_2=0;
        //Draw the textur
        //SDL_RenderCopy(ren, sprite, &srcrect_4, &dstrect_4);
        //SDL_RenderCopy(ren, water, &srcrect_2, &dstrect_2);
        //Update the screen
        SDL_RenderPresent(ren);
        //Take a quick break after all that hard work
        SDL_Delay(100);
    }

    SDL_FreeSurface(bmp);
    SDL_DestroyTexture(sprite);
    SDL_DestroyTexture(rock);
    SDL_DestroyTexture(water);
    SDL_DestroyTexture(grass);
    SDL_DestroyRenderer(ren);
    SDL_DestroyWindow(win);
    SDL_Quit();
    return 0;
}
