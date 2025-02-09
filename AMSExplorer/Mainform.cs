﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Collections.Specialized;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Timers;


namespace AMSExplorer
{

    public partial class Mainform : Form
    {
        // XML Congiguration files path.
        private static string _configurationXMLFiles;
        private static string _HelpFiles;
        public static CredentialsEntry _credentials;
        public static bool havestoragecredentials = true;
        // Field for service context.
        public static CloudMediaContext _context = null;
        public static string Salt;
        private string _backuprootfolderupload = "";
        private StringBuilder sbuilder = new StringBuilder(); // used for locator copy to clipboard
        private BindingList<TransferEntry> _MyListTransfer; // list of upload/download
        private List<int> _MyListTransferQueue; // List of transfers in the queue. It contains the index in the order of schedule
        private ILocator PlayBackLocator = null;
        //Watch folder vars
        private Dictionary<string, DateTime> seen = new Dictionary<string, DateTime>();
        private TimeSpan seenInterval = new TimeSpan();
        private string WatchFolderFolderPath = string.Empty;
        private bool WatchFolderIsOn = false;
        private bool WatchFolderDeleteFile = false;
        FileSystemWatcher WatchFolderWatcher;
        private bool ZeniumPresent = true;

        private System.Timers.Timer TimerAutoRefresh;

        public Mainform()
        {
            InitializeComponent();

            if (Properties.Settings.Default.CallUpgrade) // upgrade settings from previous version
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.CallUpgrade = false;
            }

            _configurationXMLFiles = Application.StartupPath + Constants.PathConfigFiles;


            if ((Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder == string.Empty) | (!Directory.Exists(Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder)))
            {
                Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathAMEFiles;
                Properties.Settings.Default.Save();
            }

            _HelpFiles = Application.StartupPath + Constants.PathHelpFiles;

            AMSLogin form = new AMSLogin();

            if (form.ShowDialog() == DialogResult.Cancel)
            {
                Environment.Exit(0);
            }
            _credentials = form.LoginCredentials;

            // Get the service context.
            _context = Program.ConnectAndGetNewContext(_credentials);
            toolStripStatusLabelConnection.Text = String.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version) + " - Connected to " + _context.Credentials.ClientId;

            // name of the ams acount in the title of the form - useful when several instances to navigate with icons
            this.Text = string.Format(this.Text, _context.Credentials.ClientId);

            // Let's check storage credentials
            if (string.IsNullOrEmpty(_credentials.StorageKey))
            {
                havestoragecredentials = false;
            }
            else
            {
                bool ret = Program.ConnectToStorage(_context, _credentials);
            }

            if (GetLatestMediaProcessorByName(Constants.ZeniumEncoder) == null)
            {
                ZeniumPresent = false;
                encodeAssetWithImagineZeniumToolStripMenuItem.Enabled = false;  //menu
                ContextMenuItemZenium.Enabled = false; // mouse context menu
            }

            // Timer Auto Refresh
            TimerAutoRefresh = new System.Timers.Timer(Properties.Settings.Default.AutoRefreshTime * 1000);
            TimerAutoRefresh.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Let's check if there is one streaming unit running
            if (_context.StreamingEndpoints.AsEnumerable().Where(o => o.State == StreamingEndpointState.Running).ToList().Count == 0)
                TextBoxLogWriteLine("There is no streaming endpoint running in this account.", true); // Warning

            // Let's check if there is one streaming scale units
            if (_context.StreamingEndpoints.Where(o => o.ScaleUnits > 0).ToList().Count == 0)
                TextBoxLogWriteLine("There is no reserved unit streaming endpoint in this account. Dynamic packaging will not work.", true); // Warning

