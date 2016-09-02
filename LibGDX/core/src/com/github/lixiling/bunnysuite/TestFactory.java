package com.github.lixiling.bunnysuite;

import com.badlogic.gdx.graphics.Texture;
import com.github.lixiling.bunnysuite.bunny.BunnyFlavour;
import com.github.lixiling.bunnysuite.test.BaseTest;
import com.github.lixiling.bunnysuite.test.ColorChangeDecorator;
import com.github.lixiling.bunnysuite.test.IBunnyTest;
import com.github.lixiling.bunnysuite.test.JumpDecorator;
import com.github.lixiling.bunnysuite.test.PulsationDecorator;
import com.github.lixiling.bunnysuite.test.RandomDecorator;
import com.github.lixiling.bunnysuite.test.RotationDecorator;
import com.github.lixiling.bunnysuite.test.ScaledDecorator;
import com.github.lixiling.bunnysuite.test.TeleportDecorator;
import com.github.lixiling.bunnysuite.test.TextureChangeDecorator;
import com.github.lixiling.bunnysuite.test.TintedDecorator;

/**
 * Factory for bunny tests.
 * 
 * @author Victor Schuemmer
 */
public class TestFactory {
	
	// Private constructor prevents instantiation, as this class only provides
	// static methods.
	private TestFactory() {
	}

	/**
	 * Creates a {@link IBunnyTest} composed of all given tests. With the
	 * addition of primitives, this method now also changes the state of static
	 * utility class {@link BunnymarkUtils}.
	 * 
	 * @param tests
	 *            string array containing the modifier strings
	 * @return a BunnyTest
	 */
	public static IBunnyTest createTest(String[] modifiers) {
		IBunnyTest test = new BaseTest();

		for (String m : modifiers) {
			switch (m) {
			case "standard":
				break;
			case "scaled":
				test = new ScaledDecorator(test);
				break;
			case "pulsation":
				test = new PulsationDecorator(test);
				break;
			case "random":
				test = new RandomDecorator(test);
				break;
			case "teleport":
				test = new TeleportDecorator(test);
				break;
			case "animation":
				test = new JumpDecorator(test);
				break;
			case "rotation":
				test = new RotationDecorator(test);
				break;
			case "tinted":
				test = new TintedDecorator(test);
				break;
			case "colorchange":
				test = new ColorChangeDecorator(test);
				break;
			case "texturechange":
				test = new TextureChangeDecorator(test);
				break;
			case "multitexture":
				BunnymarkUtils.addInitializer(() -> {
					BunnymarkUtils.addBunnyTexture(new Texture("wabbit_0.png"));
					BunnymarkUtils.addBunnyTexture(new Texture("wabbit_1.png"));
					BunnymarkUtils.addBunnyTexture(new Texture("wabbit_2.png"));
				});
				break;
			case "alpha":
				BunnymarkUtils.addInitializer(() -> {
					BunnymarkUtils.addBunnyTexture(new Texture("wabbit_ghost.png"));
				});
				break;
			case "hdtexture":
				BunnymarkUtils.addInitializer(() -> {
					BunnymarkUtils.addBunnyTexture(new Texture("wabbit_hd.png"));
				});
				break;
			case "thin":
				BunnymarkUtils.addInitializer(() -> {
					BunnymarkUtils.addBunnyTexture(new Texture("wabbit_y.png"));
				});
				break;
			case "rectangles":
				BunnymarkUtils.addBunnyFlavour(BunnyFlavour.RECTANGLE);
				break;
			case "circles":
				BunnymarkUtils.addBunnyFlavour(BunnyFlavour.CIRCLE);
				break;
			case "texts":
				BunnymarkUtils.addBunnyFlavour(BunnyFlavour.TEXT);
				break;
			case "lines":
				BunnymarkUtils.addBunnyFlavour(BunnyFlavour.LINE);
				break;
			case "triangles":
				BunnymarkUtils.addBunnyFlavour(BunnyFlavour.TRIANGLE);
				break;
			case "points":
				BunnymarkUtils.addBunnyFlavour(BunnyFlavour.POINT);
				break;
			case "bunnies":
				BunnymarkUtils.addBunnyFlavour(BunnyFlavour.BUNNY);
				break;
			case "no_output":
				BunnymarkUtils.enableDrawing(false);
				break;
			default:
				System.err.println("The requested modifier \'" + m + "\' does not exist.");
			}
		}
		return test;
	}
}
