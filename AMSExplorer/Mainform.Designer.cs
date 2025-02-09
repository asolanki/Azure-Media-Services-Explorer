﻿namespace AMSExplorer
{
    partial class Mainform
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.butPrevPageAsset = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.butNextPageAsset = new System.Windows.Forms.Button();
            this.comboBoxOrderAssets = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxPageAssets = new System.Windows.Forms.ComboBox();
            this.contextMenuStripAssets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuItemAssetDisplayInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemAssetCreateOutlookReportEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemAssetImportFileFromAzure = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemAssetExportAssetFilesToAzureStorage = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemAssetDownloadToLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeAssetsToANewAssetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemAssetRename = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemAssetDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.encodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemZenium = new System.Windows.Forms.ToolStripMenuItem();
            this.packageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateTheMultiMP4AssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuItemThumbnails = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemIndexer = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemStorageDecrypter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuItemGenericProcessor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.publishToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.createALocatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllLocatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuItemAssetPlayback = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemPlaybackWithFlashOSMFAzure = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemPlaybackWithSilverlightMonitoring = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemPlaybackWithMPEGDASHIFReference = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemPlaybackWithMPEGDASHAzure = new System.Windows.Forms.ToolStripMenuItem();
            this.withCustomPlayerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialogDownload = new System.Windows.Forms.FolderBrowserDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.assetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayInformationForAKnownAssetIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createOutlookReportEmailToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFromASingleFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromASingleFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromMultipleFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupAWatchFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromAzureStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromASingleHTTPURLAmazonS3EtcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAssetFilesToAzureStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toAzureStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToLocalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeSelectedAssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedAssetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allAssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDestinationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encodeAssetWithAzureMediaEncoderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encodeAssetsWithAzureMediaEncoderToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.encodeAssetWithImagineZeniumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.generateThumbnailsForTheAssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexAssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorBottomIndex = new System.Windows.Forms.ToolStripSeparator();
            this.validateMultiMP4AssetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packageSmoothStreamingTOHLSstaticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encryptWithPlayReadystaticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decryptAssetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.processAssetsadvancedModeWithToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.dynamicPackagingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encodingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayJobInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputAssetInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputAssetInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayInformationForAKnownJobIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createReportEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.priorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allJobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.publishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.createALocatorForTheAssetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllLocatorsOfTheAssetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.playbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withFlashOSMFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withSilverlightMMPPFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withMPEGDASHIFRefPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withMPEGDASHAzurePlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withDASHLiveAzurePlayerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.withCustomPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liveChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.displayProgramInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayRelatedAssetInformationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.createProgramToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.startProgramsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopProgramsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteProgramsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.originToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayOriginInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createOriginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startOriginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopOriginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteOriginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.samplePlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.azureManagementPortalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.azureMediaBlogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.azureMediaServicesPlayerPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.silverlightMonitoringPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dASHIFHTML5ReferencePlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTML5VideoElementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iVXHLSPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oSMFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jwPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.azureMediaHelpFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.azureMediaServicesMSDNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.azureMediaServicesForumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.butPrevPageJob = new System.Windows.Forms.Button();
            this.butNextPageJob = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxOrderJobs = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxPageJobs = new System.Windows.Forms.ComboBox();
            this.contextMenuStripJobs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuItemJobDisplayInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemJobInputAssetInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemJobOpenOutputAsset = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemJobCreateOutlookReportEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuItemJobChangePriority = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemJobCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemJobDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelWatchFolder = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSE = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridViewTransfer = new System.Windows.Forms.DataGridView();
            this.contextMenuStripTransfers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuItemTransferOpenDest = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageAssets = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxFilterAssetsTime = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxStateAssets = new System.Windows.Forms.ComboBox();
            this.buttonAssetSearch = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxAssetSearch = new System.Windows.Forms.TextBox();
            this.dataGridViewAssetsV = new AMSExplorer.DataGridViewAssets();
            this.tabPageTransfers = new System.Windows.Forms.TabPage();
            this.tabPageJobs = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxFilterJobsTime = new System.Windows.Forms.ComboBox();
            this.buttonJobSearch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxJobSearch = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxStateJobs = new System.Windows.Forms.ComboBox();
            this.dataGridViewJobsV = new AMSExplorer.DataGridViewJobs();
            this.tabPageLive = new System.Windows.Forms.TabPage();
            this.splitContainerLive = new System.Windows.Forms.SplitContainer();
            this.label13 = new System.Windows.Forms.Label();
            this.dataGridViewChannelsV = new AMSExplorer.DataGridViewLiveChannel();
            this.contextMenuStripChannels = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuItemChannelDisplayInfomation = new System.Windows.Forms.ToolStripMenuItem();
            this.createChannelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemChannelStart = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemChannelStop = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemChannelReset = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemChannelDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuItemChannelCopyIngestURLToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemChannelCopyPreviewURLToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.playbackTheProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withFlashOSMFAzurePlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withSilverlightMontoringPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBoxFilterTimeProgram = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBoxStatusProgram = new System.Windows.Forms.ComboBox();
            this.buttonSetFilterProgram = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxSearchNameProgram = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBoxOrderProgram = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dataGridViewProgramsV = new AMSExplorer.DataGridViewLiveProgram();
            this.contextMenuStripPrograms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuItemProgramDisplayInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.displayRelatedAssetInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemProgramStart = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemProgramStop = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemProgramDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.publishToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.createALocatorToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllLocatorsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTheOutputURLToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemProgramPlayback = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemProgramPlaybackWithFlashOSMFAzure = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemProgramPlaybackWithSilverlightMontoring = new System.Windows.Forms.ToolStripMenuItem();
            this.withDASHLiveAzurePlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withCustomPlayerToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageProcessors = new System.Windows.Forms.TabPage();
            this.dataGridViewProcessors = new System.Windows.Forms.DataGridView();
            this.tabPageOrigins = new System.Windows.Forms.TabPage();
            this.dataGridViewOriginsV = new AMSExplorer.DataGridViewOrigins();
            this.contextMenuStripStreaminEndpoint = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuItemOriginDisplayInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.createStreamingEndpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemOriginStart = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemOriginStop = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemOriginDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageChart = new System.Windows.Forms.TabPage();
            this.progressBarChart = new System.Windows.Forms.ProgressBar();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonbuildchart = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.contextMenuStripLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.contextMenuStripAssets.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.contextMenuStripJobs.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransfer)).BeginInit();
            this.contextMenuStripTransfers.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageAssets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAssetsV)).BeginInit();
            this.tabPageTransfers.SuspendLayout();
            this.tabPageJobs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJobsV)).BeginInit();
            this.tabPageLive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLive)).BeginInit();
            this.splitContainerLive.Panel1.SuspendLayout();
            this.splitContainerLive.Panel2.SuspendLayout();
            this.splitContainerLive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChannelsV)).BeginInit();
            this.contextMenuStripChannels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProgramsV)).BeginInit();
            this.contextMenuStripPrograms.SuspendLayout();
            this.tabPageProcessors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessors)).BeginInit();
            this.tabPageOrigins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOriginsV)).BeginInit();
            this.contextMenuStripStreaminEndpoint.SuspendLayout();
            this.tabPageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.contextMenuStripLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // butPrevPageAsset
            // 
            this.butPrevPageAsset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butPrevPageAsset.Enabled = false;
            this.butPrevPageAsset.Location = new System.Drawing.Point(871, 414);
            this.butPrevPageAsset.Name = "butPrevPageAsset";
            this.butPrevPageAsset.Size = new System.Drawing.Size(28, 23);
            this.butPrevPageAsset.TabIndex = 29;
            this.butPrevPageAsset.Text = "<";
            this.butPrevPageAsset.UseVisualStyleBackColor = true;
            this.butPrevPageAsset.Click += new System.EventHandler(this.butPrevPageAsset_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(660, 419);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Order by:";
            // 
            // butNextPageAsset
            // 
            this.butNextPageAsset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butNextPageAsset.Location = new System.Drawing.Point(972, 414);
            this.butNextPageAsset.Name = "butNextPageAsset";
            this.butNextPageAsset.Size = new System.Drawing.Size(28, 23);
            this.butNextPageAsset.TabIndex = 28;
            this.butNextPageAsset.Text = ">";
            this.butNextPageAsset.UseVisualStyleBackColor = true;
            this.butNextPageAsset.Click += new System.EventHandler(this.butNextPageAsset_Click);
            // 
            // comboBoxOrderAssets
            // 
            this.comboBoxOrderAssets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOrderAssets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrderAssets.FormattingEnabled = true;
            this.comboBoxOrderAssets.Location = new System.Drawing.Point(713, 415);
            this.comboBoxOrderAssets.Name = "comboBoxOrderAssets";
            this.comboBoxOrderAssets.Size = new System.Drawing.Size(102, 21);
            this.comboBoxOrderAssets.TabIndex = 13;
            this.comboBoxOrderAssets.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrderAssets_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(830, 419);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Page:";
            // 
            // comboBoxPageAssets
            // 
            this.comboBoxPageAssets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPageAssets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPageAssets.FormattingEnabled = true;
            this.comboBoxPageAssets.Location = new System.Drawing.Point(905, 415);
            this.comboBoxPageAssets.Name = "comboBoxPageAssets";
            this.comboBoxPageAssets.Size = new System.Drawing.Size(61, 21);
            this.comboBoxPageAssets.TabIndex = 11;
            this.comboBoxPageAssets.SelectedIndexChanged += new System.EventHandler(this.comboBoxPageAssets_SelectedIndexChanged);
            // 
            // contextMenuStripAssets
            // 
            this.contextMenuStripAssets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuItemAssetDisplayInfo,
            this.ContextMenuItemAssetCreateOutlookReportEmail,
            this.ContextMenuItemAssetImportFileFromAzure,
            this.ContextMenuItemAssetExportAssetFilesToAzureStorage,
            this.ContextMenuItemAssetDownloadToLocal,
            this.mergeAssetsToANewAssetToolStripMenuItem,
            this.ContextMenuItemAssetRename,
            this.ContextMenuItemAssetDelete,
            this.toolStripSeparator5,
            this.encodeToolStripMenuItem,
            this.packageToolStripMenuItem,
            this.toolStripSeparator7,
            this.ContextMenuItemThumbnails,
            this.ContextMenuItemIndexer,
            this.ContextMenuItemStorageDecrypter,
            this.toolStripSeparator10,
            this.ContextMenuItemGenericProcessor,
            this.toolStripSeparator6,
            this.publishToolStripMenuItem1,
            this.toolStripSeparator8,
            this.ContextMenuItemAssetPlayback});
            this.contextMenuStripAssets.Name = "contextMenuStripAssets";
            this.contextMenuStripAssets.Size = new System.Drawing.Size(350, 386);
            this.contextMenuStripAssets.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripAssets_Opening);
            // 
            // ContextMenuItemAssetDisplayInfo
            // 
            this.ContextMenuItemAssetDisplayInfo.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemAssetDisplayInfo.Image")));
            this.ContextMenuItemAssetDisplayInfo.Name = "ContextMenuItemAssetDisplayInfo";
            this.ContextMenuItemAssetDisplayInfo.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.ContextMenuItemAssetDisplayInfo.Size = new System.Drawing.Size(349, 22);
            this.ContextMenuItemAssetDisplayInfo.Text = "Display information";
            this.ContextMenuItemAssetDisplayInfo.Click += new System.EventHandler(this.toolStripMenuItemDisplayInfo_Click);
            // 
            // ContextMenuItemAssetCreateOutlookReportEmail
            // 
            this.ContextMenuItemAssetCreateOutlookReportEmail.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemAssetCreateOutlookReportEmail.Image")));
            this.ContextMenuItemAssetCreateOutlookReportEmail.Name = "ContextMenuItemAssetCreateOutlookReportEmail";
            this.ContextMenuItemAssetCreateOutlookReportEmail.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.ContextMenuItemAssetCreateOutlookReportEmail.Size = new System.Drawing.Size(349, 22);
            this.ContextMenuItemAssetCreateOutlookReportEmail.Text = "Create Outlook report email";
            this.ContextMenuItemAssetCreateOutlookReportEmail.Click += new System.EventHandler(this.createOutlookReportEmailToolStripMenuItem2_Click);
            // 
            // ContextMenuItemAssetImportFileFromAzure
            // 
            this.ContextMenuItemAssetImportFileFromAzure.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemAssetImportFileFromAzure.Image")));
            this.ContextMenuItemAssetImportFileFromAzure.Name = "ContextMenuItemAssetImportFileFromAzure";
            this.ContextMenuItemAssetImportFileFromAzure.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.ContextMenuItemAssetImportFileFromAzure.Size = new System.Drawing.Size(349, 22);
            this.ContextMenuItemAssetImportFileFromAzure.Text = "Import file(s) from Azure Storage...";
            this.ContextMenuItemAssetImportFileFromAzure.Click += new System.EventHandler(this.toolStripMenuItemUploadFileFromAzure_Click);
            // 
            // ContextMenuItemAssetExportAssetFilesToAzureStorage
            // 
            this.ContextMenuItemAssetExportAssetFilesToAzureStorage.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemAssetExportAssetFilesToAzureStorage.Image")));
            this.ContextMenuItemAssetExportAssetFilesToAzureStorage.Name = "ContextMenuItemAssetExportAssetFilesToAzureStorage";
            this.ContextMenuItemAssetExportAssetFilesToAzureStorage.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.ContextMenuItemAssetExportAssetFilesToAzureStorage.Size = new System.Drawing.Size(349, 22);
            this.ContextMenuItemAssetExportAssetFilesToAzureStorage.Text = "Export asset file(s) to Azure Storage...";
            this.ContextMenuItemAssetExportAssetFilesToAzureStorage.Click += new System.EventHandler(this.copyAssetFilesToAzureStorageToolStripMenuItem1_Click);
            // 
            // ContextMenuItemAssetDownloadToLocal
            // 
            this.ContextMenuItemAssetDownloadToLocal.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemAssetDownloadToLocal.Image")));
            this.ContextMenuItemAssetDownloadToLocal.Name = "ContextMenuItemAssetDownloadToLocal";
            this.ContextMenuItemAssetDownloadToLocal.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.ContextMenuItemAssetDownloadToLocal.Size = new System.Drawing.Size(349, 22);
            this.ContextMenuItemAssetDownloadToLocal.Text = "Download to local...";
            this.ContextMenuItemAssetDownloadToLocal.Click += new System.EventHandler(this.toolStripMenuItemDownloadToLocal_Click);
            // 
            // mergeAssetsToANewAssetToolStripMenuItem
            // 
            this.mergeAssetsToANewAssetToolStripMenuItem.Name = "mergeAssetsToANewAssetToolStripMenuItem";
            this.mergeAssetsToANewAssetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
            this.mergeAssetsToANewAssetToolStripMenuItem.Size = new System.Drawing.Size(349, 22);
            this.mergeAssetsToANewAssetToolStripMenuItem.Text = "Merge assets to a new asset...";
            this.mergeAssetsToANewAssetToolStripMenuItem.Click += new System.EventHandler(this.mergeAssetsToANewAssetToolStripMenuItem_Click);
            // 
            // ContextMenuItemAssetRename
            // 
            this.ContextMenuItemAssetRename.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemAssetRename.Image")));
            this.ContextMenuItemAssetRename.Name = "ContextMenuItemAssetRename";
            this.ContextMenuItemAssetRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.ContextMenuItemAssetRename.Size = new System.Drawing.Size(349, 22);
            this.ContextMenuItemAssetRename.Text = "Rename...";
            this.ContextMenuItemAssetRename.Click += new System.EventHandler(this.toolStripMenuItemRename_Click);
            // 
            // ContextMenuItemAssetDelete
            // 
            this.ContextMenuItemAssetDelete.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemAssetDelete.Image")));
            this.ContextMenuItemAssetDelete.Name = "ContextMenuItemAssetDelete";
            this.ContextMenuItemAssetDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ContextMenuItemAssetDelete.Size = new System.Drawing.Size(349, 22);
            this.ContextMenuItemAssetDelete.Text = "Delete...";
            this.ContextMenuItemAssetDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(346, 6);
            // 
            // encodeToolStripMenuItem
            // 
            this.encodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem,
            this.encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem,
            this.ContextMenuItemZenium});
            this.encodeToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.encoding;
            this.encodeToolStripMenuItem.Name = "encodeToolStripMenuItem";
            this.encodeToolStripMenuItem.Size = new System.Drawing.Size(349, 22);
            this.encodeToolStripMenuItem.Text = "Encode";
            // 
            // encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem
            // 
            this.encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.encoding;
            this.encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem.Name = "encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem";
            this.encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem.Size = new System.Drawing.Size(505, 22);
            this.encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem.Text = "Encode asset(s) with Azure Media Encoder (system preset)...";
            this.encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem.Click += new System.EventHandler(this.encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem_Click);
            // 
            // encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem
            // 
            this.encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.encoding;
            this.encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem.Name = "encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem";
            this.encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem.Size = new System.Drawing.Size(505, 22);
            this.encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem.Text = "Encode asset(s) with Azure Media Encoder (advanced mode with custom preset)...";
            this.encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem.Click += new System.EventHandler(this.encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem_Click);
            // 
            // ContextMenuItemZenium
            // 
            this.ContextMenuItemZenium.Image = global::AMSExplorer.Bitmaps.encoding;
            this.ContextMenuItemZenium.Name = "ContextMenuItemZenium";
            this.ContextMenuItemZenium.Size = new System.Drawing.Size(505, 22);
            this.ContextMenuItemZenium.Text = "Encode asset(s) with Imagine Communications Zenium...";
            this.ContextMenuItemZenium.Click += new System.EventHandler(this.encodeAssetsWithImagineCommunicationsZeniumToolStripMenuItem_Click);
            // 
            // packageToolStripMenuItem
            // 
            this.packageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.validateTheMultiMP4AssetsToolStripMenuItem,
            this.packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem,
            this.packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem,
            this.encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem});
            this.packageToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.packaging;
            this.packageToolStripMenuItem.Name = "packageToolStripMenuItem";
            this.packageToolStripMenuItem.Size = new System.Drawing.Size(349, 22);
            this.packageToolStripMenuItem.Text = "Package";
            // 
            // validateTheMultiMP4AssetsToolStripMenuItem
            // 
            this.validateTheMultiMP4AssetsToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.packaging;
            this.validateTheMultiMP4AssetsToolStripMenuItem.Name = "validateTheMultiMP4AssetsToolStripMenuItem";
            this.validateTheMultiMP4AssetsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.validateTheMultiMP4AssetsToolStripMenuItem.Size = new System.Drawing.Size(441, 22);
            this.validateTheMultiMP4AssetsToolStripMenuItem.Text = "Validate the multi MP4 asset(s)....";
            this.validateTheMultiMP4AssetsToolStripMenuItem.Click += new System.EventHandler(this.validateTheMultiMP4AssetsToolStripMenuItem_Click);
            // 
            // packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem
            // 
            this.packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.packaging;
            this.packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem.Name = "packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem";
            this.packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem.Size = new System.Drawing.Size(441, 22);
            this.packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem.Text = "Package the multi MP4 asset(s) to Smooth Streaming (static)";
            this.packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem.Click += new System.EventHandler(this.packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem_Click_1);
            // 
            // packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem
            // 
            this.packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.packaging;
            this.packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem.Name = "packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem";
            this.packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem.Size = new System.Drawing.Size(441, 22);
            this.packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem.Text = "Package the Smooth Streaming asset(s) to HLS v3 (static)...";
            this.packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem.Click += new System.EventHandler(this.packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem_Click_1);
            // 
            // encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem
            // 
            this.encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem.Name = "encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem";
            this.encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem.Size = new System.Drawing.Size(441, 22);
            this.encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem.Text = "Encrypt the Smooth Streaming asset(s) with PlayReady (static)";
            this.encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem.Click += new System.EventHandler(this.encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(346, 6);
            // 
            // ContextMenuItemThumbnails
            // 
            this.ContextMenuItemThumbnails.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemThumbnails.Image")));
            this.ContextMenuItemThumbnails.Name = "ContextMenuItemThumbnails";
            this.ContextMenuItemThumbnails.Size = new System.Drawing.Size(349, 22);
            this.ContextMenuItemThumbnails.Text = "Generate thumbnails for the asset(s)...";
            this.ContextMenuItemThumbnails.Click += new System.EventHandler(this.generateThumbnailsForTheAssetsToolStripMenuItem1_Click);
            // 
            // ContextMenuItemIndexer
            // 
            this.ContextMenuItemIndexer.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemIndexer.Image")));
            this.ContextMenuItemIndexer.Name = "ContextMenuItemIndexer";
            this.ContextMenuItemIndexer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.ContextMenuItemIndexer.Size = new System.Drawing.Size(349, 22);
            this.ContextMenuItemIndexer.Text = "Index asset(s) with Azure Media Indexer...";
            this.ContextMenuItemIndexer.Click += new System.EventHandler(this.toolStripMenuItemIndex_Click);
            // 
            // ContextMenuItemStorageDecrypter
            // 
            this.ContextMenuItemStorageDecrypter.Image = global::AMSExplorer.Bitmaps.storage_decryption;
            this.ContextMenuItemStorageDecrypter.Name = "ContextMenuItemStorageDecrypter";
            this.ContextMenuItemStorageDecrypter.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.ContextMenuItemStorageDecrypter.Size = new System.Drawing.Size(349, 22);
            this.ContextMenuItemStorageDecrypter.Text = "Storage decrypt the asset(s)...";
            this.ContextMenuItemStorageDecrypter.Click += new System.EventHandler(this.storageDecryptTheAssetsToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(346, 6);
            // 
            // ContextMenuItemGenericProcessor
            // 
            this.ContextMenuItemGenericProcessor.Name = "ContextMenuItemGenericProcessor";
            this.ContextMenuItemGenericProcessor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.ContextMenuItemGenericProcessor.Size = new System.Drawing.Size(349, 22);
            this.ContextMenuItemGenericProcessor.Text = "Process asset(s) with a processor (generic)....";
            this.ContextMenuItemGenericProcessor.Click += new System.EventHandler(this.processAssetsWithAProcessorToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(346, 6);
            // 
            // publishToolStripMenuItem1
            // 
            this.publishToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem1,
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem1,
            this.toolStripSeparator24,
            this.createALocatorToolStripMenuItem,
            this.deleteAllLocatorsToolStripMenuItem});
            this.publishToolStripMenuItem1.Name = "publishToolStripMenuItem1";
            this.publishToolStripMenuItem1.Size = new System.Drawing.Size(349, 22);
            this.publishToolStripMenuItem1.Text = "Publish";
            // 
            // setupDynamicEncryptionForTheAssetsToolStripMenuItem1
            // 
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem1.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem1.Name = "setupDynamicEncryptionForTheAssetsToolStripMenuItem1";
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem1.Size = new System.Drawing.Size(373, 22);
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem1.Text = "Add a dynamic encryption policy for the asset(s)...";
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem1.Click += new System.EventHandler(this.setupDynamicEncryptionForTheAssetsToolStripMenuItem1_Click);
            // 
            // removeDynamicEncryptionForTheAssetsToolStripMenuItem1
            // 
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem1.Image = global::AMSExplorer.Bitmaps.cancel;
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem1.Name = "removeDynamicEncryptionForTheAssetsToolStripMenuItem1";
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem1.Size = new System.Drawing.Size(373, 22);
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem1.Text = "Remove all dynamic encryption policies for the asset(s)...";
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem1.Click += new System.EventHandler(this.removeDynamicEncryptionForTheAssetsToolStripMenuItem1_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(370, 6);
            // 
            // createALocatorToolStripMenuItem
            // 
            this.createALocatorToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.streaming_locator;
            this.createALocatorToolStripMenuItem.Name = "createALocatorToolStripMenuItem";
            this.createALocatorToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.createALocatorToolStripMenuItem.Text = "Create a locator...";
            this.createALocatorToolStripMenuItem.Click += new System.EventHandler(this.createALocatorToolStripMenuItem_Click);
            // 
            // deleteAllLocatorsToolStripMenuItem
            // 
            this.deleteAllLocatorsToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.delete;
            this.deleteAllLocatorsToolStripMenuItem.Name = "deleteAllLocatorsToolStripMenuItem";
            this.deleteAllLocatorsToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.deleteAllLocatorsToolStripMenuItem.Text = "Delete all locators...";
            this.deleteAllLocatorsToolStripMenuItem.Click += new System.EventHandler(this.deleteAllLocatorsToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(346, 6);
            // 
            // ContextMenuItemAssetPlayback
            // 
            this.ContextMenuItemAssetPlayback.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuItemPlaybackWithFlashOSMFAzure,
            this.ContextMenuItemPlaybackWithSilverlightMonitoring,
            this.ContextMenuItemPlaybackWithMPEGDASHIFReference,
            this.ContextMenuItemPlaybackWithMPEGDASHAzure,
            this.withCustomPlayerToolStripMenuItem1});
            this.ContextMenuItemAssetPlayback.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemAssetPlayback.Image")));
            this.ContextMenuItemAssetPlayback.Name = "ContextMenuItemAssetPlayback";
            this.ContextMenuItemAssetPlayback.Size = new System.Drawing.Size(349, 22);
            this.ContextMenuItemAssetPlayback.Text = "Playback the asset";
            this.ContextMenuItemAssetPlayback.DropDownOpening += new System.EventHandler(this.playbackTheAssetToolStripMenuItem_DropDownOpening);
            // 
            // ContextMenuItemPlaybackWithFlashOSMFAzure
            // 
            this.ContextMenuItemPlaybackWithFlashOSMFAzure.Name = "ContextMenuItemPlaybackWithFlashOSMFAzure";
            this.ContextMenuItemPlaybackWithFlashOSMFAzure.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.ContextMenuItemPlaybackWithFlashOSMFAzure.Size = new System.Drawing.Size(308, 22);
            this.ContextMenuItemPlaybackWithFlashOSMFAzure.Text = "with Flash OSMF Azure Player";
            this.ContextMenuItemPlaybackWithFlashOSMFAzure.Click += new System.EventHandler(this.withFlashOSMFAzurePlayerToolStripMenuItem_Click);
            // 
            // ContextMenuItemPlaybackWithSilverlightMonitoring
            // 
            this.ContextMenuItemPlaybackWithSilverlightMonitoring.Name = "ContextMenuItemPlaybackWithSilverlightMonitoring";
            this.ContextMenuItemPlaybackWithSilverlightMonitoring.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.ContextMenuItemPlaybackWithSilverlightMonitoring.Size = new System.Drawing.Size(308, 22);
            this.ContextMenuItemPlaybackWithSilverlightMonitoring.Text = "with Silverlight Monitoring Player";
            this.ContextMenuItemPlaybackWithSilverlightMonitoring.Click += new System.EventHandler(this.withSilverlightMontoringPlayerToolStripMenuItem_Click);
            // 
            // ContextMenuItemPlaybackWithMPEGDASHIFReference
            // 
            this.ContextMenuItemPlaybackWithMPEGDASHIFReference.Name = "ContextMenuItemPlaybackWithMPEGDASHIFReference";
            this.ContextMenuItemPlaybackWithMPEGDASHIFReference.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.ContextMenuItemPlaybackWithMPEGDASHIFReference.Size = new System.Drawing.Size(308, 22);
            this.ContextMenuItemPlaybackWithMPEGDASHIFReference.Text = "with MPEG-DASH IF Reference Player";
            this.ContextMenuItemPlaybackWithMPEGDASHIFReference.Click += new System.EventHandler(this.withMPEGDASHIFReferencePlayerToolStripMenuItem_Click);
            // 
            // ContextMenuItemPlaybackWithMPEGDASHAzure
            // 
            this.ContextMenuItemPlaybackWithMPEGDASHAzure.Name = "ContextMenuItemPlaybackWithMPEGDASHAzure";
            this.ContextMenuItemPlaybackWithMPEGDASHAzure.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.ContextMenuItemPlaybackWithMPEGDASHAzure.Size = new System.Drawing.Size(308, 22);
            this.ContextMenuItemPlaybackWithMPEGDASHAzure.Text = "with MPEG-DASH Azure Player";
            this.ContextMenuItemPlaybackWithMPEGDASHAzure.Click += new System.EventHandler(this.withMPEGDASHAzurePlayerToolStripMenuItem1_Click);
            // 
            // withCustomPlayerToolStripMenuItem1
            // 
            this.withCustomPlayerToolStripMenuItem1.Name = "withCustomPlayerToolStripMenuItem1";
            this.withCustomPlayerToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.withCustomPlayerToolStripMenuItem1.Size = new System.Drawing.Size(308, 22);
            this.withCustomPlayerToolStripMenuItem1.Text = "with Custom Player";
            this.withCustomPlayerToolStripMenuItem1.Click += new System.EventHandler(this.withCustomPlayerToolStripMenuItem1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(167)))), ((int)(((byte)(223)))));
            this.label5.Location = new System.Drawing.Point(11, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(308, 25);
            this.label5.TabIndex = 10;
            this.label5.Text = "Azure Media Services Explorer";
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assetToolStripMenuItem,
            this.transferToolStripMenuItem,
            this.processToolStripMenuItem,
            this.encodingToolStripMenuItem,
            this.liveChannelToolStripMenuItem,
            this.publishToolStripMenuItem,
            this.originToolStripMenuItem,
            this.toolStripMenuItem1,
            this.samplePlayersToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1020, 24);
            this.menuStripMain.TabIndex = 12;
            this.menuStripMain.Text = "menuStrip1";
            this.menuStripMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // assetToolStripMenuItem
            // 
            this.assetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informationToolStripMenuItem,
            this.displayInformationForAKnownAssetIdToolStripMenuItem,
            this.createOutlookReportEmailToolStripMenuItem1,
            this.uploadFromASingleFileToolStripMenuItem,
            this.importToolStripMenuItem,
            this.copyAssetFilesToAzureStorageToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.mergeSelectedAssetsToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.assetToolStripMenuItem.Name = "assetToolStripMenuItem";
            this.assetToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.assetToolStripMenuItem.Text = "Asset";
            this.assetToolStripMenuItem.DropDownOpening += new System.EventHandler(this.toolStripMenuAsset_DropDownOpening);
            this.assetToolStripMenuItem.Click += new System.EventHandler(this.assetToolStripMenuItem_Click);
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("informationToolStripMenuItem.Image")));
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.informationToolStripMenuItem.Text = "Display information";
            this.informationToolStripMenuItem.Click += new System.EventHandler(this.informationToolStripMenuItem_Click);
            // 
            // displayInformationForAKnownAssetIdToolStripMenuItem
            // 
            this.displayInformationForAKnownAssetIdToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("displayInformationForAKnownAssetIdToolStripMenuItem.Image")));
            this.displayInformationForAKnownAssetIdToolStripMenuItem.Name = "displayInformationForAKnownAssetIdToolStripMenuItem";
            this.displayInformationForAKnownAssetIdToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.displayInformationForAKnownAssetIdToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.displayInformationForAKnownAssetIdToolStripMenuItem.Text = "Display information for a known Asset Id";
            this.displayInformationForAKnownAssetIdToolStripMenuItem.Click += new System.EventHandler(this.displayInformationForAKnownAssetIdToolStripMenuItem_Click);
            // 
            // createOutlookReportEmailToolStripMenuItem1
            // 
            this.createOutlookReportEmailToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("createOutlookReportEmailToolStripMenuItem1.Image")));
            this.createOutlookReportEmailToolStripMenuItem1.Name = "createOutlookReportEmailToolStripMenuItem1";
            this.createOutlookReportEmailToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.createOutlookReportEmailToolStripMenuItem1.Size = new System.Drawing.Size(334, 22);
            this.createOutlookReportEmailToolStripMenuItem1.Text = "Create Outlook report email";
            this.createOutlookReportEmailToolStripMenuItem1.Click += new System.EventHandler(this.createOutlookReportEmailToolStripMenuItem1_Click);
            // 
            // uploadFromASingleFileToolStripMenuItem
            // 
            this.uploadFromASingleFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromASingleFileToolStripMenuItem,
            this.fromMultipleFilesToolStripMenuItem,
            this.batchUploadToolStripMenuItem,
            this.setupAWatchFolderToolStripMenuItem});
            this.uploadFromASingleFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("uploadFromASingleFileToolStripMenuItem.Image")));
            this.uploadFromASingleFileToolStripMenuItem.Name = "uploadFromASingleFileToolStripMenuItem";
            this.uploadFromASingleFileToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.uploadFromASingleFileToolStripMenuItem.Text = "Upload";
            // 
            // fromASingleFileToolStripMenuItem
            // 
            this.fromASingleFileToolStripMenuItem.Name = "fromASingleFileToolStripMenuItem";
            this.fromASingleFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.fromASingleFileToolStripMenuItem.Size = new System.Drawing.Size(364, 22);
            this.fromASingleFileToolStripMenuItem.Text = "From local files (single file assets)...";
            this.fromASingleFileToolStripMenuItem.Click += new System.EventHandler(this.fromASingleFileToolStripMenuItem_Click);
            // 
            // fromMultipleFilesToolStripMenuItem
            // 
            this.fromMultipleFilesToolStripMenuItem.Name = "fromMultipleFilesToolStripMenuItem";
            this.fromMultipleFilesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.U)));
            this.fromMultipleFilesToolStripMenuItem.Size = new System.Drawing.Size(364, 22);
            this.fromMultipleFilesToolStripMenuItem.Text = "From a local folder (multiple files asset)...";
            this.fromMultipleFilesToolStripMenuItem.Click += new System.EventHandler(this.fromMultipleFilesToolStripMenuItem_Click);
            // 
            // batchUploadToolStripMenuItem
            // 
            this.batchUploadToolStripMenuItem.Name = "batchUploadToolStripMenuItem";
            this.batchUploadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.U)));
            this.batchUploadToolStripMenuItem.Size = new System.Drawing.Size(364, 22);
            this.batchUploadToolStripMenuItem.Text = "Batch upload...";
            this.batchUploadToolStripMenuItem.Click += new System.EventHandler(this.batchUploadToolStripMenuItem_Click);
            // 
            // setupAWatchFolderToolStripMenuItem
            // 
            this.setupAWatchFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("setupAWatchFolderToolStripMenuItem.Image")));
            this.setupAWatchFolderToolStripMenuItem.Name = "setupAWatchFolderToolStripMenuItem";
            this.setupAWatchFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.setupAWatchFolderToolStripMenuItem.Size = new System.Drawing.Size(364, 22);
            this.setupAWatchFolderToolStripMenuItem.Text = "Setup a watch folder";
            this.setupAWatchFolderToolStripMenuItem.Click += new System.EventHandler(this.setupAWatchFolderToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromAzureStorageToolStripMenuItem,
            this.fromASingleHTTPURLAmazonS3EtcToolStripMenuItem});
            this.importToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importToolStripMenuItem.Image")));
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // fromAzureStorageToolStripMenuItem
            // 
            this.fromAzureStorageToolStripMenuItem.Name = "fromAzureStorageToolStripMenuItem";
            this.fromAzureStorageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.fromAzureStorageToolStripMenuItem.Size = new System.Drawing.Size(340, 22);
            this.fromAzureStorageToolStripMenuItem.Text = "From Azure Storage...";
            this.fromAzureStorageToolStripMenuItem.Click += new System.EventHandler(this.fromAzureStorageToolStripMenuItem_Click);
            // 
            // fromASingleHTTPURLAmazonS3EtcToolStripMenuItem
            // 
            this.fromASingleHTTPURLAmazonS3EtcToolStripMenuItem.Name = "fromASingleHTTPURLAmazonS3EtcToolStripMenuItem";
            this.fromASingleHTTPURLAmazonS3EtcToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.fromASingleHTTPURLAmazonS3EtcToolStripMenuItem.Size = new System.Drawing.Size(340, 22);
            this.fromASingleHTTPURLAmazonS3EtcToolStripMenuItem.Text = "From a single HTTP URL (Amazon S3, etc)...";
            this.fromASingleHTTPURLAmazonS3EtcToolStripMenuItem.Click += new System.EventHandler(this.fromASingleHTTPURLAmazonS3EtcToolStripMenuItem_Click);
            // 
            // copyAssetFilesToAzureStorageToolStripMenuItem
            // 
            this.copyAssetFilesToAzureStorageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toAzureStorageToolStripMenuItem,
            this.downloadToLocalToolStripMenuItem1});
            this.copyAssetFilesToAzureStorageToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyAssetFilesToAzureStorageToolStripMenuItem.Image")));
            this.copyAssetFilesToAzureStorageToolStripMenuItem.Name = "copyAssetFilesToAzureStorageToolStripMenuItem";
            this.copyAssetFilesToAzureStorageToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.copyAssetFilesToAzureStorageToolStripMenuItem.Text = "Export";
            // 
            // toAzureStorageToolStripMenuItem
            // 
            this.toAzureStorageToolStripMenuItem.Name = "toAzureStorageToolStripMenuItem";
            this.toAzureStorageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.toAzureStorageToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.toAzureStorageToolStripMenuItem.Text = "To Azure Storage...";
            this.toAzureStorageToolStripMenuItem.Click += new System.EventHandler(this.toAzureStorageToolStripMenuItem_Click);
            // 
            // downloadToLocalToolStripMenuItem1
            // 
            this.downloadToLocalToolStripMenuItem1.Name = "downloadToLocalToolStripMenuItem1";
            this.downloadToLocalToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.downloadToLocalToolStripMenuItem1.Size = new System.Drawing.Size(246, 22);
            this.downloadToLocalToolStripMenuItem1.Text = "Download to local...";
            this.downloadToLocalToolStripMenuItem1.Click += new System.EventHandler(this.downloadToLocalToolStripMenuItem1_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("renameToolStripMenuItem.Image")));
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.renameToolStripMenuItem.Text = "Rename...";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // mergeSelectedAssetsToolStripMenuItem
            // 
            this.mergeSelectedAssetsToolStripMenuItem.Name = "mergeSelectedAssetsToolStripMenuItem";
            this.mergeSelectedAssetsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
            this.mergeSelectedAssetsToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.mergeSelectedAssetsToolStripMenuItem.Text = "Merge assets to a new asset...";
            this.mergeSelectedAssetsToolStripMenuItem.Click += new System.EventHandler(this.mergeSelectedAssetsToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedAssetToolStripMenuItem,
            this.allAssetsToolStripMenuItem});
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.deleteToolStripMenuItem.Text = "Delete...";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // selectedAssetToolStripMenuItem
            // 
            this.selectedAssetToolStripMenuItem.Name = "selectedAssetToolStripMenuItem";
            this.selectedAssetToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.selectedAssetToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.selectedAssetToolStripMenuItem.Text = "Selected asset(s)...";
            this.selectedAssetToolStripMenuItem.Click += new System.EventHandler(this.selectedAssetToolStripMenuItem_Click);
            // 
            // allAssetsToolStripMenuItem
            // 
            this.allAssetsToolStripMenuItem.Name = "allAssetsToolStripMenuItem";
            this.allAssetsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Delete)));
            this.allAssetsToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.allAssetsToolStripMenuItem.Text = "All assets...";
            this.allAssetsToolStripMenuItem.Click += new System.EventHandler(this.allAssetsToolStripMenuItem_Click);
            // 
            // transferToolStripMenuItem
            // 
            this.transferToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDestinationToolStripMenuItem});
            this.transferToolStripMenuItem.Enabled = false;
            this.transferToolStripMenuItem.Name = "transferToolStripMenuItem";
            this.transferToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.transferToolStripMenuItem.Text = "Transfer";
            // 
            // openDestinationToolStripMenuItem
            // 
            this.openDestinationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openDestinationToolStripMenuItem.Image")));
            this.openDestinationToolStripMenuItem.Name = "openDestinationToolStripMenuItem";
            this.openDestinationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.openDestinationToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openDestinationToolStripMenuItem.Text = "Open destination";
            this.openDestinationToolStripMenuItem.Click += new System.EventHandler(this.openDestinationToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encodeAssetWithAzureMediaEncoderToolStripMenuItem,
            this.encodeAssetsWithAzureMediaEncoderToolStripMenuItem2,
            this.toolStripSeparator2,
            this.encodeAssetWithImagineZeniumToolStripMenuItem,
            this.toolStripSeparator17,
            this.generateThumbnailsForTheAssetsToolStripMenuItem,
            this.indexAssetsToolStripMenuItem,
            this.toolStripSeparatorBottomIndex,
            this.validateMultiMP4AssetToolStripMenuItem,
            this.packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem,
            this.packageSmoothStreamingTOHLSstaticToolStripMenuItem,
            this.encryptWithPlayReadystaticToolStripMenuItem,
            this.decryptAssetToolStripMenuItem,
            this.toolStripSeparator3,
            this.processAssetsadvancedModeWithToolStripMenuItem,
            this.toolStripSeparator4,
            this.dynamicPackagingToolStripMenuItem});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // encodeAssetWithAzureMediaEncoderToolStripMenuItem
            // 
            this.encodeAssetWithAzureMediaEncoderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("encodeAssetWithAzureMediaEncoderToolStripMenuItem.Image")));
            this.encodeAssetWithAzureMediaEncoderToolStripMenuItem.Name = "encodeAssetWithAzureMediaEncoderToolStripMenuItem";
            this.encodeAssetWithAzureMediaEncoderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.encodeAssetWithAzureMediaEncoderToolStripMenuItem.Size = new System.Drawing.Size(577, 22);
            this.encodeAssetWithAzureMediaEncoderToolStripMenuItem.Text = "Encode asset(s) with Azure Media Encoder (system preset)...";
            this.encodeAssetWithAzureMediaEncoderToolStripMenuItem.Click += new System.EventHandler(this.encodeAssetWithAzureMediaEncoderToolStripMenuItem_Click);
            // 
            // encodeAssetsWithAzureMediaEncoderToolStripMenuItem2
            // 
            this.encodeAssetsWithAzureMediaEncoderToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("encodeAssetsWithAzureMediaEncoderToolStripMenuItem2.Image")));
            this.encodeAssetsWithAzureMediaEncoderToolStripMenuItem2.Name = "encodeAssetsWithAzureMediaEncoderToolStripMenuItem2";
            this.encodeAssetsWithAzureMediaEncoderToolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.encodeAssetsWithAzureMediaEncoderToolStripMenuItem2.Size = new System.Drawing.Size(577, 22);
            this.encodeAssetsWithAzureMediaEncoderToolStripMenuItem2.Text = "Encode asset(s) with Azure Media Encoder (advanced mode with custom preset)...";
            this.encodeAssetsWithAzureMediaEncoderToolStripMenuItem2.Click += new System.EventHandler(this.encodeAssetsWithAzureMediaEncoderToolStripMenuItem2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(574, 6);
            // 
            // encodeAssetWithImagineZeniumToolStripMenuItem
            // 
            this.encodeAssetWithImagineZeniumToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("encodeAssetWithImagineZeniumToolStripMenuItem.Image")));
            this.encodeAssetWithImagineZeniumToolStripMenuItem.Name = "encodeAssetWithImagineZeniumToolStripMenuItem";
            this.encodeAssetWithImagineZeniumToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.encodeAssetWithImagineZeniumToolStripMenuItem.Size = new System.Drawing.Size(577, 22);
            this.encodeAssetWithImagineZeniumToolStripMenuItem.Text = "Encode asset(s) with Imagine Communications Zenium...";
            this.encodeAssetWithImagineZeniumToolStripMenuItem.Click += new System.EventHandler(this.encodeAssetWithDigitalRapidsKayakCloudEngineToolStripMenuItem_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(574, 6);
            // 
            // generateThumbnailsForTheAssetsToolStripMenuItem
            // 
            this.generateThumbnailsForTheAssetsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("generateThumbnailsForTheAssetsToolStripMenuItem.Image")));
            this.generateThumbnailsForTheAssetsToolStripMenuItem.Name = "generateThumbnailsForTheAssetsToolStripMenuItem";
            this.generateThumbnailsForTheAssetsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.generateThumbnailsForTheAssetsToolStripMenuItem.Size = new System.Drawing.Size(577, 22);
            this.generateThumbnailsForTheAssetsToolStripMenuItem.Text = "Generate thumbnails for the asset(s)...";
            this.generateThumbnailsForTheAssetsToolStripMenuItem.Click += new System.EventHandler(this.generateThumbnailsForTheAssetsToolStripMenuItem_Click);
            // 
            // indexAssetsToolStripMenuItem
            // 
            this.indexAssetsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("indexAssetsToolStripMenuItem.Image")));
            this.indexAssetsToolStripMenuItem.Name = "indexAssetsToolStripMenuItem";
            this.indexAssetsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.indexAssetsToolStripMenuItem.Size = new System.Drawing.Size(577, 22);
            this.indexAssetsToolStripMenuItem.Text = "Index asset(s) with Azure Media Indexer...";
            this.indexAssetsToolStripMenuItem.Click += new System.EventHandler(this.indexAssetsToolStripMenuItem_Click);
            // 
            // toolStripSeparatorBottomIndex
            // 
            this.toolStripSeparatorBottomIndex.Name = "toolStripSeparatorBottomIndex";
            this.toolStripSeparatorBottomIndex.Size = new System.Drawing.Size(574, 6);
            // 
            // validateMultiMP4AssetToolStripMenuItem
            // 
            this.validateMultiMP4AssetToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("validateMultiMP4AssetToolStripMenuItem.Image")));
            this.validateMultiMP4AssetToolStripMenuItem.Name = "validateMultiMP4AssetToolStripMenuItem";
            this.validateMultiMP4AssetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.validateMultiMP4AssetToolStripMenuItem.Size = new System.Drawing.Size(577, 22);
            this.validateMultiMP4AssetToolStripMenuItem.Text = "Validate the multi MP4 asset(s)....";
            this.validateMultiMP4AssetToolStripMenuItem.Click += new System.EventHandler(this.validateMultiMP4AssetToolStripMenuItem_Click);
            // 
            // packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem
            // 
            this.packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem.Image")));
            this.packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem.Name = "packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem";
            this.packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem.Size = new System.Drawing.Size(577, 22);
            this.packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem.Text = "Package the multi MP4 asset(s) to Smooth Streaming (static)";
            this.packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem.Click += new System.EventHandler(this.packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem_Click);
            // 
            // packageSmoothStreamingTOHLSstaticToolStripMenuItem
            // 
            this.packageSmoothStreamingTOHLSstaticToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("packageSmoothStreamingTOHLSstaticToolStripMenuItem.Image")));
            this.packageSmoothStreamingTOHLSstaticToolStripMenuItem.Name = "packageSmoothStreamingTOHLSstaticToolStripMenuItem";
            this.packageSmoothStreamingTOHLSstaticToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.packageSmoothStreamingTOHLSstaticToolStripMenuItem.Size = new System.Drawing.Size(577, 22);
            this.packageSmoothStreamingTOHLSstaticToolStripMenuItem.Text = "Package the Smooth Streaming asset(s) to HLS v3 (static)...";
            this.packageSmoothStreamingTOHLSstaticToolStripMenuItem.Click += new System.EventHandler(this.packageSmoothStreamingTOHLSstaticToolStripMenuItem_Click);
            // 
            // encryptWithPlayReadystaticToolStripMenuItem
            // 
            this.encryptWithPlayReadystaticToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("encryptWithPlayReadystaticToolStripMenuItem.Image")));
            this.encryptWithPlayReadystaticToolStripMenuItem.Name = "encryptWithPlayReadystaticToolStripMenuItem";
            this.encryptWithPlayReadystaticToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.encryptWithPlayReadystaticToolStripMenuItem.Size = new System.Drawing.Size(577, 22);
            this.encryptWithPlayReadystaticToolStripMenuItem.Text = "Encrypt the Smooth Streaming asset(s) with PlayReady (static)";
            this.encryptWithPlayReadystaticToolStripMenuItem.Click += new System.EventHandler(this.encryptWithPlayReadystaticToolStripMenuItem_Click);
            // 
            // decryptAssetToolStripMenuItem
            // 
            this.decryptAssetToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.storage_decryption;
            this.decryptAssetToolStripMenuItem.Name = "decryptAssetToolStripMenuItem";
            this.decryptAssetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.decryptAssetToolStripMenuItem.Size = new System.Drawing.Size(577, 22);
            this.decryptAssetToolStripMenuItem.Text = "Storage decrypt the asset(s)...";
            this.decryptAssetToolStripMenuItem.Click += new System.EventHandler(this.decryptAssetToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(574, 6);
            // 
            // processAssetsadvancedModeWithToolStripMenuItem
            // 
            this.processAssetsadvancedModeWithToolStripMenuItem.Name = "processAssetsadvancedModeWithToolStripMenuItem";
            this.processAssetsadvancedModeWithToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.processAssetsadvancedModeWithToolStripMenuItem.Size = new System.Drawing.Size(577, 22);
            this.processAssetsadvancedModeWithToolStripMenuItem.Text = "Process asset(s) with a processor (generic)...";
            this.processAssetsadvancedModeWithToolStripMenuItem.Click += new System.EventHandler(this.processAssetsadvancedModeWithToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(574, 6);
            // 
            // dynamicPackagingToolStripMenuItem
            // 
            this.dynamicPackagingToolStripMenuItem.Name = "dynamicPackagingToolStripMenuItem";
            this.dynamicPackagingToolStripMenuItem.Size = new System.Drawing.Size(577, 22);
            this.dynamicPackagingToolStripMenuItem.Text = "Dynamic Packaging (Smooth, HLS, DASH)...";
            this.dynamicPackagingToolStripMenuItem.Click += new System.EventHandler(this.dynamicPackagingToolStripMenuItem_Click);
            // 
            // encodingToolStripMenuItem
            // 
            this.encodingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayJobInformationToolStripMenuItem,
            this.inputAssetInformationToolStripMenuItem,
            this.outputAssetInformationToolStripMenuItem,
            this.displayInformationForAKnownJobIdToolStripMenuItem,
            this.createReportEmailToolStripMenuItem,
            this.toolStripSeparator11,
            this.priorityToolStripMenuItem,
            this.cancelJobToolStripMenuItem,
            this.deleteToolStripMenuItem2});
            this.encodingToolStripMenuItem.Enabled = false;
            this.encodingToolStripMenuItem.Name = "encodingToolStripMenuItem";
            this.encodingToolStripMenuItem.ShortcutKeyDisplayString = "J";
            this.encodingToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.encodingToolStripMenuItem.Text = "Job";
            // 
            // displayJobInformationToolStripMenuItem
            // 
            this.displayJobInformationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("displayJobInformationToolStripMenuItem.Image")));
            this.displayJobInformationToolStripMenuItem.Name = "displayJobInformationToolStripMenuItem";
            this.displayJobInformationToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.displayJobInformationToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.displayJobInformationToolStripMenuItem.Text = "Display information";
            this.displayJobInformationToolStripMenuItem.Click += new System.EventHandler(this.displayJobInformationToolStripMenuItem_Click);
            // 
            // inputAssetInformationToolStripMenuItem
            // 
            this.inputAssetInformationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("inputAssetInformationToolStripMenuItem.Image")));
            this.inputAssetInformationToolStripMenuItem.Name = "inputAssetInformationToolStripMenuItem";
            this.inputAssetInformationToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.inputAssetInformationToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.inputAssetInformationToolStripMenuItem.Text = "Input asset information";
            this.inputAssetInformationToolStripMenuItem.Click += new System.EventHandler(this.inputAssetInformationToolStripMenuItem_Click);
            // 
            // outputAssetInformationToolStripMenuItem
            // 
            this.outputAssetInformationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("outputAssetInformationToolStripMenuItem.Image")));
            this.outputAssetInformationToolStripMenuItem.Name = "outputAssetInformationToolStripMenuItem";
            this.outputAssetInformationToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.outputAssetInformationToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.outputAssetInformationToolStripMenuItem.Text = "Output asset information";
            this.outputAssetInformationToolStripMenuItem.Click += new System.EventHandler(this.outputAssetInformationToolStripMenuItem_Click);
            // 
            // displayInformationForAKnownJobIdToolStripMenuItem
            // 
            this.displayInformationForAKnownJobIdToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("displayInformationForAKnownJobIdToolStripMenuItem.Image")));
            this.displayInformationForAKnownJobIdToolStripMenuItem.Name = "displayInformationForAKnownJobIdToolStripMenuItem";
            this.displayInformationForAKnownJobIdToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.displayInformationForAKnownJobIdToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.displayInformationForAKnownJobIdToolStripMenuItem.Text = "Display information for a known Job Id";
            this.displayInformationForAKnownJobIdToolStripMenuItem.Click += new System.EventHandler(this.displayInformationForAKnownJobIdToolStripMenuItem_Click);
            // 
            // createReportEmailToolStripMenuItem
            // 
            this.createReportEmailToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createReportEmailToolStripMenuItem.Image")));
            this.createReportEmailToolStripMenuItem.Name = "createReportEmailToolStripMenuItem";
            this.createReportEmailToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.createReportEmailToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.createReportEmailToolStripMenuItem.Text = "Create Outlook report email";
            this.createReportEmailToolStripMenuItem.Click += new System.EventHandler(this.createReportEmailToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(321, 6);
            // 
            // priorityToolStripMenuItem
            // 
            this.priorityToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("priorityToolStripMenuItem.Image")));
            this.priorityToolStripMenuItem.Name = "priorityToolStripMenuItem";
            this.priorityToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.priorityToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.priorityToolStripMenuItem.Text = "Change priority...";
            this.priorityToolStripMenuItem.Click += new System.EventHandler(this.priorityToolStripMenuItem_Click);
            // 
            // cancelJobToolStripMenuItem
            // 
            this.cancelJobToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cancelJobToolStripMenuItem.Image")));
            this.cancelJobToolStripMenuItem.Name = "cancelJobToolStripMenuItem";
            this.cancelJobToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.cancelJobToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.cancelJobToolStripMenuItem.Text = "Cancel job(s)";
            this.cancelJobToolStripMenuItem.Click += new System.EventHandler(this.cancelJobToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem2
            // 
            this.deleteToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedJobToolStripMenuItem,
            this.allJobsToolStripMenuItem});
            this.deleteToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem2.Image")));
            this.deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            this.deleteToolStripMenuItem2.Size = new System.Drawing.Size(324, 22);
            this.deleteToolStripMenuItem2.Text = "Delete";
            // 
            // selectedJobToolStripMenuItem
            // 
            this.selectedJobToolStripMenuItem.Name = "selectedJobToolStripMenuItem";
            this.selectedJobToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.selectedJobToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.selectedJobToolStripMenuItem.Text = "Selected job(s)...";
            this.selectedJobToolStripMenuItem.Click += new System.EventHandler(this.selectedJobToolStripMenuItem_Click);
            // 
            // allJobsToolStripMenuItem
            // 
            this.allJobsToolStripMenuItem.Name = "allJobsToolStripMenuItem";
            this.allJobsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Delete)));
            this.allJobsToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.allJobsToolStripMenuItem.Text = "All jobs...";
            this.allJobsToolStripMenuItem.Click += new System.EventHandler(this.allJobsToolStripMenuItem_Click);
            // 
            // publishToolStripMenuItem
            // 
            this.publishToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem,
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem,
            this.toolStripSeparator22,
            this.createALocatorForTheAssetToolStripMenuItem,
            this.deleteAllLocatorsOfTheAssetToolStripMenuItem,
            this.toolStripSeparator23,
            this.playbackToolStripMenuItem});
            this.publishToolStripMenuItem.Name = "publishToolStripMenuItem";
            this.publishToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.publishToolStripMenuItem.Text = "Publish";
            // 
            // setupDynamicEncryptionForTheAssetsToolStripMenuItem
            // 
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem.Name = "setupDynamicEncryptionForTheAssetsToolStripMenuItem";
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem.Text = "Add a dynamic encryption policy for the asset(s)...";
            this.setupDynamicEncryptionForTheAssetsToolStripMenuItem.Click += new System.EventHandler(this.setupDynamicEncryptionForTheAssetsToolStripMenuItem_Click);
            // 
            // removeDynamicEncryptionForTheAssetsToolStripMenuItem
            // 
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.cancel;
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem.Name = "removeDynamicEncryptionForTheAssetsToolStripMenuItem";
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem.Text = "Remove all dynamic encryption policies for the asset(s)...";
            this.removeDynamicEncryptionForTheAssetsToolStripMenuItem.Click += new System.EventHandler(this.removeDynamicEncryptionForTheAssetsToolStripMenuItem_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(370, 6);
            // 
            // createALocatorForTheAssetToolStripMenuItem
            // 
            this.createALocatorForTheAssetToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createALocatorForTheAssetToolStripMenuItem.Image")));
            this.createALocatorForTheAssetToolStripMenuItem.Name = "createALocatorForTheAssetToolStripMenuItem";
            this.createALocatorForTheAssetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.createALocatorForTheAssetToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.createALocatorForTheAssetToolStripMenuItem.Text = "Create a locator...";
            this.createALocatorForTheAssetToolStripMenuItem.Click += new System.EventHandler(this.createALocatorForTheAssetToolStripMenuItem_Click);
            // 
            // deleteAllLocatorsOfTheAssetToolStripMenuItem
            // 
            this.deleteAllLocatorsOfTheAssetToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteAllLocatorsOfTheAssetToolStripMenuItem.Image")));
            this.deleteAllLocatorsOfTheAssetToolStripMenuItem.Name = "deleteAllLocatorsOfTheAssetToolStripMenuItem";
            this.deleteAllLocatorsOfTheAssetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.deleteAllLocatorsOfTheAssetToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.deleteAllLocatorsOfTheAssetToolStripMenuItem.Text = "Delete all locators...";
            this.deleteAllLocatorsOfTheAssetToolStripMenuItem.Click += new System.EventHandler(this.deleteAllLocatorsOfTheAssetToolStripMenuItem_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(370, 6);
            // 
            // playbackToolStripMenuItem
            // 
            this.playbackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.withFlashOSMFToolStripMenuItem,
            this.withSilverlightMMPPFToolStripMenuItem,
            this.withMPEGDASHIFRefPlayerToolStripMenuItem,
            this.withMPEGDASHAzurePlayerToolStripMenuItem,
            this.withDASHLiveAzurePlayerToolStripMenuItem1,
            this.withCustomPlayerToolStripMenuItem});
            this.playbackToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("playbackToolStripMenuItem.Image")));
            this.playbackToolStripMenuItem.Name = "playbackToolStripMenuItem";
            this.playbackToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.playbackToolStripMenuItem.Text = "Playback";
            this.playbackToolStripMenuItem.DropDownOpening += new System.EventHandler(this.playbackToolStripMenuItem_DropDownOpening);
            // 
            // withFlashOSMFToolStripMenuItem
            // 
            this.withFlashOSMFToolStripMenuItem.Name = "withFlashOSMFToolStripMenuItem";
            this.withFlashOSMFToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.withFlashOSMFToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.withFlashOSMFToolStripMenuItem.Text = "with Flash OSMF Azure Player";
            this.withFlashOSMFToolStripMenuItem.DropDownOpening += new System.EventHandler(this.withFlashOSMFToolStripMenuItem_DropDownOpening);
            this.withFlashOSMFToolStripMenuItem.Click += new System.EventHandler(this.withFlashOSMFToolStripMenuItem_Click);
            // 
            // withSilverlightMMPPFToolStripMenuItem
            // 
            this.withSilverlightMMPPFToolStripMenuItem.Name = "withSilverlightMMPPFToolStripMenuItem";
            this.withSilverlightMMPPFToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.withSilverlightMMPPFToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.withSilverlightMMPPFToolStripMenuItem.Text = "with Silverlight Monitoring Player";
            this.withSilverlightMMPPFToolStripMenuItem.Click += new System.EventHandler(this.withSilverlightMMPPFToolStripMenuItem_Click);
            // 
            // withMPEGDASHIFRefPlayerToolStripMenuItem
            // 
            this.withMPEGDASHIFRefPlayerToolStripMenuItem.Name = "withMPEGDASHIFRefPlayerToolStripMenuItem";
            this.withMPEGDASHIFRefPlayerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.withMPEGDASHIFRefPlayerToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.withMPEGDASHIFRefPlayerToolStripMenuItem.Text = "with MPEG-DASH IF Reference Player";
            this.withMPEGDASHIFRefPlayerToolStripMenuItem.Click += new System.EventHandler(this.withMPEGDASHIFRefPlayerToolStripMenuItem_Click);
            // 
            // withMPEGDASHAzurePlayerToolStripMenuItem
            // 
            this.withMPEGDASHAzurePlayerToolStripMenuItem.Name = "withMPEGDASHAzurePlayerToolStripMenuItem";
            this.withMPEGDASHAzurePlayerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.withMPEGDASHAzurePlayerToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.withMPEGDASHAzurePlayerToolStripMenuItem.Text = "with MPEG-DASH Azure Player";
            this.withMPEGDASHAzurePlayerToolStripMenuItem.Click += new System.EventHandler(this.withMPEGDASHAzurePlayerToolStripMenuItem_Click);
            // 
            // withDASHLiveAzurePlayerToolStripMenuItem1
            // 
            this.withDASHLiveAzurePlayerToolStripMenuItem1.Name = "withDASHLiveAzurePlayerToolStripMenuItem1";
            this.withDASHLiveAzurePlayerToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
            this.withDASHLiveAzurePlayerToolStripMenuItem1.Size = new System.Drawing.Size(308, 22);
            this.withDASHLiveAzurePlayerToolStripMenuItem1.Text = "with DASH Live Azure Player";
            this.withDASHLiveAzurePlayerToolStripMenuItem1.Click += new System.EventHandler(this.withDASHLiveAzurePlayerToolStripMenuItem1_Click_1);
            // 
            // withCustomPlayerToolStripMenuItem
            // 
            this.withCustomPlayerToolStripMenuItem.Name = "withCustomPlayerToolStripMenuItem";
            this.withCustomPlayerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.withCustomPlayerToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.withCustomPlayerToolStripMenuItem.Text = "with Custom Player";
            this.withCustomPlayerToolStripMenuItem.Click += new System.EventHandler(this.withCustomPlayerToolStripMenuItem_Click);
            // 
            // liveChannelToolStripMenuItem
            // 
            this.liveChannelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channToolStripMenuItem,
            this.createChannelToolStripMenuItem,
            this.startChannelsToolStripMenuItem,
            this.stopChannelsToolStripMenuItem,
            this.resetChannelsToolStripMenuItem,
            this.deleteChannelsToolStripMenuItem,
            this.toolStripSeparator15,
            this.displayProgramInformationToolStripMenuItem,
            this.displayRelatedAssetInformationToolStripMenuItem1,
            this.createProgramToolStripMenuItem1,
            this.startProgramsToolStripMenuItem1,
            this.stopProgramsToolStripMenuItem,
            this.deleteProgramsToolStripMenuItem1});
            this.liveChannelToolStripMenuItem.Enabled = false;
            this.liveChannelToolStripMenuItem.Name = "liveChannelToolStripMenuItem";
            this.liveChannelToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.liveChannelToolStripMenuItem.Text = "Live";
            // 
            // channToolStripMenuItem
            // 
            this.channToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("channToolStripMenuItem.Image")));
            this.channToolStripMenuItem.Name = "channToolStripMenuItem";
            this.channToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.channToolStripMenuItem.Text = "Channel infomation and settings...";
            this.channToolStripMenuItem.Click += new System.EventHandler(this.channToolStripMenuItem_Click);
            // 
            // createChannelToolStripMenuItem
            // 
            this.createChannelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createChannelToolStripMenuItem.Image")));
            this.createChannelToolStripMenuItem.Name = "createChannelToolStripMenuItem";
            this.createChannelToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.createChannelToolStripMenuItem.Text = "Create channel...";
            this.createChannelToolStripMenuItem.Click += new System.EventHandler(this.createChannelToolStripMenuItem_Click);
            // 
            // startChannelsToolStripMenuItem
            // 
            this.startChannelsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("startChannelsToolStripMenuItem.Image")));
            this.startChannelsToolStripMenuItem.Name = "startChannelsToolStripMenuItem";
            this.startChannelsToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.startChannelsToolStripMenuItem.Text = "Start channel(s)";
            this.startChannelsToolStripMenuItem.Click += new System.EventHandler(this.startChannelsToolStripMenuItem_Click);
            // 
            // stopChannelsToolStripMenuItem
            // 
            this.stopChannelsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("stopChannelsToolStripMenuItem.Image")));
            this.stopChannelsToolStripMenuItem.Name = "stopChannelsToolStripMenuItem";
            this.stopChannelsToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.stopChannelsToolStripMenuItem.Text = "Stop channel(s)";
            this.stopChannelsToolStripMenuItem.Click += new System.EventHandler(this.stopChannelsToolStripMenuItem_Click);
            // 
            // resetChannelsToolStripMenuItem
            // 
            this.resetChannelsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetChannelsToolStripMenuItem.Image")));
            this.resetChannelsToolStripMenuItem.Name = "resetChannelsToolStripMenuItem";
            this.resetChannelsToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.resetChannelsToolStripMenuItem.Text = "Reset channel(s)";
            this.resetChannelsToolStripMenuItem.Click += new System.EventHandler(this.resetChannelsToolStripMenuItem_Click);
            // 
            // deleteChannelsToolStripMenuItem
            // 
            this.deleteChannelsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteChannelsToolStripMenuItem.Image")));
            this.deleteChannelsToolStripMenuItem.Name = "deleteChannelsToolStripMenuItem";
            this.deleteChannelsToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.deleteChannelsToolStripMenuItem.Text = "Delete channel(s)";
            this.deleteChannelsToolStripMenuItem.Click += new System.EventHandler(this.deleteChannelsToolStripMenuItem_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(259, 6);
            // 
            // displayProgramInformationToolStripMenuItem
            // 
            this.displayProgramInformationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("displayProgramInformationToolStripMenuItem.Image")));
            this.displayProgramInformationToolStripMenuItem.Name = "displayProgramInformationToolStripMenuItem";
            this.displayProgramInformationToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.displayProgramInformationToolStripMenuItem.Text = "Program information and settings...";
            this.displayProgramInformationToolStripMenuItem.Click += new System.EventHandler(this.displayProgramInformationToolStripMenuItem_Click);
            // 
            // displayRelatedAssetInformationToolStripMenuItem1
            // 
            this.displayRelatedAssetInformationToolStripMenuItem1.Image = global::AMSExplorer.Bitmaps.Display_information;
            this.displayRelatedAssetInformationToolStripMenuItem1.Name = "displayRelatedAssetInformationToolStripMenuItem1";
            this.displayRelatedAssetInformationToolStripMenuItem1.Size = new System.Drawing.Size(262, 22);
            this.displayRelatedAssetInformationToolStripMenuItem1.Text = "Display related asset information...";
            this.displayRelatedAssetInformationToolStripMenuItem1.Click += new System.EventHandler(this.displayRelatedAssetInformationToolStripMenuItem1_Click);
            // 
            // createProgramToolStripMenuItem1
            // 
            this.createProgramToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("createProgramToolStripMenuItem1.Image")));
            this.createProgramToolStripMenuItem1.Name = "createProgramToolStripMenuItem1";
            this.createProgramToolStripMenuItem1.Size = new System.Drawing.Size(262, 22);
            this.createProgramToolStripMenuItem1.Text = "Create program...";
            this.createProgramToolStripMenuItem1.Click += new System.EventHandler(this.createProgramToolStripMenuItem1_Click);
            // 
            // startProgramsToolStripMenuItem1
            // 
            this.startProgramsToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("startProgramsToolStripMenuItem1.Image")));
            this.startProgramsToolStripMenuItem1.Name = "startProgramsToolStripMenuItem1";
            this.startProgramsToolStripMenuItem1.Size = new System.Drawing.Size(262, 22);
            this.startProgramsToolStripMenuItem1.Text = "Start program(s)";
            this.startProgramsToolStripMenuItem1.Click += new System.EventHandler(this.startProgramsToolStripMenuItem1_Click);
            // 
            // stopProgramsToolStripMenuItem
            // 
            this.stopProgramsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("stopProgramsToolStripMenuItem.Image")));
            this.stopProgramsToolStripMenuItem.Name = "stopProgramsToolStripMenuItem";
            this.stopProgramsToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.stopProgramsToolStripMenuItem.Text = "Stop program(s)";
            this.stopProgramsToolStripMenuItem.Click += new System.EventHandler(this.stopProgramsToolStripMenuItem_Click);
            // 
            // deleteProgramsToolStripMenuItem1
            // 
            this.deleteProgramsToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("deleteProgramsToolStripMenuItem1.Image")));
            this.deleteProgramsToolStripMenuItem1.Name = "deleteProgramsToolStripMenuItem1";
            this.deleteProgramsToolStripMenuItem1.Size = new System.Drawing.Size(262, 22);
            this.deleteProgramsToolStripMenuItem1.Text = "Delete program(s)";
            this.deleteProgramsToolStripMenuItem1.Click += new System.EventHandler(this.deleteProgramsToolStripMenuItem1_Click);
            // 
            // originToolStripMenuItem
            // 
            this.originToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayOriginInformationToolStripMenuItem,
            this.createOriginToolStripMenuItem,
            this.startOriginsToolStripMenuItem,
            this.stopOriginsToolStripMenuItem,
            this.deleteOriginsToolStripMenuItem});
            this.originToolStripMenuItem.Enabled = false;
            this.originToolStripMenuItem.Name = "originToolStripMenuItem";
            this.originToolStripMenuItem.Size = new System.Drawing.Size(124, 20);
            this.originToolStripMenuItem.Text = "Streaming endpoint";
            // 
            // displayOriginInformationToolStripMenuItem
            // 
            this.displayOriginInformationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("displayOriginInformationToolStripMenuItem.Image")));
            this.displayOriginInformationToolStripMenuItem.Name = "displayOriginInformationToolStripMenuItem";
            this.displayOriginInformationToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.displayOriginInformationToolStripMenuItem.Size = new System.Drawing.Size(340, 22);
            this.displayOriginInformationToolStripMenuItem.Text = "Streaming endpoint information and settings...";
            this.displayOriginInformationToolStripMenuItem.Click += new System.EventHandler(this.displayOriginInformationToolStripMenuItem_Click);
            // 
            // createOriginToolStripMenuItem
            // 
            this.createOriginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createOriginToolStripMenuItem.Image")));
            this.createOriginToolStripMenuItem.Name = "createOriginToolStripMenuItem";
            this.createOriginToolStripMenuItem.Size = new System.Drawing.Size(340, 22);
            this.createOriginToolStripMenuItem.Text = "Create streaming endpoint...";
            this.createOriginToolStripMenuItem.Click += new System.EventHandler(this.createOriginToolStripMenuItem_Click);
            // 
            // startOriginsToolStripMenuItem
            // 
            this.startOriginsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("startOriginsToolStripMenuItem.Image")));
            this.startOriginsToolStripMenuItem.Name = "startOriginsToolStripMenuItem";
            this.startOriginsToolStripMenuItem.Size = new System.Drawing.Size(340, 22);
            this.startOriginsToolStripMenuItem.Text = "Start streaming endpoint(s)";
            this.startOriginsToolStripMenuItem.Click += new System.EventHandler(this.startOriginsToolStripMenuItem_Click);
            // 
            // stopOriginsToolStripMenuItem
            // 
            this.stopOriginsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("stopOriginsToolStripMenuItem.Image")));
            this.stopOriginsToolStripMenuItem.Name = "stopOriginsToolStripMenuItem";
            this.stopOriginsToolStripMenuItem.Size = new System.Drawing.Size(340, 22);
            this.stopOriginsToolStripMenuItem.Text = "Stop streaming endpoint(s)";
            this.stopOriginsToolStripMenuItem.Click += new System.EventHandler(this.stopOriginsToolStripMenuItem_Click);
            // 
            // deleteOriginsToolStripMenuItem
            // 
            this.deleteOriginsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteOriginsToolStripMenuItem.Image")));
            this.deleteOriginsToolStripMenuItem.Name = "deleteOriginsToolStripMenuItem";
            this.deleteOriginsToolStripMenuItem.Size = new System.Drawing.Size(340, 22);
            this.deleteOriginsToolStripMenuItem.Text = "Delete streaming endpoint(s)";
            this.deleteOriginsToolStripMenuItem.Click += new System.EventHandler(this.deleteOriginsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.toolStripMenuItem1.Text = "Options";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripMenuItem.Image")));
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.refreshToolStripMenuItem.Text = "Refresh grids";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("optionsToolStripMenuItem.Image")));
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // samplePlayersToolStripMenuItem
            // 
            this.samplePlayersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.azureManagementPortalToolStripMenuItem,
            this.azureMediaBlogToolStripMenuItem,
            this.toolStripSeparator1,
            this.azureMediaServicesPlayerPageToolStripMenuItem,
            this.silverlightMonitoringPlayerToolStripMenuItem,
            this.dASHIFHTML5ReferencePlayerToolStripMenuItem,
            this.hTML5VideoElementToolStripMenuItem,
            this.iVXHLSPlayerToolStripMenuItem,
            this.oSMFToolStripMenuItem,
            this.jwPlayerToolStripMenuItem});
            this.samplePlayersToolStripMenuItem.Name = "samplePlayersToolStripMenuItem";
            this.samplePlayersToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.samplePlayersToolStripMenuItem.Text = "External links";
            // 
            // azureManagementPortalToolStripMenuItem
            // 
            this.azureManagementPortalToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("azureManagementPortalToolStripMenuItem.Image")));
            this.azureManagementPortalToolStripMenuItem.Name = "azureManagementPortalToolStripMenuItem";
            this.azureManagementPortalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.azureManagementPortalToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
            this.azureManagementPortalToolStripMenuItem.Text = "Azure Management Portal";
            this.azureManagementPortalToolStripMenuItem.Click += new System.EventHandler(this.azureManagementPortalToolStripMenuItem_Click);
            // 
            // azureMediaBlogToolStripMenuItem
            // 
            this.azureMediaBlogToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("azureMediaBlogToolStripMenuItem.Image")));
            this.azureMediaBlogToolStripMenuItem.Name = "azureMediaBlogToolStripMenuItem";
            this.azureMediaBlogToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
            this.azureMediaBlogToolStripMenuItem.Text = "Azure Media Blog";
            this.azureMediaBlogToolStripMenuItem.Click += new System.EventHandler(this.azureMediaBlogToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(357, 6);
            // 
            // azureMediaServicesPlayerPageToolStripMenuItem
            // 
            this.azureMediaServicesPlayerPageToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("azureMediaServicesPlayerPageToolStripMenuItem.Image")));
            this.azureMediaServicesPlayerPageToolStripMenuItem.Name = "azureMediaServicesPlayerPageToolStripMenuItem";
            this.azureMediaServicesPlayerPageToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
            this.azureMediaServicesPlayerPageToolStripMenuItem.Text = "Azure Media Services Player Page";
            this.azureMediaServicesPlayerPageToolStripMenuItem.Click += new System.EventHandler(this.azureMediaServicesPlayerPageToolStripMenuItem_Click);
            // 
            // silverlightMonitoringPlayerToolStripMenuItem
            // 
            this.silverlightMonitoringPlayerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("silverlightMonitoringPlayerToolStripMenuItem.Image")));
            this.silverlightMonitoringPlayerToolStripMenuItem.Name = "silverlightMonitoringPlayerToolStripMenuItem";
            this.silverlightMonitoringPlayerToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
            this.silverlightMonitoringPlayerToolStripMenuItem.Text = "Silverlight Smooth Streaming Monitoring Player";
            this.silverlightMonitoringPlayerToolStripMenuItem.Click += new System.EventHandler(this.silverlightMonitoringPlayerToolStripMenuItem_Click);
            // 
            // dASHIFHTML5ReferencePlayerToolStripMenuItem
            // 
            this.dASHIFHTML5ReferencePlayerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dASHIFHTML5ReferencePlayerToolStripMenuItem.Image")));
            this.dASHIFHTML5ReferencePlayerToolStripMenuItem.Name = "dASHIFHTML5ReferencePlayerToolStripMenuItem";
            this.dASHIFHTML5ReferencePlayerToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
            this.dASHIFHTML5ReferencePlayerToolStripMenuItem.Text = "DASH-IF HTML5 Reference Player";
            this.dASHIFHTML5ReferencePlayerToolStripMenuItem.Click += new System.EventHandler(this.dASHIFHTML5ReferencePlayerToolStripMenuItem_Click);
            // 
            // hTML5VideoElementToolStripMenuItem
            // 
            this.hTML5VideoElementToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("hTML5VideoElementToolStripMenuItem.Image")));
            this.hTML5VideoElementToolStripMenuItem.Name = "hTML5VideoElementToolStripMenuItem";
            this.hTML5VideoElementToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
            this.hTML5VideoElementToolStripMenuItem.Text = "HTML5 Video Element";
            this.hTML5VideoElementToolStripMenuItem.Click += new System.EventHandler(this.hTML5VideoElementToolStripMenuItem_Click);
            // 
            // iVXHLSPlayerToolStripMenuItem
            // 
            this.iVXHLSPlayerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("iVXHLSPlayerToolStripMenuItem.Image")));
            this.iVXHLSPlayerToolStripMenuItem.Name = "iVXHLSPlayerToolStripMenuItem";
            this.iVXHLSPlayerToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
            this.iVXHLSPlayerToolStripMenuItem.Text = "3iVX HLS Player";
            this.iVXHLSPlayerToolStripMenuItem.Click += new System.EventHandler(this.iVXHLSPlayerToolStripMenuItem_Click);
            // 
            // oSMFToolStripMenuItem
            // 
            this.oSMFToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("oSMFToolStripMenuItem.Image")));
            this.oSMFToolStripMenuItem.Name = "oSMFToolStripMenuItem";
            this.oSMFToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
            this.oSMFToolStripMenuItem.Text = "Smooth Streaming Plugin for OSMF with MPEG-DASH";
            this.oSMFToolStripMenuItem.Click += new System.EventHandler(this.oSMFToolStripMenuItem_Click);
            // 
            // jwPlayerToolStripMenuItem
            // 
            this.jwPlayerToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.external_link;
            this.jwPlayerToolStripMenuItem.Name = "jwPlayerToolStripMenuItem";
            this.jwPlayerToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
            this.jwPlayerToolStripMenuItem.Text = "JW Player";
            this.jwPlayerToolStripMenuItem.Click += new System.EventHandler(this.jwPlayerToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.azureMediaHelpFileToolStripMenuItem,
            this.azureMediaServicesMSDNToolStripMenuItem,
            this.azureMediaServicesForumToolStripMenuItem,
            this.toolStripSeparator13,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // azureMediaHelpFileToolStripMenuItem
            // 
            this.azureMediaHelpFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("azureMediaHelpFileToolStripMenuItem.Image")));
            this.azureMediaHelpFileToolStripMenuItem.Name = "azureMediaHelpFileToolStripMenuItem";
            this.azureMediaHelpFileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.azureMediaHelpFileToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.azureMediaHelpFileToolStripMenuItem.Text = "Azure Media Services help file";
            this.azureMediaHelpFileToolStripMenuItem.Click += new System.EventHandler(this.azureMediaHelpFileToolStripMenuItem_Click);
            // 
            // azureMediaServicesMSDNToolStripMenuItem
            // 
            this.azureMediaServicesMSDNToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("azureMediaServicesMSDNToolStripMenuItem.Image")));
            this.azureMediaServicesMSDNToolStripMenuItem.Name = "azureMediaServicesMSDNToolStripMenuItem";
            this.azureMediaServicesMSDNToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F1)));
            this.azureMediaServicesMSDNToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.azureMediaServicesMSDNToolStripMenuItem.Text = "Azure Media Services on MSDN";
            this.azureMediaServicesMSDNToolStripMenuItem.Click += new System.EventHandler(this.azureMediaServicesMSDNToolStripMenuItem_Click_1);
            // 
            // azureMediaServicesForumToolStripMenuItem
            // 
            this.azureMediaServicesForumToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("azureMediaServicesForumToolStripMenuItem.Image")));
            this.azureMediaServicesForumToolStripMenuItem.Name = "azureMediaServicesForumToolStripMenuItem";
            this.azureMediaServicesForumToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F1)));
            this.azureMediaServicesForumToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.azureMediaServicesForumToolStripMenuItem.Text = "Azure Media Services Forum";
            this.azureMediaServicesForumToolStripMenuItem.Click += new System.EventHandler(this.azureMediaServicesForumToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(294, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // butPrevPageJob
            // 
            this.butPrevPageJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butPrevPageJob.Enabled = false;
            this.butPrevPageJob.Location = new System.Drawing.Point(871, 414);
            this.butPrevPageJob.Name = "butPrevPageJob";
            this.butPrevPageJob.Size = new System.Drawing.Size(28, 23);
            this.butPrevPageJob.TabIndex = 30;
            this.butPrevPageJob.Text = "<";
            this.butPrevPageJob.UseVisualStyleBackColor = true;
            this.butPrevPageJob.Click += new System.EventHandler(this.butPrevPageJob_Click);
            // 
            // butNextPageJob
            // 
            this.butNextPageJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butNextPageJob.Enabled = false;
            this.butNextPageJob.Location = new System.Drawing.Point(972, 414);
            this.butNextPageJob.Name = "butNextPageJob";
            this.butNextPageJob.Size = new System.Drawing.Size(28, 23);
            this.butNextPageJob.TabIndex = 30;
            this.butNextPageJob.Text = ">";
            this.butNextPageJob.UseVisualStyleBackColor = true;
            this.butNextPageJob.Click += new System.EventHandler(this.butNextPageJob_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(660, 419);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Order by:";
            // 
            // comboBoxOrderJobs
            // 
            this.comboBoxOrderJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOrderJobs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrderJobs.FormattingEnabled = true;
            this.comboBoxOrderJobs.Location = new System.Drawing.Point(713, 415);
            this.comboBoxOrderJobs.Name = "comboBoxOrderJobs";
            this.comboBoxOrderJobs.Size = new System.Drawing.Size(102, 21);
            this.comboBoxOrderJobs.TabIndex = 14;
            this.comboBoxOrderJobs.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrderJobs_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(830, 419);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Page:";
            // 
            // comboBoxPageJobs
            // 
            this.comboBoxPageJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPageJobs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPageJobs.FormattingEnabled = true;
            this.comboBoxPageJobs.Location = new System.Drawing.Point(905, 415);
            this.comboBoxPageJobs.Name = "comboBoxPageJobs";
            this.comboBoxPageJobs.Size = new System.Drawing.Size(61, 21);
            this.comboBoxPageJobs.TabIndex = 25;
            this.comboBoxPageJobs.SelectedIndexChanged += new System.EventHandler(this.comboBoxPageJobs_SelectedIndexChanged);
            // 
            // contextMenuStripJobs
            // 
            this.contextMenuStripJobs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuItemJobDisplayInfo,
            this.ContextMenuItemJobInputAssetInformation,
            this.ContextMenuItemJobOpenOutputAsset,
            this.ContextMenuItemJobCreateOutlookReportEmail,
            this.toolStripSeparator12,
            this.ContextMenuItemJobChangePriority,
            this.ContextMenuItemJobCancel,
            this.ContextMenuItemJobDelete});
            this.contextMenuStripJobs.Name = "contextMenuStripJobs";
            this.contextMenuStripJobs.Size = new System.Drawing.Size(267, 164);
            this.contextMenuStripJobs.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripJobs_Opening);
            // 
            // ContextMenuItemJobDisplayInfo
            // 
            this.ContextMenuItemJobDisplayInfo.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemJobDisplayInfo.Image")));
            this.ContextMenuItemJobDisplayInfo.Name = "ContextMenuItemJobDisplayInfo";
            this.ContextMenuItemJobDisplayInfo.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.ContextMenuItemJobDisplayInfo.Size = new System.Drawing.Size(266, 22);
            this.ContextMenuItemJobDisplayInfo.Text = "Display information";
            this.ContextMenuItemJobDisplayInfo.Click += new System.EventHandler(this.toolStripMenuJobDisplayInfo_Click);
            // 
            // ContextMenuItemJobInputAssetInformation
            // 
            this.ContextMenuItemJobInputAssetInformation.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemJobInputAssetInformation.Image")));
            this.ContextMenuItemJobInputAssetInformation.Name = "ContextMenuItemJobInputAssetInformation";
            this.ContextMenuItemJobInputAssetInformation.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.ContextMenuItemJobInputAssetInformation.Size = new System.Drawing.Size(266, 22);
            this.ContextMenuItemJobInputAssetInformation.Text = "Input asset information";
            this.ContextMenuItemJobInputAssetInformation.Click += new System.EventHandler(this.inputAssetInformationToolStripMenuItem1_Click);
            // 
            // ContextMenuItemJobOpenOutputAsset
            // 
            this.ContextMenuItemJobOpenOutputAsset.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemJobOpenOutputAsset.Image")));
            this.ContextMenuItemJobOpenOutputAsset.Name = "ContextMenuItemJobOpenOutputAsset";
            this.ContextMenuItemJobOpenOutputAsset.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.ContextMenuItemJobOpenOutputAsset.Size = new System.Drawing.Size(266, 22);
            this.ContextMenuItemJobOpenOutputAsset.Text = "Output asset information";
            this.ContextMenuItemJobOpenOutputAsset.Click += new System.EventHandler(this.openOutputAssetToolStripMenuItem_Click);
            // 
            // ContextMenuItemJobCreateOutlookReportEmail
            // 
            this.ContextMenuItemJobCreateOutlookReportEmail.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemJobCreateOutlookReportEmail.Image")));
            this.ContextMenuItemJobCreateOutlookReportEmail.Name = "ContextMenuItemJobCreateOutlookReportEmail";
            this.ContextMenuItemJobCreateOutlookReportEmail.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.ContextMenuItemJobCreateOutlookReportEmail.Size = new System.Drawing.Size(266, 22);
            this.ContextMenuItemJobCreateOutlookReportEmail.Text = "Create Outlook report email";
            this.ContextMenuItemJobCreateOutlookReportEmail.Click += new System.EventHandler(this.createOutlookReportEmailToolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(263, 6);
            // 
            // ContextMenuItemJobChangePriority
            // 
            this.ContextMenuItemJobChangePriority.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemJobChangePriority.Image")));
            this.ContextMenuItemJobChangePriority.Name = "ContextMenuItemJobChangePriority";
            this.ContextMenuItemJobChangePriority.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.ContextMenuItemJobChangePriority.Size = new System.Drawing.Size(266, 22);
            this.ContextMenuItemJobChangePriority.Text = "Change priority...";
            this.ContextMenuItemJobChangePriority.Click += new System.EventHandler(this.changePriorityToolStripMenuItem_Click);
            // 
            // ContextMenuItemJobCancel
            // 
            this.ContextMenuItemJobCancel.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemJobCancel.Image")));
            this.ContextMenuItemJobCancel.Name = "ContextMenuItemJobCancel";
            this.ContextMenuItemJobCancel.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.ContextMenuItemJobCancel.Size = new System.Drawing.Size(266, 22);
            this.ContextMenuItemJobCancel.Text = "Cancel...";
            this.ContextMenuItemJobCancel.Click += new System.EventHandler(this.toolStripMenuJobsCancel_Click);
            // 
            // ContextMenuItemJobDelete
            // 
            this.ContextMenuItemJobDelete.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemJobDelete.Image")));
            this.ContextMenuItemJobDelete.Name = "ContextMenuItemJobDelete";
            this.ContextMenuItemJobDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ContextMenuItemJobDelete.Size = new System.Drawing.Size(266, 22);
            this.ContextMenuItemJobDelete.Text = "Delete...";
            this.ContextMenuItemJobDelete.Click += new System.EventHandler(this.toolStripMenuItemJobsDelete_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelConnection,
            this.toolStripStatusLabelWatchFolder,
            this.toolStripStatusLabelSE});
            this.statusStrip1.Location = new System.Drawing.Point(0, 617);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1020, 22);
            this.statusStrip1.TabIndex = 27;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelConnection
            // 
            this.toolStripStatusLabelConnection.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelConnection.Name = "toolStripStatusLabelConnection";
            this.toolStripStatusLabelConnection.Size = new System.Drawing.Size(82, 17);
            this.toolStripStatusLabelConnection.Text = "Connected to ";
            // 
            // toolStripStatusLabelWatchFolder
            // 
            this.toolStripStatusLabelWatchFolder.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelWatchFolder.Image = global::AMSExplorer.Bitmaps.watch_folder;
            this.toolStripStatusLabelWatchFolder.Name = "toolStripStatusLabelWatchFolder";
            this.toolStripStatusLabelWatchFolder.Size = new System.Drawing.Size(136, 17);
            this.toolStripStatusLabelWatchFolder.Text = "Watch folder running";
            // 
            // toolStripStatusLabelSE
            // 
            this.toolStripStatusLabelSE.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelSE.Image = global::AMSExplorer.Bitmaps.storage_encryption;
            this.toolStripStatusLabelSE.Name = "toolStripStatusLabelSE";
            this.toolStripStatusLabelSE.Size = new System.Drawing.Size(211, 17);
            this.toolStripStatusLabelSE.Text = "New asset will be storage encrypted";
            // 
            // dataGridViewTransfer
            // 
            this.dataGridViewTransfer.AllowUserToAddRows = false;
            this.dataGridViewTransfer.AllowUserToDeleteRows = false;
            this.dataGridViewTransfer.AllowUserToResizeRows = false;
            this.dataGridViewTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTransfer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTransfer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTransfer.ContextMenuStrip = this.contextMenuStripTransfers;
            this.dataGridViewTransfer.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewTransfer.MultiSelect = false;
            this.dataGridViewTransfer.Name = "dataGridViewTransfer";
            this.dataGridViewTransfer.ReadOnly = true;
            this.dataGridViewTransfer.RowHeadersVisible = false;
            this.dataGridViewTransfer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTransfer.Size = new System.Drawing.Size(997, 431);
            this.dataGridViewTransfer.TabIndex = 32;
            this.dataGridViewTransfer.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewTransfer_CellFormatting);
            this.dataGridViewTransfer.DoubleClick += new System.EventHandler(this.dataGridViewTransfer_DoubleClick);
            // 
            // contextMenuStripTransfers
            // 
            this.contextMenuStripTransfers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuItemTransferOpenDest});
            this.contextMenuStripTransfers.Name = "contextMenuStripTransfers";
            this.contextMenuStripTransfers.Size = new System.Drawing.Size(207, 26);
            this.contextMenuStripTransfers.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripTransfers_Opening);
            // 
            // ContextMenuItemTransferOpenDest
            // 
            this.ContextMenuItemTransferOpenDest.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemTransferOpenDest.Image")));
            this.ContextMenuItemTransferOpenDest.Name = "ContextMenuItemTransferOpenDest";
            this.ContextMenuItemTransferOpenDest.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.ContextMenuItemTransferOpenDest.Size = new System.Drawing.Size(206, 22);
            this.ContextMenuItemTransferOpenDest.Text = "Open destination";
            this.ContextMenuItemTransferOpenDest.Click += new System.EventHandler(this.toolStripMenuItemOpenDest_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageAssets);
            this.tabControlMain.Controls.Add(this.tabPageTransfers);
            this.tabControlMain.Controls.Add(this.tabPageJobs);
            this.tabControlMain.Controls.Add(this.tabPageLive);
            this.tabControlMain.Controls.Add(this.tabPageProcessors);
            this.tabControlMain.Controls.Add(this.tabPageOrigins);
            this.tabControlMain.Controls.Add(this.tabPageChart);
            this.tabControlMain.Location = new System.Drawing.Point(3, 6);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1014, 469);
            this.tabControlMain.TabIndex = 28;
            this.tabControlMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlMain_Selected);
            // 
            // tabPageAssets
            // 
            this.tabPageAssets.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageAssets.Controls.Add(this.label10);
            this.tabPageAssets.Controls.Add(this.comboBoxFilterAssetsTime);
            this.tabPageAssets.Controls.Add(this.label9);
            this.tabPageAssets.Controls.Add(this.comboBoxStateAssets);
            this.tabPageAssets.Controls.Add(this.buttonAssetSearch);
            this.tabPageAssets.Controls.Add(this.label8);
            this.tabPageAssets.Controls.Add(this.textBoxAssetSearch);
            this.tabPageAssets.Controls.Add(this.butPrevPageAsset);
            this.tabPageAssets.Controls.Add(this.dataGridViewAssetsV);
            this.tabPageAssets.Controls.Add(this.label3);
            this.tabPageAssets.Controls.Add(this.comboBoxOrderAssets);
            this.tabPageAssets.Controls.Add(this.butNextPageAsset);
            this.tabPageAssets.Controls.Add(this.comboBoxPageAssets);
            this.tabPageAssets.Controls.Add(this.label1);
            this.tabPageAssets.Location = new System.Drawing.Point(4, 22);
            this.tabPageAssets.Name = "tabPageAssets";
            this.tabPageAssets.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAssets.Size = new System.Drawing.Size(1006, 443);
            this.tabPageAssets.TabIndex = 0;
            this.tabPageAssets.Text = "Assets";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(327, 418);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 43;
            this.label10.Text = "Time:";
            // 
            // comboBoxFilterAssetsTime
            // 
            this.comboBoxFilterAssetsTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFilterAssetsTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterAssetsTime.FormattingEnabled = true;
            this.comboBoxFilterAssetsTime.Location = new System.Drawing.Point(363, 415);
            this.comboBoxFilterAssetsTime.Name = "comboBoxFilterAssetsTime";
            this.comboBoxFilterAssetsTime.Size = new System.Drawing.Size(102, 21);
            this.comboBoxFilterAssetsTime.TabIndex = 42;
            this.comboBoxFilterAssetsTime.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterTime_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(481, 419);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "Status:";
            // 
            // comboBoxStateAssets
            // 
            this.comboBoxStateAssets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStateAssets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStateAssets.FormattingEnabled = true;
            this.comboBoxStateAssets.Location = new System.Drawing.Point(524, 415);
            this.comboBoxStateAssets.Name = "comboBoxStateAssets";
            this.comboBoxStateAssets.Size = new System.Drawing.Size(121, 21);
            this.comboBoxStateAssets.TabIndex = 40;
            this.comboBoxStateAssets.SelectedIndexChanged += new System.EventHandler(this.comboBoxStateAssets_SelectedIndexChanged);
            // 
            // buttonAssetSearch
            // 
            this.buttonAssetSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAssetSearch.Location = new System.Drawing.Point(241, 415);
            this.buttonAssetSearch.Name = "buttonAssetSearch";
            this.buttonAssetSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonAssetSearch.TabIndex = 39;
            this.buttonAssetSearch.Text = "Set filter";
            this.buttonAssetSearch.UseVisualStyleBackColor = true;
            this.buttonAssetSearch.Click += new System.EventHandler(this.buttonAssetSearch_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 419);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 38;
            this.label8.Text = "Search in name:";
            // 
            // textBoxAssetSearch
            // 
            this.textBoxAssetSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAssetSearch.Location = new System.Drawing.Point(101, 416);
            this.textBoxAssetSearch.Name = "textBoxAssetSearch";
            this.textBoxAssetSearch.Size = new System.Drawing.Size(139, 20);
            this.textBoxAssetSearch.TabIndex = 37;
            // 
            // dataGridViewAssetsV
            // 
            this.dataGridViewAssetsV.AllowUserToAddRows = false;
            this.dataGridViewAssetsV.AllowUserToDeleteRows = false;
            this.dataGridViewAssetsV.AllowUserToResizeRows = false;
            this.dataGridViewAssetsV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAssetsV.AssetsPerPage = 50;
            this.dataGridViewAssetsV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAssetsV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAssetsV.ContextMenuStrip = this.contextMenuStripAssets;
            this.dataGridViewAssetsV.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewAssetsV.Name = "dataGridViewAssetsV";
            this.dataGridViewAssetsV.OrderAssetsInGrid = "Last modified";
            this.dataGridViewAssetsV.ReadOnly = true;
            this.dataGridViewAssetsV.RowHeadersVisible = false;
            this.dataGridViewAssetsV.SearchInName = "";
            this.dataGridViewAssetsV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAssetsV.Size = new System.Drawing.Size(994, 403);
            this.dataGridViewAssetsV.StateFilter = "";
            this.dataGridViewAssetsV.TabIndex = 30;
            this.dataGridViewAssetsV.TimeFilter = "Last week";
            this.dataGridViewAssetsV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAssetsV_CellDoubleClick_1);
            this.dataGridViewAssetsV.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewAssetsV_CellFormatting_1);
            // 
            // tabPageTransfers
            // 
            this.tabPageTransfers.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageTransfers.Controls.Add(this.dataGridViewTransfer);
            this.tabPageTransfers.Location = new System.Drawing.Point(4, 22);
            this.tabPageTransfers.Name = "tabPageTransfers";
            this.tabPageTransfers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTransfers.Size = new System.Drawing.Size(1006, 443);
            this.tabPageTransfers.TabIndex = 2;
            this.tabPageTransfers.Text = "Transfers";
            // 
            // tabPageJobs
            // 
            this.tabPageJobs.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageJobs.Controls.Add(this.label11);
            this.tabPageJobs.Controls.Add(this.comboBoxFilterJobsTime);
            this.tabPageJobs.Controls.Add(this.buttonJobSearch);
            this.tabPageJobs.Controls.Add(this.label7);
            this.tabPageJobs.Controls.Add(this.textBoxJobSearch);
            this.tabPageJobs.Controls.Add(this.label6);
            this.tabPageJobs.Controls.Add(this.comboBoxStateJobs);
            this.tabPageJobs.Controls.Add(this.butPrevPageJob);
            this.tabPageJobs.Controls.Add(this.butNextPageJob);
            this.tabPageJobs.Controls.Add(this.comboBoxPageJobs);
            this.tabPageJobs.Controls.Add(this.label4);
            this.tabPageJobs.Controls.Add(this.label2);
            this.tabPageJobs.Controls.Add(this.comboBoxOrderJobs);
            this.tabPageJobs.Controls.Add(this.dataGridViewJobsV);
            this.tabPageJobs.Location = new System.Drawing.Point(4, 22);
            this.tabPageJobs.Name = "tabPageJobs";
            this.tabPageJobs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJobs.Size = new System.Drawing.Size(1006, 443);
            this.tabPageJobs.TabIndex = 1;
            this.tabPageJobs.Text = "Jobs";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(327, 418);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 45;
            this.label11.Text = "Time:";
            // 
            // comboBoxFilterJobsTime
            // 
            this.comboBoxFilterJobsTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFilterJobsTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterJobsTime.FormattingEnabled = true;
            this.comboBoxFilterJobsTime.Location = new System.Drawing.Point(363, 415);
            this.comboBoxFilterJobsTime.Name = "comboBoxFilterJobsTime";
            this.comboBoxFilterJobsTime.Size = new System.Drawing.Size(102, 21);
            this.comboBoxFilterJobsTime.TabIndex = 44;
            this.comboBoxFilterJobsTime.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterJobsTime_SelectedIndexChanged);
            // 
            // buttonJobSearch
            // 
            this.buttonJobSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJobSearch.Location = new System.Drawing.Point(241, 415);
            this.buttonJobSearch.Name = "buttonJobSearch";
            this.buttonJobSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonJobSearch.TabIndex = 36;
            this.buttonJobSearch.Text = "Set filter";
            this.buttonJobSearch.UseVisualStyleBackColor = true;
            this.buttonJobSearch.Click += new System.EventHandler(this.buttonJobSearch_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 419);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Search in name:";
            // 
            // textBoxJobSearch
            // 
            this.textBoxJobSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJobSearch.Location = new System.Drawing.Point(101, 416);
            this.textBoxJobSearch.Name = "textBoxJobSearch";
            this.textBoxJobSearch.Size = new System.Drawing.Size(139, 20);
            this.textBoxJobSearch.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(481, 419);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "State:";
            // 
            // comboBoxStateJobs
            // 
            this.comboBoxStateJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStateJobs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStateJobs.FormattingEnabled = true;
            this.comboBoxStateJobs.Location = new System.Drawing.Point(524, 415);
            this.comboBoxStateJobs.Name = "comboBoxStateJobs";
            this.comboBoxStateJobs.Size = new System.Drawing.Size(121, 21);
            this.comboBoxStateJobs.TabIndex = 32;
            this.comboBoxStateJobs.SelectedIndexChanged += new System.EventHandler(this.comboBoxStateJobs_SelectedIndexChanged);
            // 
            // dataGridViewJobsV
            // 
            this.dataGridViewJobsV.AllowUserToAddRows = false;
            this.dataGridViewJobsV.AllowUserToDeleteRows = false;
            this.dataGridViewJobsV.AllowUserToResizeRows = false;
            this.dataGridViewJobsV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewJobsV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewJobsV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewJobsV.ContextMenuStrip = this.contextMenuStripJobs;
            this.dataGridViewJobsV.FilterJobsState = "All";
            this.dataGridViewJobsV.JobssPerPage = 50;
            this.dataGridViewJobsV.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewJobsV.Name = "dataGridViewJobsV";
            this.dataGridViewJobsV.OrderJobsInGrid = "Last modified";
            this.dataGridViewJobsV.ReadOnly = true;
            this.dataGridViewJobsV.RowHeadersVisible = false;
            this.dataGridViewJobsV.SearchInName = null;
            this.dataGridViewJobsV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewJobsV.Size = new System.Drawing.Size(994, 403);
            this.dataGridViewJobsV.TabIndex = 31;
            this.dataGridViewJobsV.TimeFilter = "Last week";
            this.dataGridViewJobsV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewJobsV_CellDoubleClick);
            this.dataGridViewJobsV.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewJobsV_CellFormatting);
            // 
            // tabPageLive
            // 
            this.tabPageLive.Controls.Add(this.splitContainerLive);
            this.tabPageLive.Location = new System.Drawing.Point(4, 22);
            this.tabPageLive.Name = "tabPageLive";
            this.tabPageLive.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLive.Size = new System.Drawing.Size(1006, 443);
            this.tabPageLive.TabIndex = 6;
            this.tabPageLive.Text = "Live";
            this.tabPageLive.UseVisualStyleBackColor = true;
            // 
            // splitContainerLive
            // 
            this.splitContainerLive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerLive.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.splitContainerLive.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLive.Name = "splitContainerLive";
            this.splitContainerLive.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLive.Panel1
            // 
            this.splitContainerLive.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerLive.Panel1.Controls.Add(this.label13);
            this.splitContainerLive.Panel1.Controls.Add(this.dataGridViewChannelsV);
            // 
            // splitContainerLive.Panel2
            // 
            this.splitContainerLive.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerLive.Panel2.Controls.Add(this.label15);
            this.splitContainerLive.Panel2.Controls.Add(this.comboBoxFilterTimeProgram);
            this.splitContainerLive.Panel2.Controls.Add(this.label16);
            this.splitContainerLive.Panel2.Controls.Add(this.comboBoxStatusProgram);
            this.splitContainerLive.Panel2.Controls.Add(this.buttonSetFilterProgram);
            this.splitContainerLive.Panel2.Controls.Add(this.label17);
            this.splitContainerLive.Panel2.Controls.Add(this.textBoxSearchNameProgram);
            this.splitContainerLive.Panel2.Controls.Add(this.label18);
            this.splitContainerLive.Panel2.Controls.Add(this.comboBoxOrderProgram);
            this.splitContainerLive.Panel2.Controls.Add(this.label14);
            this.splitContainerLive.Panel2.Controls.Add(this.dataGridViewProgramsV);
            this.splitContainerLive.Size = new System.Drawing.Size(1006, 443);
            this.splitContainerLive.SplitterDistance = 238;
            this.splitContainerLive.TabIndex = 32;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 12);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 31;
            this.label13.Text = "Channels";
            // 
            // dataGridViewChannelsV
            // 
            this.dataGridViewChannelsV.AllowUserToAddRows = false;
            this.dataGridViewChannelsV.AllowUserToDeleteRows = false;
            this.dataGridViewChannelsV.AllowUserToResizeRows = false;
            this.dataGridViewChannelsV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewChannelsV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewChannelsV.ChannelsPerPage = 50;
            this.dataGridViewChannelsV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewChannelsV.ContextMenuStrip = this.contextMenuStripChannels;
            this.dataGridViewChannelsV.FilterJobsState = "All";
            this.dataGridViewChannelsV.Location = new System.Drawing.Point(6, 28);
            this.dataGridViewChannelsV.Name = "dataGridViewChannelsV";
            this.dataGridViewChannelsV.OrderJobsInGrid = "Last modified";
            this.dataGridViewChannelsV.ReadOnly = true;
            this.dataGridViewChannelsV.RowHeadersVisible = false;
            this.dataGridViewChannelsV.SearchInName = "";
            this.dataGridViewChannelsV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewChannelsV.Size = new System.Drawing.Size(994, 207);
            this.dataGridViewChannelsV.TabIndex = 30;
            this.dataGridViewChannelsV.TimeFilter = "Last week";
            this.dataGridViewChannelsV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLiveV_CellDoubleClick);
            this.dataGridViewChannelsV.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewLiveV_CellFormatting);
            this.dataGridViewChannelsV.SelectionChanged += new System.EventHandler(this.dataGridViewLiveV_SelectionChanged);
            // 
            // contextMenuStripChannels
            // 
            this.contextMenuStripChannels.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuItemChannelDisplayInfomation,
            this.createChannelToolStripMenuItem1,
            this.ContextMenuItemChannelStart,
            this.ContextMenuItemChannelStop,
            this.ContextMenuItemChannelReset,
            this.ContextMenuItemChannelDelete,
            this.toolStripSeparator14,
            this.ContextMenuItemChannelCopyIngestURLToClipboard,
            this.ContextMenuItemChannelCopyPreviewURLToClipboard,
            this.toolStripSeparator19,
            this.playbackTheProgramToolStripMenuItem});
            this.contextMenuStripChannels.Name = "contextMenuStripChannels";
            this.contextMenuStripChannels.Size = new System.Drawing.Size(257, 214);
            // 
            // ContextMenuItemChannelDisplayInfomation
            // 
            this.ContextMenuItemChannelDisplayInfomation.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemChannelDisplayInfomation.Image")));
            this.ContextMenuItemChannelDisplayInfomation.Name = "ContextMenuItemChannelDisplayInfomation";
            this.ContextMenuItemChannelDisplayInfomation.Size = new System.Drawing.Size(256, 22);
            this.ContextMenuItemChannelDisplayInfomation.Text = "Channel infomation and settings...";
            this.ContextMenuItemChannelDisplayInfomation.Click += new System.EventHandler(this.displayChannelInfomationToolStripMenuItem_Click);
            // 
            // createChannelToolStripMenuItem1
            // 
            this.createChannelToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("createChannelToolStripMenuItem1.Image")));
            this.createChannelToolStripMenuItem1.Name = "createChannelToolStripMenuItem1";
            this.createChannelToolStripMenuItem1.Size = new System.Drawing.Size(256, 22);
            this.createChannelToolStripMenuItem1.Text = "Create channel...";
            this.createChannelToolStripMenuItem1.Click += new System.EventHandler(this.createChannelToolStripMenuItem1_Click);
            // 
            // ContextMenuItemChannelStart
            // 
            this.ContextMenuItemChannelStart.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemChannelStart.Image")));
            this.ContextMenuItemChannelStart.Name = "ContextMenuItemChannelStart";
            this.ContextMenuItemChannelStart.Size = new System.Drawing.Size(256, 22);
            this.ContextMenuItemChannelStart.Text = "Start channel(s)";
            this.ContextMenuItemChannelStart.Click += new System.EventHandler(this.startChannelsToolStripMenuItem1_Click);
            // 
            // ContextMenuItemChannelStop
            // 
            this.ContextMenuItemChannelStop.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemChannelStop.Image")));
            this.ContextMenuItemChannelStop.Name = "ContextMenuItemChannelStop";
            this.ContextMenuItemChannelStop.Size = new System.Drawing.Size(256, 22);
            this.ContextMenuItemChannelStop.Text = "Stop channel(s)";
            this.ContextMenuItemChannelStop.Click += new System.EventHandler(this.stopChannelsToolStripMenuItem1_Click);
            // 
            // ContextMenuItemChannelReset
            // 
            this.ContextMenuItemChannelReset.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemChannelReset.Image")));
            this.ContextMenuItemChannelReset.Name = "ContextMenuItemChannelReset";
            this.ContextMenuItemChannelReset.Size = new System.Drawing.Size(256, 22);
            this.ContextMenuItemChannelReset.Text = "Reset channel(s)";
            this.ContextMenuItemChannelReset.Click += new System.EventHandler(this.resetChannelsToolStripMenuItem1_Click);
            // 
            // ContextMenuItemChannelDelete
            // 
            this.ContextMenuItemChannelDelete.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemChannelDelete.Image")));
            this.ContextMenuItemChannelDelete.Name = "ContextMenuItemChannelDelete";
            this.ContextMenuItemChannelDelete.Size = new System.Drawing.Size(256, 22);
            this.ContextMenuItemChannelDelete.Text = "Delete channel(s)";
            this.ContextMenuItemChannelDelete.Click += new System.EventHandler(this.deleteChannelsToolStripMenuItem1_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(253, 6);
            // 
            // ContextMenuItemChannelCopyIngestURLToClipboard
            // 
            this.ContextMenuItemChannelCopyIngestURLToClipboard.Name = "ContextMenuItemChannelCopyIngestURLToClipboard";
            this.ContextMenuItemChannelCopyIngestURLToClipboard.Size = new System.Drawing.Size(256, 22);
            this.ContextMenuItemChannelCopyIngestURLToClipboard.Text = "Copy Input URL to clipboard";
            this.ContextMenuItemChannelCopyIngestURLToClipboard.Click += new System.EventHandler(this.copyIngestURLToClipboardToolStripMenuItem_Click);
            // 
            // ContextMenuItemChannelCopyPreviewURLToClipboard
            // 
            this.ContextMenuItemChannelCopyPreviewURLToClipboard.Name = "ContextMenuItemChannelCopyPreviewURLToClipboard";
            this.ContextMenuItemChannelCopyPreviewURLToClipboard.Size = new System.Drawing.Size(256, 22);
            this.ContextMenuItemChannelCopyPreviewURLToClipboard.Text = "Copy Preview URL to clipboard";
            this.ContextMenuItemChannelCopyPreviewURLToClipboard.Click += new System.EventHandler(this.copyPreviewURLToClipboardToolStripMenuItem_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(253, 6);
            // 
            // playbackTheProgramToolStripMenuItem
            // 
            this.playbackTheProgramToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.withFlashOSMFAzurePlayerToolStripMenuItem,
            this.withSilverlightMontoringPlayerToolStripMenuItem});
            this.playbackTheProgramToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("playbackTheProgramToolStripMenuItem.Image")));
            this.playbackTheProgramToolStripMenuItem.Name = "playbackTheProgramToolStripMenuItem";
            this.playbackTheProgramToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.playbackTheProgramToolStripMenuItem.Text = "Playback the Preview";
            // 
            // withFlashOSMFAzurePlayerToolStripMenuItem
            // 
            this.withFlashOSMFAzurePlayerToolStripMenuItem.Name = "withFlashOSMFAzurePlayerToolStripMenuItem";
            this.withFlashOSMFAzurePlayerToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.withFlashOSMFAzurePlayerToolStripMenuItem.Text = "with Flash OSMF Azure Player";
            this.withFlashOSMFAzurePlayerToolStripMenuItem.Click += new System.EventHandler(this.withFlashOSMFAzurePlayerToolStripMenuItem_Click_2);
            // 
            // withSilverlightMontoringPlayerToolStripMenuItem
            // 
            this.withSilverlightMontoringPlayerToolStripMenuItem.Name = "withSilverlightMontoringPlayerToolStripMenuItem";
            this.withSilverlightMontoringPlayerToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.withSilverlightMontoringPlayerToolStripMenuItem.Text = "with Silverlight Monitoring Player";
            this.withSilverlightMontoringPlayerToolStripMenuItem.Click += new System.EventHandler(this.withSilverlightMontoringPlayerToolStripMenuItem_Click_2);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(326, 177);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(33, 13);
            this.label15.TabIndex = 52;
            this.label15.Text = "Time:";
            // 
            // comboBoxFilterTimeProgram
            // 
            this.comboBoxFilterTimeProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFilterTimeProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterTimeProgram.FormattingEnabled = true;
            this.comboBoxFilterTimeProgram.Location = new System.Drawing.Point(362, 174);
            this.comboBoxFilterTimeProgram.Name = "comboBoxFilterTimeProgram";
            this.comboBoxFilterTimeProgram.Size = new System.Drawing.Size(102, 21);
            this.comboBoxFilterTimeProgram.TabIndex = 51;
            this.comboBoxFilterTimeProgram.SelectedIndexChanged += new System.EventHandler(this.comboBoxTimeProgram_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(480, 178);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 13);
            this.label16.TabIndex = 50;
            this.label16.Text = "Status:";
            // 
            // comboBoxStatusProgram
            // 
            this.comboBoxStatusProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStatusProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatusProgram.FormattingEnabled = true;
            this.comboBoxStatusProgram.Location = new System.Drawing.Point(523, 174);
            this.comboBoxStatusProgram.Name = "comboBoxStatusProgram";
            this.comboBoxStatusProgram.Size = new System.Drawing.Size(121, 21);
            this.comboBoxStatusProgram.TabIndex = 49;
            this.comboBoxStatusProgram.SelectedIndexChanged += new System.EventHandler(this.comboBoxStatusProgram_SelectedIndexChanged);
            // 
            // buttonSetFilterProgram
            // 
            this.buttonSetFilterProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetFilterProgram.Location = new System.Drawing.Point(240, 174);
            this.buttonSetFilterProgram.Name = "buttonSetFilterProgram";
            this.buttonSetFilterProgram.Size = new System.Drawing.Size(75, 23);
            this.buttonSetFilterProgram.TabIndex = 48;
            this.buttonSetFilterProgram.Text = "Set filter";
            this.buttonSetFilterProgram.UseVisualStyleBackColor = true;
            this.buttonSetFilterProgram.Click += new System.EventHandler(this.buttonSetFilterProgram_Click);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 178);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 13);
            this.label17.TabIndex = 47;
            this.label17.Text = "Search in name:";
            // 
            // textBoxSearchNameProgram
            // 
            this.textBoxSearchNameProgram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearchNameProgram.Location = new System.Drawing.Point(100, 175);
            this.textBoxSearchNameProgram.Name = "textBoxSearchNameProgram";
            this.textBoxSearchNameProgram.Size = new System.Drawing.Size(139, 20);
            this.textBoxSearchNameProgram.TabIndex = 46;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(659, 178);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 13);
            this.label18.TabIndex = 45;
            this.label18.Text = "Order by:";
            // 
            // comboBoxOrderProgram
            // 
            this.comboBoxOrderProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOrderProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrderProgram.FormattingEnabled = true;
            this.comboBoxOrderProgram.Location = new System.Drawing.Point(712, 174);
            this.comboBoxOrderProgram.Name = "comboBoxOrderProgram";
            this.comboBoxOrderProgram.Size = new System.Drawing.Size(102, 21);
            this.comboBoxOrderProgram.TabIndex = 44;
            this.comboBoxOrderProgram.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrderProgram_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "Programs";
            // 
            // dataGridViewProgramsV
            // 
            this.dataGridViewProgramsV.AllowUserToAddRows = false;
            this.dataGridViewProgramsV.AllowUserToDeleteRows = false;
            this.dataGridViewProgramsV.AllowUserToResizeRows = false;
            this.dataGridViewProgramsV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProgramsV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProgramsV.ChannelSourceIDs = ((System.Collections.Generic.List<string>)(resources.GetObject("dataGridViewProgramsV.ChannelSourceIDs")));
            this.dataGridViewProgramsV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProgramsV.ContextMenuStrip = this.contextMenuStripPrograms;
            this.dataGridViewProgramsV.FilterState = "All";
            this.dataGridViewProgramsV.ItemsPerPage = 50;
            this.dataGridViewProgramsV.Location = new System.Drawing.Point(6, 25);
            this.dataGridViewProgramsV.Name = "dataGridViewProgramsV";
            this.dataGridViewProgramsV.OrderItemsInGrid = "Last modified";
            this.dataGridViewProgramsV.ReadOnly = true;
            this.dataGridViewProgramsV.RowHeadersVisible = false;
            this.dataGridViewProgramsV.SearchInName = "";
            this.dataGridViewProgramsV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProgramsV.Size = new System.Drawing.Size(994, 143);
            this.dataGridViewProgramsV.TabIndex = 31;
            this.dataGridViewProgramsV.TimeFilter = "Last week";
            this.dataGridViewProgramsV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProgramV_CellDoubleClick);
            this.dataGridViewProgramsV.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewProgramV_CellFormatting);
            // 
            // contextMenuStripPrograms
            // 
            this.contextMenuStripPrograms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuItemProgramDisplayInformation,
            this.displayRelatedAssetInformationToolStripMenuItem,
            this.createProgramToolStripMenuItem,
            this.ContextMenuItemProgramStart,
            this.ContextMenuItemProgramStop,
            this.ContextMenuItemProgramDelete,
            this.toolStripSeparator16,
            this.publishToolStripMenuItem2,
            this.copyTheOutputURLToClipboardToolStripMenuItem,
            this.ContextMenuItemProgramPlayback});
            this.contextMenuStripPrograms.Name = "contextMenuStripPrograms";
            this.contextMenuStripPrograms.Size = new System.Drawing.Size(263, 208);
            // 
            // ContextMenuItemProgramDisplayInformation
            // 
            this.ContextMenuItemProgramDisplayInformation.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemProgramDisplayInformation.Image")));
            this.ContextMenuItemProgramDisplayInformation.Name = "ContextMenuItemProgramDisplayInformation";
            this.ContextMenuItemProgramDisplayInformation.Size = new System.Drawing.Size(262, 22);
            this.ContextMenuItemProgramDisplayInformation.Text = "Program information and settings...";
            this.ContextMenuItemProgramDisplayInformation.Click += new System.EventHandler(this.displayProgramInformationToolStripMenuItem1_Click);
            // 
            // displayRelatedAssetInformationToolStripMenuItem
            // 
            this.displayRelatedAssetInformationToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.Display_information;
            this.displayRelatedAssetInformationToolStripMenuItem.Name = "displayRelatedAssetInformationToolStripMenuItem";
            this.displayRelatedAssetInformationToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.displayRelatedAssetInformationToolStripMenuItem.Text = "Display related asset information...";
            this.displayRelatedAssetInformationToolStripMenuItem.Click += new System.EventHandler(this.displayRelatedAssetInformationToolStripMenuItem_Click);
            // 
            // createProgramToolStripMenuItem
            // 
            this.createProgramToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createProgramToolStripMenuItem.Image")));
            this.createProgramToolStripMenuItem.Name = "createProgramToolStripMenuItem";
            this.createProgramToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.createProgramToolStripMenuItem.Text = "Create program...";
            this.createProgramToolStripMenuItem.Click += new System.EventHandler(this.createProgramToolStripMenuItem_Click_1);
            // 
            // ContextMenuItemProgramStart
            // 
            this.ContextMenuItemProgramStart.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemProgramStart.Image")));
            this.ContextMenuItemProgramStart.Name = "ContextMenuItemProgramStart";
            this.ContextMenuItemProgramStart.Size = new System.Drawing.Size(262, 22);
            this.ContextMenuItemProgramStart.Text = "Start program(s)";
            this.ContextMenuItemProgramStart.Click += new System.EventHandler(this.startProgramsToolStripMenuItem_Click);
            // 
            // ContextMenuItemProgramStop
            // 
            this.ContextMenuItemProgramStop.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemProgramStop.Image")));
            this.ContextMenuItemProgramStop.Name = "ContextMenuItemProgramStop";
            this.ContextMenuItemProgramStop.Size = new System.Drawing.Size(262, 22);
            this.ContextMenuItemProgramStop.Text = "Stop program(s)";
            this.ContextMenuItemProgramStop.Click += new System.EventHandler(this.stopProgramsToolStripMenuItem1_Click);
            // 
            // ContextMenuItemProgramDelete
            // 
            this.ContextMenuItemProgramDelete.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemProgramDelete.Image")));
            this.ContextMenuItemProgramDelete.Name = "ContextMenuItemProgramDelete";
            this.ContextMenuItemProgramDelete.Size = new System.Drawing.Size(262, 22);
            this.ContextMenuItemProgramDelete.Text = "Delete program(s)";
            this.ContextMenuItemProgramDelete.Click += new System.EventHandler(this.deleteProgramsToolStripMenuItem_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(259, 6);
            // 
            // publishToolStripMenuItem2
            // 
            this.publishToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createALocatorToolStripMenuItem2,
            this.deleteAllLocatorsToolStripMenuItem1});
            this.publishToolStripMenuItem2.Name = "publishToolStripMenuItem2";
            this.publishToolStripMenuItem2.Size = new System.Drawing.Size(262, 22);
            this.publishToolStripMenuItem2.Text = "Publish";
            // 
            // createALocatorToolStripMenuItem2
            // 
            this.createALocatorToolStripMenuItem2.Image = global::AMSExplorer.Bitmaps.streaming_locator;
            this.createALocatorToolStripMenuItem2.Name = "createALocatorToolStripMenuItem2";
            this.createALocatorToolStripMenuItem2.Size = new System.Drawing.Size(176, 22);
            this.createALocatorToolStripMenuItem2.Text = "Create a locator...";
            this.createALocatorToolStripMenuItem2.Click += new System.EventHandler(this.createALocatorToolStripMenuItem2_Click);
            // 
            // deleteAllLocatorsToolStripMenuItem1
            // 
            this.deleteAllLocatorsToolStripMenuItem1.Image = global::AMSExplorer.Bitmaps.delete;
            this.deleteAllLocatorsToolStripMenuItem1.Name = "deleteAllLocatorsToolStripMenuItem1";
            this.deleteAllLocatorsToolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
            this.deleteAllLocatorsToolStripMenuItem1.Text = "Delete all locators...";
            this.deleteAllLocatorsToolStripMenuItem1.Click += new System.EventHandler(this.deleteAllLocatorsToolStripMenuItem1_Click);
            // 
            // copyTheOutputURLToClipboardToolStripMenuItem
            // 
            this.copyTheOutputURLToClipboardToolStripMenuItem.Name = "copyTheOutputURLToClipboardToolStripMenuItem";
            this.copyTheOutputURLToClipboardToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.copyTheOutputURLToClipboardToolStripMenuItem.Text = "Copy the output URL to clipboard";
            this.copyTheOutputURLToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyTheOutputURLToClipboardToolStripMenuItem_Click);
            // 
            // ContextMenuItemProgramPlayback
            // 
            this.ContextMenuItemProgramPlayback.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuItemProgramPlaybackWithFlashOSMFAzure,
            this.ContextMenuItemProgramPlaybackWithSilverlightMontoring,
            this.withDASHLiveAzurePlayerToolStripMenuItem,
            this.withCustomPlayerToolStripMenuItem2});
            this.ContextMenuItemProgramPlayback.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemProgramPlayback.Image")));
            this.ContextMenuItemProgramPlayback.Name = "ContextMenuItemProgramPlayback";
            this.ContextMenuItemProgramPlayback.Size = new System.Drawing.Size(262, 22);
            this.ContextMenuItemProgramPlayback.Text = "Playback the program";
            // 
            // ContextMenuItemProgramPlaybackWithFlashOSMFAzure
            // 
            this.ContextMenuItemProgramPlaybackWithFlashOSMFAzure.Name = "ContextMenuItemProgramPlaybackWithFlashOSMFAzure";
            this.ContextMenuItemProgramPlaybackWithFlashOSMFAzure.Size = new System.Drawing.Size(250, 22);
            this.ContextMenuItemProgramPlaybackWithFlashOSMFAzure.Text = "with Flash OSMF Azure Player";
            this.ContextMenuItemProgramPlaybackWithFlashOSMFAzure.Click += new System.EventHandler(this.withFlashOSMFAzurePlayerToolStripMenuItem_Click_1);
            // 
            // ContextMenuItemProgramPlaybackWithSilverlightMontoring
            // 
            this.ContextMenuItemProgramPlaybackWithSilverlightMontoring.Name = "ContextMenuItemProgramPlaybackWithSilverlightMontoring";
            this.ContextMenuItemProgramPlaybackWithSilverlightMontoring.Size = new System.Drawing.Size(250, 22);
            this.ContextMenuItemProgramPlaybackWithSilverlightMontoring.Text = "with Silverlight Monitoring Player";
            this.ContextMenuItemProgramPlaybackWithSilverlightMontoring.Click += new System.EventHandler(this.withSilverlightMontoringPlayerToolStripMenuItem_Click_1);
            // 
            // withDASHLiveAzurePlayerToolStripMenuItem
            // 
            this.withDASHLiveAzurePlayerToolStripMenuItem.Name = "withDASHLiveAzurePlayerToolStripMenuItem";
            this.withDASHLiveAzurePlayerToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.withDASHLiveAzurePlayerToolStripMenuItem.Text = "with DASH Live Azure Player";
            this.withDASHLiveAzurePlayerToolStripMenuItem.Click += new System.EventHandler(this.withDASHLiveAzurePlayerToolStripMenuItem_Click);
            // 
            // withCustomPlayerToolStripMenuItem2
            // 
            this.withCustomPlayerToolStripMenuItem2.Name = "withCustomPlayerToolStripMenuItem2";
            this.withCustomPlayerToolStripMenuItem2.Size = new System.Drawing.Size(250, 22);
            this.withCustomPlayerToolStripMenuItem2.Text = "with Custom Player";
            this.withCustomPlayerToolStripMenuItem2.Click += new System.EventHandler(this.withCustomPlayerToolStripMenuItem2_Click);
            // 
            // tabPageProcessors
            // 
            this.tabPageProcessors.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageProcessors.Controls.Add(this.dataGridViewProcessors);
            this.tabPageProcessors.Location = new System.Drawing.Point(4, 22);
            this.tabPageProcessors.Name = "tabPageProcessors";
            this.tabPageProcessors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProcessors.Size = new System.Drawing.Size(1006, 443);
            this.tabPageProcessors.TabIndex = 4;
            this.tabPageProcessors.Text = "Processors";
            // 
            // dataGridViewProcessors
            // 
            this.dataGridViewProcessors.AllowUserToAddRows = false;
            this.dataGridViewProcessors.AllowUserToDeleteRows = false;
            this.dataGridViewProcessors.AllowUserToResizeRows = false;
            this.dataGridViewProcessors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProcessors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProcessors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProcessors.Location = new System.Drawing.Point(5, 6);
            this.dataGridViewProcessors.Name = "dataGridViewProcessors";
            this.dataGridViewProcessors.ReadOnly = true;
            this.dataGridViewProcessors.RowHeadersVisible = false;
            this.dataGridViewProcessors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProcessors.Size = new System.Drawing.Size(997, 431);
            this.dataGridViewProcessors.TabIndex = 33;
            // 
            // tabPageOrigins
            // 
            this.tabPageOrigins.Controls.Add(this.dataGridViewOriginsV);
            this.tabPageOrigins.Location = new System.Drawing.Point(4, 22);
            this.tabPageOrigins.Name = "tabPageOrigins";
            this.tabPageOrigins.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOrigins.Size = new System.Drawing.Size(1006, 443);
            this.tabPageOrigins.TabIndex = 7;
            this.tabPageOrigins.Text = "Streaming endpoints";
            this.tabPageOrigins.UseVisualStyleBackColor = true;
            // 
            // dataGridViewOriginsV
            // 
            this.dataGridViewOriginsV.AllowUserToAddRows = false;
            this.dataGridViewOriginsV.AllowUserToDeleteRows = false;
            this.dataGridViewOriginsV.AllowUserToResizeRows = false;
            this.dataGridViewOriginsV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOriginsV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewOriginsV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOriginsV.ContextMenuStrip = this.contextMenuStripStreaminEndpoint;
            this.dataGridViewOriginsV.FilterOriginsState = "All";
            this.dataGridViewOriginsV.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewOriginsV.Name = "dataGridViewOriginsV";
            this.dataGridViewOriginsV.OrderOriginsInGrid = "Last modified";
            this.dataGridViewOriginsV.OriginsPerPage = 50;
            this.dataGridViewOriginsV.ReadOnly = true;
            this.dataGridViewOriginsV.RowHeadersVisible = false;
            this.dataGridViewOriginsV.SearchInName = "";
            this.dataGridViewOriginsV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOriginsV.Size = new System.Drawing.Size(994, 431);
            this.dataGridViewOriginsV.TabIndex = 0;
            this.dataGridViewOriginsV.TimeFilter = "Last week";
            this.dataGridViewOriginsV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOriginsV_CellDoubleClick);
            this.dataGridViewOriginsV.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewOriginsV_CellFormatting);
            // 
            // contextMenuStripStreaminEndpoint
            // 
            this.contextMenuStripStreaminEndpoint.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuItemOriginDisplayInformation,
            this.createStreamingEndpointToolStripMenuItem,
            this.ContextMenuItemOriginStart,
            this.ContextMenuItemOriginStop,
            this.ContextMenuItemOriginDelete});
            this.contextMenuStripStreaminEndpoint.Name = "contextMenuStripOrigins";
            this.contextMenuStripStreaminEndpoint.Size = new System.Drawing.Size(341, 114);
            // 
            // ContextMenuItemOriginDisplayInformation
            // 
            this.ContextMenuItemOriginDisplayInformation.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemOriginDisplayInformation.Image")));
            this.ContextMenuItemOriginDisplayInformation.Name = "ContextMenuItemOriginDisplayInformation";
            this.ContextMenuItemOriginDisplayInformation.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.ContextMenuItemOriginDisplayInformation.Size = new System.Drawing.Size(340, 22);
            this.ContextMenuItemOriginDisplayInformation.Text = "Streaming endpoint information and settings...";
            this.ContextMenuItemOriginDisplayInformation.Click += new System.EventHandler(this.displayOriginInformationToolStripMenuItem1_Click);
            // 
            // createStreamingEndpointToolStripMenuItem
            // 
            this.createStreamingEndpointToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createStreamingEndpointToolStripMenuItem.Image")));
            this.createStreamingEndpointToolStripMenuItem.Name = "createStreamingEndpointToolStripMenuItem";
            this.createStreamingEndpointToolStripMenuItem.Size = new System.Drawing.Size(340, 22);
            this.createStreamingEndpointToolStripMenuItem.Text = "Create streaming endpoint...";
            this.createStreamingEndpointToolStripMenuItem.Click += new System.EventHandler(this.createStreamingEndpointToolStripMenuItem_Click);
            // 
            // ContextMenuItemOriginStart
            // 
            this.ContextMenuItemOriginStart.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemOriginStart.Image")));
            this.ContextMenuItemOriginStart.Name = "ContextMenuItemOriginStart";
            this.ContextMenuItemOriginStart.Size = new System.Drawing.Size(340, 22);
            this.ContextMenuItemOriginStart.Text = "Start streaming endpoint(s)";
            this.ContextMenuItemOriginStart.Click += new System.EventHandler(this.startOriginsToolStripMenuItem1_Click);
            // 
            // ContextMenuItemOriginStop
            // 
            this.ContextMenuItemOriginStop.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemOriginStop.Image")));
            this.ContextMenuItemOriginStop.Name = "ContextMenuItemOriginStop";
            this.ContextMenuItemOriginStop.Size = new System.Drawing.Size(340, 22);
            this.ContextMenuItemOriginStop.Text = "Stop streaming endpoint(s)";
            this.ContextMenuItemOriginStop.Click += new System.EventHandler(this.stopOriginsToolStripMenuItem1_Click);
            // 
            // ContextMenuItemOriginDelete
            // 
            this.ContextMenuItemOriginDelete.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuItemOriginDelete.Image")));
            this.ContextMenuItemOriginDelete.Name = "ContextMenuItemOriginDelete";
            this.ContextMenuItemOriginDelete.Size = new System.Drawing.Size(340, 22);
            this.ContextMenuItemOriginDelete.Text = "Delete streaming endpoint(s)";
            this.ContextMenuItemOriginDelete.Click += new System.EventHandler(this.deleteOriginsToolStripMenuItem1_Click);
            // 
            // tabPageChart
            // 
            this.tabPageChart.Controls.Add(this.progressBarChart);
            this.tabPageChart.Controls.Add(this.chart);
            this.tabPageChart.Controls.Add(this.buttonbuildchart);
            this.tabPageChart.Location = new System.Drawing.Point(4, 22);
            this.tabPageChart.Name = "tabPageChart";
            this.tabPageChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageChart.Size = new System.Drawing.Size(1006, 443);
            this.tabPageChart.TabIndex = 5;
            this.tabPageChart.Text = "Chart";
            this.tabPageChart.UseVisualStyleBackColor = true;
            // 
            // progressBarChart
            // 
            this.progressBarChart.Location = new System.Drawing.Point(134, 19);
            this.progressBarChart.Name = "progressBarChart";
            this.progressBarChart.Size = new System.Drawing.Size(158, 23);
            this.progressBarChart.TabIndex = 38;
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(5, 51);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(997, 373);
            this.chart.TabIndex = 37;
            this.chart.Text = "chartJobs";
            // 
            // buttonbuildchart
            // 
            this.buttonbuildchart.Location = new System.Drawing.Point(8, 19);
            this.buttonbuildchart.Name = "buttonbuildchart";
            this.buttonbuildchart.Size = new System.Drawing.Size(105, 23);
            this.buttonbuildchart.TabIndex = 36;
            this.buttonbuildchart.Text = "Build jobs chart";
            this.buttonbuildchart.UseVisualStyleBackColor = true;
            this.buttonbuildchart.Click += new System.EventHandler(this.buttonbuildchart_Click);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(824, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(174, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "(Use ctrl or shift to multiselect items)";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 58);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerMain.Panel1.Controls.Add(this.tabControlMain);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerMain.Panel2.Controls.Add(this.richTextBoxLog);
            this.splitContainerMain.Size = new System.Drawing.Size(1020, 556);
            this.splitContainerMain.SplitterDistance = 478;
            this.splitContainerMain.TabIndex = 33;
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLog.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTextBoxLog.ContextMenuStrip = this.contextMenuStripLog;
            this.richTextBoxLog.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(1014, 68);
            this.richTextBoxLog.TabIndex = 6;
            this.richTextBoxLog.Text = "";
            this.richTextBoxLog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxLog_LinkClicked);
            this.richTextBoxLog.TextChanged += new System.EventHandler(this.richTextBoxLog_TextChanged);
            // 
            // contextMenuStripLog
            // 
            this.contextMenuStripLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem,
            this.clearTextToolStripMenuItem});
            this.contextMenuStripLog.Name = "contextMenuStripLog";
            this.contextMenuStripLog.Size = new System.Drawing.Size(170, 48);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // clearTextToolStripMenuItem
            // 
            this.clearTextToolStripMenuItem.Name = "clearTextToolStripMenuItem";
            this.clearTextToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.clearTextToolStripMenuItem.Text = "Clear text";
            this.clearTextToolStripMenuItem.Click += new System.EventHandler(this.clearTextToolStripMenuItem_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
            this.buttonRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRefresh.Location = new System.Drawing.Point(337, 30);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(92, 28);
            this.buttonRefresh.TabIndex = 23;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1020, 639);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "Mainform";
            this.Text = "{0} - Azure Media Services Explorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mainform_FormClosing);
            this.Load += new System.EventHandler(this.Mainform_Load);
            this.Shown += new System.EventHandler(this.Mainform_Shown);
            this.contextMenuStripAssets.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.contextMenuStripJobs.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransfer)).EndInit();
            this.contextMenuStripTransfers.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageAssets.ResumeLayout(false);
            this.tabPageAssets.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAssetsV)).EndInit();
            this.tabPageTransfers.ResumeLayout(false);
            this.tabPageJobs.ResumeLayout(false);
            this.tabPageJobs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJobsV)).EndInit();
            this.tabPageLive.ResumeLayout(false);
            this.splitContainerLive.Panel1.ResumeLayout(false);
            this.splitContainerLive.Panel1.PerformLayout();
            this.splitContainerLive.Panel2.ResumeLayout(false);
            this.splitContainerLive.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLive)).EndInit();
            this.splitContainerLive.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChannelsV)).EndInit();
            this.contextMenuStripChannels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProgramsV)).EndInit();
            this.contextMenuStripPrograms.ResumeLayout(false);
            this.tabPageProcessors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessors)).EndInit();
            this.tabPageOrigins.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOriginsV)).EndInit();
            this.contextMenuStripStreaminEndpoint.ResumeLayout(false);
            this.tabPageChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.contextMenuStripLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogDownload;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem assetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadFromASingleFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromASingleFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromMultipleFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encodingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelJobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedAssetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allAssetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayJobInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem allJobsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedJobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem samplePlayersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem silverlightMonitoringPlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dASHIFHTML5ReferencePlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iVXHLSPlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem azureManagementPortalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encodeAssetWithAzureMediaEncoderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oSMFToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem packageSmoothStreamingTOHLSstaticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encryptWithPlayReadystaticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateMultiMP4AssetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decryptAssetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem azureMediaServicesPlayerPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTML5VideoElementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dynamicPackagingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem publishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createALocatorForTheAssetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllLocatorsOfTheAssetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encodeAssetWithImagineZeniumToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxPageAssets;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxPageJobs;
        private System.Windows.Forms.ComboBox comboBoxOrderAssets;
        private System.Windows.Forms.ComboBox comboBoxOrderJobs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Button butPrevPageAsset;
        private System.Windows.Forms.Button butNextPageAsset;
        private System.Windows.Forms.Button butPrevPageJob;
        private System.Windows.Forms.Button butNextPageJob;
        private System.Windows.Forms.DataGridView dataGridViewTransfer;
        private DataGridViewAssets dataGridViewAssetsV;
        private DataGridViewJobs dataGridViewJobsV;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAssets;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemAssetDelete;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemAssetDisplayInfo;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemAssetImportFileFromAzure;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemAssetRename;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemAssetDownloadToLocal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripJobs;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemJobDisplayInfo;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemJobCancel;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemJobDelete;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemStorageDecrypter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageAssets;
        private System.Windows.Forms.TabPage tabPageJobs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxStateJobs;
        private System.Windows.Forms.ToolStripMenuItem createReportEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemJobCreateOutlookReportEmail;
        private System.Windows.Forms.ToolStripMenuItem displayInformationForAKnownJobIdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayInformationForAKnownAssetIdToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageTransfers;
        private System.Windows.Forms.Button buttonJobSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxJobSearch;
        private System.Windows.Forms.Button buttonAssetSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxAssetSearch;
        private System.Windows.Forms.TabPage tabPageProcessors;
        private System.Windows.Forms.DataGridView dataGridViewProcessors;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxStateAssets;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTransfers;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemTransferOpenDest;
        private System.Windows.Forms.ToolStripMenuItem transferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDestinationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem priorityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemJobChangePriority;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxFilterAssetsTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxFilterJobsTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem encodeAssetsWithAzureMediaEncoderToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem indexAssetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorBottomIndex;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemIndexer;
        private System.Windows.Forms.ToolStripMenuItem playbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withFlashOSMFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withSilverlightMMPPFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withMPEGDASHIFRefPlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withMPEGDASHAzurePlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemAssetPlayback;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemPlaybackWithFlashOSMFAzure;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemPlaybackWithSilverlightMonitoring;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemPlaybackWithMPEGDASHIFReference;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemPlaybackWithMPEGDASHAzure;
        private System.Windows.Forms.ToolStripMenuItem createOutlookReportEmailToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemAssetCreateOutlookReportEmail;
        private System.Windows.Forms.ToolStripMenuItem processAssetsadvancedModeWithToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemGenericProcessor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemJobOpenOutputAsset;
        private System.Windows.Forms.ToolStripMenuItem inputAssetInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputAssetInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemJobInputAssetInformation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem copyAssetFilesToAzureStorageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemAssetExportAssetFilesToAzureStorage;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromAzureStorageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromASingleHTTPURLAmazonS3EtcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toAzureStorageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToLocalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setupAWatchFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelWatchFolder;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem azureMediaHelpFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem azureMediaServicesMSDNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem azureMediaServicesForumToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageChart;
        private System.Windows.Forms.ProgressBar progressBarChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Button buttonbuildchart;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageLive;
        private DataGridViewLiveChannel dataGridViewChannelsV;
        private System.Windows.Forms.ToolStripMenuItem liveChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripMenuItem channToolStripMenuItem;
        private DataGridViewLiveProgram dataGridViewProgramsV;
        private System.Windows.Forms.ToolStripMenuItem createProgramToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem startProgramsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stopProgramsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteProgramsToolStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitContainerLive;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolStripMenuItem displayProgramInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateThumbnailsForTheAssetsToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageOrigins;
        private DataGridViewOrigins dataGridViewOriginsV;
        private System.Windows.Forms.ToolStripMenuItem originToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayOriginInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startOriginsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createOriginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopOriginsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteOriginsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripChannels;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemChannelDisplayInfomation;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemChannelStart;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemChannelStop;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemChannelReset;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemChannelDelete;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPrograms;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemProgramDisplayInformation;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemProgramStart;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemProgramStop;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemProgramDelete;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripStreaminEndpoint;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemOriginDisplayInformation;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemOriginStart;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemOriginStop;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemOriginDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemChannelCopyIngestURLToClipboard;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemChannelCopyPreviewURLToClipboard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemProgramPlayback;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemProgramPlaybackWithFlashOSMFAzure;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemProgramPlaybackWithSilverlightMontoring;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemThumbnails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripMenuItem batchUploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem azureMediaBlogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createChannelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripMenuItem playbackTheProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withFlashOSMFAzurePlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withSilverlightMontoringPlayerToolStripMenuItem;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboBoxFilterTimeProgram;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboBoxStatusProgram;
        private System.Windows.Forms.Button buttonSetFilterProgram;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBoxSearchNameProgram;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox comboBoxOrderProgram;
        private System.Windows.Forms.ToolStripMenuItem createStreamingEndpointToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripMenuItem setupDynamicEncryptionForTheAssetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLog;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeSelectedAssetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeAssetsToANewAssetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeDynamicEncryptionForTheAssetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemZenium;
        private System.Windows.Forms.ToolStripMenuItem publishToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem createALocatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllLocatorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateTheMultiMP4AssetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripMenuItem setupDynamicEncryptionForTheAssetsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeDynamicEncryptionForTheAssetsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTheOutputURLToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withDASHLiveAzurePlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jwPlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSE;
        private System.Windows.Forms.ToolStripMenuItem withCustomPlayerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem withCustomPlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem publishToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem createALocatorToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteAllLocatorsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem displayRelatedAssetInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withCustomPlayerToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem displayRelatedAssetInformationToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem withDASHLiveAzurePlayerToolStripMenuItem1;
    }
}

