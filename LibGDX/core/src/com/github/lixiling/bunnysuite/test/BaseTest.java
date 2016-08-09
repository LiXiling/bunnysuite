package com.github.lixiling.bunnysuite.test;

import com.badlogic.gdx.graphics.Texture;
import com.github.lixiling.bunnysuite.Bunny;
import com.github.lixiling.bunnysuite.BunnymarkUtils;

/**
 * The most basic test, that does indeed nothing with the bunnies. Add
 * decorators to create more interesting tests.
 * 
 * @author Victor Schuemmer
 */
public class BaseTest implements BunnyTest {

	@Override
	public void update(Bunny bunny) {
	}

	@Override
	public String getTestDescription() {
		return "";
	}

	@Override
	public void setInitialValues(Bunny bunny) {
		bunny.setTexture(BunnymarkUtils.getRandomBunnyTexture());
	}
	
	@Override
	public void initialize() {
		if (!BunnymarkUtils.hasTexture())
			BunnymarkUtils.addBunnyTexture(new Texture("wabbit_0.png"));
	}

}
