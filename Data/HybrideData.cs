using Dtwo.API.DofusBase.Data;
using Dtwo.API.Retro.Data;
using Dtwo.API.Dofus2.Data;

namespace Dtwo.API.Hybride.Data
{
    public class HybrideData : DofusData
    {
        public RetroData? RetroData { get; private set; }
        public Dofus2Data? Dofus2Data { get; private set; }

        public void Init(RetroData retroData = null, Dofus2Data dofus2Data = null)
        {
            RetroData = retroData;
            Dofus2Data = dofus2Data;
        }
    }
}
