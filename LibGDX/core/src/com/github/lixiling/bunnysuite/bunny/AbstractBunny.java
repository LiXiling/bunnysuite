package com.github.lixiling.bunnysuite.bunny;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer;

/**
 * The thing used by the tests while rendering. Extend and implement draw() to
 * create concrete bunnies or other things that jump.
 * 
 * @author Victor Schuemmer
 */
public abstract class AbstractBunny {

	public AbstractBunny() {
		scaleX = 1;
		scaleY = 1;
		growth = 0.1f;
		color = Color.WHITE;
	}

	private float x;

	/**
	 * @return the current x position
	 */
	public float getX() {
		return x;
	}

	private float y;

	/**
	 * @return the current y position
	 */
	public float getY() {
		return y;
	}

	public void teleport(float x, float y) {
		if (x < 0 || y < 0 || x > Gdx.graphics.getWidth() || y > Gdx.graphics.getHeight())
			return;
		this.x = x;
		this.y = Gdx.graphics.getHeight() - y;
	}

	private float speedX;

	/**
	 * @param speedX
	 *            the speed in x direction
	 */
	public void setSpeedX(float speedX) {
		this.speedX = speedX;
	}

	private float speedY;

	/**
	 * @param speedY
	 *            the speed in y direction
	 */
	public void setSpeedY(float speedY) {
		this.speedY = speedY;
	}

	private float rotation;

	/**
	 * @return the current rotation of the bunny
	 */
	public float getRotation() {
		return rotation;
	}

	/**
	 * Rotates the bunny by given degrees, relative to its original rotation.
	 */
	public void setRotation(float rotation) {
		this.rotation = rotation % 360;
	}

	/**
	 * Rotates the bunny by given degrees, relative to its current rotation.
	 */
	public void rotate(float rotation) {
		this.rotation += rotation;
		this.rotation %= 360;
	}

	private float scaleX;

	/**
	 * @return the scale factor in x direction
	 */
	public float getScaleX() {
		return scaleX;
	}

	private float scaleY;

	/**
	 * @return the scale factor in y direction
	 */
	public float getScaleY() {
		return scaleY;
	}

	/**
	 * Convenience method to scale the bunny proportionally. Negative values are
	 * not allowed. Note that the scale is relative to the textures original
	 * size, not the previous scale.
	 * 
	 * @param scale
	 */
	public void setScale(float scale) {
		setScale(scale, scale);
	}

	/**
	 * Scales the bunny by the given scale values. Negative values are not
	 * allowed. Note that the scale is relative to the textures original size,
	 * not the previous scale.
	 * 
	 * @param scaleX
	 * @param scaleY
	 */
	public void setScale(float scaleX, float scaleY) {
		this.scaleX = scaleX;
		this.scaleY = scaleY;
	}

	private float growth;

	/**
	 * @return the current growth value for the bunny
	 */
	public float getGrowth() {
		return growth;
	}

	/**
	 * Sets the growth value. Use grow() to actually let the bunny grow.
	 * 
	 * @param growth
	 */
	public void setGrowth(float growth) {
		this.growth = growth;
	}

	/**
	 * Makes the bunny grow proportionally by the growth value. Use setGrowth()
	 * to set the growth.
	 */
	public void grow() {
		setScale(scaleX + growth, scaleY + growth);
	}

	/**
	 * Makes the bunny move on a parabola. Needs to be called every frame for a
	 * fluid motion. This will (obviously) change its position, but will also
	 * influence x and y speed.
	 * 
	 * @param gravity
	 *            the value the y speed is reduced by each call. Higher values
	 *            make for a "flatter" jumping curve.
	 * @param minX
	 *            the minimal x position of the bunny
	 * @param minY
	 *            the minimal y position of the bunny (aka the line where it
	 *            jumps off)
	 * @param maxX
	 *            the maximal x position of the bunny
	 * @param maxY
	 *            the maximum height the bunny may jump
	 */
	public void jump(float gravity, float minX, float minY, float maxX, float maxY) {
		this.x += this.speedX;
		this.y += this.speedY;
		this.speedY -= gravity;

		if (this.x < minX) {
			this.speedX *= -1;
			this.x = minX;
		}
		if (this.x > maxX) {
			this.speedX *= -1;
			this.x = maxX;
		}
		if (this.y < minY) {
			this.speedY *= -0.8;
			if (Math.random() > 0.5)
				this.speedY += 3 + Math.random() * 4;
			this.y = minY;
		} else if (this.y > maxY) {
			this.speedY = 0;
			this.y = maxY;
		}
	}
	
	private Color color;
	
	public void setColor(Color color) {
		this.color = color;
	}
	
	public Color getColor() {
		return color;
	}
	
	/**
	 * Draws the bunny on the batch or shape renderer. Assumes that begin() was
	 * called on both before calling.
	 * 
	 * @param batch
	 * @param shapeRenderer
	 */
	public abstract void draw(SpriteBatch batch, ShapeRenderer shapeRenderer);
}
