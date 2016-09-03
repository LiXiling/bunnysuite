# Framework that runs the tests and generates diagrams with the test results

import subprocess
import numpy as np
import matplotlib.pyplot as plt

def run_test(frameworks, test_name, min_val, max_val, step, res_x=800, res_y=600, repetitions = 10):
    print (frameworks, test_name, min_val, max_val, step, res_x, res_y)
    for framework in frameworks:
        try:
            subprocess.call([framework + "/bin/App.exe", test_name, str(min_val), str(max_val), str(step), str(res_x), str(res_y), str(repetitions)], cwd=framework+"/bin")
        except:
            subprocess.call(["java", "-jar", "App.jar", test_name, str(min_val), str(max_val), str(step), str(res_x), str(res_y), str(repetitions)], cwd=framework+"/bin")    
    make_bar_diagram(frameworks, test_name)
    make_detail_diagram(frameworks, test_name)
    
def make_bar_diagram(frameworks, test_name):
    thresholds = {}
    for framework in frameworks:
        (x, y) = get_data_from_log(framework, test_name)
        thresholds[framework] = get_threshold_value_from_data(x,y)
    max_threshold = max(thresholds.values())
    width = 0.35
    i = np.arange(len(frameworks))
    plt.bar(i, thresholds.values(), width, color='y')
    plt.title("Threshold values of the test: " + test_name)
    plt.xlabel("Frameworks")
    plt.xticks(i + width/2.0, frameworks)
    plt.ylabel("# objects when framerate drops under 56 fps")
    plt.yticks(np.arange(0, max_threshold*1.5, max_threshold/6.0))
    plt.grid(True)
    plt.savefig("results/" + test_name + "_thresholds.png")
    plt.gcf().canvas.set_window_title("Threshold values of the test: " + test_name)
    plt.show()
        
def get_threshold_value_from_data(x,y):
    for i in range(0,len(y)):
        if y[i] > 0.019:
            return x[i]
    return x[-1]
        
def make_detail_diagram(frameworks, test_name):
    for framework in frameworks:
        (x, y) = get_data_from_log(framework, test_name)
        plt.plot(x, y, label=framework)
    plt.title("Results of the test: " + test_name)
    plt.xlabel("#")
    plt.ylabel("Average rendering time in seconds")
    plt.grid(True)
    plt.legend()
    plt.savefig("results/" + test_name + "_detailed.png")
    plt.gcf().canvas.set_window_title("Detailed results of the test: " + test_name)
    plt.show()
    
def get_data_from_log(framework, test_name):
    try:
        with open(framework + '/bin/log/' + test_name + ".log", 'r') as f:
            lines = f.read().splitlines()
            values = [l.split("\t") for l in lines]
            x = np.array([int(v[0]) for v in values])
            y = np.array([float(v[1]) for v in values])
            return (x, y)
    except:
        print ("no log file found for " + framework)
        return ([],[])
