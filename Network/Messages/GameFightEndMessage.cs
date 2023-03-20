using Dtwo.API.Hybride.Network.Types;
namespace Dtwo.API.Hybride.Network.Messages
{
    public class GameFightEndMessage : HybrideMessage
    {
        public ReadOnlySpan<FightResultListEntry> FightResultEntries => m_fightResultEntries;
        private FightResultListEntry[] m_fightResultEntries;

        public override bool Build()
        {
            if (RetroMessage != null)
            {
                Build(RetroMessage as Retro.AnkamaGames.Dofus.Network.Messages.GameFightEndMessage);
                return true;
            }
            else if (Dofus2Message != null)
            {
                Build(Dofus2Message as Dofus2.AnkamaGames.Network.Messages.GameFightEndMessage);
                return true;
            }
            else
            {
                LogManager.LogError("Error on build GameFightEndMessage", 1);
                return false;
                // error
            }
        }

        private void Build(Dofus2.AnkamaGames.Network.Messages.GameFightEndMessage d2Message)
        {
            // todo
        }

        private void Build(Retro.AnkamaGames.Dofus.Network.Messages.GameFightEndMessage retroMessage)
        {
            // todo
        }
    }
}
