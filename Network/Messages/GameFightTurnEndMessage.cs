namespace Dtwo.API.Hybride.Network.Messages
{
    //GTF<id>
    public class GameFightTurnEndMessage : HybrideMessage
    {
        public double Id { get; private set; }
        public override bool Build()
        {
            if (RetroMessage != null)
            {
                Build(RetroMessage as Retro.AnkamaGames.Dofus.Network.Messages.GameFightTurnEndMessage);
                return true;
            }
            else if (Dofus2Message != null)
            {
                Build(Dofus2Message as Dofus2.AnkamaGames.Network.Messages.GameFightTurnEndMessage);
                return true;
            }
            else
            {
                LogManager.LogError("Error on build GameFightTurnEndMessage", 1);
                return false;
                // error
            }
        }

        private void Build(Dofus2.AnkamaGames.Network.Messages.GameFightTurnEndMessage d2Message)
        {
            Id = d2Message.id;
        }

        private void Build(Retro.AnkamaGames.Dofus.Network.Messages.GameFightTurnEndMessage retroMessage)
        {
            Id = retroMessage.Id;
        }
    }
}
