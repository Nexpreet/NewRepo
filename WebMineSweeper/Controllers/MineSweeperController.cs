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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public BoardViewModel FieldClick(int row, int column)
        {
            return _mineSweeperBL.FieldClick(row, column);
        }

        [HttpGet("createboard")]
        public BoardViewModel CreateBoard(int width, int height, int numberOfMines)
        {
            return _mineSweeperBL.CreateGame(width, height, numberOfMines);
        }

        // POST api/<MineSweeperController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MineSweeperController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MineSweeperController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
