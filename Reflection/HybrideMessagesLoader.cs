using System.Reflection;
using Dtwo.API.Dofus2.Network.Messages;
using Dtwo.API.DofusBase.Data;
using Dtwo.API.DofusBase.Reflection;
using Dtwo.API.Hybride.Network.Messages;
using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Hybride.Reflection
{
    public class HybrideMessagesLoader
    {
        public static HybrideMessagesLoader? Instance { get; private set; }

        private readonly Dictionary<string, Func<HybrideMessage>> m_retroMessages = new();
        private readonly Dictionary<string, Func<HybrideMessage>> m_dofus2Messages = new();

        public DofusBindingInfos? m_hybrideBindingInfos;

        public HybrideMessagesLoader()
        {
            Instance = this;
        }

        public bool InitializeMessages(string filePath)
        {
            LogManager.Log("Load hybride messages");
            int loadedMessages = 0;

            if (File.Exists(filePath) == false)
            {
                LogManager.LogError($"Error : initialize message fail, the file {filePath} not found", 1);
                return false;
            }

            var bindings = UpdateAndLoadHybrideMessagesBinding(filePath);
            if (bindings == null)
            {
                LogManager.LogError($"{nameof(HybrideMessagesLoader)}.{InitializeMessages}", "Binding is null");
                return false;
            }

            Assembly? asm = Assembly.GetAssembly(typeof(HybrideMessage));

            if (asm == null)
            {
                LogManager.LogError($"{nameof(HybrideMessagesLoader)}.{InitializeMessages}", "The assembly is null");
                return false;
            }

            var asmTypes = asm.GetExportedTypes();
            for (int i = 0; i < asmTypes.Length; i++)
            {
                var type = asmTypes[i];

                if (type.IsClass && type.IsAbstract == false && type.IsSubclassOf(typeof(HybrideMessage)))
                {
                    var binding = GetBinding(type, bindings);

                    if (binding == null) continue;

                    if (binding.RetroIdentifier == null)
                    {
                        LogManager.LogWarning($"{nameof(HybrideMessagesLoader)}.{InitializeMessages}", $"The retro identifier is null");
                        continue;
                    }

                    if (m_retroMessages.ContainsKey(binding.RetroIdentifier)) // error
                    {
                        LogManager.LogError(
                            $"{nameof(HybrideMessagesLoader)}.{InitializeMessages}",
                            $"Error on MessageLoader.LoadMessage : The key {binding.RetroIdentifier} aleady exist");

                        continue;
                    }

                    if (binding.Dofus2Identifier == null)
                    {
                        LogManager.LogWarning($"{nameof(HybrideMessagesLoader)}.{InitializeMessages}", $"The dofus2 identifier is null");
                        continue;
                    }

                    if (m_dofus2Messages.ContainsKey(binding.Dofus2Identifier)) // error
                    {
                        LogManager.LogError(
                            $"{nameof(HybrideMessagesLoader)}.{InitializeMessages}",
                            $"Error on MessageLoader.LoadMessage : The key {binding.Dofus2Identifier} aleady exist");

                        continue;
                    }

                    ConstructorInfo? ctor = type.GetConstructor(Type.EmptyTypes);
                    if (ctor == null)
                    {
                        LogManager.LogError(
                            $"{nameof(HybrideMessagesLoader)}.{InitializeMessages}",
                            $"Error on MessageLoader.LoadMessage : The class {binding.ClassName} has no constructor");

                        continue;
                    }

                    m_retroMessages.Add(binding.RetroIdentifier, ctor.CreateDelegate<Func<HybrideMessage>>());
                    m_dofus2Messages.Add(binding.Dofus2Identifier, ctor.CreateDelegate<Func<HybrideMessage>>());

                    loadedMessages++;
                }
            }

            LogManager.Log($"{loadedMessages} hybrides messages loaded");

            return true;
        }

        private List<HybrideMessageBinding>? UpdateAndLoadHybrideMessagesBinding(string filePath)
        {
            string hybrideBindingInfosPath = Dtwo.API.Paths.HybrideBindingInfosPath;
            if (File.Exists(hybrideBindingInfosPath) == false)
            {
                m_hybrideBindingInfos = new DofusBindingInfos();
            }
            else
            {
                m_hybrideBindingInfos = Newtonsoft.Json.JsonConvert.DeserializeObject<DofusBindingInfos>(File.ReadAllText(hybrideBindingInfosPath));
            }

            if (m_hybrideBindingInfos == null)
            {
                LogManager.LogError($"{nameof(HybrideMessagesLoader)}.{UpdateAndLoadHybrideMessagesBinding}", "Error on MessageLoader.LoadMessage : The binding infos is null");
                return null;
            }

            var dofus2Version = Dtwo.API.Dofus2.Version.Text;
            if (dofus2Version != null && dofus2Version != m_hybrideBindingInfos.LastVersionCheck)
            {
                m_hybrideBindingInfos.LastVersionCheck = dofus2Version;
                File.WriteAllText(hybrideBindingInfosPath, Newtonsoft.Json.JsonConvert.SerializeObject(m_hybrideBindingInfos));
            }

            string content = File.ReadAllText(filePath);

            List<HybrideMessageBinding>? bindings;
            bindings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HybrideMessageBinding>>(content);

            if (bindings == null)
            {
                LogManager.LogError($"{nameof(HybrideMessagesLoader)}.{UpdateAndLoadHybrideMessagesBinding}", "Error on MessageLoader.LoadMessage : The bindings is null");
                return null;
            }

            int modifiedIds = 0;

            for (int i = 0; i < bindings.Count; i++)
            {
                var binding = bindings[i];
                string? dofus2MessageId;

                if (binding.ClassName == null)
                {
                    LogManager.LogWarning($"{nameof(HybrideMessagesLoader)}.{UpdateAndLoadHybrideMessagesBinding}", $"Error on MessageLoader.LoadMessage : The class name is null");
                    continue;
                }

                if (Dtwo.API.Dofus2.Reflection.Dofus2MessagesLoader.Instance.MessagesByType.TryGetValue(binding.ClassName, out dofus2MessageId) == false)
                {
                    if (dofus2MessageId != binding.Dofus2Identifier)
                    {
                        modifiedIds++;
                        binding.Dofus2Identifier = dofus2MessageId;
                    }
                }
            }

            File.WriteAllText(filePath, Newtonsoft.Json.JsonConvert.SerializeObject(bindings));
            return bindings;
        }

        public HybrideMessage? GetRetroMessage(string identifier)
        {
            Func<HybrideMessage>? func;

            if (m_retroMessages.TryGetValue(identifier, out func))
            {
                return func();
            }

            return null;
        }

        public HybrideMessage? GetDofus2Message(string identifier)
        {
            Func<HybrideMessage>? func;

            if (m_dofus2Messages.TryGetValue(identifier, out func))
            {
                return func();
            }

            return null;
        }

        private static HybrideMessageBinding? GetBinding(Type messageType, List<HybrideMessageBinding> bindings)
        {
            string str = messageType.Name;

            for (int i = 0; i < bindings.Count; i++)
            {
                var binding = bindings[i];

                if (binding.ClassName == str)
                {
                    return binding;
                }
            }

            return null;
        }
    }
}
