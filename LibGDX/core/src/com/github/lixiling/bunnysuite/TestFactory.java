package com.github.lixiling.bunnysuite;

import com.github.lixiling.bunnysuite.test.BaseTest;
import com.github.lixiling.bunnysuite.test.BunnyTest;
import com.github.lixiling.bunnysuite.test.JumpDecorator;
import com.github.lixiling.bunnysuite.test.MultiTextureDecorator;
import com.github.lixiling.bunnysuite.test.RandomDecorator;
import com.github.lixiling.bunnysuite.test.RotationDecorator;
import com.github.lixiling.bunnysuite.test.ScaledDecorator;
import com.github.lixiling.bunnysuite.test.TextureChangeDecorator;

/**
 * Factory for bunny tests.
 * @author Victor Schuemmer
 */
public class TestFactory {
	/**
	 * Creates a {@link BunnyTest} composed of all given tests.
	 * @param tests string array containing the test identifiers
	 * @return a BunnyTest
	 */
	public static BunnyTest createTest(String[] tests) {
		BunnyTest test = new BaseTest();
		
		for (String t: tests) {
			switch (t) {
			case "standard":
				break;
			case "scaled":
				test = new ScaledDecorator(test);
				break;
			case "random":
				test = new RandomDecorator(test);
				break;
			case "multitexture":
				test = new MultiTextureDecorator(test);
				break;
			case "texturechange":
				test = new TextureChangeDecorator(test);
				break;
			case "animated":
				test = new JumpDecorator(test);
				break;
			case "rotated":
				test = new RotationDecorator(test);
				break;
			default:
				// TODO If needed in the future, this may be replaced by an appropriate exception.
				System.err.println("The requested test \'" + t + "\' does not exist.");
				System.exit(1);
				return null;
			}
		}
		return test;
	}	
}
