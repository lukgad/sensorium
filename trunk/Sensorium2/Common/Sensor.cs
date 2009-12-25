namespace Common
{
	public class Sensor
	{
		public string Type { get; protected set; }
		public virtual byte[] Data { get; set; }
		public string Name { get; protected set; }
		public string Source { get; protected set; }
		public string SourcePlugin { get; protected set; }

		public Sensor(string name, string type, string source, string sourcePlugin) {
			Type = type;
			Name = name;
			Source = source;
			SourcePlugin = sourcePlugin;
		}
	}
}
