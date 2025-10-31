using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace BussinesLogic
{
    public class MineSweeperBL : IMineSweeperBL
    {

        public BoardViewModel CreateGame(int width, int height, int mines)
        {
            if (height <= 0 || width <= 0) throw new ArgumentException("Losa tabla! ERROR!");
            if (mines <= 0 || mines >= height * width) throw new ArgumentException("Los broj mina!");

            var board = new BoardModel(width, height, mines);


        }

    }
}
