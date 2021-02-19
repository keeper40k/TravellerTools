
namespace TravellerTools.CharGen
{
    partial class MusteringOutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusteringOutDialog));
            this.rollsRemainingLabel = new System.Windows.Forms.Label();
            this.benefitsTableButton = new System.Windows.Forms.Button();
            this.cashTableButton = new System.Windows.Forms.Button();
            this.characterDisplay = new System.Windows.Forms.RichTextBox();
            this.characterDisplayLabel = new System.Windows.Forms.Label();
            this.cashRollsLabel = new System.Windows.Forms.Label();
            this.benefitsRollLabel = new System.Windows.Forms.Label();
            this.bonusToBenefitsLabel = new System.Windows.Forms.Label();
            this.bonusToCashLabel = new System.Windows.Forms.Label();
            this.resultsBox = new System.Windows.Forms.RichTextBox();
            this.resultsLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rollsRemainingLabel
            // 
            this.rollsRemainingLabel.AutoSize = true;
            this.rollsRemainingLabel.Location = new System.Drawing.Point(13, 13);
            this.rollsRemainingLabel.Name = "rollsRemainingLabel";
            this.rollsRemainingLabel.Size = new System.Drawing.Size(174, 13);
            this.rollsRemainingLabel.TabIndex = 0;
            this.rollsRemainingLabel.Text = "Mustering Out Rolls Remaining ... X";
            // 
            // benefitsTableButton
            // 
            this.benefitsTableButton.Location = new System.Drawing.Point(180, 30);
            this.benefitsTableButton.Name = "benefitsTableButton";
            this.benefitsTableButton.Size = new System.Drawing.Size(130, 100);
            this.benefitsTableButton.TabIndex = 1;
            this.benefitsTableButton.Text = "Benefits Table";
            this.benefitsTableButton.UseVisualStyleBackColor = true;
            this.benefitsTableButton.Click += new System.EventHandler(this.benefitsTableButton_Click);
            // 
            // cashTableButton
            // 
            this.cashTableButton.Location = new System.Drawing.Point(316, 30);
            this.cashTableButton.Name = "cashTableButton";
            this.cashTableButton.Size = new System.Drawing.Size(130, 100);
            this.cashTableButton.TabIndex = 2;
            this.cashTableButton.Text = "Cash Table";
            this.cashTableButton.UseVisualStyleBackColor = true;
            this.cashTableButton.Click += new System.EventHandler(this.cashTableButton_Click);
            // 
            // characterDisplay
            // 
            this.characterDisplay.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.characterDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.characterDisplay.Location = new System.Drawing.Point(16, 161);
            this.characterDisplay.Name = "characterDisplay";
            this.characterDisplay.Size = new System.Drawing.Size(430, 116);
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
            // cashRollsLabel
            // 
            this.cashRollsLabel.AutoSize = true;
            this.cashRollsLabel.Location = new System.Drawing.Point(12, 62);
            this.cashRollsLabel.Name = "cashRollsLabel";
            this.cashRollsLabel.Size = new System.Drawing.Size(109, 13);
            this.cashRollsLabel.TabIndex = 29;
            this.cashRollsLabel.Text = "Z ... Cash Rolls Made";
            // 
            // benefitsRollLabel
            // 
            this.benefitsRollLabel.AutoSize = true;
            this.benefitsRollLabel.Location = new System.Drawing.Point(12, 40);
            this.benefitsRollLabel.Name = "benefitsRollLabel";
            this.benefitsRollLabel.Size = new System.Drawing.Size(123, 13);
            this.benefitsRollLabel.TabIndex = 30;
            this.benefitsRollLabel.Text = "Y ... Benefits Rolls Made";
            // 
            // bonusToBenefitsLabel
            // 
            this.bonusToBenefitsLabel.AutoSize = true;
            this.bonusToBenefitsLabel.Location = new System.Drawing.Point(13, 84);
            this.bonusToBenefitsLabel.Name = "bonusToBenefitsLabel";
            this.bonusToBenefitsLabel.Size = new System.Drawing.Size(105, 13);
            this.bonusToBenefitsLabel.TabIndex = 31;
            this.bonusToBenefitsLabel.Text = "+1 Bonus to Benefits";
            // 
            // bonusToCashLabel
            // 
            this.bonusToCashLabel.AutoSize = true;
            this.bonusToCashLabel.Location = new System.Drawing.Point(13, 107);
            this.bonusToCashLabel.Name = "bonusToCashLabel";
            this.bonusToCashLabel.Size = new System.Drawing.Size(91, 13);
            this.bonusToCashLabel.TabIndex = 32;
            this.bonusToCashLabel.Text = "+1 Bonus to Cash";
            // 
            // resultsBox
            // 
            this.resultsBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.resultsBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultsBox.Location = new System.Drawing.Point(16, 299);
            this.resultsBox.Name = "resultsBox";
            this.resultsBox.Size = new System.Drawing.Size(430, 116);
            this.resultsBox.TabIndex = 34;
            this.resultsBox.Text = "";
            // 
            // resultsLabel
            // 
            this.resultsLabel.AutoSize = true;
            this.resultsLabel.Location = new System.Drawing.Point(13, 283);
            this.resultsLabel.Name = "resultsLabel";
            this.resultsLabel.Size = new System.Drawing.Size(148, 13);
            this.resultsLabel.TabIndex = 33;
            this.resultsLabel.Text = "Musting Out Results So Far ...";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(371, 422);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 35;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // MusteringOutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 454);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.resultsBox);
            this.Controls.Add(this.resultsLabel);
            this.Controls.Add(this.bonusToCashLabel);
            this.Controls.Add(this.bonusToBenefitsLabel);
            this.Controls.Add(this.benefitsRollLabel);
            this.Controls.Add(this.cashRollsLabel);
            this.Controls.Add(this.characterDisplay);
            this.Controls.Add(this.characterDisplayLabel);
            this.Controls.Add(this.cashTableButton);
            this.Controls.Add(this.benefitsTableButton);
            this.Controls.Add(this.rollsRemainingLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MusteringOutDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mustering Out Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label rollsRemainingLabel;
        private System.Windows.Forms.Button benefitsTableButton;
        private System.Windows.Forms.Button cashTableButton;
        private System.Windows.Forms.RichTextBox characterDisplay;
        private System.Windows.Forms.Label characterDisplayLabel;
        private System.Windows.Forms.Label cashRollsLabel;
        private System.Windows.Forms.Label benefitsRollLabel;
        private System.Windows.Forms.Label bonusToBenefitsLabel;
        private System.Windows.Forms.Label bonusToCashLabel;
        private System.Windows.Forms.RichTextBox resultsBox;
        private System.Windows.Forms.Label resultsLabel;
        private System.Windows.Forms.Button closeButton;
    }
}