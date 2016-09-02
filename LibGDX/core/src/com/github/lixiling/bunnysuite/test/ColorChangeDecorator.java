package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.BunnymarkUtils;
import com.github.lixiling.bunnysuite.bunny.AbstractBunny;

public final class ColorChangeDecorator extends AbstractTestDecorator {

	public ColorChangeDecorator(IBunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(AbstractBunny bunny) {
		bunny.setColor(BunnymarkUtils.randomColor());
		baseTest.update(bunny);
	}

	@Override
	public void setInitialValues(AbstractBunny bunny) {
		bunny.setColor(BunnymarkUtils.randomColor());
		baseTest.setInitialValues(bunny);
	}

}
