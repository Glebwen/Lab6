using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter;
        Emitter emitter1;
        Emitter emitter2;
        Fire fire1;
        Fire fire2;
        Rect Brons;
        Rect Shlang;

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            Brons = new Rect(50, 300, 0, 40, 20, Color.Gold);
            Shlang = new Rect(15, 335, -45, 100, 15, Color.Brown);

            this.emitter = new Emitter
            {
                Direction = 0,
                Spreading = 2,
                SpeedMin = 9,
                SpeedMax = 11,
                ColorFrom = Color.Blue,
                ColorTo = Color.FromArgb(0, Color.White),
                ParticlesPerTick = 10,
                X = 50,
                Y = 300,
            };

            emitters.Add(this.emitter);

            fire1 = new Fire
            {
                X = picDisplay.Width / 4,
                Y = picDisplay.Height / 4,
                life = 0,
            };

            emitter.fires.Add(fire1);

            fire2 = new Fire
            {
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 4,
                life = 0,
            };

            emitter.fires.Add(fire2);


            this.emitter1 = new TopEmitter
            {
                Width = 30,
                GravitationY = -0.25f,
                ColorFrom = Color.Orange,
                ColorTo = Color.FromArgb(0, Color.Yellow),
                ParticlesPerTick = 10,
                X = (int)fire1.X,
                Y = (int)fire1.Y,
                LifeMax = 50,
                ParticlesCount = 10
            };

            emitters.Add(this.emitter1);

            this.emitter2 = new TopEmitter
            {
                Width = 30,
                GravitationY = -0.25f,
                ColorFrom = Color.Orange,
                ColorTo = Color.FromArgb(0, Color.Yellow),
                ParticlesPerTick = 10,
                X = (int)fire2.X,
                Y = (int)fire2.Y,
                LifeMax = 50,
                ParticlesCount = 10
            };

            emitters.Add(this.emitter2);



            

        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter1.X = (int)fire1.X;
            emitter1.Y = (int)fire1.Y;
            emitter2.X = (int)fire2.X;
            emitter2.Y = (int)fire2.Y;
            foreach (var emitter in emitters)
            {
                emitter.UpdateState();
            }
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.LightBlue);

                //дом
                g.FillRectangle(new SolidBrush(Color.Gray), 300, 50, 200, 300);
                g.FillRectangle(new SolidBrush(Color.Black), 325, 75, 50, 60);
                g.FillRectangle(new SolidBrush(Color.Black), 425, 75, 50, 60);
                g.FillRectangle(new SolidBrush(Color.Black), 325, 150, 50, 60);
                g.FillRectangle(new SolidBrush(Color.Black), 425, 150, 50, 60);
                g.FillRectangle(new SolidBrush(Color.Black), 325, 225, 50, 60);
                g.FillRectangle(new SolidBrush(Color.Black), 425, 225, 50, 60);

                foreach (var emitter in emitters)
                {
                    emitter.Render(g);
                }
                //бронсбойд
                Brons.Angle = -1 * tbDirection.Value;
                g.Transform = Shlang.GetTransform();
                Shlang.Render(g);
                g.Transform = Brons.GetTransform();
                Brons.Render(g);

            }

            picDisplay.Invalidate();
        }
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            emitter.MousePositionX = e.X;
            emitter.MousePositionY = e.Y;
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value;
            lblDirection.Text = $"{tbDirection.Value}°";
        }

        private void tbForce_Scroll(object sender, EventArgs e)
        {
            emitter.SpeedMin = tbForce.Value-1;
            emitter.SpeedMax = tbForce.Value+1;
            forceLabel.Text = tbForce.Value.ToString();
        }
    }
}
