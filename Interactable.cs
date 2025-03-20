using System.Numerics;
using MohawkGame2D;

namespace Collaborative_2D_Game_Project
{
    class Interactable
    {
        public Vector2 position;
        public Vector2 homePosition = Vector2.NegativeInfinity;
        public Texture2D texture; // The texture of the interactable
        public Material? material; // The material if the interactable has one
        bool moveable = false; // Whether the interactable can be moved
        public bool moving = false;

        public static Interactable EmptyBottle = new Interactable(Vector2.Zero, Graphics.LoadTexture("../../../assets/graphics/Bottle.png"), null, true);

        public Interactable(Interactable interactable, Material? material = null)
        {
            position = interactable.position;
            texture = interactable.texture;
            moveable = interactable.moveable;
            if (material is not null)
            {
                this.material = material;
            }
            else
            {
                this.material = interactable.material;
            }
        }

        Interactable(Vector2 position, Texture2D texture, Material? material = null, bool moveable = false)
        {
            this.position = position;
            this.texture = texture;
            this.material = material;
            this.moveable = moveable;
        }

        public void Interact()
        {
            if (moveable)
            {
                moving = true;
            }
        }

        public void Render()
        {
            Graphics.Draw(texture, position);
            if (material is not null)
            {
                Graphics.Draw(material.texture, position);
            }
        }
    }
}
