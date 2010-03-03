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
using System.IO;
using System.Runtime.InteropServices;
using log4net;
using Sensorium.Common;

namespace libSensorsPlugin {
	sealed class LibSensorsWrapper {
		#region Imports
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
		private const string libc = "libc.so.6";
		private const string libsensors = "libsensors.so.4";
		private const string libsensors_conf = "libsensors.conf.3";
		private static partial class NativeConstants
		{

			/// SENSORS_MODE_R -> 1
			public const int SENSORS_MODE_R = 1;

			/// SENSORS_MODE_W -> 2
			public const int SENSORS_MODE_W = 2;

			/// SENSORS_COMPUTE_MAPPING -> 4
			public const int SENSORS_COMPUTE_MAPPING = 4;
		}

		[StructLayoutAttribute(LayoutKind.Sequential)]
		private struct sensors_bus_id
		{

			/// short
			public short type;

			/// short
			public short nr;
		}

		[StructLayoutAttribute(LayoutKind.Sequential)]
		private struct sensors_chip_name
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

		private enum sensors_feature_type
		{

			/// SENSORS_FEATURE_IN -> 0x00
			SENSORS_FEATURE_IN = 0,

			/// SENSORS_FEATURE_FAN -> 0x01
			SENSORS_FEATURE_FAN = 1,

			/// SENSORS_FEATURE_TEMP -> 0x02
			SENSORS_FEATURE_TEMP = 2,

			/// SENSORS_FEATURE_POWER -> 0x03
			SENSORS_FEATURE_POWER = 3,

			/// SENSORS_FEATURE_ENERGY -> 0x04
			SENSORS_FEATURE_ENERGY = 4,

			/// SENSORS_FEATURE_CURR -> 0x05
			SENSORS_FEATURE_CURR = 5,

			/// SENSORS_FEATURE_VID -> 0x10
			SENSORS_FEATURE_VID = 16,

			/// SENSORS_FEATURE_BEEP_ENABLE -> 0x18
			SENSORS_FEATURE_BEEP_ENABLE = 24,

			/// SENSORS_FEATURE_UNKNOWN -> 2147483647
			SENSORS_FEATURE_UNKNOWN = 2147483647,
		}

		private enum sensors_subfeature_type
		{

			/// SENSORS_SUBFEATURE_IN_INPUT -> SENSORS_FEATURE_IN<<8
			SENSORS_SUBFEATURE_IN_INPUT = (sensors_feature_type.SENSORS_FEATURE_IN) << (8),

			SENSORS_SUBFEATURE_IN_MIN,

			SENSORS_SUBFEATURE_IN_MAX,

			/// SENSORS_SUBFEATURE_IN_ALARM -> (SENSORS_FEATURE_IN<<8)|0x80
			SENSORS_SUBFEATURE_IN_ALARM = ((sensors_feature_type.SENSORS_FEATURE_IN) << (8) | 128),

			SENSORS_SUBFEATURE_IN_MIN_ALARM,

			SENSORS_SUBFEATURE_IN_MAX_ALARM,

			SENSORS_SUBFEATURE_IN_BEEP,

			/// SENSORS_SUBFEATURE_FAN_INPUT -> SENSORS_FEATURE_FAN<<8
			SENSORS_SUBFEATURE_FAN_INPUT = (sensors_feature_type.SENSORS_FEATURE_FAN) << (8),

			SENSORS_SUBFEATURE_FAN_MIN,

			/// SENSORS_SUBFEATURE_FAN_ALARM -> (SENSORS_FEATURE_FAN<<8)|0x80
			SENSORS_SUBFEATURE_FAN_ALARM = ((sensors_feature_type.SENSORS_FEATURE_FAN) << (8) | 128),

			SENSORS_SUBFEATURE_FAN_FAULT,

			SENSORS_SUBFEATURE_FAN_DIV,

			SENSORS_SUBFEATURE_FAN_BEEP,

			/// SENSORS_SUBFEATURE_TEMP_INPUT -> SENSORS_FEATURE_TEMP<<8
			SENSORS_SUBFEATURE_TEMP_INPUT = (sensors_feature_type.SENSORS_FEATURE_TEMP) << (8),

