using AuroraEmu.Utilities.Encoding;
using DotNetty.Buffers;
using System.Text;

namespace AuroraEmu.Network.Game.Packets
{
    public class MessageEvent
    {
        private IByteBuffer buffer;

        public int HeaderId { get; private set; }

        public MessageEvent(IByteBuffer buffer)
        {
            this.buffer = buffer;
            HeaderId = ReadB64();
        }

        private byte[] GetBytes(int length)
        {
            byte[] dest = new byte[length];
            buffer.GetBytes(buffer.ReaderIndex, dest);

            return dest;
        }

        private byte[] ReadBytes(int length)
        {
            byte[] dest = new byte[length];
            buffer.ReadBytes(dest);

            return dest;
        }

        public int ReadB64()
        {
            return Base64Encoding.DecodeInt32(ReadBytes(2));
        }

        public int ReadVL64()
        {
            int value = WireEncoding.DecodeInt32(GetBytes(buffer.ReadableBytes), out int totalBytes);

            ReadBytes(totalBytes);

            return value;
        }

        public string ReadString()
        {
            return Encoding.Default.GetString(ReadBytes(ReadB64()));
        }

        public override string ToString()
        {
            return Encoding.Default.GetString(buffer.ToArray());
        }
    }
}
