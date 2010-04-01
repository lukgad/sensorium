using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using UDPPlugin;
using Sensorium.Common;

namespace Tests.UDPPlugin {
	using NUnit.Framework;
	using NUnit.Framework.SyntaxHelpers;

	[TestFixture]
	public class UDPPluginTests {
		private readonly AppData _testData = (AppData) SensoriumFactory.GetAppInterface();
		
        private static readonly IPAddress ServerIP = IPAddress.Parse("127.0.0.1");
		private const int Port = 54321;
		private const int Timeout = 1000;

		private static readonly UDPPluginServer TestServer = new UDPPluginServer(ServerIP, Port, Timeout);
		private static readonly UDPPluginClient TestClient = new UDPPluginClient("127.0.0.1", Port, Timeout);

		private static Sensor _testSensor;

		public UDPPluginTests() {
			_testSensor = new Sensor("TestName", "TestType", _testData.HostId, "TestPlugin");
			_testData.Sensors = new List<Sensor> {_testSensor};
			_testData.Sensors[0].SetData(Encoding.UTF8.GetBytes("TestData"));
		}

		[SetUp]
		public void Init() {
			TestServer.Start();
			TestClient.Start();
			
			Thread.Sleep(Timeout * 2);
		}

		[TearDown]
		public void Dispose() {
			TestServer.Stop();
			TestClient.Stop();
		}

		[Test]
		public void CorrectlyRetrieveTestSensor() {
			Assert.That(TestClient.Sensors[0].Name, Is.EqualTo(_testSensor.Name), 
				"Recieved: " + TestClient.Sensors[0].Name + " Expected: " + _testSensor.Name);
			Assert.That(TestClient.Sensors[0].Type, Is.EqualTo(_testSensor.Type), 
				"Recieved: " + TestClient.Sensors[0].Name + " Expected: " + _testSensor.Name);
			Assert.That(TestClient.Sensors[0].HostId, Is.EqualTo(_testSensor.HostId), 
				"Recieved: " + TestClient.Sensors[0].Name + " Expected: " + _testSensor.Name);
			Assert.That(TestClient.Sensors[0].SourcePlugin, Is.EqualTo(_testSensor.SourcePlugin), 
				"Recieved: " + TestClient.Sensors[0].Name + " Expected: " + _testSensor.Name);
			Assert.That(TestClient.Sensors[0].Data, Is.EqualTo(_testSensor.Data), 
				"Recieved: " + TestClient.Sensors[0].Name + " Expected: " + _testSensor.Name);
		}
	}
}
