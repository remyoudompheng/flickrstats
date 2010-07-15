namespace FlickrStats
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonStats = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ButtonStream = new System.Windows.Forms.Button();
            this.ButtonSets = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonStats
            // 
            this.ButtonStats.AutoSize = true;
            this.ButtonStats.Location = new System.Drawing.Point(22, 173);
            this.ButtonStats.Name = "ButtonStats";
            this.ButtonStats.Size = new System.Drawing.Size(111, 27);
            this.ButtonStats.TabIndex = 0;
            this.ButtonStats.Text = "Statistiques";
            this.ButtonStats.UseVisualStyleBackColor = true;
            this.ButtonStats.Click += new System.EventHandler(this.ButtonStats_Click);
            // 
            // label
            // 
            this.label.BackColor = System.Drawing.SystemColors.Control;
            this.label.Location = new System.Drawing.Point(12, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(368, 161);
            this.label.TabIndex = 1;
            this.label.Text = "label1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 234);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(392, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 206);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(368, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // ButtonStream
            // 
            this.ButtonStream.Location = new System.Drawing.Point(141, 173);
            this.ButtonStream.Name = "ButtonStream";
            this.ButtonStream.Size = new System.Drawing.Size(111, 25);
            this.ButtonStream.TabIndex = 4;
            this.ButtonStream.Text = "Stats flux";
            this.ButtonStream.UseVisualStyleBackColor = true;
            this.ButtonStream.Click += new System.EventHandler(this.ButtonStream_Click);
            // 
            // ButtonSets
            // 
            this.ButtonSets.Location = new System.Drawing.Point(260, 173);
            this.ButtonSets.Name = "ButtonSets";
            this.ButtonSets.Size = new System.Drawing.Size(111, 25);
            this.ButtonSets.TabIndex = 5;
            this.ButtonSets.Text = "Stats albums";
            this.ButtonSets.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 256);
            this.Controls.Add(this.ButtonSets);
            this.Controls.Add(this.ButtonStream);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label);
            this.Controls.Add(this.ButtonStats);
            this.Name = "MainForm";
            this.Text = "Flickr Stats";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonStats;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Button ButtonStream;
        private System.Windows.Forms.Button ButtonSets;
    }
}

