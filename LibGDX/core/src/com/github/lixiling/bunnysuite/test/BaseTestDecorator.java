package com.github.lixiling.bunnysuite.test;

/**
 * Decorator for {@link BaseTest}. When extending this class, do not forget to
 * propagate method calls to baseTest.
 * 
 * @author Victor Schuemmer
 */
public abstract class BaseTestDecorator implements BunnyTest {

	protected BunnyTest baseTest;

	public BaseTestDecorator(BunnyTest baseTest) {
		this.baseTest = baseTest;
	}

}
