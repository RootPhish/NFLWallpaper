namespace NFLWallpaper
{
    partial class MainForm
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
            if (disposing && (retrieveData != null))
                retrieveData.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.teamSelectionCombo = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.generateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.bgSelectionCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // teamSelectionCombo
            // 
            this.teamSelectionCombo.FormattingEnabled = true;
            this.teamSelectionCombo.Location = new System.Drawing.Point(33, 73);
            this.teamSelectionCombo.Margin = new System.Windows.Forms.Padding(4);
            this.teamSelectionCombo.Name = "teamSelectionCombo";
            this.teamSelectionCombo.Size = new System.Drawing.Size(344, 33);
            this.teamSelectionCombo.Sorted = true;
            this.teamSelectionCombo.TabIndex = 0;
            this.teamSelectionCombo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(435, 36);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1159, 642);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(33, 257);
            this.generateButton.Margin = new System.Windows.Forms.Padding(6);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(150, 44);
            this.generateButton.TabIndex = 5;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select your favourite team";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(231, 257);
            this.saveButton.Margin = new System.Windows.Forms.Padding(6);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(150, 44);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // bgSelectionCombo
            // 
            this.bgSelectionCombo.FormattingEnabled = true;
            this.bgSelectionCombo.Location = new System.Drawing.Point(33, 182);
            this.bgSelectionCombo.Margin = new System.Windows.Forms.Padding(6);
            this.bgSelectionCombo.Name = "bgSelectionCombo";
            this.bgSelectionCombo.Size = new System.Drawing.Size(344, 33);
            this.bgSelectionCombo.TabIndex = 8;
            this.bgSelectionCombo.SelectedIndexChanged += new System.EventHandler(this.bgSelectionCombo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 146);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Select background";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1637, 710);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bgSelectionCombo);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.teamSelectionCombo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "NFLWallpaper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox teamSelectionCombo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ComboBox bgSelectionCombo;
        private System.Windows.Forms.Label label2;
    }
}

