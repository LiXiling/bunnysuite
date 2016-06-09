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

Supported Tests at the moment:
* standard
* random
* scaled
* multitexture
* texturechange
* animation

Further information can be found in the wiki.

Based upon:
https://github.com/dmitryhryppa/Frameworks_test
