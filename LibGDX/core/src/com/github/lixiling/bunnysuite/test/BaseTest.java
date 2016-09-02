package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.BunnymarkUtils;
import com.github.lixiling.bunnysuite.bunny.AbstractBunny;
import com.github.lixiling.bunnysuite.bunny.Bunny;

/**
 * The most basic test, that does indeed nothing with the bunnies. Add
 * decorators to create more interesting tests.
 * 
 * @author Victor Schuemmer
 */
public class BaseTest implements IBunnyTest {

	@Override
	public void update(AbstractBunny bunny) {
	}

	@Override
	public void setInitialValues(AbstractBunny bunny) {
		if (bunny instanceof Bunny)
			((Bunny) bunny).setTexture(BunnymarkUtils.getRandomBunnyTexture());
	}

}
