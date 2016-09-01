#define TEST "animation,points"

#include "SDL.h"
#include "SDL_image.h"
#include "SDL2_gfxPrimitives.h"
#include <iostream>
#include <fstream>
#include <cstdlib>
#include <string>
#include <vector>
#include <time.h>

using namespace std;

#define NUM_MAX_BUNNIES 100000
#define NUM_MAX_TEXTURES 16

#define NATURE_BUNNY 0
#define NATURE_TRIANGLE 1
#define NATURE_CIRCLE 2
#define NATURE_RECT 3
#define NATURE_LINE 4
#define NATURE_PARTICLE 5
#define NATURE_TEXT 6

// the arguments
string test_name;
int min_val;
int max_val;
int step;
int SCREEN_X;
int SCREEN_Y;
int REPETITIONS;

bool no_output = false;

int n;
SDL_Texture *bunnyTexture[NUM_MAX_TEXTURES];
int numTextures = 0;
bool tinted = false;

vector<int> natures;

int frameNo;
double renderTime;

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
	double pulsationX;
	double pulsationY;
	double speedX;
	double speedY;
	double rotation;
	int texture;
	SDL_Rect textureRect;
	int nature;
	SDL_Color color;
	int numVertices;
	double xVertices[4];
	double yVertices[4];

	Bunny(){
		x = 0.0;
		y = 0.0;
		scaleX = 1.0;
		scaleY = 1.0;
		pulsationX = 0.1;
		pulsationY = 0.1;
		speedX = 0.0;
		speedY = 0.0;
		rotation = 0.0;
		texture = rand() % numTextures;
		SDL_QueryTexture(bunnyTexture[texture], NULL, NULL, &textureRect.w, &textureRect.h);
		textureRect.w = int(floor((textureRect.w * 37.0 / textureRect.h)));
		textureRect.h = 37;
		nature = (natures.size() == 0) ? NATURE_BUNNY : natures.at(rand() % natures.size());
		if (nature == NATURE_TRIANGLE){
			numVertices = 3;
			xVertices[0] = -13.0;
			yVertices[0] = 12.0;
			xVertices[1] = 0.0;
			yVertices[1] = -25.0;
			xVertices[2] = 13.0;
			yVertices[2] = 12.0;
		}
		else if (nature == NATURE_RECT){
			numVertices = 4;
			xVertices[0] = -13.0;
			yVertices[0] = -18.0;
			xVertices[1] = -13.0;
			yVertices[1] = 19.0;
			xVertices[2] = 13.0;
			yVertices[2] = 19.0;
			xVertices[3] = 13.0;
			yVertices[3] = -18.0;
		} 
		else if (nature == NATURE_LINE){
			numVertices = 2;
			xVertices[0] = -13.0;
			yVertices[0] = -18.0;
			xVertices[1] = 13.0;
			yVertices[1] = 19.0;
		}
		color.a = 255;
		color.r = rand() % 256;
		color.g = rand() % 256;
		color.b = rand() % 256;
	}

	void rotate(double angle){
		rotation += angle;
		if (nature == NATURE_TRIANGLE || nature == NATURE_RECT || nature == NATURE_LINE){
			rotateVertices(angle);
		}
	}

	void rotateVertices(double angle){
		double s = sin(-angle * M_PI / 180.0);
		double c = cos(-angle * M_PI / 180.0);
		for (int i = 0; i < numVertices; ++i){
			xVertices[i] = xVertices[i] * c + yVertices[i] * s;
			yVertices[i] = -xVertices[i] * s + yVertices[i] * c;
		}
	}

	void render(SDL_Renderer *ren){
		// render rotated and scaled bunny
		if (nature == NATURE_BUNNY){
			SDL_Rect rect = textureRect;
			rect.x = int(floor(x - rect.w*scaleX / 2));
			rect.y = int(floor(y - rect.h*scaleY / 2));
			rect.w = int(floor(rect.w * scaleX));
			rect.h = int(floor(rect.h * scaleY));
			if(tinted)
				SDL_SetTextureColorMod(bunnyTexture[texture], color.r, color.g, color.b);
			SDL_RenderCopyEx(ren, bunnyTexture[texture], NULL, &rect, rotation, NULL, SDL_FLIP_NONE);
		}
		// render triangle
		else if (nature == NATURE_TRIANGLE) {
			trigonRGBA(ren, 
				int(floor(x + xVertices[0] * scaleX)), int(floor(y + yVertices[0] * scaleY)),
				int(floor(x + xVertices[1] * scaleX)), int(floor(y + yVertices[1] * scaleY)),
				int(floor(x + xVertices[2] * scaleX)), int(floor(y + yVertices[2] * scaleY)),
				color.r, color.g, color.b, color.a);
		}
		// render circle
		else if (nature == NATURE_CIRCLE) {
			aacircleRGBA(ren, int(floor(x)), int(floor(y)), int(floor(scaleX*13)), color.r, color.g, color.b, color.a);
		}
		// render rectangle
		else if (nature == NATURE_RECT && false){
			rectangleRGBA(ren, 
				int(floor(x - 13.0)), int(floor(y - 18.0)),
				int(floor(x + 13.0)), int(floor(y + 19.0)), 
				color.r, color.g, color.b, color.a);
		}
		else if (nature == NATURE_RECT){
			Sint16 xp[4], yp[4];
			for (int i = 0; i < 4; ++i){
				xp[i] = int(floor(x + xVertices[i] * scaleX));
				yp[i] = int(floor(y + yVertices[i] * scaleY));
			}
			polygonRGBA(ren, xp, yp, 4, color.r, color.g, color.b, color.a);
		}
		// render line
		else if (nature == NATURE_LINE){
			aalineRGBA(ren, 
				int(floor(x + xVertices[0] * scaleX)), int(floor(y + yVertices[0] * scaleY)),
				int(floor(x + xVertices[1] * scaleX)), int(floor(y + yVertices[1] * scaleY)),
				color.r, color.g, color.b, color.a);
		}
		// render particle
		else if (nature == NATURE_PARTICLE){
			pixelRGBA(ren, int(floor(x)), int(floor(y)), color.r, color.g, color.b, color.a);
		}
		// render text
		else if (nature == NATURE_TEXT) {
			stringRGBA(ren, int(floor(x)), int(floor(y)), "Hello World :D", color.r, color.g, color.b, color.a);
		}
	}
};

