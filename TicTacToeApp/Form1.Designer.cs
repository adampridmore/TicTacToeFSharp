namespace TicTacToeApp
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
            this.tbGameState = new System.Windows.Forms.TextBox();
            this.tbToPlay = new System.Windows.Forms.TextBox();
            this.bNewGame = new System.Windows.Forms.Button();
            this.ticTacToeControl1 = new TicTacToeApp.TicTacToeControl();
            this.cbPlayAi = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbGameState
            // 
            this.tbGameState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGameState.Location = new System.Drawing.Point(748, 13);
            this.tbGameState.Name = "tbGameState";
            this.tbGameState.ReadOnly = true;
            this.tbGameState.Size = new System.Drawing.Size(464, 38);
            this.tbGameState.TabIndex = 1;
            // 
            // tbToPlay
            // 
            this.tbToPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbToPlay.Location = new System.Drawing.Point(747, 57);
            this.tbToPlay.Name = "tbToPlay";
            this.tbToPlay.ReadOnly = true;
            this.tbToPlay.Size = new System.Drawing.Size(464, 38);
            this.tbToPlay.TabIndex = 2;
            // 
            // bNewGame
            // 
            this.bNewGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bNewGame.Location = new System.Drawing.Point(894, 697);
            this.bNewGame.Name = "bNewGame";
            this.bNewGame.Size = new System.Drawing.Size(316, 93);
            this.bNewGame.TabIndex = 3;
            this.bNewGame.Text = "New Game";
            this.bNewGame.UseVisualStyleBackColor = true;
            this.bNewGame.Click += new System.EventHandler(this.bNewGame_Click);
            // 
            // ticTacToeControl1
            // 
            this.ticTacToeControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ticTacToeControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ticTacToeControl1.Game = null;
            this.ticTacToeControl1.Location = new System.Drawing.Point(12, 12);
            this.ticTacToeControl1.Name = "ticTacToeControl1";
            this.ticTacToeControl1.Size = new System.Drawing.Size(733, 650);
            this.ticTacToeControl1.TabIndex = 0;
            this.ticTacToeControl1.CellClick += new TicTacToeApp.TicTacToeControl.CellClicked(this.ticTacToeControl1_CellClick);
            // 
            // cbPlayAi
            // 
            this.cbPlayAi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPlayAi.AutoSize = true;
            this.cbPlayAi.Checked = true;
            this.cbPlayAi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPlayAi.Location = new System.Drawing.Point(747, 129);
            this.cbPlayAi.Name = "cbPlayAi";
            this.cbPlayAi.Size = new System.Drawing.Size(142, 36);
            this.cbPlayAi.TabIndex = 4;
            this.cbPlayAi.Text = "Play AI";
            this.cbPlayAi.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 802);
            this.Controls.Add(this.cbPlayAi);
            this.Controls.Add(this.bNewGame);
            this.Controls.Add(this.tbToPlay);
            this.Controls.Add(this.tbGameState);
            this.Controls.Add(this.ticTacToeControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TicTacToeControl ticTacToeControl1;
        private System.Windows.Forms.TextBox tbGameState;
        private System.Windows.Forms.TextBox tbToPlay;
        private System.Windows.Forms.Button bNewGame;
        private System.Windows.Forms.CheckBox cbPlayAi;
    }
}

