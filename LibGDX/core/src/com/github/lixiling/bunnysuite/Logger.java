package com.github.lixiling.bunnysuite;

import java.io.File;
import java.io.IOException;
import java.nio.charset.Charset;
import java.nio.file.Files;
import java.util.Vector;

/**
 * Handles creation of and writing to bunnymark logfiles.
 * 
 * @author Victor Schuemmer
 */
public class Logger {

	private String fileName;
	private Vector<String> lines;

	/**
	 * @param fileName
	 */
	public Logger(String fileName) {
		this.fileName = fileName;
		this.lines = new Vector<String>();
	}

	/**
	 * Logs number of things and time it took to render them, seperated by a
	 * tab.
	 * 
	 * @param n
	 *            number of things to render
	 * @param drawTime
	 *            time it took to render the things
	 */
	public void addLog(int n, float drawTime) {
		lines.add(n + "\t" + drawTime);
	}

	/**
	 * Adds a line of text to the log.
	 * 
	 * @param line
	 *            a line of text
	 */
	public void addLine(String line) {
		lines.add(line);
	}

	/**
	 * Writes all logs and lines to the logfile in the order they were given.
	 * The logfile is placed in a directory called "log" which is in the same
	 * directory as the jar or exe. Be aware that calling this method may create
	 * a new directory or replace an existing file.
	 */
	public void write() {
		File file = new File(getJarPath() + "/log/" + fileName + ".log");
		file.getParentFile().mkdirs();
		try {
			Files.write(file.toPath(), lines, Charset.forName("UTF-8"));
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	/**
	 * Determines the path of the jar this is executed from. When running from
	 * eclipse this is the same directory where "com" is located.
	 * 
	 * @return
	 */
	private String getJarPath() {
		String path = getClass().getProtectionDomain().getCodeSource().getLocation().getPath();
		path = path.substring(0, path.lastIndexOf("/"));
		path = path.replaceAll("%20", " ");
		return path;
	}
}
