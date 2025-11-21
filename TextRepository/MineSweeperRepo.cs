using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Shared;

namespace TextRepository
{
    public class MineSweeperRepo : IMineSweeperRepo
    {
        public bool SaveGame(BoardModel board)
        {
            try
            {
                SaveToFile(board);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void SaveToFile(BoardModel board)
        {
            File.WriteAllText("board", JsonSerializer.Serialize(board));
        }
        private BoardModel ReadFromFile()
        {
            return JsonSerializer.Deserialize<BoardModel>(File.ReadAllText("board"));
        }
    }
}
