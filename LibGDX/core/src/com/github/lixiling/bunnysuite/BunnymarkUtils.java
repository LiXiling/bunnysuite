package com.github.lixiling.bunnysuite;

import java.util.ArrayList;
import java.util.Random;

import com.badlogic.gdx.graphics.Texture;

public class BunnymarkUtils {

	private BunnymarkUtils() {
	}

	private static ArrayList<Texture> bunnyTextures = new ArrayList<Texture>();

	public static void addBunnyTexture(Texture bunnyTexture) {
		if (!BunnymarkUtils.bunnyTextures.contains(bunnyTexture))
			BunnymarkUtils.bunnyTextures.add(bunnyTexture);
	}
	
	public static boolean hasTexture() {
		return BunnymarkUtils.bunnyTextures.size() != 0;
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
		return bunnyTextures.get(i % bunnyTextures.size());
	}

	/**
	 * Convenience method to get a random bunny texture.
	 * 
	 * @return a random bunny texture
	 */
	public static Texture getRandomBunnyTexture() {
		return bunnyTextures.get(random.nextInt(bunnyTextures.size()));
	}

	private static float maxTextureHeight = 37.0f;
	
	public static float getMaxTextureHeight() {
		return maxTextureHeight;
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
		BunnymarkUtils.screenWidth = screenWidth;
		BunnymarkUtils.screenHeight = screenHeight;
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
}