            ApplySettingsOptions(true);
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            DoRefresh();
        }



        private void ProcessImportFromHttp(Uri ObjectUrl, string assetname, string fileName, int index)
        {
            bool Error = false;

            TextBoxLogWriteLine("Starting the Http import process.");

            CloudBlockBlob blockBlob;
            IAssetFile assetFile;
            IAsset asset;
            ILocator destinationLocator = null;
            IAccessPolicy writePolicy = null;

            try
            {
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), true);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a new asset.
                asset = _context.Assets.Create(assetname, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
                writePolicy = _context.AccessPolicies.Create("writePolicy", TimeSpan.FromDays(2), AccessPermissions.Write);
                assetFile = asset.AssetFiles.Create(fileName);
                destinationLocator = _context.Locators.CreateLocator(LocatorType.Sas, asset, writePolicy);
                Uri uploadUri = new Uri(destinationLocator.Path);
                string assetContainerName = uploadUri.Segments[1];

                CloudBlobContainer mediaBlobContainer = cloudBlobClient.GetContainerReference(assetContainerName);

                TextBoxLogWriteLine("Creating the blob container.");

                mediaBlobContainer.CreateIfNotExists();

                blockBlob = mediaBlobContainer.GetBlockBlobReference(fileName);
                TextBoxLogWriteLine("Created a reference for block blob in Azure....");

                blockBlob.StartCopyFromBlob(ObjectUrl, null, null, null);

                DateTime startTime = DateTime.UtcNow;

                bool continueLoop = true;

                while (continueLoop)
                {

                    IEnumerable<IListBlobItem> blobsList = mediaBlobContainer.ListBlobs(null, true, BlobListingDetails.Copy);
                    foreach (var blob in blobsList)
                    {
                        var tempBlockBlob = (CloudBlockBlob)blob;
                        var destBlob = blob as CloudBlockBlob;

                        if (tempBlockBlob.Name == fileName)
                        {
                            var copyStatus = tempBlockBlob.CopyState;
                            if (copyStatus != null)
                            {
                                double percentComplete = (long)100 * (long)copyStatus.BytesCopied / (long)copyStatus.TotalBytes;

                                DoGridTransferUpdateProgress(percentComplete, index);

                                if (copyStatus.Status != CopyStatus.Pending)
                                {
                                    continueLoop = false;
                                    if (copyStatus.Status == CopyStatus.Failed) Error = true;
                                }
                            }
                        }
                    }

                    System.Threading.Thread.Sleep(1000);
                }
                DateTime endTime = DateTime.UtcNow;
                TimeSpan diffTime = endTime - startTime;

                if (!Error)
                {
                    TextBoxLogWriteLine("time transfer: {0}", diffTime.Duration().ToString());
                    TextBoxLogWriteLine("Creating Azure Media Services asset...");
                    blockBlob.FetchAttributes();
                    assetFile.ContentFileSize = blockBlob.Properties.Length;
                    assetFile.Update();
                    destinationLocator.Delete();
                    writePolicy.Delete();
                    // Refresh the asset.
                    asset = _context.Assets.Where(a => a.Id == asset.Id).FirstOrDefault();

                    DoGridTransferDeclareCompleted(index, asset.Id);
                    TextBoxLogWriteLine("You are ready to use asset '{0}'", asset.Name);
                }
                else // Error!
                {
                    TextBoxLogWriteLine("Error during file import.", true);
                    DoGridTransferDeclareError(index);
                    try
                    {
                        destinationLocator.Delete();
                        writePolicy.Delete();
                    }
                    catch { }


                }

            }

            catch (Exception ex)
            {
                Error = true;
                TextBoxLogWriteLine("Error during file import.", true);
                TextBoxLogWriteLine(ex.Message, true);
                DoGridTransferDeclareError(index);

                if (destinationLocator != null)
                {
                    try
                    {
                        destinationLocator.Delete();
                    }
                    catch
                    {

                    }
                }
                if (writePolicy != null)
                {
                    try
                    {
                        writePolicy.Delete();
                    }
                    catch
                    {

                    }

                }

            }

        }



        //delete all assets except those specified or published or Zenium Blueprint
        void DeleteAllAssets(string[] exceptionAssetNames)
        {
            List<string> objList_string = new List<string>();
            foreach (string assetName in exceptionAssetNames)
            {
                objList_string.Add(assetName);
            }
            foreach (IAsset objIAsset in _context.Assets)
            {
                if (!objList_string.Contains(objIAsset.Name))
                {
                    try
                    {
                        DeleteLocatorsForAsset(objIAsset);
                        objIAsset.Delete();
                    }
                    catch (Exception e)
                    {
                        TextBoxLogWriteLine("Error when deleting asset '{0}'.", objIAsset.Name);
                        TextBoxLogWriteLine(e);
                    }

                }
            }
            TextBoxLogWriteLine("DeleteAllAssets() completed.");
        }


        private void ProcessUploadFromFolder(object folderPath, int index)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);

            var filePaths = Directory.EnumerateFiles(folderPath as string);

            TextBoxLogWriteLine("There are {0} files in {1}", filePaths.Count().ToString(), (folderPath as string));
            if (!filePaths.Any())
            {
                throw new FileNotFoundException(String.Format("No files in directory, check folderPath: {0}", folderPath));
            }
            bool Error = false;

            string[] progressafname = new string[filePaths.Count()];
            double[] progressafint = new double[filePaths.Count()];

            int indexa = 0;
            IAsset asset = null;

            try
            {
                asset = _context.Assets.CreateFromFolder(
                                                               folderPath as string,
                                                               Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None,
                                                               (af, p) =>
                                                               {
                                                                   int indexc = Array.IndexOf(progressafname, af.Name);
                                                                   if (indexc == -1)
                                                                   {
                                                                       progressafname[indexa] = af.Name;
                                                                       progressafint[indexa] = (int)p.Progress;
                                                                       indexa++;

                                                                   }
                                                                   else
                                                                   {
                                                                       progressafint[indexc] = (int)p.Progress;
                                                                   }

                                                                   DoGridTransferUpdateProgress(progressafint.Average(), index);
                                                               }
                                                               );
                SetISMFileAsPrimary(asset);
            }
            catch
            {
                Error = true;
                DoGridTransferDeclareError(index);
                TextBoxLogWriteLine("Error when uploading from {0}", folderPath, true);
            }
            if (!Error)
            {
                TextBoxLogWriteLine(string.Format("Uploading of the file(s) in {0} done.", folderPath));
                DoGridTransferDeclareCompleted(index, asset.Id);
            }
            DoRefreshGridAssetV(false);

        }


        private static IMediaProcessor GetLatestMediaProcessorByName(string mediaProcessorName)
        {
            // The possible strings that can be passed into the 
            // method for the mediaProcessor parameter:
            //   Windows Azure Media Encoder
            //   Windows Azure Media Packager
            //   Windows Azure Media Encryptor
            //   Storage Decryption

            var processor = _context.MediaProcessors.Where(p => p.Name == mediaProcessorName).
                ToList().OrderBy(p => new Version(p.Version)).LastOrDefault();

            return processor;
        }

        private static List<IMediaProcessor> GetMediaProcessorsByName(string mediaProcessorName)
        {

            var processors = _context.MediaProcessors.Where(p => p.Name == mediaProcessorName).
                ToList().OrderBy(p => new Version(p.Version)).Reverse();

            return processors.ToList();
        }



        static IJob GetJob(string jobId)
        {
            // Use a Linq select query to get an updated 
            // reference by Id. 
            IJob job;
            try
            {
                var jobInstance =
                    from j in _context.Jobs
                    where j.Id == jobId
                    select j;
                // Return the job reference as an Ijob. 
                job = jobInstance.FirstOrDefault();
            }
            catch
            {
                job = null;
            }
            return job;
        }

        static IAsset GetAsset(string assetId)
        {
            IAsset asset;

            try
            {
                // Use a LINQ Select query to get an asset.
                var assetInstance =
                    from a in _context.Assets
                    where a.Id == assetId
                    select a;
                // Reference the asset as an IAsset.
                asset = assetInstance.FirstOrDefault();
            }
            catch
            {

                asset = null;
            }

            return asset;
        }

        static IChannel GetChannel(string channelId)
        {
            IChannel channel;

            try
            {
                // Use a LINQ Select query to get an asset.
                var channelInstance =
                    from a in _context.Channels
                    where a.Id == channelId
                    select a;
                // Reference the asset as an IAsset.
                channel = channelInstance.FirstOrDefault();
            }
            catch
            {

                channel = null;
            }

            return channel;
        }

        static IChannel GetChannelFromName(string name)
        {
            IChannel channel;

            try
            {
                // Use a LINQ Select query to get an asset.
                var channelInstance =
                    from a in _context.Channels
                    where a.Name == name
                    select a;
                // Reference the asset as an IAsset.
                channel = channelInstance.FirstOrDefault();
            }
            catch
            {

                channel = null;
            }

            return channel;
        }

        static IProgram GetProgram(string programId)
        {
            IProgram program;

            try
            {
                // Use a LINQ Select query to get an asset.
                var programInstance =
                    from a in _context.Programs
                    where a.Id == programId
                    select a;
                // Reference the asset as an IAsset.
                program = programInstance.FirstOrDefault();
            }
            catch
            {

                program = null;
            }

            return program;
        }

        static IStreamingEndpoint GetOrigin(string originId)
        {
            IStreamingEndpoint origin;

            try
            {
                // Use a LINQ Select query to get an asset.
                var originInstance =
                    from a in _context.StreamingEndpoints
                    where a.Id == originId
                    select a;
                // Reference the asset as an IAsset.
                origin = originInstance.FirstOrDefault();
            }
            catch
            {
                origin = null;
            }

            return origin;
        }


        static void DeleteAsset(IAsset asset)
        {
            // delete the asset
            asset.Delete();
        }

        public void DeleteLocatorsForAsset(IAsset asset)
        {
            if (asset != null)
            {
                string assetId = asset.Id;
                var locators = from a in _context.Locators
                               where a.AssetId == assetId
                               select a;

                foreach (var locator in locators)
                {
                    TextBoxLogWriteLine("Deleting locator {0} for asset {1}", locator.Path, assetId);
                    try
                    {
                        locator.Delete();
                    }
                    catch
                    {

                    }
                }
            }
        }

        void DeleteAccessPolicy(string existingPolicyId)
        {
            // To delete a specific access policy, get a reference to the policy.  
            // based on the policy Id passed to the method.
            var policyInstance =
                 from p in _context.AccessPolicies
                 where p.Id == existingPolicyId
                 select p;
            IAccessPolicy policy = policyInstance.FirstOrDefault();

            TextBoxLogWriteLine("Deleting policy {0}", existingPolicyId);
            policy.Delete();

        }


        static void SetISMFileAsPrimary(IAsset asset)
        {
            var ismAssetFiles = asset.AssetFiles.ToList().
                Where(f => f.Name.EndsWith(".ism", StringComparison.OrdinalIgnoreCase)).ToArray();

            if (ismAssetFiles.Count() != 1)
                return;

            ismAssetFiles.First().IsPrimary = true;
            ismAssetFiles.First().Update();
        }


        static void TextBoxAddLine(TextBox TB, string ST)
        {
            TB.Text += ST + "\r\n";
            TB.Refresh();
        }


        public void TextBoxLogWriteLine(string message, object o1, bool Error = false)
        {
            TextBoxLogWriteLine(string.Format(message, o1), Error);
        }

        public void TextBoxLogWriteLine(string message, object o1, object o2, bool Error = false)
        {
            TextBoxLogWriteLine(string.Format(message, o1, o2), Error);
        }

        public void TextBoxLogWriteLine(string message, object o1, object o2, object o3, bool Error = false)
        {
            TextBoxLogWriteLine(string.Format(message, o1, o2, o3), Error);
        }

        public void TextBoxLogWriteLine(Exception e)
        {
            TextBoxLogWriteLine(e.Message, true);
            if (e.InnerException != null)
            {
                TextBoxLogWriteLine(Program.GetErrorMessage(e), true);
            }
        }

        public void TextBoxLogWriteLine(string text, bool Error = false)
        {

            text += Environment.NewLine;

            if (richTextBoxLog.InvokeRequired)
            {
                richTextBoxLog.BeginInvoke(new Action(() =>
                {

                    richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                    richTextBoxLog.SelectionLength = 0;

                    richTextBoxLog.SelectionColor = Error ? Color.Red : Color.Black;
                    richTextBoxLog.AppendText(text);
                    richTextBoxLog.SelectionColor = richTextBoxLog.ForeColor;
                }));
            }
            else
            {
                richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                richTextBoxLog.SelectionLength = 0;

                richTextBoxLog.SelectionColor = Error ? Color.Red : Color.Black;
                richTextBoxLog.AppendText(text);
                richTextBoxLog.SelectionColor = richTextBoxLog.ForeColor;

            }
        }


        static private bool IsAZeniumBlueprint(IAsset asset)
        {
            if (asset.AssetFiles.Count() == 1)
            {
                return (asset.AssetFiles.FirstOrDefault().Name.ToLower().EndsWith(".kayak")
                    | asset.AssetFiles.FirstOrDefault().Name.ToLower().EndsWith(".xenio"));
            }
            else
            {
                return false;
            }
        }



        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            DoRefresh();
        }

        private void DoRefresh()
        {
            _context = Program.ConnectAndGetNewContext(_credentials);
            DoRefreshGridJobV(false);
            DoRefreshGridAssetV(false);
            DoRefreshGridChannelV(false);
            DoRefreshGridOriginV(false);
        }

        private void DoRefreshGridAssetV(bool firstime)
        {
            if (firstime)
            {

                dataGridViewAssetsV.Init(_context);
                for (int i = 1; i <= dataGridViewAssetsV.PageCount; i++) comboBoxPageAssets.Items.Add(i);
                comboBoxPageAssets.SelectedIndex = 0;
                Debug.WriteLine("DoRefreshGridAssetforsttime");
            }

            Debug.WriteLine("DoRefreshGridAssetNotforsttime");
            int backupindex = 0;
            int pagecount = 0;

            dataGridViewAssetsV.Invoke(new Action(() => dataGridViewAssetsV.AssetsPerPage = Properties.Settings.Default.NbItemsDisplayedInGrid));
            comboBoxPageAssets.Invoke(new Action(() => backupindex = comboBoxPageAssets.SelectedIndex));
            dataGridViewAssetsV.Invoke(new Action(() => dataGridViewAssetsV.RefreshAssets(_context, backupindex + 1)));
            comboBoxPageAssets.Invoke(new Action(() => comboBoxPageAssets.Items.Clear()));
            dataGridViewAssetsV.Invoke(new Action(() => pagecount = dataGridViewAssetsV.PageCount));

            for (int i = 1; i <= pagecount; i++) comboBoxPageAssets.Invoke(new Action(() => comboBoxPageAssets.Items.Add(i)));
            comboBoxPageAssets.Invoke(new Action(() => comboBoxPageAssets.SelectedIndex = dataGridViewAssetsV.CurrentPage - 1));

            tabPageAssets.Invoke(new Action(() => tabPageAssets.Text = string.Format(Constants.TabAssets + " ({0})", dataGridViewAssetsV.DisplayedCount)));
        }

        private void DoRefreshGridJobV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewJobsV.Init(_credentials);
                for (int i = 1; i <= dataGridViewJobsV.PageCount; i++) comboBoxPageJobs.Items.Add(i);
                comboBoxPageJobs.SelectedIndex = 0;

            }

            Debug.WriteLine("DoRefreshGridJobVNotforsttime");
            int backupindex = 0;
            int pagecount = 0;
            dataGridViewJobsV.Invoke(new Action(() => dataGridViewJobsV.JobssPerPage = Properties.Settings.Default.NbItemsDisplayedInGrid));

            comboBoxPageJobs.Invoke(new Action(() => backupindex = comboBoxPageJobs.SelectedIndex));
            dataGridViewJobsV.Invoke(new Action(() => dataGridViewJobsV.Refreshjobs(_context, backupindex + 1)));
            comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.Items.Clear()));
            dataGridViewJobsV.Invoke(new Action(() => pagecount = dataGridViewJobsV.PageCount));

            // add pages
            for (int i = 1; i <= pagecount; i++) comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.Items.Add(i)));
            comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.SelectedIndex = dataGridViewJobsV.CurrentPage - 1));
            //uodate tab nimber of jobs
            tabPageJobs.Invoke(new Action(() => tabPageJobs.Text = string.Format(Constants.TabJobs + " ({0})", dataGridViewJobsV.DisplayedCount)));
            dataGridViewJobsV.RestoreJobProgress();
        }


        private void fromASingleFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuUploadFromSingleFile();
        }

        private void DoMenuUploadFromSingleFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileNames.Count() > 1)
                {
                    if (System.Windows.Forms.MessageBox.Show("You selected multiple files. They will be uploaded as individual assets. If you want to create one single asset with several files, use 'Upload from a local folder' command.", "Upload as invividual assets?", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                        return;
                }

                // Each file goes in a individual asset
                foreach (String file in openFileDialog.FileNames)
                {
                    try
                    {
                        int index = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(file) + "'", TransferType.UploadFromFile, Properties.Settings.Default.useTransferQueue);
                        // Start a worker thread that does uploading.
                        Task.Factory.StartNew(() => ProcessUploadFile(file, index, false));
                        DotabControlMainSwitch(Constants.TabTransfers);
                        DoRefreshGridAssetV(false);
                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine("Error: Could not read file from disk.", true);
                        TextBoxLogWriteLine(ex.Message, true);
                    }
                }
            }
        }




        private void ProcessUploadFile(object name, int index, bool bdeletefile)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);

            TextBoxLogWriteLine("Starting upload of file '{0}'", name);
            bool Error = false;
            IAsset asset = null;
            try
            {
                asset = _context.Assets.CreateFromFile(
                                                      name as string,
                                                      Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None,
                                                      (af, p) =>
                                                      {
                                                          DoGridTransferUpdateProgress(p.Progress, index);
                                                      }
                                                      );
            }
            catch
            {
                Error = true;
                DoGridTransferDeclareError(index);
                TextBoxLogWriteLine("Error when uploading '{0}'", name, true);
            }
            if (!Error)
            {
                TextBoxLogWriteLine(string.Format("Uploading of {0} done.", name));
                DoGridTransferDeclareCompleted(index, asset.Id);
                if (bdeletefile) //use checked the box "delete the file"
                {
                    try
                    {
                        File.Delete(name as string);
                    }
                    catch
                    {
                        TextBoxLogWriteLine("Error when deleting '{0}'", name, true);
                    }
                }
            }
            DoRefreshGridAssetV(false);
        }




        private void DoGridTransferUpdateText(string progresstext, int index)
        {
            _MyListTransfer[index].Name = progresstext;
            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
        }
        private void DoGridTransferUpdateProgress(double progress, int index)
        {
            _MyListTransfer[index].Progress = progress;
            if (progress > 3 && _MyListTransfer[index].StartTime != null)
            {
                TimeSpan interval = (TimeSpan)(DateTime.UtcNow - ((DateTime)_MyListTransfer[index].StartTime).ToUniversalTime());
                DateTime ETA = DateTime.UtcNow.AddSeconds((100d / progress) * interval.TotalSeconds);
                _MyListTransfer[index].EndTime = ETA.ToLocalTime().ToString() + " ?";
            }

            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
        }

        private void DoGridTransferDeclareCompleted(int index, string DestLocation)  // Process is completed
        {
            _MyListTransfer[index].Progress = 100;
            _MyListTransfer[index].State = TransferState.Finished;
            _MyListTransfer[index].EndTime = DateTime.Now.ToString();
            _MyListTransfer[index].DestLocation = DestLocation;
            if (DoGridTransferIsQueueRequested(index)) _MyListTransferQueue.Remove(index);
            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
        }

        private void DoGridTransferDeclareError(int index)  // Process is completed
        {
            _MyListTransfer[index].Progress = 100;
            _MyListTransfer[index].EndTime = DateTime.Now.ToString();
            _MyListTransfer[index].State = TransferState.Error;
            if (DoGridTransferIsQueueRequested(index)) _MyListTransferQueue.Remove(index);
            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
        }

        private void DoGridTransferDeclareTransferStarted(int index)  // Process is started
        {
            _MyListTransfer[index].Progress = 0;
            _MyListTransfer[index].State = TransferState.Processing;
            _MyListTransfer[index].StartTime = DateTime.Now;
            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
        }

        private bool DoGridTransferQueueOurTurn(int index)  // Return true if this is out turn
        {
            return (_MyListTransferQueue.Count > 0) ? (_MyListTransferQueue[0] == index) : true;
        }

        private bool DoGridTransferIsQueueRequested(int index)  // Return true trasfer is managed in the queue
        {
            return (_MyListTransfer[index].processedinqueue);
        }

        private void DoGridTransferWaitIfNeeded(int index)
        {
            // If upload in the queue, let's wait our turn
            if (DoGridTransferIsQueueRequested(index))
            {
                while (!DoGridTransferQueueOurTurn(index))
                {
                    Thread.Sleep(500);
                }

                DoGridTransferDeclareTransferStarted(index);
            }
        }

        private void ProcessDownloadAsset(List<IAsset> SelectedAssets, object folder, int index)
        {
            bool multipleassets = false;
            // If download in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);

            string labeldb = "Starting download of " + SelectedAssets.FirstOrDefault().Name + " to " + folder as string + Constants.endline;
            if (SelectedAssets.Count > 1)
            {
                labeldb = "Starting download of files of " + SelectedAssets.Count + " assets to " + folder as string + Constants.endline;
                multipleassets = true;
            }
            TextBoxLogWriteLine(labeldb);
            foreach (IAsset mediaAsset in SelectedAssets)
            {
                string foldera = folder.ToString();
                if (multipleassets)
                {
                    foldera += "\\" + mediaAsset.Id.Substring(12);
                    Directory.CreateDirectory(foldera);

                }

                mediaAsset.DownloadToFolder(foldera,
                                                                 (af, p) =>
                                                                 {
                                                                     DoGridTransferUpdateProgress(p.Progress, index);

                                                                 }
                                                                );

            }
            TextBoxLogWriteLine("Download finished.");
            DoGridTransferDeclareCompleted(index, folder.ToString());

        }

        public void DoDownloadFileFromAsset(IAsset asset, IAssetFile File, object folder, int index)
        {

            string labeldb = "Starting download of " + File.Name + " of asset " + asset.Name + " to " + folder as string + Constants.endline;
            ILocator sasLocator = null;
            var locatorTask = Task.Factory.StartNew(() =>
            {
                sasLocator = _context.Locators.Create(LocatorType.Sas, asset, AccessPermissions.Read, TimeSpan.FromHours(24));
            });
            locatorTask.Wait();

            TextBoxLogWriteLine(labeldb);

            BlobTransferClient blobTransferClient = new BlobTransferClient
            {
                NumberOfConcurrentTransfers = _context.NumberOfConcurrentTransfers,
                ParallelTransferThreadCount = _context.ParallelTransferThreadCount
            };


            Task.Factory.StartNew(async () =>
            {
                bool Error = false;
                try
                {
                    await File.DownloadAsync(Path.Combine(folder as string, File.Name), blobTransferClient, sasLocator, CancellationToken.None);
                    sasLocator.Delete();
                }
                catch
                {
                    Error = true;
                    TextBoxLogWriteLine(string.Format("Download of file '{0}' failed !", File.Name), true);
                    DoGridTransferDeclareError(index);

                }
                if (!Error)
                {
                    TextBoxLogWriteLine(string.Format("Download of file '{0}' is finished.", File.Name));
                    DoGridTransferDeclareCompleted(index, folder.ToString());
                }
            });

            MessageBox.Show("Download process has been initiated. See the Transfers tab to see the progress.");
        }



        private void fromMultipleFilesToolStripMenuItem_Click(object sender, EventArgs e) // upload from multiple files
        {
            DoMenuUploadFromFolder();
        }

        private void DoMenuUploadFromFolder()
        {
            FolderBrowserDialog openFolderDialog1 = new FolderBrowserDialog();

            string name;
            if (!string.IsNullOrEmpty(_backuprootfolderupload)) openFolderDialog1.SelectedPath = _backuprootfolderupload;

            if (openFolderDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((name = openFolderDialog1.SelectedPath) != null)
                    {
                        _backuprootfolderupload = name;
                        int index = DoGridTransferAddItem(string.Format("Upload of folder '{0}'", Path.GetFileName(name)), TransferType.UploadFromFolder, Properties.Settings.Default.useTransferQueue);

                        // Start a worker thread that does uploading.
                        Task.Factory.StartNew(() => ProcessUploadFromFolder(name, index));
                        DotabControlMainSwitch(Constants.TabTransfers);
                        DoRefreshGridAssetV(false);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + Constants.endline + ex.Message);
                    TextBoxLogWriteLine("Error: Could not read file from disk.", true);
                    TextBoxLogWriteLine(ex.Message, true);
                }
            }

        }


        private void DoMenuImportFromHttp()
        {
            string valuekey = "";

            if (!havestoragecredentials)
            { // No blob credentials. Let's ask the user

                if (InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + _context.DefaultStorageAccount.Name + ":", ref valuekey) == DialogResult.OK)
                {
                    _credentials.StorageKey = valuekey;
                    havestoragecredentials = true;
                }
            }

            if (havestoragecredentials) // if we have the storage credentials
            {
                ImportHttp form = new ImportHttp();

                if (form.ShowDialog() == DialogResult.OK)
                {
                    int index = DoGridTransferAddItem(string.Format("Import from Http of '{0}'", form.GetAssetFileName), TransferType.ImportFromHttp, false);
                    // Start a worker thread that does uploading.
                    Task.Factory.StartNew(() => ProcessImportFromHttp(form.GetURL, form.GetAssetName, form.GetAssetFileName, index));
                    DotabControlMainSwitch(Constants.TabTransfers);
                    DoRefreshGridAssetV(false);
                }
            }
        }



        private async void DoMergeAssetsToNewAsset()
        {
            IList<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count > 0)
            {
                if (SelectedAssets.Any(a => a.Options != AssetCreationOptions.None))
                {
                    MessageBox.Show("Assets cannot be merged as at least one asset is encrypted.", "Asset encrypted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    string newassetname = string.Empty;
                    if (InputBox("Assets merging", "Enter the new asset name:", ref newassetname) == DialogResult.OK)
                    {

                        if (!havestoragecredentials)
                        { // No blob credentials.
                            MessageBox.Show("Please specifiy the account storage key in the login window to access this feature.");
                        }
                        else
                        {
                            await Task.Factory.StartNew(() => ProcessMergeAssetsInNewAsset(SelectedAssets, newassetname));
                            // Refresh the assets.
                            DoRefreshGridAssetV(false);
                        }
                    }
                }
            }
        }


        private void ProcessMergeAssetsInNewAsset(IList<IAsset> MyAssets, string newassetname)
        {
            bool Error = false;
            try
            {
                TextBoxLogWriteLine("Merging assets to new asset '{0}'...", newassetname);
                IAsset NewAsset = _context.Assets.Create(newassetname, AssetCreationOptions.None); // No encryption as we do storage copy
                CloudStorageAccount storageAccount;
                storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, Mainform._credentials.StorageKey), true);
                var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                IAccessPolicy writePolicy = _context.AccessPolicies.Create("writePolicy", TimeSpan.FromDays(1), AccessPermissions.Write);
                IAccessPolicy readPolicy = _context.AccessPolicies.Create("readPolicy", TimeSpan.FromDays(1), AccessPermissions.Read);


                ILocator destinationLocator = _context.Locators.CreateLocator(LocatorType.Sas, NewAsset, writePolicy);
                Uri uploadUri = new Uri(destinationLocator.Path);

                foreach (IAsset MyAsset in MyAssets)
                {
                    ILocator sourceLocator = _context.Locators.CreateLocator(LocatorType.Sas, MyAsset, readPolicy);
                    Uri SourceUri = new Uri(sourceLocator.Path);
                    foreach (IAssetFile MyAssetFile in MyAsset.AssetFiles)
                    {
                        TextBoxLogWriteLine("   Copying file '{0}' from asset '{1}'...", MyAssetFile.Name, MyAsset.Name);
                        if (MyAssetFile.IsEncrypted)
                        {
                            TextBoxLogWriteLine("   Cannot copy file '{0}' because it is encrypted.", MyAssetFile.Name, true);
                        }
                        else
                        {
                            IAssetFile AssetFileTarget = NewAsset.AssetFiles.Where(f => f.Name == MyAssetFile.Name).FirstOrDefault();
                            if (AssetFileTarget == null)
                            {
                                AssetFileTarget = NewAsset.AssetFiles.Create(MyAssetFile.Name); // does not exist so we create it
                            }
                            else
                            {
                                int i = 0;
                                while (NewAsset.AssetFiles.Where(f => f.Name == Path.GetFileNameWithoutExtension(MyAssetFile.Name) + "#" + i.ToString() + Path.GetExtension(MyAssetFile.Name)).FirstOrDefault() != null)
                                {
                                    i++;
                                }
                                AssetFileTarget = NewAsset.AssetFiles.Create(Path.GetFileNameWithoutExtension(MyAssetFile.Name) + "#" + i.ToString() + Path.GetExtension(MyAssetFile.Name));// exist so we add a number
                            }

                            // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                            string sourceTargetContainerName = SourceUri.Segments[1];
                            string assetTargetContainerName = uploadUri.Segments[1];
                            CloudBlobContainer mediaBlobContainer = cloudBlobClient.GetContainerReference(sourceTargetContainerName);
                            CloudBlobContainer assetTargetContainer = cloudBlobClient.GetContainerReference(assetTargetContainerName);

                            CloudBlockBlob sourceCloudBlob, destinationBlob;

                            sourceCloudBlob = mediaBlobContainer.GetBlockBlobReference(MyAssetFile.Name);
                            sourceCloudBlob.FetchAttributes();

                            if (sourceCloudBlob.Properties.Length > 0)
                            {

                                destinationBlob = assetTargetContainer.GetBlockBlobReference(AssetFileTarget.Name);

                                destinationBlob.DeleteIfExists();
                                destinationBlob.StartCopyFromBlob(sourceCloudBlob);

                                CloudBlockBlob blob;
                                blob = (CloudBlockBlob)assetTargetContainer.GetBlobReferenceFromServer(AssetFileTarget.Name);

                                while (blob.CopyState.Status == CopyStatus.Pending)
                                {
                                    Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                }
                                destinationBlob.FetchAttributes();
                                AssetFileTarget.ContentFileSize = sourceCloudBlob.Properties.Length;
                                AssetFileTarget.Update();

                                MyAsset.Update();


                            }
                        }

                    }
                    sourceLocator.Delete();
                }

                destinationLocator.Delete();
                readPolicy.Delete();
                writePolicy.Delete();

            }
            catch
            {
                MessageBox.Show("Error when merging the assets.");
                TextBoxLogWriteLine("Error when merging the assets.", true);
                Error = true;
            }
            if (!Error) TextBoxLogWriteLine("Assets merged to new asset '{0}'.", newassetname);

        }


        private void DotabControlMainSwitch(string tab)
        {
            foreach (TabPage page in tabControlMain.TabPages)
            {
                if (page.Text.Contains(tab))
                {
                    tabControlMain.BeginInvoke(new Action(() => tabControlMain.SelectedTab = page), null);
                    break;
                }
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Button buttonOk = new Button()
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Right
                };

            Button buttonCancel = new Button()
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Right
                };


            Form form = new Form()
            {
                ClientSize = new Size(396, 107),
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false,
                AcceptButton = buttonOk,
                CancelButton = buttonCancel,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            Label label = new Label()
            {
                AutoSize = true,
                Text = promptText
            };
            TextBox textBox = new TextBox()
            {
                Text = value
            };


            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }



        public DialogResult DisplayInfo(IAsset asset)
        {
            AssetInformation form = new AssetInformation()
                {
                    MyAsset = asset,
                    MyContext = _context
                };

            DialogResult dialogResult = form.ShowDialog(this);
            return dialogResult;
        }


        public static DialogResult CopyAssetToAzure(ref bool UseDefaultStorage, ref string containername, ref string otherstoragename, ref string otherstoragekey, ref List<IAssetFile> SelectedFiles, ref bool CreateNewContainer, IAsset sourceAsset)
        {
            ExportAssetToAzureStorage form = new ExportAssetToAzureStorage(_context, _credentials.StorageKey);
            TreeView TreeViewBlob = (TreeView)form.Controls.Find("TreeViewBlob", true).FirstOrDefault();
            ListBox ListBoxFiles = (ListBox)form.Controls.Find("ListBoxFiles", true).FirstOrDefault();
            ListView listViewAssetFiles = (ListView)form.Controls.Find("listViewAssetFiles", true).FirstOrDefault();

            form.BlobStorageDefault = UseDefaultStorage;
            form.BlobLabelDefaultStorage = _context.DefaultStorageAccount.Name;

            // list asset files ///////////////////////
            bool bfileinasset = (sourceAsset.AssetFiles.Count() == 0) ? false : true;
            listViewAssetFiles.Items.Clear();
            if (bfileinasset)
            {
                listViewAssetFiles.BeginUpdate();
                foreach (IAssetFile file in sourceAsset.AssetFiles)
                {
                    ListViewItem item = new ListViewItem(file.Name, 0);
                    if (file.IsPrimary) item.ForeColor = Color.Blue;
                    item.SubItems.Add(file.LastModified.ToLocalTime().ToString());
                    item.SubItems.Add(AssetInfo.FormatByteSize(file.ContentFileSize));
                    (listViewAssetFiles.Items.Add(item)).Selected = true;
                    form.listassetfiles.Add(file);
                }

                listViewAssetFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewAssetFiles.EndUpdate();

            }
            form.BlobLabelWarning = sourceAsset.Options == AssetCreationOptions.StorageEncrypted ? "Note: asset is storage encrypted" : "";

            DialogResult dialogResult = form.ShowDialog();

            UseDefaultStorage = form.BlobStorageDefault;
            if (!UseDefaultStorage)
            {
                otherstoragename = form.BlobOtherStorageName;
                otherstoragekey = form.BlobOtherStorageKey;
            }
            CreateNewContainer = form.BlobCreateNewContainer;
            containername = CreateNewContainer ? form.BlobNewContainerName : form.SelectedContainer;
            SelectedFiles = form.SelectedAssetFiles;
            return dialogResult;
        }



        public static DialogResult DisplayInfo(IJob job)
        {
            CloudMediaContext context = Program.ConnectAndGetNewContext(_credentials);
            JobInformation form = new JobInformation(context);
            // we get a new context to have the latest job and task information (otherwise, task is not dynamically updated)
            form.MyJob = context.Jobs.Where(j => j.Id == job.Id).FirstOrDefault();
            DialogResult dialogResult = form.ShowDialog();
            return dialogResult;
        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)  // RENAME ASSET
        {
            DoMenuRenameAsset();

        }


        private void DoMenuRenameAsset()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count > 0)
            {
                IAsset AssetTORename = SelectedAssets.FirstOrDefault();

                if (AssetTORename != null)
                {
                    string value = AssetTORename.Name;

                    if (InputBox("Asset rename", "Enter the new name:", ref value) == DialogResult.OK)
                    {
                        try
                        {
                            AssetTORename.Name = value;
                            AssetTORename.Update();
                        }
                        catch
                        {

                            TextBoxLogWriteLine("There is a problem when renaming the asset.", true);
                            return;
                        }


                        TextBoxLogWriteLine("Renamed asset '{0}'.", AssetTORename.Id);
                        DoRefreshGridAssetV(false);


                    }

                }
            }
        }


        private void DoMenuDownloadToLocal()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count == 0) return;
            IAsset mediaAsset = SelectedAssets.FirstOrDefault();
            if (mediaAsset == null) return;
            if (folderBrowserDialogDownload.ShowDialog() == DialogResult.OK)
            {
                string label = string.Format("Download of asset '{0}'", mediaAsset.Name);
                if (SelectedAssets.Count > 1) label = string.Format("Download of {0} assets", SelectedAssets.Count);

                int index = DoGridTransferAddItem(label, TransferType.DownloadToLocal, Properties.Settings.Default.useTransferQueue);
                // Start a worker thread that does downloading.
                Task.Factory.StartNew(() => ProcessDownloadAsset(SelectedAssets, folderBrowserDialogDownload.SelectedPath, index));
                DotabControlMainSwitch(Constants.TabTransfers);
            }
        }


        private void cancelJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCancelJobs();
        }


        private void DoCancelJobs()
        {
            List<IJob> SelectedJobs = ReturnSelectedJobs();

            if (SelectedJobs.Count > 0)
            {
                string question = "Cancel these " + SelectedJobs.Count + " jobs ?";
                if (SelectedJobs.Count == 1) question = "Cancel " + SelectedJobs[0].Name + " ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Job(s) cancelation", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (IJob JobToCancel in SelectedJobs)
                    {
                        if (JobToCancel != null)
                        {
                            //delete
                            TextBoxLogWriteLine("Canceling job '{0}'...", JobToCancel.Name);

                            try
                            {
                                JobToCancel.Cancel();
                                TextBoxLogWriteLine("Job '{0}' canceled.", JobToCancel.Name);

                            }
                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("Error when canceling job '{0}'.", JobToCancel.Name, true);
                                TextBoxLogWriteLine(e);
                            }
                        }
                    }
                    DoRefreshGridJobV(false);
                }
            }
        }

        private void assetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void DoCreateLocator(List<IAsset> SelectedAssets)
        {
            string labelAssetName;
            if (SelectedAssets.Count > 0)
            {
                labelAssetName = "A locator will be created for Asset '" + SelectedAssets.FirstOrDefault().Name + "'.";

                if (SelectedAssets.Count > 1)
                {
                    labelAssetName = "A locator will be created for the " + SelectedAssets.Count.ToString() + " selected assets.";
                }

                CreateLocator form = new CreateLocator()
                {
                    LocStartDate = DateTime.Now.ToLocalTime(),
                    LocEndDate = DateTime.Now.ToLocalTime().AddDays(30),
                    LocAssetName = labelAssetName,
                    LocHasStartDate = false,
                    LocWarning = _context.StreamingEndpoints.Where(o => o.ScaleUnits > 0).ToList().Count > 0 ? string.Empty : "Dynamic packaging will not work as there is no scale unit streaming endpoint in this account."
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // The permissions for the locator's access policy.
                    AccessPermissions accessPolicyPermissions = AccessPermissions.Read;

                    // The duration for the locator's access policy.
                    TimeSpan accessPolicyDuration = form.LocEndDate.Subtract(DateTime.Now.ToLocalTime());

                    sbuilder.Clear();

                    foreach (IAsset AssetTOProcess in SelectedAssets)

                        if (AssetTOProcess != null)
                        {
                            //delete
                            TextBoxLogWriteLine("Creating locator for asset '{0}'... ", AssetTOProcess.Name);
                            try
                            {

                                Task.Factory.StartNew(() => DoCreateLocator(form.LocType, AssetTOProcess, accessPolicyPermissions, accessPolicyDuration, form.LocStartDate));
                            }

                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when creating the locator on asset '{0}'.", AssetTOProcess.Name, true);
                                TextBoxLogWriteLine(e);
                            }
                        }
                }
            }
        }





        private void DoCreateLocator(LocatorType locatorType, IAsset AssetToP, AccessPermissions accessPolicyPermissions, TimeSpan accessPolicyDuration, Nullable<DateTime> startTime)
        {
            ILocator locator = null;
            try
            {
                locator = _context.Locators.Create(locatorType, AssetToP, accessPolicyPermissions, accessPolicyDuration, startTime);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error. Could not create a locator for '{0}' (is the asset encrypted, or locators quota has been reached ?)", AssetToP.Name, true);
                TextBoxLogWriteLine(ex.Message, true);
                return;
            }


            if (locator == null) return;

            StringBuilder sbuilderThisAsset = new StringBuilder();

            sbuilderThisAsset.AppendLine("");
            sbuilderThisAsset.AppendLine("Asset:");
            sbuilderThisAsset.AppendLine(AssetToP.Name);
            sbuilderThisAsset.AppendLine("Asset ID:");
            sbuilderThisAsset.AppendLine(AssetToP.Id);
            sbuilderThisAsset.AppendLine("Locator ID:");
            sbuilderThisAsset.AppendLine(locator.Id);
            sbuilderThisAsset.AppendLine("Locator Path:");
            sbuilderThisAsset.AppendLine(locator.Path);

            if (locatorType == LocatorType.OnDemandOrigin)
            {
                // Get the MPEG-DASH URL of the asset for adaptive streaming.
                Uri mpegDashUri = locator.GetMpegDashUri();

                // Get the HLS URL of the asset for adaptive streaming.
                Uri HLSUri = locator.GetHlsUri();

                // Get the Smooth URL of the asset for adaptive streaming.
                Uri SmoothUri = locator.GetSmoothStreamingUri();


                if (SmoothUri != null)
                {
                    sbuilderThisAsset.AppendLine(AssetInfo._smooth + " : ");
                    sbuilderThisAsset.AppendLine(AddBracket(SmoothUri.ToString()));
                    sbuilderThisAsset.AppendLine(AssetInfo._smooth_legacy + " : ");
                    sbuilderThisAsset.AppendLine(AddBracket(AssetInfo.GetSmoothLegacy(locator.GetSmoothStreamingUri().ToString())));
                }
                if (HLSUri != null)
                {
                    sbuilderThisAsset.AppendLine(AssetInfo._hls_v4 + " : ");
                    sbuilderThisAsset.AppendLine(AddBracket(HLSUri.ToString()));
                    sbuilderThisAsset.AppendLine(AssetInfo._hls_v3 + " : ");
                    sbuilderThisAsset.AppendLine(AddBracket(locator.GetHlsv3Uri().ToString()));
                }
                if (mpegDashUri != null)
                {
                    sbuilderThisAsset.AppendLine(AssetInfo._dash + " : ");
                    sbuilderThisAsset.AppendLine(AddBracket(mpegDashUri.ToString()));
                }

            }
            else //SAS
            {

                IEnumerable<IAssetFile> AssetFiles = AssetToP
                   .AssetFiles
                   .ToList();

                // Generate the Progressive Download URLs for each file. 
                List<Uri> ProgressiveDownloadUris =
                    AssetFiles.Select(af => af.GetSasUri()).ToList();


                TextBoxLogWriteLine("You can progressively download the following files :");
                ProgressiveDownloadUris.ForEach(uri =>
                                {
                                    sbuilderThisAsset.AppendLine(AddBracket(uri.ToString()));

                                }
                                    );

            }

            //log window
            TextBoxLogWriteLine(sbuilderThisAsset.ToString());

            if (sbuilderThisAsset != null)
            {
                sbuilder.Append(sbuilderThisAsset); // we add this builder to the general builder
                // COPY to clipboard. We need to create a STA thread for it
                System.Threading.Thread MyThread = new Thread(new ParameterizedThreadStart(DoCopyClipboard));
                MyThread.SetApartmentState(ApartmentState.STA);
                MyThread.IsBackground = true;
                MyThread.Start(sbuilder.ToString());
            }

            dataGridViewAssetsV.AnalyzeItemsInBackground();
        }

        public string AddBracket(string url)
        {
            return "<" + url + ">";
        }

        public void DoCopyClipboard(object text)
        {
            Clipboard.SetText((string)text);
        }



        private void DoDeleteAllLocatorsOnAssets(List<IAsset> SelectedAssets)
        {
            if (SelectedAssets.Count > 0)
            {
                string question = "Delete all locators of these " + SelectedAssets.Count + " assets ?";
                if (SelectedAssets.Count == 1) question = "Delete all the locators of " + SelectedAssets[0].Name + " ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Locators deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (IAsset AssetToProcess in SelectedAssets)
                    {
                        if (AssetToProcess != null)
                        {
                            //delete locators
                            TextBoxLogWriteLine("Deleting locators of asset '{0}'", AssetToProcess.Name);
                            try
                            {
                                DeleteLocatorsForAsset(AssetToProcess);
                                TextBoxLogWriteLine("Deletion done.");
                            }

                            catch (Exception ex)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when deleting locators of the asset {0}.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(ex.Message, true);
                            }
                            dataGridViewAssetsV.AnalyzeItemsInBackground();

                        }

                    }
                }
            }
        }

        private List<IAsset> ReturnSelectedAssetsFromProgramsOrAssets()
        {
            if (tabControlMain.SelectedTab.Text.StartsWith(Constants.TabAssets)) // we are in the asset tab
            {
                return ReturnSelectedAssets();
            }
            else if (tabControlMain.SelectedTab.Text.StartsWith(Constants.TabLive)) // we are in the live tab
            {
                return ReturnSelectedPrograms().Select(p => p.Asset).ToList();
            }
            else
            {
                return null;
            }
        }


        private List<IAsset> ReturnSelectedAssets()
        {
            List<IAsset> SelectedAssets = new List<IAsset>();
            foreach (DataGridViewRow Row in dataGridViewAssetsV.SelectedRows)
            {
                SelectedAssets.Add(_context.Assets.Where(j => j.Id == Row.Cells[dataGridViewAssetsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault());
            }
            SelectedAssets.Reverse();
            return SelectedAssets;
        }

        private List<IJob> ReturnSelectedJobs()
        {
            List<IJob> SelectedJobs = new List<IJob>();
            foreach (DataGridViewRow Row in dataGridViewJobsV.SelectedRows)
                SelectedJobs.Add(_context.Jobs.Where(j => j.Id == Row.Cells["Id"].Value.ToString()).FirstOrDefault());
            SelectedJobs.Reverse();
            return SelectedJobs;
        }

        private void selectedAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDeleteSelectedAssets();

        }

        private void DoMenuDeleteSelectedAssets()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count > 0)
            {
                string question = (dataGridViewAssetsV.SelectedRows.Count == 1) ? "Delete " + SelectedAssets[0].Name + " ?" : "Delete these " + SelectedAssets.Count + " assets ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Asset deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (IAsset AssetTODelete in SelectedAssets)

                        if (AssetTODelete != null)
                        {
                            //delete
                            TextBoxLogWriteLine("Deleting asset '{0}'", AssetTODelete.Name);
                            try
                            {
                                DeleteAsset(AssetTODelete);
                                if (GetAsset(AssetTODelete.Id) == null) TextBoxLogWriteLine("Deletion done.");
                            }

                            catch (Exception ex)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when deleting the asset {0}.", AssetTODelete.Name, true);
                                TextBoxLogWriteLine(ex.Message, true);
                            }
                        }
                    DoRefreshGridAssetV(false);
                }
            }

        }

        private void allAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to delete ALL the assets ?" + Constants.endline + "There are " + _context.Assets.Count().ToString() + " assets in the account.", "Assets deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                // Set cursor as hourglass
                this.Cursor = Cursors.WaitCursor;
                foreach (IAsset asset in _context.Assets)
                {
                    DeleteAllAssets(new string[] { "" });
                }
                System.Threading.Thread.Sleep(1000);
                DoRefreshGridAssetV(false);
                // Set cursor as default arrow
                this.Cursor = Cursors.Default;
            }
        }


        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfo();
        }

        private void DoMenuDisplayAssetInfo()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count != 1) return;
            IAsset AssetToDisplayP = SelectedAssets.FirstOrDefault();
            if (AssetToDisplayP == null) return;

            // Refresh the asset.
            AssetToDisplayP = _context.Assets.Where(a => a.Id == AssetToDisplayP.Id).FirstOrDefault();

            if (DisplayInfo(AssetToDisplayP) == DialogResult.OK)
            {
            }

        }


        private void displayJobInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayJobInfo();
        }


        private void DoMenuDisplayJobInfo()
        {
            List<IJob> SelectedJobs = ReturnSelectedJobs();
            if (SelectedJobs.Count == 1)
            {
                IJob JobToDisplayP = SelectedJobs.FirstOrDefault();

                // Refresh the job.
                _context = Program.ConnectAndGetNewContext(_credentials);
                IJob JobToDisplayP2 = _context.Jobs.Where(j => j.Id == JobToDisplayP.Id).FirstOrDefault();

                if (JobToDisplayP2 != null)
                {
                    if (DisplayInfo(JobToDisplayP2) == DialogResult.OK)
                    {
                    }
                }
            }
        }


        public int DoGridTransferAddItem(string text, TransferType TType, bool PutInTheQueue)
        {
            TransferEntry myTE = new TransferEntry()
                {
                    Name = text,
                    SubmitTime = DateTime.Now,
                    Type = TType
                };

            dataGridViewTransfer.Invoke(new Action(() =>
            {
                _MyListTransfer.Add(myTE);

            }
                ));
            int indexloc = _MyListTransfer.IndexOf(myTE);

            if (PutInTheQueue)
            {
                _MyListTransferQueue.Add(indexloc);
                myTE.processedinqueue = true;
                myTE.State = TransferState.Queued;

            }
            else
            {
                myTE.processedinqueue = false;
                myTE.State = TransferState.Processing;
                myTE.StartTime = DateTime.Now;
            }

            // refresh number in tab
            tabPageTransfers.Invoke(new Action(() => tabPageTransfers.Text = string.Format(Constants.TabTransfers + " ({0})", _MyListTransfer.Count())));
            return indexloc;
        }



        private void DoMenuImportFromAzureStorage()
        {
            string valuekey = "";
            string targetAssetID = "";


            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count > 0) targetAssetID = SelectedAssets.FirstOrDefault().Id;

            if (!havestoragecredentials)
            { // No blob credentials. Let's ask the user
                if (InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + _context.DefaultStorageAccount.Name + ":", ref valuekey) == DialogResult.OK)
                {
                    _credentials.StorageKey = valuekey;
                    havestoragecredentials = true;
                }
            }
            if (havestoragecredentials) // if we have the storage credentials
            {
                ImportFromAzureStorage form = new ImportFromAzureStorage(_context, _credentials.StorageKey)
                    {
                        ImportLabelDefaultStorageName = _context.DefaultStorageAccount.Name,
                        ImportNewAssetName = "NewAsset Blob_" + Guid.NewGuid(),
                        ImportCreateNewAsset = true
                    };

                if (!string.IsNullOrEmpty(targetAssetID))
                {
                    if (SelectedAssets.FirstOrDefault().Options == AssetCreationOptions.None) // Ok, the selected asset is not encrypyted
                    {
                        form.ImportOptionToCopyFilesToExistingAsset = true;
                        form.ImportLabelExistingAssetName = GetAsset(targetAssetID).Name;
                        form.ImportOptionToCopyFilesToExistingAssetLabel = string.Empty;
                    }
                    else // selected asset is encrypted, so we disable it and display a warning
                    {
                        form.ImportOptionToCopyFilesToExistingAsset = false;
                        form.ImportOptionToCopyFilesToExistingAssetLabel = "(Selected asset seems to be encrypted)";
                    }
                }

                else  // no selected asset so we disable the option to copy file into an existing asset
                {
                    form.ImportOptionToCopyFilesToExistingAsset = false;
                    form.ImportOptionToCopyFilesToExistingAssetLabel = string.Empty;
                }

                if (form.ShowDialog() == DialogResult.OK)
                {
                    int index = DoGridTransferAddItem("Import from Azure Storage " + (form.ImportCreateNewAsset ? "to a new asset" : "to an existing asset"), TransferType.ImportFromAzureStorage, false);
                    // Start a worker thread that does uploading.
                    Task.Factory.StartNew(() => ProcessImportFromAzureStorage(form.ImportUseDefaultStorage, form.SelectedBlobContainer, form.ImporOtherStorageName, form.ImportOtherStorageKey, form.SelectedBlobs, form.ImportCreateNewAsset, form.ImportNewAssetName, targetAssetID, index));
                    DotabControlMainSwitch(Constants.TabTransfers);
                    DoRefreshGridAssetV(false);
                }
            }
        }


        private void ProcessImportFromAzureStorage(bool UseDefaultStorage, string containername, string otherstoragename, string otherstoragekey, List<IListBlobItem> SelectedBlobs, bool CreateNewAsset, string newassetname, string targetAssetID, int index)
        {
            IAsset asset;
            if (CreateNewAsset)
            {
                // Create a new asset.
                asset = _context.Assets.Create(newassetname, AssetCreationOptions.None);
            }
            else //copy files in an existing asset
            {
                asset = GetAsset(targetAssetID);

            }
            if (UseDefaultStorage) // The default storage is used
            {
                CloudStorageAccount storageAccount;
                storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), true);


                var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                var mediaBlobContainer = cloudBlobClient.GetContainerReference(containername);

                TextBoxLogWriteLine("Starting the blob copy process.");

                IAccessPolicy writePolicy = _context.AccessPolicies.Create("writePolicy", TimeSpan.FromDays(1), AccessPermissions.Write);
                ILocator destinationLocator = _context.Locators.CreateLocator(LocatorType.Sas, asset, writePolicy);

                // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                Uri uploadUri = new Uri(destinationLocator.Path);
                string assetTargetContainerName = uploadUri.Segments[1];
                CloudBlobContainer assetTargetContainer = cloudBlobClient.GetContainerReference(assetTargetContainerName);


                CloudBlockBlob sourceCloudBlob, destinationBlob;
                long Length = 0;
                long BytesCopied = 0;
                string fileName;
                double percentComplete;
                bool Error = false;

                //calculate size
                foreach (var sourceBlob1 in SelectedBlobs)
                {
                    fileName = HttpUtility.UrlDecode(Path.GetFileName(sourceBlob1.Uri.AbsoluteUri));

                    sourceCloudBlob = mediaBlobContainer.GetBlockBlobReference(fileName);
                    sourceCloudBlob.FetchAttributes();

                    Length += sourceCloudBlob.Properties.Length;
                }

                // do the copy
                int nbblob = 0;
                foreach (var sourceBlob in SelectedBlobs)
                {
                    nbblob++;
                    fileName = HttpUtility.UrlDecode(Path.GetFileName(sourceBlob.Uri.AbsoluteUri));

                    sourceCloudBlob = mediaBlobContainer.GetBlockBlobReference(fileName);
                    sourceCloudBlob.FetchAttributes();

                    if (sourceCloudBlob.Properties.Length > 0)
                    {

                        try
                        {
                            IAssetFile assetFile = asset.AssetFiles.Create(fileName);
                            destinationBlob = assetTargetContainer.GetBlockBlobReference(fileName);

                            destinationBlob.DeleteIfExists();
                            destinationBlob.StartCopyFromBlob(sourceCloudBlob);

                            CloudBlockBlob blob;
                            blob = (CloudBlockBlob)assetTargetContainer.GetBlobReferenceFromServer(fileName);

                            while (blob.CopyState.Status == CopyStatus.Pending)
                            {
                                Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                blob = (CloudBlockBlob)assetTargetContainer.GetBlobReferenceFromServer(fileName);
                                percentComplete = (Convert.ToDouble(nbblob) / Convert.ToDouble(SelectedBlobs.Count)) * 100d * (long)(BytesCopied + blob.CopyState.BytesCopied) / Length;
                                DoGridTransferUpdateProgress(percentComplete, index);

                            }

                            if (blob.CopyState.Status == CopyStatus.Failed)
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'.", fileName, true);
                                TextBoxLogWriteLine("({0})", blob.CopyState.StatusDescription, true);
                                DoGridTransferDeclareError(index);
                                Error = true;
                                break;
                            }

                            destinationBlob.FetchAttributes();
                            assetFile.ContentFileSize = sourceCloudBlob.Properties.Length;
                            assetFile.Update();

                            if (sourceCloudBlob.Properties.Length != destinationBlob.Properties.Length)
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'", fileName, true);
                                DoGridTransferDeclareError(index);
                                Error = true;
                                break;
                            }


                        }
                        catch
                        {
                            TextBoxLogWriteLine("Failed to copy file '{0}'!.", fileName, true);
                            DoGridTransferDeclareError(index);
                            Error = true;
                        }


                        BytesCopied += sourceCloudBlob.Properties.Length;
                        percentComplete = (long)100 * (long)BytesCopied / (long)Length;
                        if (!Error) DoGridTransferUpdateProgress(percentComplete, index);

                    }
                }

                asset.Update();

                destinationLocator.Delete();
                writePolicy.Delete();

                // Refresh the asset.
                asset = _context.Assets.Where(a => a.Id == asset.Id).FirstOrDefault();
                SetISMFileAsPrimary(asset);
                if (!Error)
                {
                    TextBoxLogWriteLine("Azure Storage copy completed.");
                    DoGridTransferDeclareCompleted(index, asset.Id);
                }
                DoRefreshGridAssetV(false);

            }
            else // Use another storage account
            {
                // Create Media Services context.

                var externalStorageAccount = new CloudStorageAccount(new StorageCredentials(otherstoragename, otherstoragekey), true);
                var externalCloudBlobClient = externalStorageAccount.CreateCloudBlobClient();
                var externalMediaBlobContainer = externalCloudBlobClient.GetContainerReference(containername);

                TextBoxLogWriteLine("Starting the Azure Storage copy process.");

                externalMediaBlobContainer.CreateIfNotExists();

                // Get the SAS token to use for all blobs if dealing with multiple accounts
                string blobToken = externalMediaBlobContainer.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                {
                    // Specify the expiration time for the signature.
                    SharedAccessExpiryTime = DateTime.Now.AddDays(1),
                    // Specify the permissions granted by the signature.
                    Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Read
                });

                IAccessPolicy writePolicy = _context.AccessPolicies.Create("writePolicy",
                  TimeSpan.FromDays(1), AccessPermissions.Write);
                ILocator destinationLocator = _context.Locators.CreateLocator(LocatorType.Sas, asset, writePolicy);

                var destinationStorageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), true);
                var destBlobStorage = destinationStorageAccount.CreateCloudBlobClient();

                // Get the asset container URI and Blob copy from mediaContainer to assetContainer.
                string destinationContainerName = (new Uri(destinationLocator.Path)).Segments[1];

                CloudBlobContainer assetContainer =
                    destBlobStorage.GetContainerReference(destinationContainerName);

                CloudBlockBlob sourceCloudBlob, destinationBlob;
                long Length = 0;
                long BytesCopied = 0;
                string fileName;
                double percentComplete;

                //calculate size
                foreach (var sourceBlob in SelectedBlobs)
                {
                    fileName = HttpUtility.UrlDecode(Path.GetFileName(sourceBlob.Uri.AbsoluteUri));

                    sourceCloudBlob = externalMediaBlobContainer.GetBlockBlobReference(fileName);
                    sourceCloudBlob.FetchAttributes();

                    Length += sourceCloudBlob.Properties.Length;
                }


                // do the copy
                int nbblob = 0;
                bool Error = false;
                foreach (var sourceBlob in SelectedBlobs)
                {
                    nbblob++;
                    fileName = HttpUtility.UrlDecode(Path.GetFileName(sourceBlob.Uri.AbsoluteUri));

                    sourceCloudBlob = externalMediaBlobContainer.GetBlockBlobReference(fileName);
                    sourceCloudBlob.FetchAttributes();

                    if (sourceCloudBlob.Properties.Length > 0)
                    {
                        try
                        {
                            IAssetFile assetFile = asset.AssetFiles.Create(fileName);
                            destinationBlob = assetContainer.GetBlockBlobReference(fileName);

                            destinationBlob.DeleteIfExists();
                            destinationBlob.StartCopyFromBlob(new Uri(sourceBlob.Uri.AbsoluteUri + blobToken));


                            CloudBlockBlob blob;
                            blob = (CloudBlockBlob)assetContainer.GetBlobReferenceFromServer(fileName);

                            while (blob.CopyState.Status == CopyStatus.Pending)
                            {
                                Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                blob = (CloudBlockBlob)assetContainer.GetBlobReferenceFromServer(fileName);
                                percentComplete = (Convert.ToDouble(nbblob) / Convert.ToDouble(SelectedBlobs.Count)) * 100d * (long)(BytesCopied + blob.CopyState.BytesCopied) / (long)Length;
                                DoGridTransferUpdateProgress(percentComplete, index);
                            }

                            if (blob.CopyState.Status == CopyStatus.Failed)
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'.", fileName, true);
                                TextBoxLogWriteLine("({0})", blob.CopyState.StatusDescription, true);
                                DoGridTransferDeclareError(index);
                                Error = true;
                                break;
                            }

                            destinationBlob.FetchAttributes();

                            assetFile.ContentFileSize = sourceCloudBlob.Properties.Length;
                            assetFile.Update();
                        }
                        catch
                        {
                            TextBoxLogWriteLine("Failed to copy '{0}'", fileName, true);
                            DoGridTransferDeclareError(index);
                            Error = true;
                            break;

                        }
                        BytesCopied += sourceCloudBlob.Properties.Length;
                        percentComplete = 100d * BytesCopied / Length;
                        if (!Error) DoGridTransferUpdateProgress(percentComplete, index);

                    }
                }

                asset.Update();
                destinationLocator.Delete();
                writePolicy.Delete();

                // Refresh the asset.
                asset = _context.Assets.Where(a => a.Id == asset.Id).FirstOrDefault();
                SetISMFileAsPrimary(asset);

                if (!Error)
                {
                    DoGridTransferDeclareCompleted(index, asset.Id);
                    TextBoxLogWriteLine("Azure Storage copy completed.");
                }
                DoRefreshGridAssetV(false);

            }
        }


        private void ProcessExportAssetToAzureStorage(bool UseDefaultStorage, string containername, string otherstoragename, string otherstoragekey, List<IAssetFile> SelectedFiles, bool CreateNewContainer, int index)
        {

            bool Error = false;
            if (UseDefaultStorage) // The default storage is used
            {
                TextBoxLogWriteLine("Starting the Azure export process.");

                // let's get cloudblobcontainer for source
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), true);
                var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                IAccessPolicy readpolicy = _context.AccessPolicies.Create("readpolicy", TimeSpan.FromDays(1), AccessPermissions.Read);
                ILocator sourcelocator = _context.Locators.CreateLocator(LocatorType.Sas, SelectedFiles[0].Asset, readpolicy);

                // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                Uri sourceUri = new Uri(sourcelocator.Path);
                CloudBlobContainer assetSourceContainer = cloudBlobClient.GetContainerReference(sourceUri.Segments[1]);

                // let's get cloudblobcontainer for target
                CloudBlobContainer TargetContainer = cloudBlobClient.GetContainerReference(containername); ;

                if (CreateNewContainer)
                {
                    try
                    {
                        TargetContainer.CreateIfNotExists();
                    }
                    catch
                    {
                        TextBoxLogWriteLine("Failed to create container '{0}'", TargetContainer.Name, true);
                        DoGridTransferDeclareError(index);
                        Error = true;
                    }
                }

                if (!Error)
                {
                    Error = false;
                    CloudBlockBlob sourceCloudBlob, destinationBlob;
                    long Length = 0;
                    long BytesCopied = 0;
                    long percentComplete;

                    //calculate size
                    foreach (IAssetFile file in SelectedFiles)
                    {
                        Length += file.ContentFileSize;
                    }

                    // do the copy
                    int nbblob = 0;
                    foreach (IAssetFile file in SelectedFiles)
                    {
                        nbblob++;
                        sourceCloudBlob = assetSourceContainer.GetBlockBlobReference(file.Name);
                        sourceCloudBlob.FetchAttributes();

                        if (sourceCloudBlob.Properties.Length > 0)
                        {
                            DoGridTransferUpdateProgress(100d * nbblob / SelectedFiles.Count, index);
                            try
                            {
                                destinationBlob = TargetContainer.GetBlockBlobReference(file.Name);
                                destinationBlob.DeleteIfExists();
                                destinationBlob.StartCopyFromBlob(sourceCloudBlob);

                                CloudBlockBlob blob;
                                blob = (CloudBlockBlob)TargetContainer.GetBlobReferenceFromServer(file.Name);

                                while (blob.CopyState.Status == CopyStatus.Pending)
                                {
                                    Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                    blob = (CloudBlockBlob)TargetContainer.GetBlobReferenceFromServer(file.Name);
                                    percentComplete = (long)100 * (long)(BytesCopied + blob.CopyState.BytesCopied) / (long)Length;
                                    DoGridTransferUpdateProgress((int)percentComplete, index);

                                }

                                if (blob.CopyState.Status == CopyStatus.Failed)
                                {
                                    TextBoxLogWriteLine("Failed to copy '{0}'", file.Name, true);
                                    TextBoxLogWriteLine("({0})", blob.CopyState.StatusDescription, true);
                                    DoGridTransferDeclareError(index);
                                    Error = true;
                                    break;
                                }

                                destinationBlob.FetchAttributes();

                                if (sourceCloudBlob.Properties.Length != destinationBlob.Properties.Length)
                                {
                                    TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
                                    DoGridTransferDeclareError(index);
                                    Error = true;
                                    break;
                                }
                            }
                            catch
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
                                DoGridTransferDeclareError(index);
                                Error = true;
                            }
                            BytesCopied += sourceCloudBlob.Properties.Length;
                            percentComplete = (long)100 * (long)BytesCopied / (long)Length;
                            if (!Error) DoGridTransferUpdateProgress((int)percentComplete, index);

                        }
                    }

                    sourcelocator.Delete();


                    if (!Error)
                    {
                        TextBoxLogWriteLine("Blob copy completed.");
                        DoGridTransferDeclareCompleted(index, TargetContainer.Uri.ToString());
                    }
                    DoRefreshGridAssetV(false);
                }
            }
            else // Another storage is used
            {
                TextBoxLogWriteLine("Starting the blob copy process.");

                // let's get cloudblobcontainer for source
                CloudStorageAccount SourceStorageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), true);
                CloudStorageAccount TargetStorageAccount = new CloudStorageAccount(new StorageCredentials(otherstoragename, otherstoragekey), true);

                var SourceCloudBlobClient = SourceStorageAccount.CreateCloudBlobClient();
                var TargetCloudBlobClient = TargetStorageAccount.CreateCloudBlobClient();
                IAccessPolicy readpolicy = _context.AccessPolicies.Create("readpolicy", TimeSpan.FromDays(1), AccessPermissions.Read);
                ILocator sourcelocator = _context.Locators.CreateLocator(LocatorType.Sas, SelectedFiles[0].Asset, readpolicy);

                // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                Uri sourceUri = new Uri(sourcelocator.Path);
                CloudBlobContainer assetSourceContainer = SourceCloudBlobClient.GetContainerReference(sourceUri.Segments[1]);

                // let's get cloudblobcontainer for target
                CloudBlobContainer TargetContainer = TargetCloudBlobClient.GetContainerReference(containername);

                // Get the SAS token to use for all blobs if dealing with multiple accounts
                string blobToken = assetSourceContainer.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                {
                    // Specify the expiration time for the signature.
                    SharedAccessExpiryTime = DateTime.Now.AddDays(1),
                    // Specify the permissions granted by the signature.
                    Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Read
                });
                if (CreateNewContainer)
                {
                    try
                    {
                        TargetContainer.CreateIfNotExists();
                    }
                    catch
                    {
                        TextBoxLogWriteLine("Failed to create container '{0}' ", TargetContainer.Name, true);
                        DoGridTransferDeclareError(index);
                        Error = true;
                    }
                }

                if (!Error)
                {
                    CloudBlockBlob sourceCloudBlob, destinationBlob;
                    long Length = 0;
                    long BytesCopied = 0;
                    double percentComplete;
                    Error = false;

                    //calculate size
                    foreach (IAssetFile file in SelectedFiles)
                    {
                        Length += file.ContentFileSize;
                    }


                    // do the copy
                    int nbblob = 0;
                    foreach (IAssetFile file in SelectedFiles)
                    {
                        nbblob++;
                        sourceCloudBlob = assetSourceContainer.GetBlockBlobReference(file.Name);
                        sourceCloudBlob.FetchAttributes();

                        if (sourceCloudBlob.Properties.Length > 0)
                        {
                            DoGridTransferUpdateProgress(100d * nbblob / SelectedFiles.Count, index);
                            try
                            {
                                destinationBlob = TargetContainer.GetBlockBlobReference(file.Name);
                                destinationBlob.DeleteIfExists();
                                destinationBlob.StartCopyFromBlob(new Uri(sourceCloudBlob.Uri.AbsoluteUri + blobToken));

                                CloudBlockBlob blob;
                                blob = (CloudBlockBlob)TargetContainer.GetBlobReferenceFromServer(file.Name);

                                while (blob.CopyState.Status == CopyStatus.Pending)
                                {
                                    Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                    blob = (CloudBlockBlob)TargetContainer.GetBlobReferenceFromServer(file.Name);
                                    percentComplete = 100d * (long)(BytesCopied + blob.CopyState.BytesCopied) / Length;
                                    DoGridTransferUpdateProgress(percentComplete, index);

                                }

                                if (blob.CopyState.Status == CopyStatus.Failed)
                                {
                                    TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
                                    TextBoxLogWriteLine("({0})", blob.CopyState.StatusDescription, true);
                                    DoGridTransferDeclareError(index);
                                    Error = true;
                                    break;
                                }

                                destinationBlob.FetchAttributes();

                                if (sourceCloudBlob.Properties.Length != destinationBlob.Properties.Length)
                                {
                                    TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
                                    DoGridTransferDeclareError(index);
                                    Error = true;
                                    break;
                                }
                            }
                            catch
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
                                DoGridTransferDeclareError(index);
                                Error = true;
                            }

                            BytesCopied += sourceCloudBlob.Properties.Length;
                            percentComplete = 100d * BytesCopied / Length;
                            if (!Error) DoGridTransferUpdateProgress(percentComplete, index);
                        }
                    }
                    sourcelocator.Delete();


                    if (!Error)
                    {
                        TextBoxLogWriteLine("Blob copy completed.");
                        DoGridTransferDeclareCompleted(index, TargetContainer.Uri.ToString());
                    }
                    DoRefreshGridAssetV(false);
                }
            }
        }

        private void allJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to delete ALL the jobs ?" + Constants.endline + "There are " + _context.Jobs.Count().ToString() + " jobs in the account.", "Jobs deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                // Set cursor as hourglass
                this.Cursor = Cursors.WaitCursor;

                foreach (IJob job in _context.Jobs)
                {
                    try { job.Delete(); }

                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine("Error when deleting job '{0}'", job.Name, true);
                        TextBoxLogWriteLine(ex.Message, true);
                    }
                }
                System.Threading.Thread.Sleep(1000);
                TextBoxLogWriteLine("Jobs deleted.");
                DoRefreshGridJobV(false);
                // Set cursor as default arrow
                this.Cursor = Cursors.Default;
            }
        }

        private void selectedJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteSelectedJobs();
        }

        private void DoDeleteSelectedJobs()
        {
            List<IJob> SelectedJobs = ReturnSelectedJobs();

            if (SelectedJobs.Count > 0)
            {
                string question = "Delete these " + SelectedJobs.Count + " jobs ?";
                if (SelectedJobs.Count == 1) question = "Delete " + SelectedJobs[0].Name + " ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Job(s) deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (IJob JobToDelete in SelectedJobs)

                        if (JobToDelete != null)
                        {
                            //delete
                            TextBoxLogWriteLine("Deleting job '{0}'.", JobToDelete.Name);

                            try
                            {
                                JobToDelete.Delete();
                                TextBoxLogWriteLine("Job deleted.");
                            }

                            catch (Exception ex)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when deleting the job '{0}'", JobToDelete.Name, true);
                                TextBoxLogWriteLine(ex.Message, true);
                            }
                        }
                    DoRefreshGridJobV(false);
                }
            }
        }



        private void silverlightMonitoringPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://smf.cloudapp.net/healthmonitor");
        }

        private void dASHIFHTML5ReferencePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://dashif.org/reference/players/javascript/");
        }

        private void iVXHLSPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://apps.microsoft.com/windows/en-us/app/3ivx-hls-player/f79ce7d0-2993-4658-bc4e-83dc182a0614");
        }


        private void azureManagementPortalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://manage.windowsazure.com");
        }

        private void encodeAssetWithDigitalRapidsKayakCloudEngineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithZenium();
        }

        private void DoMenuEncodeWithZenium()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            foreach (IAsset asset in SelectedAssets) // check that there is no blueprint in the selected assets
            {
                if (IsAZeniumBlueprint(asset))
                {
                    MessageBox.Show("One of the selected asset(s) is a blueprint. Please select only video assets.");
                    return;
                }
            }

            string taskname = "Zenium Encoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvBlueprint;
            List<IAsset> listblueprints = new List<IAsset>();


            var query = _context.Files.Where(f => (f.Name.EndsWith(".xenio") | f.Name.EndsWith(".kayak")));
            foreach (IAssetFile file in query)
            {
                if (file.Asset.AssetFiles.Count() == 1)
                {
                    listblueprints.Add(file.Asset);
                }
            }


            IMediaProcessor processor = GetLatestMediaProcessorByName(Constants.ZeniumEncoder);

            EncodingZenium form = new EncodingZenium()
            {
                EncodingPromptText = (SelectedAssets.Count > 1) ? "Input assets : " + SelectedAssets.Count + " assets have been selected." : "Input asset : '" + SelectedAssets.FirstOrDefault().Name + "' will be encoded.",
                EncodingProcessorName = "Processor: " + processor.Vendor + " / " + processor.Name + " v" + processor.Version,
                EncodingJobName = "Zenium Encoding of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + "-Zenium encoded with " + Constants.NameconvBlueprint,
                EncodingPriority = Properties.Settings.Default.DefaultJobPriority,
                ZeniumBlueprints = listblueprints,
                EncodingMultipleJobs = true,
                EncodingNumberInputAssets = SelectedAssets.Count
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (!form.EncodingMultipleJobs) // ONE job with all input assets
                {
                    string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name);
                    IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);
                    foreach (IAsset graphAsset in form.SelectedZeniumBlueprints) // for each blueprint selected, we create a task
                    {
                        string tasknameloc = taskname.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name).Replace(Constants.NameconvBlueprint, graphAsset.Name);
                        ITask task = job.Tasks.AddNew(
                                    tasknameloc,
                                   processor,
                                   Constants.ZeniumConfig,
                                   Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
                        // Specify the graph asset to be encoded, followed by the input video asset to be used
                        task.InputAssets.Add(graphAsset);
                        task.InputAssets.AddRange(SelectedAssets); // we add all assets
                        string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name).Replace(Constants.NameconvBlueprint, graphAsset.Name);
                        task.OutputAssets.AddNew(outputassetnameloc, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
                    }
                    TextBoxLogWriteLine("Submitting encoding job '{0}'", jobnameloc);
                    // Submit the job and wait until it is completed. 
                    try
                    {
                        job.Submit();
                    }
                    catch (Exception e)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There has been a problem when submitting the job {0}.", jobnameloc, true);
                        TextBoxLogWriteLine(e);
                        return;
                    }
                    dataGridViewJobsV.DoJobProgress(job);

                }
                else // multiple jobs: one job for each input asset
                {
                    foreach (IAsset asset in SelectedAssets)
                    {
                        string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, asset.Name);

                        IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);
                        foreach (IAsset graphAsset in form.SelectedZeniumBlueprints) // for each blueprint selected, we create a task
                        {
                            string tasknameloc = taskname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvBlueprint, graphAsset.Name);

                            ITask task = job.Tasks.AddNew(
                                        tasknameloc,
                                       processor,
                                       Constants.ZeniumConfig,
                                       Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
                            // Specify the graph asset to be encoded, followed by the input video asset to be used
                            task.InputAssets.Add(graphAsset);
                            task.InputAssets.Add(asset); // we add one asset
                            string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvBlueprint, graphAsset.Name);

                            task.OutputAssets.AddNew(outputassetnameloc, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
                        }
                        TextBoxLogWriteLine("Submitting encoding job '{0}'", jobnameloc);
                        // Submit the job and wait until it is completed. 
                        try
                        {
                            job.Submit();
                        }
                        catch (Exception e)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There has been a problem when submitting the job {0}.", jobnameloc, true);
                            TextBoxLogWriteLine(e);
                            return;
                        }
                        dataGridViewJobsV.DoJobProgress(job);
                    }
                }
                DotabControlMainSwitch(Constants.TabJobs);
                DoRefreshGridJobV(false);
            }
        }



        private void DoMenuEncodeWithAMESystemPreset()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            List<IMediaProcessor> Encoders;

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            Encoders = GetMediaProcessorsByName(Constants.AzureMediaEncoder);
            Encoders.AddRange(GetMediaProcessorsByName(Constants.WindowsAzureMediaEncoder));

            EncodingAMEPreset form = new EncodingAMEPreset()
            {
                EncodingOutputAssetName = Constants.NameconvInputasset + "-AME encoded with " + Constants.NameconvAMEpreset,
                Text = "Azure Media Encoding",
                EncodingLabel1 = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected. " + SelectedAssets.Count + " jobs will be submitted." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be encoded.",
                EncodingJobName = "AME Encoding of " + Constants.NameconvInputasset,
                EncodingLabel2 = "Select a encoding profile:",
                EncodingProcessorsList = Encoders,
                EncodingJobPriority = Properties.Settings.Default.DefaultJobPriority
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                string taskname = "AME Encoding of " + Constants.NameconvInputasset + " with " + form.EncodingSelectedPreset;
                string outputassetname = form.EncodingOutputAssetName.Replace(Constants.NameconvAMEpreset, form.EncodingSelectedPreset);

                LaunchJobs(form.EncodingProcessorSelected, SelectedAssets, form.EncodingJobName, form.EncodingJobPriority, taskname, outputassetname, form.EncodingSelectedPreset, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
            }

        }



        private void Mainform_Shown(object sender, EventArgs e)
        {
        }



        private void DoGridTransferInit()
        {
            const string labelProgress = "Progress";

            _MyListTransfer = new BindingList<TransferEntry>();
            _MyListTransferQueue = new List<int>();


            DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn();
            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
            col.Name = labelProgress;
            col.DataPropertyName = labelProgress;
            dataGridViewTransfer.Invoke(new Action(() =>
            {
                dataGridViewTransfer.Columns.Add(col);
            }
            ));

            dataGridViewTransfer.Invoke(new Action(() =>
            {
                dataGridViewTransfer.DataSource = _MyListTransfer;
            }
          ));

            dataGridViewTransfer.Invoke(new Action(() =>
                {
                    dataGridViewTransfer.Columns[labelProgress].DisplayIndex = 3;
                    dataGridViewTransfer.Columns[labelProgress].HeaderText = labelProgress;
                    dataGridViewTransfer.Columns["processedinqueue"].Visible = false;
                }
          ));
        }

        private void oSMFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1) Set the src to MPEG-DASH or Smooth Streaming source" + Constants.endline + "2) Select 'Microsoft Adaptive Streaming Plugin'" + Constants.endline + "3) Click 'Preview and Update'");
            System.Diagnostics.Process ieProcess = System.Diagnostics.Process.Start("iexplore", @"http://wamsclient.cloudapp.net/release/setup.html");
        }


        /// <summary>
        /// Updates your configuration .xml file dynamically.
        /// </summary>
        /// <param name="licenseAcquisitionUrl">The URL of your 
        ///        license acquisition server. For example:
        ///        "http://playready.directtaps.net/pr/svc/rightsmanager.asmx"
        /// </param>
        public static string LoadAndUpdatePlayReadyConfiguration(string xmlFileName, string keyseed, string licenseAcquisitionUrlstr, Guid keyId, string contentkey, bool useSencBox, bool adjustSubSamples, string serviceid, string customattributes)
        {
            Uri keyDeliveryServiceUri = null;
            if (!string.IsNullOrEmpty(licenseAcquisitionUrlstr)) keyDeliveryServiceUri = new Uri(licenseAcquisitionUrlstr);

            XNamespace xmlns = "http://schemas.microsoft.com/iis/media/v4/TM/TaskDefinition#";

            // Prepare the encryption task template
            XDocument doc = XDocument.Load(xmlFileName);

            var keyseedEl = doc
                 .Descendants(xmlns + "property")
                 .Where(p => p.Attribute("name").Value == "keySeedValue")
                 .FirstOrDefault();
            var licenseAcquisitionUrlEl = doc
                    .Descendants(xmlns + "property")
                    .Where(p => p.Attribute("name").Value == "licenseAcquisitionUrl")
                    .FirstOrDefault();
            var contentKeyEl = doc
                    .Descendants(xmlns + "property")
                    .Where(p => p.Attribute("name").Value == "contentKey")
                    .FirstOrDefault();
            var keyIdEl = doc
                    .Descendants(xmlns + "property")
                    .Where(p => p.Attribute("name").Value == "keyId")
                    .FirstOrDefault();
            var useSencBoxEl = doc
                   .Descendants(xmlns + "property")
                   .Where(p => p.Attribute("name").Value == "useSencBox")
                   .FirstOrDefault();
            var adjustSubSamplesEl = doc
                   .Descendants(xmlns + "property")
                   .Where(p => p.Attribute("name").Value == "adjustSubSamples")
                   .FirstOrDefault();

            var serviceIdEl = doc
                   .Descendants(xmlns + "property")
                   .Where(p => p.Attribute("name").Value == "serviceId")
                   .FirstOrDefault();

            var customAttributesEl = doc
                   .Descendants(xmlns + "property")
                   .Where(p => p.Attribute("name").Value == "customAttributes")
                   .FirstOrDefault();


            // Update the "value" property.

            if (keyseed != null)
                keyseedEl.Attribute("value").SetValue(keyseed);

            if (licenseAcquisitionUrlstr != null && keyDeliveryServiceUri != null)
                licenseAcquisitionUrlEl.Attribute("value").SetValue(keyDeliveryServiceUri);

            if (contentkey != null)
                contentKeyEl.Attribute("value").SetValue(contentkey);

            if (keyId != null)
                keyIdEl.Attribute("value").SetValue(keyId);

            if (useSencBoxEl != null)
                useSencBoxEl.Attribute("value").SetValue(useSencBox.ToString());

            if (adjustSubSamplesEl != null)
                adjustSubSamplesEl.Attribute("value").SetValue(adjustSubSamples.ToString());

            if (serviceIdEl != null)
                serviceIdEl.Attribute("value").SetValue(serviceid.ToString());

            if (customAttributesEl != null)
                customAttributesEl.Attribute("value").SetValue(customattributes.ToString());

            return doc.ToString();
        }



        public static string LoadAndUpdateHLSConfiguration(string xmlFileName, bool encrypt, string key, string keyuri, string maxbitrate, string segment)
        {
            XNamespace xmlns = "http://schemas.microsoft.com/iis/media/v4/TM/TaskDefinition#";

            // Prepare the encryption task template
            XDocument doc = XDocument.Load(xmlFileName);

            var encryptEl = doc
                .Descendants(xmlns + "property")
                .Where(p => p.Attribute("name").Value == "encrypt")
                .FirstOrDefault();
            var keyEl = doc
                 .Descendants(xmlns + "property")
                 .Where(p => p.Attribute("name").Value == "key")
                 .FirstOrDefault();
            var keyuriEl = doc
                    .Descendants(xmlns + "property")
                    .Where(p => p.Attribute("name").Value == "keyuri")
                    .FirstOrDefault();
            var maxbitrateEl = doc
                    .Descendants(xmlns + "property")
                    .Where(p => p.Attribute("name").Value == "maxbitrate")
                    .FirstOrDefault();
            var segmentEl = doc
                    .Descendants(xmlns + "property")
                    .Where(p => p.Attribute("name").Value == "segment")
                    .FirstOrDefault();

            // Update the "value" property.
            if (maxbitrateEl != null)
                maxbitrateEl.Attribute("value").SetValue(maxbitrate);

            if (segmentEl != null)
                segmentEl.Attribute("value").SetValue(segment);

            if (encryptEl != null)
                encryptEl.Attribute("value").SetValue(encrypt.ToString());

            if (encrypt)
            {
                if (!string.IsNullOrEmpty(keyuri))
                {
                    Uri keyurluri = new Uri(keyuri);
                    if (keyuriEl != null)
                        keyuriEl.Attribute("value").SetValue(keyurluri);
                }

                if (keyEl != null)
                    keyEl.Attribute("value").SetValue(key);
            }
            return doc.ToString();
        }

        public static string LoadAndUpdateThumbnailsConfiguration(string xmlFileName, string ThumbnailsSize, string ThumbnailsType, string ThumbnailsFileName, string ThumbnailsTimeValue, string ThumbnailsTimeStep, string ThumbnailsTimeStop)
        {
            // Prepare the encryption task template
            XDocument doc = XDocument.Load(xmlFileName);

            var ThumbnailEl = doc.Element("Thumbnail");
            var TimeEl = ThumbnailEl.Element("Time");

            ThumbnailEl.Attribute("Size").SetValue(ThumbnailsSize);
            ThumbnailEl.Attribute("Type").SetValue(ThumbnailsType);
            ThumbnailEl.Attribute("Filename").SetValue(ThumbnailsFileName);

            TimeEl.Attribute("Value").SetValue(ThumbnailsTimeValue);
            TimeEl.Attribute("Step").SetValue(ThumbnailsTimeStep);
            if (ThumbnailsTimeStop != string.Empty)
                TimeEl.Add(new XAttribute("Stop", ThumbnailsTimeStop));

            return doc.ToString();
        }

        public static string LoadAndUpdateIndexerConfiguration(string xmlFileName, string AssetTitle, string AssetDescription)
        {
            // Prepare the encryption task template
            XDocument doc = XDocument.Load(xmlFileName);

            var inputxml = doc.Element("configuration").Element("input");

            if (!string.IsNullOrEmpty(AssetTitle)) inputxml.Add(new XElement("metadata", new XAttribute("key", "title"), new XAttribute("value", AssetTitle)));
            if (!string.IsNullOrEmpty(AssetDescription)) inputxml.Add(new XElement("metadata", new XAttribute("key", "description"), new XAttribute("value", AssetDescription)));

            return doc.ToString();
        }

        /// <summary>
        /// Converts Smooth Stream to HLS.
        /// </summary>
        /// <param name="job">The job to which to add the new task.</param>
        /// <param name="asset">The Smooth Stream asset.</param>
        /// <param name="encrypt">
        /// If you want to encrypt the HLS to HLS with AES - 128, set the encrypt to true.
        /// The smoothStreamAsset parameter must contain a clear Smooth Stream.
        /// 
        /// If you want to encrypt the HLS to HLS with PlayReady, set the encrypt to false.
        /// The smoothStreamAsset parameter must contain Smooth Stream encrypted with PlayReady.
        /// </param>
        /// <returns>The asset that was packaged to HLS.</returns>


        private void packageSmoothStreamingTOHLSstaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuPackageSmoothToHLSStatic();

        }

        private void DoMenuPackageSmoothToHLSStatic()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            IAsset mediaAsset = SelectedAssets.FirstOrDefault();
            if (mediaAsset == null) return;

            if (!SelectedAssets.All(a => a.AssetType == AssetType.SmoothStreaming))
            {
                MessageBox.Show("Asset(s) should be in Smooth Streaming format.", "Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string jobname = "Smooth to " + Constants.NameconvFormathls + " packaging of " + Constants.NameconvInputasset;
            string taskname = "Smooth to " + Constants.NameconvFormathls + " packaging of " + Constants.NameconvInputasset;

            // Get the SDK extension method to  get a reference to the Azure Media Packager.
            IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                   MediaProcessorNames.WindowsAzureMediaPackager);

            HLSAESStatic form = new HLSAESStatic()
                {
                    HLSEncrypt = false,
                    HLSMaxBitrate = "6600000",
                    HLSServiceSegment = "10",
                    HLSKey = string.Empty,
                    HLSKeyURL = string.Empty,
                    HLSProcessorLabel = "Processor: " + processor.Vendor + " / " + processor.Name + " v" + processor.Version,
                    HLSLabel = (SelectedAssets.Count > 1) ? "Batch mode: " + SelectedAssets.Count + " assets have been selected." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be packaged to HLS as a new asset",
                    HLSOutputAssetName = Constants.NameconvInputasset + "-Packaged to " + Constants.NameconvFormathls
                };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Read and update the configuration XML.
                //
                string configHLS = LoadAndUpdateHLSConfiguration(Path.Combine(_configurationXMLFiles, @"MediaPackager_SmoothToHLS.xml"),
                form.HLSEncrypt, form.HLSKey, form.HLSKeyURL, form.HLSMaxBitrate, form.HLSServiceSegment);
                jobname = jobname.Replace(Constants.NameconvFormathls, form.HLSEncrypt ? "HLS/AES" : "HLS");
                taskname = taskname.Replace(Constants.NameconvFormathls, form.HLSEncrypt ? "HLS/AES" : "HLS");
                string outputassetname = form.HLSOutputAssetName.Replace(Constants.NameconvFormathls, form.HLSEncrypt ? "HLS/AES" : "HLS");
                LaunchJobs(processor, SelectedAssets, jobname, taskname, outputassetname, configHLS, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
            }
        }

        private void packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuMP4ToSmoothStatic();
        }

        private void DoMenuMP4ToSmoothStatic()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }
            IAsset mediaAsset = SelectedAssets.FirstOrDefault();
            if (mediaAsset == null) return;

            if (!SelectedAssets.All(a => a.AssetType == AssetType.MultiBitrateMP4))
            {
                MessageBox.Show("Asset(s) should be in multi bitrate MP4 format.", "Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            string labeldb = "Package '" + mediaAsset.Name + "' to Smooth ?";

            if (SelectedAssets.Count > 1)
            {
                labeldb = "Package these " + SelectedAssets.Count + " assets to Smooth Streaming?";
            }

            string jobname = "MP4 to Smooth Packaging of " + Constants.NameconvInputasset;
            string taskname = "MP4 to Smooth Packaging of " + Constants.NameconvInputasset;
            string outputassetname = Constants.NameconvInputasset + "-Packaged to Smooth";

            if (System.Windows.Forms.MessageBox.Show(labeldb, "Multi MP4 to Smooth", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {

                // Get the SDK extension method to  get a reference to the Windows Azure Media Packager.
                IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                    MediaProcessorNames.WindowsAzureMediaPackager);

                // Windows Azure Media Packager does not accept string presets, so load xml configuration
                string smoothConfig = File.ReadAllText(Path.Combine(
                            _configurationXMLFiles,
                            "MediaPackager_MP4toSmooth.xml"));

                LaunchJobs(processor, SelectedAssets, jobname, taskname, outputassetname, smoothConfig, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
            }
        }

        private void encryptWithPlayReadystaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuProtectWithPlayReadyStatic();
        }



        private void DoMenuProtectWithPlayReadyStatic()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;

            }
            if (SelectedAssets.FirstOrDefault() == null) return;

            if (!SelectedAssets.All(a => a.AssetType == AssetType.SmoothStreaming))
            {
                MessageBox.Show("Asset(s) should be in Smooth Streaming format.", "Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            string jobname = "PlayReady Encryption of " + Constants.NameconvInputasset;
            string taskname = "PlayReady Encryption of " + Constants.NameconvInputasset;

            // Get the SDK extension method to  get a reference to the Windows Azure Media Encryptor.
            IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                MediaProcessorNames.WindowsAzureMediaEncryptor);

            PlayReadyStaticEnc form = new PlayReadyStaticEnc()
            {
                PlayReadyProcessorName = "Processor: " + processor.Vendor + " / " + processor.Name + " v" + processor.Version,
                PlayReadyKeyId = Guid.NewGuid(),
                PlayReadyKeySeed = string.Empty,
                PlayReadyLAurl = string.Empty,
                PlayReadyUseSencBox = true,
                PlayReadyAdjustSubSamples = true,
                PlayReadyContentKey = string.Empty,
                PlayReadyServiceId = string.Empty,
                PlayReadyCustomAttributes = string.Empty,
                PlayReadyOutputAssetName = Constants.NameconvInputasset + "-PlayReady protected",
                PlayReadyAssetName = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected as an input. " + SelectedAssets.Count + " jobs will be submitted." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be encrypted with PlayReady."
            };
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bool Error = false;
                IContentKey contentKey;
                string keyDeliveryServiceUri = form.PlayReadyLAurl;
                if (form.PlayReadyConfigureLicenseDelivery)
                {
                    PlayReadyLicense formPlayReady = new PlayReadyLicense();
                    if (formPlayReady.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            contentKey = DynamicEncryption.ConfigureKeyDeliveryServiceForPlayReady(
                                _context,
                                form.PlayReadyKeyId,
                                Convert.FromBase64String(form.PlayReadyContentKey),
                                formPlayReady.GetLicenseTemplate,
                                form.GetPlayReadyKeyRestrictionType,
                                formPlayReady.PlayReadyPolicyName
                                );

                            keyDeliveryServiceUri = contentKey.GetKeyDeliveryUrl(ContentKeyDeliveryType.PlayReadyLicense).ToString();
                            TextBoxLogWriteLine("PlayReady license delivery configured. License Acquisition URL is {0}.", keyDeliveryServiceUri);
                        }

                        catch (Exception e)
                        {
                            TextBoxLogWriteLine("Error when configuring PlayReady license delivery.", true);
                            TextBoxLogWriteLine(e);
                            Error = true;
                        }
                    }
                    else
                    {
                        return;
                    }



                }
                if (!Error)
                {
                    // Read and update the configuration XML.
                    //

                    string configPlayReady = LoadAndUpdatePlayReadyConfiguration(
                    Path.Combine(_configurationXMLFiles, @"MediaEncryptor_PlayReadyProtection.xml"),
                    form.PlayReadyKeySeed,
                    keyDeliveryServiceUri,
                    form.PlayReadyKeyId,
                    form.PlayReadyContentKey,
                    form.PlayReadyUseSencBox,
                    form.PlayReadyAdjustSubSamples,
                    form.PlayReadyServiceId,
                    form.PlayReadyCustomAttributes);

                    LaunchJobs(processor, SelectedAssets, jobname, taskname, form.PlayReadyOutputAssetName, configPlayReady, AssetCreationOptions.CommonEncryptionProtected);
                }

            }

        }
        private void LaunchJobs(IMediaProcessor processor, List<IAsset> selectedassets, string jobname, string taskname, string outputassetname, string configuration, AssetCreationOptions creationoptions)
        {
            LaunchJobs(processor, selectedassets, jobname, Properties.Settings.Default.DefaultJobPriority, taskname, outputassetname, configuration, creationoptions);
        }

        private void LaunchJobs(IMediaProcessor processor, List<IAsset> selectedassets, string jobname, int jobpriority, string taskname, string outputassetname, string configuration, AssetCreationOptions creationoptions)
        {
            foreach (IAsset asset in selectedassets)
            {
                string jobnameloc = jobname.Replace(Constants.NameconvInputasset, asset.Name);
                IJob myJob = _context.Jobs.Create(jobnameloc, jobpriority);
                string tasknameloc = taskname.Replace(Constants.NameconvInputasset, asset.Name);
                ITask myTask = myJob.Tasks.AddNew(
                    tasknameloc,
                   processor,
                   configuration,
                   Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);

                myTask.InputAssets.Add(asset);

                // Add an output asset to contain the results of the job.  
                string outputassetnameloc = outputassetname.Replace(Constants.NameconvInputasset, asset.Name);
                myTask.OutputAssets.AddNew(outputassetnameloc, creationoptions);

                // Submit the job and wait until it is completed. 
                try
                {
                    myJob.Submit();
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There has been a problem when submitting the job {0}.", jobnameloc, true);
                    TextBoxLogWriteLine(e);
                    return;
                }
                TextBoxLogWriteLine("Job '{0}' submitted.", jobnameloc);
                Task.Factory.StartNew(() => dataGridViewJobsV.DoJobProgress(myJob));
            }
            DotabControlMainSwitch(Constants.TabJobs);
            DoRefreshGridJobV(false);
        }

        private void validateMultiMP4AssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuValidateMultiMP4Static();
        }

        private void DoMenuValidateMultiMP4Static()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            IAsset mediaAsset = SelectedAssets.FirstOrDefault();
            if (SelectedAssets.FirstOrDefault() == null) return;

            if (!SelectedAssets.All(a => a.AssetType == AssetType.MultiBitrateMP4))
            {
                MessageBox.Show("Asset(s) should be in multi bitrate MP4 format.", "Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string labeldb = "Validate '" + mediaAsset.Name + "'  ?";

            if (SelectedAssets.Count > 1)
            {
                labeldb = "Launch the validation of these " + SelectedAssets.Count + " assets ?";
            }
            string jobname = "Validate Multi MP4 of " + Constants.NameconvInputasset;
            string taskname = "Validate Multi MP4 of " + Constants.NameconvInputasset;
            string outputassetname = Constants.NameconvInputasset + "-Multi MP4 validated";


            if (System.Windows.Forms.MessageBox.Show(labeldb, "Multi MP4 Validation", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                // Read the task configuration data into a string. 
                string configMp4Validation = File.ReadAllText(Path.Combine(
                        _configurationXMLFiles,
                        "MediaPackager_ValidateTask.xml"));

                // Get the SDK extension method to  get a reference to the Windows Azure Media Packager.
                IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                    MediaProcessorNames.WindowsAzureMediaPackager);

                LaunchJobs(processor, SelectedAssets, jobname, taskname, outputassetname, configMp4Validation, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
            }
        }

        private void DoMenuIndexAssets()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            if (SelectedAssets.FirstOrDefault() == null) return;

            // Get the SDK extension method to  get a reference to the Azure Media Indexer.
            IMediaProcessor processor = GetLatestMediaProcessorByName(Constants.AzureMediaIndexer);

            Indexer form = new Indexer()
            {
                IndexerJobName = "Indexing of " + Constants.NameconvInputasset,
                IndexerOutputAssetName = Constants.NameconvInputasset + "-Indexed",
                IndexerProcessorName = "Processor: " + processor.Vendor + " / " + processor.Name + " v" + processor.Version,
                IndexerJobPriority = Properties.Settings.Default.DefaultJobPriority,
                IndexerInputAssetName = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected for media indexing." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be indexed."
            };

            string taskname = "Indexing of " + Constants.NameconvInputasset;

            if (form.ShowDialog() == DialogResult.OK)
            {
                string configIndexer = string.Empty;

                if (!string.IsNullOrEmpty(form.IndexerTitle) | !string.IsNullOrEmpty(form.IndexerDescription))
                {
                    configIndexer = LoadAndUpdateIndexerConfiguration(
               Path.Combine(_configurationXMLFiles, @"MediaIndexer.xml"),
               form.IndexerTitle,
               form.IndexerDescription
               );
                }

                LaunchJobs(processor, SelectedAssets, form.IndexerJobName, form.IndexerJobPriority, taskname, form.IndexerOutputAssetName, configIndexer, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
            }
        }

        private void decryptAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDecryptAsset();
        }


        private void DoMenuDecryptAsset()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;

            }
            IAsset mediaAsset = SelectedAssets.FirstOrDefault();
            if (mediaAsset == null) return;

            string labeldb = (SelectedAssets.Count > 1) ? "Decrypt these " + SelectedAssets.Count + " assets  ?" : "Decrypt '" + mediaAsset.Name + "'  ?";

            string outputassetname = Constants.NameconvInputasset + "-Storage decrypted";
            string jobname = "Storage Decryption of " + Constants.NameconvInputasset;
            string taskname = "Storage Decryption of " + Constants.NameconvInputasset;

            if (System.Windows.Forms.MessageBox.Show(labeldb, "Asset Decryption", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                // Get the SDK extension method to  get a reference to the Windows Azure Media Packager.
                IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                    MediaProcessorNames.StorageDecryption);

                LaunchJobs(processor, SelectedAssets, jobname, taskname, outputassetname, "", AssetCreationOptions.None);
            }
        }

        private void storageDecryptTheAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDecryptAsset();
        }

        private void azureMediaServicesPlayerPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://amsplayer.azurewebsites.net/player.html");
        }

        private void hTML5VideoElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://www.w3schools.com/html/html5_video.asp");
        }

        private void dynamicPackagingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please create an streaming locator in the Publish menu." + Constants.endline + Constants.endline + "Check that you have, at least, one Streaming endpoint scale Unit" + Constants.endline + "The asset should be:" + Constants.endline + "- a Smooth Streaming asset (Clear or PlayReady protected)," + Constants.endline + "- or a Clear Multi MP4 asset.", "Dynamic Packaging");
        }



        private void Mainform_Load(object sender, EventArgs e)
        {
            toolStripStatusLabelWatchFolder.Visible = false;
            UpdateLabelStorageEncryption();


            comboBoxOrderAssets.Items.AddRange(
           typeof(OrderAssets)
           .GetFields()
           .Select(i => i.GetValue(null) as string)
           .ToArray()
           );
            comboBoxOrderAssets.SelectedIndex = 0;

            comboBoxOrderJobs.Items.AddRange(
            typeof(OrderJobs)
            .GetFields()
            .Select(i => i.GetValue(null) as string)
            .ToArray()
            );
            comboBoxOrderJobs.SelectedIndex = 0;

            comboBoxStateJobs.Items.AddRange(
            typeof(JobState)
            .GetFields()
            .Select(i => i.Name as string)
            .ToArray()
            );
            comboBoxStateJobs.Items[0] = "All";
            comboBoxStateJobs.SelectedIndex = 0;

            comboBoxStateAssets.Items.AddRange(
          typeof(StatusAssets)
          .GetFields()
          .Select(i => i.GetValue(null) as string)
          .ToArray()
          );
            comboBoxStateAssets.SelectedIndex = 0;

            comboBoxFilterAssetsTime.Items.AddRange(
         typeof(FilterTime)
         .GetFields()
         .Select(i => i.GetValue(null) as string)
         .ToArray()
         );
            comboBoxFilterAssetsTime.SelectedIndex = 1; // last week

            comboBoxFilterJobsTime.Items.AddRange(
         typeof(FilterTime)
         .GetFields()
         .Select(i => i.GetValue(null) as string)
         .ToArray()
         );
            comboBoxFilterJobsTime.SelectedIndex = 1; // last week


            comboBoxFilterTimeProgram.Items.AddRange(
       typeof(FilterTime)
       .GetFields()
       .Select(i => i.GetValue(null) as string)
       .ToArray()
       );
            comboBoxFilterTimeProgram.SelectedIndex = 1; // last week

            comboBoxStatusProgram.Items.AddRange(
            typeof(ProgramState)
            .GetFields()
            .Select(i => i.Name as string)
            .ToArray()
            );
            comboBoxStatusProgram.Items[0] = "All";
            comboBoxStatusProgram.SelectedIndex = 0;


            comboBoxOrderProgram.Items.AddRange(
          typeof(OrderPrograms)
          .GetFields()
          .Select(i => i.GetValue(null) as string)
          .ToArray()
          );
            comboBoxOrderProgram.SelectedIndex = 0;


            // Processors tab
            dataGridViewProcessors.ColumnCount = 5;
            dataGridViewProcessors.Columns[0].HeaderText = "Vendor";
            dataGridViewProcessors.Columns[1].HeaderText = "Name";
            dataGridViewProcessors.Columns[2].HeaderText = "Version";
            dataGridViewProcessors.Columns[3].HeaderText = "Id";
            dataGridViewProcessors.Columns[4].HeaderText = "Description";
            dataGridViewProcessors.Columns[0].Width = 110;
            dataGridViewProcessors.Columns[2].Width = 70;

            List<IMediaProcessor> Procs = _context.MediaProcessors.ToList().OrderBy(p => p.Vendor).ThenBy(p => p.Name).ThenBy(p => new Version(p.Version)).ToList();
            foreach (IMediaProcessor proc in Procs)
            {
                dataGridViewProcessors.Rows.Add(proc.Vendor, proc.Name, proc.Version, proc.Id, proc.Description);
            }
            tabPageProcessors.Text = string.Format("Processors ({0})", Procs.Count());


            // List of state and numbers of jobs per state
            DoRefreshGridJobV(true);
            DoGridTransferInit();
            DoRefreshGridAssetV(true);
            DoRefreshGridChannelV(true);
            DoRefreshGridProgramV(true);
            DoRefreshGridOriginV(true);

        }

        private void UpdateLabelStorageEncryption()
        {
            toolStripStatusLabelSE.Visible = Properties.Settings.Default.useStorageEncryption;
        }

        private void comboBoxStateJobsCountJobs() // To ad number of jobs in the combobox
        {
            int c = 0;
            string filter;
            const string p = "  (";

            for (int i = 0; i < comboBoxStateJobs.Items.Count; i++)
            {
                filter = comboBoxStateJobs.Items[i].ToString();
                if (filter.Contains(p)) filter = filter.Substring(0, filter.IndexOf(p));

                if (filter == "All")
                {
                    c = _context.Jobs.Count();
                }
                else
                {
                    c = _context.Jobs.Where(j => j.State == (JobState)Enum.Parse(typeof(JobState), filter)).Count();
                }
                if (c > 0) comboBoxStateJobs.Items[i] = string.Format("{0}  ({1})", filter, c);
                else comboBoxStateJobs.Items[i] = filter;
            }
        }

        private void createALocatorForTheAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssets();
            DoCreateLocator(SelectedAssets);
        }

        private void deleteAllLocatorsOfTheAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssets();
            DoDeleteAllLocatorsOnAssets(SelectedAssets);
        }



        private void DoMenuEncodeWithAMEAdvanced()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            List<IMediaProcessor> Encoders;

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;

            }

            if (SelectedAssets.FirstOrDefault() == null) return;

            string taskname = "AME (adv) Encoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvEncodername;
            Encoders = GetMediaProcessorsByName(Constants.AzureMediaEncoder);
            Encoders.AddRange(GetMediaProcessorsByName(Constants.WindowsAzureMediaEncoder));

            EncodingAMEAdv form = new EncodingAMEAdv()
            {
                EncodingLabel = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected. One job will be submitted." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be encoded.",
                EncodingPriority = Properties.Settings.Default.DefaultJobPriority,
                EncodingProcessorsList = Encoders,
                EncodingJobName = "AME (adv) Encoding of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + "-AME (adv) encoded",
                EncodingAMEPresetXMLFiles = Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder,
                SelectedAssets = SelectedAssets
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Read and update the configuration XML.
                //
                Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder = form.EncodingAMEPresetXMLFiles;
                Properties.Settings.Default.Save();

                string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name);
                IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);
                string tasknameloc = taskname.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name).Replace(Constants.NameconvEncodername, form.EncodingProcessorSelected.Name + " v" + form.EncodingProcessorSelected.Version);
                ITask AMETask = job.Tasks.AddNew(
                    tasknameloc,
                  form.EncodingProcessorSelected,// processor,
                   form.EncodingConfiguration,
                   Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);

                AMETask.InputAssets.AddRange(SelectedAssets);

                // Add an output asset to contain the results of the job.  
                string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name);
                AMETask.OutputAssets.AddNew(outputassetnameloc, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);

                // Submit the job and wait until it is completed. 
                try
                {
                    job.Submit();
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    MessageBox.Show("There has been a problem when submitting the job " + jobnameloc + Constants.endline + e.Message);
                    TextBoxLogWriteLine("There has been a problem when submitting the job {0}", jobnameloc, true);
                    TextBoxLogWriteLine(e);
                    return;
                }
                TextBoxLogWriteLine("Job '{0}' submitted", jobnameloc);
                DotabControlMainSwitch(Constants.TabJobs);
                DoRefreshGridJobV(false);
                Task.Factory.StartNew(() => dataGridViewJobsV.DoJobProgress(job));
            }

        }

        private void DoMenuProcessGeneric()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            if (SelectedAssets.FirstOrDefault() == null)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            string taskname = Constants.NameconvProcessorname + " processing of " + Constants.NameconvInputasset;

            GenericProcessor form = new GenericProcessor()
            {
                EncodingProcessorsList = _context.MediaProcessors.ToList().OrderBy(p => p.Vendor).ThenBy(p => p.Name).ThenBy(p => new Version(p.Version)).ToList(),
                EncodingJobName = Constants.NameconvProcessorname + " processing of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + "-" + Constants.NameconvProcessorname + " processed",
                EncodingPriority = Properties.Settings.Default.DefaultJobPriority,
                SelectedAssets = SelectedAssets,
                EncodingCreationMode = TaskJobCreationMode.MultipleTasks_MultipleJobs,
            };



            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Read and update the configuration XML.
                //
                if (form.EncodingCreationMode == TaskJobCreationMode.MultipleTasks_MultipleJobs) // One task per input asset, each task in one job
                {
                    foreach (IAsset asset in SelectedAssets)
                    {
                        string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name); ;

                        IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);

                        string tasknameloc = taskname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);

                        ITask task = job.Tasks.AddNew(
                                    tasknameloc,
                                   form.EncodingProcessorSelected,
                                   form.EncodingConfiguration,
                                   Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
                        // Specify the graph asset to be encoded, followed by the input video asset to be used
                        task.InputAssets.AddRange(SelectedAssets);
                        string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);
                        task.OutputAssets.AddNew(outputassetnameloc, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);

                        TextBoxLogWriteLine("Submitting encoding job '{0}'", jobnameloc);
                        // Submit the job and wait until it is completed. 
                        try
                        {
                            job.Submit();
                        }
                        catch (Exception e)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There has been a problem when submitting the job {0}.", jobnameloc, true);
                            TextBoxLogWriteLine(e);
                            return;
                        }
                        dataGridViewJobsV.DoJobProgress(job);
                    }

                }
                else if (form.EncodingCreationMode == TaskJobCreationMode.MultipleTasks_SingleJob)  /////////////   Several tasks but all in one job
                {
                    string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name); ;
                    IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);

                    foreach (IAsset asset in SelectedAssets)
                    {
                        string tasknameloc = taskname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);

                        ITask task = job.Tasks.AddNew(
                                    tasknameloc,
                                   form.EncodingProcessorSelected,
                                   form.EncodingConfiguration,
                                   Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
                        // Specify the graph asset to be encoded, followed by the input video asset to be used
                        task.InputAssets.Add(asset);
                        string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);
                        task.OutputAssets.AddNew(outputassetnameloc, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
                    }

                    TextBoxLogWriteLine("Submitting encoding job '{0}'", jobnameloc);
                    // Submit the job and wait until it is completed. 
                    try
                    {
                        job.Submit();
                    }
                    catch (Exception e)
                    {
                        // Add useful information to the exception
                        MessageBox.Show("There has been a problem when submitting the job " + jobnameloc);
                        TextBoxLogWriteLine("There has been a problem when submitting the job {0}.", jobnameloc, true);
                        TextBoxLogWriteLine(e);
                        return;
                    }
                    dataGridViewJobsV.DoJobProgress(job);

                }
                else if (form.EncodingCreationMode == TaskJobCreationMode.SingleTask_SingleJob) // Create one single task in one job
                {
                    string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name); ;
                    IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);

                    string tasknameloc = taskname.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);

                    ITask task = job.Tasks.AddNew(
                                tasknameloc,
                               form.EncodingProcessorSelected,
                               form.EncodingConfiguration,
                               Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
                    // Specify the graph asset to be encoded, followed by the input video asset to be used
                    task.InputAssets.AddRange(SelectedAssets);
                    string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);
                    task.OutputAssets.AddNew(outputassetnameloc, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);

                    TextBoxLogWriteLine("Submitting encoding job '{0}'", jobnameloc);
                    // Submit the job and wait until it is completed. 
                    try
                    {
                        job.Submit();
                    }
                    catch (Exception e)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There has been a problem when submitting the job '{0}'", jobnameloc, true);
                        TextBoxLogWriteLine(e);
                        return;
                    }
                    dataGridViewJobsV.DoJobProgress(job);

                }

                DotabControlMainSwitch(Constants.TabJobs);
                DoRefreshGridJobV(false);
            }

        }



        private void butNextPageAsset_Click(object sender, EventArgs e)
        {
            if (comboBoxPageAssets.SelectedIndex < (comboBoxPageAssets.Items.Count - 1))
            {
                comboBoxPageAssets.SelectedIndex++;
                butPrevPageAsset.Enabled = true;
            }
            else butNextPageAsset.Enabled = false;

        }

        private void butPrevPageAsset_Click(object sender, EventArgs e)
        {
            if (comboBoxPageAssets.SelectedIndex > 0)
            {
                comboBoxPageAssets.SelectedIndex--;
                butNextPageAsset.Enabled = true;
            }
            else butPrevPageAsset.Enabled = false;
        }

        private void butNextPageJob_Click(object sender, EventArgs e)
        {
            if (comboBoxPageJobs.SelectedIndex < (comboBoxPageJobs.Items.Count - 1))
            {
                comboBoxPageJobs.SelectedIndex++;
                butPrevPageJob.Enabled = true;
            }
            else butNextPageJob.Enabled = false;
        }

        private void butPrevPageJob_Click(object sender, EventArgs e)
        {
            if (comboBoxPageJobs.SelectedIndex > 0)
            {
                comboBoxPageJobs.SelectedIndex--;
                butNextPageJob.Enabled = true;
            }
            else butPrevPageJob.Enabled = false;
        }

        private void encodeAssetWithAzureMediaEncoderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithAMESystemPreset();
        }

        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {

            int TransferUncompleted = _MyListTransfer.Where(t => (t.State == TransferState.Processing) | (t.State == TransferState.Queued)).Count();
            if (TransferUncompleted > 0)
            {
                if (System.Windows.Forms.MessageBox.Show("One or several transfers are in the queue or in progress and will be interrupted." + Constants.endline + "Are you sure that you want to quit the application?", "Caution: transfer(s) in progress", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }

            }


        }



        private void comboBoxPageJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedindex = ((ComboBox)sender).SelectedIndex;
            dataGridViewJobsV.DisplayPage(selectedindex + 1);

            butPrevPageJob.Enabled = (selectedindex == 0) ? false : true;
            butNextPageJob.Enabled = (selectedindex == (dataGridViewJobsV.PageCount - 1)) ? false : true;
        }



        private void dataGridViewAssetsV_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {

                IAsset asset = GetAsset(dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV.Columns["Id"].Index].Value.ToString());

                if (asset == null) return;

                if (DisplayInfo(asset) == DialogResult.OK)
                {
                }
            }
        }

        private void comboBoxOrderAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewAssetsV.OrderAssetsInGrid = ((ComboBox)sender).SelectedItem.ToString();

            if (dataGridViewAssetsV.Initialized)
            {
                DoRefreshGridAssetV(false);
            }
        }

        private void comboBoxPageAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedindex = ((ComboBox)sender).SelectedIndex;
            dataGridViewAssetsV.DisplayPage(selectedindex + 1);
            butPrevPageAsset.Enabled = (selectedindex == 0) ? false : true;
            butNextPageAsset.Enabled = (selectedindex == (dataGridViewAssetsV.PageCount - 1)) ? false : true;
        }

        private void dataGridViewJobsV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == dataGridViewJobsV.Columns["State"].Index) // state column
            {
                if (dataGridViewJobsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    JobState JS = (JobState)dataGridViewJobsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    Color mycolor;

                    switch (JS)
                    {
                        case JobState.Error:
                            mycolor = Color.Red;
                            break;
                        case JobState.Canceled:
                            mycolor = Color.Blue;
                            break;
                        case JobState.Canceling:
                            mycolor = Color.LightBlue;
                            break;
                        case JobState.Processing:
                            mycolor = Color.DarkGreen;
                            break;
                        case JobState.Queued:
                            mycolor = Color.Green;
                            break;
                        default:
                            mycolor = Color.Black;
                            break;

                    }
                    for (int i = 0; i < dataGridViewJobsV.Columns["Progress"].Index; i++) dataGridViewJobsV.Rows[e.RowIndex].Cells[i].Style.ForeColor = mycolor;

                }
            }
        }

        private void dataGridViewJobsV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IJob job = GetJob(dataGridViewJobsV.Rows[e.RowIndex].Cells[dataGridViewJobsV.Columns["Id"].Index].Value.ToString());

                if (job == null) return;

                if (DisplayInfo(job) == DialogResult.OK)
                {
                }
            }
        }

        private void comboBoxOrderJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewJobsV.Initialized)
            {
                Debug.WriteLine("comboBoxOrderJobs_SelectedIndexChanged");
                dataGridViewJobsV.OrderJobsInGrid = ((ComboBox)sender).SelectedItem.ToString();
                DoRefreshGridJobV(false);
            }
        }

        private void dataGridViewAssetsV_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == dataGridViewAssetsV.Columns["Type"].Index) // state column
            {
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    string TypeStr = (string)dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (TypeStr.Equals(AssetInfo.Type_Empty)) foreach (DataGridViewCell c in dataGridViewAssetsV.Rows[e.RowIndex].Cells) c.Style.ForeColor = Color.Red;
                    else if (TypeStr.Contains(AssetInfo.Type_Blueprint)) foreach (DataGridViewCell c in dataGridViewAssetsV.Rows[e.RowIndex].Cells) c.Style.ForeColor = Color.Blue;
                }
            }
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns["Size"].Index) // size column
            {
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    string TypeStr = (string)dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (TypeStr.Equals("0 B")) foreach (DataGridViewCell c in dataGridViewAssetsV.Rows[e.RowIndex].Cells) c.Style.ForeColor = Color.Red;
                }

            }

            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._statEnc].Index)  // Mouseover for icons
            {

                var cell = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._statEncMouseOver].Value != null)
                    cell.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._statEncMouseOver].Value.ToString();
            }
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._dynEnc].Index)// Mouseover for icons
            {
                var cell = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._dynEncMouseOver].Value != null)
                    cell.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._dynEncMouseOver].Value.ToString();
            }
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._publication].Index)// Mouseover for icons
            {
                var cell = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._publicationMouseOver].Value != null)
                    cell.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._publicationMouseOver].Value.ToString();
            }




        }

        private void toolStripMenuItemDisplayInfo_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfo();
        }

        private void contextMenuStripAssets_Opening(object sender, CancelEventArgs e)
        {
            bool singleitem = (ReturnSelectedAssets().Count == 1);
            ContextMenuItemAssetDisplayInfo.Enabled = singleitem;
            ContextMenuItemAssetRename.Enabled = singleitem;
            ContextMenuItemAssetExportAssetFilesToAzureStorage.Enabled = singleitem;
        }

        private void toolStripMenuItemRename_Click(object sender, EventArgs e)
        {
            DoMenuRenameAsset();
        }



        private void toolStripMenuItemDownloadToLocal_Click(object sender, EventArgs e)
        {
            DoMenuDownloadToLocal();
        }

        private void toolStripMenuItemUploadFileFromAzure_Click(object sender, EventArgs e)
        {
            DoMenuImportFromAzureStorage();
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            DoMenuDeleteSelectedAssets();
        }


        private void toolStripMenuAsset_DropDownOpening(object sender, EventArgs e)
        {
            bool singleitem = (ReturnSelectedAssets().Count == 1);
            informationToolStripMenuItem.Enabled = singleitem;
            renameToolStripMenuItem.Enabled = singleitem;
            toAzureStorageToolStripMenuItem.Enabled = singleitem;

        }

        private void toolStripMenuJobDisplayInfo_Click(object sender, EventArgs e)
        {
            DoMenuDisplayJobInfo();
        }

        private void toolStripMenuJobsCancel_Click(object sender, EventArgs e)
        {
            DoCancelJobs();
        }

        private void contextMenuStripJobs_Opening(object sender, CancelEventArgs e)
        {
            bool singleitem = (ReturnSelectedJobs().Count == 1);
            ContextMenuItemJobDisplayInfo.Enabled = singleitem;
        }

        private void toolStripMenuItemJobsDelete_Click(object sender, EventArgs e)
        {
            DoDeleteSelectedJobs();
        }



        private void richTextBoxLog_TextChanged(object sender, EventArgs e)
        {
            // we want to scroll down the textBox
            richTextBoxLog.SelectionStart = richTextBoxLog.Text.Length;
            richTextBoxLog.ScrollToCaret();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBoxStateJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewJobsV.Initialized)
            {
                Debug.WriteLine("comboBoxStateJobs_SelectedIndexChanged");
                const string p = "  (";
                string filter = ((ComboBox)sender).SelectedItem.ToString();
                if (filter.Contains(p)) filter = filter.Substring(0, filter.IndexOf(p));
                dataGridViewJobsV.FilterJobsState = filter;
                DoRefreshGridJobV(false);
            }
        }


        private void createReportEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateJobReportEmail();
        }



        private void DoCreateJobReportEmail()
        {

            JobInfo JR = new JobInfo(ReturnSelectedJobs());
            JR.CreateOutlookMail();

        }

        private void createOutlookReportEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateJobReportEmail();
        }

        private void displayInformationForAKnownJobIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayJobInfoFromKnownID();
        }

        private static void DoMenuDisplayJobInfoFromKnownID()
        {

            string JobId = "";
            string clipbs = Clipboard.GetText();
            if (clipbs != null) if (clipbs.StartsWith("nb:jid:UUID:")) JobId = clipbs;


            if (InputBox("Job ID", "Please enter the known Job Id :", ref JobId) == DialogResult.OK)
            {
                IJob KnownJob = GetJob(JobId);
                if (KnownJob == null)
                {
                    MessageBox.Show("This job has not been found.");
                }
                else if (DisplayInfo(KnownJob) == DialogResult.OK)
                {
                }
            }

        }
        private void DoMenuDisplayAssetInfoFromKnownID()
        {

            string AssetId = "";
            string clipbs = Clipboard.GetText();
            if (clipbs != null) if (clipbs.StartsWith("nb:cid:UUID:")) AssetId = clipbs;

            if (InputBox("Asset ID", "Please enter the known Asset Id :", ref AssetId) == DialogResult.OK)
            {
                IAsset KnownAsset = GetAsset(AssetId);
                if (KnownAsset == null)
                {
                    MessageBox.Show("This asset has not been found.");
                }

                else if (DisplayInfo(KnownAsset) == DialogResult.OK)
                {
                }
            }

        }

        private void displayInformationForAKnownAssetIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfoFromKnownID();
        }

        private void dataGridViewTransfer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == dataGridViewTransfer.Columns["State"].Index) // state column
            {
                if (dataGridViewTransfer.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {

                    TransferState JS = (TransferState)dataGridViewTransfer.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    Color mycolor;

                    switch (JS)
                    {
                        case TransferState.Error:
                            mycolor = Color.Red;
                            break;

                        case TransferState.Processing:
                            mycolor = Color.DarkGreen;
                            break;

                        case TransferState.Queued:
                            mycolor = Color.Green;
                            break;

                        default:
                            mycolor = Color.Black;
                            break;

                    }
                    for (int i = 0; i < dataGridViewTransfer.Columns.Count; i++) dataGridViewTransfer.Rows[e.RowIndex].Cells[i].Style.ForeColor = mycolor;

                }
            }
        }

        private void toolStripComboBoxNbItemsPage_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBoxNbItemsPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox CB = (ToolStripComboBox)sender;
            string sel = CB.SelectedItem.ToString();
            sel = sel.Substring(0, sel.IndexOf(" "));
            int nbitem = Convert.ToInt16(sel);

            dataGridViewAssetsV.AssetsPerPage = nbitem;
            dataGridViewJobsV.JobssPerPage = nbitem;
        }

        private void buttonJobSearch_Click(object sender, EventArgs e)
        {
            if (dataGridViewJobsV.Initialized)
            {
                Debug.WriteLine("buttonJobSearch_Click");
                string search = textBoxJobSearch.Text;
                dataGridViewJobsV.SearchInName = search;
                DoRefreshGridJobV(false);
            }
        }

        private void buttonAssetSearch_Click(object sender, EventArgs e)
        {
            if (dataGridViewAssetsV.Initialized)
            {
                string search = textBoxAssetSearch.Text;
                dataGridViewAssetsV.SearchInName = search;
                DoRefreshGridAssetV(false);
            }
        }

        private void comboBoxStateAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewAssetsV.Initialized)
            {
                Debug.WriteLine("comboBoxStateAssets_SelectedIndexChanged");

                string filter = ((ComboBox)sender).SelectedItem.ToString();

                dataGridViewAssetsV.StateFilter = filter;
                DoRefreshGridAssetV(false);
            }
        }

        private void toolStripMenuItemOpenDest_Click(object sender, EventArgs e)
        {
            DoOpenTransferDestLocation();
        }

        private void DoOpenTransferDestLocation()
        {
            if (dataGridViewTransfer.SelectedRows.Count > 0)
            {
                if ((TransferState)dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["State"].Index].Value == TransferState.Finished)
                {
                    string location = dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["DestLocation"].Index].Value.ToString();

                    switch ((TransferType)dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["Type"].Index].Value)
                    {
                        case TransferType.DownloadToLocal:
                            if (!string.IsNullOrEmpty(location) && location != null) Process.Start(location);
                            break;

                        case TransferType.ImportFromAzureStorage:
                        case TransferType.ImportFromHttp:
                        case TransferType.UploadFromFile:
                        case TransferType.UploadFromFolder:
                            IAsset asset = GetAsset(location);
                            if (asset != null) DisplayInfo(asset);
                            break;

                        case TransferType.ExportToAzureStorage:
                        default:
                            break;


                    }
                }
            }
        }

        private void openDestinationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoOpenTransferDestLocation();
        }

        private void dataGridViewTransfer_DoubleClick(object sender, EventArgs e)
        {
            DoOpenTransferDestLocation();
        }

        private void contextMenuStripTransfers_Opening(object sender, CancelEventArgs e)
        {
            ToolStrip toolStripMenuItemOpenDest = (ToolStrip)sender;

            if (dataGridViewTransfer.SelectedRows.Count > 0)
            {
                if ((TransferState)dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["State"].Index].Value == TransferState.Finished)
                {
                    toolStripMenuItemOpenDest.Enabled = true;
                }
            }
            else toolStripMenuItemOpenDest.Enabled = false;


        }

        private void priorityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoChangeJobPriority();
        }

        private void DoChangeJobPriority()
        {
            List<IJob> SelectedJobs = ReturnSelectedJobs();

            if (SelectedJobs.Count > 0)
            {
                Priority form = new Priority()
                {
                    JobPriority = (SelectedJobs.Count == 1) ? SelectedJobs[0].Priority : 10 // if only one job so we pass the current priority to dialog box
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    foreach (IJob JobToProcess in SelectedJobs)

                        if (JobToProcess != null)
                        {
                            //delete
                            TextBoxLogWriteLine(string.Format("Changing priority to {0} for job '{1}'.", form.JobPriority, JobToProcess.Name));
                            try
                            {
                                JobToProcess.Priority = form.JobPriority;
                                JobToProcess.Update();
                            }

                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when changing priority for {0}.", JobToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                            }
                        }
                }
            }
        }

        private void changePriorityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoChangeJobPriority();
        }

        private void comboBoxFilterTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewAssetsV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();
            if (dataGridViewAssetsV.Initialized)
            {
                DoRefreshGridAssetV(false);
            }
        }

        private void comboBoxFilterJobsTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewJobsV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();
            if (dataGridViewJobsV.Initialized)
            {
                DoRefreshGridJobV(false);
            }
        }


        private void encodeAssetsWithAzureMediaEncoderToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithAMEAdvanced();
        }

        private void toolStripMenuItemIndex_Click(object sender, EventArgs e)
        {
            DoMenuIndexAssets();
        }

        private void indexAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuIndexAssets();
        }



        private void withFlashOSMFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.FlashAzurePage, PlayBackLocator.GetSmoothStreamingUri());
        }



        private void withSilverlightMMPPFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.SilverlightMonitoring, PlayBackLocator.GetSmoothStreamingUri());
        }



        private void withFlashOSMFToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void playbackToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            bool CanBePlay = IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator);
            withFlashOSMFToolStripMenuItem.Enabled = CanBePlay;
            withSilverlightMMPPFToolStripMenuItem.Enabled = CanBePlay;
            withMPEGDASHAzurePlayerToolStripMenuItem.Enabled = CanBePlay;
            withMPEGDASHIFRefPlayerToolStripMenuItem.Enabled = CanBePlay;
            withCustomPlayerToolStripMenuItem.Enabled = CanBePlay;
        }

        private bool IsAssetCanBePlayed(IAsset asset, ref ILocator locator)
        {
            if (asset != null)
            {
                if (asset.Locators.Count > 0)
                {
                    ILocator LocatorsOrigin = asset.Locators.Where(l => (l.Type == LocatorType.OnDemandOrigin) && ((l.StartTime < DateTime.UtcNow) | (l.StartTime == null)) && (l.ExpirationDateTime > DateTime.UtcNow)).FirstOrDefault();
                    if (LocatorsOrigin != null)
                    {
                        //OK we can play the content
                        locator = LocatorsOrigin;
                        return true;
                    }
                }
            }
            return false;
        }

        private void withMPEGDASHIFRefPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.DASHIFRefPlayer, PlayBackLocator.GetMpegDashUri());
        }



        private void withFlashOSMFAzurePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.FlashAzurePage, PlayBackLocator.GetSmoothStreamingUri());
        }

        private void withSilverlightMontoringPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.SilverlightMonitoring, PlayBackLocator.GetSmoothStreamingUri());
        }

        private void withMPEGDASHIFReferencePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.DASHIFRefPlayer, PlayBackLocator.GetMpegDashUri());
        }

        private void withMPEGDASHAzurePlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.DASHAzurePage, PlayBackLocator.GetMpegDashUri());
        }

        private void playbackTheAssetToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            bool CanBePlay = IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator);
            ContextMenuItemPlaybackWithMPEGDASHAzure.Enabled = CanBePlay;
            ContextMenuItemPlaybackWithMPEGDASHIFReference.Enabled = CanBePlay;
            ContextMenuItemPlaybackWithSilverlightMonitoring.Enabled = CanBePlay;
            ContextMenuItemPlaybackWithFlashOSMFAzure.Enabled = CanBePlay;
            withCustomPlayerToolStripMenuItem1.Enabled = CanBePlay;
        }

        private void createOutlookReportEmailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCreateAssetReportEmail();
        }

        private void DoCreateAssetReportEmail()
        {
            AssetInfo AR = new AssetInfo(ReturnSelectedAssets());
            AR.CreateOutlookMail();
        }

        private void createOutlookReportEmailToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoCreateAssetReportEmail();
        }

        private void processAssetsadvancedModeWithToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuProcessGeneric();
        }

        private void processAssetsWithAProcessorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuProcessGeneric();
        }

        private void openOutputAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoOpenJobAsset(false);
        }

        private void DoOpenJobAsset(bool inputasset) // if false, then display first outputasset
        {
            List<IJob> SelectedJobs = ReturnSelectedJobs();
            if (SelectedJobs.Count != 0)
            {
                // Refresh the job.
                IJob JobToDisplayP2 = _context.Jobs.Where(j => j.Id == SelectedJobs.FirstOrDefault().Id).FirstOrDefault();
                if (JobToDisplayP2 != null)
                {
                    ReadOnlyCollection<IAsset> assetcol = inputasset ? JobToDisplayP2.InputMediaAssets : JobToDisplayP2.OutputMediaAssets;
                    if (assetcol.Count > 0)
                    {
                        if (assetcol.Count > 1) MessageBox.Show("There are " + assetcol.Count + " assets. Displaying only the first one.");
                        IAsset asset = assetcol.FirstOrDefault();
                        if (asset != null)
                        {
                            DisplayInfo(asset);
                        }
                    }
                }
            }
        }

        private void outputAssetInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoOpenJobAsset(false);
        }

        private void inputAssetInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoOpenJobAsset(true);
        }

        private void inputAssetInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoOpenJobAsset(true);
        }


        private void DoExportAssetToAzureStorage()
        {
            string valuekey = "";
            bool UseDefaultStorage = true;
            string containername = "";
            string otherstoragename = "";
            string otherstoragekey = "";
            List<IAssetFile> SelectedFiles = new List<IAssetFile>();
            bool CreateNewContainer = false;

            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count == 1)
            {
                if (!havestoragecredentials)
                { // No blob credentials. Let's ask the user
                    if (InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + _context.DefaultStorageAccount.Name + ":", ref valuekey) == DialogResult.OK)
                    {
                        _credentials.StorageKey = valuekey;
                        havestoragecredentials = true;
                    }
                }
                if (havestoragecredentials) // if we have the storage credentials
                {
                    if (SelectedAssets.FirstOrDefault().Options == AssetCreationOptions.None) // Ok, the selected asset is not encrypyted
                    {
                        if (CopyAssetToAzure(ref UseDefaultStorage, ref containername, ref otherstoragename, ref otherstoragekey, ref SelectedFiles, ref CreateNewContainer, SelectedAssets.FirstOrDefault()) == DialogResult.OK)
                        {
                            int index = DoGridTransferAddItem("Export to Azure Storage " + (CreateNewContainer ? "to a new container" : "to an existing container"), TransferType.ExportToAzureStorage, false);
                            // Start a worker thread that does copy.
                            Task.Factory.StartNew(() => ProcessExportAssetToAzureStorage(UseDefaultStorage, containername, otherstoragename, otherstoragekey, SelectedFiles, CreateNewContainer, index));
                            DotabControlMainSwitch(Constants.TabTransfers);
                            DoRefreshGridAssetV(false);
                        }
                    }
                    else // selected asset is encrypted, so we warn the user
                    {
                        MessageBox.Show("Asset cannot be exported as it is storage encrypted.", "Asset encrypted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void fromAzureStorageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuImportFromAzureStorage();
        }

        private void fromASingleHTTPURLAmazonS3EtcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuImportFromHttp();
        }

        private void toAzureStorageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoExportAssetToAzureStorage();
        }

        private void downloadToLocalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuDownloadToLocal();
        }

        private void copyAssetFilesToAzureStorageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoExportAssetToAzureStorage();
        }

        private void setupAWatchFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoWatchFolder();
        }

        private void DoWatchFolder()
        {

            WatchFolder form = new WatchFolder()
            {
                WatchDeleteFile = WatchFolderDeleteFile,
                WatchFolderPath = WatchFolderFolderPath,
                WatchOn = WatchFolderIsOn,
                WatchUseQueue = Properties.Settings.Default.useTransferQueue
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                WatchFolderFolderPath = form.WatchFolderPath;
                WatchFolderIsOn = form.WatchOn;
                Properties.Settings.Default.useTransferQueue = form.WatchUseQueue;
                Properties.Settings.Default.Save();
                WatchFolderDeleteFile = form.WatchDeleteFile;

                if (!WatchFolderIsOn) // user want to stop the watch folder (if if exists)
                {
                    if (WatchFolderWatcher != null)
                    {
                        WatchFolderWatcher.EnableRaisingEvents = false;
                        WatchFolderWatcher = null;
                    }
                    toolStripStatusLabelWatchFolder.Visible = false;

                }
                else // User wants to active the watch folder
                {
                    if (WatchFolderWatcher == null)
                    {
                        // Create a new FileSystemWatcher and set its properties.
                        WatchFolderWatcher = new FileSystemWatcher();
                    }


                    WatchFolderWatcher.Path = WatchFolderFolderPath;
                    /* Watch for changes in LastAccess and LastWrite times, and
                       the renaming of files or directories. */
                    WatchFolderWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                       | NotifyFilters.FileName; //| NotifyFilters.DirectoryName;
                    // Only watch text files.
                    WatchFolderWatcher.Filter = "*.*";
                    WatchFolderWatcher.IncludeSubdirectories = false;


                    // Begin watching.
                    WatchFolderWatcher.EnableRaisingEvents = true;
                    toolStripStatusLabelWatchFolder.Visible = true;

                    WatchFolderWatcher.Created += (s, e) =>
                    {
                        if (!this.seen.ContainsKey(e.FullPath)
                            || (DateTime.Now - this.seen[e.FullPath]) > this.seenInterval)
                        {
                            this.seen[e.FullPath] = DateTime.Now;
                            ThreadPool.QueueUserWorkItem(
                                this.WaitForCreatingProcessToCloseFileThenDoStuff, e.FullPath);
                        }
                    };
                }
            }
        }



        private void WaitForCreatingProcessToCloseFileThenDoStuff(object threadContext)
        {
            // Make sure the just-found file is done being
            // written by repeatedly attempting to open it
            // for exclusive access.
            var path = (string)threadContext;


            //detect whether its a directory or file

            DateTime started = DateTime.Now;
            DateTime lastLengthChange = DateTime.Now;
            long lastLength = 0;
            var noGrowthLimit = new TimeSpan(0, 5, 0);
            var notFoundLimit = new TimeSpan(0, 0, 1);

            for (int tries = 0; ; ++tries)
            {
                try
                {
                    // Do Stuff
                    Debug.WriteLine(path);

                    try
                    {
                        int index = DoGridTransferAddItem(string.Format("Watch folder: upload of file '{0}'", Path.GetFileName(path)), TransferType.UploadFromFile, Properties.Settings.Default.useTransferQueue);
                        // Start a worker thread that does uploading.
                        Task.Factory.StartNew(() => ProcessUploadFile(path, index, WatchFolderDeleteFile));

                    }
                    catch (Exception e)
                    {
                        TextBoxLogWriteLine("Error: Could not read file from disk. Original error : ", true);
                        TextBoxLogWriteLine(e);
                    }


                    break;
                }
                catch (FileNotFoundException)
                {
                    // Sometimes the file appears before it is there.
                    if (DateTime.Now - started > notFoundLimit)
                    {
                        // Should be there by now
                        break;
                    }
                }
                catch (IOException ex)
                {
                    // mask in severity, customer, and code
                    var hr = (int)(ex.HResult & 0xA000FFFF);
                    if (hr != 0x80000020 && hr != 0x80000021)
                    {
                        // not a share violation or a lock violation
                        throw;
                    }
                }

                try
                {
                    var fi = new FileInfo(path);
                    if (fi.Length > lastLength)
                    {
                        lastLength = fi.Length;
                        lastLengthChange = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                }

                // still locked
                if (DateTime.Now - lastLengthChange > noGrowthLimit)
                {
                    // 5 minutes, still locked, no growth.
                    break;
                }

                Thread.Sleep(111);
            }
            // }
        }




        private void azureMediaServicesMSDNToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Process.Start(@"http://aka.ms/wamsmsdn");
        }

        private void azureMediaServicesForumToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Process.Start(@"http://aka.ms/wamshelp");
        }

        private void azureMediaHelpFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(_HelpFiles + "/AzureMedia.chm");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox myabout = new AboutBox();
            myabout.Show();
        }



        private void tabControlMain_Selected(object sender, TabControlEventArgs e)
        {
            TabControl tabcontrol = (TabControl)sender;

            // let's enable or disable all items from menu and context menu
            EnableChildItems(ref publishToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabAssets) | (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabLive))));
            EnableChildItems(ref processToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabAssets)));
            EnableChildItems(ref assetToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabAssets)));
            EnableChildItems(ref contextMenuStripAssets, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabAssets)));

            EnableChildItems(ref encodingToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabJobs)));
            EnableChildItems(ref contextMenuStripJobs, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabJobs)));

            EnableChildItems(ref transferToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabTransfers)));
            EnableChildItems(ref contextMenuStripTransfers, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabTransfers)));

            EnableChildItems(ref originToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabOrigins)));
            EnableChildItems(ref contextMenuStripStreaminEndpoint, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabOrigins)));

            EnableChildItems(ref liveChannelToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabLive)));
            EnableChildItems(ref contextMenuStripChannels, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabLive)));
            EnableChildItems(ref contextMenuStripPrograms, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabLive)));

            // let's disable Zenium or Indexer if not present
            if (!ZeniumPresent)
            {
                encodeAssetWithImagineZeniumToolStripMenuItem.Enabled = false;  //menu
                ContextMenuItemZenium.Enabled = false; // mouse context menu
            }


        }

        private void EnableChildItems(ref ToolStripMenuItem menuitem, bool bflag)
        {
            menuitem.Enabled = bflag;
            foreach (ToolStripItem item in menuitem.DropDownItems)
            {
                item.Enabled = bflag;

                if (item.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem itemt = (ToolStripMenuItem)item;
                    if (itemt.HasDropDownItems)
                    {
                        foreach (ToolStripItem itemd in itemt.DropDownItems) itemd.Enabled = bflag;
                    }

                }
            }
        }

        private void EnableChildItems(ref ContextMenuStrip menuitem, bool bflag)
        {
            menuitem.Enabled = bflag;
            foreach (ToolStripItem item in menuitem.Items)
            {
                item.Enabled = bflag;

                if (item.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem itemt = (ToolStripMenuItem)item;
                    if (itemt.HasDropDownItems)
                    {
                        foreach (ToolStripItem itemd in itemt.DropDownItems) itemd.Enabled = bflag;
                    }
                }
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoRefresh();
        }


        private void buttonbuildchart_Click(object sender, EventArgs e)
        {
            chart.Series.Clear();

            progressBarChart.Value = 0;


            var seriesJobTotal = new Series()
            {
                Name = "Job total",
                XValueType = ChartValueType.DateTime,
                ChartType = SeriesChartType.FastLine
            };

            chart.Series.Add(seriesJobTotal);


            var seriesError = new Series()
            {
                Name = "Job errors",
                XValueType = ChartValueType.DateTime,
                ChartType = SeriesChartType.FastLine
            };



            chart.Series.Add(seriesError);

            var querytotal = dataGridViewJobsV.DisplayedJobs.GroupBy(j => ((DateTime)j.Created).Date).Select(j => new { number = j.Count(), date = (DateTime)j.Key }).ToList();
            var queryerror = dataGridViewJobsV.DisplayedJobs.Where(j => j.State == JobState.Error).GroupBy(j => ((DateTime)j.Created).Date).Select(j => new { number = j.Count(), date = (DateTime)j.Key }).ToList();

            int i = 0;
            progressBarChart.Minimum = 0;
            progressBarChart.Maximum = querytotal.Count() + queryerror.Count();


            foreach (var j in querytotal)
            {
                i++;
                progressBarChart.Value = i;
                seriesJobTotal.Points.AddXY(j.date, j.number);
            }
            foreach (var j in queryerror)
            {
                i++;
                progressBarChart.Value = i;
                seriesError.Points.AddXY(j.date, j.number);
            }


            // draw!
            chart.Invalidate();

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoManageOptions();
        }

        private void DoManageOptions()
        {
            Options myForm = new Options();
            if (myForm.ShowDialog() == DialogResult.OK)
            {
                ApplySettingsOptions();
                UpdateLabelStorageEncryption();
            }
        }

        private void ApplySettingsOptions(bool init = false)
        {
            if (!init)
            {
                dataGridViewAssetsV.Columns["Id"].Visible = Properties.Settings.Default.DisplayAssetIDinGrid;
                dataGridViewJobsV.Columns["Id"].Visible = Properties.Settings.Default.DisplayJobIDinGrid;
                dataGridViewChannelsV.Columns["Id"].Visible = Properties.Settings.Default.DisplayLiveChannelIDinGrid;
                dataGridViewProgramsV.Columns["Id"].Visible = Properties.Settings.Default.DisplayLiveProgramIDinGrid;
                dataGridViewOriginsV.Columns["Id"].Visible = Properties.Settings.Default.DisplayOriginIDinGrid;
            }

            dataGridViewAssetsV.AssetsPerPage = Properties.Settings.Default.NbItemsDisplayedInGrid;
            dataGridViewJobsV.JobssPerPage = Properties.Settings.Default.NbItemsDisplayedInGrid;

            TimerAutoRefresh.Interval = Properties.Settings.Default.AutoRefreshTime * 1000;
            TimerAutoRefresh.Enabled = Properties.Settings.Default.AutoRefresh;
            withCustomPlayerToolStripMenuItem.Visible = Properties.Settings.Default.CustomPlayerEnabled;
            withCustomPlayerToolStripMenuItem1.Visible = Properties.Settings.Default.CustomPlayerEnabled;
            withCustomPlayerToolStripMenuItem2.Visible = Properties.Settings.Default.CustomPlayerEnabled;
        }




        private void DoRefreshGridChannelV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewChannelsV.Init(_credentials);
            }

            Debug.WriteLine("DoRefreshGridLiveVNotforsttime");
            int backupindex = 0;
            int pagecount = 0;
            //comboBoxPageJobs.Invoke(new Action(() => backupindex = comboBoxPageJobs.SelectedIndex));
            dataGridViewChannelsV.Invoke(new Action(() => dataGridViewChannelsV.RefreshChannels(_context, backupindex + 1)));
            //comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.Items.Clear()));
            //dataGridViewJobsV.Invoke(new Action(() => pagecount = dataGridViewJobsV.PageCount));

            // add pages
            //for (int i = 1; i <= pagecount; i++) comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.Items.Add(i)));
            //comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.SelectedIndex = dataGridViewJobsV.CurrentPage - 1));
            //uodate tab nimber of jobs

            tabPageLive.Invoke(new Action(() => tabPageLive.Text = string.Format(Constants.TabLive + " ({0})", dataGridViewChannelsV.DisplayedCount)));
        }

        private void DoRefreshGridProgramV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewProgramsV.Init(_credentials);
            }

            Debug.WriteLine("DoRefreshGridProgramVNotforsttime");
            int backupindex = 0;
            dataGridViewProgramsV.Invoke(new Action(() => dataGridViewProgramsV.RefreshPrograms(_context, backupindex + 1)));
        }

        private void DoRefreshGridOriginV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewOriginsV.Init(_credentials);

            }
            Debug.WriteLine("DoRefreshGridOriginsVNotforsttime");
            dataGridViewOriginsV.Invoke(new Action(() => dataGridViewOriginsV.RefreshOrigins(_context, 1)));

            tabPageAssets.Invoke(new Action(() => tabPageOrigins.Text = string.Format(Constants.TabOrigins + " ({0})", dataGridViewOriginsV.DisplayedCount)));

        }


        private List<IChannel> ReturnSelectedChannels()
        {
            List<IChannel> SelectedChannels = new List<IChannel>();
            foreach (DataGridViewRow Row in dataGridViewChannelsV.SelectedRows)
            {
                SelectedChannels.Add(_context.Channels.Where(j => j.Id == Row.Cells[dataGridViewChannelsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault());
            }
            SelectedChannels.Reverse();
            return SelectedChannels;
        }
        private List<IStreamingEndpoint> ReturnSelectedOrigins()
        {
            List<IStreamingEndpoint> SelectedOrigins = new List<IStreamingEndpoint>();
            foreach (DataGridViewRow Row in dataGridViewOriginsV.SelectedRows)
            {
                SelectedOrigins.Add(_context.StreamingEndpoints.Where(j => j.Id == Row.Cells[dataGridViewOriginsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault());
            }
            SelectedOrigins.Reverse();
            return SelectedOrigins;
        }

        private List<IProgram> ReturnSelectedPrograms()
        {
            List<IProgram> SelectedPrograms = new List<IProgram>();
            foreach (DataGridViewRow Row in dataGridViewProgramsV.SelectedRows)
            {
                SelectedPrograms.Add(_context.Programs.Where(j => j.Id == Row.Cells[dataGridViewProgramsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault());
            }
            SelectedPrograms.Reverse();
            return SelectedPrograms;
        }

        private void DoStopChannels()
        {
            foreach (IChannel myC in ReturnSelectedChannels())
            {
                StopChannel(myC);
            }
        }

        private void DoStartChannels()
        {
            foreach (IChannel myC in ReturnSelectedChannels())
            {
                StartChannel(myC);
            }
        }

        private void RefreshLiveGrid(IChannel channel, bool delay)
        {
            if (delay) System.Threading.Thread.Sleep(1000);
            dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.RefreshChannel(channel)), null);
        }

        private async void StartChannel(IChannel myC)
        {
            if (myC != null)
            {
                TextBoxLogWriteLine("Starting channel '{0}' ", myC.Name);
                var STask = ChannelExecuteAsync(myC.StartAsync, myC.Name, "started");
                DoChannelMonitor(myC.Name, OperationType.Start);
                await STask;
            }
        }

        private async void StopChannel(IChannel myC)
        {
            if (myC != null)
            {
                TextBoxLogWriteLine("Stopping channel '{0}'", myC.Name);
                var STask = ChannelExecuteAsync(myC.StopAsync, myC.Name, "stopped");
                DoChannelMonitor(myC.Name, OperationType.Stop);
                await STask;
            }
        }

        private async void ResetChannel(IChannel myC)
        {
            if (myC != null)
            {
                TextBoxLogWriteLine("Reseting channel '{0}'", myC.Name);
                var STask = ChannelExecuteAsync(myC.ResetAsync, myC.Name, "reset");
                DoChannelMonitor(myC.Name, OperationType.Reset);
                await STask;
            }
        }

        private async void DeleteChannel(IChannel myC)
        {
            if (myC != null)
            {
                TextBoxLogWriteLine("Deleting channel '{0}'...", myC.Name);
                var STask = ChannelExecuteAsync(myC.DeleteAsync, myC.Name, "deleted");
                DoChannelMonitor(myC.Name, OperationType.Delete);
                await STask;
                DoRefreshGridChannelV(false);
            }
        }

        private async void DeleteProgram(IProgram myP)
        {
            if (myP != null)
            {
                TextBoxLogWriteLine("Deleting program '{0}'...", myP.Name);
                var STask = ProgramExecuteAsync(myP.DeleteAsync, myP.Name, "deleted");
                DoProgramMonitor(myP.Name, OperationType.Delete);
                await STask;
                DoRefreshGridProgramV(false);
            }
        }

        private async void StartProgam(IProgram myP)
        {
            if (myP != null)
            {
                TextBoxLogWriteLine("Starting program '{0}'...", myP.Name);
                var STask = ProgramExecuteAsync(myP.StartAsync, myP.Name, "started");
                DoProgramMonitor(myP.Name, OperationType.Start);
                await STask;
                DoRefreshGridProgramV(false);
            }
        }

        private async void StopProgram(IProgram myP)
        {
            if (myP != null)
            {
                TextBoxLogWriteLine("Stopping program '{0}'...", myP.Name);
                var STask = ProgramExecuteAsync(myP.StopAsync, myP.Name, "stopped");
                DoProgramMonitor(myP.Name, OperationType.Stop);
                await STask;
                DoRefreshGridProgramV(false);
            }
        }

        private async void StartOrigin(IStreamingEndpoint myO)
        {
            if (myO != null)
            {
                TextBoxLogWriteLine("Starting streaming endpoint '{0}'...", myO.Name);
                var STask = OriginExecuteAsync(myO.StartAsync, myO.Name, "started");
                DoOriginMonitor(myO.Name, OperationType.Start);
                await STask;
            }
        }
        private async void StopOrigin(IStreamingEndpoint myO)
        {
            if (myO != null)
            {
                TextBoxLogWriteLine("Stopping streaming endpoint '{0}'...", myO.Name);
                var STask = OriginExecuteAsync(myO.StopAsync, myO.Name, "stopped");
                DoOriginMonitor(myO.Name, OperationType.Stop);
                await STask;
            }
        }

        private async void DeleteOrigin(IStreamingEndpoint myO)
        {
            if (myO != null)
            {
                TextBoxLogWriteLine("Deleting streaming endpoint '{0}'.", myO.Name);
                var STask = OriginExecuteAsync(myO.DeleteAsync, myO.Name, "deleted");
                DoOriginMonitor(myO.Name, OperationType.Delete);
                await STask;
            }
        }


        private async void ScaleOrigin(IStreamingEndpoint myO, int unit)
        {
            if (myO != null)
            {
                try
                {
                    TextBoxLogWriteLine("Scaling streaming endpoint '{0}' to {1} unit(s)...", myO.Name, unit.ToString());
                    var STask = myO.ScaleAsync(unit);
                    DoOriginMonitor(myO.Name, OperationType.Scale);
                    await STask;
                }

                catch (Exception ex)
                {
                    var si = new StatusInfo
                    {
                        EntityName = myO.Name,
                        ErrorMessage = Program.GetErrorMessage(ex)
                    };
                    TextBoxLogWriteLine("Error when scaling streaming endpoint '{0}' : {1}", si.EntityName, si.ErrorMessage, true);
                    dataGridViewOriginsV.BeginInvoke(new Action(() => dataGridViewOriginsV.AddOriginEvent(si)), null);
                }
            }
        }

        private async void UpdateOrigin(IStreamingEndpoint myO)
        {
            if (myO != null)
            {
                try
                {
                    TextBoxLogWriteLine("Updating streaming endpoint '{0}'...", myO.Name);
                    var STask = myO.UpdateAsync();
                    await STask;
                    TextBoxLogWriteLine("Streaming endpoint '{0}' updated.", myO.Name);

                }

                catch (Exception ex)
                {
                    var si = new StatusInfo
                    {
                        EntityName = myO.Name,
                        ErrorMessage = Program.GetErrorMessage(ex)
                    };
                    TextBoxLogWriteLine("Error when updating streaming endpoint '{0}' : {1}", si.EntityName, si.ErrorMessage, true);
                    dataGridViewOriginsV.BeginInvoke(new Action(() => dataGridViewOriginsV.AddOriginEvent(si)), null);
                }
                DoRefreshGridOriginV(false);
            }
        }

        private async void UpdateChannel(IChannel channel)
        {
            if (channel != null)
            {
                try
                {
                    TextBoxLogWriteLine("Updating Channel '{0}'...", channel.Name);
                    var STask = channel.UpdateAsync();
                    await STask;
                    TextBoxLogWriteLine("Channel '{0}' updated.", channel.Name);
                }

                catch (Exception ex)
                {
                    var si = new StatusInfo
                    {
                        EntityName = channel.Name,
                        ErrorMessage = Program.GetErrorMessage(ex)
                    };
                    TextBoxLogWriteLine("Error when updating channel '{0}' : {1}", si.EntityName, si.ErrorMessage, true);
                    dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.AddChannelEvent(si)), null);
                }
                DoRefreshGridChannelV(false);
            }
        }

        private async void UpdateProgram(IProgram program)
        {
            if (program != null)
            {
                try
                {
                    TextBoxLogWriteLine("Updating Program {0}...", program.Name);
                    var STask = program.UpdateAsync();
                    await STask;
                    TextBoxLogWriteLine("Program {0} updated.", program.Name);
                }

                catch (Exception ex)
                {
                    var si = new StatusInfo
                    {
                        EntityName = program.Name,
                        ErrorMessage = Program.GetErrorMessage(ex)
                    };
                    TextBoxLogWriteLine("Error when updating program '{0}' : {1}", si.EntityName, si.ErrorMessage, true);
                    dataGridViewProgramsV.BeginInvoke(new Action(() => dataGridViewProgramsV.AddProgramEvent(si)), null);
                }
                DoRefreshGridProgramV(false);
            }
        }



        internal async Task ChannelExecuteAsync(Func<Task> fCall, string strObjectName, string strStatusSuccess)
        {
            try
            {
                await fCall();
                TextBoxLogWriteLine("Channel '{0}' {1}.", strObjectName, strStatusSuccess);
            }
            catch (Exception ex)
            {
                var si = new StatusInfo
                {
                    EntityName = strObjectName,
                    ErrorMessage = Program.GetErrorMessage(ex)
                };
                TextBoxLogWriteLine("Error with channel '{0}' : {1}", si.EntityName, si.ErrorMessage, true);
                dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.AddChannelEvent(si)), null);
            }
        }

        internal async Task ProgramExecuteAsync(Func<Task> fCall, string strObjectName, string strStatusSuccess)
        {
            try
            {
                await fCall();
                TextBoxLogWriteLine("Program '{0}' {1}.", strObjectName, strStatusSuccess);
            }
            catch (Exception ex)
            {
                var si = new StatusInfo
                {
                    EntityName = strObjectName,
                    ErrorMessage = Program.GetErrorMessage(ex)
                };
                TextBoxLogWriteLine("Error with program '{0}' : {1}", si.EntityName, si.ErrorMessage, true);
                dataGridViewProgramsV.BeginInvoke(new Action(() => dataGridViewProgramsV.AddProgramEvent(si)), null);
            }
        }

        internal async Task OriginExecuteAsync(Func<Task> fCall, string strObjectName, string strStatusSuccess)
        {
            try
            {
                await fCall();
                TextBoxLogWriteLine("Streaming endpoint '{0}' {1}.", strObjectName, strStatusSuccess);
            }
            catch (Exception ex)
            {
                var si = new StatusInfo
                {
                    EntityName = strObjectName,
                    ErrorMessage = Program.GetErrorMessage(ex)
                };
                TextBoxLogWriteLine("Error with streaming endpoint '{0}' : {1}", si.EntityName, si.ErrorMessage, true);
                dataGridViewOriginsV.BeginInvoke(new Action(() => dataGridViewOriginsV.AddOriginEvent(si)), null);
            }
        }




        private void DoDeleteChannels()
        {
            List<IChannel> SelectedChannels = ReturnSelectedChannels();
            if (SelectedChannels.Count > 0)
            {
                string question = (SelectedChannels.Count == 1) ? "Delete channel " + SelectedChannels[0].Name + " ?" : "Delete these " + SelectedChannels.Count + " channels ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Channel(s) deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (IChannel myC in ReturnSelectedChannels())
                    {
                        DeleteChannel(myC);
                    }
                }
            }
        }




        private void dataGridViewLiveV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewChannelsV.Columns["State"].Index) // state column
            {
                if (dataGridViewChannelsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ChannelState CS = (ChannelState)dataGridViewChannelsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    Color mycolor;

                    switch (CS)
                    {
                        case ChannelState.Deleting:
                            mycolor = Color.Red;
                            break;
                        case ChannelState.Stopping:
                            mycolor = Color.Blue;
                            break;
                        case ChannelState.Starting:
                            mycolor = Color.LightBlue;
                            break;
                        case ChannelState.Stopped:
                            mycolor = Color.Blue;
                            break;
                        case ChannelState.Running:
                            mycolor = Color.Green;
                            break;
                        default:
                            mycolor = Color.Black;
                            break;
                    }
                    for (int i = 0; i < dataGridViewChannelsV.Columns.Count; i++) dataGridViewChannelsV.Rows[e.RowIndex].Cells[i].Style.ForeColor = mycolor;
                }
            }
        }

        private void startChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStartChannels();
        }

        private void stopChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStopChannels();
        }

        private void resetChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoResetChannels();
        }

        private void DoResetChannels()
        {
            foreach (IChannel myC in ReturnSelectedChannels())
            {
                ResetChannel(myC);
            }
        }

        private void deleteChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteChannels();
        }

        private void createChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateChannel();
        }

        private async void DoCreateChannel()
        {
            CreateLiveChannel form = new CreateLiveChannel()
            {
                KeyframeInterval = Properties.Settings.Default.LiveKeyFrameInterval,
                HLSFragmentPerSegment = Properties.Settings.Default.LiveHLSFragmentsPerSegment,
                StartChannelNow = true
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                TextBoxLogWriteLine("Creating Channel '{0}'...", form.ChannelName);

                var options = new ChannelCreationOptions()
                {
                    Name = form.ChannelName,
                    Description = form.ChannelDescription,

                    Input = new ChannelInput()
                    {
                        StreamingProtocol = form.Protocol,
                        AccessControl = new ChannelAccessControl()
                        {
                            IPAllowList = form.inputIPAllow
                        },
                        KeyFrameInterval = form.KeyframeInterval
                    },
                    Output = new ChannelOutput() { Hls = new ChannelOutputHls() { FragmentsPerSegment = form.HLSFragmentPerSegment } }
                };
                var STask = ChannelExecuteAsync(
                      () =>
                          _context.Channels.CreateAsync(
                          options
                               ),
                          form.ChannelName,
                          "created");

                await STask;
                DoRefreshGridChannelV(false);
                IChannel channel = GetChannelFromName(form.ChannelName);
                if (channel != null)
                {
                    if (form.StartChannelNow)
                    {
                        StartChannel(GetChannelFromName(form.ChannelName));

                    }
                }
            }
        }



        public void DoChannelMonitor(string channelname, OperationType operationtype)
        {
            IChannel channel = _context.Channels.Where(c => c.Name == channelname).FirstOrDefault();
            if (channel != null) dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.DoChannelMonitor(channel, operationtype)), null);

        }

        public void DoOriginMonitor(string originname, OperationType operationtype)
        {
            IStreamingEndpoint origin = _context.StreamingEndpoints.Where(c => c.Name == originname).FirstOrDefault();
            if (origin != null) dataGridViewOriginsV.BeginInvoke(new Action(() => dataGridViewOriginsV.DoOriginMonitor(origin, operationtype)), null);

        }

        public void DoProgramMonitor(string programnane, OperationType operationtype)
        {
            IProgram program = _context.Programs.Where(c => c.Name == programnane).FirstOrDefault();
            if (program != null) dataGridViewProgramsV.BeginInvoke(new Action(() => dataGridViewProgramsV.DoProgramMonitor(program, operationtype)), null);
        }



        private void channToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayChannelInfo();
        }

        private void DoDisplayChannelInfo()
        {
            DoDisplayChannelInfo(ReturnSelectedChannels().FirstOrDefault());
        }

        private void DoDisplayChannelInfo(IChannel channel)
        {

            ChannelInformation form = new ChannelInformation()
            {
                MyChannel = channel,
                MyContext = _context
            };

            if (form.ShowDialog() == DialogResult.OK)
            {

                channel.Description = form.GetChannelDescription;

                // Input allow list
                if (form.GetInputIPAllowList != null)
                {
                    if (channel.Input.AccessControl == null)
                    {
                        channel.Input.AccessControl = new ChannelAccessControl();
                    }
                    channel.Input.AccessControl.IPAllowList = form.GetInputIPAllowList;

                }
                else
                {
                    if (channel.Input.AccessControl != null)
                    {
                        channel.Input.AccessControl.IPAllowList = null;
                    }
                }


                // Preview allow list
                if (form.GetPreviewAllowList != null)
                {
                    if (channel.Preview.AccessControl == null)
                    {
                        channel.Preview.AccessControl = new ChannelAccessControl();
                    }
                    channel.Preview.AccessControl.IPAllowList = form.GetPreviewAllowList;

                }
                else
                {
                    if (channel.Preview.AccessControl != null)
                    {
                        channel.Preview.AccessControl.IPAllowList = null;
                    }
                }


                // Client Access Policy
                if (form.GetChannelClientPolicy != null)
                {
                    if (channel.CrossSiteAccessPolicies == null)
                    {
                        channel.CrossSiteAccessPolicies = new CrossSiteAccessPolicies();
                    }
                    channel.CrossSiteAccessPolicies.ClientAccessPolicy = form.GetChannelClientPolicy;

                }
                else
                {
                    if (channel.CrossSiteAccessPolicies != null)
                    {
                        channel.CrossSiteAccessPolicies.ClientAccessPolicy = null;
                    }
                }


                // Cross domain  Policy
                if (form.GetChannelCrossdomaintPolicy != null)
                {
                    if (channel.CrossSiteAccessPolicies == null)
                    {
                        channel.CrossSiteAccessPolicies = new CrossSiteAccessPolicies();
                    }
                    channel.CrossSiteAccessPolicies.CrossDomainPolicy = form.GetChannelCrossdomaintPolicy;

                }
                else
                {
                    if (channel.CrossSiteAccessPolicies != null)
                    {
                        channel.CrossSiteAccessPolicies.CrossDomainPolicy = null;
                    }
                }
                UpdateChannel(channel);
            }

        }

        private void dataGridViewLiveV_SelectionChanged(object sender, EventArgs e)
        {
            List<IChannel> SelectedChannels = ReturnSelectedChannels();
            if (SelectedChannels.Count > 0)
            {
                try // sometimes, the channel can be null (if just deleted)
                {
                    dataGridViewProgramsV.ChannelSourceIDs = SelectedChannels.Select(c => c.Id).ToList();
                }
                catch
                {

                }
                DoRefreshGridProgramV(false);
            }
        }


        private void DoDeletePrograms()
        {

            List<IProgram> SelectedPrograms = ReturnSelectedPrograms();
            if (SelectedPrograms.Count > 0)
            {
                string question = (SelectedPrograms.Count == 1) ? "Delete program " + SelectedPrograms[0].Name + " ?" : "Delete these " + SelectedPrograms.Count + " programs ?";

                DeleteProgram form = new DeleteProgram(question);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    foreach (IProgram myP in SelectedPrograms)
                    {
                        IAsset asset = myP.Asset;
                        DeleteProgram(myP);
                        if (form.DeleteAsset)
                        {
                            if (myP.Asset != null)
                            {
                                //delete
                                TextBoxLogWriteLine("Deleting asset '{0}'", asset.Name);
                                try
                                {
                                    DeleteAsset(asset);
                                    if (GetAsset(asset.Id) == null) TextBoxLogWriteLine("Deletion done.");
                                }
                                catch (Exception ex)
                                {
                                    // Add useful information to the exception
                                    TextBoxLogWriteLine("There is a problem when deleting the asset {0}.", asset.Name, true);
                                    TextBoxLogWriteLine(ex.Message, true);
                                }
                            }
                        }
                    }
                }
            }

        }


        private void DoStartPrograms()
        {
            List<IProgram> SelectedPrograms = ReturnSelectedPrograms();
            if (SelectedPrograms.Count > 0)
            {
                foreach (IProgram myP in SelectedPrograms)
                {
                    StartProgam(myP);
                }
            }
        }


        private void DoStopPrograms()
        {
            List<IProgram> SelectedPrograms = ReturnSelectedPrograms();
            if (SelectedPrograms.Count > 0)
            {
                foreach (IProgram myP in SelectedPrograms)
                {
                    StopProgram(myP);
                }
            }
        }


        private IAsset CreateLiveAsset(string strName, bool createlocator)
        {
            var myA = _context.Assets.Create(strName, AssetCreationOptions.None); // Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
            if (createlocator) CreateOriginLocator(myA);
            return myA;
        }

        public void CreateOriginLocator(IAsset outputAsset)
        {
            IAccessPolicy policy =
                _context.AccessPolicies.Create("AP:" + outputAsset.Name, TimeSpan.FromDays(365), AccessPermissions.Read);
            _context.Locators.CreateLocator(LocatorType.OnDemandOrigin, outputAsset, policy, DateTime.UtcNow.AddMinutes(-5));
        }

        private async void DoCreateProgram()
        {
            IChannel channel = ReturnSelectedChannels().FirstOrDefault();
            if (channel != null)
            {
                CreateProgram form = new CreateProgram()
                    {
                        ChannelName = channel.Name,
                        archiveWindowLength = new TimeSpan(4, 0, 0),
                        CreateLocator = true,
                        AssetName = Constants.NameconvChannel + "-" + Constants.NameconvProgram,
                        ProposeScaleUnit = _context.StreamingEndpoints.Where(o => o.ScaleUnits > 0).ToList().Count == 0
                    };
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.ScaleUnit)
                    {
                        ScaleOrigin(_context.StreamingEndpoints.FirstOrDefault(), 1);
                    }

                    TextBoxLogWriteLine("Creating Program '{0}'...", form.ProgramName);
                    string assetname = form.AssetName.Replace(Constants.NameconvProgram, form.ProgramName).Replace(Constants.NameconvChannel, form.ChannelName);
                    string assetid = CreateLiveAsset(assetname, form.CreateLocator).Id;

                    var options = new ProgramCreationOptions()
                    {
                        Name = form.ProgramName,
                        Description = form.ProgramDescription,
                        ArchiveWindowLength = form.archiveWindowLength,
                        AssetId = assetid
                    };

                    var STask = ProgramExecuteAsync(
                           () =>
                               channel.Programs.CreateAsync(options)
                               , form.ProgramName,
                               "created");
                    await STask;
                    DoRefreshGridProgramV(false);
                }
            }
            else
            {
                MessageBox.Show("No channel has been selected.");
            }
        }


        private void createProgramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCreateProgram();
        }

        private void startProgramsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStartPrograms();
        }

        private void stopProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStopPrograms();
        }

        private void deleteProgramsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDeletePrograms();
        }

        private void dataGridViewProgramV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewProgramsV.Columns["State"].Index) // state column
            {
                if (dataGridViewProgramsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ProgramState CS = (ProgramState)dataGridViewProgramsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    Color mycolor;

                    switch (CS)
                    {

                        case ProgramState.Stopping:
                            mycolor = Color.Blue;
                            break;
                        case ProgramState.Starting:
                            mycolor = Color.LightBlue;
                            break;
                        case ProgramState.Stopped:
                            mycolor = Color.Blue;
                            break;
                        case ProgramState.Running:
                            mycolor = Color.Green;
                            break;
                        default:
                            mycolor = Color.Black;
                            break;

                    }
                    for (int i = 0; i < dataGridViewProgramsV.Columns.Count; i++) dataGridViewProgramsV.Rows[e.RowIndex].Cells[i].Style.ForeColor = mycolor;
                }
            }
        }

        private void displayProgramInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayProgramInfo();

        }

        private void DoDisplayProgramInfo()
        {
            DoDisplayProgramInfo(ReturnSelectedPrograms().FirstOrDefault());
        }

        private void DoDisplayProgramInfo(IProgram program)
        {
            if (program != null)
            {
                ProgramInformation form = new ProgramInformation()
                {
                    MyProgram = program,
                    MyContext = _context
                };


                if (form.ShowDialog() == DialogResult.OK)
                {
                    program.ArchiveWindowLength = form.archiveWindowLength;
                    program.Description = form.ProgramDescription;
                    UpdateProgram(program);
                }
            }
        }

        private void generateThumbnailsForTheAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoGenerateThumbnails();
        }

        private void DoGenerateThumbnails()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            if (SelectedAssets.FirstOrDefault() == null) return;

            string taskname = "Thumbnails generation of " + Constants.NameconvInputasset;
            IMediaProcessor processor = GetLatestMediaProcessorByName(Constants.WindowsAzureMediaEncoder);

            Thumbnails form = new Thumbnails()
            {
                ThumbnailsFileName = "{OriginalFilename}_{ThumbnailIndex}.{DefaultExtension}",
                ThumbnailsInputAssetName = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected for thumbnails generation." : "Generate thumbnails for '" + SelectedAssets.FirstOrDefault().Name + "'  ?",
                ThumbnailsOutputAssetName = Constants.NameconvInputasset + "-Thumbnails",
                ThumbnailsProcessorName = "Processor: " + processor.Vendor + " / " + processor.Name + " v" + processor.Version,
                ThumbnailsJobName = "Thumbnails generation of " + Constants.NameconvInputasset,
                ThumbnailsJobPriority = Properties.Settings.Default.DefaultJobPriority,
                ThumbnailsTimeValue = "0:0:0",
                ThumbnailsTimeStep = "0:0:5",
                ThumbnailsTimeStop = string.Empty,
                ThumbnailsSize = "300,*",
                ThumbnailsType = "Jpeg"
            };


            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string configThumbnails = LoadAndUpdateThumbnailsConfiguration(
                Path.Combine(_configurationXMLFiles, @"Thumbnails.xml"),
                form.ThumbnailsSize,
                form.ThumbnailsType,
                form.ThumbnailsFileName,
                form.ThumbnailsTimeValue,
                form.ThumbnailsTimeStep,
                form.ThumbnailsTimeStop
                );

                LaunchJobs(processor, SelectedAssets, form.ThumbnailsJobName, form.ThumbnailsJobPriority, taskname, form.ThumbnailsOutputAssetName, configThumbnails, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
            }
        }

        private void dataGridViewOriginsV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewOriginsV.Columns["State"].Index) // state column
            {
                if (dataGridViewOriginsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    StreamingEndpointState OS = (StreamingEndpointState)dataGridViewOriginsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    Color mycolor;

                    switch (OS)
                    {
                        case StreamingEndpointState.Deleting:
                            mycolor = Color.Red;
                            break;
                        case StreamingEndpointState.Stopping:
                            mycolor = Color.Red;
                            break;
                        case StreamingEndpointState.Starting:
                            mycolor = Color.Blue;
                            break;
                        case StreamingEndpointState.Stopped:
                            mycolor = Color.Red;
                            break;
                        case StreamingEndpointState.Running:
                            mycolor = Color.Black;
                            break;
                        default:
                            mycolor = Color.Black;
                            break;

                    }
                    for (int i = 0; i < dataGridViewOriginsV.Columns.Count; i++) dataGridViewOriginsV.Rows[e.RowIndex].Cells[i].Style.ForeColor = mycolor;

                }
            }
        }

        private void displayOriginInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayOriginInfo();
        }


        private void DoDisplayOriginInfo()
        {
            DoDisplayOriginInfo(ReturnSelectedOrigins().FirstOrDefault());
        }
        private void DoDisplayOriginInfo(IStreamingEndpoint origin)
        {

            StreamingEndpointInformation form = new StreamingEndpointInformation()
            {
                MyOrigin = origin,
                MyContext = _context
            };


            if (form.ShowDialog() == DialogResult.OK)
            {
                if (origin.ScaleUnits != form.GetScaleUnits)
                {
                    ScaleOrigin(origin, form.GetScaleUnits);
                }

                origin.CustomHostNames = form.GetStreamingCustomHostnames;

                if (form.GetStreamingAllowList != null)
                {
                    if (origin.AccessControl == null)
                    {
                        origin.AccessControl = new StreamingEndpointAccessControl();
                    }
                    origin.AccessControl.IPAllowList = form.GetStreamingAllowList;
                }
                else
                {
                    if (origin.AccessControl != null)
                    {
                        origin.AccessControl.IPAllowList = null;
                    }
                }

                if (form.GetStreamingAkamaiList != null)
                {
                    if (origin.AccessControl == null)
                    {
                        origin.AccessControl = new StreamingEndpointAccessControl();
                    }
                    origin.AccessControl.AkamaiSignatureHeaderAuthenticationKeyList = form.GetStreamingAkamaiList;

                }
                else
                {
                    if (origin.AccessControl != null)
                    {
                        origin.AccessControl.AkamaiSignatureHeaderAuthenticationKeyList = null;
                    }
                }

                if (form.MaxCacheAge != null)
                {
                    if (origin.CacheControl == null)
                    {
                        origin.CacheControl = new StreamingEndpointCacheControl();
                    }
                    origin.CacheControl.MaxAge = form.MaxCacheAge;
                }
                else
                {
                    if (origin.CacheControl != null)
                    {
                        origin.CacheControl.MaxAge = null;
                    }
                }

                // Client Access Policy
                if (form.GetOriginClientPolicy != null)
                {
                    if (origin.CrossSiteAccessPolicies == null)
                    {
                        origin.CrossSiteAccessPolicies = new CrossSiteAccessPolicies();
                    }
                    origin.CrossSiteAccessPolicies.ClientAccessPolicy = form.GetOriginClientPolicy;

                }
                else
                {
                    if (origin.CrossSiteAccessPolicies != null)
                    {
                        origin.CrossSiteAccessPolicies.ClientAccessPolicy = null;
                    }
                }





                // Cross domain  Policy
                if (form.GetOriginCrossdomaintPolicy != null)
                {
                    if (origin.CrossSiteAccessPolicies == null)
                    {
                        origin.CrossSiteAccessPolicies = new CrossSiteAccessPolicies();
                    }
                    origin.CrossSiteAccessPolicies.CrossDomainPolicy = form.GetOriginCrossdomaintPolicy;

                }
                else
                {
                    if (origin.CrossSiteAccessPolicies != null)
                    {
                        origin.CrossSiteAccessPolicies.CrossDomainPolicy = null;
                    }
                }

                origin.Description = form.GetOriginDescription;
                UpdateOrigin(origin);
            }
        }

        private void startOriginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStartOrigins();
        }

        private void DoStartOrigins()
        {
            foreach (IStreamingEndpoint myO in ReturnSelectedOrigins())
            {
                StartOrigin(myO);
            }
        }

        private void stopOriginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStopOrigins();
        }

        private void DoStopOrigins()
        {
            foreach (IStreamingEndpoint myO in ReturnSelectedOrigins())
            {
                StopOrigin(myO);
            }
        }

        private void deleteOriginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteOrigins();
        }

        private void DoDeleteOrigins()
        {
            List<IStreamingEndpoint> SelectedOrigins = ReturnSelectedOrigins();
            if (SelectedOrigins.Count > 0)
            {
                string question = (SelectedOrigins.Count == 1) ? "Delete streaming endpoint " + SelectedOrigins[0].Name + " ?" : "Delete these " + SelectedOrigins.Count + " streaming endpoints ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Streaming endpoint(s) deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (IStreamingEndpoint myO in ReturnSelectedOrigins())
                    {
                        DeleteOrigin(myO);
                    }
                }
            }
        }

        private void createOriginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateOrigin();
        }

        private async void DoCreateOrigin()
        {
            CreateStreamingEndpoint form = new CreateStreamingEndpoint() { scaleUnits = 1 };

            if (form.ShowDialog() == DialogResult.OK)
            {
                TextBoxLogWriteLine("Creating streaming endpoint {0}...", form.OriginName);

                var options = new StreamingEndpointCreationOptions()
                {
                    Name = form.OriginName,
                    ScaleUnits = form.scaleUnits,
                    Description = form.OriginDescription
                };

                var STask = OriginExecuteAsync(
                       () =>
                           _context.StreamingEndpoints.CreateAsync(options)
                           , form.OriginName,
                           "created");

                await STask;

                DoRefreshGridOriginV(false);
            }
        }




        private static byte[] GenerateRandomBytes(int length)
        {
            var bytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }


        private void displayChannelInfomationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayChannelInfo();
        }

        private void displayProgramInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDisplayProgramInfo();
        }

        private void dataGridViewLiveV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IChannel channel = GetChannel(dataGridViewChannelsV.Rows[e.RowIndex].Cells[dataGridViewChannelsV.Columns["Id"].Index].Value.ToString());
                if (channel != null)
                {
                    DoDisplayChannelInfo(channel);
                }
            }
        }

        private void dataGridViewProgramV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IProgram program = GetProgram(dataGridViewProgramsV.Rows[e.RowIndex].Cells[dataGridViewProgramsV.Columns["Id"].Index].Value.ToString());
                if (program != null)
                {
                    DoDisplayProgramInfo(program);
                }
            }
        }

        private void startChannelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStartChannels();
        }

        private void stopChannelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStopChannels();
        }

        private void resetChannelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoResetChannels();
        }

        private void deleteChannelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDeleteChannels();
        }

        private void startProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStartPrograms();
        }

        private void stopProgramsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStopPrograms();
        }

        private void deleteProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeletePrograms();
        }

        private void displayOriginInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDisplayOriginInfo();
        }

        private void startOriginsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStartOrigins();
        }

        private void stopOriginsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStopOrigins();
        }

        private void deleteOriginsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDeleteOrigins();
        }

        private void dataGridViewOriginsV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IStreamingEndpoint origin = GetOrigin(dataGridViewOriginsV.Rows[e.RowIndex].Cells[dataGridViewOriginsV.Columns["Id"].Index].Value.ToString());
                if (origin != null)
                {
                    DoDisplayOriginInfo(origin);
                }
            }
        }

        private void withFlashOSMFAzurePlayerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoPlaybackProgram(PlayerType.FlashAzurePage);

        }

        private void DoPlaybackProgram(PlayerType ptype)
        {
            IProgram program = ReturnSelectedPrograms().FirstOrDefault();
            if (program != null)
            {
                ProgramInfo PI = new ProgramInfo(program, _context);
                IEnumerable<Uri> ValidURIs = PI.GetValidURIs();
                if (ValidURIs.FirstOrDefault() != null)
                {
                    AssetInfo.DoPlayBack(ptype, ValidURIs.FirstOrDefault().ToString());
                }
            }
        }

        private void DoPlaybackChannelPreview(PlayerType ptype)
        {
            IChannel channel = ReturnSelectedChannels().FirstOrDefault();
            if (channel != null)
            {
                if (channel.Preview.Endpoints.FirstOrDefault().Url.AbsoluteUri != null)
                {
                    AssetInfo.DoPlayBack(ptype, channel.Preview.Endpoints.FirstOrDefault().Url);
                }
            }
        }

        private void withSilverlightMontoringPlayerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoPlaybackProgram(PlayerType.SilverlightMonitoring);
        }


        private void copyIngestURLToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {

            System.Windows.Forms.Clipboard.SetText(ReturnSelectedChannels().FirstOrDefault().Input.Endpoints.FirstOrDefault().Url.AbsoluteUri);
        }

        private void copyPreviewURLToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(ReturnSelectedChannels().FirstOrDefault().Preview.Endpoints.FirstOrDefault().Url.AbsoluteUri);
        }

        private void generateThumbnailsForTheAssetsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoGenerateThumbnails();
        }

        private void withSilverlightMontoringPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedPrograms().FirstOrDefault().Asset, ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.SilverlightMonitoring, PlayBackLocator.GetSmoothStreamingUri());

        }

        private void withFlashOSMFAzurePlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedPrograms().FirstOrDefault().Asset, ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.FlashAzurePage, PlayBackLocator.GetSmoothStreamingUri());

        }



        private void batchUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoBatchUpload();
        }

        private void DoBatchUpload()
        {
            BathUploadFrame1 form = new BathUploadFrame1();
            if (form.ShowDialog() == DialogResult.OK)
            {
                BathUploadFrame2 form2 = new BathUploadFrame2(form.BatchFolder, form.BatchProcessFiles, form.BatchProcessSubFolders);
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    int index;
                    foreach (string folder in form2.BatchSelectedFolders)
                    {
                        index = DoGridTransferAddItem(string.Format("Upload of folder '{0}'", Path.GetFileName(folder)), TransferType.UploadFromFolder, Properties.Settings.Default.useTransferQueue);
                        Task.Factory.StartNew(() => ProcessUploadFromFolder(folder, index));
                    }
                    foreach (string file in form2.BatchSelectedFiles)
                    {
                        index = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(file) + "'", TransferType.UploadFromFile, Properties.Settings.Default.useTransferQueue);
                        Task.Factory.StartNew(() => ProcessUploadFile(file, index, false));
                    }
                    DotabControlMainSwitch(Constants.TabTransfers);
                    DoRefreshGridAssetV(false);
                }

            }

        }

        private void azureMediaBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://azure.microsoft.com/blog/topics/media-services/");
        }



        private void createProgramToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoCreateProgram();
        }

        private void createChannelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCreateChannel();
        }

        private void withFlashOSMFAzurePlayerToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            DoPlaybackChannelPreview(PlayerType.FlashAzurePage);
        }

        private void withSilverlightMontoringPlayerToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            DoPlaybackChannelPreview(PlayerType.SilverlightMonitoring);
        }

        private void comboBoxTimeProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewProgramsV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();
            if (dataGridViewProgramsV.Initialized)
            {
                DoRefreshGridProgramV(false);
            }
        }

        private void buttonSetFilterProgram_Click(object sender, EventArgs e)
        {
            if (dataGridViewProgramsV.Initialized)
            {
                dataGridViewProgramsV.SearchInName = textBoxSearchNameProgram.Text;
                DoRefreshGridProgramV(false);
            }
        }

        private void comboBoxStatusProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewProgramsV.Initialized)
            {
                dataGridViewProgramsV.FilterState = ((ComboBox)sender).SelectedItem.ToString();
                DoRefreshGridProgramV(false);
            }
        }

        private void comboBoxOrderProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewProgramsV.Initialized)
            {
                dataGridViewProgramsV.OrderItemsInGrid = ((ComboBox)sender).SelectedItem.ToString();
                DoRefreshGridProgramV(false);
            }
        }

        private void createStreamingEndpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateOrigin();
        }


        private void setupDynamicEncryptionForTheAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSetupDynEnc();
        }

        private void DoSetupDynEnc()
        {
            string labelAssetName;
            List<IAsset> SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssets();
            if (SelectedAssets.Count > 0)
            {
                labelAssetName = "Dynamic encryption will be applied for Asset '" + SelectedAssets.FirstOrDefault().Name + "'.";
                if (SelectedAssets.Count > 1)
                {
                    labelAssetName = "Dynamic encryption will applied to the " + SelectedAssets.Count.ToString() + " selected assets.";
                }
                AddDynamicEncryption form = new AddDynamicEncryption(_context)

                    ;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    bool Error = false;
                    string keydeliveryconfig = string.Empty;
                    foreach (IAsset AssetToProcess in SelectedAssets)
                        if (AssetToProcess != null)
                        {
                            if (form.GetDeliveryPolicyType != AssetDeliveryPolicyType.NoDynamicEncryption)  // Dynamic encryption
                            {
                                IContentKey key = null;

                                var contenkeys = AssetToProcess.ContentKeys.Where(c => c.ContentKeyType == form.GetContentKeyType);
                                if (contenkeys.Count() == 0) // no content key existing so we need to create one
                                {
                                    try
                                    {
                                        if (form.GetContentKeyType == ContentKeyType.EnvelopeEncryption) // Envelope
                                        {
                                            key = DynamicEncryption.CreateEnvelopeTypeContentKey(AssetToProcess);
                                        }
                                        else // CENC
                                        {
                                            key = DynamicEncryption.CreateCommonTypeContentKey(AssetToProcess, _context);

                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        // Add useful information to the exception
                                        TextBoxLogWriteLine("There is a problem when creating the content key for '{0}'.", AssetToProcess.Name, true);
                                        TextBoxLogWriteLine(e);
                                        Error = true;
                                    }
                                    if (Error) break;
                                    TextBoxLogWriteLine("Created key {0} for the asset {1} ", key.Id, AssetToProcess.Name);

                                }
                                else // let's use existing content key
                                {
                                    key = contenkeys.FirstOrDefault();
                                    TextBoxLogWriteLine("Existing key {0} will be used for asset {1}.", key.Id, AssetToProcess.Name);
                                }


                                // if CENC, let's build the PlayReady license template
                                if (form.GetContentKeyType == ContentKeyType.CommonEncryption)
                                {
                                    PlayReadyLicense formPlayReady = new PlayReadyLicense();
                                    if (formPlayReady.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                    {
                                        try
                                        {
                                            keydeliveryconfig = DynamicEncryption.ConfigurePlayReadyLicenseTemplate(formPlayReady.GetLicenseTemplate);
                                        }
                                        catch (Exception e)
                                        {
                                            // Add useful information to the exception
                                            TextBoxLogWriteLine("There is a problem when configuring the PlayReady license template.", true);
                                            TextBoxLogWriteLine(e);
                                            Error = true;
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }

                                }



                                try
                                {
                                    switch (form.GetKeyRestrictionType)
                                    {
                                        case ContentKeyRestrictionType.Open:
                                            IContentKeyAuthorizationPolicy pol = DynamicEncryption.AddOpenAuthorizationPolicy(key, (form.GetContentKeyType == ContentKeyType.EnvelopeEncryption) ? ContentKeyDeliveryType.BaselineHttp : ContentKeyDeliveryType.PlayReadyLicense, keydeliveryconfig, _context);
                                            break;

                                        case ContentKeyRestrictionType.TokenRestricted:
                                            string tokenTemplateString = DynamicEncryption.AddTokenRestrictedAuthorizationPolicy(key, form.GetAudienceUri, form.GetIssuerUri, _context);
                                            if (!String.IsNullOrEmpty(tokenTemplateString))
                                            {
                                                // Deserializes a string containing an Xml representation of a TokenRestrictionTemplate
                                                // back into a TokenRestrictionTemplate class instance.
                                                TokenRestrictionTemplate tokenTemplate =
                                                    TokenRestrictionTemplateSerializer.Deserialize(tokenTemplateString);

                                                // Generate a test token based on the data in the given TokenRestrictionTemplate.
                                                // Note, you need to pass the key id Guid because we specified 
                                                // TokenClaim.ContentKeyIdentifierClaim in during the creation of TokenRestrictionTemplate.
                                                Guid rawkey = EncryptionUtils.GetKeyIdAsGuid(key.Id);
                                                string testToken = TokenRestrictionTemplateSerializer.GenerateTestToken(tokenTemplate, null, rawkey);
                                                TextBoxLogWriteLine("The authorization token is:\n{0}", testToken);
                                            }

                                            break;

                                        default:
                                            break;
                                    }
                                }

                                catch (Exception e)
                                {
                                    // Add useful information to the exception
                                    TextBoxLogWriteLine("There is a problem when creating the authorization policy for '{0}'.", AssetToProcess.Name, true);
                                    TextBoxLogWriteLine(e);
                                    Error = true;
                                }
                                if (Error) break;

                                TextBoxLogWriteLine("Created authorization policy for the asset {0} ", key.Id, AssetToProcess.Name);

                                IAssetDeliveryPolicy DelPol = null;

                                var DelPols = _context.AssetDeliveryPolicies
                                    .Where(p => (p.AssetDeliveryProtocol == form.GetAssetDeliveryProtocol) && (p.AssetDeliveryPolicyType == form.GetDeliveryPolicyType));
                                if (form.ForceDeliveryPolicyCreation | DelPols.Count() == 0) // no delivery policy found or user want to force creation
                                {
                                    string name = string.Format("AssetDeliveryPolicy {0} ({1})", form.GetContentKeyType.ToString(), form.GetAssetDeliveryProtocol.ToString());
                                    try
                                    {
                                        if (form.GetDeliveryPolicyType == AssetDeliveryPolicyType.DynamicCommonEncryption) // CENC
                                        {
                                            DelPol = DynamicEncryption.CreateAssetDeliveryPolicyCENC(AssetToProcess, key, form.GetAssetDeliveryProtocol, name, _context);
                                        }
                                        else  // Envelope encryption or no encryption
                                        {
                                            DelPol = DynamicEncryption.CreateAssetDeliveryPolicyAES(AssetToProcess, key, form.GetAssetDeliveryProtocol, name, _context);
                                        }

                                        TextBoxLogWriteLine("Created asset delivery policy {0} for asset {1}.", DelPol.AssetDeliveryPolicyType, AssetToProcess.Name);
                                    }
                                    catch (Exception e)
                                    {
                                        TextBoxLogWriteLine("There is a problem when creating the delivery policy for '{0}'.", AssetToProcess.Name, true);
                                        TextBoxLogWriteLine(e);
                                        Error = true;

                                    }
                                }
                                else // use existing delivery policy
                                {
                                    try
                                    {
                                        AssetToProcess.DeliveryPolicies.Add(DelPols.FirstOrDefault());
                                        TextBoxLogWriteLine("Binded existing asset delivery policy {0} for asset {1}.", DelPols.FirstOrDefault().Id, AssetToProcess.Name);
                                    }

                                    catch (Exception e)
                                    {
                                        TextBoxLogWriteLine("There is a problem when using the delivery policy {0} for '{1}'.", DelPols.FirstOrDefault().Id, AssetToProcess.Name, true);
                                        TextBoxLogWriteLine(e);
                                        Error = true;

                                    }
                                }

                                if (Error) break;

                            }
                            else // No Dynamic encryption
                            {
                                IAssetDeliveryPolicy DelPol = null;

                                var DelPols = _context.AssetDeliveryPolicies
                                   .Where(p => (p.AssetDeliveryProtocol == form.GetAssetDeliveryProtocol) && (p.AssetDeliveryPolicyType == AssetDeliveryPolicyType.NoDynamicEncryption));
                                if (DelPols.Count() == 0) // no delivery policy found or user want to force creation
                                {
                                    try
                                    {
                                        DelPol = DynamicEncryption.CreateAssetDeliveryPolicyNoDynEnc(AssetToProcess, form.GetAssetDeliveryProtocol, _context);
                                        TextBoxLogWriteLine("Created asset delivery policy {0} for asset {1}.", DelPol.AssetDeliveryPolicyType, AssetToProcess.Name);
                                    }
                                    catch (Exception e)
                                    {
                                        // Add useful information to the exception
                                        TextBoxLogWriteLine("There is a problem when creating the delivery policy for '{0}'.", AssetToProcess.Name, true);
                                        TextBoxLogWriteLine(e);
                                        Error = true;
                                    }
                                }
                                else // let's use an existing delivery policy for no dynamic encryption
                                {
                                    try
                                    {
                                        AssetToProcess.DeliveryPolicies.Add(DelPols.FirstOrDefault());
                                        TextBoxLogWriteLine("Binded existing asset delivery policy {0} for asset {1}.", DelPols.FirstOrDefault().Id, AssetToProcess.Name);
                                    }

                                    catch (Exception e)
                                    {
                                        TextBoxLogWriteLine("There is a problem when using the delivery policy {0} for '{1}'.", DelPols.FirstOrDefault().Id, AssetToProcess.Name, true);
                                        TextBoxLogWriteLine(e);
                                        Error = true;
                                    }
                                }

                                if (Error) break;
                            }
                            dataGridViewAssetsV.AnalyzeItemsInBackground();
                        }
                }
            }

        }

        private void richTextBoxLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void clearTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxLog.Clear();
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxLog.SelectionLength > 0)
            {
                System.Windows.Forms.Clipboard.SetText(richTextBoxLog.SelectedText);
            }
            else
            {
                System.Windows.Forms.Clipboard.SetText(richTextBoxLog.Text);
            }

        }

        private void mergeSelectedAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMergeAssetsToNewAsset();
        }

        private void mergeAssetsToANewAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMergeAssetsToNewAsset();
        }

        private void removeDynamicEncryptionForTheAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoRemoveDynEnc();
        }

        private void DoRemoveDynEnc()
        {
            string labelAssetName;

            List<IAsset> SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssets();

            if (SelectedAssets.Count > 0)
            {
                labelAssetName = "Dynamic encryption will be removed for Asset '" + SelectedAssets.FirstOrDefault().Name + "'.";

                if (SelectedAssets.Count > 1)
                {
                    labelAssetName = "Dynamic encryption will removed for these " + SelectedAssets.Count.ToString() + " selected assets.";
                }



                if (MessageBox.Show(labelAssetName, "Dynamic encryption", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    bool Error = false;
                    string keydeliveryconfig = string.Empty;
                    foreach (IAsset AssetToProcess in SelectedAssets)

                        if (AssetToProcess != null)
                        {
                            IAssetDeliveryPolicy DelPol = null;
                            try
                            {
                                foreach (var loc in AssetToProcess.Locators)
                                {
                                    loc.Delete();
                                }
                                List<IAssetDeliveryPolicy> items = AssetToProcess.DeliveryPolicies.ToList(); // let's do a copy of the list in order to do a removal
                                foreach (var item in items)
                                {
                                    AssetToProcess.DeliveryPolicies.Remove(item);
                                }
                            }

                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when deleting the delivery policy or locator for '{0}'.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                            }

                            if (Error) break;
                            TextBoxLogWriteLine("Removed asset delivery policies and locator(s) for asset {0}.", AssetToProcess.Name);

                            dataGridViewAssetsV.AnalyzeItemsInBackground();
                        }
                }
            }
        }

        private void encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithAMESystemPreset();
        }

        private void encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithAMEAdvanced();
        }


        private void encodeAssetsWithImagineCommunicationsZeniumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithZenium();
        }

        private void createALocatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            DoCreateLocator(SelectedAssets);
        }

        private void deleteAllLocatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            DoDeleteAllLocatorsOnAssets(SelectedAssets);
        }

        private void validateTheMultiMP4AssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuValidateMultiMP4Static();
        }

        private void packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoMenuMP4ToSmoothStatic();
        }

        private void packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoMenuPackageSmoothToHLSStatic();
        }

        private void encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoMenuProtectWithPlayReadyStatic();
        }

        private void copyTheOutputURLToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyOutputURLToClipboard();

        }

        private void DoCopyOutputURLToClipboard()
        {
            IProgram program = ReturnSelectedPrograms().FirstOrDefault();
            if (program != null)
            {
                ProgramInfo PI = new ProgramInfo(program, _context);
                IEnumerable<Uri> ValidURIs = PI.GetValidURIs();
                if (ValidURIs.FirstOrDefault() != null)
                {
                    System.Windows.Forms.Clipboard.SetText(ValidURIs.FirstOrDefault().ToString());
                }
            }
        }

        private void withDASHLiveAzurePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaybackProgram(PlayerType.DASHLiveAzure);
        }

        private void jwPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://www.jwplayer.com/partners/azure/");

        }

        private void removeDynamicEncryptionForTheAssetsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoRemoveDynEnc();
        }

        private void setupDynamicEncryptionForTheAssetsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoSetupDynEnc();
        }

        private void withCustomPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.CustomPlayer, PlayBackLocator.GetSmoothStreamingUri());
        }

        private void withCustomPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.CustomPlayer, PlayBackLocator.GetSmoothStreamingUri());
        }


        private void DoMenuCreateLocatorOnPrograms()
        {
            List<IAsset> SelectedAssets = ReturnSelectedPrograms().Select(p => p.Asset).ToList();
            DoCreateLocator(SelectedAssets);
        }

        private void createALocatorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoMenuCreateLocatorOnPrograms();
        }

        private void deleteAllLocatorsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuDeleteAllLocatorsOnPrograms();
        }

        private void DoMenuDeleteAllLocatorsOnPrograms()
        {
            List<IAsset> SelectedAssets = ReturnSelectedPrograms().Select(p => p.Asset).ToList();
            DoDeleteAllLocatorsOnAssets(SelectedAssets);
        }

        private void displayRelatedAssetInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfoOfProgram();
        }

        private void DoMenuDisplayAssetInfoOfProgram()
        {
            List<IAsset> SelectedAssets = ReturnSelectedPrograms().Select(p => p.Asset).ToList();
            if (SelectedAssets.Count > 0)
            {
                DisplayInfo(SelectedAssets.FirstOrDefault());
            }
        }

        private void withCustomPlayerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedPrograms().FirstOrDefault().Asset, ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.CustomPlayer, PlayBackLocator.GetSmoothStreamingUri());
        }

        private void withDASHLiveAzurePlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedPrograms().FirstOrDefault().Asset, ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.DASHLiveAzure, PlayBackLocator.GetMpegDashUri());

        }

        private void withCustomPlayerToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedPrograms().FirstOrDefault().Asset, ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.CustomPlayer, PlayBackLocator.GetSmoothStreamingUri());
        }

        private void displayRelatedAssetInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfoOfProgram();
        }

        private void withMPEGDASHAzurePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.DASHAzurePage, PlayBackLocator.GetMpegDashUri());
        }

        private void withDASHLiveAzurePlayerToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.DASHLiveAzure, PlayBackLocator.GetMpegDashUri());
        }


    }

}



