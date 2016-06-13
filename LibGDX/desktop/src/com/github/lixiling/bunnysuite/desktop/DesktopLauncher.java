package com.github.lixiling.bunnysuite.desktop;

import com.badlogic.gdx.backends.lwjgl.LwjglApplication;
import com.badlogic.gdx.backends.lwjgl.LwjglApplicationConfiguration;
import com.github.lixiling.bunnysuite.Bunnymark;
import com.github.lixiling.bunnysuite.testStrategy.JumpTest;
import com.github.lixiling.bunnysuite.testStrategy.MultiTextureTest;
import com.github.lixiling.bunnysuite.testStrategy.RandomTest;
import com.github.lixiling.bunnysuite.testStrategy.ScaledTest;
import com.github.lixiling.bunnysuite.testStrategy.StandardTest;
import com.github.lixiling.bunnysuite.testStrategy.Test;
import com.github.lixiling.bunnysuite.testStrategy.TextureChangeTest;

public class DesktopLauncher {
	public static void main(String[] arg) {
		LwjglApplicationConfiguration config = new LwjglApplicationConfiguration();
		Test.setScreenDimensions(config.width = 640, config.height = 480);

		if (arg.length != 4) {
			System.out.println("Wrong number of arguments. Using default values.");
			new LwjglApplication(new Bunnymark(new JumpTest(), "animated", 0, 10000, 100), config);
			return;
		}

		Test test;
		switch (arg[0]) {
		case "standard":
			test = new StandardTest();
			break;
		case "scaled":
			test = new ScaledTest();
			break;
		case "random":
			test = new RandomTest();
			break;
		case "multitexture":
			test = new MultiTextureTest();
			break;
		case "texturechange":
			test = new TextureChangeTest();
			break;
		case "animated":
			test = new JumpTest();
			break;
		default:
			System.err.println("The requested test does not exist.");
			System.exit(1);
			return;
		}
		
		try {
			new LwjglApplication(new Bunnymark(test, arg[0], Integer.parseInt(arg[1]), Integer.parseInt(arg[2]),
				Integer.parseInt(arg[3])), config);
		} catch (NumberFormatException e) {
			System.err.println("Please provide minValue, maxValue and step as integer.");
			System.exit(1);
		}
	}
}
