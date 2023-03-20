namespace Dtwo.API.Hybride.Network.Messages
{
    //GTS<id>|time
    public class GameFightTurnStartMessage : HybrideMessage
    {
        public double Id { get; private set; }
        public uint RemainingTime { get; private set; }

        public override bool Build()
        {
            if (RetroMessage != null)
            {
                Build(RetroMessage as Retro.AnkamaGames.Dofus.Network.Messages.GameFightTurnStartMessage);
                return true;
            }
            else if (Dofus2Message != null)
            {
                Build(Dofus2Message as Dofus2.AnkamaGames.Network.Messages.GameFightTurnStartMessage);
                return true;
            }
            else
            {
                LogManager.LogError("Error on build GameFightTurnStartMessage", 1);
                return false;
                // error
            }
        }

        private void Build(Dofus2.AnkamaGames.Network.Messages.GameFightTurnStartMessage d2Message)
        {
            Id = d2Message.id;
            RemainingTime = d2Message.waitTime;
        }

        private void Build(Retro.AnkamaGames.Dofus.Network.Messages.GameFightTurnStartMessage retroMessage)
        {
            Id = retroMessage.Id;
            RemainingTime = retroMessage.RemainingTime;
        }
    }
}