namespace AMSExplorer
{
    /// <summary>
    /// A DataGridViewColumn implementation that provides a column that
    /// will display a progress bar.
    /// </summary>
    public class DataGridViewProgressBarColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewProgressBarColumn()
        {
            // Set the cell template
            CellTemplate = new DataGridViewProgressBarCell();

            // Set the default style padding
            Padding pad = new Padding(
              DataGridViewProgressBarCell.STANDARD_HORIZONTAL_MARGIN,
              DataGridViewProgressBarCell.STANDARD_VERTICAL_MARGIN,
              DataGridViewProgressBarCell.STANDARD_HORIZONTAL_MARGIN,
              DataGridViewProgressBarCell.STANDARD_VERTICAL_MARGIN);
            DefaultCellStyle.Padding = pad;

            // Set the default format
            DefaultCellStyle.Format = "## \\%";
        }


    }

    /// <summary>
    /// A DataGridViewCell class that is used to display a progress bar
    /// within a grid cell.
    /// </summary>
    public class DataGridViewProgressBarCell : DataGridViewTextBoxCell
    {
        /// <summary>
        /// Standard value to use for horizontal margins
        /// </summary>
        internal const int STANDARD_HORIZONTAL_MARGIN = 4;

        /// <summary>
        /// Standard value to use for vertical margins
        /// </summary>
        internal const int STANDARD_VERTICAL_MARGIN = 4;

        /// <summary>
        /// Constructor sets the expected type to int
        /// </summary>
        public DataGridViewProgressBarCell()
        {
            this.ValueType = typeof(int);
        }

        /// <summary>
        /// Paints the content of the cell
        /// </summary>
        protected override void Paint(Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds,
          int rowIndex, DataGridViewElementStates cellState,
          object value, object formattedValue,
          string errorText,
          DataGridViewCellStyle cellStyle,
          DataGridViewAdvancedBorderStyle advancedBorderStyle,
          DataGridViewPaintParts paintParts)
        {
            int leftMargin = STANDARD_HORIZONTAL_MARGIN;
            int rightMargin = STANDARD_HORIZONTAL_MARGIN;
            int topMargin = STANDARD_VERTICAL_MARGIN;
            int bottomMargin = STANDARD_VERTICAL_MARGIN;
            int imgHeight = 1;
            int imgWidth = 1;
            int progressWidth = 1;
            PointF fontPlacement = new PointF(0, 0);

            int progressVal;
            if (value != null)
                //progressVal = (int)value;
                progressVal = (int)((double)value);

            else
                progressVal = 0;

            // Draws the cell grid
            base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

            // Get margins from the style
            if (null != cellStyle)
            {
                leftMargin = cellStyle.Padding.Left;
                rightMargin = cellStyle.Padding.Right;
                topMargin = cellStyle.Padding.Top;
                bottomMargin = cellStyle.Padding.Bottom;
            }

            // Calculate the sizes
            imgHeight = cellBounds.Bottom - cellBounds.Top - (topMargin + bottomMargin);
            imgWidth = cellBounds.Right - cellBounds.Left - (leftMargin + rightMargin);
            if (imgWidth <= 0)
            {
                imgWidth = 1;
            }
            if (imgHeight <= 0)
            {
                imgHeight = 1;
            }

            // Calculate the progress
            progressWidth = (imgWidth * (progressVal) / 100);
            if (progressWidth <= 0)
            {
                if (progressVal > 0)
                {
                    progressWidth = 1;
                }
                else
                {
                    progressWidth = 0;
                }
            }

            // Calculate the font
            if (null != formattedValue)
            {
                SizeF availArea = new SizeF(imgWidth, imgHeight);
                SizeF fontSize = g.MeasureString(formattedValue.ToString(), cellStyle.Font, availArea);

                #region [Font Placement Calc]

                if (null == cellStyle)
                {
                    fontPlacement.Y = cellBounds.Y + topMargin + (((float)imgHeight - fontSize.Height) / 2);
                    fontPlacement.X = cellBounds.X + leftMargin + (((float)imgWidth - fontSize.Width) / 2);
                }
                else
                {
                    // Set the Y vertical position
                    switch (cellStyle.Alignment)
                    {
                        case DataGridViewContentAlignment.BottomCenter:
                        case DataGridViewContentAlignment.BottomLeft:
                        case DataGridViewContentAlignment.BottomRight:
                            {
                                fontPlacement.Y = cellBounds.Y + topMargin + imgHeight - fontSize.Height;
                                break;
                            }
                        case DataGridViewContentAlignment.TopCenter:
                        case DataGridViewContentAlignment.TopLeft:
                        case DataGridViewContentAlignment.TopRight:
                            {
                                fontPlacement.Y = cellBounds.Y + topMargin - fontSize.Height;
                                break;
                            }
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.MiddleLeft:
                        case DataGridViewContentAlignment.MiddleRight:
                        case DataGridViewContentAlignment.NotSet:
                        default:
                            {
                                fontPlacement.Y = cellBounds.Y + topMargin + (((float)imgHeight - fontSize.Height) / 2);
                                break;
                            }
                    }
                    // Set the X horizontal position
                    switch (cellStyle.Alignment)
                    {

                        case DataGridViewContentAlignment.BottomLeft:
                        case DataGridViewContentAlignment.MiddleLeft:
                        case DataGridViewContentAlignment.TopLeft:
                            {
                                fontPlacement.X = cellBounds.X + leftMargin;
                                break;
                            }
                        case DataGridViewContentAlignment.BottomRight:
                        case DataGridViewContentAlignment.MiddleRight:
                        case DataGridViewContentAlignment.TopRight:
                            {
                                fontPlacement.X = cellBounds.X + leftMargin + imgWidth - fontSize.Width;
                                break;
                            }
                        case DataGridViewContentAlignment.BottomCenter:
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.TopCenter:
                        case DataGridViewContentAlignment.NotSet:
                        default:
                            {
                                fontPlacement.X = cellBounds.X + leftMargin + (((float)imgWidth - fontSize.Width) / 2);
                                break;
                            }
                    }
                }
                #endregion [Font Placement Calc]
            }

            // Draw the background
            System.Drawing.Rectangle backRectangle = new System.Drawing.Rectangle(cellBounds.X + leftMargin, cellBounds.Y + topMargin, imgWidth, imgHeight);
            using (SolidBrush backgroundBrush = new SolidBrush(Color.FromKnownColor(KnownColor.LightGray)))
            {
                g.FillRectangle(backgroundBrush, backRectangle);
            }

            // Draw the progress bar
            if (progressWidth > 0)
            {
                System.Drawing.Rectangle progressRectangle = new System.Drawing.Rectangle(cellBounds.X + leftMargin, cellBounds.Y + topMargin, progressWidth, imgHeight);
                using (LinearGradientBrush progressGradientBrush = new LinearGradientBrush(progressRectangle, Color.LightGreen, Color.MediumSeaGreen, LinearGradientMode.Vertical))
                {
                    progressGradientBrush.SetBlendTriangularShape((float).5);
                    g.FillRectangle(progressGradientBrush, progressRectangle);
                }
            }

            // Draw the text
            if (null != formattedValue && null != cellStyle)
            {
                using (SolidBrush fontBrush = new SolidBrush(cellStyle.ForeColor))
                {
                    g.DrawString(formattedValue.ToString(), cellStyle.Font, fontBrush, fontPlacement);
                }
            }
        }
    }

}



