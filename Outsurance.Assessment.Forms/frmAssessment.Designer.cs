namespace Outsurance.Assessment.Forms
{
    partial class frmAssessment
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
            this.opencsvfiledialog = new System.Windows.Forms.OpenFileDialog();
            this.btnLoadCSV = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLoadCSV
            // 
            this.btnLoadCSV.Location = new System.Drawing.Point(12, 12);
            this.btnLoadCSV.Name = "btnLoadCSV";
            this.btnLoadCSV.Size = new System.Drawing.Size(373, 29);
            this.btnLoadCSV.TabIndex = 0;
            this.btnLoadCSV.Text = "Load CSV File and Manipulate Data";
            this.btnLoadCSV.UseVisualStyleBackColor = true;
            this.btnLoadCSV.Click += new System.EventHandler(this.btnLoadCSV_Click);
            // 
            // frmAssessment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 50);
            this.Controls.Add(this.btnLoadCSV);
            this.Name = "frmAssessment";
            this.Text = "Outsurance Assessment";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog opencsvfiledialog;
        private System.Windows.Forms.Button btnLoadCSV;
    }
}

