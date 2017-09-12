using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ResidualMaterials
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            instance = new MakeGraphics();
            instance.CreateShape(pictureBox1);
            instance.PanelSize(panel1,MyDtTable.widthWP,MyDtTable.lengthWP);
        }
        MakeGraphics instance;
        private void backToForm1_Click(object sender, EventArgs e)
        {
            instance.SaveShapeinFile(pictureBox1);
            this.Hide();
        }
        private bool clicked;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clicked = true;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicked)
            {
                Point p = new Point();//in form coordinates
                p.X = e.X + panel1.Left;
                p.Y = e.Y + panel1.Top;
                panel1.Left = p.X;
                panel1.Top = p.Y;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            clicked = false;
        }


        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int temp = panel1.Width;
            panel1.Width = panel1.Height;
            panel1.Height = temp;
        }
    }
}