			SENSORS_SUBFEATURE_TEMP_MAX,

			SENSORS_SUBFEATURE_TEMP_MAX_HYST,

			SENSORS_SUBFEATURE_TEMP_MIN,

			SENSORS_SUBFEATURE_TEMP_CRIT,

			SENSORS_SUBFEATURE_TEMP_CRIT_HYST,

			/// SENSORS_SUBFEATURE_TEMP_ALARM -> (SENSORS_FEATURE_TEMP<<8)|0x80
			SENSORS_SUBFEATURE_TEMP_ALARM = ((sensors_feature_type.SENSORS_FEATURE_TEMP) << (8) | 128),

			SENSORS_SUBFEATURE_TEMP_MAX_ALARM,

			SENSORS_SUBFEATURE_TEMP_MIN_ALARM,

			SENSORS_SUBFEATURE_TEMP_CRIT_ALARM,

			SENSORS_SUBFEATURE_TEMP_FAULT,

			SENSORS_SUBFEATURE_TEMP_TYPE,

			SENSORS_SUBFEATURE_TEMP_OFFSET,

			SENSORS_SUBFEATURE_TEMP_BEEP,

			/// SENSORS_SUBFEATURE_POWER_AVERAGE -> SENSORS_FEATURE_POWER<<8
			SENSORS_SUBFEATURE_POWER_AVERAGE = (sensors_feature_type.SENSORS_FEATURE_POWER) << (8),

			SENSORS_SUBFEATURE_POWER_AVERAGE_HIGHEST,

			SENSORS_SUBFEATURE_POWER_AVERAGE_LOWEST,

			SENSORS_SUBFEATURE_POWER_INPUT,

			SENSORS_SUBFEATURE_POWER_INPUT_HIGHEST,

			SENSORS_SUBFEATURE_POWER_INPUT_LOWEST,

			/// SENSORS_SUBFEATURE_POWER_AVERAGE_INTERVAL -> (SENSORS_FEATURE_POWER<<8)|0x80
			SENSORS_SUBFEATURE_POWER_AVERAGE_INTERVAL = ((sensors_feature_type.SENSORS_FEATURE_POWER) << (8) | 128),

			/// SENSORS_SUBFEATURE_ENERGY_INPUT -> SENSORS_FEATURE_ENERGY<<8
			SENSORS_SUBFEATURE_ENERGY_INPUT = (sensors_feature_type.SENSORS_FEATURE_ENERGY) << (8),

			/// SENSORS_SUBFEATURE_CURR_INPUT -> SENSORS_FEATURE_CURR<<8
			SENSORS_SUBFEATURE_CURR_INPUT = (sensors_feature_type.SENSORS_FEATURE_CURR) << (8),

			SENSORS_SUBFEATURE_CURR_MIN,

			SENSORS_SUBFEATURE_CURR_MAX,

			/// SENSORS_SUBFEATURE_CURR_ALARM -> (SENSORS_FEATURE_CURR<<8)|0x80
			SENSORS_SUBFEATURE_CURR_ALARM = ((sensors_feature_type.SENSORS_FEATURE_CURR) << (8) | 128),

			SENSORS_SUBFEATURE_CURR_MIN_ALARM,

			SENSORS_SUBFEATURE_CURR_MAX_ALARM,

			SENSORS_SUBFEATURE_CURR_BEEP,

			/// SENSORS_SUBFEATURE_VID -> SENSORS_FEATURE_VID<<8
			SENSORS_SUBFEATURE_VID = (sensors_feature_type.SENSORS_FEATURE_VID) << (8),

			/// SENSORS_SUBFEATURE_BEEP_ENABLE -> SENSORS_FEATURE_BEEP_ENABLE<<8
			SENSORS_SUBFEATURE_BEEP_ENABLE = (sensors_feature_type.SENSORS_FEATURE_BEEP_ENABLE) << (8),

			/// SENSORS_SUBFEATURE_UNKNOWN -> 2147483647
			SENSORS_SUBFEATURE_UNKNOWN = 2147483647,
		}

