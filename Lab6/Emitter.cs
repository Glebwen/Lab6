using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Lab6
{
    public class Emitter
    {
        List<Particle> particles = new List<Particle>();
        public int MousePositionX;
        public int MousePositionY;

        public float GravitationX = 0;
        public float GravitationY = 1;



        public List<ImpactPoints> impactPoints = new List<ImpactPoints>();
        public List<Fire> fires = new List<Fire>();

        public List<float> XCors = new List<float>{350f, 450f, 350f, 450f, 350f, 450f};
        public List<float> YCors = new List<float>{110f, 110f, 185f, 185f, 265f, 265f };

        public int ParticlesCount = 500;

        public int X; 
        public int Y; 
        public int Direction = 0; 
        public int Spreading = 360; 
        public int SpeedMin = 1;
        public int SpeedMax = 10;
        public int RadiusMin = 2;
        public int RadiusMax = 10;
        public int LifeMin = 20;
        public int LifeMax = 100;

        public int ParticlesPerTick = 1;

        public Color ColorFrom = Color.White;
        public Color ColorTo = Color.FromArgb(0, Color.Black);

        public static Random rand = new Random();

        public virtual void ResetParticle(Particle particle)
        {
            particle.Life = Particle.rand.Next(LifeMin, LifeMax);

            particle.X = X;
            particle.Y = Y;

            var direction = Direction
                + (double)Particle.rand.Next(Spreading)
                - Spreading / 2;

            var speed = Particle.rand.Next(SpeedMin, SpeedMax);

            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            particle.Radius = Particle.rand.Next(RadiusMin, RadiusMax);
        }

        public virtual Particle CreateParticle()
        {
            var particle = new ParticleColorful();
            particle.FromColor = ColorFrom;
            particle.ToColor = ColorTo;

            return particle;
        }

        public void UpdateState()
        {

            int particlesToCreate = ParticlesPerTick;

            foreach (var particle in particles)
            {
                particle.Life -= 1;
                if (particle.Life <= 0)
                {
                    if (particlesToCreate > 0)
                    {
                        particlesToCreate -= 1;
                        ResetParticle(particle);
                    }
                }
                else
                {
                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;

                    foreach (var point in impactPoints)
                    {
                        point.ImpactParticle(particle);
                    }
                    foreach (var point in fires)
                    {
                        point.ImpactParticle(particle);
                    }

                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;


                }
            }
            while (particlesToCreate >= 1)
            {
                particlesToCreate -= 1;
                var particle = CreateParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }

            foreach (var fire in fires)
            {
                if (fire.life <= 0)
                {
                    fire.life = 100;
                    int windownum = rand.Next(5);
                    fire.X = XCors[windownum];
                    fire.Y = YCors[windownum];
                }
            }
        }

        public void Render(Graphics g)
        {
            foreach (var point in fires)
            {
                point.Render(g);
            }
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }

            foreach (var point in impactPoints)
            {
                point.Render(g);
            }

        }
    }

    public class TopEmitter : Emitter
    {
        public int Width;

        public override void ResetParticle(Particle particle)
        {
            base.ResetParticle(particle); 
            particle.X = X + Particle.rand.Next(Width) - Width/2; 
            particle.Y = Y;

            particle.SpeedY = 1; 
            particle.SpeedX = Particle.rand.Next(-1, 1);
        }
    }
}
