var solution = new Solution('BunnySuite Kha');
var project = new Project('BunnySuite Kha');
project.targetOptions = {"flash":{},"android":{}};
project.setDebugDir('build/windows');
project.addSubProject(Solution.createProject('build/windows-build'));
project.addSubProject(Solution.createProject('c:/Users/Maximilian/Downloads/Kode/KodeStudio-win32/resources/app/extensions/kha/Kha'));
project.addSubProject(Solution.createProject('c:/Users/Maximilian/Downloads/Kode/KodeStudio-win32/resources/app/extensions/kha/Kha/Kore'));
solution.addProject(project);
return solution;
