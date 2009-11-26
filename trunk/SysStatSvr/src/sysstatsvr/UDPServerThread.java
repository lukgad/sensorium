/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package sysstatsvr;

import commonSensor.SensorWrapper;
import commonSensor.misc;

import java.io.IOException;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

/**
 *
 * @author Aaron Maslen
 */
public class UDPServerThread extends Thread {
    protected DatagramSocket socket = null;
    private SensorWrapper sensors = null;
    private InetAddress address = null;

    /* TODO: Ensure the buffer is never too small
     * With the label names, this packet could be quite large...
     * I'm not sure how big to make it. 4096 bytes seems safe for now.
     * v2 of the protocol should help.
     * TODO [Complete]: Protocol version 2
     *      Possible packet format:
     *          Request:
     *          byte    data
     *          0       2 (current version number)
     *          1       Request (response type)
     *                  0 = Number of sensors (byte)
     *                  1 = Label (UTF-8 string)
     *                  2 = Type (UTF-8 string)
     *                  3 = Raw Value (integer)
     *                  4 = Multiplier (float)
     *          2       Sensor ID for request (ignored if request type == 0)
     *
     *          Response:
     *          byte    data
     *          0-2     As for request
     *          3-end   Requested data
     *
     */

    //Various constructors
    //Only 2 arguments required: The sensors object and a port number
    //Optional arguments: bind address, thread id, and update sensors on request
    public UDPServerThread(SensorWrapper aSensors, int port) throws IOException {
        this("UDPServerThread", aSensors, port);
    }

    public UDPServerThread(SensorWrapper aSensors, int port, InetAddress IPaddress) throws IOException {
        this("UDPServerThread", aSensors, port, IPaddress);
    }

    
    public UDPServerThread(String name, SensorWrapper aSensors, int port) throws IOException {
        super(name);
        socket = new DatagramSocket(port);
        sensors = aSensors;
    }

    public UDPServerThread(String name, SensorWrapper aSensors, int port, InetAddress IPaddress) throws IOException {
        super(name);
        address = IPaddress;
        socket = new DatagramSocket(port, address);
        sensors = aSensors;
    }



    @Override
    public void run() {
        try {
            do {
                
                byte[] buf = new byte[misc.BUFFERSIZE];

                //Wait for packet
                DatagramPacket packet = new DatagramPacket(buf, buf.length);
				socket.receive(packet);
               
				int rPort = packet.getPort();
				InetAddress rAddress = packet.getAddress();

				ServerResponse response = new ServerResponse(sensors, packet.getData());

				//Send packet
				packet = new DatagramPacket(response.getData(), response.getLength(), rAddress, rPort);

				socket.send(packet);
				
				if(misc.DEBUG)
					System.out.println("response sent");
            } while(!Thread.interrupted());
        } catch(IOException e) {
			if(misc.DEBUG)
				e.printStackTrace();

			//socket.close();
            return;
        }
        socket.close(); //If the thread is interrupted, exit gracefully
    }
}
