using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeClient
{
    public class GameMove
    {
        public Guid PlayerId { get; set; }

        public Color? Color { get; set; }

        public int Section { get; set; }

        public int Number { get; set; }
    }
}
