using Microsoft.AspNetCore.Mvc;
using Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMineSweeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MineSweeperController : ControllerBase
    {
        protected IMineSweeperBL _mineSweeperBL { get; }
        public MineSweeperController(IMineSweeperBL mineSweeperBL)
        {
            _mineSweeperBL = mineSweeperBL;
        }

        // GET api/<MineSweeperController>/5
        [HttpGet("getboard")]
        public BoardViewModel GetBoard()
        {
            return _mineSweeperBL.GetBoard();
        }

        [HttpGet("fieldclick/{row}/{column}")]
        public BoardViewModel FieldClick(int row, int column)
        {
            return _mineSweeperBL.FieldClick(row, column);
        }

        [HttpGet("addflag/{row}/{column}")]
        public bool AddFlag(int row, int column)
        {
            return _mineSweeperBL.AddFlag(row, column);
        }

        [HttpGet("removeflag/{row}/{column}")]
        public bool RemoveFlag(int row, int column)
        {
            return _mineSweeperBL.RemoveFlag(row, column);
        }

        [HttpGet("createboard/{width}/{height}/{numberOfMines}")]
        public BoardViewModel CreateBoard(int width, int height, int numberOfMines)
        {
            return _mineSweeperBL.CreateGame(width, height, numberOfMines);
        }
    }
}
