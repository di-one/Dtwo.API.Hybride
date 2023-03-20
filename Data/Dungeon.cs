using Dtwo.API.Dofus2.AnkamaGames;
using Dtwo.API.DofusBase;

namespace Dtwo.API.Hybride.Data
{
    public class Dungeon : HybrideData
    {
        public readonly int Id;
        public readonly string Name;
        public int MapId;
        public Dungeon(int id, string name, int mapId)
        {
            Id = id;
            Name = name;
            MapId = mapId;
        }

        public static Dungeon GetDungeon(int id)
        {
            if (HybrideManager.DofusVersion == EDofusVersion.Retro)
            {
                return GetRetroDungeon(id);
            }
            else
            {
                return GetDofus2Dungeon(id);
            }
        }

        public static List<Dungeon> GetDungeons()
        {
            if (HybrideManager.DofusVersion == EDofusVersion.Retro)
            {
                return GetRetroDungeons();
            }
            else
            {
                return GetDofus2Dungeons();
            }
        }

        private static Dungeon GetRetroDungeon(int id)
        {
            return null;
        }

        private static List<Dungeon> GetRetroDungeons()
        {
            return null;
        }

        private static Dungeon GetDofus2Dungeon(int id)
        {
            var dungeon = Dofus2.Data.Database.GetData<Dofus2.AnkamaGames.Dofus.DataCenter.Dungeon>(id);
            if (dungeon == null)
            {
                // error
                return null;
            }

            //d2 dungeon to hybride
            return new Dungeon(dungeon.id, Dofus2.AnkamaGames.Jerakine.Data.I18N.GetText((int)dungeon.nameId), (int)dungeon.entranceMapId);
        }

        private static List<Dungeon> GetDofus2Dungeons()
        {
            var objs = Dofus2.Data.Database.GetData<Dofus2.AnkamaGames.Dofus.DataCenter.Dungeon>();
            List<Dungeon> ret = new();
            if (objs == null)
            {
                // error
                return null;
            }

            for (int i = 0; i < objs.Count; i++)
            {
                ret.Add(GetDofus2Dungeon(objs[i].id));
            }

            return ret;
        }
    }
}
