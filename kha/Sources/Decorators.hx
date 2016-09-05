class AbstractTestDecorator implements IBunnyTest {
	private var baseTest : IBunnyTest;
	
	private function new(baseTest: IBunnyTest) {
		this.baseTest = baseTest;
	}
	
	public function setInitialValues(bunny: Bunny.AbstractBunny){};
	public function update(bunny: Bunny.AbstractBunny){};
}

class JumpDecorator extends AbstractTestDecorator {
	public function new(baseTest: IBunnyTest) {
		super(baseTest);
	}
	
	public override function update(bunny: Bunny.AbstractBunny) {
		bunny.jump(0.5, 0, 0, BunnymarkUtils.getWidth(), BunnymarkUtils.getHeight());
		baseTest.update(bunny);
	}
	
	public override function setInitialValues(bunny: Bunny.AbstractBunny) {
		bunny.setSpeedX(Math.random() * 5);
		bunny.setSpeedY(Math.random() * 5 - 2.5);
		baseTest.setInitialValues(bunny);
	}
}


class PulsationDecorator extends AbstractTestDecorator {
	public function new(baseTest: IBunnyTest) {
		super(baseTest);
	}
	
	public override function update(bunny: Bunny.AbstractBunny) {
		bunny.grow();
		if (bunny.getScaleX() >= 5 || bunny.getScaleX() <= 0.2)
			bunny.setGrowth(-1 * bunny.getGrowth());
		baseTest.update(bunny);
	}
	
	public override function setInitialValues(bunny: Bunny.AbstractBunny) {
		baseTest.setInitialValues(bunny);
	}
}

class RandomDecorator extends AbstractTestDecorator {
	public function new(baseTest: IBunnyTest) {
		super(baseTest);
	}
	
	public override function update(bunny: Bunny.AbstractBunny) {
		baseTest.update(bunny);
	}
	
	public override function setInitialValues(bunny: Bunny.AbstractBunny) {
		bunny.teleport(BunnymarkUtils.getRandomX(), BunnymarkUtils.getRandomY());
		baseTest.setInitialValues(bunny);
	}
}

class RotationDecorator extends AbstractTestDecorator {
	public function new(baseTest: IBunnyTest) {
		super(baseTest);
	}
	
	public override function update(bunny: Bunny.AbstractBunny) {
		bunny.rotate(1);
		baseTest.update(bunny);
	}
	
	public override function setInitialValues(bunny: Bunny.AbstractBunny) {
		bunny.setRotation(Math.random() * 360);
		baseTest.setInitialValues(bunny);
	}
}

class ScaledDecorator extends AbstractTestDecorator {
	public function new(baseTest: IBunnyTest) {
		super(baseTest);
	}
	
	public override function update(bunny: Bunny.AbstractBunny) {
		baseTest.update(bunny);
	}
	
	public override function setInitialValues(bunny: Bunny.AbstractBunny) {
		bunny.setScale(
				Math.random() * 4.8 + 0.2, 
				Math.random() * 4.8 + 0.2);
		baseTest.setInitialValues(bunny);
	}
}

class TeleportDecorator extends AbstractTestDecorator {
	public function new(baseTest: IBunnyTest) {
		super(baseTest);
	}
	
	public override function update(bunny: Bunny.AbstractBunny) {
		bunny.teleport(BunnymarkUtils.getRandomX(), BunnymarkUtils.getRandomY());
		baseTest.update(bunny);
	}
	
	public override function setInitialValues(bunny: Bunny.AbstractBunny) {
		bunny.teleport(BunnymarkUtils.getRandomX(), BunnymarkUtils.getRandomY());
		baseTest.setInitialValues(bunny);
	}
}

class TextureChangeDecorator extends AbstractTestDecorator {
	public function new(baseTest: IBunnyTest) {
		super(baseTest);
	}
	
	public override function update(bunny: Bunny.AbstractBunny) {
		try {
			bunny.setTexture(BunnymarkUtils.getRandomBunnyTexture());
		} catch (e : String) {}
		baseTest.update(bunny);
	}
	
	public override function setInitialValues(bunny: Bunny.AbstractBunny) {
		baseTest.setInitialValues(bunny);
	}
}

class TintedDecorator extends AbstractTestDecorator {
	public function new(baseTest: IBunnyTest) {
		super(baseTest);
	}
	
	public override function update(bunny: Bunny.AbstractBunny) {
		baseTest.update(bunny);
	}
	
	public override function setInitialValues(bunny: Bunny.AbstractBunny) {
		bunny.setColor(BunnymarkUtils.randomColor());
		baseTest.setInitialValues(bunny);
	}
}

class ColorChangeDecorator extends AbstractTestDecorator {
	public function new(baseTest: IBunnyTest) {
		super(baseTest);
	}
	
	public override function update(bunny: Bunny.AbstractBunny) {
		bunny.setColor(BunnymarkUtils.randomColor());
		baseTest.update(bunny);
	}
	
	public override function setInitialValues(bunny: Bunny.AbstractBunny) {
		bunny.setColor(BunnymarkUtils.randomColor());
		baseTest.setInitialValues(bunny);
	}
}