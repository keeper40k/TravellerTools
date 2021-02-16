
namespace TravellerTools.CharGen
{
    partial class SkillSelectionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillSelectionDialog));
            this.skillsRemainingLabel = new System.Windows.Forms.Label();
            this.skillTable1Button = new System.Windows.Forms.Button();
            this.skillTable2Button = new System.Windows.Forms.Button();
            this.skillTable3Button = new System.Windows.Forms.Button();
            this.skillTable4Button = new System.Windows.Forms.Button();
            this.characterDisplay = new System.Windows.Forms.RichTextBox();
            this.characterDisplayLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // skillsRemainingLabel
            // 
            this.skillsRemainingLabel.AutoSize = true;
            this.skillsRemainingLabel.Location = new System.Drawing.Point(13, 13);
            this.skillsRemainingLabel.Name = "skillsRemainingLabel";
            this.skillsRemainingLabel.Size = new System.Drawing.Size(153, 13);
            this.skillsRemainingLabel.TabIndex = 0;
            this.skillsRemainingLabel.Text = "Skill Selections Remaining ... X";
            // 
            // skillTable1Button
            // 
            this.skillTable1Button.Location = new System.Drawing.Point(16, 30);
            this.skillTable1Button.Name = "skillTable1Button";
            this.skillTable1Button.Size = new System.Drawing.Size(130, 100);
            this.skillTable1Button.TabIndex = 1;
            this.skillTable1Button.Text = "button1";
            this.skillTable1Button.UseVisualStyleBackColor = true;
            this.skillTable1Button.Click += new System.EventHandler(this.skillTable1Button_Click);
            // 
            // skillTable2Button
            // 
            this.skillTable2Button.Location = new System.Drawing.Point(152, 29);
            this.skillTable2Button.Name = "skillTable2Button";
            this.skillTable2Button.Size = new System.Drawing.Size(130, 100);
            this.skillTable2Button.TabIndex = 2;
            this.skillTable2Button.Text = "button2";
            this.skillTable2Button.UseVisualStyleBackColor = true;
            this.skillTable2Button.Click += new System.EventHandler(this.skillTable2Button_Click);
            // 
            // skillTable3Button
            // 
            this.skillTable3Button.Location = new System.Drawing.Point(288, 29);
            this.skillTable3Button.Name = "skillTable3Button";
            this.skillTable3Button.Size = new System.Drawing.Size(130, 100);
            this.skillTable3Button.TabIndex = 3;
            this.skillTable3Button.Text = "button3";
            this.skillTable3Button.UseVisualStyleBackColor = true;
            this.skillTable3Button.Click += new System.EventHandler(this.skillTable3Button_Click);
            // 
            // skillTable4Button
            // 
            this.skillTable4Button.Location = new System.Drawing.Point(424, 29);
            this.skillTable4Button.Name = "skillTable4Button";
            this.skillTable4Button.Size = new System.Drawing.Size(130, 100);
            this.skillTable4Button.TabIndex = 4;
            this.skillTable4Button.Text = "button4";
            this.skillTable4Button.UseVisualStyleBackColor = true;
            this.skillTable4Button.Click += new System.EventHandler(this.skillTable4Button_Click);
            // 
            // characterDisplay
            // 
            this.characterDisplay.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.characterDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.characterDisplay.Location = new System.Drawing.Point(16, 161);
            this.characterDisplay.Name = "characterDisplay";
            this.characterDisplay.Size = new System.Drawing.Size(542, 116);
            this.characterDisplay.TabIndex = 28;
            this.characterDisplay.Text = "";
            // 
            // characterDisplayLabel
            // 
            this.characterDisplayLabel.AutoSize = true;
            this.characterDisplayLabel.Location = new System.Drawing.Point(13, 145);
            this.characterDisplayLabel.Name = "characterDisplayLabel";
            this.characterDisplayLabel.Size = new System.Drawing.Size(134, 13);
            this.characterDisplayLabel.TabIndex = 27;
            this.characterDisplayLabel.Text = "Current Traveller Character";
            // 
            // SkillSelectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 289);
            this.Controls.Add(this.characterDisplay);
            this.Controls.Add(this.characterDisplayLabel);
            this.Controls.Add(this.skillTable4Button);
            this.Controls.Add(this.skillTable3Button);
            this.Controls.Add(this.skillTable2Button);
            this.Controls.Add(this.skillTable1Button);
            this.Controls.Add(this.skillsRemainingLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SkillSelectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Skills";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label skillsRemainingLabel;
        private System.Windows.Forms.Button skillTable1Button;
        private System.Windows.Forms.Button skillTable2Button;
        private System.Windows.Forms.Button skillTable3Button;
        private System.Windows.Forms.Button skillTable4Button;
        private System.Windows.Forms.RichTextBox characterDisplay;
        private System.Windows.Forms.Label characterDisplayLabel;
    }
}