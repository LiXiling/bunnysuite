package com.github.lixiling.bunnysuite;

import java.util.Iterator;
import java.util.Vector;

import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer.ShapeType;
import com.github.lixiling.bunnysuite.bunny.AbstractBunny;
import com.github.lixiling.bunnysuite.bunny.Bunny;
import com.github.lixiling.bunnysuite.test.IBunnyTest;

/**
 * The main application.
 * 
 * @author Victor Schuemmer
 */
public class Bunnymark extends ApplicationAdapter {

	private Vector<AbstractBunny> bunnies;

	private IBunnyTest test;
	private Logger logger;

	private int minBunnies;
	private int maxBunnies;
	private int step;

	private int renderedFramesCount;
	private final int framesPerStep;
	private float[] renderTimes;

	/**
	 * The Bunnymark takes care of setting up and executing the
	 * {@link AbstractTest}
	 * 
	 * @param test
	 *            the test to be executed
	 * @param minBunnies
	 *            the initial count of {@link Bunny bunnies}
	 * @param maxBunnies
	 *            the count of bunnies at which the bunnymark terminates
	 * @param step
	 *            the amount of bunnies to be added after each performance
	 *            measurement
	 */
	public Bunnymark(IBunnyTest test, String testName, int minBunnies, int maxBunnies, int step, int framesPerStep) {
		this.test = test;

		if (step > 0) this.logger = new Logger(testName);
		
		this.minBunnies = minBunnies;
		this.maxBunnies = maxBunnies;
		this.step = step;

		this.framesPerStep = framesPerStep;
		renderedFramesCount = 0;
		renderTimes = new float[framesPerStep];

		bunnies = new Vector<AbstractBunny>();
	}

	private SpriteBatch batch;
	private ShapeRenderer shapeRenderer;
	private BitmapFont font;
	
	@Override
	public void create() {
		batch = new SpriteBatch();
		shapeRenderer = new ShapeRenderer();
		font = new BitmapFont();
		addBunnies(minBunnies);
	}

	@Override
	public void dispose() {
		if (logger != null) logger.write();
	}

	@Override
	public void render() {
		Gdx.gl.glClearColor(0.21f, 0.21f, 0.21f, 1);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);

		if (bunnies.size() > maxBunnies)
			Gdx.app.exit();

		if (renderedFramesCount >= framesPerStep) {
			renderedFramesCount = 0;

			// Per step, 10 frames are rendered and the average render time per
			// frame is logged alongside the number of rendered bunnies.
			float avgRenderTime = 0.0f;
			for (float t : renderTimes)
				avgRenderTime += t;		
			avgRenderTime /= framesPerStep;

			if (logger != null) logger.addLog(bunnies.size(), avgRenderTime);

			// add some more bunnies.
			addBunnies(step);
		}

		// manipulate each of the bunnies.
		updateBunnies();

		if (BunnymarkUtils.drawingEnabled()) {
			// Draw the bunnies.
			shapeRenderer.begin(ShapeType.Line);
			batch.begin();
			Iterator<AbstractBunny> it = bunnies.iterator();
			while (it.hasNext()) {
				it.next().draw(batch, shapeRenderer);			
			}
			font.draw(batch, "FPS=" + Gdx.graphics.getFramesPerSecond() + "               COUNT=" + bunnies.size(), 10,
					20);
			batch.end();
			shapeRenderer.end();
		}
		
		// Save the render time for the current frame.
		renderTimes[renderedFramesCount++] = Gdx.graphics.getDeltaTime();

	}

	private void updateBunnies() {
		Iterator<AbstractBunny> it = bunnies.iterator();
		while (it.hasNext()) {
			test.update(it.next());
		}
	}

	private void addBunnies(int amount) {
		for (int i = 0; i < amount; i++) {
			AbstractBunny bunny = BunnymarkUtils.createBunny();
			test.setInitialValues(bunny);
			bunnies.add(bunny);
		}
	}	
}
