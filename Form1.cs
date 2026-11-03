namespace BrailleImageClassifier_NeuralNetwork_SigmoidActivationFunction
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (picInput.Image == null)
            {
                warningLbl.Visible = true;
                return;
            }

            warningLbl.Visible = false;

            // insert logic related to the neural network here.
        }

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
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
