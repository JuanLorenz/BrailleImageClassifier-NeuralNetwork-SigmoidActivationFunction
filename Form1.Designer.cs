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
            ((System.ComponentModel.ISupportInitialize)picInput).BeginInit();
            SuspendLayout();
            // 
            // picInput
            // 
            picInput.BorderStyle = BorderStyle.FixedSingle;
            picInput.Location = new Point(36, 77);
            picInput.Name = "picInput";
            picInput.Size = new Size(350, 348);
            picInput.SizeMode = PictureBoxSizeMode.Zoom;
            picInput.TabIndex = 0;
            picInput.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(140, 41);
            label1.Name = "label1";
            label1.Size = new Size(134, 20);
            label1.TabIndex = 1;
            label1.Text = "Uploaded Image ";
            label1.Click += label1_Click;
            // 
            // interpretBtn
            // 
            interpretBtn.Location = new Point(215, 441);
            interpretBtn.Name = "interpretBtn";
            interpretBtn.Size = new Size(94, 29);
            interpretBtn.TabIndex = 2;
            interpretBtn.Text = "Interpret";
            interpretBtn.UseVisualStyleBackColor = true;
            interpretBtn.Click += button1_Click;
            // 
            // uploadBtn
            // 
            uploadBtn.Location = new Point(98, 441);
            uploadBtn.Name = "uploadBtn";
            uploadBtn.Size = new Size(94, 29);
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
            warningLbl.Location = new Point(120, 488);
            warningLbl.Name = "warningLbl";
            warningLbl.Size = new Size(161, 20);
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
            headerLbl.Font = new Font("Gill Sans Ultra Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            headerLbl.Location = new Point(471, 145);
            headerLbl.Name = "headerLbl";
            headerLbl.Size = new Size(286, 42);
            headerLbl.TabIndex = 5;
            headerLbl.Text = "Interpretation:";
            // 
            // outputLetterLbl
            // 
            outputLetterLbl.AutoSize = true;
            outputLetterLbl.Font = new Font("Gill Sans Ultra Bold", 72F, FontStyle.Regular, GraphicsUnit.Point, 0);
            outputLetterLbl.Location = new Point(520, 201);
            outputLetterLbl.Name = "outputLetterLbl";
            outputLetterLbl.Size = new Size(189, 168);
            outputLetterLbl.TabIndex = 6;
            outputLetterLbl.Text = "A";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(865, 531);
            Controls.Add(outputLetterLbl);
            Controls.Add(headerLbl);
            Controls.Add(warningLbl);
            Controls.Add(uploadBtn);
            Controls.Add(interpretBtn);
            Controls.Add(label1);
            Controls.Add(picInput);
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
    }
}
