using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared
{
    [Serializable]
    public class BoardViewModel
    {
        public BoardModel Board { get; set; }
        private GameStatus gameStatus = GameStatus.Created;
        public GameStatus GameStatus
        {

            get
            {
                int numberOfOpenedFields = 0;
                for (int i = 0; i < Board.Height; i++)
                {
                    for (int j = 0; j < Board.Width; j++)
                    {
                        if (Board.Fields[i][j].HasMine && Board.Fields[i][j].IsOpened)
                        {
                            gameStatus = GameStatus.Lost;
                            return gameStatus;
                        }
                        else if(Board.Fields[i][j].IsOpened)
                        {
                            numberOfOpenedFields++;
                        }
                    }
                }
                if(Board.Height * Board.Width - numberOfOpenedFields == Board.NumberOfMines)
                {
                    gameStatus = GameStatus.Won;
                }
                else
                {
                    gameStatus = GameStatus.Running; 
                }
                return gameStatus;
            }
            
        }
        
        public BoardViewModel(BoardModel board)
        {
            Board = board;
            gameStatus = GameStatus.Created;
        }
        

        public static BoardViewModel CleanBoardViewModelFromMineInfo(BoardViewModel boardViewModel)
        {
            for (int i = 0; i < boardViewModel.Board.Height; i++)
            {
                for (int j = 0; j < boardViewModel.Board.Width; j++)
                {
                    boardViewModel.Board.Fields[i][j].HasMine = false;
                }
            }

            return boardViewModel;
        }

        public static BoardViewModel GetFromJSON(string json)
        {
            // FIX 2: Add options to handle camelCase JSON vs PascalCase C# properties
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<BoardViewModel>(json, options);
        }
    }
}
