using AuroraEmu.Game.Clients;
using AuroraEmu.Utilities.Encoding;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System.Text;
using System;

namespace AuroraEmu.Network.Game
{
    public class GameNetworkHandler : ChannelHandlerAdapter
    {
        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            base.ChannelActive(ctx);

            Engine.MainDI.ClientController.AddClient(ctx.Channel);

            Engine.Logger.Debug($"Client connected to client: {ctx.Channel.RemoteAddress}");
        }

        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
            base.ChannelInactive(ctx);

            Engine.MainDI.ClientController.RemoveClient(ctx.Channel);

            Engine.Logger.Debug($"Client disconnected from client: {ctx.Channel.RemoteAddress}");
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        {
            Client client = Engine.MainDI.ClientController.GetClient(ctx.Channel);
            IByteBuffer message = msg as IByteBuffer;
            if (message.ReadByte() == 60)
            {
                string policy =
                    "<?xml version=\"1.0\"?>\r\n<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n<cross-domain-policy>\r\n   <allow-access-from domain=\"*\" to-ports=\"1-65535\" />\r\n</cross-domain-policy>\0";
                ctx.Channel.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.GetEncoding(0).GetBytes(policy))).Wait();
            }
            else
            {
                int length = Base64Encoding.DecodeInt32(message.ReadBytes(2).ToArray());
                IByteBuffer packet = message.ReadBytes(length);

                Engine.MainDI.PacketController.Handle(client, packet);
            }

            base.ChannelRead(ctx, msg);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) =>
            Engine.Logger.Error(exception.ToString());
    }
}