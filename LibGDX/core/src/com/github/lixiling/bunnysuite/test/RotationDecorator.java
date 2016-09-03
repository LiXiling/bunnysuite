package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.BunnymarkUtils;
import com.github.lixiling.bunnysuite.bunny.AbstractBunny;

/**
 * A decorator for {@link BaseTest} that makes bunnies rotate 1 degree every
 * frame.
 * 
 * @author Victor Schuemmer
 */
public final class RotationDecorator extends AbstractTestDecorator {

	public RotationDecorator(IBunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(AbstractBunny bunny) {
		bunny.rotate(1);
		baseTest.update(bunny);
	}

	@Override
	public void setInitialValues(AbstractBunny bunny) {
		bunny.setRotation(BunnymarkUtils.nextRandomFloat() * 360);
		baseTest.setInitialValues(bunny);
	}
}
