package com.github.lixiling.bunnysuite.testStrategy;

import java.util.Iterator;

import com.github.lixiling.bunnysuite.Bunny;

/**
 * @author Victor Schuemmer
 */
public final class RandomTest extends Test {
	
	@Override
	public void update() {
		Iterator<Bunny> it = getBunnies().iterator();	
		while(it.hasNext()) {
			Bunny bunny = it.next();
			bunny.teleport(random.nextFloat() * Test.screenWidth, random.nextFloat() * Test.screenHeight);
		}
	}

	@Override
	public void addBunnies(int amount) {
		for (int i = 0; i < amount; i++){
			Bunny bunny = new Bunny(getBunnyTexture(0));
			bunny.teleport(random.nextFloat() * 640, random.nextFloat() * 480);
			getBunnies().add(bunny);
		}
	}

	@Override
	public String getTestDescription() {
		return "Random position.";
	}

}
