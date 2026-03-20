using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace ConsoleGame
{
    internal class GameLoop
    {
        public void Play()
        {
            Service service = new Service();
            BoardViewModel board = new BoardViewModel(
                new BoardModel(10, 10, 25)
                );

            while (true)
            {
                Display display = new Display(board);

                display.WriteHeader();
                display.WriteBoard();
                display.WriteOptions();

                ConsoleKeyInfo info= Console.ReadKey();

                int x, y;

                switch (info.Key)
                {
                    case ConsoleKey.D1:
                        (x, y) = GetRowCol(board.Board.Height, board.Board.Width);
                        board = service.FieldClick(x, y);
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("You pressed B!");
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine("You pressed C!");
                        break;
                    case ConsoleKey.D8:
                        Console.WriteLine("You pressed C!");
                        break;
                    case ConsoleKey.D9:
                        Console.WriteLine("You pressed C!");
                        break;
                    default:
                        Console.WriteLine("You pressed another key.");
                        break;
                }
            }
        }

        public (int red, int kolona) GetRowCol(int height, int width)
        {
            int x = 0;
            int y = 0;
            Console.WriteLine("Unesite red i kolonu:");
            while (x < 1 || x > height + 1)
            {
                Console.Write("Red: ");
                x = Convert.ToInt32(Console.ReadLine());
            }
            while (y < 1 || y > width + 1)
            {
                Console.Write("kolona: ");
                y = Convert.ToInt32(Console.ReadLine());
            }
            return (x, y);
        }
    }
}
