using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Backprop;

namespace BrailleImageClassifier_NeuralNetwork_SigmoidActivationFunction
{
    public partial class Form2 : Form
    {
        NeuralNet net;
        List<BrailleImage> allImages = new List<BrailleImage>();
        List<BrailleImage> trainSet;
        List<BrailleImage> testSet;
        List<double> trainLoss = new List<double>();
        List<double> testLoss = new List<double>();
        string logCsv = "";
        bool stopTraining = false;
        string csvPath = Path.Combine(Application.StartupPath, "Data", "braille.csv");

        public Form2()
        {
            InitializeComponent();
            btnExperiments.Click += btnExperiments_Click;
            panelPlot.Paint += panelPlot_Paint;
            FormClosing += Form2_FormClosing;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cmbSplit.SelectedIndex = 0;

            Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Data"));
            Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Models"));
        }

        // do not close the window while a training thread is still running
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnTrain.Enabled)
            {
                e.Cancel = true;
                MessageBox.Show("Press Stop and wait until it finishes.");
            }
        }

        // ---------- buttons ----------

        // reads the images from a folder, shows how many were found and saves them as braille.csv
        private void btnBuildCsv_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;

            UseWaitCursor = true;
            allImages = BrailleData.LoadFolder(dialog.SelectedPath);
            BrailleData.SaveCsv(allImages, csvPath);
            UseWaitCursor = false;

            int letters = allImages.Select(i => i.Letter).Distinct().Count();
            Log("Read " + allImages.Count + " images, " + letters + " letters. Saved " + csvPath);
        }

        // loads a csv made by Build CSV (faster than reading the images again)
        private void btnLoadCsv_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV files|*.csv";

            string dataDir = Path.Combine(Application.StartupPath, "Data");
            Directory.CreateDirectory(dataDir);
            dialog.InitialDirectory = dataDir;

            if (dialog.ShowDialog() != DialogResult.OK) return;

            int skipped;
            allImages = BrailleData.LoadCsv(dialog.FileName, out skipped);
            int letters = allImages.Select(i => i.Letter).Distinct().Count();
            Log("Loaded " + allImages.Count + " images, " + letters + " letters, " + skipped + " bad lines skipped");
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            double lrOut, lrIn;
            if (!ReadRates(out lrOut, out lrIn)) return;
            if (!MakeSplit()) return;

            int hidden = (int)nudHidden.Value;
            int epochs = (int)nudEpochs.Value;
            net = new NeuralNet(784, hidden, 26);
            net.setLearningRates(lrOut, lrIn);

            trainLoss.Clear();
            testLoss.Clear();
            logCsv = "epoch,train_loss,test_loss,test_acc\r\n";
            stopTraining = false;
            SetButtons(false);
            Log("Training: hidden " + hidden + ", epochs " + epochs + ", lr " + lrOut + " / " + lrIn);

            Thread t = new Thread(() => TrainLoop(epochs, hidden));
            t.IsBackground = true;
            t.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stopTraining = true;
            Log("Stopping...");
        }

        private void btnEvaluate_Click(object sender, EventArgs e)
        {
            if (net == null || testSet == null)
            {
                MessageBox.Show("Train first.");
                return;
            }
            int[,] matrix = NetHelper.ConfusionMatrix(net, testSet);
            Log(NetHelper.Report(matrix));
            NetHelper.SaveMatrix(matrix, Path.Combine(Application.StartupPath, "Data", "confusion_matrix.csv"));
            Log("Saved confusion_matrix.csv in Data/");
        }

        private void btnSaveWeights_Click(object sender, EventArgs e)
        {
            if (net == null)
            {
                MessageBox.Show("There is no network to save.");
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files|*.txt";

            // Set initial directory to the Models subfolder
            string modelsDir = Path.Combine(Application.StartupPath, "Models");
            Directory.CreateDirectory(modelsDir);
            dialog.InitialDirectory = modelsDir;
            dialog.FileName = "braille_weights.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                net.saveWeights(dialog.FileName);
                Log("Weights saved to " + dialog.FileName);
            }
        }

        private void btnExperiments_Click(object sender, EventArgs e)
        {
            if (!MakeSplit()) return;
            int epochs = (int)nudEpochs.Value;
            stopTraining = false;
            SetButtons(false);

            Thread t = new Thread(() => RunExperiments(epochs));
            t.IsBackground = true;
            t.Start();
        }

        private void lblEpochs_Click(object sender, EventArgs e) { }

        // ---------- training ----------

        // one pass over the whole training set, in a new random order
        void TrainOneEpoch(NeuralNet n)
        {
            BrailleData.Shuffle(trainSet);
            foreach (BrailleImage img in trainSet)
            {
                NetHelper.SetSample(n, img);
                n.learn();
            }
        }

        // runs on a second thread so the window does not freeze
        void TrainLoop(int epochs, int hidden)
        {
            for (int epoch = 1; epoch <= epochs; epoch++)
            {
                if (stopTraining) break;
                TrainOneEpoch(net);


                if (epoch == 1 || epoch % 10 == 0)
                {
                    double trLoss, trAcc, teLoss, teAcc;
                    NetHelper.Test(net, trainSet, out trLoss, out trAcc);
                    NetHelper.Test(net, testSet, out teLoss, out teAcc);
                    int ep = epoch;
                    Invoke(new Action(() => ShowEpoch(ep, trLoss, teLoss, teAcc)));
                }
            }

            // Form1 needs both files: the weights and the hidden size they were trained with
            net.saveWeights(NetHelper.WeightsFile);
            File.WriteAllText(NetHelper.HiddenFile, hidden.ToString());
            File.WriteAllText(Path.Combine(Application.StartupPath, "Data", "training_log.csv"), logCsv);

            Log("Finished. Saved braille_weights.txt (Models/) and training_log.csv (Data/)");
            Invoke(new Action(() => SetButtons(true)));
        }

        // tries every hidden size with every learning rate and adds the results to experiments.csv
        void RunExperiments(int epochs)
        {
            int[] hiddenSizes = { 64, 128, 256 };
            double[] rates = { 0.02, 0.05, 0.1 };   // output layer rate, the input layer uses 75% of it
            string path = Path.Combine(Application.StartupPath, "Data", "experiments.csv");
            if (!File.Exists(path)) File.WriteAllText(path, "hidden,lr_out,lr_in,epochs,test_loss,test_acc\r\n");

            foreach (int hidden in hiddenSizes)
            {
                foreach (double rate in rates)
                {
                    if (stopTraining) break;

                    NeuralNet n = new NeuralNet(784, hidden, 26);
                    n.setLearningRates(rate, rate * 0.75);
                    for (int epoch = 1; epoch <= epochs; epoch++) TrainOneEpoch(n);

                    double loss, acc;
                    NetHelper.Test(n, testSet, out loss, out acc);
                    File.AppendAllText(path, hidden + "," + rate + "," + (rate * 0.75) + "," + epochs + "," + loss.ToString("F5") + "," + acc.ToString("F4") + "\r\n");
                    Log("hidden " + hidden + ", lr " + rate + ": test loss " + loss.ToString("F4") + ", accuracy " + acc.ToString("P1"));
                }
                if (stopTraining) break;
            }
            Log("Experiments finished, results are in Data/experiments.csv");
            Invoke(new Action(() => SetButtons(true)));
        }

        // ---------- helpers ----------

        void Log(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Log(text)));
                return;
            }
            rtbLog.AppendText(text + "\r\n");
        }

        bool ReadRates(out double lrOut, out double lrIn)
        {
            bool ok1 = double.TryParse(txtLrOut.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out lrOut);
            bool ok2 = double.TryParse(txtLrIn.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out lrIn);
            if (!ok1 || !ok2) MessageBox.Show("Learning rates must be numbers like 0.2");
            return ok1 && ok2;
        }

        bool MakeSplit()
        {
            if (allImages.Count == 0)
            {
                MessageBox.Show("Build or load the dataset first.");
                return false;
            }
            trainSet = new List<BrailleImage>();
            testSet = new List<BrailleImage>();

            if (cmbSplit.SelectedIndex == 0)
            {
                BrailleData.RandomSplit(allImages, trainSet, testSet);
            }
            else
            {
                string[] types = { "", "dim", "rot", "whs" };
                BrailleData.HoldOutSplit(allImages, types[cmbSplit.SelectedIndex], trainSet, testSet);
            }
            Log("Train: " + trainSet.Count + ", Test: " + testSet.Count);
            return trainSet.Count > 0 && testSet.Count > 0;
        }

        // while something is running only the Stop button works
        void SetButtons(bool enabled)
        {
            btnBuildCsv.Enabled = enabled;
            btnLoadCsv.Enabled = enabled;
            btnTrain.Enabled = enabled;
            btnEvaluate.Enabled = enabled;
            btnSaveWeights.Enabled = enabled;
            btnExperiments.Enabled = enabled;
        }

        void ShowEpoch(int epoch, double trLoss, double teLoss, double teAcc)
        {
            trainLoss.Add(trLoss);
            testLoss.Add(teLoss);
            logCsv += epoch + "," + trLoss.ToString("F6") + "," + teLoss.ToString("F6") + "," + teAcc.ToString("F4") + "\r\n";
            Log("Epoch " + epoch + ": train loss " + trLoss.ToString("F4") + ", test loss " + teLoss.ToString("F4") + ", test accuracy " + teAcc.ToString("P1"));
            panelPlot.Invalidate();
        }

        // ---------- loss graph ----------

        private void panelPlot_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            g.DrawString("Loss (blue = train, red = test)", Font, Brushes.Black, 5, 5);
            if (trainLoss.Count < 2) return;

            double max = Math.Max(trainLoss.Max(), testLoss.Max());
            DrawLine(g, trainLoss, Pens.Blue, max);
            DrawLine(g, testLoss, Pens.Red, max);
        }

        void DrawLine(Graphics g, List<double> values, Pen pen, double max)
        {
            float width = panelPlot.Width - 20;
            float height = panelPlot.Height - 40;
            for (int i = 1; i < values.Count; i++)
            {
                float x1 = 10 + (i - 1) * width / (values.Count - 1);
                float x2 = 10 + i * width / (values.Count - 1);
                float y1 = 30 + height - (float)(values[i - 1] / max * height);
                float y2 = 30 + height - (float)(values[i] / max * height);
                g.DrawLine(pen, x1, y1, x2, y2);
            }
        }
    }
}