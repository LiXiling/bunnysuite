package;
import Bunnymark;
import kha.System;

class Main {
	public static function main() {
		
		//var args = Sys.args();
		var args = ["triangles,animation,rotation","5","10000","0", "800", "600", "1"];

		var width = args[4] != null ? Std.parseInt(args[4]) : 800;
		var height = args[5] != null ? Std.parseInt(args[5]) : 600;
		var framesPerStep = args[6] != null ? Std.parseInt(args[6]) : 10;
		
		//System.changeResolution does not seem to do anything..
		BunnymarkUtils.setResolution(width, height);
		// {title: "Bunnymark", width: width, height: height},
		System.init("Bunnymark",width,height, function () {
			new Bunnymark(TestFactory.createTest(args[0].split(",")),
					args[0],
					Std.parseInt(args[1]),
					Std.parseInt(args[2]),
					Std.parseInt(args[3]),
					framesPerStep);
		});
	}
}
