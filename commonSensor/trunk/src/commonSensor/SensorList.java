package commonSensor;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */



import java.util.ArrayList;

/**
 *
 * @author Aaron
 */
public class SensorList extends ArrayList<Sensor> {

    @Override
    public boolean add(Sensor sensor) {
        for(Sensor s : this) {
            if(s.getId() == sensor.getId())
                return false;
        }
        this.add(sensor.getId(), sensor);
        return true;
    }

    @Override
    public void add(int index, Sensor sensor) {
        for(Sensor s : this) {
            if(s.getId() == sensor.getId())
                return;
        }
        super.add(index, sensor);
    }
}