namespace AMSExplorer
{
    public static class OrderAssets
    {
        public const string LastModified = "Last modified";
        public const string Name = "Name";
        public const string Size = "Size";
    }

    public static class OrderJobs
    {
        public const string LastModified = "Last modified";
        public const string StartTime = "Start Time";
        public const string EndTime = "End Time";
        public const string ProcessTime = "Duration";
        public const string Name = "Name";
        public const string State = "State";
    }


    public static class StatusAssets
    {
        public const string All = "All";
        public const string Published = "Published";
        public const string PublishedExpired = "Published but expired";
        public const string NotPublished = "Not published";
        public const string Streamable = "Streamable";
        public const string CENC = "CENC static";
        public const string Envelope = "Envelope static";
        public const string Storage = "Storage encrypted";
        public const string SupportDynEnc = "Support dyn. encryption";
        public const string DynEnc = "Dynamic encrypted";
        public const string NotEncrypted = "Not encrypted";
        public const string Empty = "Empty";

    }

    public static class FilterTime
    {
        public const string LastDay = "Last 24 hours";
        public const string LastWeek = "Last week";
        public const string LastMonth = "Last month";
        public const string LastYear = "Last year";
        public const string All = "All";
    }
    public enum TransferState
    {
        Queued = 0,
        Processing = 1,
        Finished = 2,
        Error = 3,
    }

