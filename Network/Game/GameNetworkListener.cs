using AuroraEmu.DI.Network.Game;
using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Threading.Tasks;

namespace AuroraEmu.Network.Game
{
    public class GameNetworkListener : IGameNetworkListener
    {
        public async Task RunServer()
        {
            try
            {
                ServerBootstrap bootstrap = new ServerBootstrap()
                    .Group(new MultithreadEventLoopGroup(1), new MultithreadEventLoopGroup(10))
                    .Channel<TcpServerSocketChannel>()
                    .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                        channel.Pipeline.AddLast("ClientHandler", new GameNetworkHandler())
                    ))
                    .ChildOption(ChannelOption.TcpNodelay, true)
                    .ChildOption(ChannelOption.SoKeepalive, true)
                    .ChildOption(ChannelOption.SoReuseaddr, true)
                    .ChildOption(ChannelOption.SoRcvbuf, 1024)
                    .ChildOption(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(1024))
                    .ChildOption(ChannelOption.Allocator, PooledByteBufferAllocator.Default);
                await bootstrap.BindAsync(30000);
                Engine.Logger.Info($"Server is now listening on port: {30000}!");
            }
            catch (Exception e)
            {
                Engine.Logger.Error($"Failed to setup network listener... {e}");
            }
        }
    }
}
