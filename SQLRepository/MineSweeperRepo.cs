using System.Data;
using System.Text.Json;
using Shared;
using Microsoft.Data.SqlClient;
namespace SQLRepository
{

    public class MineSweeperRepo : IMineSweeperRepo
    {
        private readonly SqlConnection _connection;

        public MineSweeperRepo(IDbConnection connection)
        {
            
            _connection = connection as SqlConnection;
            _connection.Open();
        }

        public bool SaveGame(BoardModel board)
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

            
            int? boardId = GetBoardId();
            using SqlCommand cmd = new SqlCommand("dbo.UpsertBoardAndFields", _connection);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BoardId", SqlDbType.Int).Value =
                boardId.HasValue ? boardId.Value : DBNull.Value;

            cmd.Parameters.Add("@Height", SqlDbType.Int).Value = board.Height;
            cmd.Parameters.Add("@Width", SqlDbType.Int).Value = board.Width;
            cmd.Parameters.Add("@NumberOfMines", SqlDbType.Int).Value = board.NumberOfMines;

            cmd.Parameters.Add("@FieldsJson", SqlDbType.NVarChar).Value = json;

            int result = cmd.ExecuteNonQuery();

            return result == 0;
        }

        public BoardModel GetGame()
        {
            BoardModel board = null;


            
            int? boardId = GetBoardId();
            if (boardId.HasValue)
            {

                using (SqlCommand cmd = new SqlCommand("dbo.GetBoard", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BoardId", boardId);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // ----------- BOARD -----------
                        if (reader.Read())
                        {
                            int height = reader.GetInt32(reader.GetOrdinal("height"));
                            int width = reader.GetInt32(reader.GetOrdinal("width"));
                            int mines = reader.GetInt32(reader.GetOrdinal("number_of_mines"));

                            board = new BoardModel(height, width, mines);
                        }

                        // ----------- FIELDS -----------
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                int row = reader.GetInt32(reader.GetOrdinal("row_index"));
                                int col = reader.GetInt32(reader.GetOrdinal("column_index"));

                                board.Fields[row][col].HasMine =
                                    reader.GetBoolean(reader.GetOrdinal("has_mine"));

                                board.Fields[row][col].IsOpened =
                                    reader.GetBoolean(reader.GetOrdinal("is_opened"));

                                board.Fields[row][col].HasFlag =
                                    reader.GetBoolean(reader.GetOrdinal("has_flag"));

                                object numberOfMines = reader.GetValue(reader.GetOrdinal("number_of_mines_around"));
                                board.Fields[row][col].NumberOfMinesAround = numberOfMines as int?;
                            }
                        }
                    }
                }
            }
            return board;
        }
        private int? GetBoardId()
        {
            using SqlCommand cmd = new SqlCommand("select top 1 Id from dbo.board ", _connection);

            cmd.CommandType = CommandType.Text;

            object result = cmd.ExecuteScalar();

            return result as int?;
        }
    }


}
