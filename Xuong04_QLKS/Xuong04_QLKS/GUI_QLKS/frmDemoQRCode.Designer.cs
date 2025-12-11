namespace GUI_PolyCafe
{
    partial class frmDemoQRCode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            butGenQRCode = new Button();
            pictureBox1 = new PictureBox();
            txtInput = new TextBox();
            label1 = new Label();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // butGenQRCode
            // 
            butGenQRCode.Location = new Point(510, 199);
            butGenQRCode.Margin = new Padding(3, 4, 3, 4);
            butGenQRCode.Name = "butGenQRCode";
            butGenQRCode.Size = new Size(199, 86);
            butGenQRCode.TabIndex = 0;
            butGenQRCode.Text = "Genarate QRCode";
            butGenQRCode.UseVisualStyleBackColor = true;
            butGenQRCode.Click += butGenQRCode_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(59, 179);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(341, 294);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // txtInput
            // 
            txtInput.Location = new Point(130, 82);
            txtInput.Margin = new Padding(3, 4, 3, 4);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(220, 27);
            txtInput.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(56, 82);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 3;
            label1.Text = "Số HĐ";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(510, 339);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(199, 86);
            btnSave.TabIndex = 4;
            btnSave.Text = "Download QRCode";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // frmDemoQRCode
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(843, 606);
            Controls.Add(btnSave);
            Controls.Add(label1);
            Controls.Add(txtInput);
            Controls.Add(pictureBox1);
            Controls.Add(butGenQRCode);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmDemoQRCode";
            Text = "frmDemoQRCode";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label1;
        protected internal System.Windows.Forms.Button butGenQRCode;
        protected internal Button btnSave;
    }
}