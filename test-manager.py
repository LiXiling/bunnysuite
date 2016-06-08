# This script runs the tests automatically and analyses the results


# SUPPORTED TESTS
# standard
# random
# scaled

# animation
# rotation
# textureChange



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
	plt.title("Results of the test: " + test_name)
	plt.xlabel("#")
	plt.ylabel("fps")
	plt.grid(True)
	plt.legend()
	plt.savefig("results/" + test_name + ".png")
	plt.gcf().canvas.set_window_title("Results of the test: " + test_name)
	plt.show()
	
def get_data_from_log(framework, test_name):
	with open(framework + '/bin/log/' + test_name + ".log", 'r') as f:
		lines = f.read().splitlines()
		values = [l.split("\t") for l in lines]
		x = np.array([int(v[0]) for v in values])
		y = np.array([int(v[1]) for v in values])
		return (x, y)

###################################################################################################		
		
frameworks = ['xna_monogame','sdl']

# run some tests for all frameworks
run_test(frameworks, "standard", 1000, 5000, 50)
run_test(frameworks, "random", 1000, 5000, 50)
run_test(frameworks, "scaled", 1000, 5000, 50)

###################################################################################################
