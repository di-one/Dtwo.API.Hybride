using System.Runtime.Serialization;

namespace Dtwo.API.Hybride.Reflection
{
    [DataContract]
    public class HybrideMessageBinding
    {
        [DataMember]
        public string? RetroIdentifier { get; set; }
        [DataMember]
        public string? Dofus2Identifier { get; set; }
        [DataMember]
        public string? ClassName { get; set; }
    }
}
