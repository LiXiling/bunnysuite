package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.BunnymarkUtils;
import com.github.lixiling.bunnysuite.bunny.AbstractBunny;

public final class TintedDecorator extends AbstractTestDecorator {

	public TintedDecorator(IBunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(AbstractBunny bunny) {
		baseTest.update(bunny);
	}

	@Override
	public void setInitialValues(AbstractBunny bunny) {
		bunny.setColor(BunnymarkUtils.randomColor());
		baseTest.update(bunny);
	}

}
