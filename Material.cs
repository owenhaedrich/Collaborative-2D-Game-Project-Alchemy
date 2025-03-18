using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Collaborative_2D_Game_Project
{
    class Material(string name, Material[] madeFrom, int rarity = 0)
    {
        public string name = name;
        public int rarity = rarity; // Rarity of the material, 0 being the most common and 5 being the rarest
        public Material[] madeFrom = madeFrom; // The materials required to create this material

        public static Material Junk = new Material("Junk", []);
        public static Material Fire = new Material("Fire", []);
        public static Material Water = new Material("Water", []);
        public static Material Earth = new Material("Earth", []);
        public static Material Air = new Material("Air", []); 

        public static Material[] materials = { Junk, Fire, Water, Earth, Air };

        public Material Combine(Material[] inputMaterials)
        {
            // Combine the materials to create a new material

            // Sort the input materials alphabetically
            Material[] orderedInputMaterials = inputMaterials.OrderBy(GetMaterialName).ToArray();

            foreach (Material material in materials)
            {
                // Ensure the materials are sorted alphabetically
                Material[] orderedMadeFrom = material.madeFrom.OrderBy(GetMaterialName).ToArray();
                if (material.madeFrom.SequenceEqual(orderedMadeFrom))
                {
                    // The new material is returned
                    return material;
                }
            }

            // Return Junk if there are no valid combinations
            return Junk;
        }

        string GetMaterialName(Material material)
        {
            return material.name;
        }
    }


}
