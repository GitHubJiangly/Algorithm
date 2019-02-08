using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AStarPathSearch
{

    public partial class Form1 : Form
    {
        public int X = 10;
        public int Y = 10;
        private AStar ast = new AStar();

        public Form1()
        {
            InitializeComponent();
            drawButtons();
            button2_Click(null, null);
            ast.InitMap(X, Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindButtonsToMap();
            List<PathNode> path = ast.FindPath();
            foreach (PathNode item in path)
            {
                DrawPath(item.Position);
            }
        }

        private void DrawPath(Point pos)
        {
            foreach (MButton item in bts)
            {
                if(item.MapPosition.Equals(pos))
                    item.BackColor = Color.DarkOrange;
            }
        }

        private void BindButtonsToMap()
        {
            foreach( MButton item in bts)
            {
                switch (item.Statu)
                {
                    case 0:
                        ast.SetNodeValue(item.MapPosition, 0);
                        break;
                    case 1:
                        ast.SetNodeValue(item.MapPosition, -1);
                        break;
                    case 2:
                        ast.SetNodeValue(item.MapPosition, 0);
                        ast.Start = item.MapPosition;
                        break;
                    case 3:
                        ast.SetNodeValue(item.MapPosition, 0);
                        ast.End = item.MapPosition;
                        break;
                }
            }
        }

        private List<MButton> bts = new List<MButton>();

        private void drawButtons()
        {
            int bx, by;
            for(int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    MButton b = new MButton();
                    b.MapPosition = new Point(i, j);
                    b.Size = new Size(25, 25);
                    b.Click += new EventHandler(Buttons_Click);
                    bx = i * 25 + 10;
                    by = j * 25 + 10;
                    b.Location = new Point(bx, by);
                    bts.Add(b);
                    this.panel1.Controls.Add(b);
                }
            }
        }

        private void Buttons_Click(object sender, EventArgs args)
        {
            MButton msender = (MButton)sender;

            switch (msender.Statu)
            {
                case 0:
                    msender.Text = "X";
                    msender.Statu = 1;
                    break;
                case 1:
                    msender.Text = "S";
                    msender.Statu = 2;
                    break;
                case 2:
                    msender.Text = "E";
                    msender.Statu = 3;
                    break;
                case 3:
                    msender.Text = "";
                    msender.Statu = 0;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (MButton item in bts)
            {
                item.BackColor = Color.Azure;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            foreach (MButton item in bts)
            {
                item.BackColor = Color.Azure;
                item.Statu = 0;
                item.Text = "";
            }
        }
    }
}
