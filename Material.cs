using System;
using System.Linq;
using MohawkGame2D;

namespace Collaborative_2D_Game_Project
{
    public class Material(string name, Material[] madeFrom, int rarity = 0)
    {
        public string name = name;
        public int rarity = rarity; // Rarity of the material, 0 being the most common and 5 being the rarest
        public Texture2D texture;
        public Material[] madeFrom = madeFrom; // The materials required to create this material

        //Basic Materials
        public static Material Junk = new Material("Junk", []); // Junk is made from unsuccessful combinations
        public static Material Fire = new Material("Fire", []);
        public static Material Water = new Material("Water", []);
        public static Material Earth = new Material("Earth", []);
        public static Material Air = new Material("Air", []);

        //Combined Materials
        public static Material Mud = new Material("Mud", [Water, Earth]);

        public static Material[] materials = { Junk, Fire, Water, Earth, Air, Mud };
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
    }


}