    public enum TransferType
    {
        UploadFromFile = 0,
        UploadFromFolder = 1,
        ImportFromAzureStorage = 2,
        ImportFromHttp = 3,
        ExportToAzureStorage = 4,
        DownloadToLocal = 5
    }

    public static class OrderOrigins
    {
        public const string LastModified = "Last modified";
        public const string StartTime = "Start Time";
        public const string EndTime = "End Time";
        public const string ProcessTime = "Duration";
        public const string Name = "Name";
        public const string State = "State";
    }




    public class DataGridViewAssets : DataGridView
    {
        public int AssetsPerPage
        {
            get
            {
                return _assetsperpage;
            }
            set
            {
                _assetsperpage = value;
            }
        }
        public int PageCount
        {
            get
            {
                return _pagecount;
            }

        }
        public int CurrentPage
        {
            get
            {
                return _currentpage;
            }

        }
        public string OrderAssetsInGrid
        {
            get
            {
                return _orderassets;
            }
            set
            {
                _orderassets = value;
            }

        }
        public bool Initialized
        {
            get
            {
                return _initialized;
            }

        }
        public string SearchInName
        {
            get
            {
                return _searchinname;
            }
            set
            {
                _searchinname = value;
            }
        }
        public string StateFilter
        {
            get
            {
                return _statefilter;
            }
            set
            {
                _statefilter = value;
            }
        }
        public string TimeFilter
        {
            get
            {
                return _timefilter;
            }
            set
            {
                _timefilter = value;
            }
        }
        public int DisplayedCount
        {
            get
            {
                return _MyObservAsset.Count();
            }

        }
        public string _statEnc = "StaticEncryption";
        public string _publication = "Publication";
        public string _dynEnc = "DynamicEncryption";
        public string _statEncMouseOver = "StaticEncryptionMouseOver";
        public string _publicationMouseOver = "PublicationMouseOver";
        public string _dynEncMouseOver = "DynamicEncryptionMouseOver";

