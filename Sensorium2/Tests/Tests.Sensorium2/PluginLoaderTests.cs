using System;
using System.IO;
using Sensorium2;

namespace Tests.Sensorium2 {
	using NUnit.Framework;

	[TestFixture]
	class PluginLoaderTests {
		
		[Test]
		public void ThrowsExceptionOnNonexistantDirectory() {
			//Nonexistent directory. Ensure this dir does not exist
			const string pluginDirectory = "Nonexistent";

			Console.WriteLine((new DirectoryInfo(pluginDirectory)).FullName);

			Assert.Throws<DirectoryNotFoundException>(delegate { PluginLoader.GetPlugins(pluginDirectory, false); });
		}

		[Test]
		public void DoesNotThrowExceptionOnInvalidDLL() {
			//Invalid DLL in this directory. Ensure this dir exists
			const string pathToInvalidDLL = ".";

			Console.WriteLine((new DirectoryInfo(pathToInvalidDLL)).FullName);

			Assert.DoesNotThrow(delegate { PluginLoader.GetPlugins(pathToInvalidDLL, false); });
		}
	}
}
