using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;
using Priority_Queue;
using FloodIt.Solvers;

namespace FloodIt
{
    public partial class Form1 : Form
    {
        private Board _board;

        public Form1()
        {
            InitializeComponent();
            InitBoard();
        }

        private void InitBoard()
        {
            var dimText = BoardSizeTextBox.Text.Trim().Split(',');
            if (dimText.Length > 2 || dimText.Length < 2)
                return;

            Point dims = new Point();
            int colors = 4;

            if (int.TryParse(dimText[0], out int dimX) && int.TryParse(dimText[1], out int dimY) && int.TryParse(NumColorsTextBox.Text.Trim(), out int nColors))
            {
                dims.X = dimX;
                dims.Y = dimY;

                if (nColors < 2 || nColors > ColorHelper.Colors.Length)
                    return;

                colors = nColors;
            }
            else
            {
                return;
            }

            if (_board != null)
                _board.RedrawRequested -= board_RedrawRequested;
            _board = new Board(dims, colors);
            _board.RedrawRequested += board_RedrawRequested;

            InitColorBoxes(colors);
        }

        private void InitColorBoxes(int colors)
        {
            ColorButtons.Controls.Clear();
            ColorButtons.RowCount = colors;
            ColorButtons.RowStyles.Clear();

            for (int i = 0; i < colors; i++)
            {
                var color = ColorHelper.Colors[i];
                var button = new Button() { BackColor = color, Dock = DockStyle.Fill };
                button.Click += ColorButton_Click;
                button.Tag = color;
                ColorButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 50.0f));
                ColorButtons.Controls.Add(button, 0, i);
            }
        }

        private void ColorButton_Click(object? sender, EventArgs e)
        {
            var button = sender as Button;
            var color = (Color)button.Tag;

            _board.DoColorFill(color);


            if (_board.GameOver())
                Debug.WriteLine("GameOver!!!!");

            _board.Redraw();
        }

        private void DrawCells(Graphics gfx)
        {
            _board.DrawCells(gfx);
        }

        private void CellBox_Paint(object sender, PaintEventArgs e)
        {
            DrawCells(e.Graphics);
        }

        private void board_RedrawRequested(object? sender, EventArgs e)
        {
            CellBox.Invalidate();
            CellBox.Refresh();
        }

        private void NewBoardButton_Click(object sender, EventArgs e)
        {
            InitBoard();

            _board.Redraw();
        }

        //private void CellBox_MouseDown(object sender, MouseEventArgs e)
        //{
        //    var clickedIdx = new Point(e.Location.X / _sideLen, e.Location.Y / _sideLen);

        //    _selectedCell = _board.Cells[clickedIdx.X, clickedIdx.Y];

        //    Debug.WriteLine(_selectedCell.ToString());


        //    CellBox.Invalidate();
        //    CellBox.Refresh();
        //}

        private void PenetrationSolverButton_Click(object sender, EventArgs e)
        {
            var solver = new PenetrationSolver(_board);
            solver.Solve();
        }

        private void BestColorSolverButton_Click(object sender, EventArgs e)
        {
            var solver = new BestColorSolver(_board);
            solver.Solve();
        }

        private void BruteForceSolverButton_Click(object sender, EventArgs e)
        {
            int iterations = 10;

            if (int.TryParse(BruteForceItsTextBox.Text.Trim(), out int its))
                iterations = its;
            else
                return;

            var solver = new BruteForceSolver(_board, iterations);
            solver.Solve();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            _board.ResetAll();
            _board.Redraw();
        }
    }
}