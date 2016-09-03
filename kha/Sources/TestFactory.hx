import kha.Assets;

class TestFactory {
	public static function createTest(tests: Array<String>) : IBunnyTest {
		var test : IBunnyTest = new BaseTest();
		
		for (t in tests) {
			switch(t) {
				case "standard":					
				case "animation":
					test = new Decorators.JumpDecorator(test);										
				case "rotation":
					test = new Decorators.RotationDecorator(test);				
				case "random":
					test = new Decorators.RandomDecorator(test);				
				case "teleport":
					test = new Decorators.TeleportDecorator(test);	
				case "scaled":
					test = new Decorators.ScaledDecorator(test);
				case "pulsation":
					test = new Decorators.PulsationDecorator(test);
				case "texturechange":
				 	test = new Decorators.TextureChangeDecorator(test);
				case "multitexture":
					BunnymarkUtils.addInitializer(
						function () {
							BunnymarkUtils.addBunnyTexture(Assets.images.wabbit_0);
							BunnymarkUtils.addBunnyTexture(Assets.images.wabbit_1);
							BunnymarkUtils.addBunnyTexture(Assets.images.wabbit_2);
						});
				case "alpha":
					BunnymarkUtils.addInitializer(
						function () {
							BunnymarkUtils.addBunnyTexture(Assets.images.wabbit_ghost);
						});
				case "hdtexture":
					BunnymarkUtils.addInitializer(
						function () {
							BunnymarkUtils.addBunnyTexture(Assets.images.wabbit_hd);
						});
				case "thin":
					BunnymarkUtils.addInitializer(
						function () {
							BunnymarkUtils.addBunnyTexture(Assets.images.wabbit_y);
						});
				case "no_output":
					BunnymarkUtils.enableDrawing(false);
				case "rectangles":
					BunnymarkUtils.addBunnyFlavour(RECTANGLE);
				case "circles":
					BunnymarkUtils.addBunnyFlavour(CIRCLE);
				case "triangles":
					BunnymarkUtils.addBunnyFlavour(TRIANGLE);	
				case "texts":
					BunnymarkUtils.addBunnyFlavour(TEXT);	
				case "lines":	
					BunnymarkUtils.addBunnyFlavour(LINE);	
				case "particles":	
					BunnymarkUtils.addBunnyFlavour(PARTICLE);
				case "bunnies":
					BunnymarkUtils.addBunnyFlavour(BUNNY);	
				default:
					trace("The requested modifier \'" + t + "\' does not exist.");		
			}
		}
		return test;
	}
}