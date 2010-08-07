using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using UDPPlugin;
using Sensorium.Core;

namespace Tests.UDPPlugin {
	using NUnit.Framework;
	using NUnit.Framework.SyntaxHelpers;

	[TestFixture]
	public class UDPPluginTests {
		private readonly AppData _testData = (AppData) SensoriumFactory.GetAppInterface();

		private IPAddress _serverIP;
		private int _port;
		private int _timeout;

		readonly Sensor _testSensor = new Sensor("TestName", "TestType", SensoriumFactory.GetAppInterface().HostId, "TestSource");

		private UDPPluginServer _testServer;
		private UDPPluginClient _testClient;

		[TestFixtureSetUp]
		public void Init() {
			_testSensor.SetData(Encoding.UTF8.GetBytes("TestData"));
			((AppData) SensoriumFactory.GetAppInterface()).Sensors = new List<Sensor> {_testSensor};
		}

		[Test]
		public void CorrectlyRetrieveTestSensor() {
			_serverIP = IPAddress.Parse("127.0.0.1");
			_port = 54321;
			_timeout = 1000;

            _testServer = new UDPPluginServer(_serverIP, _port, _timeout);
			_testClient = new UDPPluginClient("127.0.0.1", _port, _timeout);

			_testServer.Start();
			_testClient.Start();
			Thread.Sleep(_timeout * 2);

			Assert.That(_testClient.Sensors[0].Name, Is.EqualTo(_testSensor.Name), 
				"Recieved: " + _testClient.Sensors[0].Name + " Expected: " + _testSensor.Name);
			Assert.That(_testClient.Sensors[0].Type, Is.EqualTo(_testSensor.Type), 
				"Recieved: " + _testClient.Sensors[0].Name + " Expected: " + _testSensor.Name);
			Assert.That(_testClient.Sensors[0].HostId, Is.EqualTo(_testSensor.HostId), 
				"Recieved: " + _testClient.Sensors[0].Name + " Expected: " + _testSensor.Name);
			Assert.That(_testClient.Sensors[0].SourcePlugin, Is.EqualTo(_testSensor.SourcePlugin), 
				"Recieved: " + _testClient.Sensors[0].Name + " Expected: " + _testSensor.Name);
			Assert.That(_testClient.Sensors[0].Data, Is.EqualTo(_testSensor.Data), 
				"Recieved: " + _testClient.Sensors[0].Name + " Expected: " + _testSensor.Name);

			_testServer.Stop();
			_testClient.Stop();
		}

		[Test]
		public void CorrectlyHandlesSocketExceptions() {
			_port = 8765;
			_timeout = 1000;

			_testClient = new UDPPluginClient("123.231.132.213", _port, _timeout);

			_testClient.Start();
			Thread.Sleep(_timeout * 2);

			Assert.That(_testClient.Sensors, Is.Empty);

			_testClient.Stop();
		}
	}
}
