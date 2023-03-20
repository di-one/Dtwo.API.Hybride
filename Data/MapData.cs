using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtwo.API.Hybride.Data
{
    internal class MapData : HybrideData
    {
        public int Id;
        public List<CellData> Cells;

        public static MapData Build(Dtwo.API.Dofus2.AnkamaGames.Atouin.MapData mapData)
        {
            MapData map = new MapData();
            map.Id = mapData.Id;

            map.Cells = new List<CellData>();

            for (int i = 0; i < mapData.Cells.Count;i++)
            {
                var cell = mapData.Cells[i];
                CellData cellData = CellData.Parse(cell);
                map.Cells.Add(cellData);
            }

            return map;
        }

        public static MapData Build(D_One.Core.DofusBehavior.Map.MapInformations mapData)
        {
            MapData map = new MapData();
            map.Id = mapData.Map.Id;

            map.Cells = new List<CellData>();

            for (int i = 0; i < mapData.Cells.Length; i++)
            {
                var cell = mapData.Cells[i];
                CellData cellData = CellData.Parse(cell);
                map.Cells.Add(cellData);
            }


            return map;
        }
    }
}
