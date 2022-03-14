using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FloodIt
{
    public class Board
    {
        private Cell[,] _cells;
        private Cell _startCell;
        private Cell _endCell;

        private Point _boardSize;
       
        private int _numColors = 4;
        private int _sizeLen;

        private Pen _boarderPen = new Pen(Color.Black, 2);
        private Font _cellCoordFont = new Font("Tahoma", 5, FontStyle.Regular);

        public event EventHandler RedrawRequested;

        public Cell StartCell
        {
            get { return _startCell; }
        }

        public Cell EndCell
        {
            get { return _endCell; }
        }

        public Cell[,] Cells
        {
            get { return _cells; }
        }

        public int NumColors
        {
            get { return _numColors; }
        }

        public Board(Point size, int numColors)
        {
            _boardSize = size;
            _numColors = numColors;

            Populate();
        }

        private void OnRedrawRequested()
        {
            RedrawRequested?.Invoke(this, EventArgs.Empty);
        }

        public void Populate()
        {
            var cells = new Cell[_boardSize.X, _boardSize.Y];

            // Fill cells with random colors.
            for (int x = 0; x < _boardSize.X; x++)
            {
                for (int y = 0; y < _boardSize.Y; y++)
                {
                    cells[x, y] = new Cell(new Point(x, y), ColorHelper.RandomColor(_numColors));
                }
            }

            // Link neighbors.
            foreach (var cell in cells)
            {
                var neighbors = new List<Cell>();
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 || y == 0)
                        {
                            if (x == 0 && y == 0)
                                continue;

                            int oX = cell.GridIdx.X + x;
                            int oY = cell.GridIdx.Y + y;

                            if (oX >= 0 && oX < _boardSize.X && oY >= 0 && oY < _boardSize.Y)
                            {
                                neighbors.Add(cells[oX, oY]);
                            }
                        }
                    }
                }

                cell.Neighbors = neighbors.ToArray();
            }

            _cells = cells;

            _startCell = _cells[0, 0];
            _endCell = _cells[_boardSize.X - 1, _boardSize.Y - 1];

            DoColorFill(_startCell.Color);

        }

        public void DrawCells(Graphics gfx)
        {
            var sideLen = new PointF(gfx.ClipBounds.Width / _boardSize.X, gfx.ClipBounds.Height / _boardSize.Y);


            foreach (var cell in _cells)
            {
                var pos = new PointF(cell.GridIdx.X * sideLen.X, cell.GridIdx.Y * sideLen.Y);

                //using (var brush = new SolidBrush(cell.Color))
                using (var brush = new SolidBrush(cell.IsFilled ? Color.Gray : cell.Color))
                {
                    gfx.FillRectangle(brush, new RectangleF(pos, new SizeF(sideLen.X, sideLen.Y)));
                    //gfx.DrawRectangle(_boarderPen, new Rectangle(cell.Position, new Size(cell.SideLen, cell.SideLen)));
                }

                //if (cell.IsFilled)
                //{
                //    gfx.FillEllipse(Brushes.LimeGreen, cell.Position.X, cell.Position.Y, 5.0f, 5.0f);
                //}

                //if (cell.Visited)
                //{
                //    gfx.FillEllipse(Brushes.Blue, cell.Position.X + 10, cell.Position.Y, 5.0f, 5.0f);
                //}

                //var coords = $"{cell.GridIdx.X},{cell.GridIdx.Y}";
                //var sz = gfx.MeasureString(coords, _cellCoordFont);
                //var center = new Point(cell.Position.X + (cell.SideLen / 2) - (int)(sz.Width / 2f), cell.Position.Y + (cell.SideLen / 2) - (int)(sz.Height / 2f));
                //gfx.DrawString(coords, _cellCoordFont, Brushes.Black, center);
            }
        }

        public void Redraw()
        {
            OnRedrawRequested();
        }

        public List<Color> AvailableColors()
        {
            var colors = new List<Color>();
            for (int i = 0; i < _numColors; i++)
                colors.Add(ColorHelper.Colors[i]);

            return colors;
        }

        public void DoColorFill(Color color)
        {
            SetFilledColor(color);

            ResetFilled();
            ResetVisited();

            var queue = new Queue<Cell>();
            queue.Enqueue(_startCell);

            while (queue.Count > 0)
            {
                var c = queue.Dequeue();

                c.Visited = true;

                if (c.Color == color)
                {
                    c.IsFilled = true;
                }

                foreach (var n in c.Neighbors)
                {
                    if (n.Color == color)
                    {
                        n.IsFilled = true;

                        if (n.Visited == false)
                            queue.Enqueue(n);

                        n.Visited = true;
                    }
                }
            }

            SetFilledColor(color);
        }

        public bool GameOver()
        {
            foreach (var cell in _cells)
            {
                if (cell.IsFilled == false)
                    return false;
            }

            return true;
        }

        public void SetFilledColor(Color color)
        {
            foreach (var cell in _cells)
            {
                if (cell.IsFilled)
                    cell.Color = color;
            }
        }

        public List<Cell> GetFilledCells()
        {
            var filled = new List<Cell>();

            foreach (var cell in _cells)
                if (cell.IsFilled)
                    filled.Add(cell);

            return filled;
        }


        public List<Color> RemainingColors()
        {
            var colors = new HashSet<Color>();
            foreach (var cell in _cells)
                colors.Add(cell.Color);

            return colors.ToList();
        }

        public void ResetVisited()
        {
            foreach (var cell in _cells)
                cell.Visited = false;
        }

        public void ResetFilled()
        {
            foreach (var cell in _cells)
                cell.IsFilled = false;
        }

        public void ResetColors()
        {
            foreach (var cell in _cells)
                cell.Color = cell.OGColor;
        }

        public void ResetAll()
        {
            foreach (var cell in _cells)
            {
                cell.Color = cell.OGColor;
                cell.Visited = false;
                cell.IsFilled = false;
            }

            DoColorFill(_startCell.Color);

        }

    }
}
