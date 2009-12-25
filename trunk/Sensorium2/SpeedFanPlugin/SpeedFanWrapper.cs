using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SpeedFanPlugin
{
	static class SpeedFanWrapper
	{
		//private const int PROCESS_ALL_ACCESS = 0x1F0FFF; //Not needed
		private const int FILE_MAP_READ = 0x0004;

		// ReSharper disable InconsistentNaming
		// ReSharper disable FieldCanBeMadeReadOnly.Local
#pragma warning disable 169
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private class SpeedFanSharedMem
		{

			public ushort version;

			ushort flags;
			Int32 size;
			Int32 handle;
			public ushort numTemps;
			public ushort numFans;
			public ushort numVolts;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public Int32[] temps;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public Int32[] fans;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public Int32[] volts;

			public SpeedFanSharedMem() {
				version = 0;
				numFans = 0;
				numVolts = 0;
				numTemps = 0;
				temps = new Int32[32];
				fans = new Int32[32];
				volts = new Int32[32];
			}
		}

		private static SpeedFanSharedMem sm;
#pragma warning restore 169
		// ReSharper restore FieldCanBeMadeReadOnly.Local
		// ReSharper restore InconsistentNaming

		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr OpenFileMapping(int dwDesiredAccess,
				bool bInheritHandle, StringBuilder lpName);

		[DllImport("Kernel32.dll")]
		private static extern IntPtr MapViewOfFile(IntPtr hFileMapping,
				int dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow,
				int dwNumberOfBytesToMap);

		[DllImport("Kernel32.dll")]
		private static extern bool UnmapViewOfFile(IntPtr map);

		[DllImport("kernel32.dll")]
		private static extern bool CloseHandle(IntPtr hObject);


		public static void OpenSharedMemory() {
			StringBuilder sharedMemFile = new StringBuilder("SFSharedMemory_ALM");
			IntPtr handle = OpenFileMapping(FILE_MAP_READ, false, sharedMemFile);
			IntPtr mem = MapViewOfFile(handle, FILE_MAP_READ, 0, 0, Marshal.SizeOf(typeof(SpeedFanSharedMem)));
			if (mem == IntPtr.Zero) {
				throw new NullReferenceException("Unable to read shared memory.");
			}

			sm = (SpeedFanSharedMem)Marshal.PtrToStructure(mem, typeof(SpeedFanSharedMem));
			UnmapViewOfFile(handle);
			CloseHandle(handle);
		}

		public static ushort GetVersion() {
			return sm.version;
		}
        public static ushort GetNumFans() {
			return sm.numFans;
        }
        public static ushort GetNumTemps() {
        	return sm.numTemps;
        }
        public static ushort GetNumVolts() {
			return sm.numVolts;
        }
        public static Int32 GetTemp(int id) {
			return sm.temps[id];
        }
        public static Int32 GetFan(int id) {
        	return sm.fans[id];
        }
        public static Int32 GetVolt(int id){
        	return sm.volts[id];
        }
	}
}
