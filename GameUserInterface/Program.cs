using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GameUserInterface
{
    static class Program
    {
        [STAThread]
        public static void Main()
        {
            //GameSettings gameForm = new GameSettings();
            //gameForm.ShowDialog();
            
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainGame(5, 4, "Holy", "Moly"));
            Application.Run(new GameSettings());
        }
    }
}
