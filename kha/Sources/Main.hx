package;
import BunnyMark;
import kha.System;

class Main {
	public static function main() {
		System.init("BunnyMark", 1024, 768, function () {
			new BunnyMark();
		});
	}
}
