-- ============================================
-- CREATE DATABASE
-- ============================================

IF DB_ID('Minesweeper') IS NULL
BEGIN
    CREATE DATABASE Minesweeper;
END
GO

USE Minesweeper;
GO


-- ============================================
-- DROP TABLES IF THEY ALREADY EXIST
-- ============================================

IF OBJECT_ID('dbo.field', 'U') IS NOT NULL
    DROP TABLE dbo.field;

IF OBJECT_ID('dbo.board', 'U') IS NOT NULL
    DROP TABLE dbo.board;
GO


-- ============================================
-- BOARD TABLE
-- ============================================

CREATE TABLE dbo.board
(
    id INT IDENTITY(1,1) NOT NULL,
    height INT NOT NULL,
    width INT NOT NULL,
    number_of_mines INT NOT NULL,

    CONSTRAINT PK_board PRIMARY KEY (id)
);
GO


-- ============================================
-- FIELD TABLE
-- ============================================

CREATE TABLE dbo.field
(
    id INT IDENTITY(1,1) NOT NULL,

    board_id INT NOT NULL,

    row_index INT NOT NULL,
    column_index INT NOT NULL,

    has_mine BIT NOT NULL CONSTRAINT DF_field_has_mine DEFAULT 0,
    is_opened BIT NOT NULL CONSTRAINT DF_field_is_opened DEFAULT 0,
    has_flag BIT NOT NULL CONSTRAINT DF_field_has_flag DEFAULT 0,

    number_of_mines_around INT NULL,

    CONSTRAINT PK_field PRIMARY KEY (id),

    CONSTRAINT FK_field_board
        FOREIGN KEY (board_id)
        REFERENCES dbo.board(id)
        ON DELETE CASCADE
);
GO


-- ============================================
-- UNIQUE POSITION CONSTRAINT
-- ============================================

ALTER TABLE dbo.field
ADD CONSTRAINT UQ_field_position
UNIQUE (board_id, row_index, column_index);
GO