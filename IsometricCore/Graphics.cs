using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric.Core
{
    public class Graphics
    {
        public SpriteBatch SpriteBatch { get; set; }
        public List<KeyValuePair<int, Texture2D>> Textures { get; set; }
        public List<KeyValuePair<Item, Texture2D>> Items { get; set; }
        public SpriteFont Font { get; set; }
        public Field Field { get; set; }
        public int XCam { get; set; }
        public int YCam { get; set; }
        public int ZCam { get; set; }
        public double Zoom { get; set; }

        public Graphics(GraphicsDevice graphicsDevice)
        {
            SpriteBatch = new SpriteBatch(graphicsDevice);
            Textures = new List<KeyValuePair<int, Texture2D>>();
            Textures.Add(new KeyValuePair<int, Texture2D>(Field.EMPTY_CUBE, null));
            Items = new List<KeyValuePair<Item, Texture2D>>();

            XCam = 0;
            YCam = 0;
            ZCam = 0;
            Zoom = 1.0;
        }

        public void RegisterTexture(int textureIndex, Texture2D texture2d)
        {
            foreach (var texture in Textures)
            {
                if (texture.Key == textureIndex)
                {
                    throw new ArgumentException(string.Format("Texture index {0} already added.", textureIndex));
                }
            }

            Textures.Add(new KeyValuePair<int, Texture2D>(textureIndex, texture2d));
        }

        public void RegisterItem(Item item, Texture2D texture2d)
        {
            foreach (var kvpItem in Items)
            {
                if (kvpItem.Key == item)
                {
                    throw new ArgumentException(string.Format("Texture item {0} already added.", item));
                }
            }

            Items.Add(new KeyValuePair<Item, Texture2D>(item, texture2d));
        }

        public void RegisterFont(SpriteFont font)
        {
            Font = font;
        }

        public void RegisterField(Field field)
        {
            Field = field;
        }

        public void Draw(bool debugStrings)
        {
            SpriteBatch.Begin();

            for (int z = 0; z < Field.FieldArray.GetLength(2); z++)
            {
                for (int y = 0; y < Field.FieldArray.GetLength(1); y++)
                {
                    for (int x = 0; x < Field.FieldArray.GetLength(0); x++)
                    {
                        // Draw field
                        SetTile(new Point(x, y, z), Field.FieldArray[x, y, z]);

                        // Draw item
                        foreach (var kvpItem in Items)
                        {
                            if (kvpItem.Key.X == x && kvpItem.Key.Y == y && kvpItem.Key.Z == z)
                            {
                                SetTile(new Point(x, y, z), kvpItem.Value);
                            }
                        }
                    }
                }
            }

            if (debugStrings)
            {
                // Debug strings
                SpriteBatch.DrawString(Font, string.Format("XCam: {0}", XCam), new Vector2(10, 10), Color.White);
                SpriteBatch.DrawString(Font, string.Format("YCam: {0}", YCam), new Vector2(10, 40), Color.White);
            }
            
            SpriteBatch.End();
        }

        public void SetTile(Point point, int indexTexture)
        {
            // Get texture from index
            Texture2D texture = null;
            foreach (var kvpTexture in Textures)
            {
                if (kvpTexture.Key == indexTexture)
                {
                    texture = kvpTexture.Value;
                }
            }

            if (indexTexture != Field.EMPTY_CUBE)
            {
                if (texture == null)
                {
                    throw new ArgumentException(string.Format("Texture with index {0} is not registered.", indexTexture));
                }

                SetTile(point, texture);
            }
        }

        public void SetTile(Point point, Texture2D texture2d)
        {
            int tileSize = (int)(texture2d.Width * Zoom);
            int tileHeight = (int)(texture2d.Height * Zoom);

            Point normalPoint = new Point(point.X * tileSize / 2 + XCam, point.Y * tileSize / 2 + YCam);
            Point isoPoint = NormalToIso(normalPoint);

            SpriteBatch.Draw(texture2d, new Rectangle(isoPoint.X, isoPoint.Y - (tileSize / 2 * point.Z) + ZCam, tileSize, tileHeight), Color.White);
        }

        private Point IsoToNormal(Point isoPoint)
        {
            Point normalPoint = new Point(0, 0);
            normalPoint.X = (2 * isoPoint.Y + isoPoint.X) / 2;
            normalPoint.Y = (2 * isoPoint.Y - isoPoint.X) / 2;

            return normalPoint;
        }

        private Point NormalToIso(Point normalPoint)
        {
            Point isoPoint = new Point(0, 0);
            isoPoint.X = normalPoint.X - normalPoint.Y;
            isoPoint.Y = (normalPoint.X + normalPoint.Y) / 2;

            return isoPoint;
        }
    }
}
