namespace BrailleImageClassifier_NeuralNetwork_SigmoidActivationFunction
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            nudHidden = new NumericUpDown();
            nudEpochs = new NumericUpDown();
            txtLrOut = new TextBox();
            txtLrIn = new TextBox();
            cmbSplit = new ComboBox();
            btnBuildCsv = new Button();
            btnLoadCsv = new Button();
            btnTrain = new Button();
            btnStop = new Button();
            btnEvaluate = new Button();
            btnSaveWeights = new Button();
            panelPlot = new Panel();
            rtbLog = new RichTextBox();
            lblHidden = new Label();
            lblEpochs = new Label();
            lblLrOut = new Label();
            lblLrIn = new Label();
            lblSplit = new Label();
            btnExperiments = new Button();
            ((System.ComponentModel.ISupportInitialize)nudHidden).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudEpochs).BeginInit();
            SuspendLayout();
            // 
            // nudHidden
            // 
            nudHidden.Location = new Point(528, 45);
            nudHidden.Maximum = new decimal(new int[] { 512, 0, 0, 0 });
            nudHidden.Minimum = new decimal(new int[] { 4, 0, 0, 0 });
            nudHidden.Name = "nudHidden";
            nudHidden.Size = new Size(134, 39);
            nudHidden.TabIndex = 5;
            nudHidden.Value = new decimal(new int[] { 128, 0, 0, 0 });
            // 
            // nudEpochs
            // 
            nudEpochs.Location = new Point(796, 45);
            nudEpochs.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            nudEpochs.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudEpochs.Name = "nudEpochs";
            nudEpochs.Size = new Size(166, 39);
            nudEpochs.TabIndex = 7;
            nudEpochs.Value = new decimal(new int[] { 200, 0, 0, 0 });
            // 
            // txtLrOut
            // 
            txtLrOut.Location = new Point(1087, 46);
            txtLrOut.Name = "txtLrOut";
            txtLrOut.Size = new Size(116, 39);
            txtLrOut.TabIndex = 9;
            txtLrOut.Text = "0.05";
            // 
            // txtLrIn
            // 
            txtLrIn.Location = new Point(1314, 45);
            txtLrIn.Name = "txtLrIn";
            txtLrIn.Size = new Size(132, 39);
            txtLrIn.TabIndex = 11;
            txtLrIn.Text = "0.0375";
            // 
            // cmbSplit
            // 
            cmbSplit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSplit.FormattingEnabled = true;
            cmbSplit.Items.AddRange(new object[] { "Random 80/20", "Hold out: dim", "Hold out: rot", "Hold out: whs" });
            cmbSplit.Location = new Point(229, 46);
            cmbSplit.Name = "cmbSplit";
            cmbSplit.Size = new Size(172, 40);
            cmbSplit.TabIndex = 3;
            // 
            // btnBuildCsv
            // 
            btnBuildCsv.Location = new Point(9, 45);
            btnBuildCsv.Name = "btnBuildCsv";
            btnBuildCsv.Size = new Size(142, 136);
            btnBuildCsv.TabIndex = 0;
            btnBuildCsv.Text = "Build CSV";
            btnBuildCsv.Click += btnBuildCsv_Click;
            // 
            // btnLoadCsv
            // 
            btnLoadCsv.Location = new Point(180, 115);
            btnLoadCsv.Name = "btnLoadCsv";
            btnLoadCsv.Size = new Size(164, 72);
            btnLoadCsv.TabIndex = 1;
            btnLoadCsv.Text = "Load CSV";
            btnLoadCsv.Click += btnLoadCsv_Click;
            // 
            // btnTrain
            // 
            btnTrain.Location = new Point(425, 115);
            btnTrain.Name = "btnTrain";
            btnTrain.Size = new Size(124, 78);
            btnTrain.TabIndex = 12;
            btnTrain.Text = "Train";
            btnTrain.Click += btnTrain_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(613, 115);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(143, 78);
            btnStop.TabIndex = 13;
            btnStop.Text = "Stop";
            btnStop.Click += btnStop_Click;
            // 
            // btnEvaluate
            // 
            btnEvaluate.Location = new Point(828, 115);
            btnEvaluate.Name = "btnEvaluate";
            btnEvaluate.Size = new Size(164, 78);
            btnEvaluate.TabIndex = 14;
            btnEvaluate.Text = "Evaluate";
            btnEvaluate.Click += btnEvaluate_Click;
            // 
            // btnSaveWeights
            // 
            btnSaveWeights.Location = new Point(1053, 115);
            btnSaveWeights.Name = "btnSaveWeights";
            btnSaveWeights.Size = new Size(172, 78);
            btnSaveWeights.TabIndex = 15;
            btnSaveWeights.Text = "Save Weights";
            btnSaveWeights.Click += btnSaveWeights_Click;
            // 
            // panelPlot
            // 
            panelPlot.BackColor = Color.White;
            panelPlot.BorderStyle = BorderStyle.FixedSingle;
            panelPlot.Location = new Point(180, 268);
            panelPlot.Name = "panelPlot";
            panelPlot.Size = new Size(1080, 380);
            panelPlot.TabIndex = 16;
            // 
            // rtbLog
            // 
            rtbLog.Font = new Font("Consolas", 9F);
            rtbLog.Location = new Point(180, 744);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(1080, 200);
            rtbLog.TabIndex = 17;
            rtbLog.Text = "";
            // 
            // lblHidden
            // 
            lblHidden.AutoSize = true;
            lblHidden.Location = new Point(425, 49);
            lblHidden.Name = "lblHidden";
            lblHidden.Size = new Size(97, 32);
            lblHidden.TabIndex = 4;
            lblHidden.Text = "Hidden:";
            // 
            // lblEpochs
            // 
            lblEpochs.AutoSize = true;
            lblEpochs.Location = new Point(696, 49);
            lblEpochs.Name = "lblEpochs";
            lblEpochs.Size = new Size(94, 32);
            lblEpochs.TabIndex = 6;
            lblEpochs.Text = "Epochs:";
            lblEpochs.Click += lblEpochs_Click;
            // 
            // lblLrOut
            // 
            lblLrOut.AutoSize = true;
            lblLrOut.Location = new Point(990, 52);
            lblLrOut.Name = "lblLrOut";
            lblLrOut.Size = new Size(91, 32);
            lblLrOut.TabIndex = 8;
            lblLrOut.Text = "LR Out:";
            // 
            // lblLrIn
            // 
            lblLrIn.AutoSize = true;
            lblLrIn.Location = new Point(1237, 49);
            lblLrIn.Name = "lblLrIn";
            lblLrIn.Size = new Size(71, 32);
            lblLrIn.TabIndex = 10;
            lblLrIn.Text = "LR In:";
            // 
            // lblSplit
            // 
            lblSplit.AutoSize = true;
            lblSplit.Location = new Point(157, 48);
            lblSplit.Name = "lblSplit";
            lblSplit.Size = new Size(66, 32);
            lblSplit.TabIndex = 2;
            lblSplit.Text = "Split:";
            // 
            // btnExperiments
            // 
            btnExperiments.Location = new Point(1274, 115);
            btnExperiments.Name = "btnExperiments";
            btnExperiments.Size = new Size(172, 78);
            btnExperiments.TabIndex = 18;
            btnExperiments.Text = "Run Experiments";
            // 
            // Form2
            // 
            ClientSize = new Size(1470, 1056);
            Controls.Add(btnExperiments);
            Controls.Add(btnBuildCsv);
            Controls.Add(btnLoadCsv);
            Controls.Add(lblSplit);
            Controls.Add(cmbSplit);
            Controls.Add(lblHidden);
            Controls.Add(nudHidden);
            Controls.Add(lblEpochs);
            Controls.Add(nudEpochs);
            Controls.Add(lblLrOut);
            Controls.Add(txtLrOut);
            Controls.Add(lblLrIn);
            Controls.Add(txtLrIn);
            Controls.Add(btnTrain);
            Controls.Add(btnStop);
            Controls.Add(btnEvaluate);
            Controls.Add(btnSaveWeights);
            Controls.Add(panelPlot);
            Controls.Add(rtbLog);
            Name = "Form2";
            Text = "Braille Reader - Trainer Tool";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)nudHidden).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudEpochs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudHidden;
        private System.Windows.Forms.NumericUpDown nudEpochs;
        private System.Windows.Forms.TextBox txtLrOut;
        private System.Windows.Forms.TextBox txtLrIn;
        private System.Windows.Forms.ComboBox cmbSplit;
        private System.Windows.Forms.Button btnBuildCsv;
        private System.Windows.Forms.Button btnLoadCsv;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnEvaluate;
        private System.Windows.Forms.Button btnSaveWeights;
        private System.Windows.Forms.Panel panelPlot;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Label lblHidden;
        private System.Windows.Forms.Label lblEpochs;
        private System.Windows.Forms.Label lblLrOut;
        private System.Windows.Forms.Label lblLrIn;
        private System.Windows.Forms.Label lblSplit;
        private Button btnExperiments;
    }
}