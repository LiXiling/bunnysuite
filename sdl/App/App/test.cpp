#include "SDL.h"
#include "SDL_image.h"
#include <iostream>
#include <fstream>
#include <cstdlib>
#include <string>
#include <time.h>

using namespace std;

#define NUM_MAX_BUNNIES 100
#define REPETITIONS 10

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

double renderTimes[REPETITIONS];
int frameNo;

class Bunny {
public:
	SDL_Rect rect;
	double rotation;

	Bunny(){
		rect.x = rand() % SCREEN_X;
		rect.y = rand() % SCREEN_Y;
		rect.w = rect_bunny.w;
		rect.h = rect_bunny.h;
		rotation = 0.0;
	}
};

Bunny *bunnies;

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
		double scale_x = ((double)rand() / RAND_MAX)*4.8 + 0.2;
		double scale_y = ((double)rand() / RAND_MAX)*4.8 + 0.2;
		bunnies[i].rect.w = rect_bunny.w * scale_x;
		bunnies[i].rect.h = rect_bunny.h * scale_y;
		SDL_RenderCopy(ren, bunny, NULL, &bunnies[i].rect);
	}
	// show result
	SDL_RenderPresent(ren);
}

// ##### rotation #####
void renderFrameRotated(SDL_Renderer* ren){
	// make the window black
	SDL_RenderClear(ren);
	for (int i = 0; i < n; ++i){
		// perform the rotation
		bunnies[i].rotation += 0.2;
		// render rotated bunny
		//SDL_RenderCopy(ren, bunny, NULL, &(bunnies[i].rect));
		SDL_RenderCopyEx(ren, bunny, NULL, &bunnies[i].rect, bunnies[i].rotation, NULL, SDL_FLIP_NONE);
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
	else if (test_name == "rotation"){
		renderFrame = renderFrameRotated;
	}
	else {
		renderFrame = NULL;
	}
}

int main(int argc, char* argv[]){
	if (argc < 5){
		// missing arguments?
		cout << "Missing arguments. We assume some standard values for testing." << endl;
		test_name = "rotation";
		min_val = 1000;
		max_val = 20000;
		step = 1000;
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

	// prepare bunny
	bunny = IMG_LoadTexture(ren, "bunny.png");
	rect_bunny.x = 0;
	rect_bunny.y = 0;
	SDL_QueryTexture(bunny, NULL, NULL, &rect_bunny.w, &rect_bunny.h);
	bunnies = new Bunny[max_val];

	// prepare test
	setFrameRenderFunction();

	// prepare timing
	Uint32 time_last_log;
	Uint32 time_current;
	frameNo = 0;

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
		// measure frame time
		time_current = SDL_GetTicks();
		renderTimes[frameNo] = (time_current - time_last_log) / 1000.0;
		time_last_log = time_current;
		// check if one second is over
		frameNo += 1;
		if (frameNo == REPETITIONS){
			double renderTime = 0.0;
			for (int i = 0; i < REPETITIONS; ++i){
				renderTime += renderTimes[i];
			}
			renderTime /= REPETITIONS;
			logfile << n << "\t" << renderTime << endl;
			frameNo = 0;
			// set to next value
			n += step;
			if (n > max_val)
				break;
		}
	}

	// exit everything
	logfile.close();
	IMG_Quit();
	SDL_Quit();
	delete[] bunnies;
	return 0;
}