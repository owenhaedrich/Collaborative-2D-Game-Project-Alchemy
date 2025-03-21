using System;
using System.Diagnostics;
using System.Linq;

namespace Collaborative_2D_Game_Project
{
    class Material(string name, Material[] madeFrom, int rarity = 0)
    {
        public string name = name;
        public int rarity = rarity; // Rarity of the material, 0 being the most common and 5 being the rarest
        public Material[] madeFrom = madeFrom; // The materials required to create this material

        //Basic Materials
        public static Material Junk = new Material("Junk", []); // Junk is made from unsuccessful combinations
        public static Material Fire = new Material("Fire", []);
        public static Material Water = new Material("Water", []);
        public static Material Earth = new Material("Earth", []);
        public static Material Air = new Material("Air", []);
        public static Material Sun = new Material("Sun", []);
        public static Material Ice = new Material("Ice", []);


        //Combined Materials
        public static Material Mud = new Material("Mud", [Water, Earth]);
        public static Material Sand = new Material("Sand", [Earth, Air]);
        public static Material Soil = new Material("Soil", [Mud, Sand, Water, Sun]);
        public static Material Salt = new Material("Salt", [Water, Fire, Earth]);
        public static Material Metal = new Material("Metal",[Earth, Fire]);
        public static Material Plant = new Material("Plant", [Soil, Sun]);
        public static Material Glass = new Material("Glass",[Sand, Fire]);
        public static Material Lava = new Material("Lava",[Earth, Fire]);
        public static Material Obsidian = new Material("Obsidian", [Water,Lava]);







        public static Material[] materials = { Junk, Fire, Water, Earth, Air, Mud, Sun, Ice };

        public static Material Combine(Material[] inputMaterials)
        {
            // Combine the materials to create a new material

            // Sort the input materials alphabetically
            Material[] orderedInputMaterials = inputMaterials.OrderBy(GetMaterialName).ToArray();

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
        static string GetMaterialName(Material material)
        {
            return material.name;
        }
    }


}
