/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package testclientv2;

import commonSensor.*;
import java.net.InetAddress;

/**
 *
 * @author Aaron Maslen
 */
public class Main {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
		for(int i = 0; i < args.length; i++) {
			System.out.print(args[i]);
			System.out.println();
		}
		
		try {
			
			SensorWrapper remoteSensors = new ClientSensorWrapper(InetAddress.getByName(args[0]), Integer.parseInt(args[1]), 1000);
			for(byte i = 0; i < remoteSensors.getNumSensors(); i++) {
				System.out.print("ID: " + i + " ");
				System.out.print("Label: " + remoteSensors.getSensorByID(i).getLabel() + " ");
				System.out.print("Type: " + remoteSensors.getSensorByID(i).getType() + " ");
				System.out.print("Value: " + remoteSensors.getSensorByID(i).getValue() + "\n");
			}
		}
		catch (Exception e) {
			e.printStackTrace();
		}
    }
}
