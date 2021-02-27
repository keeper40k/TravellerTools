
namespace TravellerTools.TravellerCombatTool
{
    partial class TravellerCombatTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TravellerCombatTool));
            this.heightMapPictureBox = new System.Windows.Forms.PictureBox();
            this.heightMapLabel = new System.Windows.Forms.Label();
            this.selectHeightMapButton = new System.Windows.Forms.Button();
            this.open3DButton = new System.Windows.Forms.Button();
            this.close3DButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.heightMapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // heightMapPictureBox
            // 
            this.heightMapPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.heightMapPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.heightMapPictureBox.Location = new System.Drawing.Point(527, 42);
            this.heightMapPictureBox.Name = "heightMapPictureBox";
            this.heightMapPictureBox.Size = new System.Drawing.Size(261, 349);
            this.heightMapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.heightMapPictureBox.TabIndex = 0;
            this.heightMapPictureBox.TabStop = false;
            // 
            // heightMapLabel
            // 
            this.heightMapLabel.AutoSize = true;
            this.heightMapLabel.Location = new System.Drawing.Point(524, 18);
            this.heightMapLabel.Name = "heightMapLabel";
            this.heightMapLabel.Size = new System.Drawing.Size(65, 13);
            this.heightMapLabel.TabIndex = 1;
            this.heightMapLabel.Text = "Height Map:";
            // 
            // selectHeightMapButton
            // 
            this.selectHeightMapButton.Location = new System.Drawing.Point(713, 13);
            this.selectHeightMapButton.Name = "selectHeightMapButton";
            this.selectHeightMapButton.Size = new System.Drawing.Size(75, 23);
            this.selectHeightMapButton.TabIndex = 2;
            this.selectHeightMapButton.Text = "Select";
            this.selectHeightMapButton.UseVisualStyleBackColor = true;
            this.selectHeightMapButton.Click += new System.EventHandler(this.selectHeightMapButton_Click);
            // 
            // open3DButton
            // 
            this.open3DButton.Location = new System.Drawing.Point(433, 415);
            this.open3DButton.Name = "open3DButton";
            this.open3DButton.Size = new System.Drawing.Size(75, 23);
            this.open3DButton.TabIndex = 3;
            this.open3DButton.Text = "Open 3D";
            this.open3DButton.UseVisualStyleBackColor = true;
            this.open3DButton.Click += new System.EventHandler(this.open3DButton_Click);
            // 
            // close3DButton
            // 
            this.close3DButton.Location = new System.Drawing.Point(527, 415);
            this.close3DButton.Name = "close3DButton";
            this.close3DButton.Size = new System.Drawing.Size(75, 23);
            this.close3DButton.TabIndex = 4;
            this.close3DButton.Text = "Close 3D";
            this.close3DButton.UseVisualStyleBackColor = true;
            this.close3DButton.Click += new System.EventHandler(this.close3DButton_Click);
            // 
            // TravellerCombatTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.close3DButton);
            this.Controls.Add(this.open3DButton);
            this.Controls.Add(this.selectHeightMapButton);
            this.Controls.Add(this.heightMapLabel);
            this.Controls.Add(this.heightMapPictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TravellerCombatTool";
            this.Text = "Traveller Combat Tool";
            ((System.ComponentModel.ISupportInitialize)(this.heightMapPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox heightMapPictureBox;
        private System.Windows.Forms.Label heightMapLabel;
        private System.Windows.Forms.Button selectHeightMapButton;
        private System.Windows.Forms.Button open3DButton;
        private System.Windows.Forms.Button close3DButton;
    }
}

