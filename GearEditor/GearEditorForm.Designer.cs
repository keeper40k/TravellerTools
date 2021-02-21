
namespace GearEditor
{
    partial class GearEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GearEditorForm));
            this.addGearButton = new System.Windows.Forms.Button();
            this.gearBox = new System.Windows.Forms.ListBox();
            this.gearListLabel = new System.Windows.Forms.Label();
            this.removeGearButton = new System.Windows.Forms.Button();
            this.gearUpButton = new System.Windows.Forms.Button();
            this.gearDownButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.gearDetailsLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.RichTextBox();
            this.techLevelLabel = new System.Windows.Forms.Label();
            this.weightLabel = new System.Windows.Forms.Label();
            this.valueLabel = new System.Windows.Forms.Label();
            this.techLevelBox = new System.Windows.Forms.NumericUpDown();
            this.weightBox = new System.Windows.Forms.NumericUpDown();
            this.valueBox = new System.Windows.Forms.NumericUpDown();
            this.gearTypeBox = new System.Windows.Forms.ComboBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.weightUnitLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.techLevelBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueBox)).BeginInit();
            this.SuspendLayout();
            // 
            // addGearButton
            // 
            this.addGearButton.Location = new System.Drawing.Point(232, 587);
            this.addGearButton.Name = "addGearButton";
            this.addGearButton.Size = new System.Drawing.Size(75, 23);
            this.addGearButton.TabIndex = 17;
            this.addGearButton.Text = "Add";
            this.addGearButton.UseVisualStyleBackColor = true;
            this.addGearButton.Click += new System.EventHandler(this.addGearButton_Click);
            // 
            // gearBox
            // 
            this.gearBox.FormattingEnabled = true;
            this.gearBox.Location = new System.Drawing.Point(12, 52);
            this.gearBox.Name = "gearBox";
            this.gearBox.Size = new System.Drawing.Size(295, 524);
            this.gearBox.TabIndex = 22;
            this.gearBox.SelectedIndexChanged += new System.EventHandler(this.gearBox_SelectedIndexChanged);
            // 
            // gearListLabel
            // 
            this.gearListLabel.AutoSize = true;
            this.gearListLabel.Location = new System.Drawing.Point(12, 38);
            this.gearListLabel.Name = "gearListLabel";
            this.gearListLabel.Size = new System.Drawing.Size(30, 13);
            this.gearListLabel.TabIndex = 21;
            this.gearListLabel.Text = "Gear";
            // 
            // removeGearButton
            // 
            this.removeGearButton.Location = new System.Drawing.Point(12, 587);
            this.removeGearButton.Name = "removeGearButton";
            this.removeGearButton.Size = new System.Drawing.Size(75, 23);
            this.removeGearButton.TabIndex = 18;
            this.removeGearButton.Text = "Remove";
            this.removeGearButton.UseVisualStyleBackColor = true;
            this.removeGearButton.Click += new System.EventHandler(this.removeGearButton_Click);
            // 
            // gearUpButton
            // 
            this.gearUpButton.Location = new System.Drawing.Point(127, 587);
            this.gearUpButton.Name = "gearUpButton";
            this.gearUpButton.Size = new System.Drawing.Size(28, 23);
            this.gearUpButton.TabIndex = 19;
            this.gearUpButton.Text = "↑";
            this.gearUpButton.UseVisualStyleBackColor = true;
            this.gearUpButton.Click += new System.EventHandler(this.gearUpButton_Click);
            // 
            // gearDownButton
            // 
            this.gearDownButton.Location = new System.Drawing.Point(161, 587);
            this.gearDownButton.Name = "gearDownButton";
            this.gearDownButton.Size = new System.Drawing.Size(28, 23);
            this.gearDownButton.TabIndex = 20;
            this.gearDownButton.Text = "↓";
            this.gearDownButton.UseVisualStyleBackColor = true;
            this.gearDownButton.Click += new System.EventHandler(this.gearDownButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(15, 12);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 24;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(232, 12);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 23;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // gearDetailsLabel
            // 
            this.gearDetailsLabel.AutoSize = true;
            this.gearDetailsLabel.Location = new System.Drawing.Point(326, 13);
            this.gearDetailsLabel.Name = "gearDetailsLabel";
            this.gearDetailsLabel.Size = new System.Drawing.Size(68, 13);
            this.gearDetailsLabel.TabIndex = 25;
            this.gearDetailsLabel.Text = "Gear Details:";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(326, 43);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 26;
            this.nameLabel.Text = "Name";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(370, 43);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(418, 20);
            this.nameBox.TabIndex = 27;
            this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(326, 68);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 28;
            this.descriptionLabel.Text = "Description";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(329, 84);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(459, 119);
            this.descriptionBox.TabIndex = 29;
            this.descriptionBox.Text = "";
            this.descriptionBox.TextChanged += new System.EventHandler(this.descriptionBox_TextChanged);
            // 
            // techLevelLabel
            // 
            this.techLevelLabel.AutoSize = true;
            this.techLevelLabel.Location = new System.Drawing.Point(326, 213);
            this.techLevelLabel.Name = "techLevelLabel";
            this.techLevelLabel.Size = new System.Drawing.Size(61, 13);
            this.techLevelLabel.TabIndex = 30;
            this.techLevelLabel.Text = "Tech Level";
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Location = new System.Drawing.Point(326, 240);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(41, 13);
            this.weightLabel.TabIndex = 31;
            this.weightLabel.Text = "Weight";
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(326, 268);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(68, 13);
            this.valueLabel.TabIndex = 32;
            this.valueLabel.Text = "Value        Cr";
            // 
            // techLevelBox
            // 
            this.techLevelBox.Location = new System.Drawing.Point(394, 211);
            this.techLevelBox.Maximum = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.techLevelBox.Name = "techLevelBox";
            this.techLevelBox.Size = new System.Drawing.Size(120, 20);
            this.techLevelBox.TabIndex = 33;
            this.techLevelBox.ValueChanged += new System.EventHandler(this.techLevelBox_ValueChanged);
            // 
            // weightBox
            // 
            this.weightBox.Location = new System.Drawing.Point(394, 238);
            this.weightBox.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.weightBox.Name = "weightBox";
            this.weightBox.Size = new System.Drawing.Size(120, 20);
            this.weightBox.TabIndex = 34;
            this.weightBox.ValueChanged += new System.EventHandler(this.weightBox_ValueChanged);
            // 
            // valueBox
            // 
            this.valueBox.Location = new System.Drawing.Point(394, 266);
            this.valueBox.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(120, 20);
            this.valueBox.TabIndex = 35;
            this.valueBox.ValueChanged += new System.EventHandler(this.valueBox_ValueChanged);
            // 
            // gearTypeBox
            // 
            this.gearTypeBox.FormattingEnabled = true;
            this.gearTypeBox.Location = new System.Drawing.Point(666, 13);
            this.gearTypeBox.Name = "gearTypeBox";
            this.gearTypeBox.Size = new System.Drawing.Size(121, 21);
            this.gearTypeBox.TabIndex = 36;
            this.gearTypeBox.SelectedIndexChanged += new System.EventHandler(this.gearTypeBox_SelectedIndexChanged);
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(591, 17);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(69, 13);
            this.typeLabel.TabIndex = 37;
            this.typeLabel.Text = "Type of Gear";
            // 
            // weightUnitLabel
            // 
            this.weightUnitLabel.AutoSize = true;
            this.weightUnitLabel.Location = new System.Drawing.Point(520, 240);
            this.weightUnitLabel.Name = "weightUnitLabel";
            this.weightUnitLabel.Size = new System.Drawing.Size(35, 13);
            this.weightUnitLabel.TabIndex = 38;
            this.weightUnitLabel.Text = "grams";
            // 
            // GearEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 623);
            this.Controls.Add(this.weightUnitLabel);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.gearTypeBox);
            this.Controls.Add(this.valueBox);
            this.Controls.Add(this.weightBox);
            this.Controls.Add(this.techLevelBox);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.weightLabel);
            this.Controls.Add(this.techLevelLabel);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.gearDetailsLabel);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.gearBox);
            this.Controls.Add(this.gearListLabel);
            this.Controls.Add(this.gearDownButton);
            this.Controls.Add(this.gearUpButton);
            this.Controls.Add(this.removeGearButton);
            this.Controls.Add(this.addGearButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GearEditorForm";
            this.Text = "Traveller Gear Editor";
            ((System.ComponentModel.ISupportInitialize)(this.techLevelBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addGearButton;
        private System.Windows.Forms.ListBox gearBox;
        private System.Windows.Forms.Label gearListLabel;
        private System.Windows.Forms.Button removeGearButton;
        private System.Windows.Forms.Button gearUpButton;
        private System.Windows.Forms.Button gearDownButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label gearDetailsLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.RichTextBox descriptionBox;
        private System.Windows.Forms.Label techLevelLabel;
        private System.Windows.Forms.Label weightLabel;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.NumericUpDown techLevelBox;
        private System.Windows.Forms.NumericUpDown weightBox;
        private System.Windows.Forms.NumericUpDown valueBox;
        private System.Windows.Forms.ComboBox gearTypeBox;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label weightUnitLabel;
    }
}

