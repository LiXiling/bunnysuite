package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.Bunny;

/**
 * @author Victor Schuemmer
 */
public interface BunnyTest {

	/**
	 * Manipulates the bunny's properties in some way. This should be called
	 * every time a frame is rendered.
	 */
	public abstract void update(Bunny bunny);

	/**
	 * @return A String describing the test.
	 */
	public abstract String getTestDescription();

	/**
	 * Initializes the given bunny. This should be called every time a new bunny
	 * is created.
	 */
	public abstract void setInitialValues(Bunny bunny);
	
	public abstract void initialize();
}
