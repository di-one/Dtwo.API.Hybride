using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtwo.API.Hybride.Data
{
    internal class CellData : HybrideData
    {
        public bool IsMovable;

        public static CellData Parse(Dtwo.API.Dofus2.AnkamaGames.Atouin.CellData cellData)
        {
            CellData cell = new CellData();
            cell.IsMovable = cellData.Mov;

            return cell;
        }

        public static CellData Parse(D_One.Core.DofusBehavior.Map.Cell cellData)
        {
            CellData cell = new CellData();
            cell.IsMovable = cellData.IsWalkable;

            return cell;
        }
    }
}
