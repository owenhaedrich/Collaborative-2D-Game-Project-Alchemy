using System;
using System.Linq;
using System.Numerics;
using MohawkGame2D;

namespace Collaborative_2D_Game_Project
{
    public class Material(string name, Material[] madeFrom, int rarity = 0)
    {
        public string name = name;
        public int rarity = rarity; // Rarity of the material, 0 being the most common and 5 being the rarest
        public Texture2D texture = texture;
        public Material[] madeFrom = madeFrom; // The materials required to create this material

        //MATERIAL TEXTURES
        static Texture2D fireTexture = Graphics.LoadTexture("../../../assets/materials/fire.png");
        static Texture2D bloodTexture = Graphics.LoadTexture("../../../assets/materials/blood.png");
        static Texture2D batWingTexture = Graphics.LoadTexture("../../../assets/materials/bat wing.png");
        static Texture2D crystalTexture = Graphics.LoadTexture("../../../assets/materials/crystal.png");
        static Texture2D eyeballTexture = Graphics.LoadTexture("../../../assets/materials/eyeball.png");
        static Texture2D featherTexture = Graphics.LoadTexture("../../../assets/materials/feather.png");
        static Texture2D frogLegTexture = Graphics.LoadTexture("../../../assets/materials/frog leg.png");
        static Texture2D iceTexture = Graphics.LoadTexture("../../../assets/materials/ice.png");
        static Texture2D inkTexture = new Texture2D(); //Graphics.LoadTexture("../../../assets/materials/ink.png"); ---------------------
        static Texture2D ironTexture = Graphics.LoadTexture("../../../assets/materials/iron.png");
        static Texture2D moonlightTexture = Graphics.LoadTexture("../../../assets/materials/moonlight.png");
        static Texture2D mushroomTexture = Graphics.LoadTexture("../../../assets/materials/mushroom.png");
        static Texture2D paperTexture = Graphics.LoadTexture("../../../assets/materials/paper.png");
        static Texture2D rockTexture = Graphics.LoadTexture("../../../assets/materials/rock.png");
        static Texture2D saltTexture = Graphics.LoadTexture("../../../assets/materials/salt.png");
        static Texture2D snowTexture = Graphics.LoadTexture("../../../assets/materials/snow.png");
        static Texture2D sunTexture = Graphics.LoadTexture("../../../assets/materials/sun.png");
        static Texture2D waterTexture = Graphics.LoadTexture("../../../assets/materials/water.png");
        static Texture2D woodTexture = Graphics.LoadTexture("../../../assets/materials/wood.png");

        //Basic Materials
        public static Material Junk = new Material("Junk", []); // Junk is made from unsuccessful combinations
        public static Material Fire = new Material("Fire", []);
        public static Material Water = new Material("Water", []);
        public static Material Earth = new Material("Earth", []);
        public static Material Air = new Material("Air", []);

        //Combined Materials
        public static Material Mud = new Material("Mud", [Water, Earth]);

        public static Material[] materials = { Junk, Fire, Water, Earth, Air, Mud };
        public static Material[] basicMaterials = { Fire, Water, Earth, Air };
        public static Material[] craftableMaterials = { Mud };

        public static Material Combine(Material?[] inputMaterials)
        {
            // Combine the materials to create a new material

            // Sort the input materials alphabetically
            Material?[] orderedInputMaterials = inputMaterials.OrderBy(GetMaterialName).ToArray();

            foreach (Material material in materials)
            {
                // Ensure the made-from materials are sorted alphabetically
                Material[] orderedMadeFrom = material.madeFrom.OrderBy(GetMaterialName).ToArray();
                if (orderedMadeFrom.SequenceEqual(orderedInputMaterials))
                {
                    // The new material is returned
                    return material;
                }
            }

            // Return Junk if there are no valid combinations
            return Junk;
        }

        // Get the name of the material. This is used to sort the materials alphabetically
        static string GetMaterialName(Material? material)
        {
            if (material is null)
                return "";
            else
                return material.name;
        }

        public void Render(Vector2 position)
        {
            Vector2 textureSize = new Vector2(texture.Width, texture.Height);
            Graphics.Draw(texture, position - textureSize/2);
        }
    }


}
