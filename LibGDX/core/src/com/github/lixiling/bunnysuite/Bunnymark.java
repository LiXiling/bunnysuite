package com.github.lixiling.bunnysuite;

import java.util.Iterator;
import java.util.Vector;

import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.github.lixiling.bunnysuite.test.BunnyTest;

/**
 * The main application.
 * 
 * @author Victor Schuemmer
 */
public class Bunnymark extends ApplicationAdapter {

	private Vector<Bunny> bunnies;

	private BunnyTest test;
	private Logger logger;

	private int minBunnies;
	private int maxBunnies;
	private int step;

	private int renderedFramesCount;
	private final int framesPerStep;
	private float[] renderTimes;

	/**
	 * The Bunnymark takes care of setting up and executing the
	 * {@link AbstractTest}
	 * 
	 * @param test
	 *            the test to be executed
	 * @param minBunnies
	 *            the initial count of {@link Bunny bunnies}
	 * @param maxBunnies
	 *            the count of bunnies at which the bunnymark terminates
	 * @param step
	 *            the amount of bunnies to be added after each performance
	 *            measurement
	 */
	public Bunnymark(BunnyTest test, String testName, int minBunnies, int maxBunnies, int step) {
		this.test = test;
		if (step > 0) this.logger = new Logger(testName);
		// this.logger = new Logger(Long.toString(System.currentTimeMillis()));
		// logger.addLine(test.getTestDescription());
		// logger.addLine("");
		this.minBunnies = minBunnies;
		this.maxBunnies = maxBunnies;
		this.step = step;

		renderedFramesCount = 0;
		framesPerStep = 10;
		renderTimes = new float[framesPerStep];

		bunnies = new Vector<Bunny>();
	}

	private SpriteBatch batch;
	private BitmapFont font;

	@Override
	public void create() {
		batch = new SpriteBatch();
		font = new BitmapFont();
		test.initialize();
		addBunnies(minBunnies);
		
	}

	@Override
	public void dispose() {
		if (logger != null) logger.write();
	}

	@Override
	public void render() {
		Gdx.gl.glClearColor(0.21f, 0.21f, 0.21f, 1);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);

		if (bunnies.size() > maxBunnies)
			Gdx.app.exit();

		if (renderedFramesCount >= framesPerStep) {
			renderedFramesCount = 0;

			// Per step, 10 frames are rendered and the average render time per
			// frame is logged alongside the number of rendered bunnies.
			float avgRenderTime = 0.0f;
			for (float t : renderTimes)
				avgRenderTime += t;		
			avgRenderTime /= framesPerStep;

			if (logger != null) logger.addLog(bunnies.size(), avgRenderTime);

			// add some more bunnies.
			addBunnies(step);
		}

		// manipulate each of the bunnies.
		updateBunnies();

		// Draw the bunnies.
		batch.begin();
		draw();

		// Save the render time for the current frame.
		renderTimes[renderedFramesCount++] = Gdx.graphics.getDeltaTime();

		font.draw(batch, "FPS=" + Gdx.graphics.getFramesPerSecond() + "               COUNT=" + bunnies.size(), 10,
				20);
		batch.end();
	}

	private void updateBunnies() {
		Iterator<Bunny> it = bunnies.iterator();
		while (it.hasNext()) {
			Bunny bunny = it.next();
			test.update(bunny);
		}
	}

	private void addBunnies(int amount) {
		for (int i = 0; i < amount; i++) {
			Bunny bunny = new Bunny(BunnymarkUtils.getBunnyTexture(0));
			test.setInitialValues(bunny);
			bunnies.add(bunny);
		}
	}

	private void draw() {
		Iterator<Bunny> it = bunnies.iterator();

		while (it.hasNext()) {
			Bunny bunny = it.next();
			Texture t = bunny.getTexture();
			
			// scale down hd textures to be smaller than maxTextureHeight
			float height = Math.min(BunnymarkUtils.getMaxTextureHeight(), t.getHeight());
			float width = height/t.getHeight() * t.getWidth();
			
			batch.draw(t, bunny.getX(), bunny.getY(), 0, 0, width, height, bunny.getScaleX(),
					bunny.getScaleY(), bunny.getRotation(), 0, 0, t.getWidth(), t.getHeight(), false, false);
		}
	}
	
}
