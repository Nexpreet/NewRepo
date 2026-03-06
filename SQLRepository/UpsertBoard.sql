CREATE OR ALTER PROCEDURE dbo.UpsertBoardAndFields
(
    @BoardId INT = NULL,
    @Height INT,
    @Width INT,
    @NumberOfMines INT,
    @FieldsJson NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ActualBoardId INT;

    -- INSERT ili UPDATE board
    IF @BoardId IS NULL OR NOT EXISTS (SELECT 1 FROM dbo.board WHERE id = @BoardId)
    BEGIN
        INSERT INTO dbo.board(height, width, number_of_mines)
        VALUES (@Height, @Width, @NumberOfMines);

        SET @ActualBoardId = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        UPDATE dbo.board
        SET
            height = @Height,
            width = @Width,
            number_of_mines = @NumberOfMines
        WHERE id = @BoardId;

        SET @ActualBoardId = @BoardId;
    END


    -- Parse JSON u tabelu
    ;WITH FieldsCTE AS
    (
        SELECT
            row_index,
            column_index,
            has_mine,
            is_opened,
            has_flag,
            number_of_mines_around
        FROM OPENJSON(@FieldsJson)
        WITH
        (
            row_index INT '$.row',
            column_index INT '$.col',
            has_mine BIT '$.hasMine',
            is_opened BIT '$.isOpened',
            has_flag BIT '$.hasFlag',
            number_of_mines_around INT '$.numberOfMinesAround'
        )
    )

    -- UPSERT fields
    MERGE dbo.field AS target
    USING FieldsCTE AS source
    ON target.board_id = @ActualBoardId
       AND target.row_index = source.row_index
       AND target.column_index = source.column_index

    WHEN MATCHED THEN
        UPDATE SET
            has_mine = source.has_mine,
            is_opened = source.is_opened,
            has_flag = source.has_flag,
            number_of_mines_around = source.number_of_mines_around

    WHEN NOT MATCHED THEN
        INSERT
        (
            board_id,
            row_index,
            column_index,
            has_mine,
            is_opened,
            has_flag,
            number_of_mines_around
        )
        VALUES
        (
            @ActualBoardId,
            source.row_index,
            source.column_index,
            source.has_mine,
            source.is_opened,
            source.has_flag,
            source.number_of_mines_around
        );

END
GO