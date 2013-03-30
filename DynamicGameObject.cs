using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace AsciiBirds
{
    class DynamicGameObject
    {
        public Body Body { get; private set; }

        public char Symbol { get; protected set; }

        public DynamicGameObject(Body body)
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