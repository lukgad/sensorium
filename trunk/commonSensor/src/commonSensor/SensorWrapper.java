package commonSensor;



import java.util.List;

/**
 *
 * @author Aaron Maslen
 *
 * Generic Sensors class. Used as base class for WinSensors and LinuxSensors
 *
 * TODO: Implement LinuxSensorWrapper
 *
 */
public abstract class SensorWrapper {

    protected SensorList Sensors;

    public SensorWrapper() {
        
    }

    public abstract byte getNumSensors();

    public abstract byte getNumSensors(String type);

    public abstract Sensor getSensorByID(byte id);

    public abstract List<Sensor> getSensorsByLabel(String label);

    public abstract List<Sensor> getSensorsByType(String type);

    public abstract void update();
}

