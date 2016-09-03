package com.github.lixiling.bunnysuite.test;

/**
 * Decorator for {@link BaseTest}. When extending this class, do not forget to
 * propagate method calls to baseTest.
 * 
 * @author Victor Schuemmer
 */
public abstract class AbstractTestDecorator implements IBunnyTest {

	protected IBunnyTest baseTest;

	public AbstractTestDecorator(IBunnyTest baseTest) {
		this.baseTest = baseTest;
	}

}
