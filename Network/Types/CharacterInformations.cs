namespace Dtwo.API.Hybride.Network.Types
{
    public class CharacterInformations
    {
        public readonly double Id;
        public readonly string Name;
        public readonly bool Sex;
        public readonly int Breed;
        public int Level { get; private set; }
        public CharacterInformations(bool sex, int breed, int level, string name, double id)
        {
            Id = id;
            Name = name;
            Sex = sex;
            Breed = breed;
            Level = level;
        }
    }
}
