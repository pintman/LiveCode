namespace LiveCodeKonsole
{
    partial class HauptfensterProxy
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HauptfensterProxy));
            this.tbQuelltext = new System.Windows.Forms.TextBox();
            this.tbFehler = new System.Windows.Forms.TextBox();
            this.tbAusgabe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbQuelltext
            // 
            this.tbQuelltext.AcceptsTab = true;
            this.tbQuelltext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbQuelltext.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbQuelltext.Location = new System.Drawing.Point(287, 29);
            this.tbQuelltext.Multiline = true;
            this.tbQuelltext.Name = "tbQuelltext";
            this.tbQuelltext.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbQuelltext.Size = new System.Drawing.Size(436, 378);
            this.tbQuelltext.TabIndex = 0;
            this.tbQuelltext.TabStop = false;
            this.tbQuelltext.Text = resources.GetString("tbQuelltext.Text");
            // 
            // tbFehler
            // 
            this.tbFehler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbFehler.Location = new System.Drawing.Point(13, 354);
            this.tbFehler.Multiline = true;
            this.tbFehler.Name = "tbFehler";
            this.tbFehler.ReadOnly = true;
            this.tbFehler.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbFehler.Size = new System.Drawing.Size(268, 53);
            this.tbFehler.TabIndex = 2;
            // 
            // tbAusgabe
            // 
            this.tbAusgabe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbAusgabe.Location = new System.Drawing.Point(13, 29);
            this.tbAusgabe.Multiline = true;
            this.tbAusgabe.Name = "tbAusgabe";
            this.tbAusgabe.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbAusgabe.Size = new System.Drawing.Size(268, 319);
            this.tbAusgabe.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ausgabe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Quelltext";
            // 
            // HauptfensterProxy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 419);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbAusgabe);
            this.Controls.Add(this.tbFehler);
            this.Controls.Add(this.tbQuelltext);
            this.Name = "HauptfensterProxy";
            this.Text = "LiveCode Konsole";
            this.Shown += new System.EventHandler(this.HauptfensterProxy_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbQuelltext;
        private System.Windows.Forms.TextBox tbFehler;
        private System.Windows.Forms.TextBox tbAusgabe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

