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
    private static final short BUFFERSIZE = 4096;

    //Because Dan said it looks nicer...
    private static class REQUEST_TYPE {
        public static final byte NUMSENSORS = 0;
        public static final byte LABEL = 1;
        public static final byte TYPE = 2;
        public static final byte RAW_VALUE = 3;
        public static final byte MULTIPLIER = 4;
    }

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
                
                byte[] buf = new byte[BUFFERSIZE];

                //Wait for packet
                DatagramPacket packet = new DatagramPacket(buf, buf.length);
                socket.receive(packet);

                //Retrieve message
                //String message = new String(packet.getData(), 0, packet.getLength());

                //Debug: Print recieved text
                //System.out.println(message);
                
                
                    int rPort = packet.getPort();
                    InetAddress rAddress = packet.getAddress();
                    //buf = "sup dawg".getBytes();

                    buf = new byte[BUFFERSIZE];

                    if(packet.getData()[0] == 2) {
                        buf[0] = 2;
                        buf[1] = packet.getData()[1];
                        
                        int currentIndex = 2;

                        //Collect the packet data
                        buf[2] = packet.getData()[2];
                        byte[] dataBytes = null;

                        switch(packet.getData()[1]) {
                            case REQUEST_TYPE.NUMSENSORS:
                                buf[2] = sensors.getNumSensors();
                                break;

                            case REQUEST_TYPE.LABEL:
                                String label = sensors.getSensorByID(buf[2]).getLabel();

                                dataBytes = label.getBytes("UTF-8");
                                break;

                            case REQUEST_TYPE.TYPE:
                                String type = sensors.getSensorByID(buf[2]).getType();
                                
                                dataBytes = type.getBytes("UTF-8");
                                break;

                            case REQUEST_TYPE.RAW_VALUE:
                                dataBytes = misc.toBytes(sensors.getSensorByID(buf[2]).getValueRaw());
                                break;

                            case REQUEST_TYPE.MULTIPLIER:
                                dataBytes = misc.toBytes(sensors.getSensorByID(buf[2]).getMultiplier());
                                break;
                        }
                        if(packet.getData()[1] >= 0 && packet.getData()[1] <= 4) {
                            if(dataBytes != null) {
								for(int i = 0; i < dataBytes.length; i++) {
											currentIndex++;

											buf[currentIndex] = dataBytes[i];
								}
							}

							currentIndex++;
							
                            //Construct and send packet
                            packet = new DatagramPacket(buf, currentIndex, rAddress, rPort);

                            socket.send(packet);
							System.out.println("response sent");
                        }
                    }



                    /*if(packet.getData()[0] == 1) { //Request is now simply the protocol version
                        
                     /* Packet (v1) format is as follows:
                     * Index    Data
                     * 0        1 if sensors can be retrieved, 0 if not
                     * 1        Number of Fan sensors
                     * 2        Number of Temperature sensors
                     * 3        Number of Voltage Sensors
                     * 4 - x    Sensor data as series of integers: Fans, then
                     *          Temperatures, then Voltages
                     * x - end  Newline-separated string containg label names in
                     *          same order as sensor data. UTF-8 encoding.
                     * /
                        
                        //TODO: Reimplement v1... if necessary

                    }*/
            } while(!Thread.interrupted());
        } catch(IOException e) {
            e.printStackTrace();
            socket.close();
            return;
        }
        socket.close(); //If the thread is interrupted, exit gracefully
    }
}
