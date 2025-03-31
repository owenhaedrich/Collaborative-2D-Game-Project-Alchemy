using System;
using System.Linq;
using System.Numerics;
using MohawkGame2D;

namespace Collaborative_2D_Game_Project
{
    public class Material(string name, Material[] madeFrom, Texture2D texture = new Texture2D(), int rarity = 0)
    {
        public string name = name;
        public int rarity = rarity; // Rarity of the material, 0 being the most common and 5 being the rarest
        public Texture2D texture = texture;
        public Material[] madeFrom = madeFrom; // The materials required to create this material

        // Ingredient Textures
        static Texture2D fireTexture = Graphics.LoadTexture("../../../assets/materials/fire.png");
        static Texture2D bloodTexture = Graphics.LoadTexture("../../../assets/materials/blood.png");
        static Texture2D batWingTexture = Graphics.LoadTexture("../../../assets/materials/bat wing.png");
        static Texture2D crystalTexture = Graphics.LoadTexture("../../../assets/materials/crystal.png");
        static Texture2D eyeballTexture = Graphics.LoadTexture("../../../assets/materials/eyeball.png");
        static Texture2D featherTexture = Graphics.LoadTexture("../../../assets/materials/feather.png");
        static Texture2D frogLegTexture = Graphics.LoadTexture("../../../assets/materials/frog leg.png");
        static Texture2D ironTexture = Graphics.LoadTexture("../../../assets/materials/iron.png");
        static Texture2D moonlightTexture = Graphics.LoadTexture("../../../assets/materials/moonlight.png");
        static Texture2D mushroomTexture = Graphics.LoadTexture("../../../assets/materials/mushroom.png");
        static Texture2D snowTexture = Graphics.LoadTexture("../../../assets/materials/snow.png");
        static Texture2D sunTexture = Graphics.LoadTexture("../../../assets/materials/sun.png");
        static Texture2D waterTexture = Graphics.LoadTexture("../../../assets/materials/water.png");
        static Texture2D roseQuartzTexture = new Texture2D(); //Graphics.LoadTexture("../../../assets/materials/rose quartz.png"); ---------------------
        static Texture2D animalFurTexture = new Texture2D(); //Graphics.LoadTexture("../../../assets/materials/animal fur.png"); ---------------------
        static Texture2D clayTexture = new Texture2D(); //Graphics.LoadTexture("../../../assets/materials/clay.png"); ---------------------
        static Texture2D spiderSilkTexture = new Texture2D(); //Graphics.LoadTexture("../../../assets/materials/spider silk.png"); ---------------------
        static Texture2D tongueTexture = new Texture2D(); //Graphics.LoadTexture("../../../assets/materials/tongue.png"); ---------------------
        static Texture2D dolphinFinTexture = new Texture2D(); //Graphics.LoadTexture("../../../assets/materials/dolphin fin.png"); ---------------------
        static Texture2D inkTexture = new Texture2D(); //Graphics.LoadTexture("../../../assets/materials/ink.png"); ---------------------

        // Ingredients
        public static Material Junk = new Material("Junk", []); // Junk is made from unsuccessful combinations
        public static Material Fire = new Material("Fire", [], fireTexture);
        public static Material Blood = new Material("Earth", [], bloodTexture);
        public static Material BatWing = new Material ("Bat Wing", [], batWingTexture);
        public static Material Crystal = new Material("Crystal", [], crystalTexture);
        public static Material Eyeball = new Material("Eyeball", [], eyeballTexture);
        public static Material Feather = new Material("Feather", [], featherTexture);
        public static Material FrogLeg = new Material("Frog Leg", [], frogLegTexture);
        public static Material Iron = new Material("Iron", [], ironTexture);
        public static Material Moonlight = new Material("Moonlight", [], moonlightTexture);
        public static Material Mushroom = new Material("Mushroom", [], mushroomTexture);
        public static Material Snow = new Material("Snow", [], snowTexture);
        public static Material Sun = new Material("Sun", [], sunTexture);
        public static Material Water = new Material("Water", [], waterTexture);
        public static Material RoseQuartz = new Material("Rose Quartz", [], roseQuartzTexture);
        public static Material AnimalFur = new Material("Animal Fur", [], animalFurTexture);
        public static Material Clay = new Material("Clay", [], clayTexture);
        public static Material SpiderSilk = new Material("Spider Silk", [], spiderSilkTexture);
        public static Material Tongue = new Material("Tongue", [], tongueTexture);
        public static Material DolphinFin = new Material("Dolphin Fin", [], dolphinFinTexture);
        public static Material Ink = new Material("Ink", [], inkTexture);

