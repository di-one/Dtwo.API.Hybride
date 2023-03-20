namespace Dtwo.API.Hybride.Network.Types
{
    public class FightResultListEntry
    {
        public int Result { get; private set; }
        public uint PlayerId { get; private set; }
        public string PlayerName { get; private set; }
        public bool IsAlive { get; private set; }
        public uint ExperienceGain { get; private set; }
        public uint KamasGain { get; private set; }
        public FightLoot FightLoot { get; private set; }

        public FightResultListEntry(int result, uint playerId, string playerName, 
           bool isAlive, uint exp, uint kamas,  FightLoot fightLoot)
        {
            Result = result;
            PlayerId = playerId;
            PlayerName = playerName;
            IsAlive = isAlive;
            ExperienceGain = exp;
            KamasGain = kamas;
            FightLoot = fightLoot;
        }
    }
}
