using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Crosswalk
{
    class Assets
    {
        public static List<Image> Cars;
        public static List<Image> Humans;
        public static List<Image> Blood;
        public static List<Image> Debris;
        public static List<Image> Train;
        public static Image Street;
        public static Image Smoke;
        
        public static void Load()
        {
            Cars = new List<Image>();
            foreach (string file in Directory.GetFiles("Content/Sprites/Car/"))
            {
                Cars.Add(Image.FromFile(file));
            }

            Humans = new List<Image>();
            foreach (string file in Directory.GetFiles("Content/Sprites/Human/"))
            {
                Humans.Add(Image.FromFile(file));
            }

            Street = Image.FromFile("Content/Backgrounds/Street.png");
            Smoke = Image.FromFile("Content/Sprites/Particles/Smoke.png");

            Blood = new List<Image>();
            foreach (string file in Directory.GetFiles("Content/Sprites/Particles/Blood/"))
            {
                Blood.Add(Image.FromFile(file));
            }

            Debris = new List<Image>();
            foreach (string file in Directory.GetFiles("Content/Sprites/Particles/Debris/"))
            {
                Debris.Add(Image.FromFile(file));
            }

            Train = new List<Image>();
            foreach (string file in Directory.GetFiles("Content/Sprites/Train/"))
            {
                Train.Add(Image.FromFile(file));
            }
        }
    }
}
