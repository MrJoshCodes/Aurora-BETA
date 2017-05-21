using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets;
using AuroraEmu.Utilities.Encoding;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System.Text;

namespace AuroraEmu.Network.Game
{
    public class GameNetworkHandler : ChannelHandlerAdapter
    {
        public GameNetworkHandler()
        {
        }

        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            base.ChannelActive(ctx);

            ClientManager.GetInstance().AddClient(ctx.Channel);

            Engine.Logger.Debug($"Client connected to client: {ctx.Channel.RemoteAddress.ToString()}");
        }

        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
            base.ChannelInactive(ctx);

            ClientManager.GetInstance().RemoveClient(ctx.Channel);

            Engine.Logger.Debug($"Client disconnected from client: {ctx.Channel.RemoteAddress.ToString()}");
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        {
            Client client = ClientManager.GetInstance().GetClient(ctx.Channel);
            var message = msg as IByteBuffer;

            if (message.ReadByte() == 60)
            {
                string policy = "<?xml version=\"1.0\"?>\r\n<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n<cross-domain-policy>\r\n   <allow-access-from domain=\"*\" to-ports=\"1-65535\" />\r\n</cross-domain-policy>\0";
                ctx.Channel.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.Default.GetBytes(policy))).Wait();
            }
            else
            {
                int length = Base64Encoding.DecodeInt32(message.ReadBytes(2).ToArray());
                IByteBuffer packet = message.ReadBytes(length);

                PacketHelper.GetInstance().Handle(client, packet);
            }

            base.ChannelRead(ctx, msg);
        }
    }
}
