using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_WPF
{
   public class GameStatusEventArgs : EventArgs
    {
        public int Score { get;private set; }

        public GameStatusEventArgs(int score)
        {
            Score = score;
        }
    }
}
