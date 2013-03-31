using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace AsciiBirds
{
    class Box : GameObject
    {
        public Box(Body body) : base(body)
        {
            Symbol = '#';
        }
        
    }

    class Pig : GameObject
    {
        public Pig(Body body) : base(body)
        {
            Color = ConsoleColor.Green;
            Symbol = 'P';
        }

        public void Die()
        {
            Symbol = '+';
        }
    }

    class Bird : GameObject
    {
        public Bird(Body body) : base(body)
        {
            Color = ConsoleColor.Red;
            Angle = 30;
            Power = 60;
            Symbol = 'v';
        }

        public int Angle { get; set; }

        public int Power { get; set; }

        public void Shoot()
        {
            var dx = (float) (Power * Math.Cos(Angle/180.0*Math.PI));
            var dy = (float) (Power * Math.Sin(Angle/180.0*Math.PI));
            Body.ApplyLinearImpulse(new Vector2(dx,dy));
        }
    }

    class GameObject
    {
        public Body Body { get; private set; }

        public char Symbol { get; protected set; }

        public GameObject(Body body)
        {
            Color = ConsoleColor.White;
            Body = body;
            Symbol = ' ';
        }

        public ConsoleColor Color { get; protected set; }

        public void Draw(Canvas canvas)
        {
            canvas.DrawSquare((int) Body.Position.X, (int) Body.Position.Y, Color, Symbol);
        }

        internal static Body CreateDynamicBody(
            World world, float x, float y, bool awake = false, float restitution = 0.1f, float density = 1.0f)
        {
            Body body = BodyFactory.CreateRectangle(world, 1.0f, 1.0f, density, new Vector2(x + 0.5f, y - 0.5f));
            body.BodyType = BodyType.Dynamic;
            body.SleepingAllowed = true;
            body.Awake = awake;
            body.Restitution = restitution;
            body.Friction = 0.5f;

            return body;
        }
    }
}