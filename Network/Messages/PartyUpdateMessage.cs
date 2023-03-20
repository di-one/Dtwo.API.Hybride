using Dtwo.API.Hybride.Network.Types;

namespace Dtwo.API.Hybride.Network.Messages
{
    public class PartyUpdateMessage : HybrideMessage
    {
        public PartyMemberInformations MemberInformations { get; private set; }

        public override bool Build()
        {
            if (RetroMessage != null)
            {
                Build(RetroMessage as Retro.AnkamaGames.Dofus.Network.Messages.PartyUpdateMessage);
                return true;
            }
            else if (Dofus2Message != null)
            {
                Build(Dofus2Message as Dofus2.AnkamaGames.Network.Messages.PartyUpdateMessage);
                return true;
            }
            else
            {
                LogManager.LogError("Error on build PartyUpdateMessage", 1);
                return false;
                // error
            }
        }

        private void Build(Dofus2.AnkamaGames.Network.Messages.PartyUpdateMessage d2Message)
        {
            MemberInformations = PartyMemberInformations.Parse(d2Message.memberInformations);
        }

        private void Build(Retro.AnkamaGames.Dofus.Network.Messages.PartyUpdateMessage retroMessage)
        {
            MemberInformations = PartyMemberInformations.Parse(retroMessage.MemberInformations);
        }
    }
}
