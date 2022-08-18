using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2;

namespace YoutubeVideoDownloader
{
    public partial class YoutubeBrowser : Form
    {
        public YoutubeBrowser()
        {
            InitializeComponent();
        }
        public YoutubeBrowser(string url)
        {
            InitializeComponent();
            webBrowser1.Source = new Uri(url);
        }
        private void YoutubeBrowser_Load(object sender, EventArgs e)
        {
     
        }

        private async void YoutubeBrowser_Shown(object sender, EventArgs e)
        {
         
        }

        private void YoutubeBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                control.Dispose();
            }
        }

        private void webBrowser1_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
            try
            {
                if (webBrowser1.Source.ToString().Substring(0, "https://www.youtube.com/watch?".Length) == "https://www.youtube.com/watch?")
                {
                    Program.Url = webBrowser1.Source.ToString();

                    webBrowser1.Source = new Uri("https://www.youtube.com");

                    Program.ValidUrl = true;
                    this.Close();
                }
            } catch { }
          
        }
    }
}
