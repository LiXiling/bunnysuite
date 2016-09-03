package com.github.lixiling.bunnysuite.bunny;

import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer;
import com.github.lixiling.bunnysuite.BunnymarkUtils;

/**
 * A bunny, i.e., a thing with a texture.
 * 
 * @author Victor Schuemmer
 */
public class Bunny extends AbstractBunny {

	private Texture texture;

	private int width;
	private int height;
	
	private float scaledWidth;
	private float scaledHeight;
	
	/**
	 * Sets a new texture for the bunny. May actually show a panda, this is not
	 * checked.
	 * 
	 * @param texture
	 */
	public void setTexture(Texture texture) {
		this.texture = texture;
		this.width = texture.getWidth();
		this.height = texture.getHeight();
		this.scaledHeight = Math.min(BunnymarkUtils.getMaxTextureHeight(), height);
		this.scaledWidth = scaledHeight / height * width;
	}


	/**
	 * Instantly moves the bunny to the given position.
	 * 
	 * @param x
	 * @param y
	 */
	public void teleport(float x, float y) {
		super.teleport(x, y + scaledHeight);
	}

	/**
	 * Creates a bunny with the given texture (that may actually show a giraffe,
	 * this is not checked).
	 * 
	 * @param texture
	 *            the bunny texture
	 */
	public Bunny(Texture texture) {
		setTexture(texture);
		teleport(0,0);
	}


	public void draw(SpriteBatch batch, ShapeRenderer shapeRenderer) {
		// scale down hd textures to be smaller than maxTextureHeight
		batch.setColor(getColor());
		batch.draw(texture, getX(), getY(), scaledWidth/2, scaledHeight/2, scaledWidth, scaledHeight, getScaleX(), getScaleY(), getRotation(), 0, 0,
				width, height, false, false);
		batch.setColor(Color.WHITE);

	}
}
