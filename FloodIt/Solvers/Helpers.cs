using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FloodIt.Solvers
{
    public static class Helpers
    {
        public static Color MostCommonTouchingColor(Board board)
        {
            board.ResetVisited();
            var counts = new Dictionary<Color, int>();
            var colors = board.AvailableColors();

            var edgeCells = new List<Cell>();

            foreach (var cell in board.Cells)
            {
                if (cell.IsFilled)
                {
                    var edgeNs = cell.Neighbors.Where(c => c.IsFilled == false).ToList();
                    edgeCells.AddRange(edgeNs);
                }
            }

            edgeCells = edgeCells.Distinct().ToList();


            foreach (var color in colors)
            {
                board.ResetVisited();

                if (!counts.ContainsKey(color))
                    counts.Add(color, 0);

                foreach (var cell in edgeCells)
                {
                    if (cell.Color == color)
                    {
                        cell.Visited = true;
                        counts[color]++;
                        counts[color] += CountTouching(color, cell);
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
                Debugger.Break();
                return Color.Empty;
                //return _cells[0, 0].Neighbors[_rnd.Next(_cells[0, 0].Neighbors.Length)].Color;
            }

        }


        public static int CountTouching(Color testColor, Cell cell)
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
    }
}
