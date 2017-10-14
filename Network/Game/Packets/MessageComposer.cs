using AuroraEmu.Utilities.Encoding;
using DotNetty.Buffers;
using System.Collections.Generic;
using System.Text;

namespace AuroraEmu.Network.Game.Packets
{
    public class MessageComposer
    {
        private List<byte> buffer;

        public MessageComposer(int header)
        {
            buffer = new List<byte>();
            buffer.AddRange(Base64Encoding.EncodeInt32(header));
        }

        public void AppendVL64(int value)
        {
            buffer.AddRange(WireEncoding.EncodeInt32(value));
        }

        public void AppendVL64(bool value)
        {
            AppendVL64(value ? 1 : 0);
        }

        public void AppendString(string value, byte breaker = 2)
        {
            buffer.AddRange(Encoding.GetEncoding(0).GetBytes(value));

            if (breaker > 0)
                buffer.Add(breaker);
        }

        public byte[] GetBytes()
        {
            List<byte> list = new List<byte>();
            list.AddRange(buffer);
            list.Add(1);

            return list.ToArray();
        }
    }
}
