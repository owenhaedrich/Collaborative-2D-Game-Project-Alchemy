

using System.Numerics;
using MohawkGame2D;

namespace Collaborative_2D_Game_Project
{
    class ItemHolder
    {
        public Vector2 position;
        public Texture2D texture;

        public ItemHolder(Vector2 position)
        {
            this.position = position;
        }

        public void Render()
        {
            Graphics.Draw(texture, position);
            Draw.FillColor = Color.Black;
            Draw.Rectangle(position, Vector2.One * 10);
        }
    }
}
