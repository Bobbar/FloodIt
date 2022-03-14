namespace FloodIt
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CellBox = new System.Windows.Forms.PictureBox();
            this.NewBoardButton = new System.Windows.Forms.Button();
            this.ColorButtons = new System.Windows.Forms.TableLayoutPanel();
            this.PenetrationSolverButton = new System.Windows.Forms.Button();
            this.BestColorSolverButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.BruteForceSolverButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BoardSizeTextBox = new System.Windows.Forms.TextBox();
            this.NumColorsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BruteForceItsTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CellBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CellBox
            // 
            this.CellBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CellBox.Location = new System.Drawing.Point(202, 24);
            this.CellBox.Name = "CellBox";
            this.CellBox.Size = new System.Drawing.Size(570, 569);
            this.CellBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.CellBox.TabIndex = 0;
            this.CellBox.TabStop = false;
            this.CellBox.Paint += new System.Windows.Forms.PaintEventHandler(this.CellBox_Paint);
            // 
            // NewBoardButton
            // 
            this.NewBoardButton.Location = new System.Drawing.Point(22, 66);
            this.NewBoardButton.Name = "NewBoardButton";
            this.NewBoardButton.Size = new System.Drawing.Size(84, 27);
            this.NewBoardButton.TabIndex = 1;
            this.NewBoardButton.Text = "New Board";
            this.NewBoardButton.UseVisualStyleBackColor = true;
            this.NewBoardButton.Click += new System.EventHandler(this.NewBoardButton_Click);
            // 
            // ColorButtons
            // 
            this.ColorButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorButtons.ColumnCount = 1;
            this.ColorButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ColorButtons.Location = new System.Drawing.Point(791, 19);
            this.ColorButtons.Name = "ColorButtons";
            this.ColorButtons.RowCount = 1;
            this.ColorButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ColorButtons.Size = new System.Drawing.Size(176, 318);
            this.ColorButtons.TabIndex = 2;
            // 
            // PenetrationSolverButton
            // 
            this.PenetrationSolverButton.Location = new System.Drawing.Point(88, 386);
            this.PenetrationSolverButton.Name = "PenetrationSolverButton";
            this.PenetrationSolverButton.Size = new System.Drawing.Size(84, 38);
            this.PenetrationSolverButton.TabIndex = 3;
            this.PenetrationSolverButton.Text = "Solve Shortest";
            this.PenetrationSolverButton.UseVisualStyleBackColor = true;
            this.PenetrationSolverButton.Click += new System.EventHandler(this.PenetrationSolverButton_Click);
            // 
            // BestColorSolverButton
            // 
            this.BestColorSolverButton.Location = new System.Drawing.Point(88, 316);
            this.BestColorSolverButton.Name = "BestColorSolverButton";
            this.BestColorSolverButton.Size = new System.Drawing.Size(84, 64);
            this.BestColorSolverButton.TabIndex = 4;
            this.BestColorSolverButton.Text = "Solve Most Common";
            this.BestColorSolverButton.UseVisualStyleBackColor = true;
            this.BestColorSolverButton.Click += new System.EventHandler(this.BestColorSolverButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(22, 14);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(84, 46);
            this.ResetButton.TabIndex = 6;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // BruteForceSolverButton
            // 
            this.BruteForceSolverButton.Location = new System.Drawing.Point(88, 231);
            this.BruteForceSolverButton.Name = "BruteForceSolverButton";
            this.BruteForceSolverButton.Size = new System.Drawing.Size(84, 38);
            this.BruteForceSolverButton.TabIndex = 7;
            this.BruteForceSolverButton.Text = "Solve Brute Force";
            this.BruteForceSolverButton.UseVisualStyleBackColor = true;
            this.BruteForceSolverButton.Click += new System.EventHandler(this.BruteForceSolverButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Board Size (X,Y):";
            // 
            // BoardSizeTextBox
            // 
            this.BoardSizeTextBox.Location = new System.Drawing.Point(22, 122);
            this.BoardSizeTextBox.Name = "BoardSizeTextBox";
            this.BoardSizeTextBox.Size = new System.Drawing.Size(84, 23);
            this.BoardSizeTextBox.TabIndex = 9;
            this.BoardSizeTextBox.Text = "20,20";
            // 
            // NumColorsTextBox
            // 
            this.NumColorsTextBox.Location = new System.Drawing.Point(22, 166);
            this.NumColorsTextBox.Name = "NumColorsTextBox";
            this.NumColorsTextBox.Size = new System.Drawing.Size(84, 23);
            this.NumColorsTextBox.TabIndex = 11;
            this.NumColorsTextBox.Text = "4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "# Colors (Max: 8):";
            // 
            // BruteForceItsTextBox
            // 
            this.BruteForceItsTextBox.Location = new System.Drawing.Point(88, 275);
            this.BruteForceItsTextBox.Name = "BruteForceItsTextBox";
            this.BruteForceItsTextBox.Size = new System.Drawing.Size(84, 23);
            this.BruteForceItsTextBox.TabIndex = 12;
            this.BruteForceItsTextBox.Text = "20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 278);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Iterations:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 619);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BruteForceItsTextBox);
            this.Controls.Add(this.NumColorsTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BoardSizeTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BruteForceSolverButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.BestColorSolverButton);
            this.Controls.Add(this.PenetrationSolverButton);
            this.Controls.Add(this.ColorButtons);
            this.Controls.Add(this.NewBoardButton);
            this.Controls.Add(this.CellBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.CellBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox CellBox;
        private Button NewBoardButton;
        private TableLayoutPanel ColorButtons;
        private Button PenetrationSolverButton;
        private Button BestColorSolverButton;
        private Button ResetButton;
        private Button BruteForceSolverButton;
        private Label label1;
        private TextBox BoardSizeTextBox;
        private TextBox NumColorsTextBox;
        private Label label2;
        private TextBox BruteForceItsTextBox;
        private Label label3;
    }
}