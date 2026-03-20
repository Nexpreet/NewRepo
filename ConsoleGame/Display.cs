using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace ConsoleGame
{
    internal class Display
    {
        BoardViewModel _board;
        public Display(BoardViewModel board)
        {
            _board = board;
        }
        internal void WriteBoard()
        {
            if(_board == null)
            {
                Console.WriteLine("Tabla mora biti popunjena!");
                return;
            }
            Console.Write("{0, 4}", " ");
            for(int l=0; l<_board.Board.Width; l++) Console.Write("{0, 3}", l+1);
            Console.WriteLine();
            Console.WriteLine();
            for (int i=0; i<_board.Board.Height; i++)
            {                   
                Console.Write("{0, 3}", i+1);
                Console.Write("{0, 1}", " ");
                for(int j=0; j<_board.Board.Width; j++)
                {
                    FieldModel field = _board.Board.Fields[i][j];

                    if (field.IsOpened)
                    {
                        if (field.HasMine) Console.Write("{0, 3}", "*");
                        else Console.Write("{0, 3}", " ");
                    }
                    else if (field.HasFlag) Console.Write("{0, 3}", "P");
                    else Console.Write("{0, 3}", "X");
                }
                Console.WriteLine();
            }
        }

        internal void WriteHeader()
        {
            Console.Clear();
            Console.WriteLine("Mine Sweeper!");
            Console.WriteLine("Game status: " + _board.GameStatus);
            Console.WriteLine("***********************************");
            Console.WriteLine();
        }

        internal void WriteOptions()
        {
            Console.WriteLine();
            if(_board.GameStatus == GameStatus.Running || _board.GameStatus == GameStatus.Created)
            {
                Console.WriteLine("Opcije:");
                Console.WriteLine("1 - Otvori polje   2 - Postavi zastavu   3 - Ukloni zastavu            8 - Nova igra   9 - Izlaz iz igre");
            }
            else if(_board.GameStatus == GameStatus.Lost || _board.GameStatus == GameStatus.Won)
            {
                Console.WriteLine("8 - Nova igra   9 - Izlaz iz igre");
            }
        }
    }
}
