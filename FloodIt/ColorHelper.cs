using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FloodIt
{
    internal static class ColorHelper
    {
        private static Random _rnd = new Random();

        public static Color[] Colors = new Color[]
        {
            Color.Green,
            Color.DarkOrange,
            Color.Red,
            Color.Yellow,
            Color.Magenta,
            Color.Aqua,
            Color.CornflowerBlue,
            Color.Purple
        };

        public static Color RandomColor(int numColors)
        {
            return Colors[_rnd.Next(numColors)];
        }
    }
}
