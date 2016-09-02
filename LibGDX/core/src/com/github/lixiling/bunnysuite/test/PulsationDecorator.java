package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.BunnymarkUtils;
import com.github.lixiling.bunnysuite.bunny.AbstractBunny;

/**
 * A decorator for {@link BaseTest} that makes the bunnies change size
 * continuously.
 * 
 * @author Victor Schuemmer
 */
public final class PulsationDecorator extends AbstractTestDecorator {

	public PulsationDecorator(IBunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(AbstractBunny bunny) {
		bunny.grow();
		if (bunny.getScaleX() >= 5 || bunny.getScaleX() <= 0.2)
			bunny.setGrowth(-1 * bunny.getGrowth());
		baseTest.update(bunny);
	}

	@Override
	public void setInitialValues(AbstractBunny bunny) {
		bunny.setScale(BunnymarkUtils.nextRandomFloat() * 4.8f + 0.2f);
		baseTest.setInitialValues(bunny);
	}
}
