using Microsoft.AspNetCore.Mvc;
using Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMineSweeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MineSweeperController : ControllerBase
    {
        // GET api/<MineSweeperController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public string FieldClick(int row, int column)
        {
            return "value";
        }

        [HttpGet("createboard")]
        public BoardViewModel CreateBoard(int height, int width, int numberOfMines)
        {
            return new BoardViewModel
            {
                Board = new BoardModel(height, width, numberOfMines),
                GameStatus = GameStatus.Created
            };
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
