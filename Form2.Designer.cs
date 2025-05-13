namespace ChessGameWithThemes
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.pnlBoard = new System.Windows.Forms.Panel();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.btnExitGame = new System.Windows.Forms.Button();
            this.lblTurn = new System.Windows.Forms.Label();
            this.btnStopMusic = new System.Windows.Forms.Button();
            this.lblTimer = new System.Windows.Forms.Label();
            this.timerGame = new System.Windows.Forms.Timer(this.components);
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBoard
            // 
            this.pnlBoard.Location = new System.Drawing.Point(123, 115);
            this.pnlBoard.Name = "pnlBoard";
            this.pnlBoard.Size = new System.Drawing.Size(200, 212);
            this.pnlBoard.TabIndex = 0;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.btnExitGame);
            this.pnlInfo.Controls.Add(this.lblTurn);
            this.pnlInfo.Controls.Add(this.btnStopMusic);
            this.pnlInfo.Controls.Add(this.lblTimer);
            this.pnlInfo.Location = new System.Drawing.Point(435, 70);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(199, 214);
            this.pnlInfo.TabIndex = 1;
            // 
            // btnExitGame
            // 
            this.btnExitGame.Location = new System.Drawing.Point(19, 133);
            this.btnExitGame.Name = "btnExitGame";
            this.btnExitGame.Size = new System.Drawing.Size(122, 23);
            this.btnExitGame.TabIndex = 5;
            this.btnExitGame.Text = "OYUNU KAPAT";
            this.btnExitGame.UseVisualStyleBackColor = true;
            this.btnExitGame.Click += new System.EventHandler(this.btnExitGame_Click);
            // 
            // lblTurn
            // 
            this.lblTurn.AutoSize = true;
            this.lblTurn.Location = new System.Drawing.Point(19, 13);
            this.lblTurn.Name = "lblTurn";
            this.lblTurn.Size = new System.Drawing.Size(109, 16);
            this.lblTurn.TabIndex = 2;
            this.lblTurn.Text = "SIRA:OYUNCU 1";
            // 
            // btnStopMusic
            // 
            this.btnStopMusic.Location = new System.Drawing.Point(22, 86);
            this.btnStopMusic.Name = "btnStopMusic";
            this.btnStopMusic.Size = new System.Drawing.Size(131, 23);
            this.btnStopMusic.TabIndex = 4;
            this.btnStopMusic.Text = "MÜZİĞİ KAPAT";
            this.btnStopMusic.UseVisualStyleBackColor = true;
            this.btnStopMusic.Click += new System.EventHandler(this.btnStopMusic_Click);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Location = new System.Drawing.Point(19, 45);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(122, 16);
            this.lblTimer.TabIndex = 3;
            this.lblTimer.Text = "KALAN SÜRE 30sn";
            // 
            // timerGame
            // 
            this.timerGame.Interval = 1000;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1039, 757);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlBoard);
            this.Name = "Form2";
            this.Text = " OYUN EKRANI";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBoard;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblTurn;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btnStopMusic;
        private System.Windows.Forms.Button btnExitGame;
        private System.Windows.Forms.Timer timerGame;
    }
}