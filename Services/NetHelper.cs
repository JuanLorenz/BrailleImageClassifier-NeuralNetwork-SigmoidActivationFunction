using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Backprop
{
    public static class NetHelper
    {
        // the trainer saves these two files and Form1 loads them
        public static string WeightsFile = Path.Combine(Application.StartupPath, "Models", "braille_weights.txt");
        public static string HiddenFile = Path.Combine(Application.StartupPath, "Models", "braille_hidden.txt");

        // puts one image on the input neurons and its letter on the desired outputs (1 for the letter, 0 for the rest)
        public static void SetSample(NeuralNet net, BrailleImage img)
        {
            for (int i = 0; i < 784; i++) net.setInputs(i, img.Pixels[i]);
            for (int k = 0; k < 26; k++)
            {
                if (k == img.Letter) net.setDesiredOutput(k, 1.0);
                else net.setDesiredOutput(k, 0.0);
            }
        }

        // runs the network on the pixels and returns the 26 output values
        public static double[] GetOutputs(NeuralNet net, double[] pixels)
        {
            for (int i = 0; i < 784; i++) net.setInputs(i, pixels[i]);
            net.run();
            double[] outputs = new double[26];
            for (int k = 0; k < 26; k++) outputs[k] = net.getOuputData(k);
            return outputs;
        }

        // index of the biggest value
        public static int BiggestIndex(double[] values)
        {
            int best = 0;
            for (int k = 1; k < values.Length; k++)
            {
                if (values[k] > values[best]) best = k;
            }
            return best;
        }

        // text like "A 0.93   C 0.12   B 0.05"
        public static string TopThree(double[] outputs)
        {
            string text = "";
            bool[] used = new bool[26];
            for (int rank = 0; rank < 3; rank++)
            {
                int best = -1;
                for (int k = 0; k < 26; k++)
                {
                    if (!used[k] && (best == -1 || outputs[k] > outputs[best])) best = k;
                }
                used[best] = true;
                text += (char)('A' + best) + " " + outputs[best].ToString("F2") + "   ";
            }
            return text;
        }

        // average loss and accuracy over a list of images
        public static void Test(NeuralNet net, List<BrailleImage> images, out double loss, out double accuracy)
        {
            double totalLoss = 0;
            int correct = 0;
            foreach (BrailleImage img in images)
            {
                double[] outputs = GetOutputs(net, img.Pixels);
                for (int k = 0; k < 26; k++)
                {
                    double target = (k == img.Letter) ? 1.0 : 0.0;
                    totalLoss += 0.5 * (target - outputs[k]) * (target - outputs[k]);
                }
                if (BiggestIndex(outputs) == img.Letter) correct++;
            }
            loss = totalLoss / images.Count;
            accuracy = (double)correct / images.Count;
        }

        // 26x26 table: row = real letter, column = what the network said
        public static int[,] ConfusionMatrix(NeuralNet net, List<BrailleImage> images)
        {
            int[,] matrix = new int[26, 26];
            foreach (BrailleImage img in images)
            {
                double[] outputs = GetOutputs(net, img.Pixels);
                matrix[img.Letter, BiggestIndex(outputs)]++;
            }
            return matrix;
        }

        // overall accuracy plus precision and recall for each letter
        public static string Report(int[,] m)
        {
            string text = "Letter  Precision  Recall\r\n";
            int correct = 0;
            int total = 0;
            for (int c = 0; c < 26; c++)
            {
                int truePos = m[c, c];
                int predicted = 0;   // everything the network called this letter
                int actual = 0;      // everything that really is this letter
                for (int k = 0; k < 26; k++)
                {
                    predicted += m[k, c];
                    actual += m[c, k];
                }
                double precision = predicted == 0 ? 0 : (double)truePos / predicted;
                double recall = actual == 0 ? 0 : (double)truePos / actual;
                text += (char)('a' + c) + "       " + precision.ToString("F2") + "       " + recall.ToString("F2") + "\r\n";
                correct += truePos;
                total += actual;
            }
            double accuracy = total == 0 ? 0 : (double)correct / total;
            return "Accuracy: " + accuracy.ToString("P1") + " (" + correct + "/" + total + ")\r\n" + text;
        }

        public static void SaveMatrix(int[,] m, string path)
        {
            string csv = "actual/predicted";
            for (int j = 0; j < 26; j++) csv += "," + (char)('a' + j);
            csv += "\r\n";
            for (int i = 0; i < 26; i++)
            {
                csv += (char)('a' + i);
                for (int j = 0; j < 26; j++) csv += "," + m[i, j];
                csv += "\r\n";
            }
            File.WriteAllText(path, csv);
        }
    }
}