using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace GameUserInterface
{
    static class Program
    {
        public static void Main()
        {
            GameSettings gameForm = new GameSettings();
            gameForm.ShowDialog();
        }
    }
}
