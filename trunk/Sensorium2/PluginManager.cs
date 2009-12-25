using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Common;
using System.Collections;

namespace Sensorium2
{
	static class PluginManager
	{
		/// <summary>
		/// Gets all compatible plugins from a specified directory.
		/// </summary>
		/// <param name="pluginDirectory">Directory to search</param>
		/// <param name="recursive">Search recursively?</param>
		/// <returns>Collection containing instances of all compatible plugins</returns>
		public static List<IPluginInterface> GetPlugins(String pluginDirectory, bool recursive) {
			Console.WriteLine("Plugin Directory: " + pluginDirectory);

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
				try
				{
					Assembly pluginAssembly = Assembly.LoadFile(f.FullName);

					//Get all the relevant classes in the library
					foreach (Type t in pluginAssembly.GetTypes())
						if (!t.IsAbstract && !t.IsInterface &&
						    ((IList) t.GetInterfaces()).Contains(typeof (IPluginInterface)))
							//Add an instance of the plugin to the list
							plugins.Add((IPluginInterface) Activator.CreateInstance(t));
				} catch (BadImageFormatException e)
				{
					Console.WriteLine(e.Message);
					Console.WriteLine(f.FullName + " is not a valid .NET assembly");
				}
			}
			
			return plugins;
		}
	}
}
