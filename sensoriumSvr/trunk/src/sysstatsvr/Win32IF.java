package sysstatsvr;

import com.sun.jna.*;

/**
 *
 * @author Aaron Maslen
 */
public interface Win32IF extends com.sun.jna.Library {
     
   public static Win32IF INSTANCE = (Win32IF) Native.loadLibrary("SpeedFanDll", Win32IF.class);

   // Optional: wraps every call to the native library in a
   // synchronized block, limiting native calls to one at a time
   //Win32IF SYNC_INSTANCE = (Win32IF) Native.synchronizedLibrary(INSTANCE);

   short getVersion();
   short getNumFans();
   short getNumTemps();
   short getNumVolts();
   int getTemp(int id);
   int getFan(int id);
   int getVolt(int id);
}

