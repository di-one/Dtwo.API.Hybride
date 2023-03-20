using System.Reflection;
using Dtwo.API.Dofus2.Network.Messages;
using Dtwo.API.DofusBase.Reflection;
using Dtwo.API.Hybride.Network.Messages;
using Dtwo.API.Retro.Network.Messages;

namespace Dtwo.API.Hybride.Reflection
{
    public class HybrideMessagesLoader
    {
        public static HybrideMessagesLoader Instance { get; private set; }

        private readonly Dictionary<string, Func<HybrideMessage>> m_retroMessages = new();
        private readonly Dictionary<string, Func<HybrideMessage>> m_dofus2Messages = new();

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

            string content = File.ReadAllText(filePath);
            List<HybrideMessageBinding> bindings;
            try
            {
                bindings = Json.JSonSerializer<List<HybrideMessageBinding>>.DeSerialize(content);
            }
            catch (Exception)
            {
                return false;
            }

            Assembly asm = Assembly.GetAssembly(typeof(HybrideMessage));
            var asmTypes = asm.GetExportedTypes();
            for (int i = 0; i < asmTypes.Length; i++)
            {
                var type = asmTypes[i];

                if (type.IsClass && type.IsAbstract == false && type.IsSubclassOf(typeof(HybrideMessage)))
                {
                    var binding = GetBinding(type, bindings);

                    if (binding == null) continue;

                    if (m_retroMessages.ContainsKey(binding.RetroIdentifier)) // error
                    {
                        Console.WriteLine($"Error on MessageLoader.LoadMessage : The key {binding.RetroIdentifier} aleady exist");
                        continue;
                    }

                    if (m_dofus2Messages.ContainsKey(binding.Dofus2Identifier)) // error
                    {
                        Console.WriteLine($"Error on MessageLoader.LoadMessage : The key {binding.Dofus2Identifier} aleady exist");
                        continue;
                    }

                    ConstructorInfo? ctor = type.GetConstructor(Type.EmptyTypes);
                    if (ctor == null)
                    {
                        Console.WriteLine($"Error on MessageLoader.LoadMessage : The class {binding.ClassName} has no constructor");
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

        public HybrideMessage GetRetroMessage(string identifier)
        {
            Func<HybrideMessage> func;

            if (m_retroMessages.TryGetValue(identifier, out func))
            {
                return func();
            }

            return null;
        }

        public HybrideMessage GetDofus2Message(string identifier)
        {
            Func<HybrideMessage> func;

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
