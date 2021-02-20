
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
            // GearEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 623);
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
    }
}

