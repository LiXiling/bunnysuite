#include "SDL.h"
#include "SDL_image.h"
#include <iostream>
#include <fstream>
#include <cstdlib>
#include <string>
#include <time.h>

using namespace std;

// the arguments
string test_name;
int min_val;
int max_val;
int step;
int SCREEN_X = 800;
int SCREEN_Y = 600;

void (*renderFrame)(SDL_Renderer*);

int n;
SDL_Rect rect_bunny;
SDL_Texture *bunny;

// ##### standard #####
void renderFrameStandard(SDL_Renderer* ren){
	// make the window black
	SDL_RenderClear(ren);
	// draw n normal bunnies in the top left corner
	rect_bunny.x = rect_bunny.y = 0;
	for (int i = 0; i < n; ++i){
		SDL_RenderCopy(ren, bunny, NULL, &rect_bunny);
	}
	// show result
	SDL_RenderPresent(ren);
}

// ##### random #####
void renderFrameRandom(SDL_Renderer* ren){
	// make the window black
	SDL_RenderClear(ren);
	// draw n normal bunnies at random places
	for (int i = 0; i < n; ++i){
		rect_bunny.x = rand() % SCREEN_X;
		rect_bunny.y = rand() % SCREEN_Y;
		SDL_RenderCopy(ren, bunny, NULL, &rect_bunny);
	}
	// show result
	SDL_RenderPresent(ren);
}

// ##### scaled #####
void renderFrameScaled(SDL_Renderer* ren){
	// make the window black
	SDL_RenderClear(ren);
	// draw n randomly scaled bunnies at random places
	for (int i = 0; i < n; ++i){
		SDL_Rect rect_bunny_scaled;
		rect_bunny_scaled.x = rand() % SCREEN_X;
		rect_bunny_scaled.y = rand() % SCREEN_Y;
		double scale_x = ((double)rand() / RAND_MAX)*4.8 + 0.2;
		double scale_y = ((double)rand() / RAND_MAX)*4.8 + 0.2;
		rect_bunny_scaled.w = rect_bunny.w * scale_x;
		rect_bunny_scaled.h = rect_bunny.h * scale_y;
		SDL_RenderCopy(ren, bunny, NULL, &rect_bunny_scaled);
	}
	// show result
	SDL_RenderPresent(ren);
}

// sets the function that is used to render a frame
void setFrameRenderFunction(){
	if (test_name == "standard"){
		renderFrame = renderFrameStandard;
	}
	else if (test_name == "random"){
		renderFrame = renderFrameRandom;
	}
	else if (test_name == "scaled"){
		renderFrame = renderFrameScaled;
	}
	else {
		renderFrame = NULL;
	}
}

int main(int argc, char* argv[]){
	if (argc < 5){
		// missing arguments?
		cout << "Missing arguments. We assume some standard values for testing." << endl;
		test_name = "standard";
		min_val = 0;
		max_val = 40000;
		step = 500;
	} else {
		// read the arguments
		test_name = string(argv[1]);
		min_val = atoi(argv[2]);
		max_val = atoi(argv[3]);
		step = atoi(argv[4]);
	}
	n = min_val;

	// start SDL window
	cout << "Starting SDL" << endl;
	SDL_Init(SDL_INIT_VIDEO);
	IMG_Init(IMG_INIT_PNG);
	SDL_Window *win = SDL_CreateWindow("SDL", 100, 100, SCREEN_X, SCREEN_Y, SDL_WINDOW_SHOWN);
	SDL_Renderer *ren = SDL_CreateRenderer(win, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
	bunny = IMG_LoadTexture(ren, "bunny.png");
	rect_bunny.x = 0;
	rect_bunny.y = 0;
	SDL_QueryTexture(bunny, NULL, NULL, &rect_bunny.w, &rect_bunny.h);

	// prepare test
	setFrameRenderFunction();

	// prepare timing
	Uint32 time_last_log;
	Uint32 time_current;
	int framerate = 0;

	// seed random number generator
	srand(time(NULL));

	// prepare logging
	ofstream logfile;
	logfile.open("log/" + test_name + ".log");
	if (!logfile.is_open()){
		cout << "Error: logfile could not be opened.";
		return 1;
	}
	// main loop
	time_last_log = SDL_GetTicks();
	while (true){
		renderFrame(ren);
		framerate += 1;
		// check if one second is over
		time_current = SDL_GetTicks();
		if (time_current - time_last_log > 1000){
			// if yes, then we log the framerate and measure the next value
			logfile << n << "\t" << framerate << endl;
			framerate = 0;
			time_last_log = time_current;
			n += step;
			if (n > max_val)
				break;
		}
	}

	// exit everything
	logfile.close();
	IMG_Quit();
	SDL_Quit();
	return 0;
}