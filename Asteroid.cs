﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{
    class Asteroid
    {
        public Vector2 position;
        public int speed = 220;
        public int radius = 59;
        static Random rand = new Random();
        public bool offScreen = false;

        public Asteroid(int newSpeed)
        {
            speed = newSpeed;
            
            position = new Vector2(1280 + radius, rand.Next(0,721));
        }

        public void asteroidUpdate(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= speed * dt;
        }
    }
}
