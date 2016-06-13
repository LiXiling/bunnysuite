package com.github.lixiling.bunnysuite;

import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.github.lixiling.bunnysuite.testStrategy.Test;
import com.badlogic.gdx.graphics.Texture;

/**
 * @author Victor Schuemmer
 */
public class Bunnymark extends ApplicationAdapter {

	private Test test;
	private Logger logger;

	private int minBunnies;
	private int maxBunnies;
	private int step;

	private int renderedFramesCount;
	private final int framesPerStep;
	private float[] renderTimes;

	/**
	 * The Bunnymark takes care of setting up and executing the {@link Test}
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
	public Bunnymark(Test test, String testName, int minBunnies, int maxBunnies, int step) {
		this.test = test;
		this.logger = new Logger(testName);
		// this.logger = new Logger(Long.toString(System.currentTimeMillis()));
		// logger.addLine(test.getTestDescription());
		// logger.addLine("");
		this.minBunnies = minBunnies;
		this.maxBunnies = maxBunnies;
		this.step = step;

		renderedFramesCount = 0;
		framesPerStep = 10;
		renderTimes = new float[framesPerStep];
	}

	private SpriteBatch batch;
	private BitmapFont font;
	private Texture[] bunnyTextures;

	@Override
	public void create() {
		batch = new SpriteBatch();
		font = new BitmapFont();
		bunnyTextures = new Texture[] { new Texture("wabbit_alpha0.png"), new Texture("wabbit_alpha1.png"),
				new Texture("wabbit_alpha2.png") };
		test.init(bunnyTextures);
		test.addBunnies(minBunnies);
	}

	@Override
	public void dispose() {
		logger.write();
	}

	@Override
	public void render() {
		Gdx.gl.glClearColor(0.21f, 0.21f, 0.21f, 1);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);

		if (test.numberOfBunnies() > maxBunnies)
			Gdx.app.exit();

		if (renderedFramesCount >= framesPerStep) {
			renderedFramesCount = 0;

			// Per step, 10 frames are rendered and the average render time per
			// frame is logged alongside the number of rendered bunnies.
			float avgRenderTime = 0.0f;
			for (float t : renderTimes)
				avgRenderTime += t;

			avgRenderTime /= framesPerStep;

			logger.addLog(test.numberOfBunnies(), avgRenderTime);

			// add some more bunnies.
			test.addBunnies(step);
		}

		// manipulate each of the bunnies.
		test.update();

		// draw the bunnies.
		batch.begin();
		test.draw(batch);

		renderTimes[renderedFramesCount++] = Gdx.graphics.getDeltaTime();

		font.draw(batch, "FPS=" + Gdx.graphics.getFramesPerSecond() + "               COUNT=" + test.numberOfBunnies(),
				10, 20);
		batch.end();
	}
}
