package com.github.lixiling.bunnysuite.test;

import com.badlogic.gdx.Gdx;
import com.github.lixiling.bunnysuite.BunnymarkUtils;
import com.github.lixiling.bunnysuite.bunny.AbstractBunny;

/**
 * A decorator for {@link BaseTest} that makes the bunnies jump.
 * 
 * @author Victor Schuemmer
 */
public final class JumpDecorator extends AbstractTestDecorator {

	public JumpDecorator(IBunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(AbstractBunny bunny) {
		bunny.jump(0.5f, 0, 0, Gdx.graphics.getWidth(), Gdx.graphics.getHeight());
		baseTest.update(bunny);
	}

	@Override
	public void setInitialValues(AbstractBunny bunny) {
		bunny.setSpeedX(BunnymarkUtils.nextRandomFloat() * 5);
		bunny.setSpeedY(BunnymarkUtils.nextRandomFloat() * 5 - 2.5f);
		baseTest.setInitialValues(bunny);
	}
}
