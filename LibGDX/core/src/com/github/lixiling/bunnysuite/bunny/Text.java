package com.github.lixiling.bunnysuite.bunny;

import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer;

/**
 * Colored bitmap text. Wont be affected by rotation and scale, as this is
 * unprovided for {@link BitmapFont}.
 * 
 * @author Victor Schuemmer
 */
public class Text extends AbstractBunny {

	private String text;

	public void setText(String text) {
		this.text = text;
	}

	private BitmapFont font;

	public Text(String text, BitmapFont font, Color color) {
		this.text = text;
		this.font = font;
		setColor(color);
		teleport(0, 0);
	}

	@Override
	public void teleport(float x, float y) {
		super.teleport(x, y + font.getCapHeight());
	}

	public void draw(SpriteBatch batch, ShapeRenderer shapeRenderer) {
		// The following line would scale the text. This is disabled to prevent
		// unfair disadvantage against other frameworks, where this could not be
		// implemented.
		// font.getData().setScale(getScaleX(), getScaleY());
		font.setColor(getColor());
		font.draw(batch, text, getX(), getY());
	}

}
