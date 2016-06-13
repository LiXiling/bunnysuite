package com.github.lixiling.bunnysuite.testStrategy;

import java.util.Iterator;

import com.github.lixiling.bunnysuite.Bunny;

/**
 * @author Victor Schuemmer
 */
public final class TextureChangeTest extends Test {

	@Override
	public void update() {
		Iterator<Bunny> it = getBunnies().iterator();
		
		while(it.hasNext()) {
			Bunny bunny = it.next();
			bunny.setTexture(getBunnyTexture(random.nextInt(3)));
		}
	}

	@Override
	public void addBunnies(int amount) {
		for (int i = 0; i < amount; i++){
			Bunny bunny = new Bunny(getBunnyTexture(random.nextInt(3)));
			bunny.teleport(random.nextFloat() * Test.screenWidth, random.nextFloat() * Test.screenHeight);
			getBunnies().add(bunny);
		}
	}

	@Override
	public String getTestDescription() {
		return null;
	}

}
