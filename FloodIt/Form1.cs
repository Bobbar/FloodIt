using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace FloodIt
{
    public partial class Form1 : Form
    {
        private Cell[,] _cells;
        private const int _numColors = 4;
        private Random _rnd = new Random();
        private Pen _boarderPen = new Pen(Color.Black, 2);
        private Point _boardSize = new Point(14, 14);
        private int _sideLen = 0;
        private Cell _selectedCell = null;
        private Font _cellCoordFont = new Font("Tahoma", 5, FontStyle.Regular);

        public Form1()
        {
            InitializeComponent();

            InitColorBoxes();
            _cells = InitCells(_boardSize);
            ProcessColorClick(_cells[0, 0], _cells[0, 0].Color);

            var best = MostCommonTouchingColor();
        }

        private void InitColorBoxes()
        {
            ColorButtons.RowCount = _numColors;
            ColorButtons.RowStyles.Clear();

            for (int i = 0; i < _numColors; i++)
            {
                var color = ColorHelper.Colors[i];
                var button = new Button() { BackColor = color, Dock = DockStyle.Fill };
                button.Click += ColorButton_Click;
                button.Tag = color;
                ColorButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 50.0f));
                ColorButtons.Controls.Add(button, 0, i);
            }
        }

		private void ProcessColorClick(Cell cell, Color color)
		{
			var queue = new Queue<Cell>();
			queue.Enqueue(cell);

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
		}

		private bool GameOver()
        {
            foreach (var cell in _cells)
            {
                if (cell.IsFilled == false)
                    return false;
            }

            return true;
        }

        private void SetCompletedCellsColor(Color color)
        {
            foreach (var cell in _cells)
            {
                if (cell.IsFilled)
                    cell.Color = color;
            }
        }

        private void ResetVisited()
        {
            foreach (var cell in _cells)
                cell.Visited = false;
        }

        private void ResetFilled()
        {
            foreach (var cell in _cells)
                cell.IsFilled = false;
        }

        private void ResetColors()
        {
            foreach (var cell in _cells)
                cell.Color = cell.OGColor;
        }

        private List<Color> RemainingColors()
        {
            var colors = new HashSet<Color>();
            foreach (var cell in _cells)
                colors.Add(cell.Color);

            return colors.ToList();
        }


        private void ColorButton_Click(object? sender, EventArgs e)
        {
            var button = sender as Button;
            var color = (Color)button.Tag;


            SetCompletedCellsColor(color);
            ResetFilled();
            ResetVisited();
            ProcessColorClick(_cells[0, 0], color);
            SetCompletedCellsColor(color);

            if (GameOver())
                Debug.WriteLine("GameOver!!!!");


            //var best = MostCommonTouchingColor();

            //Debug.WriteLine($"Best color: {best.ToString()}");

            //SetCompletedCellsColor(_cells, color);
            //ResetFilled(_cells);
            //ResetVisited(_cells);
            //ProcessColorClick(_cells[0, 0], color);
            //SetCompletedCellsColor(_cells, color);

            //if (GameOver(_cells))
            //    Debug.WriteLine("GameOver!!!!");

            CellBox.Invalidate();
            CellBox.Refresh();
        }

        private Cell[,] InitCells(Point boardSize)
        {
            var cells = new Cell[boardSize.X, boardSize.Y];
            _sideLen = CellBox.Width / boardSize.X;

            for (int x = 0; x < boardSize.X; x++)
            {
                for (int y = 0; y < boardSize.Y; y++)
                {
                    cells[x, y] = new Cell(new Point(x, y), GetRandomColor(), _sideLen);
                }
            }

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

            return cells;


        }

        private void DrawCells(Graphics gfx)
        {
            foreach (var cell in _cells)
            {
                using (var brush = new SolidBrush(cell.Color))
                {
                    gfx.FillRectangle(brush, new Rectangle(cell.Position, new Size(cell.SideLen, cell.SideLen)));
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
                //var center = new Point(cell.Position.X + (cell.SideLen / 2), cell.Position.Y + (cell.SideLen / 2));
                //gfx.DrawString(coords, _cellCoordFont, Brushes.Black, center);
            }

            if (_selectedCell != null)
            {
                //gfx.DrawRectangle(Pens.LimeGreen, new Rectangle(_selectedCell.Position, new Size(_selectedCell.SideLen, _selectedCell.SideLen)));

                foreach (var n in _selectedCell.Neighbors)
                {
                    if (n != null)
                    {
                        gfx.DrawRectangle(Pens.LimeGreen, new Rectangle(n.Position, new Size(n.SideLen, n.SideLen)));
                    }
                }
            }
        }


        private int CountTouching(Color testColor, Cell cell)
        {
            int count = 0;

            foreach (var n in cell.Neighbors)
            {
                if (n.Visited)
                    continue;

                if (n.Color == testColor)
                {
                    count++;

                    n.Visited = true;

                    count += CountTouching(testColor, n);
                }
            }

            return count;
        }

        private Color MostCommonTouchingColor()
        {
            ResetVisited();
            var counts = new Dictionary<Color, int>();

            var edgeCells = new List<Cell>();

            foreach (var cell in _cells)
            {
                if (cell.IsFilled)
                {
                    var edgeNs = cell.Neighbors.Where(c => c.IsFilled == false).ToList();
                    edgeCells.AddRange(edgeNs);
                }
            }

            edgeCells = edgeCells.Distinct().ToList();

            for (int i = 0; i < _numColors; i++)
            {
                ResetVisited();

                var testColor = ColorHelper.Colors[i];

                if (!counts.ContainsKey(testColor))
                    counts.Add(testColor, 0);

                foreach (var cell in edgeCells)
                {
                    if (cell.Color == testColor)
                    {
                        cell.Visited = true;
                        counts[testColor]++;
                        counts[testColor] += CountTouching(testColor, cell);
                    }
                }

            }

            if (counts.Sum(c => c.Value) > 0)
            {
                var best = counts.OrderByDescending(c => c.Value).First();
                //Debug.WriteLine($"Best cnt: {best.Value}");
                return best.Key;
            }
            else
            {
                return _cells[0, 0].Neighbors[_rnd.Next(_cells[0, 0].Neighbors.Length)].Color;
            }

        }

        private Color GetRandomColor()
        {
            int rndIdx = _rnd.Next(_numColors);
            return ColorHelper.Colors[rndIdx];
        }

        private void Test(bool alt = false)
        {
            var rnd = new Random();

            var games = new List<List<Color>>();
            var bestCount = int.MaxValue;

            int n = 200;

            //var ogCopy = ResizeBidimArrayWithElements(InitCells(_boardSize), _boardSize.X, _boardSize.Y);
            //var ogCopy = ResizeBidimArrayWithElements(_cells, _boardSize.X, _boardSize.Y);



            for (int i = 0; i < n; i++)
            {
                //_cells = ogCopy;
                games.Add(new List<Color>());

                ResetFilled();
                ResetVisited();
                ResetColors();
                ProcessColorClick(_cells[0, 0], _cells[0, 0].Color);
                Color lastColor = Color.White;

                if (alt)
                {
                    while (!GameOver())
                    {
                        var color = MostCommonTouchingColor();

                        SetCompletedCellsColor(color);
                        ResetFilled();
                        ResetVisited();
                        ProcessColorClick(_cells[0, 0], color);
                        SetCompletedCellsColor(color);

                        games.Last().Add(color);

                        CellBox.Invalidate();
                        CellBox.Refresh();
                        Application.DoEvents();
                        //Task.Delay(25).Wait();
                    }

                    break;
                }
                else
                {

                    bool first = true;

                    while (!GameOver())
                    {
                        //Color color = Color.White;
                        //if (first)
                        //    color = MostCommonTouchingColor();
                        //else
                        //    color = ColorHelper.Colors[rnd.Next(_numColors)];

                        //var color = MostCommonTouchingColor();

                        var color = ColorHelper.Colors[rnd.Next(_numColors)];

                        var remaining = RemainingColors();

                        //while (!remaining.Contains(color))
                        //    color = ColorHelper.Colors[rnd.Next(_numColors)];

                        while (color == lastColor || !remaining.Contains(color))
                            color = ColorHelper.Colors[rnd.Next(_numColors)];

                        lastColor = color;

                        SetCompletedCellsColor(color);
                        ResetFilled();
                        ResetVisited();
                        ProcessColorClick(_cells[0, 0], color);
                        SetCompletedCellsColor(color);

                        games.Last().Add(color);

                        if (games.Last().Count > bestCount)
                            break;

                        //CellBox.Invalidate();
                        //CellBox.Refresh();
                        //Application.DoEvents();
                        //Task.Delay(200).Wait();
                    }
                }

                //CellBox.Invalidate();
                //CellBox.Refresh();
                //Application.DoEvents();
                //Task.Delay(200).Wait();

                bestCount = Math.Min(bestCount, games.OrderBy(g => g.Count).First().Count);

            }

            ResetFilled();
            ResetVisited();
            ResetColors();
            ProcessColorClick(_cells[0, 0], _cells[0, 0].Color);

            games = games.OrderBy(g => g.Count).ToList();


            Debug.WriteLine($"Best Solution (Moves: {games.First().Count}): ");
            foreach (var move in games.First())
            {
                Debug.WriteLine($"{move.ToString()}");
            }
        }

        private Cell[,] CopyBoard(Cell[,] cells)
        {
            var copy = new Cell[_boardSize.X, _boardSize.Y];

            for (int x = 0; x < _boardSize.X; x++)
            {
                for (int y = 0; y < _boardSize.Y; y++)
                {
                    copy[x, y] = new Cell(_cells[x, y]);
                }
            }

            return copy;
        }

        public T[,] ResizeBidimArrayWithElements<T>(T[,] original, int rows, int cols)
        {

            T[,] newArray = new T[rows, cols];
            int minX = Math.Min(original.GetLength(0), newArray.GetLength(0));
            int minY = Math.Min(original.GetLength(1), newArray.GetLength(1));

            for (int i = 0; i < minX; ++i)
                Array.Copy(original, i * original.GetLength(1), newArray, i * newArray.GetLength(1), minY);

            return newArray;
        }

        private void CellBox_Paint(object sender, PaintEventArgs e)
        {
            DrawCells(e.Graphics);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _cells = InitCells(_boardSize);
            ProcessColorClick(_cells[0, 0], _cells[0, 0].Color);
            var best = MostCommonTouchingColor();

            Debug.WriteLine($"Best color: {best.ToString()}");


            CellBox.Invalidate();
            CellBox.Refresh();
        }

        private void CellBox_MouseDown(object sender, MouseEventArgs e)
        {

            //var clickedIdx = new Point(e.Location.X / CellBox.Width, e.Location.Y / CellBox.Height);
            var clickedIdx = new Point(e.Location.X / _sideLen, e.Location.Y / _sideLen);

            _selectedCell = _cells[clickedIdx.X, clickedIdx.Y];

            Debug.WriteLine(_selectedCell.ToString());


            CellBox.Invalidate();
            CellBox.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Test();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Test(true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var best = MostCommonTouchingColor();

            Debug.WriteLine($"Best color: {best.ToString()}");
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetColors();
            ResetVisited();
            ResetFilled();

            CellBox.Invalidate();
            CellBox.Refresh();
        }
    }
}