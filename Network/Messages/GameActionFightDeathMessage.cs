namespace Dtwo.API.Hybride.Network.Messages
{
    //GA;103
    public class GameActionFightDeathMessage : GameActionMessage
    {
        public double TargetId { get; private set; }
        public override bool Build()
        {
            if (base.Build() == false)
            {
                return false;
            }

            if (RetroMessage != null)
            {
                Build(RetroMessage as Retro.AnkamaGames.Dofus.Network.Messages.GameActionFightDeathMessage);
                return true;
            }
            else if (Dofus2Message != null)
            {
                Build(Dofus2Message as Dofus2.AnkamaGames.Network.Messages.GameActionFightDeathMessage);
                return true;
            }
            else
            {
                LogManager.LogError("Error on build GameActionFightDeathMessage", 1);
                return false;
                // error
            }
        }

        private void Build(Dofus2.AnkamaGames.Network.Messages.GameActionFightDeathMessage d2Message)
        {
            TargetId = d2Message.targetId;
        }

        private void Build(Retro.AnkamaGames.Dofus.Network.Messages.GameActionFightDeathMessage retroMessage)
        {
            TargetId = retroMessage.TargetId;
        }
    }
}
