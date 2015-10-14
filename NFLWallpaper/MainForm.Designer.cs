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
            this.label1 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.bgSelectionCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.wallpaperBox = new System.Windows.Forms.GroupBox();
            this.setWallpaperButton = new System.Windows.Forms.Button();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.wallpaperBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // teamSelectionCombo
            // 
            this.teamSelectionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.teamSelectionCombo.FormattingEnabled = true;
            this.teamSelectionCombo.Location = new System.Drawing.Point(16, 38);
            this.teamSelectionCombo.Margin = new System.Windows.Forms.Padding(2);
            this.teamSelectionCombo.Name = "teamSelectionCombo";
            this.teamSelectionCombo.Size = new System.Drawing.Size(174, 21);
            this.teamSelectionCombo.Sorted = true;
            this.teamSelectionCombo.TabIndex = 0;
            this.teamSelectionCombo.SelectionChangeCommitted += new System.EventHandler(this.generateButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(218, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(580, 334);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select your favourite team";
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(16, 131);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save...";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // bgSelectionCombo
            // 
            this.bgSelectionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bgSelectionCombo.FormattingEnabled = true;
            this.bgSelectionCombo.Location = new System.Drawing.Point(16, 95);
            this.bgSelectionCombo.Name = "bgSelectionCombo";
            this.bgSelectionCombo.Size = new System.Drawing.Size(174, 21);
            this.bgSelectionCombo.TabIndex = 8;
            this.bgSelectionCombo.SelectionChangeCommitted += new System.EventHandler(this.generateButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Select background";
            // 
            // wallpaperBox
            // 
            this.wallpaperBox.Controls.Add(this.setWallpaperButton);
            this.wallpaperBox.Controls.Add(this.radioButton3);
            this.wallpaperBox.Controls.Add(this.radioButton2);
            this.wallpaperBox.Controls.Add(this.radioButton1);
            this.wallpaperBox.Enabled = false;
            this.wallpaperBox.Location = new System.Drawing.Point(16, 182);
            this.wallpaperBox.Name = "wallpaperBox";
            this.wallpaperBox.Size = new System.Drawing.Size(178, 120);
            this.wallpaperBox.TabIndex = 11;
            this.wallpaperBox.TabStop = false;
            this.wallpaperBox.Text = "Set as Wallpaper";
            // 
            // setWallpaperButton
            // 
            this.setWallpaperButton.Location = new System.Drawing.Point(7, 91);
            this.setWallpaperButton.Name = "setWallpaperButton";
            this.setWallpaperButton.Size = new System.Drawing.Size(75, 23);
            this.setWallpaperButton.TabIndex = 11;
            this.setWallpaperButton.Text = "Set Wallpaper";
            this.setWallpaperButton.UseVisualStyleBackColor = true;
            this.setWallpaperButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 66);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(48, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Tiled";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 43);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(68, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Centered";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Stretched";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 369);
            this.Controls.Add(this.wallpaperBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bgSelectionCombo);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.teamSelectionCombo);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "NFLWallpaper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.wallpaperBox.ResumeLayout(false);
            this.wallpaperBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox teamSelectionCombo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ComboBox bgSelectionCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox wallpaperBox;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button setWallpaperButton;
    }
}

