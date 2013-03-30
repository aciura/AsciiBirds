using FarseerPhysics.Dynamics;

namespace AsciiBirds
{
    class Box : DynamicGameObject
    {
        public Box(Body body) : base(body)
        {
            Symbol = '#';
        }
        
    }
}