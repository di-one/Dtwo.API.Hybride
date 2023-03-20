using Dtwo.API.DofusBase;
using Dtwo.API.Hybride.Reflection;

namespace Dtwo.API.Hybride
{
    public static class HybrideManager
    {
        public static EDofusVersion DofusVersion { get; private set; }
        public static void Init(EDofusVersion version, string messagesPath)
        {
            DofusVersion = version;

            HybrideMessagesLoader loader = new HybrideMessagesLoader();
            loader.InitializeMessages(messagesPath);
        }
    }
}
