package com.github.lixiling.bunnysuite.testStrategy;

import com.github.lixiling.bunnysuite.Bunny;

/**
 * @author Victor Schuemmer
 */
public final class MultiTextureTest extends Test {

	@Override
	public void update() {
	}

	@Override
	public void addBunnies(int amount) {
		for (int i = 0; i < amount; i++){
			Bunny bunny = new Bunny(getBunnyTexture(i));
			bunny.teleport(random.nextFloat() * Test.screenWidth, random.nextFloat() * Test.screenHeight);
			getBunnies().add(bunny);
		}
	}

	@Override
	public String getTestDescription() {
		return "Random position and one of 3 textures.";
	}

}
