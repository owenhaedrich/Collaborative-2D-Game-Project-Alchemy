

using System.Numerics;
using MohawkGame2D;

namespace Collaborative_2D_Game_Project
{
    class ItemHolder
    {
        public Vector2 position;
        public Texture2D texture;
        public Interactable? item;

        public ItemHolder(Vector2 position)
        {
            this.position = position;
        }

        public void Render()
        {
            Graphics.Draw(texture, position);
            Draw.FillColor = Color.Black;
            if (item is not null)
            {
                Draw.FillColor = Color.Red;
            }
            Draw.Rectangle(position, Vector2.One * 10);
        }
    }
}
