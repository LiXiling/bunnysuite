#include "SDL.h"
#include "SDL_image.h"
#include <iostream>
#include <fstream>
#include <cstdlib>
#include <string>
#include "json/json.h"

using namespace std;

// the arguments
string test_name;
string jsonStr;
string ind_var;
int min_val;
int max_val;
int step;
int* iv;


// the parameters
int num_bunnies_normal;
int num_bunnies_rotated;
int num_bunnies_scaled;
string file_bunny;
SDL_Rect size_bunny;
SDL_Texture *bunny;

// returns a pointer on the independant variable
int* getIVPointer(string ind_var){
	if (ind_var == "num_bunnies_normal")
		return &num_bunnies_normal;
	else
		return NULL;
}

// render a frame
void renderFrame(SDL_Renderer* ren){
	// make the window black
	SDL_RenderClear(ren);
	// draw normal bunnies
	for (int i = 0; i < num_bunnies_normal; ++i){
		SDL_RenderCopy(ren, bunny, NULL, &size_bunny);
	}

	// TODO ...

	// show result
	SDL_RenderPresent(ren);
}

int main(int argc, char* argv[]){
	if (argc < 6){
		// missing arguments?
		cout << "Missing arguments. We assume some standard values for testing." << endl;
		test_name = "standard";
		jsonStr = "{\"num_bunnies_normal\" : 0,\"num_bunnies_rotated\" : 0,\"file_bunny\" : \"./orx/data/wabbit_alpha.png\"}";
		ind_var = "num_bunnies_normal";
		min_val = 0;
		max_val = 40000;
		step = 500;
	} else {
		// read the parameters
		test_name = string(argv[1]);
		jsonStr = string(argv[2]);
		ind_var = string(argv[3]);
		min_val = atoi(argv[4]);
		max_val = atoi(argv[5]);
		step = atoi(argv[6]);
	}
	iv = getIVPointer(ind_var);
	*iv = min_val;

	// parse JSON
	Json::Value jsonObj;
	Json::Reader reader;
	if (!reader.parse(jsonStr.c_str(), jsonObj)){
		cout << "JSON parsing failed!" << endl;
		return 1;
	}

	// start SDL window
	cout << "Starting SDL" << endl;
	SDL_Init(SDL_INIT_VIDEO);
	IMG_Init(IMG_INIT_PNG);
	SDL_Window *win = SDL_CreateWindow("SDL", 100, 100, 640, 480, SDL_WINDOW_SHOWN);
	SDL_Renderer *ren = SDL_CreateRenderer(win, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
	file_bunny = jsonObj.get("file_bunny", "").asString();
	bunny = IMG_LoadTexture(ren, file_bunny.c_str());
	size_bunny.x = 0;
	size_bunny.y = 0;
	SDL_QueryTexture(bunny, NULL, NULL, &size_bunny.w, &size_bunny.h);

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
	logfile << jsonStr << endl;
	// main loop
	time_last_log = SDL_GetTicks();
	while (true){
		renderFrame(ren);
		framerate += 1;
		// check if one second is over
		time_current = SDL_GetTicks();
		if (time_current - time_last_log > 1000){
			// if yes, then we log the framerate and measure the next value
			logfile << *iv << "\t" << framerate << endl;
			framerate = 0;
			time_last_log = time_current;
			*iv += step;
			if (*iv > max_val)
				break;
		}
		
	}

	// exit everything
	logfile.close();
	IMG_Quit();
	SDL_Quit();
	return 0;
}