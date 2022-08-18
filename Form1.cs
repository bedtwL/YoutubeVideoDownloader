using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode.Videos;
using YoutubeExplode;
using System.Reflection.Emit;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;
using System.IO;

namespace YoutubeVideoDownloader
{
    public partial class Form1 : Form
    {
        public static Video Video;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new YoutubeBrowser();
            form.ShowDialog();
            if (Program.ValidUrl)
            {
                textBox1.Text = Program.Url;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try {
                if (textBox1.Text.ToLower().Contains("youtube"))
                {

                    Program.ValidUrl = true;
                    LoadVideoInfo();

                }
                else if (textBox1.Text.ToLower().Contains("youtu.be"))
                {
                    Program.ValidUrl = true;
                    LoadVideoInfo();
                }
                else
                {
                    Program.ValidUrl = false; foreach (Control control in this.Controls)
                    {
                        control.Visible = false;
                        textBox1.Visible = true;
                        button1.Visible = true;
                    }
                    textBox1.Visible = true;
                    button1.Visible = true;
                }
            }
            catch {
                Program.ValidUrl = false; foreach (Control control in this.Controls)
                {

                    control.Visible = false;
                    textBox1.Visible = true;
                    button1.Visible = true;
                }
                textBox1.Visible = true;
                button1.Visible = true;
            }
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
          
            do
            {
                try
                {

                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
                await Task.Delay(1000);
            }
            while (true);
        }
        private async void LoadVideoInfo()
        {
     
                var YoutubeClient = new YoutubeExplode.YoutubeClient();
               
                Video = await YoutubeClient.Videos.GetAsync(textBox1.Text);
                label1.Text = Video.Duration.ToString();
                label2.Text = Video.Title;
                label3.Text = Video.Author.ToString();
                label4.Text = "View: " +Video.Engagement.ViewCount.ToString() + " Like Count: " + Video.Engagement.LikeCount;
            richTextBox1.Text = Video.Description;
         
            string LastUrl = null;
            foreach (var thumbnail in Video.Thumbnails)
            {
                try
                {
                    Console.WriteLine($"{thumbnail.Resolution} {thumbnail.Url}");
                    LastUrl = thumbnail.Url;
                    //pictureBox1.BackgroundImage = new Bitmap(thumbnail.Url);
                }
                catch (Exception ex) {  }

            }
            Random rd = new Random();

            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Baner");

            string FileNameImg = rd.Next(0, 999).ToString() + ".jpg";
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(new Uri(LastUrl), FileNameImg);

                }
                catch (Exception ex) {  }

            }
           
                System.IO.File.Copy(AppDomain.CurrentDomain.BaseDirectory + FileNameImg, AppDomain.CurrentDomain.BaseDirectory + "Baner\\" + FileNameImg);
            pictureBox1.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Baner\\" + FileNameImg);
            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory+FileNameImg);
            LastUrl = null;
            var youtube = new YoutubeClient();
            var channel = await youtube.Channels.GetAsync(Video.Author.ChannelId);
            


            foreach (var thumbnail in channel.Thumbnails)
            {
                try
                {
                    Console.WriteLine($"{thumbnail.Resolution} {thumbnail.Url}");
                    LastUrl = thumbnail.Url;
                    //pictureBox1.BackgroundImage = new Bitmap(thumbnail.Url);
                }
                catch (Exception ex) { }

            }


            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Baner");

