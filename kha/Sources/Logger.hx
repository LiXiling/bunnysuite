import sys.io.File;
import sys.FileSystem;

class Logger {
	private var filename : String;
	private var lines : Array<String>;
	
	public function new(filename : String) {
		this.filename = filename;
		this.lines = new Array<String>();
	}
	
	public function addLog(n : Int, drawTime : Float) {
		lines.push(n + "\t" + drawTime);
	}
	
	
	public function write() {
		var path = sys.FileSystem.absolutePath("./log");
		if (!sys.FileSystem.exists(path)) sys.FileSystem.createDirectory(path);
		var out = sys.io.File.write(path + "/" + filename + ".log", false);	
		for (line in lines) {
			out.writeString(line + "\n");
		}		
		out.close();
	}
	
}