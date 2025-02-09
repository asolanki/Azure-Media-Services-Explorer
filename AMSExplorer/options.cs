﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DisplayAssetIDinGrid = checkBoxDisplayAssetID.Checked;
            Properties.Settings.Default.DisplayJobIDinGrid = checkBoxDisplayJobID.Checked;
            Properties.Settings.Default.DisplayLiveChannelIDinGrid = checkBoxDisplayChannelID.Checked;
            Properties.Settings.Default.DisplayLiveProgramIDinGrid = checkBoxDisplayProgramID.Checked;
            Properties.Settings.Default.DisplayOriginIDinGrid = checkBoxDisplayOriginID.Checked;
            Properties.Settings.Default.AutoRefresh = checkBoxAutoRefresh.Checked;
            Properties.Settings.Default.AutoRefreshTime = Convert.ToInt16(comboBoxAutoRefreshTime.SelectedItem);

            Properties.Settings.Default.useProtectedConfiguration = checkBoxUseProtectedConfig.Checked;
            Properties.Settings.Default.useStorageEncryption = checkBoxUseStorageEncryption.Checked;
            Properties.Settings.Default.useTransferQueue = checkBoxOneUpDownload.Checked;
            Properties.Settings.Default.NbItemsDisplayedInGrid = Convert.ToInt16(comboBoxNbItems.SelectedItem.ToString());

            Properties.Settings.Default.CustomPlayerUrl = textBoxCustomPlayer.Text;
            Properties.Settings.Default.CustomPlayerEnabled = checkBoxEnableCustomPlayer.Checked;

            Properties.Settings.Default.DefaultJobPriority = (int)numericUpDownPriority.Value;

            Properties.Settings.Default.Save();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            checkBoxDisplayAssetID.Checked = false;
            checkBoxDisplayJobID.Checked = false;
            checkBoxDisplayChannelID.Checked = false;
            checkBoxDisplayOriginID.Checked = false;
            checkBoxDisplayProgramID.Checked = false;
            checkBoxAutoRefresh.Checked = false;
            comboBoxAutoRefreshTime.SelectedItem = "60";

            checkBoxUseProtectedConfig.Checked = false;
            checkBoxUseStorageEncryption.Checked = false;
            checkBoxOneUpDownload.Checked = true;

            int indexc = comboBoxNbItems.Items.IndexOf("50");
            if (indexc == -1) indexc = 1; // not found!
            comboBoxNbItems.SelectedIndex = indexc;

            textBoxCustomPlayer.Text = Constants.AMSPlayer + Constants.NameconvManifestURL;
            checkBoxEnableCustomPlayer.Checked = false;

            numericUpDownPriority.Value = 10;

            Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathAMEFiles; // we reset the XML files folder
            Properties.Settings.Default.Save();
        }

        private void options_Load(object sender, EventArgs e)
        {
            comboBoxNbItems.Items.AddRange(new string[] { "25", "50", "75", "100" });
            int indexc = comboBoxNbItems.Items.IndexOf(Properties.Settings.Default.NbItemsDisplayedInGrid.ToString());
            if (indexc == -1) indexc = 1; // not found!
            comboBoxNbItems.SelectedIndex = indexc;

            checkBoxDisplayAssetID.Checked = Properties.Settings.Default.DisplayAssetIDinGrid;
            checkBoxDisplayJobID.Checked = Properties.Settings.Default.DisplayJobIDinGrid;
            checkBoxDisplayChannelID.Checked = Properties.Settings.Default.DisplayLiveChannelIDinGrid;
            checkBoxDisplayProgramID.Checked = Properties.Settings.Default.DisplayLiveProgramIDinGrid;
            checkBoxDisplayOriginID.Checked = Properties.Settings.Default.DisplayOriginIDinGrid;
            checkBoxAutoRefresh.Checked = Properties.Settings.Default.AutoRefresh;
            comboBoxAutoRefreshTime.SelectedItem = Properties.Settings.Default.AutoRefreshTime.ToString();

            checkBoxUseProtectedConfig.Checked = Properties.Settings.Default.useProtectedConfiguration;
            checkBoxUseStorageEncryption.Checked = Properties.Settings.Default.useStorageEncryption;
            checkBoxOneUpDownload.Checked = Properties.Settings.Default.useTransferQueue;

            textBoxCustomPlayer.Text = Properties.Settings.Default.CustomPlayerUrl;
            checkBoxEnableCustomPlayer.Checked = Properties.Settings.Default.CustomPlayerEnabled;
            textBoxCustomPlayer.Enabled = checkBoxEnableCustomPlayer.Checked;

            numericUpDownPriority.Value = Properties.Settings.Default.DefaultJobPriority;
        }

        private void checkBoxEnableCustomPlayer_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCustomPlayer.Enabled = checkBoxEnableCustomPlayer.Checked;
        }
    }
}
