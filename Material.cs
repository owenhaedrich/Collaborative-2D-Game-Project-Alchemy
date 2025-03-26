using System;
using System.Linq;
using MohawkGame2D;

namespace Collaborative_2D_Game_Project
{
    public class Material(string name, Material[] madeFrom, Texture2D texture = new Texture2D(), int rarity = 0)
    {
        public string name = name;
        public int rarity = rarity; // Rarity of the material, 0 being the most common and 5 being the rarest
        public Texture2D texture;
        public Material[] madeFrom = madeFrom; // The materials required to create this material

        // Game Screens
        public Texture2D gameBackground = Graphics.LoadTexture("../../../assets/Screens/Game_Background");

        // Cauldron Textures
        public Texture2D cauldronAsset = Graphics.LoadTexture("../../../assets/graphics/Cauldron");
        public Texture2D litCauldronAsset = Graphics.LoadTexture("../../../assets/graphics/CauldronLit");

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
        public static Material Fire = new Material("Fire", [], fireTexture);
        public static Material Blood = new Material("Earth", [], bloodTexture);
        public static Material BatWing = new Material ("Bat Wing", [], batWingTexture);
        public static Material Crystal = new Material("Crystal", [], crystalTexture);
        public static Material Eyeball = new Material("Eyeball", [], eyeballTexture);
        public static Material Feather = new Material("Feather", [], featherTexture);
        public static Material FrogLeg = new Material("Frog Leg", [], frogLegTexture);
        public static Material Ice = new Material("Ice", [], iceTexture);
        public static Material Ink = new Material("Ink", [], inkTexture);
        public static Material Iron = new Material("Iron", [], ironTexture);
        public static Material Moonlight = new Material("Moonlight", [], moonlightTexture);
        public static Material Mushroom = new Material("Mushroom", [], mushroomTexture);
        public static Material Paper = new Material("Paper", [], paperTexture);
        public static Material Rock = new Material("Rock", [], rockTexture);
        public static Material Salt = new Material("Salt", [], saltTexture);
        public static Material Snow = new Material("Snow", [], snowTexture);
        public static Material Sun = new Material("Sun", [], sunTexture);
        public static Material Water = new Material("Water", [], waterTexture);
        public static Material Wood = new Material("Wood", [], woodTexture);


        //Combined Materials
        public static Material frogPotion = new Material("Frog Transmorphicator", [FrogLeg, Water]);
        public static Material healingPotion = new Material("Health Bomb", [Sun, Water]);
        public static Material spiritWorldPotion = new Material("Enter the Spirit World ahh potion", [Eyeball, Moonlight, Ink, Crystal]);
        public static Material bloodMoonPotion = new Material("Blood Moon Appearus", [Sun, Water, Moonlight]);
        public static Material invisPotion = new Material("Temp Invis in the Dark ahh potion", [BatWing, Water, Rock]);
        public static Material floatyLightPotion = new Material("Floaty Lighty", [Sun, Fire, Paper]);
        public static Material sightPotion = new Material("Potion of Seeing", [Eyeball, Moonlight, Water]);
        public static Material barrierPotion = new Material("Magicus Barrius", [Iron, Salt, Rock]);
        public static Material randomAnimalPotion = new Material("Turns into a Random Animall ahh potion", [Blood, Mushroom, Feather]);
        public static Material pastPotion = new Material("User replay sounds or conversations from the past ahh potion", [Ink, Paper, Eyeball, Water]);


        public static Material[] materials = { Junk, Fire, Blood, BatWing, Crystal, Eyeball, Feather, FrogLeg, Ice, Ink, Iron, Moonlight, Mushroom, Paper, Rock, Salt, Snow, Sun, Water, Wood };
        public static Material[] craftableMaterials = { frogPotion, healingPotion, spiritWorldPotion, bloodMoonPotion, invisPotion, floatyLightPotion, sightPotion, barrierPotion, randomAnimalPotion, pastPotion };

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
