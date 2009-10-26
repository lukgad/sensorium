package commonSensor;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */



/**
 *
 * @author Aaron Maslen
 */
public class Sensor {
    private final int id;
    
    private int value;
    private String label;
    private String type;
    private float multiplier;

    public Sensor(int sensorID) {
        this("Unspecified", sensorID);
    }

    public Sensor(String sensorType, int sensorID) {
        this("", sensorType, sensorID);
    }

    public Sensor(String sensorLabel, String sensorType, int sensorID) {
        this(sensorLabel, sensorType, (float) 1.0, sensorID);
    }

    public Sensor(float sensorMult, int sensorID) {
        this("", "Unspecified", sensorMult, sensorID);
    }

    public Sensor(String sensorType, float sensorMult, int sensorID) {
        this("", sensorType, sensorMult, sensorID);
    }

    public Sensor(String sensorLabel, String sensorType, float sensorMult, int sensorID) {
        value = 0;
        label = sensorLabel;
        type = sensorType;
        multiplier = sensorMult;
        id = sensorID;
    }

    /**
     * @return the real value
     */
    public float getValue() {
        return value * multiplier;
    }
    
    /**
     * @return the raw value
     */

    public int getValueRaw() {
        return value;
    }

    /**
     * @param value the value to set
     */
    public void setValue(int value) {
        this.value = value;
    }

    /**
     * @return the label
     */
    public String getLabel() {
        return label;
    }

    /**
     * @param label the label to set
     */
    public void setLabel(String label) {
        this.label = label;
    }

    /**
     * @return the type
     */
    public String getType() {
        return type;
    }

    /**
     * @return the multiplier
     */
    public float getMultiplier() {
        return multiplier;
    }

    /**
     * @param multiplier the multiplier to set
     */
    public void setMultiplier(float multiplier) {
        this.multiplier = multiplier;
    }

    /**
     * @param type the type to set
     */
    public void setType(String type) {
        this.type = type;
    }

    /**
     * @return the id
     */
    public int getId() {
        return id;
    }

}
