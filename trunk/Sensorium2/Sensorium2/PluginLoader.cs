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
using System.Reflection;
using System.IO;
using System.Collections;
using Sensorium.Common.Plugins;

namespace Sensorium2
{
	static class PluginLoader {
		/// <summary>
		/// Gets all compatible plugins from a specified directory.
		/// </summary>
		/// <param name="pluginDirectory">Directory to search</param>
		/// <param name="recursive">Search recursively?</param>
		/// <returns>Collection containing instances of all compatible plugins</returns>
		public static List<IPluginInterface> GetPlugins(String pluginDirectory, bool recursive) {
			Console.WriteLine("Plugin Directory: " + pluginDirectory);
			Console.WriteLine();

			List<IPluginInterface> plugins = new List<IPluginInterface>();

			if (!Directory.Exists(pluginDirectory))
				throw new DirectoryNotFoundException("Plugin directory does not exist");

			DirectoryInfo dir = new DirectoryInfo(pluginDirectory);

			if (recursive) {
				foreach (DirectoryInfo d in dir.GetDirectories()) {
					//Ignore .. and .
					if (d.Name == ".." || d.Name == ".")
						continue;

					//Add all plugins for this directory
					foreach (IPluginInterface p in GetPlugins(d.FullName, true))
						plugins.Add(p);
				}
			}
			
			foreach (FileInfo f in dir.GetFiles("*.dll")) {
				try {
					Assembly pluginAssembly = Assembly.LoadFile(f.FullName);

					//Get all the relevant classes in the library
					foreach (Type t in pluginAssembly.GetTypes())
						if (!t.IsAbstract && !t.IsInterface &&
						    ((IList) t.GetInterfaces()).Contains(typeof (IPluginInterface)))
							//Add an instance of the plugin to the list
							plugins.Add((IPluginInterface) Activator.CreateInstance(t));
				} catch (BadImageFormatException e) {
					Console.WriteLine(e.Message);
					Console.WriteLine(f.FullName + " is not a valid .NET assembly");
				}
			}
			
			return plugins;
		}
	}
}
