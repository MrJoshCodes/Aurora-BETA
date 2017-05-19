using AuroraEmu.Network.Game.Packets;
using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace AuroraEmu.Network.Game
{
    public class GameNetworkListener
    {
        public PacketHelper Packets { get; private set; }

        private readonly ServerBootstrap bootstrap;

        public GameNetworkListener()
        {
            bootstrap = new ServerBootstrap()
                .Group(new MultithreadEventLoopGroup(1), new MultithreadEventLoopGroup(10))
                .Channel<TcpServerSocketChannel>()
                .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    channel.Pipeline.AddLast("ClientHandler", new GameNetworkHandler());
                }))
                .ChildOption(ChannelOption.TcpNodelay, true)
                .ChildOption(ChannelOption.SoKeepalive, true)
                .ChildOption(ChannelOption.SoReuseaddr, true)
                .ChildOption(ChannelOption.SoRcvbuf, 1024)
                .ChildOption(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(1024))
                .ChildOption(ChannelOption.Allocator, new PooledByteBufferAllocator());
            bootstrap.BindAsync(30000);

            Packets = new PacketHelper();
        }
    }
}
