using AuroraEmu.Utilities.Encoding;
using DotNetty.Buffers;
using System.Text;

namespace AuroraEmu.Network.Game.Packets
{
    public class MessageComposer
    {
        private IByteBuffer buffer;

        public MessageComposer(int header)
        {
            buffer = Unpooled.Buffer();
            buffer.WriteBytes(Base64Encoding.EncodeInt32(header));
        }

        public void AppendVL64(int value)
        {
            buffer.WriteBytes(WireEncoding.EncodeInt32(value));
        }

        public void AppendVL64(bool value)
        {
            AppendVL64(value ? 1 : 0);
        }

        public void AppendString(string value, byte breaker = 2)
        {
            buffer.WriteBytes(Encoding.Default.GetBytes(value));

            if (breaker > 0)
                buffer.WriteByte(breaker);
        }

        public IByteBuffer GetBytes()
        {
            IByteBuffer copy = buffer;
            copy.WriteByte(1);

            return copy;
        }
    }
}
