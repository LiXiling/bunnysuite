package com.github.lixiling.bunnysuite.testStrategy;

import java.util.Iterator;

import com.github.lixiling.bunnysuite.Bunny;

/**
 * @author Victor Schuemmer
 */
public final class JumpTest extends Test {

	@Override
	public void update() {
		Iterator<Bunny> it = getBunnies().iterator();
		while(it.hasNext()) {
			Bunny bunny = it.next();
			bunny.jump(0.5f, 0, 0, Test.screenWidth, Test.screenHeight);
		}
	}

	@Override
	public void addBunnies(int amount) {
		for (int i = 0; i < amount; i++){
			Bunny bunny = new Bunny(getBunnyTexture(0));
			bunny.setSpeedX(random.nextFloat() * 5);
			bunny.setSpeedY(random.nextFloat() * 5 - 2.5f);
			getBunnies().add(bunny);
		}
	}

	@Override
	public String getTestDescription() {
		return "Fills the screen with jumping bunnies.";
	}

}
