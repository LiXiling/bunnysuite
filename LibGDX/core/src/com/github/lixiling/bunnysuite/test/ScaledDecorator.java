package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.Bunny;
import com.github.lixiling.bunnysuite.Bunnymark;

/**
 * A decorator for {@link BaseTest} that makes the bunnies change size in a
 * pulsing fashion.
 * 
 * @author Victor Schuemmer
 */
public final class ScaledDecorator extends BaseTestDecorator {

	public ScaledDecorator(BunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(Bunny bunny) {
		bunny.grow();
		if (bunny.getScaleX() >= 5 || bunny.getScaleX() <= 0.2)
			bunny.setGrowth(-1 * bunny.getGrowth());
		baseTest.update(bunny);
	}

	@Override
	public String getTestDescription() {
		return "Random scale and position.\n" + baseTest.getTestDescription();
	}

	@Override
	public void setInitialValues(Bunny bunny) {
		bunny.setScale(Bunnymark.nextRandomFloat() * 5);
		bunny.teleport(Bunnymark.getRandomX(), Bunnymark.getRandomY());
		baseTest.setInitialValues(bunny);
	}

}
