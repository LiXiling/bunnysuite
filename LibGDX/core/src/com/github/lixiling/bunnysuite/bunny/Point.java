package com.github.lixiling.bunnysuite.bunny;

import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer;

/**
 * Primitive colored dot. Rotation and scale wont do anything.
 * 
 * @author Victor Schuemmer
 */
public class Point extends AbstractBunny {

	private Color color;
	
	public Point(Color color) {
		this.color = color;
	}

	@Override
	public void draw(SpriteBatch batch, ShapeRenderer shapeRenderer) {
		shapeRenderer.setColor(color);
		shapeRenderer.point(getX(), getY(), 0);
	}

}
