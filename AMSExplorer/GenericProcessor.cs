﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class GenericProcessor : Form
    {
        public XDocument doc;
        public List<IAsset> SelectedAssets;

        private Bitmap bitmap_multitasksinglejob = Bitmaps.modetaskjob1;
        private Bitmap bitmap_multitasksmultijobs = Bitmaps.modetaskjob2;
        private Bitmap bitmap_singletasksinglejob = Bitmaps.modetaskjob3;

        private List<IMediaProcessor> Procs;


        public string EncodingJobName
        {
            get
            {
                return textBoxJobName.Text;
            }
            set
            {
                textBoxJobName.Text = value;
            }
        }


        public List<IMediaProcessor> EncodingProcessorsList
        {
            set
            {
                listViewProcessors.BeginUpdate();
                foreach (IMediaProcessor proc in value)
                {
                    ListViewItem item = new ListViewItem(proc.Vendor, 0);
                    item.SubItems.Add(proc.Name);
                    item.SubItems.Add(proc.Version);
                    item.SubItems.Add(proc.Description);
                    listViewProcessors.Items.Add(item);

                }
                listViewProcessors.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewProcessors.EndUpdate();
                Procs = value;
            }
        }

        public IMediaProcessor EncodingProcessorSelected
        {
            get
            {
                return Procs[listViewProcessors.SelectedIndices[0]];
            }
        }

        public string EncodingOutputAssetName
        {
            get
            {
                return textboxoutputassetname.Text;
            }
            set
            {
                textboxoutputassetname.Text = value;
            }
        }


        public string EncodingConfiguration
        {
            get
            {
                return textBoxConfiguration.Text;
            }
        }

        public int EncodingPriority
        {
            get
            {
                return (int)numericUpDownPriority.Value;
            }
            set
            {
                numericUpDownPriority.Value = value;
            }

        }

        public TaskJobCreationMode EncodingCreationMode
        {
            get
            {
                if (radioButtonMultipleTasksMultipleJobs.Checked) return TaskJobCreationMode.MultipleTasks_MultipleJobs;
                else if (radioButtonMultipleTasksSingleJob.Checked) return TaskJobCreationMode.MultipleTasks_SingleJob;
                else return TaskJobCreationMode.SingleTask_SingleJob;

            }
            set
            {
                switch (value)
                {
                    case TaskJobCreationMode.MultipleTasks_MultipleJobs:
                        radioButtonMultipleTasksMultipleJobs.Checked = true;
                        break;

                    case TaskJobCreationMode.MultipleTasks_SingleJob:
                        radioButtonMultipleTasksSingleJob.Checked = true;
                        break;

                    case TaskJobCreationMode.SingleTask_SingleJob:
                        radioButtonSingleTaskSingleJob.Checked = true;
                        break;
                }

            }

        }



        public GenericProcessor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialogPreset.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    doc = XDocument.Load(openFileDialogPreset.FileName);
                    textBoxConfiguration.Text = doc.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

            }
        }


        private void GenericProcessor_Load(object sender, EventArgs e)
        {
            UpdateJobSummary();
            UpdateWarning();
        }

        private void listViewProcessors_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = (listViewProcessors.SelectedItems.Count > 0);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJobSummary();
        }

        private void UpdateJobSummary()
        {

            labelsummaryjob.Text = "You are going to submit "
                + (radioButtonSingleTaskSingleJob.Checked ? "1 task" : listViewInputAssets.Items.Count.ToString() + " tasks")
            + " in "
            + (radioButtonMultipleTasksMultipleJobs.Checked ? listViewInputAssets.Items.Count.ToString() + " jobs" : "1 job")
            ;

            pictureBoxJob.Image = radioButtonSingleTaskSingleJob.Checked ? bitmap_singletasksinglejob : radioButtonMultipleTasksMultipleJobs.Checked ? bitmap_multitasksmultijobs : bitmap_multitasksinglejob;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void textBoxConfiguration_TextChanged(object sender, EventArgs e)
        {
            UpdateWarning();
        }

        private void UpdateWarning()
        {
            labelWarning.Text = (string.IsNullOrEmpty(textBoxConfiguration.Text)) ? "Note: the processor configuration string/XML is empty" : "";
        }

        private void GenericProcessor_Shown(object sender, EventArgs e)
        {

            int i = 0;
            listViewInputAssets.BeginUpdate();
            foreach (IAsset asset in SelectedAssets)
            {
                ListViewItem item = new ListViewItem(i.ToString(), 0);
                item.SubItems.Add(asset.Name);
                listViewInputAssets.Items.Add(item);
                i++;
            }
            listViewInputAssets.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewInputAssets.EndUpdate();

        }

    }
}
