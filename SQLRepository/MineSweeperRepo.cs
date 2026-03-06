using System.Data;
using System.Text.Json;
using Shared;

namespace SQLRepository
{

    public class MineSweeperRepo : IMineSweeperRepo
    {
        public bool SaveGame(BoardModel board)
        {
            try
            {
                using System.Data;
                using System.Data.SqlClient;
                using System.Text.Json;

public static void SaveBoard(BoardModel board, int? boardId, string connectionString)
        {
            var fields = new List<object>();

            for (int i = 0; i < board.Height; i++)
            {
                for (int j = 0; j < board.Width; j++)
                {
                    var f = board.Fields[i][j];

                    fields.Add(new
                    {
                        row = i,
                        col = j,
                        hasMine = f.HasMine,
                        isOpened = f.IsOpened,
                        hasFlag = f.HasFlag,
                        numberOfMinesAround = f.NumberOfMinesAround
                    });
                }
            }

            string json = JsonSerializer.Serialize(fields);

            using SqlConnection conn = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("dbo.UpsertBoardAndFields", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BoardId", SqlDbType.Int).Value =
                boardId.HasValue ? boardId.Value : DBNull.Value;

            cmd.Parameters.Add("@Height", SqlDbType.Int).Value = board.Height;
            cmd.Parameters.Add("@Width", SqlDbType.Int).Value = board.Width;
            cmd.Parameters.Add("@NumberOfMines", SqlDbType.Int).Value = board.NumberOfMines;

            cmd.Parameters.Add("@FieldsJson", SqlDbType.NVarChar).Value = json;

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BoardModel GetGame()
        {
            try
            {
                return ReadFromFile();
            }
            catch (Exception ex)
            {
                return null;
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
