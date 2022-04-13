#include<SDL2/SDL.h>
#include<SDL2/SDL_video.h>
using namespace std;

const int WINDOW_HEIGHT = 480;
const int WINDOW_WEDTH = 640;

int main(){
    SDL_Init(SDL_INIT_VIDEO);
    SDL_Window* window = SDL_CreateWindow("Hello, world", SDL_WINDOWPOS_UNDEFINED,
        SDL_WINDOWPOS_UNDEFINED, WINDOW_WEDTH, WINDOW_HEIGHT, SDL_WINDOW_SHOWN);
    SDL_Surface* surface = SDL_GetWindowSurface(window);
    SDL_FillRect(surface, NULL, SDL_MapRGB(surface -> format, 0xff, 0xff, 0xff));
    SDL_UpdateWindowSurface(window);
    SDL_Event e;
    bool quit = false;
    while (!quit){
        while (SDL_PollEvent(&e)){
            if (e.type == SDL_QUIT){
                quit = true;
            }
            if (e.type == SDL_KEYDOWN){
                quit = true;
            }
            if (e.type == SDL_MOUSEBUTTONDOWN){
                quit = true;
            }
        }
    }
    SDL_DestroyWindow(window);
    SDL_Quit();
    return 0;
}