from testmanager import *

frameworks = ['SDL','xna_monogame']
test_name = 'multitexture,alpha,thin,animation,texturechange'
min_val = 1000
max_val = 10000
step = 1000
run_test(frameworks, test_name, min_val, max_val, step)