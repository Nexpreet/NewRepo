CREATE OR ALTER PROCEDURE dbo.GetBoard
(
    @BoardId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Board
    SELECT 
        id,
        height,
        width,
        number_of_mines
    FROM dbo.board
    WHERE id = @BoardId;

    -- Fields
    SELECT
        row_index,
        column_index,
        has_mine,
        is_opened,
        has_flag,
        number_of_mines_around
    FROM dbo.field
    WHERE board_id = @BoardId
    ORDER BY row_index, column_index;
END
GO