#include "SDL.h"
#include "SDL_image.h"
#include <iostream>
#include <fstream>
#include <cstdlib>
#include <string>
#include <time.h>

using namespace std;

#define NUM_MAX_BUNNIES 100000
#define NUM_ANIM_FRAMES 3
#define REPETITIONS 10

// the arguments
string test_name;
int min_val;
int max_val;
int step;
int SCREEN_X;
int SCREEN_Y;

int n;
SDL_Rect rect_bunny;
SDL_Texture *bunny[NUM_ANIM_FRAMES];

double renderTimes[REPETITIONS];
int frameNo;

// helper function to create random doubles
double randomDouble(double min, double max)
{
	double f = (double)rand() / RAND_MAX;
	return min + f * (max - min);
}

class Bunny {
public:
	double x;
	double y;
	double scaleX;
	double scaleY;
	double speedX;
	double speedY;
	double rotation;
	int texture;

	Bunny(){
		x = 0.0;
		y = 0.0;
		scaleX = 1.0;
		scaleY = 1.0;
		speedX = 0.0;
		speedY = 0.0;
		rotation = 0.0;
		texture = 0;
	}
};

Bunny *bunnies;

void renderFrame(SDL_Renderer* ren){
	// make the window black
	SDL_RenderClear(ren);
	for (int i = 0; i < n; ++i){
		// set scale and position
		SDL_Rect rect = rect_bunny;
		rect.x = bunnies[i].x;
		rect.y = bunnies[i].y;
		rect.w *= bunnies[i].scaleX;
		rect.h *= bunnies[i].scaleY;
		// render rotated bunny
		SDL_RenderCopyEx(ren, bunny[bunnies[i].texture], NULL, &rect, bunnies[i].rotation, NULL, SDL_FLIP_NONE);
	}
	// show result
	SDL_RenderPresent(ren);
}

// set initial bunny values
void setInitialValues(){
	for (int i = 0; i < max_val; ++i){
		if (test_name.find("random") != string::npos){
			// set bunny to random position
			bunnies[i].x = rand() % SCREEN_X;
			bunnies[i].y = rand() % SCREEN_Y;
		}
		if (test_name.find("scaled") != string::npos){
			// random scaling
			bunnies[i].scaleX = randomDouble(0.2, 5.0);
			bunnies[i].scaleY = randomDouble(0.2, 5.0);
		}
		if (test_name.find("multitexture") != string::npos){
			// set random texture
			bunnies[i].texture = rand() % NUM_ANIM_FRAMES;
		}
		if (test_name.find("animation") != string::npos){
			// set random speed
			bunnies[i].speedX = randomDouble(0.0, 5.0);
			bunnies[i].speedY = randomDouble(-2.5, 2.5);
		}
	}
}

// updates bunny values for a new frame
void updateBunnies(){
	for (int i = 0; i < n; ++i){
		if (test_name.find("teleport") != string::npos){
			// set bunny to new random position
			bunnies[i].x = rand() % SCREEN_X;
			bunnies[i].y = rand() % SCREEN_Y;
		}
		if (test_name.find("scaling") != string::npos){
			// random rescaling
			bunnies[i].scaleX = randomDouble(0.2, 5.0);
			bunnies[i].scaleY = randomDouble(0.2, 5.0);
		}
		if (test_name.find("rotation") != string::npos){
			// perform the rotation
			bunnies[i].rotation += 1.0;
		}
		if (test_name.find("texturechange") != string::npos){
			// set random texture
			bunnies[i].texture = rand() % NUM_ANIM_FRAMES;
		}
		if (test_name.find("animation") != string::npos){
			bunnies[i].x += bunnies[i].speedX;
			if (bunnies[i].x < 0 || bunnies[i].x > SCREEN_X){
				bunnies[i].speedX *= -1;
			}
			// add gravity
			bunnies[i].speedY += 0.5;
			bunnies[i].y += bunnies[i].speedY;
			// collision with floor
			if (bunnies[i].y > SCREEN_Y){
				bunnies[i].y = SCREEN_Y;
				bunnies[i].speedY *= -0.8;
				if (rand() % 2 == 0)
					bunnies[i].speedY -= randomDouble(3.0, 7.0);
			}
			// collision with ceiling
			if (bunnies[i].y < 0){
				bunnies[i].speedY = 0.0;
			}
		}
		
	}
}

int main(int argc, char* argv[]){
	if (argc < 5){
		// missing arguments?
		cout << "Missing arguments. We assume some standard values for testing." << endl;
		test_name = "animation,multitexture";
		min_val = 10;
		max_val = 2000;
		step = 10;
		
	} else {
		// read the arguments
		test_name = string(argv[1]);
		min_val = atoi(argv[2]);
		max_val = atoi(argv[3]);
		step = atoi(argv[4]);
		
	}
	// screensize is optional parameter
	if (argc < 7){
		SCREEN_X = 800;
		SCREEN_Y = 600;
	}
	else {
		SCREEN_X = atoi(argv[5]);
		SCREEN_Y = atoi(argv[6]);
	}

	n = min_val;

	// start SDL window
	cout << "Starting SDL" << endl;
	SDL_Init(SDL_INIT_VIDEO);
	IMG_Init(IMG_INIT_PNG);
	SDL_Window *win = SDL_CreateWindow("SDL", 100, 100, SCREEN_X, SCREEN_Y, SDL_WINDOW_SHOWN);
	SDL_Renderer *ren = SDL_CreateRenderer(win, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);

	// prepare bunny
	bunny[0] = IMG_LoadTexture(ren, "wabbit_alpha0.png");
	bunny[1] = IMG_LoadTexture(ren, "wabbit_alpha1.png");
	bunny[2] = IMG_LoadTexture(ren, "wabbit_alpha2.png");
	rect_bunny.x = 0;
	rect_bunny.y = 0;
	SDL_QueryTexture(bunny[0], NULL, NULL, &rect_bunny.w, &rect_bunny.h);
	bunnies = new Bunny[max_val];
	setInitialValues();

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
		updateBunnies();
		renderFrame(ren);
		// measure frame time
		time_current = SDL_GetTicks();
		renderTimes[frameNo] = (time_current - time_last_log) / 1000.0;
		time_last_log = time_current;
		// check if #REPETITIONS frames are over
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