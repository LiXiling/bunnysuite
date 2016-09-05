package;

import kha.Assets;
import kha.Color;
import kha.Image;
import kha.Framebuffer;
import kha.Scaler;
import kha.Scheduler;
import kha.System;

class Bunnymark {
	
	private static var bgColor = Color.fromValue(0x000000);
	private var backbuffer: Image;
	
	private var initialized = false;
	
	private var bunnies : Array<Bunny.AbstractBunny>;
	
	private var test : IBunnyTest;
	private var minBunnies : Int;
	private var maxBunnies : Int;
	private var step : Int;
	
	private var renderedFramesCount : Int;
	private var framesPerStep : Int;
	private var renderTimes : Array<Float>;
	
	private var logger : Logger;
	
	private var currentTime : Float;
	private var lastTime : Float;
	
	public function new(test : IBunnyTest, testName : String, minBunnies : Int, maxBunnies : Int, step : Int, framesPerStep : Int) {
		//Register Render & Update in GameLoop
		System.notifyOnRender(render);
		Scheduler.addTimeTask(update, 0, 1 / 60);

		if (step > 0) {
			logger = new Logger(testName);
		}
		
		this.test = test;
		this.minBunnies = minBunnies;
		this.maxBunnies = maxBunnies;
		this.step = step;
		
		renderedFramesCount = 0;
		this.framesPerStep = framesPerStep;
		renderTimes = new Array<Float>();
		
		currentTime = 0;
		lastTime = 0;
		
		bunnies = new Array<Bunny.AbstractBunny>();
			
		// Load Assets with Callback
		Assets.loadEverything(loadingFinished);	
	}

	function loadingFinished() : Void {
		initialized = true;
		//create a rendering Buffer
		backbuffer = Image.createRenderTarget(BunnymarkUtils.getWidth(), BunnymarkUtils.getHeight());
		backbuffer.g2.font = Assets.fonts.arimo;	
		
		// add initial bunnies	
		addBunnies(minBunnies);
	}

	function update(): Void {
		//Wait for AssetLoading
		if (!initialized) {
      		return;
    	}
		
		for (bunny in bunnies) {
			test.update(bunny);
		}
	}

	function render(framebuffer: Framebuffer): Void {
		//Wait for AssetLoading
		if (!initialized) {
      		return;
    	}	

		if (bunnies.length > maxBunnies) {
			if (logger != null) logger.write();
			System.requestShutdown();
		}

		if (renderedFramesCount >= framesPerStep) {
			renderedFramesCount = 0;

			// Per step, 10 frames are rendered and the average render time per
			// frame is logged alongside the number of rendered bunnies.
			var avgRenderTime = 0.0;
			for (t in renderTimes)
				avgRenderTime += t;		
			avgRenderTime /= framesPerStep;

			if (logger != null) logger.addLog(bunnies.length, avgRenderTime);

			// add some more bunnies.
			addBunnies(step);
		}
		
		if (!BunnymarkUtils.drawingDisabled()) {	
			var g = backbuffer.g2;
			
			// clear our backbuffer using graphics2
			g.begin(bgColor);
			for (bunny in bunnies) {
				bunny.render(g);
			}
			g.end();
		}
		
		currentTime = Scheduler.time();
		renderTimes[renderedFramesCount++] = currentTime - lastTime;
		lastTime = currentTime;
		
		if (!BunnymarkUtils.drawingDisabled()) {	
			// draw our backbuffer onto the active framebuffer
			framebuffer.g2.begin();
			Scaler.scale(backbuffer, framebuffer, System.screenRotation);
			framebuffer.g2.end();
		}	
	}
	
	private function addBunnies(amount : Int) {
		for (i in 0...amount) {
			var bunny = BunnymarkUtils.createBunny();
			test.setInitialValues(bunny);
			bunnies.push(bunny);
		}
	}
}