            FileNameImg = rd.Next(0, 999).ToString() + ".jpg";
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(new Uri(LastUrl), FileNameImg);

                }
                catch (Exception ex) {}

            }
           
                System.IO.File.Copy(AppDomain.CurrentDomain.BaseDirectory + FileNameImg, AppDomain.CurrentDomain.BaseDirectory + "Baner\\" + FileNameImg);

            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory+FileNameImg);
           
           
            
                pictureBox2.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Baner\\" + FileNameImg);
            
            foreach (Control control in this.Controls)
            {
                control.Visible = true;
                textBox1.Visible = true;
                button1.Visible = true;
            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            HideAll();
            var youtube = new YoutubeClient();
            progressBar1.Value = 1;
            // Get the video ID

            var videoId = VideoId.Parse(textBox1.Text ?? "");
            progressBar1.Value = 20;
            // Get available streams and choose the best muxed (audio + video) stream
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoId);
            var streamInfo = streamManifest.GetAudioStreams().TryGetWithHighestBitrate();
            progressBar1.Value = 40;
            if (streamInfo is null)
            {
                // Available streams vary depending on the video and it's possible
                // there may not be any muxed streams at all.
                // See the readme to learn how to handle adaptive streams.

                return;
            }

            // Download the stream




            progressBar1.Value = 63;


           
                var fileName = $"{Video.Title}.{streamInfo.Container.Name}";
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + fileName))
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + fileName);
                }

                await youtube.Videos.Streams.DownloadAsync(streamInfo, fileName);
                MessageBox.Show("Audio have Saved into " + AppDomain.CurrentDomain.BaseDirectory + fileName);
            progressBar1.Value = 0;
            ShowAll();

        }
        private async void Download(string url,bool High)
        {
            try
            {
                HideAll();
                var youtube = new YoutubeClient();
                progressBar1.Value = 1;
                // Get the video ID

                var videoId = VideoId.Parse(url ?? "");
                progressBar1.Value = 20;
                // Get available streams and choose the best muxed (audio + video) stream
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoId);
                var streamInfo = streamManifest.GetMuxedStreams().TryGetWithHighestVideoQuality();
                progressBar1.Value = 40;
                if (streamInfo is null)
                {
                    // Available streams vary depending on the video and it's possible
                    // there may not be any muxed streams at all.
                    // See the readme to learn how to handle adaptive streams.

                    return;
                }

                // Download the stream




                progressBar1.Value = 63;


                if (!High)
                {
                    var fileName = $"{Video.Title}.{streamInfo.Container.Name}";
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + fileName))
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + fileName);
                    }

                    await youtube.Videos.Streams.DownloadAsync(streamInfo, fileName);
                    MessageBox.Show("Video have Saved into " + AppDomain.CurrentDomain.BaseDirectory + fileName);
                    progressBar1.Value = 0;
                    ShowAll();
                }
                else
                {
                   
                    /*
                    var streamInfo1 = streamManifest.GetVideoOnlyStreams().TryGetWithHighestVideoQuality();
                    var fileName = $"{videoId}.{streamInfo.Container.Name}";
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + fileName))
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + fileName);
                    }

                    await youtube.Videos.Streams.DownloadAsync(streamInfo1, fileName);*/
                    var streamManifest1 = await youtube.Videos.Streams.GetManifestAsync(url);
                    var fileName = $"{Video.Title}.{streamInfo.Container.Name}";
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + fileName))
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + fileName);
                    }
                    var te = streamManifest1.GetVideoStreams().TryGetWithHighestVideoQuality();
                    // Select streams (1080p60 / highest bitrate audio)
                    var audioStreamInfo = streamManifest.GetAudioStreams().GetWithHighestBitrate();
                    //var videoStreamInfo = streamManifest.GetVideoStreams().First(s => s.VideoQuality.Label == "1080p60");
                    var streamInfos = new IStreamInfo[] { audioStreamInfo, te };

                    // Download and process them into one file
                 
                    await youtube.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(fileName).Build());
                    MessageBox.Show("Video have Saved into "+AppDomain.CurrentDomain.BaseDirectory+fileName);
                    
                    /*
                   var streamInfo2 = streamManifest.GetAudioStreams().TryGetWithHighestBitrate();
                    Program.BetaAudioPath=  $"{videoId}_1.{streamInfo.Container.Name}";
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Program.BetaAudioPath))
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + Program.BetaAudioPath);
                    }
                    await youtube.Videos.Streams.DownloadAsync(streamInfo2, Program.BetaAudioPath);
                    string args = $"-i \"{Program.BetaVideoOnlyPath}\" -i \"{Program.BetaAudioPath}\" -shortest {$"{videoId}.{streamInfo.Container.Name}"}";
                    Process startInfo = new Process();
                    startInfo.StartInfo.CreateNoWindow = true;
                    startInfo.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory+ "ffmpeg-2022-08-10-git-8fc7f0fdec-full_build\\bin\\ffmpeg.exe";
                    startInfo.StartInfo.WorkingDirectory = @"" + AppDomain.CurrentDomain.BaseDirectory+ " ffmpeg-2022-08-10-git-8fc7f0fdec-full_build\\bin";
                    startInfo.StartInfo.Arguments = args;
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory+Program.BetaAudioPath,AppDomain.CurrentDomain.BaseDirectory+ "ffmpeg-2022-08-10-git-8fc7f0fdec-full_build\\"+Program.BetaAudioPath, true);
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + Program.BetaVideoOnlyPath, AppDomain.CurrentDomain.BaseDirectory + "ffmpeg-2022-08-10-git-8fc7f0fdec-full_build\\" + Program.BetaVideoOnlyPath, true);
                    startInfo.Start();
                    startInfo.WaitForExit();
                    try { File.Copy("ffmpeg-2022-08-10-git-8fc7f0fdec-full_build\\" + $"{videoId}.{streamInfo.Container.Name}", AppDomain.CurrentDomain.BaseDirectory + $"{videoId}.{streamInfo.Container.Name}", true); } catch { } */



                }



                // await youtube.Videos.Streams.DownloadAsync(streamInfo, fileName);
                progressBar1.Value = 100;
                progressBar1.Value = 0;
                ShowAll();
            }
            catch (Exception ex) { MessageBox.Show("Some Error on Downloading Youtube Video\n" + ex.Message); progressBar1.Value = 0; ShowAll(); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Download(textBox1.Text,false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Download(textBox1.Text, true);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            HideAll();
            var youtube = new YoutubeClient();
            progressBar1.Value = 1;
            // Get the video ID

            var videoId = VideoId.Parse(textBox1.Text ?? "");
            progressBar1.Value = 20;
            // Get available streams and choose the best muxed (audio + video) stream
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoId);
            var streamInfo = streamManifest.GetVideoOnlyStreams().TryGetWithHighestBitrate();
            progressBar1.Value = 40;
            if (streamInfo is null)
            {
                // Available streams vary depending on the video and it's possible
                // there may not be any muxed streams at all.
                // See the readme to learn how to handle adaptive streams.

                return;
            }

            // Download the stream




            progressBar1.Value = 63;



            var fileName = $"{Video.Title}.{streamInfo.Container.Name}";
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + fileName))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + fileName);
            }

            await youtube.Videos.Streams.DownloadAsync(streamInfo, fileName);
            MessageBox.Show("Audio have Saved into " + AppDomain.CurrentDomain.BaseDirectory + fileName);
            progressBar1.Value = 0;
            ShowAll();
        }
        private void HideAll()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            textBox1.Enabled = false;
        }
        private void ShowAll()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            textBox1.Enabled = true;
        }
    }
}
