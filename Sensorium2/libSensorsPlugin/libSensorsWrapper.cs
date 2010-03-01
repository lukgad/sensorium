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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace libSensorsPlugin {
	sealed class LibSensorsWrapper {
// ReSharper disable InconsistentNaming
		private static class UnixImports {
			[StructLayoutAttribute(LayoutKind.Sequential)]
			public struct sensors_bus_id
			{

				/// short
				public short type;

				/// short
				public short nr;
			}

			[StructLayoutAttribute(LayoutKind.Sequential)]
			public struct sensors_chip_name
			{

				/// char*
				[MarshalAsAttribute(UnmanagedType.LPStr)]
				public string prefix;

				/// sensors_bus_id
				public sensors_bus_id bus;

				/// int
				public int addr;

				/// char*
				[MarshalAsAttribute(UnmanagedType.LPStr)]
				public string path;
			}

			[StructLayoutAttribute(LayoutKind.Sequential)]
			public struct sensors_feature_data
			{

				/// int
				public int number;

				/// char*
				[MarshalAsAttribute(UnmanagedType.LPStr)]
				public string name;

				/// int
				public int mapping;

				/// int
				public int unused;

				/// int
				public int mode;
			}

			[DllImport("libc.so", SetLastError = true)]
				public static extern IntPtr fopen(String filename, String mode);

				[DllImport("libc.so", SetLastError = true)]
				public static extern Int32 fclose(IntPtr file);

				/// Return Type: int
				///input: void*
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_init")]
				public static extern int sensors_init(IntPtr input);


				/// Return Type: void
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_cleanup")]
				public static extern void sensors_cleanup();


				/// Return Type: int
				///orig_name: char*
				///res: sensors_chip_name*
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_parse_chip_name")]
				public static extern int sensors_parse_chip_name(
					[InAttribute] [MarshalAsAttribute(UnmanagedType.LPStr)] string orig_name,
					ref sensors_chip_name res);


				/// Return Type: int
				///chip1: sensors_chip_name
				///chip2: sensors_chip_name
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_match_chip")]
				public static extern int sensors_match_chip(sensors_chip_name chip1, 
					sensors_chip_name chip2);


				/// Return Type: int
				///chip: sensors_chip_name
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_chip_name_has_wildcards")]
				public static extern int sensors_chip_name_has_wildcards(sensors_chip_name chip);


				/// Return Type: char*
				///bus_nr: int
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_get_adapter_name")]
				public static extern IntPtr sensors_get_adapter_name(int bus_nr);


				/// Return Type: char*
				///bus_nr: int
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_get_algorithm_name")]
				public static extern IntPtr sensors_get_algorithm_name(int bus_nr);


				/// Return Type: int
				///name: sensors_chip_name
				///feature: int
				///result: char**
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_get_label")]
				public static extern int sensors_get_label(sensors_chip_name name, int feature, 
					ref IntPtr result);


				/// Return Type: int
				///name: sensors_chip_name
				///feature: int
				///result: double*
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_get_feature")]
				public static extern int sensors_get_feature(sensors_chip_name name, int feature, 
					ref double result);


				/// Return Type: int
				///name: sensors_chip_name
				///feature: int
				///value: double
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_set_feature")]
				public static extern int sensors_set_feature(sensors_chip_name name, int feature, 
					double value);


				/// Return Type: int
				///name: sensors_chip_name
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_do_chip_sets")]
				public static extern int sensors_do_chip_sets(sensors_chip_name name);


				/// Return Type: int
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_do_all_sets")]
				public static extern int sensors_do_all_sets();


				/// Return Type: sensors_chip_name*
				///nr: int*
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_get_detected_chips")]
				public static extern IntPtr sensors_get_detected_chips(ref int nr);


				/// Return Type: sensors_feature_data*
				///name: sensors_chip_name
				///nr1: int*
				///nr2: int*
				[DllImportAttribute("libsensors.so", EntryPoint = "sensors_get_all_features")]
				public static extern IntPtr sensors_get_all_features(sensors_chip_name name, 
					ref int nr1, ref int nr2);
		}
// ReSharper restore InconsistentNaming

		private readonly Dictionary<UnixImports.sensors_chip_name, List<UnixImports.sensors_feature_data>> _chips =
				new Dictionary<UnixImports.sensors_chip_name, List<UnixImports.sensors_feature_data>>();

		public LibSensorsWrapper() {
			if (Environment.OSVersion.Platform != PlatformID.Unix)
				return;

			//Open config file
			IntPtr filePointer = UnixImports.fopen("/etc/sensors.conf", "r");

			//Init libsensors
			if (UnixImports.sensors_init(filePointer) != 0)
				throw new Exception();

			//Close config file
			UnixImports.fclose(filePointer);

			//Get all chip names and features
			int nr = 0;
			while(true) {
				//Get chip name
				IntPtr chipNamePtr = UnixImports.sensors_get_detected_chips(ref nr);

				if (chipNamePtr == IntPtr.Zero)
					break;

				UnixImports.sensors_chip_name chipName = new UnixImports.sensors_chip_name();

				Marshal.PtrToStructure(chipNamePtr, chipName);
				_chips.Add(chipName, new List<UnixImports.sensors_feature_data>());

				//Get all chip features
				int nr1 = 0, nr2 = 0;
				while (true) {
					IntPtr featurePointer = UnixImports.sensors_get_all_features(chipName, ref nr1, ref nr2);

					if (featurePointer == IntPtr.Zero)
						break;

					UnixImports.sensors_feature_data feature = new UnixImports.sensors_feature_data();

					Marshal.PtrToStructure(featurePointer, feature);
					_chips[chipName].Add(feature);
				}
			}
		}

		~LibSensorsWrapper() {
			if (Environment.OSVersion.Platform != PlatformID.Unix)
				return;

			UnixImports.sensors_cleanup();
		}
	}
}