		[StructLayoutAttribute(LayoutKind.Sequential)]
		private struct sensors_feature
		{

			/// char*
			[MarshalAsAttribute(UnmanagedType.LPStr)]
			public string name;

			/// int
			public int number;

			/// sensors_feature_type
			public sensors_feature_type type;

			/// int
			public int first_subfeature;

			/// int
			public int padding1;
		}

		[StructLayoutAttribute(LayoutKind.Sequential)]
		private struct sensors_subfeature
		{

			/// char*
			[MarshalAsAttribute(UnmanagedType.LPStr)]
			public string name;

			/// int
			public int number;

			/// sensors_subfeature_type
			public sensors_subfeature_type type;

			/// int
			public int mapping;

			/// unsigned int
			public uint flags;
		}

		private static partial class NativeMethods
		{

			/// Return Type: int
			///input: void*
			[DllImportAttribute(libsensors, EntryPoint = "sensors_init")]
			public static extern int sensors_init(IntPtr input);


			/// Return Type: void
			[DllImportAttribute(libsensors, EntryPoint = "sensors_cleanup")]
			public static extern void sensors_cleanup();


			/// Return Type: int
			///orig_name: char*
			///res: sensors_chip_name*
			[DllImportAttribute(libsensors, EntryPoint = "sensors_parse_chip_name")]
			public static extern int sensors_parse_chip_name(
				[InAttribute] [MarshalAsAttribute(UnmanagedType.LPStr)] string orig_name, 
				IntPtr res);


			/// Return Type: void
			///chip: sensors_chip_name*
			[DllImportAttribute(libsensors, EntryPoint = "sensors_free_chip_name")]
			public static extern void sensors_free_chip_name(IntPtr chip);


			/// Return Type: int
			///str: char*
			///size: size_t->unsigned int
			///chip: sensors_chip_name*
			[DllImportAttribute(libsensors, EntryPoint = "sensors_snprintf_chip_name")]
			public static extern int sensors_snprintf_chip_name(
				[InAttribute] [MarshalAsAttribute(UnmanagedType.LPStr)] string str, 
				[MarshalAsAttribute(UnmanagedType.SysUInt)] uint size, 
				IntPtr chip);


			/// Return Type: char*
			///bus: sensors_bus_id*
			[DllImportAttribute(libsensors, EntryPoint = "sensors_get_adapter_name")]
			[return: MarshalAs(UnmanagedType.LPStr)]
			public static extern string sensors_get_adapter_name(IntPtr bus);


			/// Return Type: char*
			///name: sensors_chip_name*
			///feature: sensors_feature*
			[DllImportAttribute(libsensors, EntryPoint = "sensors_get_label")]
			[return: MarshalAs(UnmanagedType.LPStr)]
			public static extern string sensors_get_label(IntPtr name, 
				IntPtr feature);


			/// Return Type: int
			///name: sensors_chip_name*
			///subfeat_nr: int
			///value: double*
			[DllImportAttribute(libsensors, EntryPoint = "sensors_get_value")]
			public static extern int sensors_get_value(IntPtr name, 
				int subfeat_nr, ref double value);


			/// Return Type: int
			///name: sensors_chip_name*
			///subfeat_nr: int
			///value: double
			[DllImportAttribute(libsensors, EntryPoint = "sensors_set_value")]
			public static extern int sensors_set_value(IntPtr name, 
				int subfeat_nr, double value);


			/// Return Type: int
			///name: sensors_chip_name*
			[DllImportAttribute(libsensors, EntryPoint = "sensors_do_chip_sets")]
			public static extern int sensors_do_chip_sets(IntPtr name);


			/// Return Type: sensors_chip_name*
			///match: sensors_chip_name*
			///nr: int*
			[DllImportAttribute(libsensors, EntryPoint = "sensors_get_detected_chips")]
			public static extern IntPtr sensors_get_detected_chips(IntPtr match, 
				ref int nr);


