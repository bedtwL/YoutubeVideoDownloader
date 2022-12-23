using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeVideoDownloader
{
    internal static class Program
    {
        public static string Url=null;
        public static bool ValidUrl=false;
       
        public static YoutubeExplode.Videos.Video Videos;

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try { System.IO.Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "Baner",true); } catch { }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainUI());
        }
    }
}
