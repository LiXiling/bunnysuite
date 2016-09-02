package com.github.lixiling.bunnysuite.bunny;

import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer;

/**
 * Primitive colored circle. Works with scale. (Rotation, obviously, wont affect
 * the circle.)
 * 
 * @author Victor Schuemmer
 */
public class Circle extends AbstractBunny {

	private int radius;

	public Circle(int radius, Color color) {
		this.radius = radius;
		setColor(color);
		teleport(0, 0);
	}

	@Override
	public void teleport(float x, float y) {
		super.teleport(x + radius, y + radius);
	}

	public void draw(SpriteBatch batch, ShapeRenderer shapeRenderer) {
		shapeRenderer.setColor(getColor());
		shapeRenderer.circle(getX(), getY(), radius * getScaleX());
		shapeRenderer.setColor(Color.WHITE);
	}

}
