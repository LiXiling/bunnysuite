package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.BunnymarkUtils;
import com.badlogic.gdx.graphics.Texture;
import com.github.lixiling.bunnysuite.Bunny;

/**
 * A decorator for {@link BaseTest} that makes the bunnies have random initial
 * bunny textures.
 * 
 * @author Victor Schuemmer
 */
public final class MultiTextureDecorator extends BaseTestDecorator {

	public MultiTextureDecorator(BunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(Bunny bunny) {
		baseTest.update(bunny);
	}

	@Override
	public String getTestDescription() {
		return "Random position and fixed random texture.\n" + baseTest.getTestDescription();
	}

	@Override
	public void setInitialValues(Bunny bunny) {
		bunny.teleport(BunnymarkUtils.getRandomX(), BunnymarkUtils.getRandomY());
		baseTest.setInitialValues(bunny);
	}

	@Override
	public void initialize() {
		BunnymarkUtils.addBunnyTexture(new Texture("wabbit_0.png"));
		BunnymarkUtils.addBunnyTexture(new Texture("wabbit_1.png"));
		BunnymarkUtils.addBunnyTexture(new Texture("wabbit_2.png"));	
		baseTest.initialize();
	}

}
