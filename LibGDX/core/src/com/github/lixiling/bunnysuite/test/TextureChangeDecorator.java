package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.BunnymarkUtils;
import com.github.lixiling.bunnysuite.bunny.AbstractBunny;
import com.github.lixiling.bunnysuite.bunny.Bunny;

/**
 * A decorator for {@link BaseTest} that makes the bunnies randomly change their
 * texture.
 * 
 * @author Victor Schuemmer
 */
public final class TextureChangeDecorator extends AbstractTestDecorator {

	public TextureChangeDecorator(IBunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(AbstractBunny bunny) {
		if (bunny instanceof Bunny)
			((Bunny) bunny).setTexture(BunnymarkUtils.getRandomBunnyTexture());
		baseTest.update(bunny);
	}

	@Override
	public void setInitialValues(AbstractBunny bunny) {
		baseTest.setInitialValues(bunny);
	}
}
