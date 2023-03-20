namespace Dtwo.API.Hybride.Network.Messages
{
    // GA
    public class GameActionMessage : HybrideMessage
    {
        public double SourceId { get; private set; }
        public override bool Build()
        {
            if (RetroMessage != null)
            {
                Build(RetroMessage as Retro.AnkamaGames.Dofus.Network.Messages.GameActionMessage);
                return true;
            }
            else if (Dofus2Message != null)
            {
                Build(Dofus2Message as Dofus2.AnkamaGames.Network.Messages.AbstractGameActionMessage);
                return true;
            }
            else
            {
                LogManager.LogError("Error on build GameActionMessage", 1);
                return false;
                // error
            }
        }

        private void Build(Dofus2.AnkamaGames.Network.Messages.AbstractGameActionMessage d2Message)
        {
            SourceId = d2Message.sourceId;
        }

        private void Build(Retro.AnkamaGames.Dofus.Network.Messages.GameActionMessage retroMessage)
        {
            SourceId = retroMessage.SourceId;
        }
    }
}
