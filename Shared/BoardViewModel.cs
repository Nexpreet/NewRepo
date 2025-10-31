using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [Serializable]
    public class BoardViewModel
    {
        public BoardModel Board { get; set; }
        public GameStatus GameStatus { get; set; }
    }
}
