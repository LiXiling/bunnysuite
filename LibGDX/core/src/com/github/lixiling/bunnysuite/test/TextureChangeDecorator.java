package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.Bunny;
import com.github.lixiling.bunnysuite.Bunnymark;

/**
 * A decorator for {@link BaseTest} that makes the bunnies randomly change their
 * texture.
 * 
 * @author Victor Schuemmer
 */
public final class TextureChangeDecorator extends BaseTestDecorator {

	public TextureChangeDecorator(BunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(Bunny bunny) {
		bunny.setTexture(Bunnymark.getRandomBunnyTexture());
		baseTest.update(bunny);
	}

	@Override
	public String getTestDescription() {
		return "Bunnies get a new random texture every frame.\n" + baseTest.getTestDescription();
	}

	@Override
	public void setInitialValues(Bunny bunny) {
		bunny.setTexture(Bunnymark.getRandomBunnyTexture());
		bunny.teleport(Bunnymark.getRandomX(), Bunnymark.getRandomY());
		baseTest.setInitialValues(bunny);
	}

}
