
namespace SkillEditor
{
    partial class SkillEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillEditorForm));
            this.skillsListLabel = new System.Windows.Forms.Label();
            this.addSkillButton = new System.Windows.Forms.Button();
            this.removeSkillButton = new System.Windows.Forms.Button();
            this.skillNameLabel = new System.Windows.Forms.Label();
            this.skillNameBox = new System.Windows.Forms.TextBox();
            this.skillDescriptionLabel = new System.Windows.Forms.Label();
            this.skillDescriptionBox = new System.Windows.Forms.RichTextBox();
            this.skillsBox = new System.Windows.Forms.ListBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.skillSummaryBox = new System.Windows.Forms.RichTextBox();
            this.skillSummaryLabel = new System.Windows.Forms.Label();
            this.skillRefereeBox = new System.Windows.Forms.RichTextBox();
            this.skillRefereeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // skillsListLabel
            // 
            this.skillsListLabel.AutoSize = true;
            this.skillsListLabel.Location = new System.Drawing.Point(12, 33);
            this.skillsListLabel.Name = "skillsListLabel";
            this.skillsListLabel.Size = new System.Drawing.Size(31, 13);
            this.skillsListLabel.TabIndex = 1;
            this.skillsListLabel.Text = "Skills";
            // 
            // addSkillButton
            // 
            this.addSkillButton.Location = new System.Drawing.Point(235, 608);
            this.addSkillButton.Name = "addSkillButton";
            this.addSkillButton.Size = new System.Drawing.Size(75, 23);
            this.addSkillButton.TabIndex = 2;
            this.addSkillButton.Text = "Add";
            this.addSkillButton.UseVisualStyleBackColor = true;
            this.addSkillButton.Click += new System.EventHandler(this.addSkillButton_Click);
            // 
            // removeSkillButton
            // 
            this.removeSkillButton.Location = new System.Drawing.Point(15, 608);
            this.removeSkillButton.Name = "removeSkillButton";
            this.removeSkillButton.Size = new System.Drawing.Size(75, 23);
            this.removeSkillButton.TabIndex = 3;
            this.removeSkillButton.Text = "Remove";
            this.removeSkillButton.UseVisualStyleBackColor = true;
            this.removeSkillButton.Click += new System.EventHandler(this.removeSkillButton_Click);
            // 
            // skillNameLabel
            // 
            this.skillNameLabel.AutoSize = true;
            this.skillNameLabel.Location = new System.Drawing.Point(316, 10);
            this.skillNameLabel.Name = "skillNameLabel";
            this.skillNameLabel.Size = new System.Drawing.Size(35, 13);
            this.skillNameLabel.TabIndex = 4;
            this.skillNameLabel.Text = "Name";
            // 
            // skillNameBox
            // 
            this.skillNameBox.Location = new System.Drawing.Point(316, 26);
            this.skillNameBox.Name = "skillNameBox";
            this.skillNameBox.Size = new System.Drawing.Size(344, 20);
            this.skillNameBox.TabIndex = 5;
            this.skillNameBox.TextChanged += new System.EventHandler(this.skillNameBox_TextChanged);
            // 
            // skillDescriptionLabel
            // 
            this.skillDescriptionLabel.AutoSize = true;
            this.skillDescriptionLabel.Location = new System.Drawing.Point(316, 184);
            this.skillDescriptionLabel.Name = "skillDescriptionLabel";
            this.skillDescriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.skillDescriptionLabel.TabIndex = 6;
            this.skillDescriptionLabel.Text = "Description";
            // 
            // skillDescriptionBox
            // 
            this.skillDescriptionBox.Location = new System.Drawing.Point(316, 200);
            this.skillDescriptionBox.Name = "skillDescriptionBox";
            this.skillDescriptionBox.Size = new System.Drawing.Size(344, 272);
            this.skillDescriptionBox.TabIndex = 12;
            this.skillDescriptionBox.Text = "";
            this.skillDescriptionBox.TextChanged += new System.EventHandler(this.skillDescriptionBox_TextChanged);
            // 
            // skillsBox
            // 
            this.skillsBox.FormattingEnabled = true;
            this.skillsBox.Location = new System.Drawing.Point(15, 54);
            this.skillsBox.Name = "skillsBox";
            this.skillsBox.Size = new System.Drawing.Size(295, 550);
            this.skillsBox.TabIndex = 8;
            this.skillsBox.SelectedIndexChanged += new System.EventHandler(this.skillsBox_SelectedIndexChanged);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(3, 4);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 10;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(232, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // skillSummaryBox
            // 
            this.skillSummaryBox.Location = new System.Drawing.Point(316, 65);
            this.skillSummaryBox.Name = "skillSummaryBox";
            this.skillSummaryBox.Size = new System.Drawing.Size(344, 116);
            this.skillSummaryBox.TabIndex = 7;
            this.skillSummaryBox.Text = "";
            this.skillSummaryBox.TextChanged += new System.EventHandler(this.skillSummaryBox_TextChanged);
            // 
            // skillSummaryLabel
            // 
            this.skillSummaryLabel.AutoSize = true;
            this.skillSummaryLabel.Location = new System.Drawing.Point(316, 49);
            this.skillSummaryLabel.Name = "skillSummaryLabel";
            this.skillSummaryLabel.Size = new System.Drawing.Size(50, 13);
            this.skillSummaryLabel.TabIndex = 11;
            this.skillSummaryLabel.Text = "Summary";
            // 
            // skillRefereeBox
            // 
            this.skillRefereeBox.Location = new System.Drawing.Point(316, 497);
            this.skillRefereeBox.Name = "skillRefereeBox";
            this.skillRefereeBox.Size = new System.Drawing.Size(344, 134);
            this.skillRefereeBox.TabIndex = 14;
            this.skillRefereeBox.Text = "";
            this.skillRefereeBox.TextChanged += new System.EventHandler(this.skillRefereeBox_TextChanged);
            // 
            // skillRefereeLabel
            // 
            this.skillRefereeLabel.AutoSize = true;
            this.skillRefereeLabel.Location = new System.Drawing.Point(316, 481);
            this.skillRefereeLabel.Name = "skillRefereeLabel";
            this.skillRefereeLabel.Size = new System.Drawing.Size(45, 13);
            this.skillRefereeLabel.TabIndex = 13;
            this.skillRefereeLabel.Text = "Referee";
            // 
            // SkillEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 643);
            this.Controls.Add(this.skillRefereeBox);
            this.Controls.Add(this.skillRefereeLabel);
            this.Controls.Add(this.skillSummaryBox);
            this.Controls.Add(this.skillSummaryLabel);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.skillsBox);
            this.Controls.Add(this.skillDescriptionBox);
            this.Controls.Add(this.skillDescriptionLabel);
            this.Controls.Add(this.skillNameBox);
            this.Controls.Add(this.skillNameLabel);
            this.Controls.Add(this.removeSkillButton);
            this.Controls.Add(this.addSkillButton);
            this.Controls.Add(this.skillsListLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SkillEditorForm";
            this.Text = "Traveller Skills Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label skillsListLabel;
        private System.Windows.Forms.Button addSkillButton;
        private System.Windows.Forms.Button removeSkillButton;
        private System.Windows.Forms.Label skillNameLabel;
        private System.Windows.Forms.TextBox skillNameBox;
        private System.Windows.Forms.Label skillDescriptionLabel;
        private System.Windows.Forms.RichTextBox skillDescriptionBox;
        private System.Windows.Forms.ListBox skillsBox;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.RichTextBox skillSummaryBox;
        private System.Windows.Forms.Label skillSummaryLabel;
        private System.Windows.Forms.RichTextBox skillRefereeBox;
        private System.Windows.Forms.Label skillRefereeLabel;
    }
}