        // Potion Recipes
        public static Material healingPotion = new Material("Healing Potion", [Sun, Water, RoseQuartz]);
        public static Material clairvoyancePotion = new Material("Clairvoyance Potion", [Eyeball, Moonlight, Crystal]);
        public static Material invisibilityPotion = new Material("Invisibility Potion", [Eyeball, Moonlight, Snow]);
        public static Material seeInvisibilityPotion = new Material("See Invisibility Potion", [Eyeball, Sun, Snow]);
        public static Material glowingPotion = new Material("Glowing Potion", [Eyeball, Fire, Sun]);
        public static Material shieldPotion = new Material("Shield Potion", [Iron, Crystal]);
        public static Material strengthPotion = new Material("Strength Potion", [Fire, Blood, Iron]);
        public static Material compLanguagePotion = new Material("Comprehend Language Potion", [Tongue, Ink]);
        public static Material lovePotion = new Material("Love Potion", [RoseQuartz, Blood]);
        public static Material poisonPotion = new Material("Poison Potion", [Mushroom, Blood]);
        public static Material enlargingPotion = new Material("Enlarging Potion", [Mushroom, Sun, Clay]);
        public static Material shrinkingPotion = new Material("Shrinking Potion", [Mushroom, Moonlight, Clay]);
        public static Material mindReadingPotion = new Material("Mind Reading Potion", [Blood, Eyeball, Ink]);
        public static Material swimmingPotion = new Material("Swimming Potion", [DolphinFin, FrogLeg, Blood]);
        public static Material talkWithAnimalsPotion = new Material("Talk With Animals Potion", [AnimalFur, Tongue]);
        public static Material animalFriendshipPotion = new Material("Animal Friendship Potion", [AnimalFur, RoseQuartz]);
        public static Material polymorphPotion = new Material("Polymorph Potion", [Blood, AnimalFur, Clay]);
        public static Material wallClimbingPotion = new Material("Wall Climbing Potion", [SpiderSilk, Blood]);
        public static Material flyingPotion = new Material("Flying Potion", [BatWing, Feather, Blood]);
        public static Material waterBreathingPotion = new Material("Water Breathing Potion", [Water, DolphinFin, Blood]);
        public static Material speedPotion = new Material("Speed Potion", [FrogLeg, Feather, Blood]);
        public static Material jumpingPotion = new Material("Jumping Potion", [FrogLeg, Blood]);


        public static Material[] materials = [ Junk, Fire, Blood, BatWing, Crystal, Eyeball, Feather, FrogLeg, Iron, Moonlight, Mushroom, Snow, Sun, Water, RoseQuartz, AnimalFur, Clay, SpiderSilk, Tongue, DolphinFin, Ink ];
        public static Material[] potions = [ healingPotion, clairvoyancePotion, invisibilityPotion, seeInvisibilityPotion, glowingPotion, shieldPotion, strengthPotion, compLanguagePotion, lovePotion, poisonPotion, enlargingPotion, shrinkingPotion, mindReadingPotion, swimmingPotion, talkWithAnimalsPotion, animalFriendshipPotion, polymorphPotion, wallClimbingPotion, flyingPotion, waterBreathingPotion, speedPotion, jumpingPotion ];

        public static Material Combine(Material?[] inputMaterials)
        {
            // Combine the materials to create a new material

            // Sort the input materials alphabetically
            Material?[] orderedInputMaterials = inputMaterials.Where(MaterialIsNotNull).OrderBy(GetMaterialName).ToArray();

            foreach (Material potion in potions)
            {
                // Ensure the made-from materials are sorted alphabetically
                Material[] orderedMadeFrom = potion.madeFrom.OrderBy(GetMaterialName).ToArray();
                if (orderedMadeFrom.SequenceEqual(orderedInputMaterials))
                {
                    // The new material is returned
                    return potion;
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

        static bool MaterialIsNotNull(Material? material)
        { 
                return material is not null;
        }

        public void Render(Vector2 position)
        {
            Graphics.Draw(texture, position);
        }
    }


}
