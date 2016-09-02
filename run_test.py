from testmanager import *

frameworks = ['SDL']
test_name = 'multitexture,alpha,thin,animation,texturechange'
min_val = 2000
max_val = 200000
step = 20
run_test(frameworks, test_name, min_val, max_val, step)