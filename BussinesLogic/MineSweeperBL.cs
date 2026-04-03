using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace BussinesLogic
{
    public class MineSweeperBL : IMineSweeperBL
    {
        readonly IMineSweeperRepo _repo;

        public MineSweeperBL(IMineSweeperRepo repo)
        {
            _repo = repo;
        }
        public BoardViewModel CreateGame(int width, int height, int numberOfMines)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Wrong width or height");

            if (numberOfMines >= height * width || numberOfMines <= 0)
                throw new ArgumentException("Wrong number of mines");

            BoardModel board = new BoardModel(height, width, numberOfMines);

            CreateMineField(board);

            _repo.SaveGame(board);

            return BoardViewModel.CleanBoardViewModelFromMineInfo(new BoardViewModel(board));
        }

        public BoardViewModel FieldClick(int row, int col)
        {
            BoardModel board = _repo.GetGame();
            BoardViewModel boardViewModel = new BoardViewModel(board);

            if (!board.Fields[row][col].HasFlag)
            {
                board.Fields[row][col].IsOpened = true;

                if (!board.Fields[row][col].HasMine)
                {
                    OpenFields(row, col, board);
                }
                
                _repo.SaveGame(board);

                if (boardViewModel.GameStatus == GameStatus.Lost || boardViewModel.GameStatus == GameStatus.Won)
                    return boardViewModel;
            }
            
            return BoardViewModel.CleanBoardViewModelFromMineInfo(boardViewModel);
        }

        public bool AddFlag(int row, int col)
        {
            BoardModel board = _repo.GetGame();
            BoardViewModel boardViewModel = new BoardViewModel(board);

            if (!board.Fields[row][col].IsOpened)
            {
                board.Fields[row][col].HasFlag = true;

                _repo.SaveGame(board);

                return true;
            }
            return false;
        }

        public bool RemoveFlag(int row, int col)
        {
            BoardModel board = _repo.GetGame();
            BoardViewModel boardViewModel = new BoardViewModel(board);

            if (board.Fields[row][col].HasFlag)
            {
                board.Fields[row][col].HasFlag = false;

                _repo.SaveGame(board);

                return true;
            }

            return false;
        }

        //TODO Expand response; Check CalculateNumberOfMinesAround method 
        private void OpenFields(int row, int col, BoardModel board)
        {
            if (board.Fields[row][col].HasFlag)
                return;

            if (!board.Fields[row][col].NumberOfMinesAround.HasValue)
                board.Fields[row][col].NumberOfMinesAround = CalculateNumberOfMinesAround(board, row, col);

            if (board.Fields[row][col].NumberOfMinesAround.Value > 0)
            {
                board.Fields[row][col].IsOpened = true;
                return;
            }

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < board.Height && j >= 0 && j < board.Width && !(i == row && j == col))
                    {
                        if (!board.Fields[i][j].HasMine)
                        {
                            if (!board.Fields[i][j].IsOpened)
                            {
                                board.Fields[i][j].IsOpened = true;
                                OpenFields(i, j, board);
                            }
                        }
                    }
                }
            }
        }

        private void CreateMineField(BoardModel board)
        {
            Random rand = new Random();
            int placedMines = 0;

            while (placedMines < board.NumberOfMines)
            {
                int row = rand.Next(board.Height);
                int col = rand.Next(board.Width);

                if (!board.Fields[row][col].HasMine)
                {
                    board.Fields[row][col].HasMine = true;
                    placedMines++;
                }
            }
        }

        private int CalculateNumberOfMinesAround(BoardModel board, int row, int col)
        {
            int numberOfMines = 0;

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < board.Height
                        && j >= 0 && j < board.Width
                        && board.Fields[i][j].HasMine
                        && !(i == row && j == col))
                        numberOfMines++;
                }
            }

            return numberOfMines;
        }

        public BoardViewModel GetBoard()
        {
            BoardModel board = _repo.GetGame();
            BoardViewModel boardViewModel = new BoardViewModel(board);

            return BoardViewModel.CleanBoardViewModelFromMineInfo(boardViewModel);
        }
    }
}
