package com.github.lixiling.bunnysuite.bunny;

import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer;
import com.badlogic.gdx.math.MathUtils;

/**
 * Primitive colored line. Works with rotation and scale.
 * 
 * @author Victor Schuemmer
 */
public class Line extends AbstractBunny {

	private int deltaX;
	private int deltaY;

	public Line(int deltaX, int deltaY, Color color) {
		this.deltaX = deltaX;
		this.deltaY = deltaY;
		setColor(color);
	}

	@Override
	public void draw(SpriteBatch batch, ShapeRenderer shapeRenderer) {
		float cos = MathUtils.cosDeg(getRotation());
		float sin = MathUtils.sinDeg(getRotation());
		
		float scaledX = getScaleX() * deltaX / 2.0f;
		float scaledY = getScaleY() * deltaY / 2.0f;
		
		float dx = scaledX * cos - scaledY * sin;
		float dy = scaledY * cos + scaledX * sin;
		
		shapeRenderer.line(getX() - dx, getY() - dy, getX() + dx, getY() + dy, getColor(), getColor());
	}

}
