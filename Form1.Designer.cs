namespace ChessGameWithThemes
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnStart = new System.Windows.Forms.Button();
            this.lblBoard = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lbltheme = new System.Windows.Forms.Label();
            this.cmbCharacter = new System.Windows.Forms.ComboBox();
            this.TwoPlayerMode = new System.Windows.Forms.Label();
            this.cmbBoard = new System.Windows.Forms.ComboBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnStart.Font = new System.Drawing.Font("Mistral", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnStart.Location = new System.Drawing.Point(293, 410);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(207, 43);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "BAŞLA";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblBoard
            // 
            this.lblBoard.AutoSize = true;
            this.lblBoard.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblBoard.Font = new System.Drawing.Font("Mistral", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBoard.Location = new System.Drawing.Point(297, 246);
            this.lblBoard.Name = "lblBoard";
            this.lblBoard.Size = new System.Drawing.Size(190, 23);
            this.lblBoard.TabIndex = 2;
            this.lblBoard.Text = "         TAHTA SEÇİMİ         ";
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonExit.Font = new System.Drawing.Font("Mistral", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonExit.Location = new System.Drawing.Point(603, 410);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(212, 43);
            this.buttonExit.TabIndex = 9;
            this.buttonExit.Text = "ÇIKIŞ";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTitle.Font = new System.Drawing.Font("Mistral", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTitle.Location = new System.Drawing.Point(365, 61);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(375, 34);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "       KARAKTERLİ SATRANÇ OYUNU     ";
            // 
            // lbltheme
            // 
            this.lbltheme.AutoSize = true;
            this.lbltheme.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbltheme.Font = new System.Drawing.Font("Mistral", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbltheme.Location = new System.Drawing.Point(297, 182);
            this.lbltheme.Name = "lbltheme";
            this.lbltheme.Size = new System.Drawing.Size(193, 23);
            this.lbltheme.TabIndex = 1;
            this.lbltheme.Text = "        KARAKTER SEÇİMİ     ";
            // 
            // cmbCharacter
            // 
            this.cmbCharacter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbCharacter.FormattingEnabled = true;
            this.cmbCharacter.Location = new System.Drawing.Point(574, 181);
            this.cmbCharacter.Name = "cmbCharacter";
            this.cmbCharacter.Size = new System.Drawing.Size(301, 24);
            this.cmbCharacter.TabIndex = 4;
            // 
            // TwoPlayerMode
            // 
            this.TwoPlayerMode.AutoSize = true;
            this.TwoPlayerMode.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TwoPlayerMode.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TwoPlayerMode.Location = new System.Drawing.Point(579, 311);
            this.TwoPlayerMode.Name = "TwoPlayerMode";
            this.TwoPlayerMode.Size = new System.Drawing.Size(180, 22);
            this.TwoPlayerMode.TabIndex = 10;
            this.TwoPlayerMode.Text = "     ÇİFT KİŞİLİK      ";
            // 
            // cmbBoard
            // 
            this.cmbBoard.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBoard.FormattingEnabled = true;
            this.cmbBoard.Location = new System.Drawing.Point(574, 258);
            this.cmbBoard.Name = "cmbBoard";
            this.cmbBoard.Size = new System.Drawing.Size(301, 24);
            this.cmbBoard.TabIndex = 5;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblMode.Font = new System.Drawing.Font("Mistral", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMode.Location = new System.Drawing.Point(297, 310);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(191, 23);
            this.lblMode.TabIndex = 3;
            this.lblMode.Text = "       OYUN MODU            ";
            // 
            // panel
            // 
            this.panel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel.BackgroundImage")));
            this.panel.Controls.Add(this.TwoPlayerMode);
            this.panel.Controls.Add(this.btnStart);
            this.panel.Controls.Add(this.lblTitle);
            this.panel.Controls.Add(this.cmbBoard);
            this.panel.Controls.Add(this.lblMode);
            this.panel.Controls.Add(this.buttonExit);
            this.panel.Controls.Add(this.cmbCharacter);
            this.panel.Controls.Add(this.lbltheme);
            this.panel.Controls.Add(this.lblBoard);
            this.panel.Location = new System.Drawing.Point(-2, 1);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1035, 543);
            this.panel.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1036, 535);
            this.Controls.Add(this.panel);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ANA MENÜ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblBoard;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lbltheme;
        private System.Windows.Forms.ComboBox cmbCharacter;
        private System.Windows.Forms.Label TwoPlayerMode;
        private System.Windows.Forms.ComboBox cmbBoard;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Panel panel;
    }
}

