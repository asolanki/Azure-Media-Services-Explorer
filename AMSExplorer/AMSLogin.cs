﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace AMSExplorer
{
    public partial class AMSLogin : Form
    {
        StringCollection CredentialsList;

        // strings for field API
        private const string _Default = "Default";
        private const string _Partner = "Partner";
        private const string _Other = "Other";

        public CredentialsEntry LoginCredentials
        {
            get
            {
                return new CredentialsEntry(
               textBoxAccountName.Text,
               textBoxAccountKey.Text,
               textBoxBlobKey.Text,
               textBoxDescription.Text,
               radioButtonPartner.Checked.ToString(),
               radioButtonOther.Checked.ToString(),
               textBoxAPIServer.Text,
               textBoxScope.Text,
               textBoxACSBaseAddress.Text,
               string.Empty);
            }
        }


        public AMSLogin()
        {
            InitializeComponent();
        }

        private void AMSLogin_Load(object sender, EventArgs e)
        {
            CredentialsList = Properties.Settings.Default.LoginList;
            try
            {
                if (CredentialsList != null)
                {
                    for (int i = 0; i < (CredentialsList.Count / CredentialsEntry.StringsCount); i++)
                        listBoxAcounts.Items.Add(CredentialsList[i * CredentialsEntry.StringsCount]);
                    buttonExportAll.Enabled = (listBoxAcounts.Items.Count > 0);
                }
                else
                {
                    // if null, let's create it
                    CredentialsList = new StringCollection();
                }
            }
            catch // error, let's purge all
            {
                MessageBox.Show("Error reading creadentials. Settings have been deleted.");
                Properties.Settings.Default.LoginList.Clear();
                Properties.Settings.Default.Save();
                listBoxAcounts.Items.Clear();
            }
            accountmgtlink.Links.Add(new LinkLabel.Link(0, accountmgtlink.Text.Length, "http://azure.microsoft.com/en-us/documentation/articles/media-services-create-account/"));
        }
        private void buttonSaveToList_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAccountName.Text))
            {
                MessageBox.Show("The account name cannot be empty.");
                return;
            }
            CredentialsEntry myCredentials = new CredentialsEntry(textBoxAccountName.Text, textBoxAccountKey.Text, textBoxBlobKey.Text, textBoxDescription.Text, radioButtonPartner.Checked.ToString(), radioButtonOther.Checked.ToString(), textBoxAPIServer.Text, textBoxScope.Text, textBoxACSBaseAddress.Text, "");
            if (CredentialsList == null) CredentialsList = new StringCollection();

            //let's find if the account name is already in the list
            int foundindex = -1;
            for (int i = 0; i < CredentialsList.Count; i += CredentialsEntry.StringsCount)
            {
                if (CredentialsList[i] == textBoxAccountName.Text)
                {
                    foundindex = i;
                    break;
                }
            }

            if (foundindex == -1) // not found
            {
                CredentialsList.AddRange(myCredentials.ToArray());
                Properties.Settings.Default.LoginList = CredentialsList;
                Properties.Settings.Default.Save();
                listBoxAcounts.Items.Add(myCredentials.AccountName);
            }
            else
            {
                //found, let's update the entry et insert the new data
                for (int i = 0; i < CredentialsEntry.StringsCount; i++) CredentialsList.RemoveAt(foundindex);
                for (int i = 0; i < CredentialsEntry.StringsCount; i++) CredentialsList.Insert(foundindex + i, myCredentials.ToArray().Skip(i).Take(1).FirstOrDefault());
                Properties.Settings.Default.LoginList = CredentialsList;
                Properties.Settings.Default.Save();

            }

        }

        private void buttonDeleteAccount_Click(object sender, EventArgs e)
        {
            int index = listBoxAcounts.SelectedIndex;
            try
            {
                for (int i = 0; i < CredentialsEntry.StringsCount; i++) CredentialsList.RemoveAt(index * CredentialsEntry.StringsCount);
                Properties.Settings.Default.LoginList = CredentialsList;
                Properties.Settings.Default.Save();
                listBoxAcounts.Items.Clear();
                if (CredentialsList != null)
                {
                    for (int i = 0; i < (CredentialsList.Count / CredentialsEntry.StringsCount); i++)
                        listBoxAcounts.Items.Add(CredentialsList[i * CredentialsEntry.StringsCount]);

                }
                buttonDeleteAccountEntry.Enabled = false; // no selected item, so login button not active

            }
            catch // error, let's purge all
            {
                CredentialsList.Clear();
                Properties.Settings.Default.LoginList.Clear();
                Properties.Settings.Default.Save();
                listBoxAcounts.Items.Clear();
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

        }

        private void listBoxAcounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDeleteAccountEntry.Enabled = (listBoxAcounts.SelectedIndex > -1); // no selected item, so login button not active
            buttonExportAll.Enabled = (listBoxAcounts.Items.Count > 0);
            if (listBoxAcounts.SelectedIndex > -1) // one selected
            {
                textBoxAccountName.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount];
                textBoxAccountKey.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 1];
                textBoxBlobKey.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 2];
                textBoxDescription.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 3];
                radioButtonPartner.Checked = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 4] == true.ToString() ? true : false;
                radioButtonOther.Checked = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 5] == true.ToString() ? true : false;
                textBoxAPIServer.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 6];
                textBoxScope.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 7];
                textBoxACSBaseAddress.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 8];

                // if not partner or other, then defaut
                if (!radioButtonPartner.Checked && !radioButtonOther.Checked) radioButtonProd.Checked = true;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            DoClearFields();
        }

        private void DoClearFields()
        {
            textBoxAccountName.Text = "";
            textBoxAccountKey.Text = "";
            textBoxBlobKey.Text = "";
            textBoxDescription.Text = "";
            textBoxACSBaseAddress.Text = "";
            textBoxAPIServer.Text = "";
            textBoxScope.Text = "";
            radioButtonProd.Checked = true;
            listBoxAcounts.ClearSelected();
        }

        private void radioButtonOther_CheckedChanged(object sender, EventArgs e)
        {
            textBoxACSBaseAddress.Enabled = radioButtonOther.Checked;
            textBoxAPIServer.Enabled = radioButtonOther.Checked;
            textBoxScope.Enabled = radioButtonOther.Checked;
        }

        private void buttonExportAll_Click(object sender, EventArgs e)
        {

            XDocument xmlexport = new XDocument();
            xmlexport.Add(new XComment("Created by Azure Media Services Explorer"));
            xmlexport.Add(new XElement("Credentials", new XAttribute("Version", "1.0")));

            for (int i = 0; i < (CredentialsList.Count / CredentialsEntry.StringsCount); i++)
                xmlexport.Descendants("Credentials").FirstOrDefault().Add(new XElement("Entry",
                    new XAttribute("AccountName", CredentialsList[i * CredentialsEntry.StringsCount]),
                      new XAttribute("AccountKey", CredentialsList[i * CredentialsEntry.StringsCount + 1]),
                   new XAttribute("StorageKey", CredentialsList[i * CredentialsEntry.StringsCount + 2]),
                   new XAttribute("Description", CredentialsList[i * CredentialsEntry.StringsCount + 3]),
                   new XAttribute("UsePartnerAPI", CredentialsList[i * CredentialsEntry.StringsCount + 4]),
                   new XAttribute("UseOtherAPI", CredentialsList[i * CredentialsEntry.StringsCount + 5]),
                   new XAttribute("OtherAPIServer", CredentialsList[i * CredentialsEntry.StringsCount + 6]),
                   new XAttribute("OtherScope", CredentialsList[i * CredentialsEntry.StringsCount + 7]),
                   new XAttribute("OtherACSBaseAddress", CredentialsList[i * CredentialsEntry.StringsCount + 8])
                   ));


            DialogResult diares = saveFileDialog1.ShowDialog();
            if (diares == DialogResult.OK)
            {
                System.IO.Stream myStream = saveFileDialog1.OpenFile();
                xmlexport.Save(myStream);
                myStream.Close();
            }
        }

        private void buttonImportAll_Click(object sender, EventArgs e)
        {
            XDocument xmlimport = new XDocument();
            bool mergesentries = false;

            if (CredentialsList != null)
            {
                if (CredentialsList.Count > 0) // There are entries. Let's ask if user want to delete them or merge
                {
                    if (System.Windows.Forms.MessageBox.Show("There are current entries in the list. Do you want replace them with the new ones or do a merge? Select 'Yes' to replace them, 'No' to merge them.", "Delete existing entries", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        mergesentries = true;
                    }
                }
            }

            DialogResult diares = openFileDialog1.ShowDialog();
            if (diares == DialogResult.OK)
            {
                System.IO.Stream myStream = openFileDialog1.OpenFile();
                xmlimport = XDocument.Load(myStream);

                var test = xmlimport.Descendants("Credentials").FirstOrDefault();
                Version version = new Version(xmlimport.Descendants("Credentials").Attributes("Version").FirstOrDefault().Value.ToString());

                if ((test != null) && (version == new Version("1.0")))
                {
                    if (!mergesentries)
                    {
                        CredentialsList.Clear(); // let's purge entries if user does not want to keep them
                    }
                    try
                    {
                        foreach (var att in xmlimport.Descendants("Credentials").Elements("Entry"))
                        {
                            CredentialsList.Add(att.Attribute("AccountName").Value.ToString());
                            CredentialsList.Add(att.Attribute("AccountKey").Value.ToString());
                            CredentialsList.Add(att.Attribute("StorageKey").Value.ToString());
                            CredentialsList.Add(att.Attribute("Description").Value.ToString());
                            CredentialsList.Add(att.Attribute("UsePartnerAPI").Value.ToString());
                            CredentialsList.Add(att.Attribute("UseOtherAPI").Value.ToString());
                            CredentialsList.Add(att.Attribute("OtherAPIServer").Value.ToString());
                            CredentialsList.Add(att.Attribute("OtherScope").Value.ToString());
                            CredentialsList.Add(att.Attribute("OtherACSBaseAddress").Value.ToString());
                            CredentialsList.Add(string.Empty);

                        }
                    }
                    catch
                    {
                        MessageBox.Show("File not recognized or incorrect.");
                        return;

                    }

                    listBoxAcounts.Items.Clear();
                    DoClearFields();

                    for (int i = 0; i < (CredentialsList.Count / CredentialsEntry.StringsCount); i++)
                        listBoxAcounts.Items.Add(CredentialsList[i * CredentialsEntry.StringsCount]);
                    buttonExportAll.Enabled = (listBoxAcounts.Items.Count > 0);


                    // let's save the list of credentials in settings
                    Properties.Settings.Default.LoginList = CredentialsList;
                    Properties.Settings.Default.Save();

                }
                else
                {
                    MessageBox.Show("Wrong XML file.");
                    return;
                }

            }
        }

        private void accountmgtlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
