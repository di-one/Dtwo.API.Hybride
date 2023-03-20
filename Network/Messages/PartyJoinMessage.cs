using Dtwo.API.Hybride.Network.Types;

namespace Dtwo.API.Hybride.Network.Messages
{
    // PM+<member>|<etc...>
    public class PartyJoinMessage : HybrideMessage
    {
        public ReadOnlySpan<PartyMemberInformations> Members => m_members;
        private PartyMemberInformations[] m_members;

        public override bool Build()
        {
            if (RetroMessage != null)
            {
                Build(RetroMessage as Retro.AnkamaGames.Dofus.Network.Messages.PartyJoinMessage);
                return true;
            }
            else if (Dofus2Message != null)
            {
                Build(Dofus2Message as Dofus2.AnkamaGames.Network.Messages.PartyJoinMessage);
                return true;
            }
            else
            {
                LogManager.LogError("Error on build PartyJoinMessage", 1);
                return false;
                // error
            }
        }

        private void Build(Dofus2.AnkamaGames.Network.Messages.PartyJoinMessage d2Message)
        {
            m_members = new PartyMemberInformations[d2Message.members.Length];

            for (int i  = 0; i < d2Message.members.Length; i++)
            {
                m_members[i] = PartyMemberInformations.Parse(d2Message.members[i]);
            }
        }

        private void Build(Retro.AnkamaGames.Dofus.Network.Messages.PartyJoinMessage retroMessage)
        {
            m_members = new PartyMemberInformations[retroMessage.Members.Length];

            for (int i = 0; i < retroMessage.Members.Length; i++)
            {
                m_members[i] = PartyMemberInformations.Parse(retroMessage.Members[i]);
            }
        }
    }
}
