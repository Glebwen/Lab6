using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class BaseObject
    {
        public float X;
        public float Y;
        public float Angle;

        public BaseObject(float x, float y, float angle)
        {
            X = x;
            Y = y;
            Angle = angle;
        }

        public Matrix GetTransform()
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);
            return matrix;
        }

        public virtual void Render(Graphics g)
        {
        }
    }
    public class Rect: BaseObject
    {
        public int SizeX = 0;
        public int SizeY = 0;
        public Color Color;
        public Rect(float x, float y, float angle, int sizeX, int sizeY, Color color) : base(x, y, angle)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Color = color;
        }

        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color), -0.5f * SizeX, -0.5f * SizeY, SizeX, SizeY);
        }
    }
}
