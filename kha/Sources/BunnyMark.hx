package;

import kha.Assets;
import kha.Color;
import kha.Image;
import kha.Framebuffer;
import kha.Scaler;
import kha.Scheduler;
import kha.System;

class BunnyMark {
	
	private static var bgColor = Color.fromValue(0x000000);
	private var backbuffer: Image;
	private static inline var screenWidth = 1024;
	private static inline var screenHeight = 768;
	private var bunnyTexture : Image;
	
	private var initialized = false;
	
	private var bunny : Bunny;
	
	
	public function new() {
		//Register Render & Update in GameLoop
		System.notifyOnRender(render);
		Scheduler.addTimeTask(update, 0, 1 / 60);
		
		//Load Assets with Callback
		Assets.loadEverything(loadingFinished);
		
	}

	function loadingFinished() : Void {
		initialized = true;
		
		//create a rendering Buffer
		backbuffer = Image.createRenderTarget(screenWidth, screenHeight);
		
		bunnyTexture = Assets.images.wabbit_alpha0;
		
		
	}

	function update(): Void {
		//Wait for AssetLoading
		if (!initialized) {
      		return;
    	}
					
		bunny = new Bunny(Std.int(screenWidth / 2) - Std.int(bunnyTexture.width / 2), 
      						Std.int(screenHeight / 2) - Std.int(bunnyTexture.height / 2), 
      						bunnyTexture);
	}

	function render(framebuffer: Framebuffer): Void {
		//Wait for AssetLoading
		if (!initialized) {
      		return;
    	}	
		var g = backbuffer.g2;
		
		// clear our backbuffer using graphics2
    	g.begin(bgColor);
		bunny.render(g);
    	g.end();
		
		
		// draw our backbuffer onto the active framebuffer
    	framebuffer.g2.begin();
    	Scaler.scale(backbuffer, framebuffer, System.screenRotation);
    	framebuffer.g2.end();	
	}
}