        static BindingList<AssetEntry> _MyObservAsset;
        static private int _assetsperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static private bool _neveranalyzed = true;
        static private string _searchinname = "";
        static private string _statefilter = "";
        static private string _timefilter = FilterTime.LastWeek;

        static string _orderassets = OrderAssets.LastModified;
        static BackgroundWorker WorkerAnalyzeAssets;
        static CloudMediaContext _context;
        static Bitmap cancelimage = Bitmaps.cancel;
        static Bitmap envelopeencryptedimage = Bitmaps.envelope_encryption;
        static Bitmap storageencryptedimage = Bitmaps.storage_encryption;
        static Bitmap storagedecryptedimage = Bitmaps.storage_decryption;
        static Bitmap commonencryptedimage = Bitmaps.DRM_protection;
        static Bitmap unsupportedencryptedimage = Bitmaps.help;
        static Bitmap SASlocatorimage = Bitmaps.SAS_locator;
        static Bitmap Streaminglocatorimage = Bitmaps.streaming_locator;
        static Bitmap Redstreamimage = MakeRed(Streaminglocatorimage);
        static Bitmap Reddownloadimage = MakeRed(SASlocatorimage);
        static Bitmap Bluestreamimage = MakeBlue(Streaminglocatorimage);
        static Bitmap Bluedownloadimage = MakeBlue(SASlocatorimage);

