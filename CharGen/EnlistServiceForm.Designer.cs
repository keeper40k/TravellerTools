
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.characterDisplay = new System.Windows.Forms.RichTextBox();
            this.characterDisplayLabel = new System.Windows.Forms.Label();
            this.serviceRecommendationsBox = new System.Windows.Forms.RichTextBox();
            this.serviceRecommendationsLabel = new System.Windows.Forms.Label();
            this.enlistmentChoiceLabel = new System.Windows.Forms.Label();
            this.enlistmentChoiceBox = new System.Windows.Forms.ComboBox();
            this.enlistTargetLabel = new System.Windows.Forms.Label();
            this.enlistButton = new System.Windows.Forms.Button();
            this.enlistmentResultLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(713, 415);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(632, 415);
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
            this.characterDisplay.Location = new System.Drawing.Point(407, 25);
            this.characterDisplay.Name = "characterDisplay";
            this.characterDisplay.Size = new System.Drawing.Size(381, 49);
            this.characterDisplay.TabIndex = 7;
            this.characterDisplay.Text = "";
            // 
            // characterDisplayLabel
            // 
            this.characterDisplayLabel.AutoSize = true;
            this.characterDisplayLabel.Location = new System.Drawing.Point(404, 9);
            this.characterDisplayLabel.Name = "characterDisplayLabel";
            this.characterDisplayLabel.Size = new System.Drawing.Size(134, 13);
            this.characterDisplayLabel.TabIndex = 6;
            this.characterDisplayLabel.Text = "Current Traveller Character";
            // 
            // serviceRecommendationsBox
            // 
            this.serviceRecommendationsBox.Location = new System.Drawing.Point(407, 94);
            this.serviceRecommendationsBox.Name = "serviceRecommendationsBox";
            this.serviceRecommendationsBox.Size = new System.Drawing.Size(381, 315);
            this.serviceRecommendationsBox.TabIndex = 9;
            this.serviceRecommendationsBox.Text = "";
            // 
            // serviceRecommendationsLabel
            // 
            this.serviceRecommendationsLabel.AutoSize = true;
            this.serviceRecommendationsLabel.Location = new System.Drawing.Point(404, 78);
            this.serviceRecommendationsLabel.Name = "serviceRecommendationsLabel";
            this.serviceRecommendationsLabel.Size = new System.Drawing.Size(134, 13);
            this.serviceRecommendationsLabel.TabIndex = 8;
            this.serviceRecommendationsLabel.Text = "Service Recommendations";
            // 
            // enlistmentChoiceLabel
            // 
            this.enlistmentChoiceLabel.AutoSize = true;
            this.enlistmentChoiceLabel.Location = new System.Drawing.Point(12, 16);
            this.enlistmentChoiceLabel.Name = "enlistmentChoiceLabel";
            this.enlistmentChoiceLabel.Size = new System.Drawing.Size(192, 13);
            this.enlistmentChoiceLabel.TabIndex = 11;
            this.enlistmentChoiceLabel.Text = "Choose a Service to attempt Enlistment";
            // 
            // enlistmentChoiceBox
            // 
            this.enlistmentChoiceBox.FormattingEnabled = true;
            this.enlistmentChoiceBox.Location = new System.Drawing.Point(211, 13);
            this.enlistmentChoiceBox.Name = "enlistmentChoiceBox";
            this.enlistmentChoiceBox.Size = new System.Drawing.Size(187, 21);
            this.enlistmentChoiceBox.TabIndex = 13;
            this.enlistmentChoiceBox.SelectedIndexChanged += new System.EventHandler(this.enlistmentChoiceBox_SelectedIndexChanged);
            // 
            // enlistTargetLabel
            // 
            this.enlistTargetLabel.AutoSize = true;
            this.enlistTargetLabel.Location = new System.Drawing.Point(45, 45);
            this.enlistTargetLabel.Name = "enlistTargetLabel";
            this.enlistTargetLabel.Size = new System.Drawing.Size(272, 13);
            this.enlistTargetLabel.TabIndex = 14;
            this.enlistTargetLabel.Text = "You need X+ on 2d6 to Enlist. You will get a +Y modifier.";
            // 
            // enlistButton
            // 
            this.enlistButton.Enabled = false;
            this.enlistButton.Location = new System.Drawing.Point(323, 40);
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
            this.enlistmentResultLabel.Location = new System.Drawing.Point(13, 77);
            this.enlistmentResultLabel.Name = "enlistmentResultLabel";
            this.enlistmentResultLabel.Size = new System.Drawing.Size(100, 13);
            this.enlistmentResultLabel.TabIndex = 16;
            this.enlistmentResultLabel.Text = "Enlistment Result ...";
            // 
            // EnlistServiceForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.enlistmentResultLabel);
            this.Controls.Add(this.enlistButton);
            this.Controls.Add(this.enlistTargetLabel);
            this.Controls.Add(this.enlistmentChoiceBox);
            this.Controls.Add(this.enlistmentChoiceLabel);
            this.Controls.Add(this.serviceRecommendationsBox);
            this.Controls.Add(this.serviceRecommendationsLabel);
            this.Controls.Add(this.characterDisplay);
            this.Controls.Add(this.characterDisplayLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnlistServiceForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enlist In a Service";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.RichTextBox characterDisplay;
        private System.Windows.Forms.Label characterDisplayLabel;
        private System.Windows.Forms.RichTextBox serviceRecommendationsBox;
        private System.Windows.Forms.Label serviceRecommendationsLabel;
        private System.Windows.Forms.Label enlistmentChoiceLabel;
        private System.Windows.Forms.ComboBox enlistmentChoiceBox;
        private System.Windows.Forms.Label enlistTargetLabel;
        private System.Windows.Forms.Button enlistButton;
        private System.Windows.Forms.Label enlistmentResultLabel;
    }
}