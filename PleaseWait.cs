using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;

namespace SharpOcarina
{
    public partial class PleaseWait : Form
    {
        string website = "";
        string downloadlocation = "";
        string unziplocation = "";
        bool delete = true;
        ExtractExistingFileAction unzipaction = ExtractExistingFileAction.Throw;
        public PleaseWait()
        {
            Init();
        }
        public PleaseWait(string _website, string _downloadlocation, string _unziplocation = "", bool _delete = false, ExtractExistingFileAction _unzipaction = ExtractExistingFileAction.Throw)
        {
            website = _website;
            downloadlocation = _downloadlocation;
            unziplocation = _unziplocation;
            delete = _delete;
            unzipaction = _unzipaction;
            Init();
        }

        public void Init()
        {
            InitializeComponent();
            DownloadLabel.Text = "Downloading " + website;
        }
        public async Task BeginDownload()
        {
            
            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += (s, e) =>
                {
                    ProgressBar.Invoke(new Action(() =>
                    {
                        ProgressBar.Value = e.ProgressPercentage;
                    }));
                };

                try
                {

                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


                    if (website != "")
                        await client.DownloadFileTaskAsync(new Uri(website), downloadlocation);
                    
                    if (unziplocation != "")
                    {
                        DownloadLabel.Text = "Extracting " + downloadlocation;
                        await Task.Run(() =>
                        {
                            using (var zip = ZipFile.Read(downloadlocation))
                            {
                                int totalFiles = zip.Count;
                                int extractedFiles = 0;

                                zip.ExtractProgress += (sender, e) =>
                                {
                                    if (e.EventType == ZipProgressEventType.Extracting_AfterExtractEntry)
                                    {
                                        extractedFiles++;
                                        int progress = (int)((extractedFiles / (double)totalFiles) * 100);

                                        ProgressBar.Invoke(new Action(() =>
                                        {
                                            ProgressBar.Value = progress;
                                        }));
                                    }
                                };
                                
                                zip.ExtractAll(unziplocation, unzipaction);
                            }
                            if (delete)
                                File.Delete(downloadlocation);
                        });


                    }
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    this.Close();
                }
            }


        }

        private void PleaseWait_Shown(object sender, EventArgs e)
        {

            BeginDownload();
        }
    }
}
