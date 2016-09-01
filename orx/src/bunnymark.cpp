#include <iostream>
#include <fstream>
#include <time.h>
#include "orx.h"

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

int n;

int frameNo = 0;
clock_t lastMeasure;
clock_t currentMeasure;
ofstream logfile;


orxSTATUS orxFASTCALL Update(void *_pContext){
	// TODO update everything
	return orxSTATUS_SUCCESS;
}


orxSTATUS orxFASTCALL Init(){

  // Creates viewport
  orxVIEWPORT *vp = orxViewport_CreateFromConfig("Viewport");

  // TODO init bunnies

  // start the clock
  lastMeasure = clock();

  return orxSTATUS_SUCCESS;
}

void addBunnies(int num){
	for (int i = 0; i < num; ++i){
		orxOBJECT *b = orxObject_CreateFromConfig("Bunny");
	}
}

orxSTATUS orxFASTCALL Run(){
	// TODO exit on close
	if(false)  {
		return orxSTATUS_FAILURE;
	}

	frameNo++;
	if (frameNo > REPETITIONS){
		frameNo = 0;
		currentMeasure = clock();
		double renderTime = (currentMeasure - lastMeasure) / 1000.0;
		lastMeasure = currentMeasure;
		logfile << n << "\t" << renderTime << endl;
		n += step;
		addBunnies(step);
		if (n > max_val){
			return orxSTATUS_FAILURE;
		}
	}

	return orxSTATUS_SUCCESS;
}

void orxFASTCALL Exit()
{
  // We could delete everything we created here but orx will do it for us anyway =)
}

int main(int argc, char **argv)
{
	if (argc < 5){
		// missing arguments?
		cout << "Missing arguments. We assume some standard values for testing." << endl;
		test_name = "lines,random,pulsation,bunnies";
		min_val = 1;
		max_val = 50000;
		step = 1;
	}
	else {
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

	// prepare logging
	logfile.open("log/" + test_name + ".log");
	if (!logfile.is_open()){
		cout << "Error: logfile could not be opened.";
		return 1;
	}


	// TODO set screen size according to arguments!!!

	

	// start orx
	orx_Execute(argc, argv, Init, Run, Exit);

	return 0;
}