        public void Init(CloudMediaContext context)
        {
            Debug.WriteLine("AssetsInit");

            IEnumerable<AssetEntry> assetquery;
            _context = context;

            assetquery = from a in context.Assets orderby a.LastModified descending select new AssetEntry { Name = a.Name, Id = a.Id, LastModified = ((DateTime)a.LastModified).ToLocalTime() };


            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle()
            {
                NullValue = null,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            DataGridViewImageColumn imageCol = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _publication,
                DataPropertyName = _publication,
            };
            this.Columns.Add(imageCol);

            DataGridViewImageColumn imageCol2 = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _statEnc,
                DataPropertyName = _statEnc,
            };
            this.Columns.Add(imageCol2);

            DataGridViewImageColumn imageCol3 = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _dynEnc,
                DataPropertyName = _dynEnc,
            };
            this.Columns.Add(imageCol3);

            BindingList<AssetEntry> MyObservAssethisPage = new BindingList<AssetEntry>(assetquery.Take(0).ToList()); // just to create columns
            this.DataSource = MyObservAssethisPage;

            int lastColumn_sIndex = this.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).DisplayIndex;
            this.Columns[_statEncMouseOver].Visible = false;
            this.Columns[_dynEncMouseOver].Visible = false;
            this.Columns[_publicationMouseOver].Visible = false;

            this.Columns["Type"].HeaderText = "Type (streams nb)";
            this.Columns["LastModified"].HeaderText = "Last modified";
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayAssetIDinGrid;
            this.Columns["SizeLong"].Visible = false;
            this.Columns[_publication].DisplayIndex = lastColumn_sIndex;
            this.Columns[_publication].DefaultCellStyle.NullValue = null;
            this.Columns[_dynEnc].DisplayIndex = lastColumn_sIndex - 1;
            this.Columns[_dynEnc].DefaultCellStyle.NullValue = null;
            this.Columns[_statEnc].DisplayIndex = lastColumn_sIndex - 2;
            this.Columns[_statEnc].DefaultCellStyle.NullValue = null;

            this.Columns[_statEnc].HeaderText = "Static Encryption";
            this.Columns[_dynEnc].HeaderText = "Dynamic Encryption";
            this.Columns["Size"].Width = 70;
            this.Columns[_statEnc].Width = 70;
            this.Columns[_dynEnc].Width = 70;
            this.Columns[_publication].Width = 70;

            WorkerAnalyzeAssets = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true
            };
            WorkerAnalyzeAssets.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerAnalyzeAssets_DoWork);

            _initialized = true;
        }

        private void WorkerAnalyzeAssets_DoWork(object sender, DoWorkEventArgs e)
        {

            Debug.WriteLine("WorkerAnalyzeAssets_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;
            IAsset asset;

            PublishStatus SASLoc;
            PublishStatus OrigLoc;
            int i = 0;

            foreach (AssetEntry AE in _MyObservAsset)
            {

                asset = null;
                try
                {
                    asset = _context.Assets.Where(a => a.Id == AE.Id).FirstOrDefault();
                    if (asset != null)
                    {
                        AssetInfo AR = new AssetInfo(asset);

                        SASLoc = AR.GetPublishedStatus(LocatorType.Sas);
                        OrigLoc = AR.GetPublishedStatus(LocatorType.OnDemandOrigin);
                        AssetBitmapAndText ABR = ReturnStaticProtectedBitmap(asset);
                        AE.StaticEncryption = ABR.bitmap;
                        AE.StaticEncryptionMouseOver = ABR.MouseOverDesc;
                        ABR = BuildBitmapPublication(SASLoc, OrigLoc);
                        AE.Publication = ABR.bitmap;
                        AE.PublicationMouseOver = ABR.MouseOverDesc;
                        AE.Type = AssetInfo.GetAssetType(asset);
                        AE.SizeLong = AR.GetSize();
                        AE.Size = AssetInfo.FormatByteSize(AE.SizeLong);
                        ABR = BuildBitmapDynEncryption(asset);
                        AE.DynamicEncryption = ABR.bitmap;
                        AE.DynamicEncryptionMouseOver = ABR.MouseOverDesc;
                        i++;
                        if (i % 5 == 0)
                        {
                            this.BeginInvoke(new Action(() => this.Refresh()), null);
                        }
                    }
                }
                catch // in some case, we have a timeout on Assets.Where...
                {

                }
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }

            }
            this.BeginInvoke(new Action(() => this.Refresh()), null);
        }

        public void DisplayPage(int page)
        {
            if (!_initialized) return;
            if (!_refreshedatleastonetime) return;

            if (_neveranalyzed) // first display of the page, let's analyzed the assets
            {
                _neveranalyzed = false;
                AnalyzeItemsInBackground();

            }
            if ((page <= _pagecount) && (page > 0))
            {
                _currentpage = page;
                BindingList<AssetEntry> MyObservAssethisPage = new BindingList<AssetEntry>(_MyObservAsset.Skip(_assetsperpage * (page - 1)).Take(_assetsperpage).ToList());

                this.DataSource = MyObservAssethisPage;
            }
        }




        public void RefreshAssets(CloudMediaContext context, int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;
            Debug.WriteLine("RefreshAssets");

            if (WorkerAnalyzeAssets.IsBusy)
            {
                // cancel the analyze.
                WorkerAnalyzeAssets.CancelAsync();

                //System.Threading.Thread.Sleep(1000);

            }
            this.FindForm().Cursor = Cursors.WaitCursor;


            //      Task.Run(() =>
            //   {

            IEnumerable<AssetEntry> assetquery;

            IEnumerable<IAsset> assets;

            if ((!string.IsNullOrEmpty(_timefilter)) && _timefilter != FilterTime.All)
            {
                switch (_timefilter)
                {
                    case FilterTime.LastDay:
                        assets = context.Assets.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(1)))));
                        break;
                    case FilterTime.LastWeek:
                        assets = context.Assets.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(7)))));
                        break;
                    case FilterTime.LastMonth:
                        assets = context.Assets.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(30)))));
                        break;
                    case FilterTime.LastYear:
                        assets = context.Assets.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(365)))));
                        break;

                    default:
                        assets = context.Assets;

                        break;

                }

            }
            else assets = context.Assets;

            if (!string.IsNullOrEmpty(_searchinname))
            {
                string searchlower = _searchinname.ToLower();
                assets = assets.Where(a => (a.Name.ToLower().Contains(searchlower)));
            }

            if ((!string.IsNullOrEmpty(_statefilter)) && _statefilter != StatusAssets.All)
            {
                switch (_statefilter)
                {
                    case StatusAssets.Published:
                        assets = assets.Where(a => a.Locators.Count > 0);
                        break;
                    case StatusAssets.PublishedExpired:
                        assets = assets.Where(a => a.Locators.Count > 0).Where(a => a.Locators.All(l => l.ExpirationDateTime < DateTime.UtcNow));
                        break;
                    case StatusAssets.NotPublished:
                        assets = assets.Where(a => a.Locators.Count == 0);
                        break;
                    case StatusAssets.Storage:
                        assets = assets.Where(a => a.Options == AssetCreationOptions.StorageEncrypted);
                        break;
                    case StatusAssets.CENC:
                        assets = assets.Where(a => a.Options == AssetCreationOptions.CommonEncryptionProtected);
                        break;
                    case StatusAssets.Envelope:
                        assets = assets.Where(a => a.Options == AssetCreationOptions.EnvelopeEncryptionProtected);
                        break;
                    case StatusAssets.NotEncrypted:
                        assets = assets.Where(a => a.Options == AssetCreationOptions.None);
                        break;
                    case StatusAssets.DynEnc:
                        assets = assets.Where(a => a.DeliveryPolicies.Count > 0);
                        break;
                    case StatusAssets.Streamable:
                        assets = assets.Where(a => a.IsStreamable);
                        break;
                    case StatusAssets.SupportDynEnc:
                        assets = assets.Where(a => a.SupportsDynamicEncryption);
                        break;
                    case StatusAssets.Empty:
                        assets = assets.Where(a => a.AssetFiles.Count() == 0);
                        break;
                    default:
                        break;
                }
            }


            _context = context;
            _pagecount = (int)Math.Ceiling(((double)assets.Count()) / ((double)_assetsperpage));
            if (_pagecount == 0) _pagecount = 1; // no asset but one page

            if (pagetodisplay < 1) pagetodisplay = 1;
            if (pagetodisplay > _pagecount) pagetodisplay = _pagecount;
            _currentpage = pagetodisplay;

            var size = new Func<IAsset, long>(AssetInfo.GetSize);


            switch (_orderassets)
            {
                case OrderAssets.LastModified:
                    assetquery = from a in assets orderby a.LastModified descending select new AssetEntry { Name = a.Name, Id = a.Id, Type = null, LastModified = ((DateTime)a.LastModified).ToLocalTime() };
                    break;
                case OrderAssets.Name:
                    assetquery = from a in assets orderby a.Name select new AssetEntry { Name = a.Name, Id = a.Id, Type = null, LastModified = ((DateTime)a.LastModified).ToLocalTime() };
                    break;
                case OrderAssets.Size:
                    assetquery = from a in assets orderby size(a) descending select new AssetEntry { Name = a.Name, Id = a.Id, Type = null, LastModified = ((DateTime)a.LastModified).ToLocalTime() };
                    break;

                default:
                    assetquery = from a in assets orderby a.LastModified descending select new AssetEntry { Name = a.Name, Id = a.Id, Type = null, LastModified = ((DateTime)a.LastModified).ToLocalTime() };
                    break;
            }

            try
            {
                _MyObservAsset = new BindingList<AssetEntry>(assetquery.ToList());
            }
            catch (Exception e)
            {
                MessageBox.Show("There is a problem when connecting to Azure Media Services. Application will close. " + Constants.endline + e.Message);
                Environment.Exit(0);
            }


            BindingList<AssetEntry> MyObservAssethisPage = new BindingList<AssetEntry>(_MyObservAsset.Skip(_assetsperpage * (_currentpage - 1)).Take(_assetsperpage).ToList());
            this.BeginInvoke(new Action(() => this.DataSource = MyObservAssethisPage));
            _refreshedatleastonetime = true;

            AnalyzeItemsInBackground();
            this.FindForm().Cursor = Cursors.Default;

        }




        public void AnalyzeItemsInBackground()
        {
            Task.Run(() =>
       {

           // let's wait a little for the previous worker to cancel if needed
           System.Threading.Thread.Sleep(2000);


           if (WorkerAnalyzeAssets.IsBusy != true)
           {
               // Start the asynchronous operation.
               try
               {
                   WorkerAnalyzeAssets.RunWorkerAsync();
               }
               catch { }
           }

       });

        }

        public static Bitmap MakeRed(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromArgb(originalColor.A, 255, originalColor.G, originalColor.B));
                }
            }

            return newBitmap;
        }

        public static Bitmap MakeBlue(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromArgb(originalColor.A, originalColor.R, originalColor.G, 255));
                }
            }

            return newBitmap;
        }



        private static AssetBitmapAndText BuildBitmapPublication(PublishStatus SASPub, PublishStatus OriginPub)
        {
            // optmized for speed
            AssetBitmapAndText ABT = new AssetBitmapAndText();

            if ((SASPub == PublishStatus.NotPublished) && (OriginPub == PublishStatus.NotPublished)) // IF NOT PUBLISHED
            {
                ABT.MouseOverDesc = "Not published";
                return ABT;
            }


            Bitmap MyPublishedImage;
            Bitmap streami = null;
            Bitmap downloadi = null;
            string streams = string.Empty;
            string downloads = string.Empty;

            switch (SASPub)
            {
                case PublishStatus.PublishedActive:
                    downloadi = SASlocatorimage;
                    downloads = "Active SAS locator";
                    break;

                case PublishStatus.PublishedExpired:
                    downloadi = Reddownloadimage;
                    downloads = "Expired SAS locator";
                    break;

                case PublishStatus.PublishedFuture:
                    downloadi = Bluedownloadimage;
                    downloads = "Future SAS locator";
                    break;

                case PublishStatus.NotPublished:
                    downloadi = null;
                    break;

            }

            switch (OriginPub)
            {
                case PublishStatus.PublishedActive:
                    streami = Streaminglocatorimage;
                    streams = "Active Streaming locator";
                    break;

                case PublishStatus.PublishedExpired:
                    streami = Redstreamimage;
                    streams = "Expired Streaming locator";
                    break;

                case PublishStatus.PublishedFuture:
                    streami = Bluestreamimage;
                    streams = "Future Streaming locator";
                    break;

                case PublishStatus.NotPublished:
                    streami = null;

                    break;

            }

            // IF BOTH PUBLISHED
            if ((SASPub != PublishStatus.NotPublished) && (OriginPub != PublishStatus.NotPublished)) // SAS and Origin
            {
                MyPublishedImage = new Bitmap((downloadi.Width + streami.Width), streami.Height);
                using (Graphics graphicsObject = Graphics.FromImage(MyPublishedImage))
                {
                    graphicsObject.DrawImage(downloadi, new Point(0, 0));
                    graphicsObject.DrawImage(streami, new Point(downloadi.Width, 0));
                }
            }
            else //only one published
            {
                MyPublishedImage = (SASPub != PublishStatus.NotPublished) ? downloadi : streami;
            }
            ABT.bitmap = MyPublishedImage;
            ABT.MouseOverDesc = downloads + (string.IsNullOrEmpty(downloads) ? string.Empty : Constants.endline) + streams;
            return ABT;
        }




        private AssetBitmapAndText ReturnStaticProtectedBitmap(IAsset asset)
        {
            AssetBitmapAndText ABT = new AssetBitmapAndText();


            switch (asset.Options)
            {
                case AssetCreationOptions.StorageEncrypted:
                    ABT.bitmap = storageencryptedimage;
                    ABT.MouseOverDesc = "Storage encrypted";
                    break;

                case AssetCreationOptions.CommonEncryptionProtected:
                    ABT.bitmap = commonencryptedimage;
                    ABT.MouseOverDesc = "CENC encrypted";
                    break;

                case AssetCreationOptions.EnvelopeEncryptionProtected:
                    ABT.bitmap = envelopeencryptedimage;
                    ABT.MouseOverDesc = "Envelope encrypted";
                    break;


                default:
                    break;
            }
            return ABT;
        }



        private static AssetBitmapAndText BuildBitmapDynEncryption(IAsset asset)
        {
            AssetBitmapAndText ABT = new AssetBitmapAndText();
            AssetEncryptionState assetEncryptionState = asset.GetEncryptionState(AssetDeliveryProtocol.SmoothStreaming | AssetDeliveryProtocol.HLS | AssetDeliveryProtocol.Dash);

            switch (assetEncryptionState)
            {
                case AssetEncryptionState.DynamicCommonEncryption:
                    ABT.bitmap = commonencryptedimage;
                    ABT.MouseOverDesc = "Dynamic Common Encryption (CENC)";
                    break;

                case AssetEncryptionState.DynamicEnvelopeEncryption:
                    ABT.bitmap = envelopeencryptedimage;
                    ABT.MouseOverDesc = "Dynamic Envelope Encryption (AES)";
                    break;

                case AssetEncryptionState.NoDynamicEncryption:
                    ABT.bitmap = storagedecryptedimage;
                    ABT.MouseOverDesc = "No Dynamic Encryption";
                    break;

                case AssetEncryptionState.NoSinglePolicyApplies:
                    AssetEncryptionState assetEncryptionStateHLS = asset.GetEncryptionState(AssetDeliveryProtocol.HLS);
                    AssetEncryptionState assetEncryptionStateSmooth = asset.GetEncryptionState(AssetDeliveryProtocol.SmoothStreaming);
                    AssetEncryptionState assetEncryptionStateDash = asset.GetEncryptionState(AssetDeliveryProtocol.Dash);
                    bool CENCEnable = (assetEncryptionStateHLS == AssetEncryptionState.DynamicCommonEncryption | assetEncryptionStateSmooth == AssetEncryptionState.DynamicCommonEncryption | assetEncryptionStateDash == AssetEncryptionState.DynamicCommonEncryption);
                    bool EnvelopeEnable = (assetEncryptionStateHLS == AssetEncryptionState.DynamicEnvelopeEncryption | assetEncryptionStateSmooth == AssetEncryptionState.DynamicEnvelopeEncryption | assetEncryptionStateDash == AssetEncryptionState.DynamicEnvelopeEncryption);
                    if (CENCEnable && EnvelopeEnable)
                    {
                        ABT.bitmap = new Bitmap((envelopeencryptedimage.Width + commonencryptedimage.Width), envelopeencryptedimage.Height);
                        using (Graphics graphicsObject = Graphics.FromImage(ABT.bitmap))
                        {
                            graphicsObject.DrawImage(envelopeencryptedimage, new Point(0, 0));
                            graphicsObject.DrawImage(commonencryptedimage, new Point(envelopeencryptedimage.Width, 0));
                        }
                    }
                    else
                    {
                        ABT.bitmap = CENCEnable ? commonencryptedimage : envelopeencryptedimage;
                    }
                    ABT.MouseOverDesc = "Multiple policies";
                    break;

                default:
                    break;
            }
            return ABT;
        }
    }



    public class DataGridViewJobs : DataGridView
    {
        public int JobssPerPage
        {
            get
            {
                return _jobsperpage;
            }
            set
            {
                _jobsperpage = value;
            }
        }
        public int PageCount
        {
            get
            {
                return _pagecount;
            }

        }
        public int CurrentPage
        {
            get
            {
                return _currentpage;
            }

        }
        public string OrderJobsInGrid
        {
            get
            {
                return _orderjobs;
            }
            set
            {
                _orderjobs = value;
            }

        }
        public string FilterJobsState
        {
            get
            {
                return _filterjobsstate;
            }
            set
            {
                _filterjobsstate = value;
            }

        }
        public string SearchInName
        {
            get
            {
                return _searchinname;
            }
            set
            {
                _searchinname = value;
            }

        }
        public bool Initialized
        {
            get
            {
                return _initialized;
            }

        }
        public string TimeFilter
        {
            get
            {
                return _timefilter;
            }
            set
            {
                _timefilter = value;
            }
        }
        public int DisplayedCount
        {
            get
            {
                return _MyObservJob.Count();
            }

        }
        public IEnumerable<IJob> DisplayedJobs
        {
            get
            {
                return jobs;
            }

        }


        static BindingList<JobEntry> _MyObservJob;
        static BindingList<JobEntry> _MyObservAssethisPage;

        static IEnumerable<IJob> jobs;
        static List<string> _MyListJobsMonitored = new List<string>(); // List of jobds monitored. It contains the jobs ids. Used when a new job is discovered (created by another program) to activate the display of job progress

        static private int _jobsperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static string _orderjobs = OrderJobs.LastModified;
        static string _filterjobsstate = "All";
        static CloudMediaContext _context;
        static private CredentialsEntry _credentials;
        static private string _searchinname = "";
        static private string _timefilter = FilterTime.LastWeek;

        public void Init(CredentialsEntry credentials)
        {
            IEnumerable<JobEntry> jobquery;
            _credentials = credentials;

            _context = Program.ConnectAndGetNewContext(_credentials);
            jobquery = from j in _context.Jobs
                       orderby j.LastModified descending
                       select new JobEntry
                           {
                               Name = j.Name,
                               Id = j.Id,
                               Tasks = j.Tasks.Count,
                               Priority = j.Priority,
                               State = j.State,
                               StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                               EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString() : null,
                               Duration = (j.StartTime.HasValue && j.EndTime.HasValue) ? ((DateTime)j.EndTime).Subtract((DateTime)j.StartTime).ToString(@"hh\:mm\:ss") : string.Empty,
                               Progress = j.GetOverallProgress()
                           };

            DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn()
            {
                Name = "Progress",
                DataPropertyName = "Progress",
                HeaderText = "Progress"
            };

            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();

            this.Columns.Add(col);
            BindingList<JobEntry> MyObservJobInPage = new BindingList<JobEntry>(jobquery.Take(0).ToList());
            this.DataSource = MyObservJobInPage;
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayJobIDinGrid;
            this.Columns["Progress"].DisplayIndex = 5;
            this.Columns["Tasks"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Columns["Tasks"].Width = 50;
            this.Columns["Priority"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Columns["Priority"].Width = 50;

            _initialized = true;
        }


        public void DisplayPage(int page)
        {
            if (!_initialized) return;
            if (!_refreshedatleastonetime) return;

            if ((page <= _pagecount) && (page > 0))
            {
                _currentpage = page;
                this.DataSource = new BindingList<JobEntry>(_MyObservJob.Skip(_jobsperpage * (page - 1)).Take(_jobsperpage).ToList());


            }
        }


        public void Refreshjobs(CloudMediaContext context, int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;

            this.FindForm().Cursor = Cursors.WaitCursor;
            _context = context;


            IEnumerable<JobEntry> jobquery;

            if ((!string.IsNullOrEmpty(_timefilter)) && _timefilter != FilterTime.All)
            {
                switch (_timefilter)
                {
                    case FilterTime.LastDay:
                        jobs = context.Jobs.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(1)))));
                        break;
                    case FilterTime.LastWeek:
                        jobs = context.Jobs.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(7)))));
                        break;
                    case FilterTime.LastMonth:
                        jobs = context.Jobs.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(30)))));
                        break;
                    case FilterTime.LastYear:
                        jobs = context.Jobs.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(365)))));
                        break;

                    default:
                        jobs = context.Jobs;

                        break;

                }

            }
            else jobs = context.Jobs;



            if (_filterjobsstate != "All")
            {
                jobs = jobs.Where(j => j.State == (JobState)Enum.Parse(typeof(JobState), _filterjobsstate));
            }



            if (!string.IsNullOrEmpty(_searchinname))
            {
                string searchlower = _searchinname.ToLower();
                jobs = jobs.Where(j => (j.Name.ToLower().Contains(searchlower)));
            }


            _context = context;
            _pagecount = (int)Math.Ceiling(((double)jobs.Count()) / ((double)_jobsperpage));
            if (_pagecount == 0) _pagecount = 1; // no asset but one page

            if (pagetodisplay < 1) pagetodisplay = 1;
            if (pagetodisplay > _pagecount) pagetodisplay = _pagecount;
            _currentpage = pagetodisplay;

            try
            {
                int c = jobs.Count();
            }
            catch (Exception e)
            {
                MessageBox.Show("There is a problem when connecting to Azure Media Services. Application will close. " + Constants.endline + e.Message);
                Environment.Exit(0);
            }



            switch (_orderjobs)
            {
                case OrderJobs.LastModified:
                    jobquery = from j in jobs
                               orderby j.LastModified descending
                               select new JobEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString() : null,
                                   Duration = (j.StartTime.HasValue && j.EndTime.HasValue) ? ((DateTime)j.EndTime).Subtract((DateTime)j.StartTime).ToString(@"hh\:mm\:ss") : string.Empty,

                                   Progress = j.GetOverallProgress()
                               };
                    break;
                case OrderJobs.Name:
                    jobquery = from j in jobs
                               orderby j.Name
                               select new JobEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString() : null,
                                   Duration = (j.State == JobState.Processing) ? (j.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - j.StartTime)).ToString(@"hh\:mm\:ss") : null) : j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   Progress = j.GetOverallProgress()
                               };
                    break;
                case OrderJobs.EndTime:
                    jobquery = from j in jobs
                               orderby j.EndTime descending
                               select new JobEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString() : null,
                                   Duration = (j.State == JobState.Processing) ? (j.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - j.StartTime)).ToString(@"hh\:mm\:ss") : null) : j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   Progress = j.GetOverallProgress()
                               };
                    break;
                case OrderJobs.ProcessTime:
                    jobquery = from j in jobs
                               orderby j.RunningDuration descending
                               select new JobEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString() : null,
                                   Duration = (j.State == JobState.Processing) ? (j.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - j.StartTime)).ToString(@"hh\:mm\:ss") : null) : j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   Progress = j.GetOverallProgress()
                               };
                    break;
                case OrderJobs.StartTime:
                    jobquery = from j in jobs
                               orderby j.StartTime descending
                               select new JobEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString() : null,
                                   Duration = (j.State == JobState.Processing) ? (j.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - j.StartTime)).ToString(@"hh\:mm\:ss") : null) : j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   Progress = j.GetOverallProgress()
                               };
                    break;
                case OrderJobs.State:
                    jobquery = from j in jobs
                               orderby j.State
                               select new JobEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString() : null,
                                   Duration = (j.State == JobState.Processing) ? (j.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - j.StartTime)).ToString(@"hh\:mm\:ss") : null) : j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   Progress = j.GetOverallProgress()
                               };
                    break;
                default:
                    jobquery = from j in jobs
                               orderby j.LastModified descending
                               select new JobEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString() : null,
                                   Duration = (j.State == JobState.Processing) ? (j.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - j.StartTime)).ToString(@"hh\:mm\:ss") : null) : j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   Progress = j.GetOverallProgress()
                               };
                    break;
            }

            _MyObservJob = new BindingList<JobEntry>(jobquery.ToList());
            _MyObservAssethisPage = new BindingList<JobEntry>(_MyObservJob.Skip(_jobsperpage * (_currentpage - 1)).Take(_jobsperpage).ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservAssethisPage));
            _refreshedatleastonetime = true;
            this.FindForm().Cursor = Cursors.Default;


        }

        // Used to restore job progress. 2 cases: when app is launched or when a job has been created by an external program
        public void RestoreJobProgress()  // when app is launched, we want to restore job progress updates
        {
            Task.Run(() =>
           {
               IEnumerable<IJob> ActiveJobs = _context.Jobs.Where(j => (j.State == JobState.Queued) | (j.State == JobState.Scheduled) | (j.State == JobState.Processing));

               foreach (IJob job in ActiveJobs)
               {
                   if (!_MyListJobsMonitored.Contains(job.Id))
                   {
                       _MyListJobsMonitored.Add(job.Id);
                       this.DoJobProgress(job);
                   }

               }
           });
        }



        public void DoJobProgress(IJob job)
        {
            Task.Run(() =>
            {
                try
                {

                    job = job.StartExecutionProgressTask(
                       j =>
                       {
                           // refesh context and job

                           _context = Program.ConnectAndGetNewContext(_credentials); // needed to get overallprogress
                           /// NET TO RESTORE CONTEXT
                           IJob JobRefreshed = GetJob(j.Id);
                           int index = -1;


                           foreach (JobEntry je in _MyObservJob) // let's search for index
                           {
                               if (je.Id == JobRefreshed.Id)
                               {
                                   index = _MyObservJob.IndexOf(je);
                                   break;
                               }
                           }


                           if (index >= 0) // we found it
                           { // we update the observation collection

                               if ((JobRefreshed.State == JobState.Processing | JobRefreshed.State == JobState.Queued)) // in progress
                               {
                                   double progress = JobRefreshed.GetOverallProgress();
                                   _MyObservJob[index].Progress = progress;
                                   _MyObservJob[index].Priority = JobRefreshed.Priority;
                                   _MyObservJob[index].StartTime = JobRefreshed.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)JobRefreshed.StartTime).ToLocalTime() : null;
                                   _MyObservJob[index].EndTime = JobRefreshed.EndTime.HasValue ? ((DateTime)JobRefreshed.EndTime).ToLocalTime().ToString() : null;

                                   _MyObservJob[index].State = JobRefreshed.State;
                                   Debug.WriteLine(index.ToString() + JobRefreshed.State);

                                   StringBuilder sb = new StringBuilder(); // display percentage for each task for mouse hover (tooltiptext)
                                   foreach (ITask task in JobRefreshed.Tasks) sb.AppendLine(Convert.ToInt32(task.Progress).ToString() + " %");

                                   // let's calculate the estipated time
                                   string ETAstr = "", Durationstr = "";
                                   if (progress > 3)
                                   {
                                       DateTime startlocaltime = ((DateTime)JobRefreshed.StartTime).ToLocalTime();
                                       TimeSpan interval = (TimeSpan)(DateTime.Now - startlocaltime);
                                       DateTime ETA = DateTime.Now.AddSeconds((100d / progress) * interval.TotalSeconds);
                                       TimeSpan estimatedduration = (TimeSpan)(ETA - startlocaltime);

                                       ETAstr = "Estimated: " + ETA.ToString();
                                       Durationstr = "Estimated: " + estimatedduration.ToString(@"hh\:mm\:ss");
                                       _MyObservJob[index].EndTime = ETA.ToString(@"g") + " ?";
                                       _MyObservJob[index].Duration = JobRefreshed.EndTime.HasValue ? ((DateTime)JobRefreshed.EndTime).ToLocalTime().ToString() : estimatedduration.ToString(@"hh\:mm\:ss") + " ?";
                                   }



                                   int indexdisplayed = -1;
                                   foreach (JobEntry je in _MyObservAssethisPage) // let's search for index in the page
                                   {
                                       if (je.Id == JobRefreshed.Id)
                                       {
                                           indexdisplayed = _MyObservAssethisPage.IndexOf(je);
                                           try
                                           {
                                               this.BeginInvoke(new Action(() =>
                                               {
                                                   this.Rows[indexdisplayed].Cells[this.Columns["Progress"].Index].ToolTipText = sb.ToString(); // mouse hover info
                                                   if (progress != 0)
                                                   {
                                                       this.Rows[indexdisplayed].Cells[this.Columns["EndTime"].Index].ToolTipText = ETAstr;// mouse hover info
                                                       this.Rows[indexdisplayed].Cells[this.Columns["Duration"].Index].ToolTipText = Durationstr;// mouse hover info

                                                   }
                                                   this.Refresh();
                                               }));
                                           }
                                           catch
                                           {

                                           }

                                           break;
                                       }
                                   }
                               }
                               else // no progress anymore (finished or failed)
                               {
                                   double progress = JobRefreshed.GetOverallProgress();
                                   _MyObservJob[index].Duration = JobRefreshed.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - JobRefreshed.StartTime)).ToString(@"hh\:mm\:ss") : null;
                                   _MyObservJob[index].Progress = progress;
                                   _MyObservJob[index].Priority = JobRefreshed.Priority;
                                   _MyObservJob[index].StartTime = JobRefreshed.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)JobRefreshed.StartTime).ToLocalTime() : null;
                                   _MyObservJob[index].EndTime = JobRefreshed.EndTime.HasValue ? ((DateTime)JobRefreshed.EndTime).ToLocalTime().ToString() : null;
                                   _MyObservJob[index].State = JobRefreshed.State;
                                   _MyListJobsMonitored.Remove(JobRefreshed.Id); // let's removed from the list of monitored jobs
                                   this.BeginInvoke(new Action(() =>
                                   {
                                       this.Refresh();
                                   }));
                               }
                           }
                       },
                       CancellationToken.None).Result;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Job Monitoring Error");
                }
            });
        }


        static IJob GetJob(string jobId)
        {
            // Use a Linq select query to get an updated 
            // reference by Id. 
            var jobInstance =
                from j in _context.Jobs
                where j.Id == jobId
                select j;
            // Return the job reference as an Ijob. 
            IJob job = jobInstance.FirstOrDefault();

            return job;
        }
    }
}