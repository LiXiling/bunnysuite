using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace App
{
    public class Bunny
    {
        private const int numberOfTextures = 3;
        public float X;
        public float Y;
        public float SpeedX;
        public float SpeedY;
        public Texture2D texture;
        private int i = 0;

        public Bunny(ContentManager content)
        {
            loadTexture(content);
        }
        private void loadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"wabbit_alpha"+i);

        }

        public void changeTexture(int j, ContentManager content) {
            i = j % numberOfTextures;
            loadTexture(content);
        }
    }
}
