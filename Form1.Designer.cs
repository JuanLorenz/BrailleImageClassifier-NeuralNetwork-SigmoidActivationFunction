namespace BrailleImageClassifier_NeuralNetwork_SigmoidActivationFunction
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            picInput = new PictureBox();
            label1 = new Label();
            interpretBtn = new Button();
            uploadBtn = new Button();
            warningLbl = new Label();
            openFileDialog1 = new OpenFileDialog();
            headerLbl = new Label();
            outputLetterLbl = new Label();
            btnOpenTrainer = new Button();
            lblTop3 = new Label();
            ((System.ComponentModel.ISupportInitialize)picInput).BeginInit();
            SuspendLayout();
            // 
            // picInput
            // 
            picInput.BorderStyle = BorderStyle.FixedSingle;
            picInput.Location = new Point(58, 123);
            picInput.Margin = new Padding(5);
            picInput.Name = "picInput";
            picInput.Size = new Size(568, 556);
            picInput.SizeMode = PictureBoxSizeMode.Zoom;
            picInput.TabIndex = 0;
            picInput.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(228, 66);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(211, 32);
            label1.TabIndex = 1;
            label1.Text = "Uploaded Image ";
            label1.Click += label1_Click;
            // 
            // interpretBtn
            // 
            interpretBtn.Location = new Point(349, 706);
            interpretBtn.Margin = new Padding(5);
            interpretBtn.Name = "interpretBtn";
            interpretBtn.Size = new Size(153, 46);
            interpretBtn.TabIndex = 2;
            interpretBtn.Text = "Interpret";
            interpretBtn.UseVisualStyleBackColor = true;
            interpretBtn.Click += button1_Click;
            // 
            // uploadBtn
            // 
            uploadBtn.Location = new Point(159, 706);
            uploadBtn.Margin = new Padding(5);
            uploadBtn.Name = "uploadBtn";
            uploadBtn.Size = new Size(153, 46);
            uploadBtn.TabIndex = 3;
            uploadBtn.Text = "Upload";
            uploadBtn.UseVisualStyleBackColor = true;
            uploadBtn.Click += button1_Click_1;
            // 
            // warningLbl
            // 
            warningLbl.AutoSize = true;
            warningLbl.BackColor = SystemColors.Control;
            warningLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            warningLbl.ForeColor = Color.Red;
            warningLbl.Location = new Point(195, 781);
            warningLbl.Margin = new Padding(5, 0, 5, 0);
            warningLbl.Name = "warningLbl";
            warningLbl.Size = new Size(262, 32);
            warningLbl.TabIndex = 4;
            warningLbl.Text = "Upload an Image first";
            warningLbl.Visible = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "\"Image files|*.png;*.jpg;*.jpeg;*.bmp\"";
            openFileDialog1.FileOk += openFileDialog1_FileOk;
            // 
            // headerLbl
            // 
            headerLbl.AutoSize = true;
            headerLbl.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            headerLbl.Location = new Point(765, 232);
            headerLbl.Margin = new Padding(5, 0, 5, 0);
            headerLbl.Name = "headerLbl";
            headerLbl.Size = new Size(290, 52);
            headerLbl.TabIndex = 5;
            headerLbl.Text = "Interpretation:";
            headerLbl.Click += headerLbl_Click;
            // 
            // outputLetterLbl
            // 
            outputLetterLbl.AutoSize = true;
            outputLetterLbl.Font = new Font("Microsoft Sans Serif", 72F, FontStyle.Regular, GraphicsUnit.Point, 0);
            outputLetterLbl.Location = new Point(845, 322);
            outputLetterLbl.Margin = new Padding(5, 0, 5, 0);
            outputLetterLbl.Name = "outputLetterLbl";
            outputLetterLbl.Size = new Size(220, 217);
            outputLetterLbl.TabIndex = 6;
            outputLetterLbl.Text = "A";
            // 
            // btnOpenTrainer
            // 
            btnOpenTrainer.Location = new Point(803, 76);
            btnOpenTrainer.Name = "btnOpenTrainer";
            btnOpenTrainer.Size = new Size(150, 46);
            btnOpenTrainer.TabIndex = 7;
            btnOpenTrainer.Text = "Trainer";
            btnOpenTrainer.UseVisualStyleBackColor = true;
            btnOpenTrainer.Click += btnOpenTrainer_Click_1;
            // 
            // lblTop3
            // 
            lblTop3.AutoSize = true;
            lblTop3.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTop3.Location = new Point(803, 700);
            lblTop3.Margin = new Padding(5, 0, 5, 0);
            lblTop3.Name = "lblTop3";
            lblTop3.Size = new Size(160, 52);
            lblTop3.TabIndex = 8;
            lblTop3.Text = "Top 3 -";
            lblTop3.Click += label2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1406, 850);
            Controls.Add(lblTop3);
            Controls.Add(btnOpenTrainer);
            Controls.Add(outputLetterLbl);
            Controls.Add(headerLbl);
            Controls.Add(warningLbl);
            Controls.Add(uploadBtn);
            Controls.Add(interpretBtn);
            Controls.Add(label1);
            Controls.Add(picInput);
            Margin = new Padding(5);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)picInput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picInput;
        private Label label1;
        private Button interpretBtn;
        private Button uploadBtn;
        private Label warningLbl;
        private OpenFileDialog openFileDialog1;
        private Label headerLbl;
        private Label outputLetterLbl;
        private Button btnOpenTrainer;
        private Label lblTop3;
    }
}
