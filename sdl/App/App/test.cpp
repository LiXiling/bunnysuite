#include "SDL.h"
#include "SDL_image.h"
#include <iostream>
#include <fstream>
#include <cstdlib>
#include <string>

using namespace std;

// the arguments
string test_name;
int min_val;
int max_val;
int step;

void (*renderFrame)(SDL_Renderer*);

int n;
SDL_Rect size_bunny;
SDL_Texture *bunny;

// ##### standard #####
void renderFrameStandard(SDL_Renderer* ren){
	// make the window black
	SDL_RenderClear(ren);
	// draw n normal bunnies in the top left corner
	for (int i = 0; i < n; ++i){
		SDL_RenderCopy(ren, bunny, NULL, &size_bunny);
	}
	// show result
	SDL_RenderPresent(ren);
}

// sets the function that is used to render a frame
void setFrameRenderFunction(){
	if (test_name == "standard"){
		renderFrame = renderFrameStandard;
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
	SDL_Window *win = SDL_CreateWindow("SDL", 100, 100, 640, 480, SDL_WINDOW_SHOWN);
	SDL_Renderer *ren = SDL_CreateRenderer(win, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
	bunny = IMG_LoadTexture(ren, "bunny.png");
	size_bunny.x = 0;
	size_bunny.y = 0;
	SDL_QueryTexture(bunny, NULL, NULL, &size_bunny.w, &size_bunny.h);

	// prepare test
	setFrameRenderFunction();

	// prepare timing
	Uint32 time_last_log;
	Uint32 time_current;
	int framerate = 0;

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