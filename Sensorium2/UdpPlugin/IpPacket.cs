using System.Net.Sockets;

namespace UdpPlugin {
	class IpPacket {
		public IPPacketInformation PacketInfo { get; private set; }
        public byte[] Data { get; private set; }

		public IpPacket(IPPacketInformation packetInfo, byte[] data) {
			Data = data;
			PacketInfo = packetInfo;
		}
	}
}
