# bunnysuite

An automatic Test Framework for 2D Engines
------------------
To conduct tests:
Compile an executable for the desired frameworks. The path should be `<framework>\bin\App.exe`

In the Python-File `test-manager.py` add these frameworks to the list of frameworks:

```python
frameworks = ['sdl','xna_monogame','LibGDX']
```
To costumize tests, edit or add lines like:

```python
# run some tests for all frameworks
# run_test(frameworks, <test_name>, <min_val>, <max_val>, <step_width>)
run_test(frameworks, "standard", 1000, 20000, 1000)
run_test(frameworks, "random", 1000, 20000, 1000)
run_test(frameworks, "scaled", 1000, 20000, 1000)
```

Supported Frameworks at the moment:
* sdl
* xna
* libGDX
* (kha, koming soon)

Supported Tests at the moment (not all frameworks support all tests yet):
* standard
* random
* scaled
* multitexture
* texturechange
* animation
* rotation
* (alpha, coming soon)

Some of the frameworks (LibGDX, xna, ..) already allow for combined tests:
```python
run_test(frameworks, "scaled,animation,texturechange", 1000, 20000, 1000)
```

The result of each test can be found at `<framework>\bin\log\<testname>.log`

The chart visualization can be found at `results\<testname>.png`

Further information can be found in the wiki.

Based upon:
https://github.com/dmitryhryppa/Frameworks_test
