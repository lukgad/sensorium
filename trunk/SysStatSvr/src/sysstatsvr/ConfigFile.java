/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package sysstatsvr;

import commonSensor.Sensor;

import java.io.BufferedReader;
import java.io.IOException;

import java.util.List;

/**
 *
 * @author Aaron Maslen
 */
public class ConfigFile {
	

	List<Plugin> plugins;

	public ConfigFile(BufferedReader inputStream) throws IOException {
		try {
			String currentLine;
			Plugin currentPlugin = null;

			while((currentLine = inputStream.readLine()) != null) {

				String[] currentWords = currentLine.split(" ");

				if(currentPlugin != null && currentWords[0].equals("plugin"))
					currentPlugin = new Plugin(currentWords[2], currentWords[1]);
				else {

					if(currentWords[0].equals("end") && currentWords[1].equals("plugin")) {
						plugins.add(currentPlugin);
						currentPlugin = null;
					}
					else if(currentPlugin.getType().equals("input") && currentWords[0].equals("sensor")) {
						if(currentWords[1].equals("id") && currentWords[3].equals("type") && currentWords[5].equals("label")) {
							currentPlugin.addSensor(new Sensor(currentWords[6], currentWords[4], Integer.getInteger(currentWords[2])));
						}
					}
				}

			}
		}
		finally {
			if (inputStream!= null)
				inputStream.close();
		}
	}
}
