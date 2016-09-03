package com.github.lixiling.bunnysuite;

import java.util.ArrayList;
import java.util.Random;
import java.util.Vector;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.github.lixiling.bunnysuite.bunny.AbstractBunny;
import com.github.lixiling.bunnysuite.bunny.Bunny;
import com.github.lixiling.bunnysuite.bunny.BunnyFlavour;
import com.github.lixiling.bunnysuite.bunny.Circle;
import com.github.lixiling.bunnysuite.bunny.Line;
import com.github.lixiling.bunnysuite.bunny.Point;
import com.github.lixiling.bunnysuite.bunny.Rectangle;
import com.github.lixiling.bunnysuite.bunny.Text;
import com.github.lixiling.bunnysuite.bunny.Triangle;

/**
 * "Static" utility class that handles various resources and provides static
 * utility methods.
 * 
 * @author Victor Schuemmer
 */
public class BunnymarkUtils {

	// Private constructor prevents instantiation, as this class only provides
	// static methods.
	private BunnymarkUtils() {
	}

	private static Vector<Runnable> initializers = new Vector<Runnable>();

	/**
	 * Adds an initializer which will be called before the first bunny is
	 * created. Used to add bunny textures.
	 * 
	 * @param initializer
	 */
	public static void addInitializer(Runnable initializer) {
		initializers.add(initializer);
	}

	private static boolean initialized = false;

	private static void initialize() {
		if (initialized)
			return;

		for (Runnable i : initializers) {
			i.run();
		}
		if (bunnyTextures.isEmpty())
			addBunnyTexture(new Texture("wabbit_0.png"));
		if (bunnyFlavours.isEmpty())
			addBunnyFlavour(BunnyFlavour.BUNNY);
		font = new BitmapFont();
		initialized = true;
	}

	private static ArrayList<Texture> bunnyTextures = new ArrayList<Texture>();

	/**
	 * Adds a new bunny texture that will randomly be used for bunnies of type
	 * BUNNY.
	 * 
	 * @param bunnyTexture
	 */
	public static void addBunnyTexture(Texture bunnyTexture) {
		if (!bunnyTextures.contains(bunnyTexture))
			bunnyTextures.add(bunnyTexture);
	}

	/**
	 * @param i
	 * @return A bunny texture. For different values of i the same texture may
	 *         be returned. Using the same i twice will always result in the
	 *         same texture.
	 */
	public static Texture getBunnyTexture(int i) {
		if (!initialized)
			initialize();
		if (i < 0)
			i *= -1;
		return bunnyTextures.get(i % bunnyTextures.size());
	}

	/**
	 * Convenience method to get a random bunny texture.
	 * 
	 * @return a random bunny texture
	 */
	public static Texture getRandomBunnyTexture() {
		if (!initialized)
			initialize();
		return bunnyTextures.get(random.nextInt(bunnyTextures.size()));
	}

	private static float maxTextureHeight = 37.0f;

	/**
	 * @return the height in pixels large textures should be scaled down to.
	 */
	public static float getMaxTextureHeight() {
		return maxTextureHeight;
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
		return random.nextFloat() * Gdx.graphics.getWidth();
	}

	/**
	 * @return a random float between 0 and the screen height.
	 */
	public static float getRandomY() {
		return random.nextFloat() * Gdx.graphics.getHeight();
	}

	private static boolean enableDrawing = true;

	/**
	 * Enables or disables if bunnies should be drawn. Note that this does not
	 * enforce drawing to be disabled.
	 * 
	 * @param enable
	 */
	public static void enableDrawing(boolean enable) {
		enableDrawing = enable;
	}

	/**
	 * @return true iff bunnies should be drawn
	 */
	public static boolean drawingEnabled() {
		return enableDrawing;
	}

	private static Vector<BunnyFlavour> bunnyFlavours = new Vector<BunnyFlavour>();
	private static BitmapFont font;

	/**
	 * Adds a new type of Bunny to randomly use in tests.
	 * 
	 * @param bunnyFlavour
	 */
	public static void addBunnyFlavour(BunnyFlavour bunnyFlavour) {
		if (!bunnyFlavours.contains(bunnyFlavour))
			bunnyFlavours.add(bunnyFlavour);
	}

	public static Color randomColor() {
		return new Color(nextRandomFloat(), nextRandomFloat(), nextRandomFloat(), 1);
	}
	public static AbstractBunny createBunny() {
		if (!initialized)
			initialize();
		switch (bunnyFlavours.get(nextRandomInt(bunnyFlavours.size()))) {
		case RECTANGLE:
			return new Rectangle(26, 37, randomColor());
		case CIRCLE:
			return new Circle(20, randomColor());
		case TEXT:
			return new Text("Hello World!", font, randomColor());
		case LINE:
			return new Line(26, 37, randomColor());
		case POINT:
			return new Point(randomColor());
		case TRIANGLE:
			return new Triangle(0, 37, 26, 37, 13, 0, randomColor());
		case BUNNY:
			return new Bunny(getRandomBunnyTexture());
		default:
			return null;
		}
	}
}
