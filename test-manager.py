# This script runs the tests automatically and analyses the results

import subprocess
import json
import numpy as np
import matplotlib.pyplot as plt

def run_test(frameworks, test_name, params, ind_var, min_val, max_val, step):
	for framework in frameworks:
		subprocess.call([framework + "/App.exe", test_name, json.dumps(params), ind_var, str(min_val), str(max_val), str(step)], cwd=framework)
	make_diagram(frameworks, test_name, ind_var)

def make_diagram(frameworks, test_name, ind_var):
	for framework in frameworks:
		(x, y) = get_data_from_log(framework, test_name)
		plt.plot(x, y, label=framework)
	plt.title("Results of the test " + test_name)
	plt.xlabel(ind_var)
	plt.ylabel("fps")
	plt.grid(True)
	plt.legend()
	plt.savefig("results/" + test_name + ".png")
	plt.show()
	
def get_data_from_log(framework, test_name):
	with open(framework + '/log/' + test_name + ".log", 'r') as f:
		lines = f.read().splitlines()
		lines.pop(0)	# ignore the parameter line
		values = [l.split("\t") for l in lines]
		x = np.array([int(v[0]) for v in values])
		y = np.array([int(v[1]) for v in values])
		return (x, y)

###################################################################################################		
		
frameworks = ['xna_monogame/App/App/bin/x86/Release']

#do we really need params? difficult to process. 
#PNG-File compiled into .exe in XNA, if I am not mistaken
params = {
	"num_bunnies_normal" : 1000,
	"num_bunnies_rotated" : 0,
	"file_bunny" : "./orx/data/wabbit_alpha.png"
}

# run some tests for all frameworks
run_test(frameworks, "standard", params, "num_bunnies_normal", 0, 60000, 50)
#run_test(frameworks, "rotated", params, "num_bunnies_rotated", 0, 40000, 500)

###################################################################################################

