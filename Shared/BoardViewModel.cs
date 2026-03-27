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
        public GameStatus GameStatus { get; set; }

        public BoardViewModel(BoardModel board)
        {
            Board = board;
            GameStatus = GameStatus.Created;
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
