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
        static Texture2D roseQuartzTexture = Graphics.LoadTexture("../../../assets/materials/rose quartz.png"); 
        static Texture2D animalFurTexture = Graphics.LoadTexture("../../../assets/materials/animal fur.png");
        static Texture2D clayTexture = Graphics.LoadTexture("../../../assets/materials/clay.png"); 
        static Texture2D spiderSilkTexture = Graphics.LoadTexture("../../../assets/materials/spider silk.png");
        static Texture2D tongueTexture = Graphics.LoadTexture("../../../assets/materials/tongue.png"); 
        static Texture2D dolphinFinTexture = Graphics.LoadTexture("../../../assets/materials/dolphin fin.png"); 
        static Texture2D inkTexture = Graphics.LoadTexture("../../../assets/materials/ink.png");


        static Texture2D Healing = Graphics.LoadTexture("../../../assets/materials/HealingPotion.png");
        static Texture2D Clairvoyance = Graphics.LoadTexture("../../../assets/materials/Clairvoyance.png");
        static Texture2D Invisibility = Graphics.LoadTexture("../../../assets/materials/Invisibility.png");
        static Texture2D SeeInvis = Graphics.LoadTexture("../../../assets/materials/SeeInvis.png");
        static Texture2D Glowing = Graphics.LoadTexture("../../../assets/materials/Glowing.png");
        static Texture2D Shield = Graphics.LoadTexture("../../../assets/materials/Shield.png");
        static Texture2D Strength = Graphics.LoadTexture("../../../assets/materials/Strength.png");
        static Texture2D CompLang = Graphics.LoadTexture("../../../assets/materials/CompLang.png");
        static Texture2D Love = Graphics.LoadTexture("../../../assets/materials/Love.png");
        static Texture2D Poison = Graphics.LoadTexture("../../../assets/materials/Poison.png");
        static Texture2D Enlarging = Graphics.LoadTexture("../../../assets/materials/Enlarging.png");
        static Texture2D Shrinking = Graphics.LoadTexture("../../../assets/materials/Shrinking.png");
        static Texture2D MindRead = Graphics.LoadTexture("../../../assets/materials/MindRead.png");
        static Texture2D Swimming = Graphics.LoadTexture("../../../assets/materials/Swimming.png");
        static Texture2D AnimalTalk = Graphics.LoadTexture("../../../assets/materials/SpeakAnimal.png");
        static Texture2D AnimalFriend = Graphics.LoadTexture("../../../assets/materials/AnimalFriend.png");
        static Texture2D Polymorph = Graphics.LoadTexture("../../../assets/materials/Polymorph.png");
        static Texture2D WallClimb = Graphics.LoadTexture("../../../assets/materials/WallClimb.png");
        static Texture2D Flying = Graphics.LoadTexture("../../../assets/materials/Flying.png");
        static Texture2D WaterBreath = Graphics.LoadTexture("../../../assets/materials/WaterBreath.png");
        static Texture2D Speed = Graphics.LoadTexture("../../../assets/materials/Speed.png");
        static Texture2D Jumping = Graphics.LoadTexture("../../../assets/materials/Jumping.png");
        //static Texture2D InvisInk = Graphics.LoadTexture("../../../assets/materials/InvisibleInk.png");

        // Ingredients
        public static Material Junk = new Material("Junk", []); // Junk is made from unsuccessful combinations
        public static Material Fire = new Material("Fire", [], fireTexture);
        public static Material Blood = new Material("Earth", [], bloodTexture);
        public static Material BatWing = new Material("Bat Wing", [], batWingTexture);
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
        public static Material healingPotion = new Material("Healing Potion", [Sun, Water, RoseQuartz], Healing);
        public static Material clairvoyancePotion = new Material("Clairvoyance Potion", [Eyeball, Moonlight, Crystal], Clairvoyance);
        public static Material invisibilityPotion = new Material("Invisibility Potion", [Eyeball, Moonlight, Snow], Invisibility);
        public static Material seeInvisibilityPotion = new Material("See Invisibility Potion", [Eyeball, Sun, Snow], SeeInvis);
        public static Material glowingPotion = new Material("Glowing Potion", [Eyeball, Fire, Sun], Glowing);
        public static Material shieldPotion = new Material("Shield Potion", [Iron, Crystal], Shield);
        public static Material strengthPotion = new Material("Strength Potion", [Fire, Blood, Iron], Strength);
        public static Material compLanguagePotion = new Material("Comprehend Language Potion", [Tongue, Ink], CompLang);
        public static Material lovePotion = new Material("Love Potion", [RoseQuartz, Blood], Love);
        public static Material poisonPotion = new Material("Poison Potion", [Mushroom, Blood], Poison);
        public static Material enlargingPotion = new Material("Enlarging Potion", [Mushroom, Sun, Clay], Enlarging);
        public static Material shrinkingPotion = new Material("Shrinking Potion", [Mushroom, Moonlight, Clay], Shrinking);
        public static Material mindReadingPotion = new Material("Mind Reading Potion", [Blood, Eyeball, Ink], MindRead);
        public static Material swimmingPotion = new Material("Swimming Potion", [DolphinFin, FrogLeg, Blood], Swimming);
        public static Material talkWithAnimalsPotion = new Material("Talk With Animals Potion", [AnimalFur, Tongue], AnimalTalk);
        public static Material animalFriendshipPotion = new Material("Animal Friendship Potion", [AnimalFur, RoseQuartz], AnimalFriend);
        public static Material polymorphPotion = new Material("Polymorph Potion", [Blood, AnimalFur, Clay], Polymorph);
        public static Material wallClimbingPotion = new Material("Wall Climbing Potion", [SpiderSilk, Blood], WallClimb);
        public static Material flyingPotion = new Material("Flying Potion", [BatWing, Feather, Blood], Flying);
        public static Material waterBreathingPotion = new Material("Water Breathing Potion", [Water, DolphinFin, Blood], WaterBreath);
        public static Material speedPotion = new Material("Speed Potion", [FrogLeg, BatWing, Blood], Speed);
        public static Material jumpingPotion = new Material("Jumping Potion", [FrogLeg, Blood], Jumping);
        //public static Material invisibleink = new Material("Invisible Ink", [Eyeball, Moonlight, Ink, Crystal], InvisInk);


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
