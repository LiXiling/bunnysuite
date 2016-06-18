package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.Bunny;
import com.github.lixiling.bunnysuite.Bunnymark;

/**
 * A decorator for {@link BaseTest} that makes the bunnies jump.
 * 
 * @author Victor Schuemmer
 */
public final class JumpDecorator extends BaseTestDecorator {

	public JumpDecorator(BunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(Bunny bunny) {
		bunny.jump(0.5f, 0, 0, Bunnymark.getScreenWidth(), Bunnymark.getScreenHeight());
		baseTest.update(bunny);
	}

	@Override
	public String getTestDescription() {
		return "Fills the screen with jumping bunnies.\n" + baseTest.getTestDescription();
	}

	@Override
	public void setInitialValues(Bunny bunny) {
		bunny.setSpeedX(Bunnymark.nextRandomFloat() * 5);
		bunny.setSpeedY(Bunnymark.nextRandomFloat() * 5 - 2.5f);
		baseTest.setInitialValues(bunny);
	}

}
