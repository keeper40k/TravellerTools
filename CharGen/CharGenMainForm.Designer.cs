
namespace TravellerTools.CharGen
{
    partial class CharGenMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharGenMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.characterDisplayLabel = new System.Windows.Forms.Label();
            this.autoCreate = new System.Windows.Forms.Button();
            this.autoCreateLabel = new System.Windows.Forms.Label();
            this.characterDisplay = new System.Windows.Forms.RichTextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.titleBox = new System.Windows.Forms.ComboBox();
            this.useTitleCheckBox = new System.Windows.Forms.CheckBox();
            this.useRankCheckBox = new System.Windows.Forms.CheckBox();
            this.rankLabel = new System.Windows.Forms.Label();
            this.rankBox = new System.Windows.Forms.TextBox();
            this.ageLabel = new System.Windows.Forms.Label();
            this.ageNumberBox = new System.Windows.Forms.NumericUpDown();
            this.enlistButton = new System.Windows.Forms.Button();
            this.enlistLabel = new System.Windows.Forms.Label();
            this.serviceBox = new System.Windows.Forms.TextBox();
            this.serviceLabel = new System.Windows.Forms.Label();
            this.termTitleLabel = new System.Windows.Forms.Label();
            this.termRollButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ageNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            this.fIleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            this.fIleToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fIleToolStripMenuItem.Text = "FIle";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // characterDisplayLabel
            // 
            this.characterDisplayLabel.AutoSize = true;
            this.characterDisplayLabel.Location = new System.Drawing.Point(404, 13);
            this.characterDisplayLabel.Name = "characterDisplayLabel";
            this.characterDisplayLabel.Size = new System.Drawing.Size(134, 13);
            this.characterDisplayLabel.TabIndex = 2;
            this.characterDisplayLabel.Text = "Current Traveller Character";
            // 
            // autoCreate
            // 
            this.autoCreate.Location = new System.Drawing.Point(326, 32);
            this.autoCreate.Name = "autoCreate";
            this.autoCreate.Size = new System.Drawing.Size(75, 23);
            this.autoCreate.TabIndex = 3;
            this.autoCreate.Text = "ROLL";
            this.autoCreate.UseVisualStyleBackColor = true;
            this.autoCreate.Click += new System.EventHandler(this.autoCreate_Click);
            // 
            // autoCreateLabel
            // 
            this.autoCreateLabel.AutoSize = true;
            this.autoCreateLabel.Location = new System.Drawing.Point(160, 37);
            this.autoCreateLabel.Name = "autoCreateLabel";
            this.autoCreateLabel.Size = new System.Drawing.Size(160, 13);
            this.autoCreateLabel.TabIndex = 4;
            this.autoCreateLabel.Text = "Re-Roll Traveller Character UPP";
            // 
            // characterDisplay
            // 
            this.characterDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.characterDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.characterDisplay.Location = new System.Drawing.Point(407, 29);
            this.characterDisplay.Name = "characterDisplay";
            this.characterDisplay.Size = new System.Drawing.Size(381, 409);
            this.characterDisplay.TabIndex = 5;
            this.characterDisplay.Text = "";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 112);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 6;
            this.nameLabel.Text = "Name";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(53, 109);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(348, 20);
            this.nameBox.TabIndex = 7;
            this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(12, 62);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(27, 13);
            this.titleLabel.TabIndex = 8;
            this.titleLabel.Text = "Title";
            // 
            // titleBox
            // 
            this.titleBox.Enabled = false;
            this.titleBox.FormattingEnabled = true;
            this.titleBox.Location = new System.Drawing.Point(53, 59);
            this.titleBox.Name = "titleBox";
            this.titleBox.Size = new System.Drawing.Size(291, 21);
            this.titleBox.TabIndex = 9;
            this.titleBox.SelectedIndexChanged += new System.EventHandler(this.titleBox_SelectedIndexChanged);
            this.titleBox.TextChanged += new System.EventHandler(this.titleBox_TextChanged);
            // 
            // useTitleCheckBox
            // 
            this.useTitleCheckBox.AutoSize = true;
            this.useTitleCheckBox.Location = new System.Drawing.Point(350, 61);
            this.useTitleCheckBox.Name = "useTitleCheckBox";
            this.useTitleCheckBox.Size = new System.Drawing.Size(51, 17);
            this.useTitleCheckBox.TabIndex = 10;
            this.useTitleCheckBox.Text = "Use?";
            this.useTitleCheckBox.UseVisualStyleBackColor = true;
            this.useTitleCheckBox.CheckedChanged += new System.EventHandler(this.useTitleCheckBox_CheckedChanged);
            // 
            // useRankCheckBox
            // 
            this.useRankCheckBox.AutoSize = true;
            this.useRankCheckBox.Location = new System.Drawing.Point(350, 87);
            this.useRankCheckBox.Name = "useRankCheckBox";
            this.useRankCheckBox.Size = new System.Drawing.Size(51, 17);
            this.useRankCheckBox.TabIndex = 13;
            this.useRankCheckBox.Text = "Use?";
            this.useRankCheckBox.UseVisualStyleBackColor = true;
            this.useRankCheckBox.CheckedChanged += new System.EventHandler(this.useRankCheckBox_CheckedChanged);
            // 
            // rankLabel
            // 
            this.rankLabel.AutoSize = true;
            this.rankLabel.Location = new System.Drawing.Point(14, 88);
            this.rankLabel.Name = "rankLabel";
            this.rankLabel.Size = new System.Drawing.Size(33, 13);
            this.rankLabel.TabIndex = 11;
            this.rankLabel.Text = "Rank";
            // 
            // rankBox
            // 
            this.rankBox.Enabled = false;
            this.rankBox.Location = new System.Drawing.Point(53, 85);
            this.rankBox.Name = "rankBox";
            this.rankBox.Size = new System.Drawing.Size(291, 20);
            this.rankBox.TabIndex = 14;
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Location = new System.Drawing.Point(14, 137);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(26, 13);
            this.ageLabel.TabIndex = 15;
            this.ageLabel.Text = "Age";
            // 
            // ageNumberBox
            // 
            this.ageNumberBox.Enabled = false;
            this.ageNumberBox.Location = new System.Drawing.Point(53, 135);
            this.ageNumberBox.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.ageNumberBox.Name = "ageNumberBox";
            this.ageNumberBox.Size = new System.Drawing.Size(348, 20);
            this.ageNumberBox.TabIndex = 16;
            this.ageNumberBox.ValueChanged += new System.EventHandler(this.ageNumberBox_ValueChanged);
            // 
            // enlistButton
            // 
            this.enlistButton.Location = new System.Drawing.Point(326, 172);
            this.enlistButton.Name = "enlistButton";
            this.enlistButton.Size = new System.Drawing.Size(75, 23);
            this.enlistButton.TabIndex = 17;
            this.enlistButton.Text = "ENLIST";
            this.enlistButton.UseVisualStyleBackColor = true;
            this.enlistButton.Click += new System.EventHandler(this.enlistButton_Click);
            // 
            // enlistLabel
            // 
            this.enlistLabel.AutoSize = true;
            this.enlistLabel.Location = new System.Drawing.Point(229, 177);
            this.enlistLabel.Name = "enlistLabel";
            this.enlistLabel.Size = new System.Drawing.Size(91, 13);
            this.enlistLabel.TabIndex = 18;
            this.enlistLabel.Text = "Enlist in a Service";
            // 
            // serviceBox
            // 
            this.serviceBox.Enabled = false;
            this.serviceBox.Location = new System.Drawing.Point(53, 201);
            this.serviceBox.Name = "serviceBox";
            this.serviceBox.Size = new System.Drawing.Size(348, 20);
            this.serviceBox.TabIndex = 20;
            this.serviceBox.TextChanged += new System.EventHandler(this.serviceBox_TextChanged);
            // 
            // serviceLabel
            // 
            this.serviceLabel.AutoSize = true;
            this.serviceLabel.Location = new System.Drawing.Point(12, 204);
            this.serviceLabel.Name = "serviceLabel";
            this.serviceLabel.Size = new System.Drawing.Size(43, 13);
            this.serviceLabel.TabIndex = 19;
            this.serviceLabel.Text = "Service";
            // 
            // termTitleLabel
            // 
            this.termTitleLabel.AutoSize = true;
            this.termTitleLabel.Location = new System.Drawing.Point(14, 263);
            this.termTitleLabel.Name = "termTitleLabel";
            this.termTitleLabel.Size = new System.Drawing.Size(108, 13);
            this.termTitleLabel.TabIndex = 21;
            this.termTitleLabel.Text = "Processing Term X ...";
            // 
            // termRollButton
            // 
            this.termRollButton.Location = new System.Drawing.Point(326, 258);
            this.termRollButton.Name = "termRollButton";
            this.termRollButton.Size = new System.Drawing.Size(75, 23);
            this.termRollButton.TabIndex = 22;
            this.termRollButton.Text = "ROLL";
            this.termRollButton.UseVisualStyleBackColor = true;
            this.termRollButton.Click += new System.EventHandler(this.termRollButton_Click);
            // 
            // CharGenMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.termRollButton);
            this.Controls.Add(this.termTitleLabel);
            this.Controls.Add(this.serviceBox);
            this.Controls.Add(this.serviceLabel);
            this.Controls.Add(this.enlistLabel);
            this.Controls.Add(this.enlistButton);
            this.Controls.Add(this.ageNumberBox);
            this.Controls.Add(this.ageLabel);
            this.Controls.Add(this.rankBox);
            this.Controls.Add(this.useRankCheckBox);
            this.Controls.Add(this.rankLabel);
            this.Controls.Add(this.useTitleCheckBox);
            this.Controls.Add(this.titleBox);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.characterDisplay);
            this.Controls.Add(this.autoCreateLabel);
            this.Controls.Add(this.autoCreate);
            this.Controls.Add(this.characterDisplayLabel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CharGenMainForm";
            this.Text = "Classic Traveller Character Generator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ageNumberBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label characterDisplayLabel;
        private System.Windows.Forms.Button autoCreate;
        private System.Windows.Forms.Label autoCreateLabel;
        private System.Windows.Forms.RichTextBox characterDisplay;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.ComboBox titleBox;
        private System.Windows.Forms.CheckBox useTitleCheckBox;
        private System.Windows.Forms.CheckBox useRankCheckBox;
        private System.Windows.Forms.Label rankLabel;
        private System.Windows.Forms.TextBox rankBox;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.NumericUpDown ageNumberBox;
        private System.Windows.Forms.Button enlistButton;
        private System.Windows.Forms.Label enlistLabel;
        private System.Windows.Forms.TextBox serviceBox;
        private System.Windows.Forms.Label serviceLabel;
        private System.Windows.Forms.Label termTitleLabel;
        private System.Windows.Forms.Button termRollButton;
    }
}

