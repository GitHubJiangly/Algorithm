using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AStarPathSearch
{
    public class MButton : Button
    {
        public Point MapPosition { get; set; }
        public int Statu { get; set; }
        public MButton()
            : base()
        {
            Statu = 0;
        }
    }

}
