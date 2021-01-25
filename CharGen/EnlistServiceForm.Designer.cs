
namespace TravellerTools.CharGen
{
    partial class EnlistServiceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnlistServiceForm));
            this.okButton = new System.Windows.Forms.Button();
            this.characterDisplay = new System.Windows.Forms.RichTextBox();
            this.characterDisplayLabel = new System.Windows.Forms.Label();
            this.recommendationsBox = new System.Windows.Forms.RichTextBox();
            this.recommendationsLabel = new System.Windows.Forms.Label();
            this.enlistmentChoiceLabel = new System.Windows.Forms.Label();
            this.enlistmentChoiceBox = new System.Windows.Forms.ComboBox();
            this.enlistTargetLabel = new System.Windows.Forms.Label();
            this.enlistButton = new System.Windows.Forms.Button();
            this.enlistmentResultLabel = new System.Windows.Forms.Label();
            this.draftResultLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(87, 177);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // characterDisplay
            // 
            this.characterDisplay.Enabled = false;
            this.characterDisplay.Location = new System.Drawing.Point(9, 32);
            this.characterDisplay.Name = "characterDisplay";
            this.characterDisplay.Size = new System.Drawing.Size(384, 32);
            this.characterDisplay.TabIndex = 7;
            this.characterDisplay.Text = "";
            // 
            // characterDisplayLabel
            // 
            this.characterDisplayLabel.AutoSize = true;
            this.characterDisplayLabel.Location = new System.Drawing.Point(7, 16);
            this.characterDisplayLabel.Name = "characterDisplayLabel";
            this.characterDisplayLabel.Size = new System.Drawing.Size(134, 13);
            this.characterDisplayLabel.TabIndex = 6;
            this.characterDisplayLabel.Text = "Current Traveller Character";
            // 
            // recommendationsBox
            // 
            this.recommendationsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recommendationsBox.BackColor = System.Drawing.SystemColors.Control;
            this.recommendationsBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.recommendationsBox.Location = new System.Drawing.Point(407, 12);
            this.recommendationsBox.Name = "recommendationsBox";
            this.recommendationsBox.ReadOnly = true;
            this.recommendationsBox.Size = new System.Drawing.Size(151, 181);
            this.recommendationsBox.TabIndex = 9;
            this.recommendationsBox.Text = "";
            // 
            // recommendationsLabel
            // 
            this.recommendationsLabel.AutoSize = true;
            this.recommendationsLabel.Location = new System.Drawing.Point(404, 78);
            this.recommendationsLabel.Name = "recommendationsLabel";
            this.recommendationsLabel.Size = new System.Drawing.Size(134, 13);
            this.recommendationsLabel.TabIndex = 8;
            this.recommendationsLabel.Text = "Service Recommendations";
            // 
            // enlistmentChoiceLabel
            // 
            this.enlistmentChoiceLabel.AutoSize = true;
            this.enlistmentChoiceLabel.Location = new System.Drawing.Point(6, 78);
            this.enlistmentChoiceLabel.Name = "enlistmentChoiceLabel";
            this.enlistmentChoiceLabel.Size = new System.Drawing.Size(192, 13);
            this.enlistmentChoiceLabel.TabIndex = 11;
            this.enlistmentChoiceLabel.Text = "Choose a Service to attempt Enlistment";
            // 
            // enlistmentChoiceBox
            // 
            this.enlistmentChoiceBox.FormattingEnabled = true;
            this.enlistmentChoiceBox.Location = new System.Drawing.Point(205, 75);
            this.enlistmentChoiceBox.Name = "enlistmentChoiceBox";
            this.enlistmentChoiceBox.Size = new System.Drawing.Size(187, 21);
            this.enlistmentChoiceBox.TabIndex = 13;
            this.enlistmentChoiceBox.SelectedIndexChanged += new System.EventHandler(this.enlistmentChoiceBox_SelectedIndexChanged);
            // 
            // enlistTargetLabel
            // 
            this.enlistTargetLabel.AutoSize = true;
            this.enlistTargetLabel.Location = new System.Drawing.Point(39, 107);
            this.enlistTargetLabel.Name = "enlistTargetLabel";
            this.enlistTargetLabel.Size = new System.Drawing.Size(272, 13);
            this.enlistTargetLabel.TabIndex = 14;
            this.enlistTargetLabel.Text = "You need X+ on 2d6 to Enlist. You will get a +Y modifier.";
            // 
            // enlistButton
            // 
            this.enlistButton.Enabled = false;
            this.enlistButton.Location = new System.Drawing.Point(317, 102);
            this.enlistButton.Name = "enlistButton";
            this.enlistButton.Size = new System.Drawing.Size(75, 23);
            this.enlistButton.TabIndex = 15;
            this.enlistButton.Text = "ENLIST";
            this.enlistButton.UseVisualStyleBackColor = true;
            this.enlistButton.Click += new System.EventHandler(this.enlistButton_Click);
            // 
            // enlistmentResultLabel
            // 
            this.enlistmentResultLabel.AutoSize = true;
            this.enlistmentResultLabel.Location = new System.Drawing.Point(7, 139);
            this.enlistmentResultLabel.Name = "enlistmentResultLabel";
            this.enlistmentResultLabel.Size = new System.Drawing.Size(100, 13);
            this.enlistmentResultLabel.TabIndex = 16;
            this.enlistmentResultLabel.Text = "Enlistment Result ...";
            // 
            // draftResultLabel
            // 
            this.draftResultLabel.AutoSize = true;
            this.draftResultLabel.Location = new System.Drawing.Point(7, 156);
            this.draftResultLabel.Name = "draftResultLabel";
            this.draftResultLabel.Size = new System.Drawing.Size(75, 13);
            this.draftResultLabel.TabIndex = 17;
            this.draftResultLabel.Text = "Draft Result ...";
            // 
            // EnlistServiceForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 205);
            this.Controls.Add(this.draftResultLabel);
            this.Controls.Add(this.enlistmentResultLabel);
            this.Controls.Add(this.enlistButton);
            this.Controls.Add(this.enlistTargetLabel);
            this.Controls.Add(this.enlistmentChoiceBox);
            this.Controls.Add(this.enlistmentChoiceLabel);
            this.Controls.Add(this.recommendationsBox);
            this.Controls.Add(this.recommendationsLabel);
            this.Controls.Add(this.characterDisplay);
            this.Controls.Add(this.characterDisplayLabel);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnlistServiceForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enlist In a Service";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.RichTextBox characterDisplay;
        private System.Windows.Forms.Label characterDisplayLabel;
        private System.Windows.Forms.RichTextBox recommendationsBox;
        private System.Windows.Forms.Label recommendationsLabel;
        private System.Windows.Forms.Label enlistmentChoiceLabel;
        private System.Windows.Forms.ComboBox enlistmentChoiceBox;
        private System.Windows.Forms.Label enlistTargetLabel;
        private System.Windows.Forms.Button enlistButton;
        private System.Windows.Forms.Label enlistmentResultLabel;
        private System.Windows.Forms.Label draftResultLabel;
    }
}