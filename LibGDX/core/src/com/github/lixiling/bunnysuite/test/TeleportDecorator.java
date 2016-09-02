package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.BunnymarkUtils;
import com.github.lixiling.bunnysuite.bunny.AbstractBunny;

/**
 * A decorator for {@link BaseTest} that makes bunnies teleport to a new random
 * location every frame.
 * 
 * @author Victor Schuemmer
 */
public final class TeleportDecorator extends AbstractTestDecorator {

	public TeleportDecorator(IBunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(AbstractBunny bunny) {
		bunny.teleport(BunnymarkUtils.getRandomX(), BunnymarkUtils.getRandomY());
		baseTest.update(bunny);
	}

	@Override
	public void setInitialValues(AbstractBunny bunny) {
		bunny.teleport(BunnymarkUtils.getRandomX(), BunnymarkUtils.getRandomY());
		baseTest.setInitialValues(bunny);
	}
}
