package com.github.lixiling.bunnysuite;

import java.util.Iterator;
import java.util.Random;
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

	private static Texture[] bunnyTextures;

	/**
	 * Sets the textures the tests pull from. Should be more than 1, otherwise
	 * some tests might get boring.
	 * 
	 * @param bunnyTextures
	 */
	public static void setBunnyTextures(Texture[] bunnyTextures) {
		Bunnymark.bunnyTextures = bunnyTextures;
	}

	/**
	 * @param i
	 * @return A bunny texture. For different values of i the same texture may
	 *         be returned. Using the same i twice will always result in the
	 *         same texture.
	 */
	public static Texture getBunnyTexture(int i) {
		if (i < 0)
			i *= -1;
		return bunnyTextures[i % bunnyTextures.length];
	}

	/**
	 * Convenience method to get a random bunny texture.
	 * 
	 * @return a random bunny texture
	 */
	public static Texture getRandomBunnyTexture() {
		return bunnyTextures[random.nextInt(bunnyTextures.length)];
	}

	private static int screenWidth = 640;
	private static int screenHeight = 480;

	/**
	 * Set the screen dimension the bunnymark assumes.
	 * 
	 * @param screenWidth
	 * @param screenHeight
	 */
	public static void setScreenDimensions(int screenWidth, int screenHeight) {
		Bunnymark.screenWidth = screenWidth;
		Bunnymark.screenHeight = screenHeight;
	}

	/**
	 * @return the screen width the bunnymark assumes, which may not be the
	 *         actual window width.
	 */
	public static int getScreenWidth() {
		return screenWidth;
	}

	/**
	 * @return the screen height the bunnymark assumes, which may not be the
	 *         actual window height.
	 */
	public static int getScreenHeight() {
		return screenHeight;
	}

	private static Random random = new Random();

	/**
	 * Convenience method to get a random integer without creating a new Random
	 * object.
	 * 
	 * @param max
	 *            the random integer is in the interval [0,max)
	 * @return a random integer
	 */
	public static int nextRandomInt(int max) {
		return random.nextInt(max);
	}

	/**
	 * Convenience method to get a random float without creating a new Random
	 * object.
	 * 
	 * @return a random float
	 */
	public static float nextRandomFloat() {
		return random.nextFloat();
	}

	/**
	 * @return a random float between 0 and the screen width.
	 */
	public static float getRandomX() {
		return random.nextFloat() * screenWidth;
	}

	/**
	 * @return a random float between 0 and the screen height.
	 */
	public static float getRandomY() {
		return random.nextFloat() * screenHeight;
	}

	private Vector<Bunny> bunnies;

	/**
	 * @return a vector containing all current bunnies.
	 */
	public Vector<Bunny> getBunnies() {
		return bunnies;
	}

	/**
	 * @return the current number of bunnies.
	 */
	public int numberOfBunnies() {
		return bunnies.size();
	}

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

		bunnies = new Vector<Bunny>();
	}

	private SpriteBatch batch;
	private BitmapFont font;

	@Override
	public void create() {
		batch = new SpriteBatch();
		font = new BitmapFont();
		bunnyTextures = new Texture[] { new Texture("wabbit_alpha0.png"), new Texture("wabbit_alpha1.png"),
				new Texture("wabbit_alpha2.png") };
		addBunnies(minBunnies);
	}

	@Override
	public void dispose() {
		logger.write();
	}

	@Override
	public void render() {
		Gdx.gl.glClearColor(0.21f, 0.21f, 0.21f, 1);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);

		if (numberOfBunnies() > maxBunnies)
			Gdx.app.exit();

		if (renderedFramesCount >= framesPerStep) {
			renderedFramesCount = 0;

			// Per step, 10 frames are rendered and the average render time per
			// frame is logged alongside the number of rendered bunnies.
			float avgRenderTime = 0.0f;
			for (float t : renderTimes)
				avgRenderTime += t;

			avgRenderTime /= framesPerStep;

			logger.addLog(numberOfBunnies(), avgRenderTime);

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

		font.draw(batch, "FPS=" + Gdx.graphics.getFramesPerSecond() + "               COUNT=" + numberOfBunnies(), 10,
				20);
		batch.end();
	}

	private void updateBunnies() {
		Iterator<Bunny> it = getBunnies().iterator();
		while (it.hasNext()) {
			Bunny bunny = it.next();
			test.update(bunny);
		}
	}

	private void addBunnies(int amount) {
		for (int i = 0; i < amount; i++) {
			Bunny bunny = new Bunny(Bunnymark.getBunnyTexture(0));
			test.setInitialValues(bunny);
			getBunnies().add(bunny);
		}
	}

	private void draw() {
		Iterator<Bunny> it = bunnies.iterator();

		while (it.hasNext()) {
			Bunny bunny = it.next();
			Texture t = bunny.getTexture();
			batch.draw(t, bunny.getX(), bunny.getY(), 0, 0, t.getWidth(), t.getHeight(), bunny.getScaleX(),
					bunny.getScaleY(), bunny.getRotation(), 0, 0, t.getWidth(), t.getHeight(), false, false);
		}
	}
}
