# This script runs the tests automatically and analyses the results

import subprocess
import json
import numpy as np
import matplotlib.pyplot as plt

def run_test(frameworks, test_name, min_val, max_val, step):
	for framework in frameworks:
		subprocess.call([framework + "/bin/App.exe", test_name, str(min_val), str(max_val), str(step)], cwd=framework+"/bin")
	make_diagram(frameworks, test_name)

def make_diagram(frameworks, test_name):
	for framework in frameworks:
		(x, y) = get_data_from_log(framework, test_name)
		plt.plot(x, y, label=framework)
	plt.title("Results of the test " + test_name)
	plt.xlabel("#")
	plt.ylabel("fps")
	plt.grid(True)
	plt.legend()
	plt.savefig("results/" + test_name + ".png")
	plt.show()
	
def get_data_from_log(framework, test_name):
	with open(framework + '/bin/log/' + test_name + ".log", 'r') as f:
		lines = f.read().splitlines()
		values = [l.split("\t") for l in lines]
		x = np.array([int(v[0]) for v in values])
		y = np.array([int(v[1]) for v in values])
		return (x, y)

###################################################################################################		
		
frameworks = ['sdl','xna_monogame']

# run some tests for all frameworks
run_test(frameworks, "standard", 0, 40000, 500)

###################################################################################################

