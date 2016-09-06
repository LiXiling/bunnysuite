# Framework that runs the tests and generates diagrams with the test results

import subprocess
import numpy as np
import matplotlib.pyplot as plt
import datetime

def run_test(frameworks, test_name, min_val, max_val, step, res_x=800, res_y=600, repetitions = 10):
    print (frameworks, test_name, min_val, max_val, step, res_x, res_y)
    for framework in frameworks:
        try:
            subprocess.call([framework + "/bin/App.exe", test_name, str(min_val), str(max_val), str(step), str(res_x), str(res_y), str(repetitions)], cwd=framework+"/bin")
        except:
            subprocess.call(["java", "-jar", "App.jar", test_name, str(min_val), str(max_val), str(step), str(res_x), str(res_y), str(repetitions)], cwd=framework+"/bin")    
    timestamp = datetime.datetime.now().strftime("%Y%m%d-%H-%M-%S")
    make_report_file(frameworks, test_name, timestamp, min_val, max_val, step, repetitions, res_x, res_y)
    make_bar_diagram(frameworks, test_name, timestamp)
    make_detail_diagram(frameworks, test_name, timestamp)
     
def make_bar_diagram(frameworks, test_name, name_prefix):
    thresholds = []
    for framework in frameworks:
        (x, y) = get_data_from_log(framework, test_name)
        thresholds.append(get_threshold_value_from_data(x,y))
    max_threshold = max(thresholds)
    width = 0.35
    i = np.arange(len(frameworks))
    plt.bar(i, thresholds, width, color='y')
    plt.title("Threshold values")
    plt.xlabel("Frameworks")
    plt.xticks(i + width/2.0, frameworks)
    plt.ylabel("# objects when framerate drops under 56 fps")
    plt.yticks(np.arange(0, max_threshold*1.5, max_threshold/6.0))
    plt.grid(True)
    plt.savefig("results/" + name_prefix + "_thresholds.png")
    plt.gcf().canvas.set_window_title("Threshold values of the test: " + test_name)
    plt.show()
        
def get_threshold_value_from_data(x,y):
    if len(y) == 0:
        return 0
    for i in reversed(range(0, len(y))):
        if y[i] < 0.02:
            return x[i]
    return x[0]
        
def make_detail_diagram(frameworks, test_name, name_prefix):
    for framework in frameworks:
        (x, y) = get_data_from_log(framework, test_name)
        plt.plot(x, y, label=framework)
    plt.title("Rendering times")
    plt.xlabel("#")
    plt.ylabel("Average rendering time in seconds")
    plt.grid(True)
    plt.legend(loc=2)
    plt.savefig("results/" + name_prefix + "_detailed.png")
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
        
def make_report_file(frameworks, test_name, name_prefix, minBunnies,
                     maxBunnies, stepSize, repetitions, width, height):
    outString = 'Bunnysuite report created at %s\n\n' % datetime.datetime.now().strftime("%Y/%m/%d %H:%M")
    outString += 'modifiers: %s\n' % test_name.replace(',', ', ')
    outString += 'frameworks: %s\nminBunnies: %s\nmaxBunnies: %s\nstepSize: %s\nrepetitions: %s\nresolution: %sx%s\n\n' % (', '.join(frameworks), minBunnies, maxBunnies, stepSize, repetitions, width, height)
    
    logs = {}
    
    for framework in frameworks:
        f = open(framework + '/bin/log/' + test_name + '.log', 'r')
        logs[framework] = f.read().splitlines()
        f.close()
    
    outString += 'bunnies\t%s\n' % '\t'.join(frameworks)
    
    for i in range(len(logs[frameworks[0]])):
        logline = logs[frameworks[0]][i].split('\t')[0]
        for framework in frameworks:
            logline += '\t' + logs[framework][i].split('\t')[1]
        outString += logline + '\n'
  
    out = open("results/" + name_prefix + "_report.log", "w")
    out.write(outString)
    out.close()
    
def find_frameworks():
    # Returns a list with all names of available frameworks by searching for
    # subdirectories containing a 'bin' folder containing App.jar/App.exe.
    import os
    frameworks = []    
    for directory in next(os.walk('.'))[1]:
        if 'bin' in next(os.walk(directory))[1]:
            for fname in os.listdir(directory+'/bin'):
                if fname == 'App.exe' or fname == 'App.jar':
                    frameworks.append(directory)
                    break
    return frameworks
        