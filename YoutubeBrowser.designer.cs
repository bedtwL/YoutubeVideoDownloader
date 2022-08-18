namespace YoutubeVideoDownloader
{
    partial class YoutubeBrowser
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
            this.webBrowser1 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.webBrowser1)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowExternalDrop = true;
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.CreationProperties = null;
            this.webBrowser1.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1339, 766);
            this.webBrowser1.Source = new System.Uri("https://youtube.com", System.UriKind.Absolute);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.ZoomFactor = 1D;
            this.webBrowser1.SourceChanged += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs>(this.webBrowser1_SourceChanged);
            // 
            // YoutubeBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 766);
            this.Controls.Add(this.webBrowser1);
            this.Name = "YoutubeBrowser";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select one...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.YoutubeBrowser_FormClosing);
            this.Load += new System.EventHandler(this.YoutubeBrowser_Load);
            this.Shown += new System.EventHandler(this.YoutubeBrowser_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.webBrowser1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webBrowser1;
    }
}