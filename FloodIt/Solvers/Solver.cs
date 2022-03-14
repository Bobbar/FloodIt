using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FloodIt.Solvers
{
    public abstract class Solver
    {
        protected Board _board;

        public Solver(Board board)
        {
            _board = board;
        }


        public abstract void Solve();
    }
}
