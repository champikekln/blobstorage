namespace BlobHandler
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
            openFileDialog1 = new OpenFileDialog();
            btnLoadFile = new Button();
            lblFileName = new Label();
            btnUpload = new Button();
            blobGrid = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)blobGrid).BeginInit();
            SuspendLayout();
            // 
            // btnLoadFile
            // 
            btnLoadFile.Location = new Point(129, 106);
            btnLoadFile.Name = "btnLoadFile";
            btnLoadFile.Size = new Size(75, 23);
            btnLoadFile.TabIndex = 0;
            btnLoadFile.Text = "Load File";
            btnLoadFile.UseVisualStyleBackColor = true;
            btnLoadFile.Click += button1_Click;
            // 
            // lblFileName
            // 
            lblFileName.AutoSize = true;
            lblFileName.Location = new Point(210, 110);
            lblFileName.Name = "lblFileName";
            lblFileName.Size = new Size(38, 15);
            lblFileName.TabIndex = 1;
            lblFileName.Text = "label1";
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(129, 148);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(90, 34);
            btnUpload.TabIndex = 2;
            btnUpload.Text = "Upload File";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            // 
            // blobGrid
            // 
            blobGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            blobGrid.Location = new Point(43, 213);
            blobGrid.Name = "blobGrid";
            blobGrid.Size = new Size(707, 150);
            blobGrid.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(blobGrid);
            Controls.Add(btnUpload);
            Controls.Add(lblFileName);
            Controls.Add(btnLoadFile);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)blobGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private Button btnLoadFile;
        private Label lblFileName;
        private Button btnUpload;
        private DataGridView blobGrid;
    }
}
