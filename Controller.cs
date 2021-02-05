using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{
    class Controller
    {
        public List<Asteroid> asteroids = new List<Asteroid>(); // create a List for multiple asteroids
        public double timer = 2D; // D as in Double
        public double maxTime = 2D;
        public int nextSpeed = 240;
        public bool inGame = false;  // menu
        public float totalTime = 0f;


        public void conUpdate(GameTime gameTime)
        {
            if (inGame)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                KeyboardState kState = Keyboard.GetState();
                if (kState.IsKeyDown(Keys.Enter))
                {
                    inGame = true;
                    totalTime = 0f;
                    timer = 2D;
                    maxTime = 2D;
                    nextSpeed = 240;
                }
            }
            
            if (timer <= 0)
            {
                asteroids.Add(new Asteroid(nextSpeed));     //create asteroid every two sec
                timer = 2D;

                if (maxTime > 0.5)
                {
                    maxTime -= 0.1D;
                }

                if (nextSpeed < 720)
                {
                    nextSpeed += 4;
                }
            }
        } 
    }
}
