using System;
using FarseerPhysics.Dynamics;

namespace AsciiBirds
{
    class Pig : DynamicGameObject
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
}