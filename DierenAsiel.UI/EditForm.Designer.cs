namespace DierenAsiel.UI
{
    partial class EditForm
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
            this.label30 = new System.Windows.Forms.Label();
            this.RtbAbout = new System.Windows.Forms.RichTextBox();
            this.TxtBreed = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.BtnImagepicker = new System.Windows.Forms.Button();
            this.PbAnimalImage = new System.Windows.Forms.PictureBox();
            this.label22 = new System.Windows.Forms.Label();
            this.RtbCharacteristics = new System.Windows.Forms.RichTextBox();
            this.NudAnimalPrice = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.NudAnimalCage = new System.Windows.Forms.NumericUpDown();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.CbAnimalType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GbAnimalGender = new System.Windows.Forms.GroupBox();
            this.RadAnimalFemale = new System.Windows.Forms.RadioButton();
            this.RadAnimalMale = new System.Windows.Forms.RadioButton();
            this.NudAnimalWeight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.NudAnimalAge = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtAnimalName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OfdImage = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.PbAnimalImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudAnimalPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudAnimalCage)).BeginInit();
            this.GbAnimalGender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudAnimalWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudAnimalAge)).BeginInit();
            this.SuspendLayout();
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(210, 195);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(33, 13);
            this.label30.TabIndex = 44;
            this.label30.Text = "Over:";
            // 
            // RtbAbout
            // 
            this.RtbAbout.Location = new System.Drawing.Point(211, 211);
            this.RtbAbout.Name = "RtbAbout";
            this.RtbAbout.Size = new System.Drawing.Size(175, 146);
            this.RtbAbout.TabIndex = 43;
            this.RtbAbout.Text = "";
            // 
            // TxtBreed
            // 
            this.TxtBreed.Location = new System.Drawing.Point(85, 192);
            this.TxtBreed.Name = "TxtBreed";
            this.TxtBreed.Size = new System.Drawing.Size(120, 20);
            this.TxtBreed.TabIndex = 42;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(16, 195);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(29, 13);
            this.label29.TabIndex = 41;
            this.label29.Text = "Ras:";
            // 
            // BtnImagepicker
            // 
            this.BtnImagepicker.Location = new System.Drawing.Point(213, 165);
            this.BtnImagepicker.Name = "BtnImagepicker";
            this.BtnImagepicker.Size = new System.Drawing.Size(93, 23);
            this.BtnImagepicker.TabIndex = 40;
            this.BtnImagepicker.Text = "Kies afbeelding.";
            this.BtnImagepicker.UseVisualStyleBackColor = true;
            this.BtnImagepicker.Click += new System.EventHandler(this.BtnImagepicker_Click);
            // 
            // PbAnimalImage
            // 
            this.PbAnimalImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PbAnimalImage.Location = new System.Drawing.Point(213, 11);
            this.PbAnimalImage.Name = "PbAnimalImage";
            this.PbAnimalImage.Size = new System.Drawing.Size(173, 151);
            this.PbAnimalImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbAnimalImage.TabIndex = 39;
            this.PbAnimalImage.TabStop = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(19, 267);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(84, 13);
            this.label22.TabIndex = 38;
            this.label22.Text = "Eigenschappen:";
            // 
            // RtbCharacteristics
            // 
            this.RtbCharacteristics.Location = new System.Drawing.Point(21, 283);
            this.RtbCharacteristics.Name = "RtbCharacteristics";
            this.RtbCharacteristics.Size = new System.Drawing.Size(185, 74);
            this.RtbCharacteristics.TabIndex = 37;
            this.RtbCharacteristics.Text = "";
            // 
            // NudAnimalPrice
            // 
            this.NudAnimalPrice.DecimalPlaces = 2;
            this.NudAnimalPrice.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.NudAnimalPrice.Location = new System.Drawing.Point(86, 244);
            this.NudAnimalPrice.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.NudAnimalPrice.Name = "NudAnimalPrice";
            this.NudAnimalPrice.Size = new System.Drawing.Size(120, 20);
            this.NudAnimalPrice.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 246);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Prijs:";
            // 
            // NudAnimalCage
            // 
            this.NudAnimalCage.Location = new System.Drawing.Point(85, 218);
            this.NudAnimalCage.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.NudAnimalCage.Name = "NudAnimalCage";
            this.NudAnimalCage.Size = new System.Drawing.Size(120, 20);
            this.NudAnimalCage.TabIndex = 34;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(21, 363);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(127, 23);
            this.BtnAdd.TabIndex = 33;
            this.BtnAdd.Text = "Voer wijzigingen door.";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Hok:";
            // 
            // CbAnimalType
            // 
            this.CbAnimalType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbAnimalType.FormattingEnabled = true;
            this.CbAnimalType.Location = new System.Drawing.Point(86, 165);
            this.CbAnimalType.Name = "CbAnimalType";
            this.CbAnimalType.Size = new System.Drawing.Size(121, 21);
            this.CbAnimalType.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Diersoort:";
            // 
            // GbAnimalGender
            // 
            this.GbAnimalGender.Controls.Add(this.RadAnimalFemale);
            this.GbAnimalGender.Controls.Add(this.RadAnimalMale);
            this.GbAnimalGender.Location = new System.Drawing.Point(12, 89);
            this.GbAnimalGender.Name = "GbAnimalGender";
            this.GbAnimalGender.Size = new System.Drawing.Size(195, 73);
            this.GbAnimalGender.TabIndex = 29;
            this.GbAnimalGender.TabStop = false;
            this.GbAnimalGender.Text = "Geslacht:";
            // 
            // RadAnimalFemale
            // 
            this.RadAnimalFemale.AutoSize = true;
            this.RadAnimalFemale.Location = new System.Drawing.Point(9, 43);
            this.RadAnimalFemale.Name = "RadAnimalFemale";
            this.RadAnimalFemale.Size = new System.Drawing.Size(55, 17);
            this.RadAnimalFemale.TabIndex = 1;
            this.RadAnimalFemale.Tag = "";
            this.RadAnimalFemale.Text = "Vrouw";
            this.RadAnimalFemale.UseVisualStyleBackColor = true;
            // 
            // RadAnimalMale
            // 
            this.RadAnimalMale.AutoSize = true;
            this.RadAnimalMale.Checked = true;
            this.RadAnimalMale.Location = new System.Drawing.Point(9, 20);
            this.RadAnimalMale.Name = "RadAnimalMale";
            this.RadAnimalMale.Size = new System.Drawing.Size(46, 17);
            this.RadAnimalMale.TabIndex = 0;
            this.RadAnimalMale.TabStop = true;
            this.RadAnimalMale.Tag = "";
            this.RadAnimalMale.Text = "Man";
            this.RadAnimalMale.UseVisualStyleBackColor = true;
            // 
            // NudAnimalWeight
            // 
            this.NudAnimalWeight.Location = new System.Drawing.Point(87, 63);
            this.NudAnimalWeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.NudAnimalWeight.Name = "NudAnimalWeight";
            this.NudAnimalWeight.Size = new System.Drawing.Size(120, 20);
            this.NudAnimalWeight.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Gewicht:";
            // 
            // NudAnimalAge
            // 
            this.NudAnimalAge.Location = new System.Drawing.Point(87, 37);
            this.NudAnimalAge.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.NudAnimalAge.Name = "NudAnimalAge";
            this.NudAnimalAge.Size = new System.Drawing.Size(120, 20);
            this.NudAnimalAge.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Leeftijd:";
            // 
            // TxtAnimalName
            // 
            this.TxtAnimalName.Location = new System.Drawing.Point(87, 11);
            this.TxtAnimalName.Name = "TxtAnimalName";
            this.TxtAnimalName.Size = new System.Drawing.Size(120, 20);
            this.TxtAnimalName.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Naam:";
            // 
            // OfdImage
            // 
            this.OfdImage.Filter = "Image files|*.jpg;*.jpeg;*.png;";
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 401);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.RtbAbout);
            this.Controls.Add(this.TxtBreed);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.BtnImagepicker);
            this.Controls.Add(this.PbAnimalImage);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.RtbCharacteristics);
            this.Controls.Add(this.NudAnimalPrice);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.NudAnimalCage);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CbAnimalType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GbAnimalGender);
            this.Controls.Add(this.NudAnimalWeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NudAnimalAge);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtAnimalName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditForm";
            this.Text = "EditForm";
            ((System.ComponentModel.ISupportInitialize)(this.PbAnimalImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudAnimalPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudAnimalCage)).EndInit();
            this.GbAnimalGender.ResumeLayout(false);
            this.GbAnimalGender.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudAnimalWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudAnimalAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.RichTextBox RtbAbout;
        private System.Windows.Forms.TextBox TxtBreed;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button BtnImagepicker;
        private System.Windows.Forms.PictureBox PbAnimalImage;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.RichTextBox RtbCharacteristics;
        private System.Windows.Forms.NumericUpDown NudAnimalPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown NudAnimalCage;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CbAnimalType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox GbAnimalGender;
        private System.Windows.Forms.RadioButton RadAnimalFemale;
        private System.Windows.Forms.RadioButton RadAnimalMale;
        private System.Windows.Forms.NumericUpDown NudAnimalWeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NudAnimalAge;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtAnimalName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog OfdImage;
    }
}