package commonSensor;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */



import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;

/**
 *
 * @author Aaron Maslen
 */
public class misc {

    // Useful, miscellaneous functions.
    // Pretty self-explanatory

    public static byte[] toBytes (final int integer) throws IOException {
        ByteArrayOutputStream bos = new ByteArrayOutputStream();
        DataOutputStream dos = new DataOutputStream(bos);
        dos.writeInt(integer);
        dos.flush();
        return bos.toByteArray();
    }

    public static byte[] toBytes (final float floatVar) throws IOException {
        ByteArrayOutputStream bos = new ByteArrayOutputStream();
        DataOutputStream dos = new DataOutputStream(bos);
        dos.writeFloat(floatVar);
        dos.flush();
        return bos.toByteArray();
    }

    public static int bytesToInt(final byte[] bytes) throws IOException{
        ByteArrayInputStream bis = new ByteArrayInputStream(bytes);
        DataInputStream in = new DataInputStream(bis);
        return in.readInt();
    }

    public static float bytesToFloat(final byte[] bytes) throws IOException{
        ByteArrayInputStream bis = new ByteArrayInputStream(bytes);
        DataInputStream in = new DataInputStream(bis);
        return in.readFloat();
    }
}
