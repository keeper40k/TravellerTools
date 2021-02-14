
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
            this.skillUpButton = new System.Windows.Forms.Button();
            this.skillDownButton = new System.Windows.Forms.Button();
            this.skillHasSpecialisationsBox = new System.Windows.Forms.CheckBox();
            this.specialisationSkillsBox = new System.Windows.Forms.ListBox();
            this.specialisationSkillsLabel = new System.Windows.Forms.Label();
            this.specialisationSkillRefereeBox = new System.Windows.Forms.RichTextBox();
            this.specialisationSkillRefereeLabel = new System.Windows.Forms.Label();
            this.specialisationSkillSummaryBox = new System.Windows.Forms.RichTextBox();
            this.specialisationSkillSummaryLabel = new System.Windows.Forms.Label();
            this.specialisationSkillDescriptionBox = new System.Windows.Forms.RichTextBox();
            this.specialisationSkillDescriptionLabel = new System.Windows.Forms.Label();
            this.specialisationSkillNameBox = new System.Windows.Forms.TextBox();
            this.specialisationSkillNameLabel = new System.Windows.Forms.Label();
            this.specialistSkillDownButton = new System.Windows.Forms.Button();
            this.specialistSkillUpButton = new System.Windows.Forms.Button();
            this.removeSpecialistSkillButton = new System.Windows.Forms.Button();
            this.addSpecialistSkillButton = new System.Windows.Forms.Button();
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
            this.skillDescriptionLabel.Location = new System.Drawing.Point(316, 207);
            this.skillDescriptionLabel.Name = "skillDescriptionLabel";
            this.skillDescriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.skillDescriptionLabel.TabIndex = 6;
            this.skillDescriptionLabel.Text = "Description";
            // 
            // skillDescriptionBox
            // 
            this.skillDescriptionBox.Location = new System.Drawing.Point(316, 223);
            this.skillDescriptionBox.Name = "skillDescriptionBox";
            this.skillDescriptionBox.Size = new System.Drawing.Size(344, 249);
            this.skillDescriptionBox.TabIndex = 12;
            this.skillDescriptionBox.Text = "";
            this.skillDescriptionBox.TextChanged += new System.EventHandler(this.skillDescriptionBox_TextChanged);
            // 
            // skillsBox
            // 
            this.skillsBox.FormattingEnabled = true;
            this.skillsBox.Location = new System.Drawing.Point(3, 54);
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
            this.skillSummaryBox.Location = new System.Drawing.Point(316, 88);
            this.skillSummaryBox.Name = "skillSummaryBox";
            this.skillSummaryBox.Size = new System.Drawing.Size(344, 116);
            this.skillSummaryBox.TabIndex = 7;
            this.skillSummaryBox.Text = "";
            this.skillSummaryBox.TextChanged += new System.EventHandler(this.skillSummaryBox_TextChanged);
            // 
            // skillSummaryLabel
            // 
            this.skillSummaryLabel.AutoSize = true;
            this.skillSummaryLabel.Location = new System.Drawing.Point(316, 72);
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
            // skillUpButton
            // 
            this.skillUpButton.Location = new System.Drawing.Point(130, 608);
            this.skillUpButton.Name = "skillUpButton";
            this.skillUpButton.Size = new System.Drawing.Size(28, 23);
            this.skillUpButton.TabIndex = 15;
            this.skillUpButton.Text = "↑";
            this.skillUpButton.UseVisualStyleBackColor = true;
            this.skillUpButton.Click += new System.EventHandler(this.skillUpButton_Click);
            // 
            // skillDownButton
            // 
            this.skillDownButton.Location = new System.Drawing.Point(164, 608);
            this.skillDownButton.Name = "skillDownButton";
            this.skillDownButton.Size = new System.Drawing.Size(28, 23);
            this.skillDownButton.TabIndex = 16;
            this.skillDownButton.Text = "↓";
            this.skillDownButton.UseVisualStyleBackColor = true;
            this.skillDownButton.Click += new System.EventHandler(this.skillDownButton_Click);
            // 
            // skillHasSpecialisationsBox
            // 
            this.skillHasSpecialisationsBox.AutoSize = true;
            this.skillHasSpecialisationsBox.Location = new System.Drawing.Point(542, 52);
            this.skillHasSpecialisationsBox.Name = "skillHasSpecialisationsBox";
            this.skillHasSpecialisationsBox.Size = new System.Drawing.Size(118, 17);
            this.skillHasSpecialisationsBox.TabIndex = 17;
            this.skillHasSpecialisationsBox.Text = "Skill Specialisations";
            this.skillHasSpecialisationsBox.UseVisualStyleBackColor = true;
            this.skillHasSpecialisationsBox.CheckedChanged += new System.EventHandler(this.hasSpecialisationsBox_CheckedChanged);
            // 
            // specialisationSkillsBox
            // 
            this.specialisationSkillsBox.FormattingEnabled = true;
            this.specialisationSkillsBox.Location = new System.Drawing.Point(692, 54);
            this.specialisationSkillsBox.Name = "specialisationSkillsBox";
            this.specialisationSkillsBox.Size = new System.Drawing.Size(295, 550);
            this.specialisationSkillsBox.TabIndex = 19;
            this.specialisationSkillsBox.SelectedIndexChanged += new System.EventHandler(this.specialisationSkillsBox_SelectedIndexChanged);
            // 
            // specialisationSkillsLabel
            // 
            this.specialisationSkillsLabel.AutoSize = true;
            this.specialisationSkillsLabel.Location = new System.Drawing.Point(689, 33);
            this.specialisationSkillsLabel.Name = "specialisationSkillsLabel";
            this.specialisationSkillsLabel.Size = new System.Drawing.Size(99, 13);
            this.specialisationSkillsLabel.TabIndex = 18;
            this.specialisationSkillsLabel.Text = "Specialisation Skills";
            // 
            // specialisationSkillRefereeBox
            // 
            this.specialisationSkillRefereeBox.Location = new System.Drawing.Point(993, 497);
            this.specialisationSkillRefereeBox.Name = "specialisationSkillRefereeBox";
            this.specialisationSkillRefereeBox.Size = new System.Drawing.Size(344, 134);
            this.specialisationSkillRefereeBox.TabIndex = 25;
            this.specialisationSkillRefereeBox.Text = "";
            this.specialisationSkillRefereeBox.TextChanged += new System.EventHandler(this.specialisationSkillRefereeBox_TextChanged);
            // 
            // specialisationSkillRefereeLabel
            // 
            this.specialisationSkillRefereeLabel.AutoSize = true;
            this.specialisationSkillRefereeLabel.Location = new System.Drawing.Point(993, 481);
            this.specialisationSkillRefereeLabel.Name = "specialisationSkillRefereeLabel";
            this.specialisationSkillRefereeLabel.Size = new System.Drawing.Size(45, 13);
            this.specialisationSkillRefereeLabel.TabIndex = 24;
            this.specialisationSkillRefereeLabel.Text = "Referee";
            // 
            // specialisationSkillSummaryBox
            // 
            this.specialisationSkillSummaryBox.Location = new System.Drawing.Point(993, 88);
            this.specialisationSkillSummaryBox.Name = "specialisationSkillSummaryBox";
            this.specialisationSkillSummaryBox.Size = new System.Drawing.Size(344, 116);
            this.specialisationSkillSummaryBox.TabIndex = 21;
            this.specialisationSkillSummaryBox.Text = "";
            this.specialisationSkillSummaryBox.TextChanged += new System.EventHandler(this.specialisationSkillSummaryBox_TextChanged);
            // 
            // specialisationSkillSummaryLabel
            // 
            this.specialisationSkillSummaryLabel.AutoSize = true;
            this.specialisationSkillSummaryLabel.Location = new System.Drawing.Point(993, 72);
            this.specialisationSkillSummaryLabel.Name = "specialisationSkillSummaryLabel";
            this.specialisationSkillSummaryLabel.Size = new System.Drawing.Size(50, 13);
            this.specialisationSkillSummaryLabel.TabIndex = 22;
            this.specialisationSkillSummaryLabel.Text = "Summary";
            // 
            // specialisationSkillDescriptionBox
            // 
            this.specialisationSkillDescriptionBox.Location = new System.Drawing.Point(993, 223);
            this.specialisationSkillDescriptionBox.Name = "specialisationSkillDescriptionBox";
            this.specialisationSkillDescriptionBox.Size = new System.Drawing.Size(344, 249);
            this.specialisationSkillDescriptionBox.TabIndex = 23;
            this.specialisationSkillDescriptionBox.Text = "";
            this.specialisationSkillDescriptionBox.TextChanged += new System.EventHandler(this.specialisationSkillDescriptionBox_TextChanged);
            // 
            // specialisationSkillDescriptionLabel
            // 
            this.specialisationSkillDescriptionLabel.AutoSize = true;
            this.specialisationSkillDescriptionLabel.Location = new System.Drawing.Point(993, 207);
            this.specialisationSkillDescriptionLabel.Name = "specialisationSkillDescriptionLabel";
            this.specialisationSkillDescriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.specialisationSkillDescriptionLabel.TabIndex = 20;
            this.specialisationSkillDescriptionLabel.Text = "Description";
            // 
            // specialisationSkillNameBox
            // 
            this.specialisationSkillNameBox.Location = new System.Drawing.Point(993, 49);
            this.specialisationSkillNameBox.Name = "specialisationSkillNameBox";
            this.specialisationSkillNameBox.Size = new System.Drawing.Size(344, 20);
            this.specialisationSkillNameBox.TabIndex = 27;
            this.specialisationSkillNameBox.TextChanged += new System.EventHandler(this.specialisationSkillNameBox_TextChanged);
            // 
            // specialisationSkillNameLabel
            // 
            this.specialisationSkillNameLabel.AutoSize = true;
            this.specialisationSkillNameLabel.Location = new System.Drawing.Point(993, 33);
            this.specialisationSkillNameLabel.Name = "specialisationSkillNameLabel";
            this.specialisationSkillNameLabel.Size = new System.Drawing.Size(35, 13);
            this.specialisationSkillNameLabel.TabIndex = 26;
            this.specialisationSkillNameLabel.Text = "Name";
            // 
            // specialistSkillDownButton
            // 
            this.specialistSkillDownButton.Location = new System.Drawing.Point(841, 610);
            this.specialistSkillDownButton.Name = "specialistSkillDownButton";
            this.specialistSkillDownButton.Size = new System.Drawing.Size(28, 23);
            this.specialistSkillDownButton.TabIndex = 31;
            this.specialistSkillDownButton.Text = "↓";
            this.specialistSkillDownButton.UseVisualStyleBackColor = true;
            this.specialistSkillDownButton.Click += new System.EventHandler(this.specialistSkillDownButton_Click);
            // 
            // specialistSkillUpButton
            // 
            this.specialistSkillUpButton.Location = new System.Drawing.Point(807, 610);
            this.specialistSkillUpButton.Name = "specialistSkillUpButton";
            this.specialistSkillUpButton.Size = new System.Drawing.Size(28, 23);
            this.specialistSkillUpButton.TabIndex = 30;
            this.specialistSkillUpButton.Text = "↑";
            this.specialistSkillUpButton.UseVisualStyleBackColor = true;
            this.specialistSkillUpButton.Click += new System.EventHandler(this.specialistSkillUpButton_Click);
            // 
            // removeSpecialistSkillButton
            // 
            this.removeSpecialistSkillButton.Location = new System.Drawing.Point(692, 610);
            this.removeSpecialistSkillButton.Name = "removeSpecialistSkillButton";
            this.removeSpecialistSkillButton.Size = new System.Drawing.Size(75, 23);
            this.removeSpecialistSkillButton.TabIndex = 29;
            this.removeSpecialistSkillButton.Text = "Remove";
            this.removeSpecialistSkillButton.UseVisualStyleBackColor = true;
            this.removeSpecialistSkillButton.Click += new System.EventHandler(this.removeSpecialistSkillButton_Click);
            // 
            // addSpecialistSkillButton
            // 
            this.addSpecialistSkillButton.Location = new System.Drawing.Point(912, 610);
            this.addSpecialistSkillButton.Name = "addSpecialistSkillButton";
            this.addSpecialistSkillButton.Size = new System.Drawing.Size(75, 23);
            this.addSpecialistSkillButton.TabIndex = 28;
            this.addSpecialistSkillButton.Text = "Add";
            this.addSpecialistSkillButton.UseVisualStyleBackColor = true;
            this.addSpecialistSkillButton.Click += new System.EventHandler(this.addSpecialistSkillButton_Click);
            // 
            // SkillEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 643);
            this.Controls.Add(this.specialistSkillDownButton);
            this.Controls.Add(this.specialistSkillUpButton);
            this.Controls.Add(this.removeSpecialistSkillButton);
            this.Controls.Add(this.addSpecialistSkillButton);
            this.Controls.Add(this.specialisationSkillNameBox);
            this.Controls.Add(this.specialisationSkillNameLabel);
            this.Controls.Add(this.specialisationSkillRefereeBox);
            this.Controls.Add(this.specialisationSkillRefereeLabel);
            this.Controls.Add(this.specialisationSkillSummaryBox);
            this.Controls.Add(this.specialisationSkillSummaryLabel);
            this.Controls.Add(this.specialisationSkillDescriptionBox);
            this.Controls.Add(this.specialisationSkillDescriptionLabel);
            this.Controls.Add(this.specialisationSkillsBox);
            this.Controls.Add(this.specialisationSkillsLabel);
            this.Controls.Add(this.skillHasSpecialisationsBox);
            this.Controls.Add(this.skillDownButton);
            this.Controls.Add(this.skillUpButton);
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
        private System.Windows.Forms.Button skillUpButton;
        private System.Windows.Forms.Button skillDownButton;
        private System.Windows.Forms.CheckBox skillHasSpecialisationsBox;
        private System.Windows.Forms.ListBox specialisationSkillsBox;
        private System.Windows.Forms.Label specialisationSkillsLabel;
        private System.Windows.Forms.RichTextBox specialisationSkillRefereeBox;
        private System.Windows.Forms.Label specialisationSkillRefereeLabel;
        private System.Windows.Forms.RichTextBox specialisationSkillSummaryBox;
        private System.Windows.Forms.Label specialisationSkillSummaryLabel;
        private System.Windows.Forms.RichTextBox specialisationSkillDescriptionBox;
        private System.Windows.Forms.Label specialisationSkillDescriptionLabel;
        private System.Windows.Forms.TextBox specialisationSkillNameBox;
        private System.Windows.Forms.Label specialisationSkillNameLabel;
        private System.Windows.Forms.Button specialistSkillDownButton;
        private System.Windows.Forms.Button specialistSkillUpButton;
        private System.Windows.Forms.Button removeSpecialistSkillButton;
        private System.Windows.Forms.Button addSpecialistSkillButton;
    }
}