Bunny *bunnies;

void addTexture(string name, SDL_Renderer* ren){
	bunnyTexture[numTextures] = IMG_LoadTexture(ren, (name + ".png").c_str());
	numTextures++;
}

void renderFrame(SDL_Renderer* ren){
	// make the window black
	SDL_SetRenderDrawColor(ren, 0, 0, 0, 255);
	SDL_RenderClear(ren);
	// render bunnies
	for (int i = 0; i < n; ++i){
		bunnies[i].render(ren);		
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
		if (test_name.find("pulsation") != string::npos){
			// bunnies grow and shrink
			bunnies[i].scaleX += bunnies[i].pulsationX;
			if (bunnies[i].scaleX >= 5.0 || bunnies[i].scaleX <= 0.2)
				bunnies[i].pulsationX *= -1;
			bunnies[i].scaleY += bunnies[i].pulsationY;
			if (bunnies[i].scaleY >= 5.0 || bunnies[i].scaleY <= 0.2)
				bunnies[i].pulsationY *= -1;
		}
		if (test_name.find("rotation") != string::npos){
			// perform the rotation
			bunnies[i].rotate(1.0);
		}
		if (test_name.find("texturechange") != string::npos){
			// set random texture
			bunnies[i].texture = rand() % numTextures;
			SDL_QueryTexture(bunnyTexture[bunnies[i].texture], NULL, NULL, 
				&bunnies[i].textureRect.w, &bunnies[i].textureRect.h);
		}
		if (test_name.find("colorchange") != string::npos){
			bunnies[i].color.r = rand() % 256;
			bunnies[i].color.g = rand() % 256;
			bunnies[i].color.b = rand() % 256;
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
		test_name = TEST;
		min_val = 1000;
		max_val = 50000;
		step = 1000;
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
	// number of repetitions is another optional parameter
	if (argc < 8){
		REPETITIONS = 10;
	}
	else {
		REPETITIONS = atoi(argv[7]);
	}

	n = min_val;

	// start SDL window
	cout << "Starting SDL" << endl;
	SDL_Init(SDL_INIT_VIDEO);
	IMG_Init(IMG_INIT_PNG);
	SDL_Window *win = SDL_CreateWindow("SDL", 100, 100, SCREEN_X, SCREEN_Y, SDL_WINDOW_SHOWN);
	SDL_Renderer *ren = SDL_CreateRenderer(win, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);

	// no_output?
	if (test_name.find("no_output") != string::npos){
		no_output = true;
	}
	
	// load textures depending on flags
	if (test_name.find("multitexture") != string::npos){
		addTexture("wabbit_alpha0", ren);
		addTexture("wabbit_alpha1", ren);
		addTexture("wabbit_alpha2", ren);
	}
	if (test_name.find("alpha") != string::npos){
		addTexture("wabbit_ghost", ren);
	}
	if (test_name.find("thin") != string::npos){
		addTexture("wabbit_y", ren);
	}
	if (test_name.find("hdtexture") != string::npos){
		addTexture("wabbit_hd", ren);
	}
	// default case
	if (numTextures == 0){
		addTexture("wabbit_alpha0", ren);
	}
	// tinted?
	if (test_name.find("tinted") != string::npos || test_name.find("colorchange") != string::npos){
		tinted = true;
	}

	// collect natures
	if (test_name.find("triangles") != string::npos){
		natures.push_back(NATURE_TRIANGLE);
	}
	if (test_name.find("circles") != string::npos){
		natures.push_back(NATURE_CIRCLE);
	}
	if (test_name.find("rectangles") != string::npos){
		natures.push_back(NATURE_RECT);
	}
	if (test_name.find("lines") != string::npos){
		natures.push_back(NATURE_LINE);
	}
	if (test_name.find("points") != string::npos){
		natures.push_back(NATURE_PARTICLE);
	}
	if (test_name.find("texts") != string::npos){
		natures.push_back(NATURE_TEXT);
	}
	if (test_name.find("bunnies") != string::npos){
		natures.push_back(NATURE_BUNNY);
	}

	// prepare bunny
	bunnies = new Bunny[max_val];
	setInitialValues();

	// prepare timing
	Uint32 time_last_log;
	Uint32 time_current;
	frameNo = 0;

	// seed random number generator
	srand((unsigned int)time(NULL));

	// prepare logging
	ofstream logfile;
	logfile.open("log/" + test_name + ".log");
	if (!logfile.is_open()){
		cout << "Error: logfile could not be opened.";
		return 1;
	}
	// main loop
	time_last_log = SDL_GetTicks();
	bool running = true;
	while (running){
		// pull events
		SDL_Event event;
		while (SDL_PollEvent(&event) != 0) {
			// close on X
			if (event.type == SDL_QUIT){
				running = false;
				break;
			}
		}
		// update
		updateBunnies();
		// render
		if (!no_output)
			renderFrame(ren);
		// check if #REPETITIONS frames are over
		frameNo += 1;
		if (frameNo == REPETITIONS){
			// measure time
			time_current = SDL_GetTicks();
			renderTime = (time_current - time_last_log) / (1000.0 * REPETITIONS);
			time_last_log = time_current;
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