package com.github.lixiling.bunnysuite.testStrategy;

import java.util.Iterator;

import com.github.lixiling.bunnysuite.Bunny;

/**
 * @author Victor Schuemmer
 */
public final class ScaledTest extends Test {
	
	@Override
	public void update() {
		Iterator<Bunny> it = getBunnies().iterator();
		
		while(it.hasNext()) {
			Bunny bunny = it.next();
			bunny.grow();
			if (bunny.getScaleX() >= 5 || bunny.getScaleX() <= 0.2)
				bunny.setGrowth(-1 * bunny.getGrowth());
		}
	}

	@Override
	public void addBunnies(int amount) {
		for (int i = 0; i < amount; i++){
			Bunny bunny = new Bunny(getBunnyTexture(0));
			bunny.setScale(random.nextFloat() * 5);
			bunny.teleport(random.nextFloat() * Test.screenWidth, random.nextFloat() * Test.screenHeight);
			getBunnies().add(bunny);
		}
	}

	@Override
	public String getTestDescription() {
		return "Random scale and position.";
	}

}
