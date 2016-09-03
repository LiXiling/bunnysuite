package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.BunnymarkUtils;
import com.github.lixiling.bunnysuite.bunny.AbstractBunny;

/**
 * A decorator for {@link BaseTest} that makes bunnies scaled in x and y
 * direction, each by a random stretch factor betwenn 0.2 and 5.
 * 
 * @author Victor Schuemmer
 */
public final class ScaledDecorator extends AbstractTestDecorator {

	public ScaledDecorator(IBunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(AbstractBunny bunny) {
		baseTest.update(bunny);
	}

	@Override
	public void setInitialValues(AbstractBunny bunny) {
		bunny.setScale(BunnymarkUtils.nextRandomFloat() * 4.8f + 0.2f, BunnymarkUtils.nextRandomFloat() * 4.8f + 0.2f);
		baseTest.setInitialValues(bunny);
	}
}
