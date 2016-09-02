package com.github.lixiling.bunnysuite.desktop;

import com.badlogic.gdx.backends.lwjgl.LwjglApplication;
import com.badlogic.gdx.backends.lwjgl.LwjglApplicationConfiguration;
import com.github.lixiling.bunnysuite.Bunnymark;
import com.github.lixiling.bunnysuite.TestFactory;

/**
 * The entry point to the application.
 * @author Victor Schuemmer
 */
public class DesktopLauncher {
	
	private static final String USAGE = "Usage:\nApp.jar <testname1>[,testname2...] <minValue> <maxValue> <stepSize> [width] [height] [framesPerStep]"; 
	
	public static void main(String[] arg) {
		
		LwjglApplicationConfiguration config = new LwjglApplicationConfiguration();
		try {
			config.width = Integer.parseInt(arg[4]);
			config.height = Integer.parseInt(arg[5]);
		} catch (Exception e) {
			config.width = 800;
			config.height = 600;
		}
		config.resizable = false;
		int framesPerStep;
		try {
			framesPerStep = Integer.parseInt(arg[6]);
		} catch (Exception e) {
			framesPerStep = 10;
		}
		try {
			if (Integer.parseInt(arg[3]) < 1) {
				System.err.println("The Bunnymark will not terminate automatically with step size 0.");
			}
			new LwjglApplication(
					new Bunnymark(TestFactory.createTest(arg[0].split(",")), 
							arg[0], 
							Integer.parseInt(arg[1]), 
							Integer.parseInt(arg[2]),
							Integer.parseInt(arg[3]),
							framesPerStep), config);
			
		} catch (NumberFormatException e) {
			System.err.println("Please provide minValue, maxValue and step as integer.\n" + USAGE);
			System.exit(1);
		} catch (ArrayIndexOutOfBoundsException e) {
			System.err.println("Wrong number of arguments.\n" + USAGE);
			System.exit(1);
		}
	}
}
