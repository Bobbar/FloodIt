using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FloodIt.Solvers
{
    public class BruteForceSolver : Solver
    {
        private Random _rnd = new Random();

        private int _numIterations = 50;

        public BruteForceSolver(Board board, int iterations) : base(board)
        {
            _numIterations = iterations;
        }

        public override void Solve()
        {
            _board.ResetAll();

            var solutions = new List<List<Color>>();
            var bestCount = int.MaxValue;


            for (int i = 0; i < _numIterations; i++)
            {
                solutions.Add(new List<Color>());

                _board.ResetAll();

                Color lastColor = Color.White;

                while (!_board.GameOver())
                {
                    var color = ColorHelper.RandomColor(_board.NumColors);
                    var remaining = _board.RemainingColors();

                    while (color == lastColor || !remaining.Contains(color))
                        color = ColorHelper.RandomColor(_board.NumColors);

                    lastColor = color;

                    _board.DoColorFill(color);

                    solutions.Last().Add(color);

                    if (solutions.Last().Count > bestCount)
                        break;
                }

                bestCount = Math.Min(bestCount, solutions.OrderBy(g => g.Count).First().Count);

            }

            _board.ResetAll();

            solutions = solutions.OrderBy(g => g.Count).ToList();
            var best = solutions.First();

            foreach (var color in best)
            {
                _board.DoColorFill(color);

                _board.Redraw();
            }

            Debug.WriteLine($"[Brute] Best Solution (Moves: {best.Count}): ");
        }
    }
}
