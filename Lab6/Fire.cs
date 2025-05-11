using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Fire : ImpactPoints
    {
        public int life = 100;
        public int Radius = 50;

        public static Random rand = new Random();

        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            double r = Math.Sqrt(gX * gX + gY * gY); // считаем расстояние от центра точки до центра частицы
            if (r + particle.Radius < Radius / 2) // если частица оказалось внутри окружности
            {
                particle.Life = 0;
                life -= 1;
            }
        }

        public override void Render(Graphics g)
        {
            g.DrawEllipse(
                   new Pen(Color.Red),
                   X - Radius / 2,
                   Y - Radius / 2,
                   Radius,
                   Radius
               );

            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // обязательно выносим текст и шрифт в переменные
            var text = $"{life}";
            var font = new Font("Verdana", 10);

            // вызываем MeasureString, чтобы померить размеры текста
            var size = g.MeasureString(text, font);

            // рисуем подложнку под текст
            g.FillRectangle(
                new SolidBrush(Color.Red),
                X - size.Width / 2, // так как я выравнивал текст по центру то подложка должна быть центрирована относительно X,Y
                Y + 20 - size.Height / 2,
                size.Width,
                size.Height
            );

            // ну и текст рисую уже на базе переменных
            g.DrawString(
                text,
                font,
                new SolidBrush(Color.White),
                X,
                Y+20,
                stringFormat
            );
        }
    }
}
