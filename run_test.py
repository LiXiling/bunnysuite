from testmanager import *

frameworks = ['SDL','xna_monogame','LibGDX','kha']
test_name = 'bunnies'
min_val = 1000
max_val = 20000
step = 1000
repetitions = 2
run_test(frameworks, test_name, min_val, max_val, step, repetitions)