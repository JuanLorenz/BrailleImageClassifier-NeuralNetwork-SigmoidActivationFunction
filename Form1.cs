using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Backprop;

namespace BrailleImageClassifier_NeuralNetwork_SigmoidActivationFunction
{
    public partial class Form1 : Form
    {
        NeuralNet net;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files|*.png;*.jpg;*.jpeg;*.bmp";
            outputLetterLbl.Visible = false;
            warningLbl.Visible = false;
            lblTop3.Text = "";
            LoadNet();
        }

        // loads the weights saved by the trainer, with the hidden size it was trained with
        void LoadNet()
        {
            net = null;
            if (!File.Exists(NetHelper.WeightsFile) || !File.Exists(NetHelper.HiddenFile)) return;

            int hidden = int.Parse(File.ReadAllText(NetHelper.HiddenFile));
            net = new NeuralNet(784, hidden, 26);
            net.loadWeights(NetHelper.WeightsFile);
        }

        // Trainer button
        private void btnOpenTrainer_Click_1(object sender, EventArgs e)
        {
            Form2 trainer = new Form2();
            trainer.ShowDialog();
            LoadNet();   // pick up the newly saved weights
        }

        // Upload button
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picInput.Image?.Dispose();
                using (var temp = new Bitmap(openFileDialog1.FileName))
                {
                    picInput.Image = new Bitmap(temp);
                }
                warningLbl.Visible = false;
                outputLetterLbl.Visible = false;
                lblTop3.Text = "";
            }
        }

        // Interpret button
        private void button1_Click(object sender, EventArgs e)
        {
            if (picInput.Image == null)
            {
                warningLbl.Text = "Upload an Image first";
                warningLbl.Visible = true;
                return;
            }
            if (net == null)
            {
                warningLbl.Text = "Train the network first (Trainer)";
                warningLbl.Visible = true;
                return;
            }
            warningLbl.Visible = false;

            double[] pixels = BrailleData.ReadPixels((Bitmap)picInput.Image);
            double[] outputs = NetHelper.GetOutputs(net, pixels);
            int best = NetHelper.BiggestIndex(outputs);

            outputLetterLbl.Text = ((char)('A' + best)).ToString();
            outputLetterLbl.Visible = true;
            lblTop3.Text = "Top 3: " + NetHelper.TopThree(outputs);
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void headerLbl_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e) { }
    }
}