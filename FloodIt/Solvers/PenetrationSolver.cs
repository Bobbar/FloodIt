using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FloodIt.Solvers
{
    public class PenetrationSolver : Solver
    {
        public PenetrationSolver(Board board) : base(board) { }

        public override void Solve()
        {
            _board.ResetAll();

            var moves = new List<Color>();
            var colors = _board.AvailableColors();

            while (!_board.GameOver())
            {
                float bestDist = float.MaxValue;
                Color bestColor = Color.Transparent;

                if (_board.EndCell.IsFilled == false)
                {
                    foreach (var color in colors)
                    {

                        _board.SetFilledColor(color);

                        var minDist = MinDistToEnd(color);

                        if (minDist < bestDist)
                        {
                            bestDist = minDist;
                            bestColor = color;
                        }

                    }
                }
                else
                {
                    bestColor = Helpers.MostCommonTouchingColor(_board);
                }

                _board.DoColorFill(bestColor);

                _board.Redraw();

                moves.Add(bestColor);
            }

            _board.ResetAll();

            Debug.WriteLine($"[Penetration] Best Solution (Moves: {moves.Count}): ");
        }

        private float MinDistToEnd(Color color)
        {
            var effected = GetEffectedCells(color);
            var end = _board.EndCell;

            float minDist = float.MaxValue;

            if (effected.Count == 0)
                return minDist;

            foreach (var cell in effected)
            {
                var dist = cell.GridIdx.DistanceSqrt(end.GridIdx);
                minDist = Math.Min(minDist, dist);
            }

            return minDist;
        }

        private List<Cell> GetEffectedCells(Color color)
        {
            var effected = new List<Cell>();

            _board.ResetVisited();

            var filled = _board.GetFilledCells();

            var queue = new Queue<Cell>();

            foreach (var cell in filled)
                queue.Enqueue(cell);

            while (queue.Count > 0)
            {
                var c = queue.Dequeue();

                c.Visited = true;

                if (c.Color == color && c.IsFilled == false)
                {
                    effected.Add(c);
                }

                foreach (var n in c.Neighbors)
                {
                    if (n.Visited == false && n.IsFilled == false && n.Color == color)
                        queue.Enqueue(n);

                    n.Visited = true;
                }
            }

            return effected;
        }
    }
}
