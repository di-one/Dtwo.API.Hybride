namespace Dtwo.API.Hybride.Network.Messages
{
    //GS
    public class GameFightStartMessage : HybrideMessage
    {
        public override bool Build()
        {
            if (RetroMessage != null)
            {
                Build(RetroMessage as Retro.AnkamaGames.Dofus.Network.Messages.GameFightStartMessage);
                return true;
            }
            else if (Dofus2Message != null)
            {
                Build(Dofus2Message as Dofus2.AnkamaGames.Network.Messages.GameFightStartMessage);
                return true;
            }
            else
            {
                LogManager.LogError("Error on build GameFightStartMessage", 1);
                return false;
                // error
            }
        }

        private void Build(Dofus2.AnkamaGames.Network.Messages.GameFightStartMessage d2Message)
        {
            // todo
        }

        private void Build(Retro.AnkamaGames.Dofus.Network.Messages.GameFightStartMessage retroMessage)
        {
            // todo
        }
    }
}
