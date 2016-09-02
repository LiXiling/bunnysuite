package com.github.lixiling.bunnysuite.bunny;

import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer;

/**
 * Primitive colored line. Works with rotation and scale.
 * 
 * @author Victor Schuemmer
 */
public class Rectangle extends AbstractBunny {

	private int width;
	private int height;

	public Rectangle(int width, int height, Color color) {
		this.width = width;
		this.height = height;
		setColor(color);
		teleport(0, 0);
	}

	@Override
	public void teleport(float x, float y) {
		super.teleport(x, y + height);
	}

	public void draw(SpriteBatch batch, ShapeRenderer shapeRenderer) {
		shapeRenderer.rect(getX(), getY(), width / 2, height / 2, width, height, getScaleX(), getScaleY(),
				getRotation(), getColor(), getColor(), getColor(), getColor());
	}
}
