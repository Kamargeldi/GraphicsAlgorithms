using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphicsModeler.Parser;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Numerics;
using GraphicsModeler.Extensions;
using GraphicsModeler.Helper;

namespace GraphicsModeler.MainWindow
{
    public partial class MainWindow : Form
    {
        private ObjectFileParser parser;
        private Model model;

        public MainWindow()
        {
            InitializeComponent();
            _canvas.Size = this.ClientSize;
            _canvas.Image = new Bitmap(_canvas.Width, _canvas.Height);
            parser = new ObjectFileParser(@"C:\Users\Kamar\Desktop\gun.obj");
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            model = parser.GetModel();
            model.Translation = new Vector3(_canvas.Width / 2, _canvas.Height / 2, 0);
            model.ScaleFactor = 150f;
            
            _drawTimer.Enabled = true;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                model.Rotation.X += 0.2f;
            }
            else if (e.KeyCode == Keys.Up)
            {
                model.Rotation.X -= 0.2f;
            }
            else if (e.KeyCode == Keys.S)
            {
                model.ScaleFactor -= 2f;
            }
            else if (e.KeyCode == Keys.W)
            {
                model.ScaleFactor += 2f;
            }
            else if (e.KeyCode == Keys.A)
            {
                model.Rotation.Z -= 0.2f;
            }
            else if (e.KeyCode == Keys.D)
            {
                model.Rotation.Z += 0.2f;
            }
            else if (e.KeyCode == Keys.Left)
            {
                model.Rotation.Y -= 0.2f;
            }
            else if (e.KeyCode == Keys.Right)
            {
                model.Rotation.Y += 0.2f;
            }
        }


        private void MainWindow_Resize(object sender, EventArgs e)
        {
            _canvas.Size = this.ClientSize;
            if (model != null)
            {
                model.Translation = new Vector3(_canvas.Width / 2, _canvas.Height / 2, 0);
            }
        }

        private void _drawTimer_Tick(object sender, EventArgs e)
        {
            var bmp = new ExtendedBitmap(_canvas.Width, _canvas.Height);
            model.DrawToBitmap(bmp);
            _canvas.Image = bmp.Source;
        }

        private void _canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
