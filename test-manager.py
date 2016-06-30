# This script runs the tests automatically and analyses the results


# SUPPORTED TESTS
# standard
# random
# scaled

# animation
# rotation
# textureChange



import subprocess
import numpy as np
import matplotlib.pyplot as plt

def run_test(frameworks, test_name, min_val, max_val, step):
	for framework in frameworks:
		if framework == "LibGDX":
			subprocess.call(["java", "-jar", "App.jar", test_name, str(min_val), str(max_val), str(step)], cwd=framework+"/bin")
		else:
			subprocess.call([framework + "/bin/App.exe", test_name, str(min_val), str(max_val), str(step)], cwd=framework+"/bin")
	make_diagram(frameworks, test_name)

def make_diagram(frameworks, test_name):
	for framework in frameworks:
		(x, y) = get_data_from_log(framework, test_name)
		plt.plot(x, y, label=framework)
	plt.title("Results of the test: " + test_name)
	plt.xlabel("#")
	plt.ylabel("Average rendering time in seconds")
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
		y = np.array([float(v[1]) for v in values])
		return (x, y)

###################################################################################################		
		
frameworks = ['SDL','xna_monogame','LibGDX']

# run some tests for all frameworks
run_test(frameworks, "random", 1000, 20000, 1000)

###################################################################################################
