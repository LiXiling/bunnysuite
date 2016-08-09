package com.github.lixiling.bunnysuite.test;

import com.badlogic.gdx.graphics.Texture;
import com.github.lixiling.bunnysuite.Bunny;
import com.github.lixiling.bunnysuite.BunnymarkUtils;

public class AlphaTextureDecorator extends BaseTestDecorator {

	public AlphaTextureDecorator(BunnyTest baseTest) {
		super(baseTest);
		// TODO Auto-generated constructor stub
	}

	@Override
	public void update(Bunny bunny) {
		baseTest.update(bunny);
	}

	@Override
	public String getTestDescription() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public void setInitialValues(Bunny bunny) {
		baseTest.setInitialValues(bunny);
	}

	@Override
	public void initialize() {
		BunnymarkUtils.addBunnyTexture(new Texture("wabbit_ghost.png"));
		baseTest.initialize();
	}

}
