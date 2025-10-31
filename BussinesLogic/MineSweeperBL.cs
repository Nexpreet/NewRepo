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

        public bool[,] CreateMineField(BoardModel board) {


            bool[,] mineField = new bool[board.Height, board.Width];
            Random rand = new Random();
            int placedMines = 0;

            while (placedMines < board.NumberOfMines) {
                int row = rand.Next(board.Height);
                int col = rand.Next(board.Width);

                if (!mineField[row, col]) {
                    mineField[row, col] = true;
                    placedMines++;
                }
            }

            return mineField;
        }

        //public uint NumberOfMinesAround(BoardModel board, bool[,] MineField, int row, int col) {

        //    if (!MineField[row, col]) {

        //        int i, j;

        //        int rowLimit = board.Height - 1;
                
        //        switch (row) {
        //            case 0: i = 0;break;
        //            case rowLimit: i = board.Height - 2;break;
        //        }

        //    }


        //}

        public BoardViewModel CreateGame(int width, int height, int mines)
        {
            if (height <= 0 || width <= 0) throw new ArgumentException("Losa tabla! ERROR!");
            if (mines <= 0 || mines >= height * width) throw new ArgumentException("Los broj mina!");

            var board = new BoardModel(width, height, mines);

            return new BoardViewModel(board);
        }

    }
}
