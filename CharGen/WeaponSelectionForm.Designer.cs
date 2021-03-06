
namespace TravellerTools.CharGen
{
    partial class WeaponSelectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeaponSelectionForm));
            this.promptLabel = new System.Windows.Forms.Label();
            this.weaponChoiceBox = new System.Windows.Forms.CheckBox();
            this.skillChoiceBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.selectButton = new System.Windows.Forms.Button();
            this.choicesBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // promptLabel
            // 
            this.promptLabel.AutoSize = true;
            this.promptLabel.Location = new System.Drawing.Point(13, 13);
            this.promptLabel.Name = "promptLabel";
            this.promptLabel.Size = new System.Drawing.Size(217, 13);
            this.promptLabel.TabIndex = 0;
            this.promptLabel.Text = "You have rolled X. Please choose on option:";
            // 
            // weaponChoiceBox
            // 
            this.weaponChoiceBox.AutoSize = true;
            this.weaponChoiceBox.Location = new System.Drawing.Point(16, 40);
            this.weaponChoiceBox.Name = "weaponChoiceBox";
            this.weaponChoiceBox.Size = new System.Drawing.Size(104, 17);
            this.weaponChoiceBox.TabIndex = 1;
            this.weaponChoiceBox.Text = "Take a Weapon";
            this.weaponChoiceBox.UseVisualStyleBackColor = true;
            this.weaponChoiceBox.CheckedChanged += new System.EventHandler(this.weaponChoiceBox_CheckedChanged);
            // 
            // skillChoiceBox
            // 
            this.skillChoiceBox.AutoSize = true;
            this.skillChoiceBox.Location = new System.Drawing.Point(16, 75);
            this.skillChoiceBox.Name = "skillChoiceBox";
            this.skillChoiceBox.Size = new System.Drawing.Size(146, 17);
            this.skillChoiceBox.TabIndex = 2;
            this.skillChoiceBox.Text = "Take a Skill Improvement";
            this.skillChoiceBox.UseVisualStyleBackColor = true;
            this.skillChoiceBox.CheckedChanged += new System.EventHandler(this.skillChoiceBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Skill Improvements can only be taken";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "if the character has already taken a";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "weapon as a benefit.";
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(387, 116);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(75, 23);
            this.selectButton.TabIndex = 7;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // choicesBox
            // 
            this.choicesBox.FormattingEnabled = true;
            this.choicesBox.Location = new System.Drawing.Point(285, 9);
            this.choicesBox.Name = "choicesBox";
            this.choicesBox.Size = new System.Drawing.Size(177, 21);
            this.choicesBox.TabIndex = 8;
            this.choicesBox.SelectedIndexChanged += new System.EventHandler(this.choicesBox_SelectedIndexChanged);
            // 
            // WeaponSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 157);
            this.Controls.Add(this.choicesBox);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.skillChoiceBox);
            this.Controls.Add(this.weaponChoiceBox);
            this.Controls.Add(this.promptLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WeaponSelectionForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Weapon Choice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label promptLabel;
        private System.Windows.Forms.CheckBox weaponChoiceBox;
        private System.Windows.Forms.CheckBox skillChoiceBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.ComboBox choicesBox;
    }
}