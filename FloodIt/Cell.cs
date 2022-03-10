using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FloodIt
{
    internal class Cell
    {
        public bool IsFilled { get; set; } = false;
        public bool Visited { get; set; } = false;

        public Point Position { get; set; } = new Point();
        public Point GridIdx { get; set; } = new Point();
        public Color Color { get; set; } = Color.White;

        public Color OGColor { get; set; } = Color.White;
        public int SideLen { get; set; } = 0;
        public Cell[] Neighbors { get; set; } = new Cell[4];

        public Cell(Point gridIdx, Color color, int sideLen)
        {
            Position = new Point(gridIdx.X * sideLen, gridIdx.Y * sideLen);
            GridIdx = gridIdx;
            Color = color;
            OGColor = color;
            SideLen = sideLen;
        }

        public Cell(Cell copyCell)
        {
            IsFilled = copyCell.IsFilled;
            Visited = copyCell.Visited;
            Position = copyCell.Position;
            GridIdx = copyCell.GridIdx;
            Color = copyCell.Color;
            SideLen = copyCell.SideLen;
            Neighbors = copyCell.Neighbors;
        }

        public override string ToString()
        {
            return $"Pos: {GridIdx} Col: {Color.ToString()} Fill: {IsFilled}  Vis: {Visited} ";
        }
    }
}
