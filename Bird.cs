using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace AsciiBirds
{
    class Bird : DynamicGameObject
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
}
