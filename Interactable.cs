using System;
using System.Linq;
using System.Numerics;
using MohawkGame2D;

namespace Collaborative_2D_Game_Project
{
    public class Interactable
    {
        public Vector2 position;
        public Vector2 homePosition = Vector2.NegativeInfinity;
        public Texture2D texture; // The texture of the interactable
        public Material? material; // The material if the interactable has one
        bool moveable = false; // Whether the interactable can be moved
        public bool moving = false;
        public bool free = false;

        //Bottle Setup
        static Texture2D bottleTexture = Graphics.LoadTexture("../../../assets/graphics/Bottle.png");
        public static Vector2 bottleSize = new Vector2(bottleTexture.Width, bottleTexture.Height);
        public static Interactable EmptyBottle = new Interactable(Vector2.Zero, bottleTexture, null, true);
        
        //Clone Interactable
        public Interactable(Interactable interactable, Vector2 spawnPosition, Material? material = null)
        {
            position = spawnPosition;
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

        //Free Interactable
        public Interactable()
        {
            this.texture = new Texture2D();
            this.free = true;
        }

        //Unique Interactable Generator
        Interactable(Vector2 position, Texture2D texture, Material? material = null, bool moveable = false)
        {
            this.position = position;
            this.texture = texture;
            this.material = material;
            this.moveable = moveable;
        }

        //Player interaction with the interactable
        public void Interact()
        {
            if (moveable)
            {
                moving = true;
            }
        }

        public void Render()
        {
            //Check if the interactable is a potion or a generic material
            if (material is not null)
            {
                if (Material.potions.Contains(material))
                {
                    material.Render(position + new Vector2(texture.Width, texture.Height) / 2 - new Vector2(material.texture.Width, material.texture.Height) / 2);
                }
                else
                {
                    Graphics.Draw(texture, position);
                    material.Render(position + new Vector2(texture.Width, texture.Height) / 2 - new Vector2(material.texture.Width, material.texture.Height) / 2 + Vector2.UnitY * 20);
                }
            }
            else
            {
                Graphics.Draw(texture, position);
            }
        }

        // Free the interactable from the game
        public void Free()
        {
            texture = new Texture2D();
            moveable = false;  
            free = true;
        }
    }
}
