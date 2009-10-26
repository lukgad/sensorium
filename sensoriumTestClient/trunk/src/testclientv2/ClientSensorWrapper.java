/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package testclientv2;

import commonSensor.*;
import java.io.IOException;
import java.io.InterruptedIOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketException;
import java.util.List;

/**
 *
 * @author Aaron Maslen
 */
public class ClientSensorWrapper extends SensorWrapper{
	private DatagramSocket socket = null;
    private InetAddress address = null;
	private int UDPPort = 0;

	private static final short BUFFER_SIZE = 4096;

	private static class REQUEST_TYPE {
        public static final byte NUMSENSORS = 0;
        public static final byte LABEL = 1;
        public static final byte TYPE = 2;
        public static final byte RAW_VALUE = 3;
        public static final byte MULTIPLIER = 4;
    }

	private class MalformedResponseException extends IOException {
		public MalformedResponseException() {
			super();
		}
		public MalformedResponseException(String s) {
			super(s);
		}
	}

    public ClientSensorWrapper(InetAddress IPAddress, int port, int timeout) throws SocketException {
        address = IPAddress;
		socket = new DatagramSocket();
		socket.setSoTimeout(timeout);
		UDPPort = port;

		this.update();
    }

    public byte getNumSensors() {
        return (byte)Sensors.size();
    }

    public byte getNumSensors(String type) {
		int i = 0;
        for(Sensor s : Sensors) {
            if(s.getType().equals(type))
                i++;
        }
        return (byte)i;
    }

    public Sensor getSensorByID(byte id) {
		return Sensors.get(id);
    }

    public List<Sensor> getSensorsByLabel(String label) {
        SensorList temp = new SensorList();

        for(Sensor s : Sensors) {
            if(s.getLabel().equals(label))
                temp.add(s);
        }
        return temp;
    }

    public List<Sensor> getSensorsByType(String type) {
        SensorList temp = new SensorList();

        for(Sensor s : Sensors) {
            if(s.getType().equals(type));
        }

        return temp;
    }

	private byte[] requestData(byte request, byte id) throws IOException {
			byte[] buf = new byte[BUFFER_SIZE];
			buf[0] = 2; //Protocol version 2
			buf[1] = request;
			buf[2] = id;

			DatagramPacket packet = new DatagramPacket(buf, buf.length, address, UDPPort);

			socket.send(packet);


			//Get response
			buf = new byte[BUFFER_SIZE];
			packet = new DatagramPacket(buf, buf.length);

			socket.receive(packet);

			if (packet.getData()[0] == 2 && packet.getData()[1] == request) {
				byte[] packetData = new byte[packet.getLength() + 1];
				for(int i = 0; i <= packet.getLength(); i++) {
					packetData[i] = packet.getData()[i];
				}
				return packetData;
			}
			else {
				throw new MalformedResponseException("Unexpected response");
			}
	}

    public void update() {
		try {
			byte[] packetData = this.requestData(REQUEST_TYPE.NUMSENSORS, (byte)0);

			Sensors = new SensorList();

			byte numSensors = packetData[2];
			
			for(byte i = 0; i < numSensors; i++) {
				String label = new String();
				String type = new String();
				int rawValue = 0;
				float multiplier = 0;

				try {
					//Get label
					packetData = this.requestData(REQUEST_TYPE.LABEL, i);
					label = new String(packetData, 3, packetData.length - 3, "UTF-8");


					//Get type
					packetData = this.requestData(REQUEST_TYPE.TYPE, i);
					type = new String(packetData, 3, packetData.length - 3, "UTF-8");


					//Get raw value
					packetData = this.requestData(REQUEST_TYPE.RAW_VALUE, i);
					byte[] valueBytes = new byte[packetData.length - 2];

					for(int j = 3; j < packetData.length; j++)
						valueBytes[j-3] = packetData[j];

					rawValue = misc.bytesToInt(valueBytes);
					
					//Get Multiplier
					packetData = this.requestData(REQUEST_TYPE.MULTIPLIER, i);
					byte[] multBytes = new byte[packetData.length - 2];

					for(int j = 3; j < packetData.length; j++)
						multBytes[j-3] = packetData[j];

					multiplier = misc.bytesToFloat(multBytes);
					
					Sensors.add(new Sensor(label, type, multiplier, i));
					Sensors.get(i).setValue(rawValue);
				}
				catch(InterruptedIOException ie) {
					System.out.println("A response timed out");
				}
				catch(MalformedResponseException mre) {
					System.out.println(mre.getMessage());
				}
			}
			}
		catch(IOException e) {
			e.printStackTrace();
		}
    }
}
