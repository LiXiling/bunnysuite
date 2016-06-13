package com.github.lixiling.bunnysuite.testStrategy;

import java.util.Iterator;
import java.util.Random;
import java.util.Vector;

import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.github.lixiling.bunnysuite.Bunny;

/**
 * @author Victor Schuemmer
 */
public abstract class Test {

	protected static Random random = new Random();
	
	protected static int screenWidth = 640;
	protected static int screenHeight = 480;
	
	public static void setScreenDimensions(int screenWidth, int screenHeight) {
		Test.screenWidth = screenWidth;
		Test.screenHeight = screenHeight;
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

	private Texture[] bunnyTextures;

	/**
	 * @param i
	 * @return A bunny texture. For different values of i the same texture may
	 *         be returned. Using the same i twice will always result in the
	 *         same texture.
	 */
	public Texture getBunnyTexture(int i) {
		if (i < 0)
			i *= -1;
		return bunnyTextures[i % bunnyTextures.length];
	}
	
	public Test() {
		bunnies = new Vector<Bunny>();
	}

	// TODO get rid of init
	public void init(Texture[] bunnyTextures) {
		this.bunnyTextures = bunnyTextures;
	}

	/**
	 * Manipulates the bunnies' properties in some way. The number of bunnies
	 * must not be changed. To draw the changed bunnies call draw().
	 */
	public abstract void update();

	/**
	 * Draws all Bunnies to the screen.
	 * 
	 * @param batch
	 */
	public void draw(SpriteBatch batch) {
		Iterator<Bunny> it = bunnies.iterator();

		while (it.hasNext()) {
			Bunny bunny = it.next();
			Texture t = bunny.getTexture();
			batch.draw(t, bunny.getX(), bunny.getY(), 0, 0, t.getWidth(), t.getHeight(), bunny.getScaleX(),
					bunny.getScaleY(), bunny.getRotation(), 0, 0, t.getWidth(), t.getHeight(), false, false);
		}
	};

	/**
	 * Adds the given amount of bunnies and may set some initial properties for
	 * each new bunny. Must not change existing bunnies (particularly not reduce
	 * their number).
	 * 
	 * @param amount
	 *            the number of bunnies to add.
	 */
	public abstract void addBunnies(int amount);

	/**
	 * @return A String describing the test.
	 */
	public abstract String getTestDescription();
}
