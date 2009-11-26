/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package sysstatsvr;

import commonSensor.Sensor;
import commonSensor.SensorList;

/**
 *
 * @author Aaron Maslen
 */
public class Plugin {
	enum PluginType {input, server};

	private PluginType type;
	private String file;
	SensorList sensors = null;
	
	public Plugin (String fileName, String t) {
		if(t.equals("input"))
			type = PluginType.input;
		else if (t.equals("server"))
			type = PluginType.server;

		file = fileName;
	}

	/**
	 * @return the type
	 */
	public String getType() {
		return type.toString();
	}

	/**
	 * @return the file
	 */
	public String getFile() {
		return file;
	}

	public boolean addSensor(Sensor sensor) {
		if(type != PluginType.input)
			return false;

		for(Sensor s : sensors) {
			if(s.getId() == sensor.getId())
				return false;
		}

		sensors.add(sensor);
		return true;
	}
}
