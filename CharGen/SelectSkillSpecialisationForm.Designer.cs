
namespace TravellerTools.CharGen
{
    partial class SelectSkillSpecialisationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectSkillSpecialisationForm));
            this.skillSpecialisationsBox = new System.Windows.Forms.ComboBox();
            this.specialisationSummaryBox = new System.Windows.Forms.RichTextBox();
            this.selectButton = new System.Windows.Forms.Button();
            this.chooseLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // skillSpecialisationsBox
            // 
            this.skillSpecialisationsBox.FormattingEnabled = true;
            this.skillSpecialisationsBox.Location = new System.Drawing.Point(12, 29);
            this.skillSpecialisationsBox.Name = "skillSpecialisationsBox";
            this.skillSpecialisationsBox.Size = new System.Drawing.Size(305, 21);
            this.skillSpecialisationsBox.TabIndex = 0;
            this.skillSpecialisationsBox.SelectedIndexChanged += new System.EventHandler(this.skillSpecialisationsBox_SelectedIndexChanged);
            // 
            // specialisationSummaryBox
            // 
            this.specialisationSummaryBox.Enabled = false;
            this.specialisationSummaryBox.Location = new System.Drawing.Point(12, 56);
            this.specialisationSummaryBox.Name = "specialisationSummaryBox";
            this.specialisationSummaryBox.Size = new System.Drawing.Size(305, 96);
            this.specialisationSummaryBox.TabIndex = 1;
            this.specialisationSummaryBox.Text = "";
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(242, 158);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(75, 23);
            this.selectButton.TabIndex = 2;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // chooseLabel
            // 
            this.chooseLabel.AutoSize = true;
            this.chooseLabel.Location = new System.Drawing.Point(13, 13);
            this.chooseLabel.Name = "chooseLabel";
            this.chooseLabel.Size = new System.Drawing.Size(133, 13);
            this.chooseLabel.TabIndex = 3;
            this.chooseLabel.Text = "Choose Skill Specialisation";
            // 
            // SelectSkillSpecialisationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 197);
            this.Controls.Add(this.chooseLabel);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.specialisationSummaryBox);
            this.Controls.Add(this.skillSpecialisationsBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectSkillSpecialisationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Skill Specialisation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox skillSpecialisationsBox;
        private System.Windows.Forms.RichTextBox specialisationSummaryBox;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Label chooseLabel;
    }
}