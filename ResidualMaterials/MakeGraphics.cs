using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ResidualMaterials
{
    class MakeGraphics
    {
        int x = MyDtTable.itemToCutFrom.W * 3;
        int y = MyDtTable.itemToCutFrom.Length * 3;
        string name = MyDtTable.itemToCutFrom.Name.ToString();

        public void CreateShape(PictureBox pctBox)
        {
            Pen greenPen = new Pen(Color.FromArgb(255, 0, 255, 0), 3);
            pctBox.Image = (Image)new Bitmap(pctBox.Width, pctBox.Height);

            Image bmp = pctBox.Image;
           
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            g.DrawLine(greenPen, new Point(0, 0), new Point(y, 0));
            g.DrawLine(greenPen, new Point(0, 0), new Point(0, x));//1st vertical
            g.DrawLine(greenPen, new Point(y, 0), new Point(y, x));
            g.DrawLine(greenPen, new Point(0, x), new Point(y, x));
            

        }

        public void SaveShapeinFile(PictureBox pctBox)
        {      
            pctBox.Image.Save(@"D:\\Visual Studio Projects\\" + name, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public void PanelSize(Panel p, int x, int y)
        {
            p.Width = x*3;
            p.Height = y*3;
        }
        
    }
}
