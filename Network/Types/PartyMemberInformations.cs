namespace Dtwo.API.Hybride.Network.Types
{
    public class PartyMemberInformations
    {
        public double id;
        public string name;
        public uint level;
        public uint lifePoints;
        public uint maxLifePoints;
        public uint prospecting;
        public uint initiative;
        public int alignmentSide;

        public static PartyMemberInformations Parse(Dofus2.AnkamaGames.Network.Types.PartyMemberInformations partyMemberInformations)
        {
            PartyMemberInformations infos = new PartyMemberInformations()
            {
                id = partyMemberInformations.id,
                name = partyMemberInformations.name,
                level = partyMemberInformations.level,
                lifePoints = partyMemberInformations.lifePoints,
                maxLifePoints = partyMemberInformations.maxLifePoints,
                prospecting = partyMemberInformations.prospecting,
                initiative = partyMemberInformations.initiative,
                alignmentSide = partyMemberInformations.alignmentSide
            }; 

            return infos;
        }

        public static PartyMemberInformations Parse(Dtwo.API.Retro.AnkamaGames.Dofus.Network.Types.PartyMemberInformations partyMemberInformations)
        {
            PartyMemberInformations infos = new PartyMemberInformations()
            {
                id = partyMemberInformations.id,
                name = partyMemberInformations.name,
                level = partyMemberInformations.level,
                lifePoints = partyMemberInformations.lifePoints,
                maxLifePoints = partyMemberInformations.maxLifePoints,
                prospecting = partyMemberInformations.prospecting,
                initiative = partyMemberInformations.initiative,
                alignmentSide = partyMemberInformations.alignmentSide
            };

            return infos;
        }
    }
}
