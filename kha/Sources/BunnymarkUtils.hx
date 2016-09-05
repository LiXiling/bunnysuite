
import kha.Image;
import kha.Assets;
import kha.System;

class BunnymarkUtils {
	
	private static var initializers = new Array<Void->Void>();
	
	public static function addInitializer(initializer : Void -> Void) {
		initializers.push(initializer);
	}
	
	private static var initialized = false;
	
	private static function initialize() {
		if (initialized)
			return;
		for (i in initializers) {
			i();
		}
		if (bunnyTextures.length == 0)
			addBunnyTexture(Assets.images.wabbit_0);
		if (bunnyFlavours.length == 0)
			addBunnyFlavour(BUNNY);
		initialized = true;
	}
	
	private static var bunnyTextures = new Array<Image>();
	private static var bunnyFlavours = new Array<Bunny.BunnyFlavour>();
	
	public static function addBunnyTexture(texture : Image) {
		if (! Lambda.has(bunnyTextures, texture)) {
			bunnyTextures.push(texture);
		}
	}
	
	public static function addBunnyFlavour(flavour : Bunny.BunnyFlavour) {
		bunnyFlavours.push(flavour);
	}
	
	public static function getRandomBunnyTexture() {
		return bunnyTextures[Std.random(bunnyTextures.length)];
	}
	private static var drawingEnabled = true;
	
	public static function enableDrawing(enable : Bool) {
		drawingEnabled = enable;
	}
	
	public static function drawingDisabled() {
		return drawingEnabled == false;
	}
	
	private static var maxTextureHeight = 37;
	
	public static function getMaxTextureHeight() {
		return maxTextureHeight;
	}
	
	private static var width = 800;
	private static var height = 600;
	
	public static function setResolution(x : Int, y : Int) {
		width = x;
		height = y;
	}
	
	public static function getWidth() {
		return width;
	}
	
	public static function getHeight() {
		return height;
	}
	
	public static function getRandomX() {
		return Math.random() * width;
	}
	
	public static function getRandomY() {
		return Math.random() * height;
	}
	
	public static function randomColor() {
		return kha.Color.fromBytes(Std.random(256),Std.random(256),Std.random(256));
	}
	
	public static function createBunny() : Bunny.AbstractBunny {
		if (!initialized)
			initialize();
		switch (bunnyFlavours[Std.random(bunnyFlavours.length)]) {
			case BUNNY:
				return new Bunny(getRandomBunnyTexture());
			case TEXT:
				return new Bunny.Text("Hello World!", randomColor());
			case CIRCLE:
				return new Bunny.Circle(13, randomColor());
			case RECTANGLE:
				return new Bunny.Rectangle(26, 37, randomColor());
			case POINT:
				return new Bunny.Point(randomColor());
			case TRIANGLE:
				return new Bunny.Triangle(0, 37, 26, 37, 13, 0, randomColor());
			case LINE:
				return new Bunny.Line(26, 37, randomColor());
		}
	}
}