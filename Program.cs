using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;


namespace AsciiBirds
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.Init();
            game.Run();
        }

    }
}
