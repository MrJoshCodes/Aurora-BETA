using AuroraEmu.Game.Clients;
using AuroraEmu.Utilities.Encoding;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System.Text;
using System;
using AuroraEmu.Game.Players.Models;

namespace AuroraEmu.Network.Game
{
    public class GameNetworkHandler : ChannelHandlerAdapter
    {
        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            base.ChannelActive(ctx);

            Engine.Locator.ClientController.AddClient(ctx.Channel);

            Engine.Logger.Debug($"Client connected to client: {ctx.Channel.RemoteAddress}");
        }

        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
            base.ChannelInactive(ctx);

            using (Client client = Engine.Locator.ClientController.GetClient(ctx.Channel))
            {
                if (client.Player != null)
                {
                    Engine.FlashClients.Remove(client.IP);
                }

                client.Disconnect();
            }
            Engine.Locator.ClientController.RemoveClient(ctx.Channel);

            Engine.Logger.Debug($"Client disconnected from client: {ctx.Channel.RemoteAddress}");
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        {
            Client client = Engine.Locator.ClientController.GetClient(ctx.Channel);
            System.Console.WriteLine(client.IP);
            IByteBuffer message = msg as IByteBuffer;
            if (message.GetByte(0) == 60)
            {
                Engine.FlashClients.Add(client.IP);

                string policy =
                    "<?xml version=\"1.0\"?>\r\n<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n<cross-domain-policy>\r\n   <allow-access-from domain=\"*\" to-ports=\"1-65535\" />\r\n</cross-domain-policy>\0";
                ctx.Channel.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.GetEncoding(0).GetBytes(policy))).Wait();
            }
            else
            {
                while (message.ReadableBytes >= 5)
                {
                    int length = Base64Encoding.DecodeInt32(message.ReadBytes(3).ToArray());

                    if (length > 0)
                    {
                        IByteBuffer packet = message.ReadBytes(length);

                        Engine.Locator.PacketController.Handle(client, packet);
                    }
                }
            }

            base.ChannelRead(ctx, msg);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) =>
            Engine.Logger.Error(exception.ToString());
    }
}