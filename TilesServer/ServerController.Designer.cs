namespace TilesServer
{
    public partial class ServerController
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
            this.serverStart = new System.Windows.Forms.Button();
            this.consoleText = new System.Windows.Forms.RichTextBox();
            this.serverStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverStart
            // 
            this.serverStart.Location = new System.Drawing.Point(12, 12);
            this.serverStart.Name = "serverStart";
            this.serverStart.Size = new System.Drawing.Size(132, 48);
            this.serverStart.TabIndex = 0;
            this.serverStart.Text = "Start Server";
            this.serverStart.UseVisualStyleBackColor = true;
            this.serverStart.Click += new System.EventHandler(this.serverStart_Click);
            // 
            // consoleText
            // 
            this.consoleText.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.consoleText.ForeColor = System.Drawing.Color.Chartreuse;
            this.consoleText.Location = new System.Drawing.Point(12, 356);
            this.consoleText.Name = "consoleText";
            this.consoleText.ReadOnly = true;
            this.consoleText.Size = new System.Drawing.Size(491, 166);
            this.consoleText.TabIndex = 1;
            this.consoleText.Text = "";
            this.consoleText.TextChanged += new System.EventHandler(this.consoleText_TextChanged);
            // 
            // serverStop
            // 
            this.serverStop.Location = new System.Drawing.Point(150, 12);
            this.serverStop.Name = "serverStop";
            this.serverStop.Size = new System.Drawing.Size(132, 48);
            this.serverStop.TabIndex = 2;
            this.serverStop.Text = "Stop Server";
            this.serverStop.UseVisualStyleBackColor = true;
            this.serverStop.Click += new System.EventHandler(this.serverStop_Click);
            // 
            // ServerController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 534);
            this.Controls.Add(this.serverStop);
            this.Controls.Add(this.consoleText);
            this.Controls.Add(this.serverStart);
            this.KeyPreview = true;
            this.Name = "ServerController";
            this.Text = "ServerController";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button serverStart;
        private System.Windows.Forms.RichTextBox consoleText;
        private System.Windows.Forms.Button serverStop;
    }
}

