using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface IMineSweeperBL
    {
        BoardViewModel CreateGame(int width, int height, int numberOfMines);
        BoardViewModel FieldClick(int row, int column);
    }
}
