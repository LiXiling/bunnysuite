package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.Bunny;
import com.github.lixiling.bunnysuite.Bunnymark;

/**
 * A decorator for {@link BaseTest} that makes the bunnies have random initial
 * bunny textures.
 * 
 * @author Victor Schuemmer
 */
public final class MultiTextureDecorator extends BaseTestDecorator {

	public MultiTextureDecorator(BunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(Bunny bunny) {
		baseTest.update(bunny);
	}

	@Override
	public String getTestDescription() {
		return "Random position and fixed random texture.\n" + baseTest.getTestDescription();
	}

	@Override
	public void setInitialValues(Bunny bunny) {
		bunny.setTexture(Bunnymark.getRandomBunnyTexture());
		bunny.teleport(Bunnymark.getRandomX(), Bunnymark.getRandomY());
		baseTest.setInitialValues(bunny);
	}

}
