package sysstatsvr;

import commonSensor.Sensor;
import commonSensor.SensorList;
import commonSensor.SensorWrapper;

import java.util.List;

/**
 *
 * @author Aaron Maslen
 * 
 */

public class WinSensorWrapper extends SensorWrapper {
    private Win32IF SFLib;

    private SensorList Fans = new SensorList();
    private SensorList Temps = new SensorList();
    private SensorList Volts = new SensorList();

    public WinSensorWrapper(Win32IF SpeedFanLib) {
        SFLib = SpeedFanLib;

        Sensors = new SensorList();
        
        byte currentID = 0;

        //Add Sensors to the Sensors list
        for(int i = 0; i < SFLib.getNumFans(); i++) {
            Fans.add(i, new Sensor("Fan" + (i+1), "Fan", currentID));
            Sensors.add(Fans.get(i));
            currentID++;
        }
        
        for(int i = 0; i < SFLib.getNumTemps(); i++) {
            Temps.add(i, new Sensor("Temp" + (i+1), "Temperature", (float) 0.01, currentID));
            Sensors.add(Temps.get(i));
            currentID++;
        }

        for(int i = 0; i < SFLib.getNumVolts(); i++) {
            Volts.add(i, new Sensor("Volt" + (i+1), "Voltage", (float) 0.01,currentID));
            Sensors.add(Volts.get(i));

            //Hardcoded Voltage labels =]
            if(SFLib.getNumVolts() == 9) {
                switch(i) {
                        case 0:
                            this.getSensorByID(currentID).setLabel("Vcore1");
                            break;
                        case 1:
                            this.getSensorByID(currentID).setLabel("Vcore2");
                            break;
                        case 2:
                            this.getSensorByID(currentID).setLabel("+3.3V");
                            break;
                        case 3:
                            this.getSensorByID(currentID).setLabel("+5V");
                            break;
                        case 4:
                            this.getSensorByID(currentID).setLabel("+12V");
                            break;
                        case 5:
                            this.getSensorByID(currentID).setLabel("-12V");
                            break;
                        case 6:
                            this.getSensorByID(currentID).setLabel("-5V");
                            break;
                        case 7:
                            this.getSensorByID(currentID).setLabel("+5V");
                            break;
                        case 8:
                            this.getSensorByID(currentID).setLabel("Vbat");
                            break;
                }

                currentID++;
            }

        }

        this.update();
    }

    public boolean running() {
        return (SFLib.getVersion() == 1);
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

    public synchronized void update() {
        if(this.running()) {
            for (int i = 0; i < SFLib.getNumFans(); i++) {
                Fans.get(i).setValue(SFLib.getFan(i));
            }
            for (int i = 0; i < SFLib.getNumTemps(); i++) {
                Temps.get(i).setValue(SFLib.getTemp(i));
            }
            for (int i = 0; i < SFLib.getNumVolts(); i++) {
                Volts.get(i).setValue(SFLib.getVolt(i));
            }
        }
    }
}