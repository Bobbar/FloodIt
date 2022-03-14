using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FloodIt.Solvers
{
    public class BestColorSolver : Solver
    {
        public BestColorSolver(Board board) : base(board) { }

        public override void Solve()
        {
            _board.ResetAll();

            var moves = new List<Color>();
            
            while (!_board.GameOver())
            {
                var color = Helpers.MostCommonTouchingColor(_board);

                _board.DoColorFill(color);

                moves.Add(color);

                _board.Redraw();
            }

            _board.ResetAll();

            Debug.WriteLine($"[BestColor] Best Solution (Moves: {moves.Count}): ");

        }
    }
}
