# bunnysuite

An automatic Test Framework for 2D Engines
------------------
To start a benchmark with our delivered frameworks, we recommend to use our GUI.
To start the GUI execute `bunnysuite_gui.py`. We tested the GUI with Python2, Python3 should be supported.

Supported Frameworks at the moment:
* sdl
* xna
* libGDX
* kha

For supported Tests and their definition, please refer to the wiki

The result of each test can be found at `<framework>\bin\log\<testname>.log`

The chart visualization can be found at `results\<testname>.png`


For new frameworks, compile an executable for the desired framework. The path should be `<framework>\bin\App.exe` (`<framework>\bin\App.jar` for Java-based frameworks). If this structure is followed, the framework will automatically be detected and offered by the GUI.


To conduct tests manually without GUI, either use the CLI of the `App.exe` files:
`App.exe animation,rotation,pulsation 0 10000 500` (for more options, refer to the wiki)

Or use the `run_test.py` Python file:
Add your frameworks to the list of frameworks:

```python
frameworks = ['kha','sdl','xna_monogame','LibGDX']
```
To costumize tests, edit or add lines like:

```python
# run some tests for all frameworks
# run_test(frameworks, <test_names>, <min_val>, <max_val>, <step_width>)
run_test(frameworks, "standard", 1000, 20000, 1000)
run_test(frameworks, "random", 1000, 20000, 1000)
run_test(frameworks, "scaled,animation,triangles", 1000, 20000, 1000)
```
Further information can be found in the wiki.

Based upon:
https://github.com/dmitryhryppa/Frameworks_test
