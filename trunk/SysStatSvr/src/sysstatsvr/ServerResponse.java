/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package sysstatsvr;

import commonSensor.SensorWrapper;
import commonSensor.misc;
import java.io.IOException;

/**
 *
 * @author Aaron Maslen
 */
public class ServerResponse {

	private byte[] data;
	private SensorWrapper sensors;
	private int length;

	/**
	 * @return the length
	 */
	public int getLength() {
		return length;
	}

	//Because Dan said it looks nicer...
	//TODO: Move to commonSensor
	private static class REQUEST_TYPE {
        public static final byte NUMSENSORS = 0;
        public static final byte LABEL = 1;
        public static final byte TYPE = 2;
        public static final byte RAW_VALUE = 3;
        public static final byte MULTIPLIER = 4;
    }

	public ServerResponse(SensorWrapper aSensors, byte[] request) {
		sensors = aSensors;
		
		try {
			if(request[0] == 2) {
				data = new byte[misc.BUFFERSIZE];
				
				data[0] = 2;
				data[1] = request[1];

				length = 2;

				//Collect the packet data
				data[2] = request[2];
				byte[] dataBytes = null;

				switch(request[1]) {
					case REQUEST_TYPE.NUMSENSORS:
						data[2] = sensors.getNumSensors();
						break;

					case REQUEST_TYPE.LABEL:
						String label = sensors.getSensorByID(data[2]).getLabel();

						dataBytes = label.getBytes("UTF-8");
						break;

					case REQUEST_TYPE.TYPE:
						String type = sensors.getSensorByID(data[2]).getType();

						dataBytes = type.getBytes("UTF-8");
						break;

					case REQUEST_TYPE.RAW_VALUE:
						dataBytes = misc.toBytes(sensors.getSensorByID(data[2]).getValueRaw());
						break;

					case REQUEST_TYPE.MULTIPLIER:
						dataBytes = misc.toBytes(sensors.getSensorByID(data[2]).getMultiplier());
						break;
				}
				if(request[1] >= 0 && request[1] <= 4) {
					if(dataBytes != null) {
						for(int i = 0; i < dataBytes.length; i++) {
									length++;

									data[length] = dataBytes[i];
						}
					}

					length++;
				}
			}
		}
		catch(IOException e) {
			if(misc.DEBUG)
				e.printStackTrace();

			return;
		}
	}

	/**
	 * @return the data
	 */
	public byte[] getData() {
		return data;
	}

}
