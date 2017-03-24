using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Sprite
    {
        private Texture2D spriteTexture;
        private Vector2 position;
        private Rectangle borderRect;
        private float scale;
        public BoundingBox BoundBox { get; set; }
        public BoundingSphere BoundSphere { get; set; }


        public Sprite(Texture2D spriteSheet, Vector2 position, Rectangle borderRect, float scale)
        {
            spriteTexture = spriteSheet;
            this.position = position;
            this.borderRect = borderRect;
            BoundBox = new BoundingBox(new Vector3(position, 0), new Vector3(position.X + borderRect.Width, position.Y + borderRect.Height, 0));
            BoundSphere = new BoundingSphere(new Vector3(position.X + borderRect.Width / 2, position.Y + borderRect.Height / 2, 0), (borderRect.Height + borderRect.Width) / 4);
            this.scale = scale;
        }

        public Sprite(Texture2D spriteSheet, Vector2 position, Rectangle borderRect, Vector2 minBoundingBox, Vector2 maxBoundingBox, float scale)
        {
            spriteTexture = spriteSheet;
            this.position = position;
            this.borderRect = borderRect;
            BoundBox = new BoundingBox(new Vector3(position + minBoundingBox, 0), new Vector3(position.X + maxBoundingBox.X, position.Y + maxBoundingBox.Y, 0));
            BoundSphere = new BoundingSphere(new Vector3(position.X + borderRect.Width / 2, position.Y + borderRect.Height / 2, 0), (borderRect.Height + borderRect.Width) / 4);
            this.scale = scale;
        }

        public Sprite(Texture2D spriteSheet, Vector2 position, Rectangle borderRect, Vector2 center, float radious, float scale)
        {
            spriteTexture = spriteSheet;
            this.position = position;
            this.borderRect = borderRect;
            BoundBox = new BoundingBox(new Vector3(position, 0), new Vector3(position.X + borderRect.Width, position.Y + borderRect.Height, 0));
            BoundSphere = new BoundingSphere(new Vector3(position + center, 0), radious);
            this.scale = scale;
        }

        public Sprite(Texture2D spriteSheet, Vector2 position, Rectangle borderRect, Vector2 minBoundingBox, Vector2 maxBoundingBox, Vector2 center, float radious, float scale)
        {
            spriteTexture = spriteSheet;
            this.position = position;
            this.borderRect = borderRect;
            BoundBox = new BoundingBox(new Vector3(position + minBoundingBox, 0), new Vector3(position.X + maxBoundingBox.X, position.Y + maxBoundingBox.Y, 0));
            BoundSphere = new BoundingSphere(new Vector3(position + center, 0), radious);
            this.scale = scale;
        }

        public void Update()
        {

            /*if (borderRect.X < spriteTexture.Width - borderRect.X)
            {
                borderRect.X += 50;
            }
            else
            {
                borderRect.X = 0;
            }*/

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(spriteTexture, position, borderRect, Color.White);
            spriteBatch.Draw(spriteTexture, position, null, null, null, 0, new Vector2(scale, scale), Color.White, SpriteEffects.None, 0);
        }



    }
}
