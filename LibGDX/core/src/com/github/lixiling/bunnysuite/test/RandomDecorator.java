package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.Bunny;
import com.github.lixiling.bunnysuite.Bunnymark;

/**
 * A decorator for {@link BaseTest} that makes the bunnies randomly change their
 * position.
 * 
 * @author Victor Schuemmer
 */
public final class RandomDecorator extends BaseTestDecorator {

	public RandomDecorator(BunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(Bunny bunny) {
		bunny.teleport(Bunnymark.getRandomX(), Bunnymark.getRandomY());
		baseTest.update(bunny);
	}

	@Override
	public String getTestDescription() {
		return "Random position.\n" + baseTest.getTestDescription();
	}

	@Override
	public void setInitialValues(Bunny bunny) {
		bunny.teleport(Bunnymark.getRandomX(), Bunnymark.getRandomY());
		baseTest.setInitialValues(bunny);
	}

}
