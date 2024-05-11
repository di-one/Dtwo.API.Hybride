using Dtwo.API.Hybride.Network.Types;

namespace Dtwo.API.Hybride.Network.Messages
{
    public class CharacterSelectedSuccessMessage : HybrideMessage
    {
        public CharacterInformations? CharacterInformations { get; private set; }
        
        public CharacterSelectedSuccessMessage(CharacterInformations characterInformations)
        {
            CharacterInformations = characterInformations;
        }

        public CharacterSelectedSuccessMessage()
        {

        }

        public override bool Build()
        {
            if (RetroMessage != null)
            {
                var message = RetroMessage as Retro.AnkamaGames.Dofus.Network.Messages.CharacterSelectedSuccessMessage;
                if (message == null)
                {
                    return false;
                }

                Build(message);
                return true;
            }
            else if (Dofus2Message != null)
            {
                var message = Dofus2Message as Dofus2.AnkamaGames.Network.Messages.CharacterSelectedSuccessMessage;
                if (message == null)
                {
                    return false;
                }

                Build(message);
                return true;
            }
            else
            {
                LogManager.LogError("Error on build CharacterSelectedSucces", 1);
                return false;
                // error
            }
        }
        
        private void Build(Dofus2.AnkamaGames.Network.Messages.CharacterSelectedSuccessMessage d2Message)
        {
            double id = d2Message.infos.id;
            string name = d2Message.infos.name;
            bool sex = d2Message.infos.sex;
            int breed = d2Message.infos.breed;
            int level = (int)d2Message.infos.level;

            CharacterInformations infos = new CharacterInformations(sex, breed, level, name, id);

            CharacterInformations = infos;
        }

        private void Build(Retro.AnkamaGames.Dofus.Network.Messages.CharacterSelectedSuccessMessage retroMessage)
        {
            double id = retroMessage.Infos.Id;
            string name = retroMessage.Infos.Name;
            bool sex = retroMessage.Infos.Sex;
            int breed = retroMessage.Infos.Breed;
            int level = (int)retroMessage.Infos.Level;

            CharacterInformations infos = new CharacterInformations(sex, breed, level, name, id);

            CharacterInformations = infos;
        }
    }
}
