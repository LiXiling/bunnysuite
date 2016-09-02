package com.github.lixiling.bunnysuite.bunny;

import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer;
import com.badlogic.gdx.math.MathUtils;

/**
 * Primitive colored triangle. Works with scale.
 * 
 * @author Victor Schuemmer
 */
public class Triangle extends AbstractBunny {

	private int x1;
	private int y1;
	private int x2;
	private int y2;
	private int x3;
	private int y3;

	public Triangle(int x1, int y1, int x2, int y2, int x3, int y3, Color color) {
		this.x1 = x1;
		this.y1 = -y1;
		this.x2 = x2;
		this.y2 = -y2;
		this.x3 = x3;
		this.y3 = -y3;
		setColor(color);
	}

	@Override
	public void draw(SpriteBatch batch, ShapeRenderer shapeRenderer) {
		float cos = MathUtils.cosDeg(getRotation());
		float sin = MathUtils.sinDeg(getRotation());

		float originX = getScaleX() * (x1 + x2 + x3) / 3.0f;
		float originY = getScaleY() * (y1 + y2 + y3) / 3.0f;

		float x1rotated = (x1 * getScaleX() - originX) * cos - (y1 * getScaleY() - originY) * sin + originX;
		float x2rotated = (x2 * getScaleX() - originX) * cos - (y2 * getScaleY() - originY) * sin + originX;
		float x3rotated = (x3 * getScaleX() - originX) * cos - (y3 * getScaleY() - originY) * sin + originX;
		float y1rotated = (y1 * getScaleY() - originY) * cos + (x1 * getScaleX() - originX) * sin + originY;
		float y2rotated = (y2 * getScaleY() - originY) * cos + (x2 * getScaleX() - originX) * sin + originY;
		float y3rotated = (y3 * getScaleY() - originY) * cos + (x3 * getScaleX() - originX) * sin + originY;

		shapeRenderer.triangle(getX() + x1rotated, getY() + y1rotated,
				getX() + x2rotated, getY() + y2rotated, getX() + x3rotated,
				getY() + y3rotated, getColor(), getColor(), getColor());
	}

}
