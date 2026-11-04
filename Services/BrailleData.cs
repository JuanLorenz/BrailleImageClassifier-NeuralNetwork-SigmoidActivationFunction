using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Backprop
{
    public class BrailleImage
    {
        public double[] Pixels;   // 784 values from 0 to 1
        public int Letter;        // 0 = a ... 25 = z
        public string Type;       // dim, rot or whs (kind of augmentation)
    }

    public static class BrailleData
    {
        static Random rand = new Random();

        // makes the image 28x28 gray and scales every pixel to 0..1
        public static double[] ReadPixels(Bitmap bmp)
        {
            Bitmap small = new Bitmap(bmp, 28, 28);
            double[] pixels = new double[28 * 28];
            double min = 1.0;
            double max = 0.0;
            for (int y = 0; y < 28; y++)
            {
                for (int x = 0; x < 28; x++)
                {
                    Color c = small.GetPixel(x, y);
                    double gray = ((c.R + c.G + c.B) / 3.0) / 255.0;
                    pixels[y * 28 + x] = gray;
                    if (gray < min) min = gray;
                    if (gray > max) max = gray;
                }
            }
            small.Dispose();

            // stretch each image to the full 0..1 range so dim images look like bright ones
            if (max > min)
            {
                for (int i = 0; i < pixels.Length; i++) pixels[i] = (pixels[i] - min) / (max - min);
            }
            return pixels;
        }

        // reads every image in the folder (and its subfolders)
        public static List<BrailleImage> LoadFolder(string folder)
        {
            List<BrailleImage> list = new List<BrailleImage>();
            foreach (string file in Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories))
            {
                string name = Path.GetFileName(file).ToLower();
                if (!(name.EndsWith(".jpg") || name.EndsWith(".jpeg") || name.EndsWith(".png"))) continue;

                // the letter is the first character of the file name,
                // or the folder name if the images are inside folders named a to z
                char ch = name[0];
                string folderName = new DirectoryInfo(Path.GetDirectoryName(file)).Name.ToLower();
                if (folderName.Length == 1) ch = folderName[0];

                int letter = ch - 'a';
                if (letter < 0 || letter > 25) continue;

                string type = "other";
                if (name.Contains("dim")) type = "dim";
                else if (name.Contains("rot")) type = "rot";
                else if (name.Contains("whs")) type = "whs";

                using (Bitmap bmp = new Bitmap(file))
                {
                    BrailleImage img = new BrailleImage();
                    img.Pixels = ReadPixels(bmp);
                    img.Letter = letter;
                    img.Type = type;
                    list.Add(img);
                }
            }
            return list;
        }

        // one line per image: letter,type,784 pixel values
        public static void SaveCsv(List<BrailleImage> images, string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (BrailleImage img in images)
            {
                sb.Append(img.Letter + "," + img.Type);
                foreach (double p in img.Pixels) sb.Append("," + p.ToString("F4", CultureInfo.InvariantCulture));
                sb.AppendLine();
            }
            File.WriteAllText(path, sb.ToString());
        }

        // reads the csv back, lines that look wrong are skipped
        public static List<BrailleImage> LoadCsv(string path, out int skipped)
        {
            List<BrailleImage> list = new List<BrailleImage>();
            skipped = 0;
            foreach (string line in File.ReadAllLines(path))
            {
                string[] parts = line.Split(',');
                int letter;
                if (parts.Length != 786 || !int.TryParse(parts[0], out letter) || letter < 0 || letter > 25)
                {
                    skipped++;
                    continue;
                }

                double[] pixels = new double[784];
                bool ok = true;
                for (int i = 0; i < 784; i++)
                {
                    if (!double.TryParse(parts[i + 2], NumberStyles.Float, CultureInfo.InvariantCulture, out pixels[i]))
                    {
                        ok = false;
                        break;
                    }
                }
                if (!ok)
                {
                    skipped++;
                    continue;
                }

                BrailleImage img = new BrailleImage();
                img.Pixels = pixels;
                img.Letter = letter;
                img.Type = parts[1];
                list.Add(img);
            }
            return list;
        }

        // 80% train and 20% test, done separately for every letter
        public static void RandomSplit(List<BrailleImage> all, List<BrailleImage> train, List<BrailleImage> test)
        {
            for (int letter = 0; letter < 26; letter++)
            {
                List<BrailleImage> group = all.Where(i => i.Letter == letter).OrderBy(i => rand.Next()).ToList();
                int trainCount = (int)(group.Count * 0.8);
                for (int k = 0; k < group.Count; k++)
                {
                    if (k < trainCount) train.Add(group[k]);
                    else test.Add(group[k]);
                }
            }
        }

        // test on one type of augmentation, train on the others
        public static void HoldOutSplit(List<BrailleImage> all, string type, List<BrailleImage> train, List<BrailleImage> test)
        {
            foreach (BrailleImage img in all)
            {
                if (img.Type == type) test.Add(img);
                else train.Add(img);
            }
        }

        // mixes the order of the list (used before every epoch)
        public static void Shuffle(List<BrailleImage> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                BrailleImage temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}