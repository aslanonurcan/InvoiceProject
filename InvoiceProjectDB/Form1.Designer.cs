namespace InvoiceProjectDB
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.supportTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.countyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ınvoiceTransactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ınvoiceHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ınvoiceDetailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supportTablesToolStripMenuItem,
            this.ınvoiceTransactionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(536, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // supportTablesToolStripMenuItem
            // 
            this.supportTablesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customerToolStripMenuItem,
            this.productToolStripMenuItem,
            this.unitToolStripMenuItem,
            this.cityToolStripMenuItem,
            this.countyToolStripMenuItem});
            this.supportTablesToolStripMenuItem.Name = "supportTablesToolStripMenuItem";
            this.supportTablesToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.supportTablesToolStripMenuItem.Text = "Support Tables";
            // 
            // customerToolStripMenuItem
            // 
            this.customerToolStripMenuItem.Name = "customerToolStripMenuItem";
            this.customerToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.customerToolStripMenuItem.Text = "Customer";
            this.customerToolStripMenuItem.Click += new System.EventHandler(this.customerToolStripMenuItem_Click);
            // 
            // productToolStripMenuItem
            // 
            this.productToolStripMenuItem.Name = "productToolStripMenuItem";
            this.productToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.productToolStripMenuItem.Text = "Product";
            this.productToolStripMenuItem.Click += new System.EventHandler(this.productToolStripMenuItem_Click);
            // 
            // unitToolStripMenuItem
            // 
            this.unitToolStripMenuItem.Name = "unitToolStripMenuItem";
            this.unitToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.unitToolStripMenuItem.Text = "Unit";
            this.unitToolStripMenuItem.Click += new System.EventHandler(this.unitToolStripMenuItem_Click);
            // 
            // cityToolStripMenuItem
            // 
            this.cityToolStripMenuItem.Name = "cityToolStripMenuItem";
            this.cityToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.cityToolStripMenuItem.Text = "City";
            this.cityToolStripMenuItem.Click += new System.EventHandler(this.cityToolStripMenuItem_Click);
            // 
            // countyToolStripMenuItem
            // 
            this.countyToolStripMenuItem.Name = "countyToolStripMenuItem";
            this.countyToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.countyToolStripMenuItem.Text = "County";
            this.countyToolStripMenuItem.Click += new System.EventHandler(this.countyToolStripMenuItem_Click);
            // 
            // ınvoiceTransactionsToolStripMenuItem
            // 
            this.ınvoiceTransactionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ınvoiceHeaderToolStripMenuItem,
            this.ınvoiceDetailToolStripMenuItem});
            this.ınvoiceTransactionsToolStripMenuItem.Name = "ınvoiceTransactionsToolStripMenuItem";
            this.ınvoiceTransactionsToolStripMenuItem.Size = new System.Drawing.Size(127, 20);
            this.ınvoiceTransactionsToolStripMenuItem.Text = "Invoice Transactions";
            // 
            // ınvoiceHeaderToolStripMenuItem
            // 
            this.ınvoiceHeaderToolStripMenuItem.Name = "ınvoiceHeaderToolStripMenuItem";
            this.ınvoiceHeaderToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.ınvoiceHeaderToolStripMenuItem.Text = "New Invoiece Transaction";
            this.ınvoiceHeaderToolStripMenuItem.Click += new System.EventHandler(this.ınvoiceHeaderToolStripMenuItem_Click);
            // 
            // ınvoiceDetailToolStripMenuItem
            // 
            this.ınvoiceDetailToolStripMenuItem.Name = "ınvoiceDetailToolStripMenuItem";
            this.ınvoiceDetailToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.ınvoiceDetailToolStripMenuItem.Text = "View Edit Search";
            this.ınvoiceDetailToolStripMenuItem.Click += new System.EventHandler(this.ınvoiceDetailToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 276);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem supportTablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem countyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ınvoiceTransactionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ınvoiceHeaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ınvoiceDetailToolStripMenuItem;
    }
}

