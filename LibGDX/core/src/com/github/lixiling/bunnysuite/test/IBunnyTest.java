package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.bunny.AbstractBunny;

/**
 * Interface for bunny tests.
 * 
 * @author Victor Schuemmer
 */
public interface IBunnyTest {

	/**
	 * Manipulates the bunny's properties in some way. This should be called
	 * every time a frame is rendered.
	 */
	public abstract void update(AbstractBunny bunny);

	/**
	 * Initializes the given bunny. This should be called every time a new bunny
	 * is created.
	 */
	public abstract void setInitialValues(AbstractBunny bunny);
}
