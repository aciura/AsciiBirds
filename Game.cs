using System;
using System.Collections.Generic;
using System.Threading;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace AsciiBirds
{

    class Game
    {
        private readonly World _world = new World(new Vector2(0f, 9.82f));
        private const int TimeStepInMs = 33/*ms*/;
        private bool _isRunning = true;

        private float ViewportWidth { get; set; }
        private float ViewportHeight { get; set; }

        private Body _groundBody;
        
        private Fixture _groundFix;
        private Bird _bird;
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        private readonly Canvas _canvas;

        public Game()
        {
            _canvas = new Canvas();
        }

        internal void Init()
        {
            ViewportWidth = Console.WindowWidth;
            ViewportHeight = Console.WindowHeight;
            
            /* Ground */
            var groundPosition = new Vector2(ViewportWidth/2f, ViewportHeight);

            // Create the ground fixture
            _groundBody = BodyFactory.CreateBody(_world, position:groundPosition);
            _groundBody.IsStatic = true;
            _groundBody.Restitution = 0.3f;
            _groundBody.Friction = 0.5f;
            
            Vertices rectVerts = PolygonTools.CreateRectangle(ViewportWidth/2f, 1f);            
            //We create a circle shape with a radius of 0.5 meters
            var rectangleShape = new PolygonShape(rectVerts, 1.0f);
            
            //We fix the body and shape together using a Fixture object
            _groundFix = _groundBody.CreateFixture(rectangleShape);
            _groundBody.Awake = false;

            //We create a body object and make it dynamic (movable)
            //var myBody = CreateDynamicBody(ViewportWidth/2f, 0f, awake:true, restitution:0.5f);
            //_gameBodies.Add(myBody);

            const int numberOfBoxes = 10;
            for (int i = 0; i < numberOfBoxes; i++)
            {
                _gameObjects.Add(new Box(
                    GameObject.CreateDynamicBody(
                        _world, ViewportWidth - 30, ViewportHeight - 1 - i, awake:false, restitution:0f, density: 2f)));
                _gameObjects.Add(new Box(GameObject.CreateDynamicBody(
                        _world, ViewportWidth - 31, ViewportHeight - 1 - i, awake: false, restitution: 0f, density: 2f)));
                _gameObjects.Add(
                    new Box(GameObject.CreateDynamicBody(
                        _world, ViewportWidth - 32, ViewportHeight - 1 - i, awake: false, restitution: 0f, density: 2f)));
            }

            _gameObjects.Add(
                new Pig(
                    GameObject.CreateDynamicBody(_world, ViewportWidth - 31, ViewportHeight - 11, awake: false, restitution: 0.0f)));
            
            //Create a bird
            _bird = new Bird(GameObject.CreateDynamicBody(_world, 0, ViewportHeight - 1, awake: false, restitution: 0.6f));
            _gameObjects.Add(_bird);
            
        }

        

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected void Update(int miliseconds)
        {
            HandleKeyboard();
            _world.Step(miliseconds * 0.001f);
        }

        /// <summary>
        /// Handle any waiting user keystrokes 
        /// </summary>
        private void HandleKeyboard()
        {
            //Thread.Sleep(50);
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    _bird.Shoot();
                }
                if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
                {
                    _bird.Angle += 1;
                }
                if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
                {
                    _bird.Angle -= 1;
                }
                if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    _bird.Power -= 1;
                }
                if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
                {
                    _bird.Power += 1;
                }
                if (keyInfo.Key == ConsoleKey.R)
                {
                    _gameObjects.Remove(_bird);
                    _bird = new Bird(
                        GameObject.CreateDynamicBody(_world, 0, ViewportHeight - 1, awake: false, restitution: 0.6f));
                    _gameObjects.Add(_bird);
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    _isRunning = false;
                }
                //else string key = keyInfo.KeyChar.ToString().ToUpper();
            }
        }

        public void Run()
        {
            while(_isRunning)
            {
                Thread.Sleep(TimeStepInMs);
                Update(TimeStepInMs);
                DrawGame();
            }
        }

        private void DrawGame()
        {
            _canvas.Clear();

            _canvas.DrawGround(_groundFix, (int) ViewportWidth-1, _bird.Angle, _bird.Power);

            foreach (var gameObj in _gameObjects)
            {
                gameObj.Draw(_canvas);
            }
        }
    }
}