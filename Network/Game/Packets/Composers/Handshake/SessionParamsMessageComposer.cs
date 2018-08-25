namespace AuroraEmu.Network.Game.Packets.Composers.Handshake
{
    public class SessionParamsMessageComposer : MessageComposer
    {
        public SessionParamsMessageComposer() : base(257)
        {
            AppendVL64(10);

            // conf_coppa
            AppendVL64(0);
            AppendVL64(false);

            // conf_voucher
            AppendVL64(1);
            AppendVL64(true);

            // conf_parent_email_request
            AppendVL64(2);
            AppendVL64(false);

            // conf_parent_email_request_reregistration
            AppendVL64(3);
            AppendVL64(false);

            // conf_allow_direct_mail
            AppendVL64(4);
            AppendVL64(false);

            // date
            AppendVL64(5);
            AppendString("dd-MM-yyyy", 2);

            // conf_partner_integration (TODO: Find out what this is)
            AppendVL64(6);
            AppendVL64(false);

            // allow_profile_editing (TODO: Find out what this does change)
            AppendVL64(7);
            AppendVL64(true);

            // tracking_header
            AppendVL64(8);
            AppendVL64(true);

            // tutorial_enabled
            AppendVL64(9);
            AppendVL64(true);
        }
    }
}
