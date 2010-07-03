/*	This file is part of Sensorium2 <http://code.google.com/p/sensorium>
 * 
 *	Copyright (C) 2009-2010 Aaron Maslen
 *	This program is free software: you can redistribute it and/or modify it 
 *	under the terms of the GNU General Public License as published by 
 *	the Free Software Foundation, either version 3 of the License, or 
 *	(at your option) any later version. This program is distributed in the 
 *	hope that it will be useful, but WITHOUT ANY WARRANTY; without 
 *	even the implied warranty of MERCHANTABILITY or FITNESS FOR 
 *	A PARTICULAR PURPOSE. See the GNU General Public License 
 *	for more details. You should have received a copy of the GNU General 
 *	Public License along with this program. If not, see <http://www.gnu.org/licenses/>
*/
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SpeedFanPlugin
{
	static internal class SpeedFanWrapper
	{
		//private const int PROCESS_ALL_ACCESS = 0x1F0FFF; //Not needed
		// ReSharper disable InconsistentNaming
		private const int FILE_MAP_READ = 0x0004;
        
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

		//private static SpeedFanSharedMem sm;
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

		private static IntPtr _handle;
		private static IntPtr _mem;
		public static void OpenSharedMemory() {
			StringBuilder sharedMemFile = new StringBuilder("SFSharedMemory_ALM");
			_handle = OpenFileMapping(FILE_MAP_READ, false, sharedMemFile);
			_mem = MapViewOfFile(_handle, FILE_MAP_READ, 0, 0, Marshal.SizeOf(typeof(SpeedFanSharedMem)));
			if (_mem == IntPtr.Zero) {
				throw new NullReferenceException("Unable to read shared memory.");
			}

			//sm = (SpeedFanSharedMem)Marshal.PtrToStructure(_mem, typeof(SpeedFanSharedMem));
		}

		public static void CloseSharedMemory() {
			UnmapViewOfFile(_handle);
			CloseHandle(_handle);
		}

		public static ushort GetVersion() {
			return ((SpeedFanSharedMem)Marshal.PtrToStructure(_mem, typeof(SpeedFanSharedMem))).version;
		}
        public static ushort GetNumFans() {
			return ((SpeedFanSharedMem)Marshal.PtrToStructure(_mem, typeof(SpeedFanSharedMem))).numFans;
        }
        public static ushort GetNumTemps() {
			return ((SpeedFanSharedMem)Marshal.PtrToStructure(_mem, typeof(SpeedFanSharedMem))).numTemps;
        }
        public static ushort GetNumVolts() {
			return ((SpeedFanSharedMem)Marshal.PtrToStructure(_mem, typeof(SpeedFanSharedMem))).numVolts;
        }
        public static Int32 GetTemp(int id) {
			return ((SpeedFanSharedMem)Marshal.PtrToStructure(_mem, typeof(SpeedFanSharedMem))).temps[id];
        }
        public static Int32 GetFan(int id) {
			return ((SpeedFanSharedMem)Marshal.PtrToStructure(_mem, typeof(SpeedFanSharedMem))).fans[id];
        }
        public static Int32 GetVolt(int id){
			return ((SpeedFanSharedMem)Marshal.PtrToStructure(_mem, typeof(SpeedFanSharedMem))).volts[id];
        }
	}
}