			/// Return Type: sensors_feature*
			///name: sensors_chip_name*
			///nr: int*
			[DllImportAttribute(libsensors, EntryPoint = "sensors_get_features")]
			public static extern IntPtr sensors_get_features(IntPtr name, 
				ref int nr);


			/// Return Type: sensors_subfeature*
			///name: sensors_chip_name*
			///feature: sensors_feature*
			///nr: int*
			[DllImportAttribute(libsensors, EntryPoint = "sensors_get_all_subfeatures")]
			public static extern IntPtr sensors_get_all_subfeatures(IntPtr name, 
				IntPtr feature, ref int nr);


			/// Return Type: sensors_subfeature*
			///name: sensors_chip_name*
			///feature: sensors_feature*
			///type: sensors_subfeature_type
			[DllImportAttribute(libsensors, EntryPoint = "sensors_get_subfeature")]
			public static extern IntPtr sensors_get_subfeature(IntPtr name, 
				IntPtr feature, sensors_subfeature_type type);

		}

		private static partial class NativeMethods {
			[DllImport(libc)]
			public static extern IntPtr fopen(String filename, String mode);
			
			[DllImport(libc)]
			public static extern Int32 fclose(IntPtr file);
		}
// ReSharper restore UnusedMember.Local
// ReSharper restore InconsistentNaming
		#endregion
		private readonly ILog _log = LogManager.GetLogger(typeof (LibSensorsWrapper));

		private readonly TreeNode<IntPtr> _chips = new TreeNode<IntPtr>();

		public LibSensorsWrapper() {
			if(Environment.OSVersion.Platform != PlatformID.Unix)
				return;

			if(NativeMethods.sensors_init(NativeMethods.fopen(libsensors_conf, "r")) != 0)
				throw new InvalidDataException();
			
			//Incredibly messy process of getting all chips, features and subfeatures
			int cnr = 0;
			IntPtr chipNamePtr;
            while((chipNamePtr = NativeMethods.sensors_get_detected_chips(IntPtr.Zero, ref cnr)) != IntPtr.Zero) {
				//Add a new chip (pointer)
				TreeNode<IntPtr> chipNameNode = new LibSensorsTreeNode(chipNamePtr, NodeType.Chip);
                _chips.Add(chipNameNode);

            	int fnr = 0;
            	IntPtr mainFeaturePtr;
				while((mainFeaturePtr = NativeMethods.sensors_get_features(chipNamePtr, ref fnr)) != IntPtr.Zero) {
					//Add main feature (pointer)
					TreeNode<IntPtr> mainFeatureNode = new LibSensorsTreeNode(mainFeaturePtr, NodeType.Feature);
					chipNameNode.Add(mainFeatureNode);

					int sfnr = 0;
					IntPtr subFeaturePtr;
					while ((subFeaturePtr = NativeMethods.sensors_get_all_subfeatures(chipNamePtr, mainFeaturePtr,
						ref sfnr)) != IntPtr.Zero) {
						//Add subfeature (pointer)
							mainFeatureNode.Add(new LibSensorsTreeNode(subFeaturePtr, NodeType.SubFeature));
					}
				}
			}

			//Print collected data
			foreach (TreeNode<IntPtr> cn in _chips.Children) {
				_log.Debug("Detected chip: " + ((sensors_chip_name) 
					Marshal.PtrToStructure(cn.Contents, typeof (sensors_chip_name))).prefix);
				foreach (TreeNode<IntPtr> f in cn.Children) {
					_log.Debug("Feature: " + ((sensors_feature) 
						Marshal.PtrToStructure(f.Contents, typeof (sensors_feature))).name);
					foreach (TreeNode<IntPtr> sf in f.Children) {
						double value = 0;
						if (NativeMethods.sensors_get_value(cn.Contents, ((sensors_subfeature)
							Marshal.PtrToStructure(sf.Contents, typeof (sensors_subfeature))).number,ref value) != 0)
							_log.Debug("Error retrieving value for...");

							_log.Debug("Subfeature: " + ((sensors_subfeature) 
										Marshal.PtrToStructure(sf.Contents, typeof (sensors_subfeature))).name + 
										" " + value);
					}
				}
			}
		}
	}
}
