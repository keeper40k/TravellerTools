﻿
namespace TravellerTools.CharGen
{
    partial class GeneralSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralSettings));
            this.closeButton = new System.Windows.Forms.Button();
            this.promptOnNewCheckBox = new System.Windows.Forms.CheckBox();
            this.allowAgeEditingBox = new System.Windows.Forms.CheckBox();
            this.allowCharacterSurvivalBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(181, 51);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // promptOnNewCheckBox
            // 
            this.promptOnNewCheckBox.AutoSize = true;
            this.promptOnNewCheckBox.Location = new System.Drawing.Point(12, 12);
            this.promptOnNewCheckBox.Name = "promptOnNewCheckBox";
            this.promptOnNewCheckBox.Size = new System.Drawing.Size(148, 17);
            this.promptOnNewCheckBox.TabIndex = 2;
            this.promptOnNewCheckBox.Text = "Prompt on New Character";
            this.promptOnNewCheckBox.UseVisualStyleBackColor = true;
            this.promptOnNewCheckBox.CheckedChanged += new System.EventHandler(this.promptOnNewCheckBox_CheckedChanged);
            // 
            // allowAgeEditingBox
            // 
            this.allowAgeEditingBox.AutoSize = true;
            this.allowAgeEditingBox.Location = new System.Drawing.Point(12, 35);
            this.allowAgeEditingBox.Name = "allowAgeEditingBox";
            this.allowAgeEditingBox.Size = new System.Drawing.Size(108, 17);
            this.allowAgeEditingBox.TabIndex = 4;
            this.allowAgeEditingBox.Text = "Allow Age Editing";
            this.allowAgeEditingBox.UseVisualStyleBackColor = true;
            this.allowAgeEditingBox.CheckedChanged += new System.EventHandler(this.allowAgeEditingBox_CheckedChanged);
            // 
            // allowCharacterSurvivalBox
            // 
            this.allowCharacterSurvivalBox.AutoSize = true;
            this.allowCharacterSurvivalBox.Location = new System.Drawing.Point(12, 58);
            this.allowCharacterSurvivalBox.Name = "allowCharacterSurvivalBox";
            this.allowCharacterSurvivalBox.Size = new System.Drawing.Size(141, 17);
            this.allowCharacterSurvivalBox.TabIndex = 5;
            this.allowCharacterSurvivalBox.Text = "Allow Character Survival";
            this.allowCharacterSurvivalBox.UseVisualStyleBackColor = true;
            this.allowCharacterSurvivalBox.CheckedChanged += new System.EventHandler(this.allowCharacterSurvival_CheckedChanged);
            // 
            // GeneralSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 86);
            this.Controls.Add(this.allowCharacterSurvivalBox);
            this.Controls.Add(this.allowAgeEditingBox);
            this.Controls.Add(this.promptOnNewCheckBox);
            this.Controls.Add(this.closeButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GeneralSettings";
            this.Text = "General Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.CheckBox promptOnNewCheckBox;
        private System.Windows.Forms.CheckBox allowAgeEditingBox;
        private System.Windows.Forms.CheckBox allowCharacterSurvivalBox;
    }
}