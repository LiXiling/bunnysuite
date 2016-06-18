package com.github.lixiling.bunnysuite.test;

import com.github.lixiling.bunnysuite.Bunny;
import com.github.lixiling.bunnysuite.Bunnymark;

/**
 * A decorator for {@link BaseTest} that makes bunnies rotate 1 degree every
 * frame.
 * 
 * @author Victor Schuemmer
 */
public class RotationDecorator extends BaseTestDecorator {

	public RotationDecorator(BunnyTest baseTest) {
		super(baseTest);
	}

	@Override
	public void update(Bunny bunny) {
		bunny.rotate(1);
		baseTest.update(bunny);
	}

	@Override
	public String getTestDescription() {
		return "Bunnies are rotated by 1 degree every frame.\n" + baseTest.getTestDescription();
	}

	@Override
	public void setInitialValues(Bunny bunny) {
		bunny.setRotation(Bunnymark.nextRandomFloat() * 360);
		baseTest.setInitialValues(bunny);
	}

}
