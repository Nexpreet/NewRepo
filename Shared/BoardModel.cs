using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [Serializable]
    public class BoardModel
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int NumberOfMines { get; set; }
        public FieldModel[][] Fields { get; set; }

        public BoardModel(int height, int width, int numberOfMines)
        {
            Height = height;
            Width = width;
            NumberOfMines = numberOfMines;

            Fields = new FieldModel[height][];
            for (int i = 0; i < height; i++)
            {
                Fields[i]=new FieldModel[width];
                for (int j = 0; j < width; j++)
                {
                    Fields[i][j] = new FieldModel();
                }
            }
        }
    }
}
