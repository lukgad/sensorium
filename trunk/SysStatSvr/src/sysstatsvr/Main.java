/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 * TODO: Version 0.2 features
 * * v0.1 functionality
 * * Linux support
 * * v2 protocol and v1 backwards compatibility
 * 
 * TODO: Future features
 * * Additional sensor classes: Info and Data
 * * Info type has additional string private member and encapsulating methods
 * * Data type has additional byte array private member and encapulating methods
 * * Config file for server
 * * Move major functionality to plugins
 */

package sysstatsvr;

import commonSensor.SensorWrapper;
import commonSensor.misc;

import java.io.IOException;

import java.net.InetAddress;
import java.net.UnknownHostException;

/**
 *
 * @author Aaron Maslen
 */
public class Main {

    /**
     * @param args the command line arguments
     */

    static SensorWrapper PIsensors = null;

    public static void main(String[] args) {

        //Select a random port between 10000 and 20000
        //No real reason for this. I thought it would be fun.
        //TODO: Choose a logical default port.
        int UDPPort = (int) (Math.random() * 10000 + 10000);
        InetAddress IPAddress = null;

        //Update delay. Default is 1 second. For now.
        int uDelay = 1000;

        //Command-line arguments here. Pretty self-explanatory
        for(int i = 0; i < args.length; i++) {
            if (args[i].equals("-h") || args[i].equals("--help")) {
                System.out.println("Usage: ");
                System.out.println("sysstatsvr [-a address] [-L path] [-p port] [-h || --help]");
                System.out.println();
                System.out.println("OPTIONS");
                System.out.println("-a              Bind to a specific internet address");
                System.out.println("-L path         Set the path to the required native library");
                System.out.println("-p port         Set the listening port");
                System.out.println("-h || --help    Print this message and quit");
                System.out.println("-u delay        Update delay in milliseconds. " + "\n" +
                                   "                Default: " + uDelay);
                System.exit(0);
            }

            else if(args[i].equals("-L")) {
                i++;
                System.setProperty("jna.library.path",args[i]);
                System.out.println("Running with lib path: " + System.getProperty("jna.library.path"));
            }

            else if(args[i].equals("-a")) {
                i++;
                try {
                    IPAddress = InetAddress.getByName(args[i]);
                } catch (UnknownHostException e) {
                    System.out.println("Unknown host " + IPAddress.toString());
                    System.out.println("Using default bind address");
                    IPAddress = null;
                }
            }

            else if(args[i].equals("-p")) {
                i++;
                UDPPort = Integer.parseInt(args[i]);
            }
            
            else if(args[i].equals("-u")) {
                i++;
                if(Integer.parseInt(args[i]) > 0) {
                    uDelay = Integer.parseInt(args[i]);
                }
            }
        }

        if(System.getProperty("sun.desktop").equals("windows")) {

            //lib points to an instance of the Win32lib interface
            Win32IF lib = Win32IF.INSTANCE;

            //The "version" is 1 if SpeedFan is running, 0 (or NULL) if it is not
            short SFversion = lib.getVersion();
            System.out.println((SFversion == 1) ? "SpeedFan is running" :
                "SpeedFan is not running"); //I love the conditional operator =]

            //Define PIsensors for windows hosts using native library
            PIsensors = new WinSensorWrapper(lib);

        }
/*
        for(byte i = 0; i < PIsensors.getNumSensors(); i++) {
            System.out.println("ID: " + PIsensors.getSensorByID(i).getId() +
                    " Label: " + PIsensors.getSensorByID(i).getLabel() +
                    " Type: " + PIsensors.getSensorByID(i).getType() +
                    " Value: " + PIsensors.getSensorByID(i).getValue());
        }
*/
        //TODO [Complete]: Start server thread here
        try {
            if(IPAddress != null) {
                (new UDPServerThread(PIsensors, UDPPort, IPAddress)).start();
                System.out.println("Server listening on " + IPAddress + ":" + UDPPort);
            }

            else {
                (new UDPServerThread(PIsensors, UDPPort)).start();
                System.out.println("Server listening on port " + UDPPort);
            }

        }
        catch(IOException e) {
            System.out.println("Could not start server thread. Exiting");

			if(misc.DEBUG)
				e.printStackTrace();
			
            return;
        }

        //Update sensors here
        System.out.println("Updating every " + uDelay + "ms");

        while(true) {
            try {
                Thread.sleep(uDelay);

                PIsensors.update();
            } catch (InterruptedException e) {
                break;
            }

        }
    }

    
}
