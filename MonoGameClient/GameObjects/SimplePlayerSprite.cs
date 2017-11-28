﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using CommonDataItems;
using Engine.Engines;
using Microsoft.Xna.Framework.Input;
using Microsoft.AspNet.SignalR.Client;

namespace Sprites
{
    public class SimplePlayerSprite :DrawableGameComponent
    {
        public Texture2D Image;
        public Point Position;
        public Rectangle BoundingRect;
        public bool Visible = true;
        public Color tint = Color.White;
		public PlayerData pData;
        public Point previousPosition;		
        public int speed = 5;
        private int delay;

        // Constructor epects to see a loaded Texture
        // and a start position
        public SimplePlayerSprite(Game game, PlayerData data, Texture2D spriteImage,
                            Point startPosition) :base(game)
        {
            pData = data;
            DrawOrder = 1;
            game.Components.Add(this);
            // Take a copy of the texture passed down
            Image = spriteImage;
            // Take a copy of the start position
            previousPosition = Position = startPosition;
            // Calculate the bounding rectangle
            BoundingRect = new Rectangle((int)Position.X, Position.Y, Image.Width, Image.Height);

        }

        public override void Update(GameTime gameTime)
        {
            previousPosition = Position;
            if(InputEngine.IsKeyHeld(Keys.Up))
                Position += new Point(0, -speed);
            if (InputEngine.IsKeyHeld(Keys.Down))
                Position += new Point(0, speed) ;
            if (InputEngine.IsKeyHeld(Keys.Left))
                Position += new Point(-speed,0);
            if (InputEngine.IsKeyHeld(Keys.Right))
                Position += new Point(speed,0) ;

            delay -= gameTime.ElapsedGameTime.Milliseconds;

            // if we have moved pull back the proxy reference and send a message to the hub
            if(Position != previousPosition && delay <= 0)
            {
                delay = 2000;
                pData.playerPosition = new Position { X = Position.X, Y = Position.Y };
                IHubProxy proxy = Game.Services.GetService<IHubProxy>();
                proxy.Invoke("Moved", new Object[] 
                {
                    pData.playerID,
                    pData.playerPosition});
            }

            BoundingRect = new Rectangle(Position.X, Position.Y, Image.Width, Image.Height);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sp = Game.Services.GetService<SpriteBatch>();
            if (sp == null) return;
            if (Image != null && Visible)
            {
                sp.Begin();
                sp.Draw(Image, BoundingRect, tint);
                sp.End();
            }

            base.Draw(gameTime);
        }



        
    }
}
