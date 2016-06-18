package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.Bunny;

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
	}

}
