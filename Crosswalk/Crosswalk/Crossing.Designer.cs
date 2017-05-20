namespace Crosswalk
{
    partial class Crossing
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

        

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Crossing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Crossing";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Crossing_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Crossing_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Crossing_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Crossing_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Crossing_MouseUp);
            this.ResumeLayout(false);

        }

        

    }
}

