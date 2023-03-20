using Dtwo.API.Dofus2.Network.Messages;
using Dtwo.API.DofusBase.Network.Messages;
using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Hybride.Network.Messages
{
    public abstract class HybrideMessage : DofusMessage
    {
        public RetroMessage? RetroMessage { get; private set; }
        public Dofus2Message? Dofus2Message { get; private set; }

        public void Init(RetroMessage? retroMessage = null, Dofus2Message? dofus2Message = null)
        {
            RetroMessage = retroMessage;
            Dofus2Message = dofus2Message;
        }

        public abstract bool Build();
    }
}
