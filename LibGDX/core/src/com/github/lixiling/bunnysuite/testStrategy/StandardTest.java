package com.github.lixiling.bunnysuite.testStrategy;

import com.github.lixiling.bunnysuite.Bunny;

/**
 * @author Victor Schuemmer
 */
public final class StandardTest extends Test {

	@Override
	public String getTestDescription() {
		return "Standard test: Draw non-moving bunnies to (0,0).";
				//+ System.getProperty("line.separator") 
				//+ "Minimum number of bunnies: "
				//+ minBunnies + ", maximum number of bunnies: " + maxBunnies + ", step size: " + step;
	};
	
	@Override
	public void update() {
	}

	@Override
	public void addBunnies(int amount) {	
		for (int i = 0; i < amount; i++){
			Bunny bunny = new Bunny(getBunnyTexture(0));
			getBunnies().add(bunny);
		}
	}		


}
