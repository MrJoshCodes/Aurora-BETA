using AuroraEmu.Network.Game.Packets;
using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;

namespace AuroraEmu.Network.Game
{
    public class GameNetworkListener
    {
        private static GameNetworkListener gameNetworkListenerInstance;
        private readonly ServerBootstrap bootstrap;
        private int port;

        public GameNetworkListener()
        {
            try
            {
                port = 30000;
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
                bootstrap.BindAsync(port);
                Engine.Logger.Info($"Server is now listening on port: {port}!");
            } catch (Exception e)
            {
                Engine.Logger.Error($"Failed to setup network listener... {e}");
            }
        }

        public static GameNetworkListener GetInstance()
        {
            if (gameNetworkListenerInstance == null)
                gameNetworkListenerInstance = new GameNetworkListener();
            return gameNetworkListenerInstance;
        }
    }
}
